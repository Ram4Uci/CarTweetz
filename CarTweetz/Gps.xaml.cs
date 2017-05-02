using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CarTweetz
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Gps : Page
    {
        public static string myContent = "Please Wait";
        double Lat = 12.822030;            
        double Lon = 80.038429;
        public Gps()
        {
            this.InitializeComponent();
            myMap.Loaded += MyMap_Loaded;

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
          
        }
        private async void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            // center on Notre Dame Cathedral       
            myMap.Center=
                new Geopoint(new BasicGeoposition()
                {
                    Latitude = Lat,
                    Longitude = Lon

                });
            myMap.ZoomLevel = 20;
             myMap.Style = MapStyle.AerialWithRoads;
           

            // retrieve map
            await myMap.TrySetSceneAsync(MapScene.CreateFromLocationAndRadius(myMap.Center, 1000));
        }
        private void Fetch_Click(object sender, RoutedEventArgs e)
        {

            GetRequest("https://dweet.io/get/latest/dweet/for/paper_project_gps");
        myMap.Center = new Geopoint(new BasicGeoposition()
                {
                    Latitude = Lat,
                    Longitude = Lon

                });
           
        }

        async void GetRequest(string url)
        {
            JsonObject myContentObject;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        myContent = await content.ReadAsStringAsync();
                        myContentObject = JsonObject.Parse(myContent);
                        JsonArray jsonArray = myContentObject["with"].GetArray();
                        foreach (JsonValue groupvalue in jsonArray)
                        {
                            JsonObject withObject = groupvalue.GetObject();
                            JsonObject contentObject = withObject["content"].GetObject();
                            Lat = contentObject["LAT"].GetNumber();
                            Lon = contentObject["LON"].GetNumber();
                           

                        }

                    }
                }
            }
        }

        private void Ariel_Click(object sender, RoutedEventArgs e)
        {
            myMap.Style = MapStyle.AerialWithRoads;
        }

        private void Sat_Click(object sender, RoutedEventArgs e)
        {
            myMap.Style = MapStyle.Terrain;
        }

        private void Road_Click(object sender, RoutedEventArgs e)
        {
            myMap.Style = MapStyle.Road;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class Sensors : Page
    {
        public static string myContent = "Please Wait";

        public Sensors()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            GetRequest("https://dweet.io/get/latest/dweet/for/paper_project_sensor?key=2o68HSmhokV8OmUFGONzRm");
 
        }
        async void GetRequest(string url)
        {
            textBlock1.Visibility = Visibility.Visible;
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
                            double Panic = contentObject["Panic"].GetNumber();
                            double Alcohol = contentObject["Alcohol"].GetNumber();
                            double Crash = contentObject["Crash"].GetNumber();
                            if (Panic == 1)
                            { El_on_panic.Visibility = Visibility.Visible;
                                El_off_panic.Visibility = Visibility.Collapsed; }
                            else
                            { El_off_panic.Visibility = Visibility.Visible;
                                El_on_panic.Visibility = Visibility.Collapsed; }
                            if (Crash == 1)
                            { El_on_crash.Visibility = Visibility.Visible;
                                El_off_crash.Visibility = Visibility.Collapsed; }
                            else
                            {    El_off_crash.Visibility = Visibility.Visible;
                            El_on_crash.Visibility = Visibility.Collapsed; }
                            if (Alcohol == 0)
                            {
                                El_on_alcohol.Visibility = Visibility.Visible;
                                El_off_alcohol.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                El_off_alcohol.Visibility = Visibility.Visible;
                                El_on_alcohol.Visibility = Visibility.Collapsed;
                            }
                        }
                        }
                }
            }
            textBlock1.Visibility = Visibility.Collapsed;
        }
    }
}

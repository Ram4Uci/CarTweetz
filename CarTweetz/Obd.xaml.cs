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
    public sealed partial class Obd : Page
    {
        public static string myContent = "Please Wait";
        public Obd()
        {
            this.InitializeComponent();
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
        }

        private void Fetch_Click(object sender, RoutedEventArgs e)
        {
           
            GetRequest("https://dweet.io/get/latest/dweet/for/paper_project_obd"); 

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
                            double ERT = contentObject["ERT"].GetNumber();
                            double ECT = contentObject["ECT"].GetNumber();
                            double RPM = contentObject["RPM"].GetNumber();
                            double SPD = contentObject["SPD"].GetNumber();
                            string DTC = contentObject["DTC"].GetString();
                            Ect.Text = "ENGINE COOLANT TEMPERATURE = " + ECT.ToString() + " `C";
                            Ert.Text =  "ENGINE RUN TIME = " + ERT.ToString() + " secs";
                            Rpm.Text = "RPM = " + RPM.ToString() + " rev/min";
                            Spd.Text = "SPEED = " + SPD.ToString() + " Kmph";
                            Dtc.Text = "DIANOSTIC TROUBLE CODE = " + DTC;
                            if (SPD > slider.Value )
                                Spd.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                           
                        }

                    }
                }
            }
        }
    }
}

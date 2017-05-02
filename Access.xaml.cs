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
    public sealed partial class Access : Page
    {
        public static string myContent = "Please Wait";
        

        public Access()
        {
            this.InitializeComponent();
           
        }

        private void Remote_Toggled(object sender, RoutedEventArgs e)
        {
            
            if ( Remote.IsOn == true)
            {
                GetRequest("https://dweet.io/dweet/for/paper_project_access?Remote=ON");
                El_on.Visibility = Visibility.Visible;
                El_off.Visibility = Visibility.Collapsed;
            }
            else
            {
                GetRequest("https://dweet.io/dweet/for/paper_project_access?Remote=OFF");
                El_off.Visibility = Visibility.Visible;
                El_on.Visibility = Visibility.Collapsed;

            }
        }
        async void GetRequest(string url)
        {
            textBlock1.Visibility = Visibility.Visible;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        myContent = await content.ReadAsStringAsync();
                       
                    }
                }
            }
            textBlock1.Visibility = Visibility.Collapsed;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BlankPage1));
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
             GetRequest("https://dweet.io/dweet/for/paper_project_camera?Camera=ON");
        }
    }
}

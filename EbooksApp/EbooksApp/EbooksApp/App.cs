using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EbooksApp.ContentPages;
using EbooksApp.Utilities;
using Xamarin.Forms;

namespace EbooksApp
{
    public class App : Application
    {
        public static string PleaseWaitLocalizedString = string.Empty;

        public App()
        {
            // The root page of your application
            //MainPage = new ContentPage
            //{
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            }
            //        }
            //    }
            //};
            PleaseWaitLocalizedString = Constants.LOADING_MESSAGE_TEXT;

            MainPage = new NavigationPage(new EbooksListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

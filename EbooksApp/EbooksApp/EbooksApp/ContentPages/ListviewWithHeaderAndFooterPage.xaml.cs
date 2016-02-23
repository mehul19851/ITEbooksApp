using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbooksApp.ContentPages;
using EbooksApp.Models;
using EbooksApp.Utilities;
using EbooksApp.Views;
using Xamarin.Forms;

namespace EbooksApp.ContentPages
{
    public partial class ListviewWithHeaderAndFooterPage : ContentPage
    {
        public ListviewWithHeaderAndFooterPage()
        {
            InitializeComponent();

            listView.ItemTemplate = new DataTemplate(typeof(BooksCell));
            listView.HasUnevenRows = false;

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                listView.RowHeight = 360;
            }
            else
            {
                listView.RowHeight = 220;
            }

            listView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row
                if (e.Item == null) return;
                // do something with e.SelectedItem


                var bookListViewElement = (BooksModel)e.Item;
                DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, bookListViewElement.ID, Constants.DISPLAY_ALERT_CANCEL_TEXT);
                App.Current.Properties[Constants.BOOK_DETAILS_BOOK_ID] = bookListViewElement.ID;
                //Navigation.PushModalAsync(new NavigationPage(new EbooksDetailsPage()), true);
                Navigation.PushAsync(new NavigationPage(new EbooksDetailsPage()), true);
                ((ListView)sender).SelectedItem = null; // de-select the row

            };
        }
    }
}

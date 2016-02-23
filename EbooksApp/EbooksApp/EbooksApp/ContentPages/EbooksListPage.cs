using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using Acr.DeviceInfo;
using Acr.UserDialogs;
using EbooksApp.Utilities;
using EbooksApp.Models;
using EbooksApp.Services;
using System.Threading.Tasks;
using EbooksApp.Views;
using XLabs.Forms.Controls;

namespace EbooksApp.ContentPages
{
    public class EbooksListPage : ContentPage
    {
        ExtendedEntry searchEntry;
        ExtendedButton btnSearchBooks;
        List<BooksModel> booksList;
        int result;
        Grid booksContainerGrid;
        ListView booksListView;
        StackLayout booksListStackLayout;

        public EbooksListPage()
        {
            this.Icon = "";
            BackgroundColor = Color.FromHex(Constants.PAGE_BACKGROUND_COLOR);

            booksContainerGrid = new Grid
            {
                RowSpacing = 10,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            Grid booksSearchGrid = new Grid
            {
                Padding = new Thickness(0),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                }
            };

            Image logoImage = new Image
            {
                WidthRequest = 60,
                HeightRequest = 60,
                Aspect = Aspect.AspectFit,
                Source = "AppIcon.png",
                VerticalOptions = LayoutOptions.Start
            };

            var pageHeaderLabel = new ExtendedLabel
            {
                TextColor = Color.FromHex(Constants.PAGE_HEADER_LABEL_TEXT_COLOR),
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Text = Constants.PAGE_HEADER_TEXT,
                FontSize = 30,
                HorizontalTextAlignment = TextAlignment.Center
            };
            var lblListViewHeader = new Label
            {
                Text = "Navigation for books"
            };

            booksListView = new ListView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                booksListView.RowHeight = 360;
            }
            else
            {
                booksListView.RowHeight = 215;
            }

            booksListView.ItemTemplate = new DataTemplate(typeof(BooksCell));
            booksListView.HasUnevenRows = false;
            //booksListView.SeparatorVisibility = SeparatorVisibility.None;

            booksListView.ItemTapped += (object sender, ItemTappedEventArgs e) =>
            {
                // don't do anything if we just de-selected the row
                if (e.Item == null) return;
                // do something with e.SelectedItem


                var bookListViewElement = (BooksModel)e.Item;
                //DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, bookListViewElement.ID, Constants.DISPLAY_ALERT_CANCEL_TEXT);
                App.Current.Properties[Constants.BOOK_DETAILS_BOOK_ID] = bookListViewElement.ID;
                //Navigation.PushModalAsync(new NavigationPage(new EbooksDetailsPage()), true);
                Navigation.PushAsync(new NavigationPage(new EbooksDetailsPage()), true);
                ((ListView)sender).SelectedItem = null; // de-select the row

            };

            searchEntry = new ExtendedEntry
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Click here to search..",
                TextColor = Color.FromHex(Constants.EXTENDED_ENTRY_TEXT_COLOR),
                PlaceholderTextColor = Color.FromHex(Constants.PLACEHOLDER_TEXT_COLOR),
                HeightRequest = 50,
                Text = "wcf"

            };

            btnSearchBooks = new ExtendedButton
            {
                Text = "Search",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.FromHex(Constants.BUTTON_BACKGROUND_COLOR),
                TextColor = Color.FromHex(Constants.BUTTON_TEXT_COLOR),
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(ExtendedButton)),
                BorderRadius = 0,
                HeightRequest = 40,
            };

            btnSearchBooks.Clicked += btnSearchBooks_Clicked;

            booksSearchGrid.Children.Add(searchEntry, 0, 0);
            booksSearchGrid.Children.Add(btnSearchBooks, 1, 0);

            booksContainerGrid.Children.Add(booksSearchGrid, 0, 0);
            booksContainerGrid.Children.Add(booksListView, 0, 1);

            var headerLayout = new StackLayout
            {
                Padding = new Thickness(0),
                Spacing = 18,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
                Children = {
                    logoImage,
                    pageHeaderLabel
				}
            };

            Content = new StackLayout
            {
                Padding = new Thickness(20, 10, 20, 20),
                Orientation = StackOrientation.Vertical,
                Spacing = 5,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    headerLayout,
					booksContainerGrid
				}
            };
        }

        private async void btnSearchBooks_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchEntry.Text))
            {
                await DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, Constants.DISPLAY_ALERT_SEARCH_EMPTY_MESSAGE_TEXT, Constants.DISPLAY_ALERT_CANCEL_TEXT);
            }
            else
            {
                using (var dialog = UserDialogs.Instance.Loading(Constants.LOADING_MESSAGE_TEXT))
                {
                    result = await SearchAndGetBooksList(searchEntry.Text);

                    if (result == ErrorConstants.NO_NETWORK_CODE)
                    {
                        await DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, Constants.NO_NETWORK_ERROR_MESSAGE_TEXT, Constants.DISPLAY_ALERT_CANCEL_TEXT);
                    }
                    else if (result == ErrorConstants.NO_BOOKS_RETUNRED_CODE)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, Constants.NO_RECORDS_TEXT, Constants.DISPLAY_ALERT_CANCEL_TEXT);
                            RefreshBooksListView();
                        });
                    }
                    else if (result == ErrorConstants.NO_ERROR_CODE)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            RefreshBooksListView();
                        });
                    }
                }
            }
        }

        protected async Task<int> SearchAndGetBooksList(string searchQuery)
        {
            booksList = new List<BooksModel>();

            var booksSearchResult = await (new BooksService()).FetchBooks(searchQuery).ConfigureAwait(false);

            if (booksSearchResult.ErrorCode == ErrorConstants.NO_NETWORK_CODE)
            {
                return booksSearchResult.ErrorCode;
            }
            else if (booksSearchResult.ErrorCode == ErrorConstants.ERROR_WHILE_RETRIVING_DATA_CODE)
            {
                return booksSearchResult.ErrorCode;
            }
            else if (booksSearchResult.ErrorCode == ErrorConstants.NO_BOOKS_RETUNRED_CODE)
            {
                return booksSearchResult.ErrorCode;
            }
            else
            {
                booksList = booksSearchResult.BooksList.ToList() as List<BooksModel>;
                //booksList = booksSearchResult.BooksList.OrderByDescending(x => x.AnsweredDate).ToList() as List<Recomendation>;
                return 0;
            }

        }

        protected void RefreshBooksListView()
        {
            booksListView.ItemsSource = booksList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Acr.DeviceInfo;
using Acr.UserDialogs;
using EbooksApp.Models;
using EbooksApp.Services;
using EbooksApp.Utilities;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace EbooksApp.ContentPages
{
    public class EbooksDetailsPage : ContentPage
    {
        BookDetailsModel bookDetails;
        string bookDetailsBookID;
        Label labelTitle, labelSubTitle, labelDescription, labelAuthor, labelISBN, labelYear, labelPublisher, labelDescriptionCaption;
        Frame frameImage;
        Image imageBook, imageBookPopUp;
        ExtendedButton buttonDownload;
        Button button, buttonClosePopup;

        StackLayout bookDetailsLayout;

        PopupLayout popup;


        public EbooksDetailsPage()
        {
            BackgroundColor = Color.FromHex(Constants.BOOK_DETAILS_PAGE_BACKGROUND_COLOR);
            bookDetailsBookID = App.Current.Properties[Constants.BOOK_DETAILS_BOOK_ID].ToString();


            //button = new Button
            //{
            //    Text = "Open popup"
            //};

            buttonClosePopup = new Button
            {
                Text = "Close"
            };

            bookDetailsLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20),
            };

            IntializeControls();

            popup = new PopupLayout
            {
                //Content = new StackLayout
                //{
                //    VerticalOptions = LayoutOptions.Center,
                //    Children =
                //    {
                //        bookDetailsLayout
                //    }
                //}
                Content = bookDetailsLayout
            };

            Content = popup;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            using (var dialog = UserDialogs.Instance.Loading(App.PleaseWaitLocalizedString))
            {
                var bookDeatils = await GetBookDetails();

                Device.BeginInvokeOnMainThread(() =>
                {
                    SetBookImageAndFrameDimensions();
                    BindBookDetails(bookDeatils);
                });

            };

        }

        private void IntializeControls()
        {
            labelTitle = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                TextColor = Color.FromHex(Constants.BOOK_TITLE_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold
            };

            labelSubTitle = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                TextColor = Color.FromHex(Constants.BOOK_SUBTITLE_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start
            };

            labelDescription = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.FromHex(Constants.BOOK_DESCRIPTION_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start
            };

            labelAuthor = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                TextColor = Color.FromHex(Constants.BOOK_TITLE_TEXT_COLOR),
                //HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };

            labelISBN = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                TextColor = Color.FromHex(Constants.BOOK_TITLE_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
            };

            labelYear = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                TextColor = Color.FromHex(Constants.BOOK_TITLE_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
            };

            labelPublisher = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                TextColor = Color.FromHex(Constants.BOOK_TITLE_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold
            };

            imageBook = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.FillAndExpand,
                //Scale = 2
            };

            imageBookPopUp = new Image
            {
                Aspect = Aspect.AspectFill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Scale = 2
            };

            var popupView = new StackLayout
            {
                Padding = new Thickness(20),
                BackgroundColor = Color.FromHex(Constants.BOOK_DETAILS_PAGE_BACKGROUND_COLOR),
                Orientation = StackOrientation.Vertical,
                Spacing = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = { 
                    imageBookPopUp, buttonClosePopup
                }
            };


            var tgrImageZoom = new TapGestureRecognizer();

            tgrImageZoom.Tapped += async (sender, e) =>
            {
                //popup.ShowPopup(popupView, bookDetailsLayout, PopupLayout.PopupLocation.Top, 250, -170);
                popup.ShowPopup(popupView);//, Constraint.RelativeToParent(250), Constraint.RelativeToParent(150), null, null);
            };
            imageBook.GestureRecognizers.Add(tgrImageZoom);



            //var label = new Label()
            //{
            //    HeightRequest = 100,
            //    WidthRequest = 200
            //};



            //button.Clicked += async (sender, e) =>
            //{
            //    button.IsEnabled = false;
            //    popup.ShowPopup(popupView);
            //    //for (var i = 0; i < 5; i++)
            //    //{
            //    //    label.Text = string.Format("Disappearing in {0}s...", 5 - i);
            //    //    await Task.Delay(1000);
            //    //}

            //    //await Task.Delay(10000);
            //    //popup.DismissPopup();
            //    //button.IsEnabled = true;
            //};

            buttonClosePopup.Clicked += async (sender, e) =>
            {
                popup.DismissPopup();
                //button.IsEnabled = true;
            };

            bookDetailsLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(20),
            };



            frameImage = new Frame
            {
                //Padding = new Thickness(0),
                Content = imageBook,
                HasShadow = true,
                OutlineColor = Color.FromHex(Constants.BOOK_IMAGE_FRAME_BORDER_COLOR),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Start
            };

            labelDescriptionCaption = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                TextColor = Color.FromHex(Constants.BOOK_TITLE_TEXT_COLOR),
                //HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Text = "Description:"
            };

            buttonDownload = new ExtendedButton
            {
                Text = "Download",
                BorderRadius = 0,
                HeightRequest = 40,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex(Constants.BOOK_DETAILS_DOWNLOAD_BUTTON_BACKGROUND_COLOR),
                BorderColor = Color.Transparent,
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(ExtendedButton)) - 1
            };



        }

        protected async Task<BookDetailsModel> GetBookDetails()
        {
            bookDetails = new BookDetailsModel();

            using (var dialog = UserDialogs.Instance.Loading(Constants.LOADING_BOOKDETAILS_MESSAGE_TEXT))
            {
                bookDetails = await (new BookDetailsService()).FetchBookDetails(bookDetailsBookID).ConfigureAwait(false);

                if (bookDetails.ErrorCode == ErrorConstants.NO_NETWORK_CODE)
                {
                    await DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, Constants.NO_NETWORK_ERROR_MESSAGE_TEXT, Constants.DISPLAY_ALERT_CANCEL_TEXT);
                }
                else if (bookDetails.ErrorCode == ErrorConstants.NO_ERROR_CODE)
                {
                    //Device.BeginInvokeOnMainThread(() =>
                    //{
                    //    BindBookDetails(bookDetails);
                    //});
                }
                else
                {
                    await DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, bookDetails.BookError, Constants.DISPLAY_ALERT_CANCEL_TEXT);
                }
            }

            return bookDetails;

        }

        //protected async Task<BookDetailsModel> GetBookDetails()
        //{
        //    bookDetails = new BookDetailsModel();

        //    using (var dialog = UserDialogs.Instance.Loading(Constants.LOADING_MESSAGE_TEXT))
        //    {
        //        bookDetails = await (new BookDetailsService()).FetchBookDetails(bookDetailsBookID).ConfigureAwait(false);

        //        if (bookDetails == ErrorConstants.NO_NETWORK_CODE)
        //        {
        //            await DisplayAlert(Constants.DISPLAY_ALERT_TITLE_TEXT, Constants.NO_NETWORK_ERROR_MESSAGE_TEXT, Constants.DISPLAY_ALERT_CANCEL_TEXT);
        //        }
        //        else if (result == ErrorConstants.NO_ERROR_CODE)
        //        {
        //            Device.BeginInvokeOnMainThread(() =>
        //            {
        //                BindBookDetails(bookDetails);
        //            });
        //        }
        //    }

        //    return bookDetails;

        //}

        protected void BindBookDetails(BookDetailsModel bookDetails)
        {
            labelTitle.Text = bookDetails.Title;
            labelSubTitle.Text = bookDetails.SubTitle;
            labelDescription.Text = bookDetails.Description;
            labelAuthor.Text = "Author: " + bookDetails.Author;
            labelISBN.Text = "ISBN: " + bookDetails.ISBN;
            labelYear.Text = "Year: " + bookDetails.Year;
            labelPublisher.Text = "Publisher: " + bookDetails.Publisher;
            imageBook.Source = bookDetails.Image;
            imageBookPopUp.Source = bookDetails.Image;

            Grid booksTopSectionGrid = new Grid
            {
                ColumnSpacing = 20,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                    },
                ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = DeviceInfo.Hardware.ScreenWidth * 0.40},
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    }
            };

            Grid booksBottomSectionGrid = new Grid
            {
                ColumnSpacing = 20,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                    },
                ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = DeviceInfo.Hardware.ScreenWidth * 0.40},
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    }
            };

            booksTopSectionGrid.Children.Add(frameImage, 0, 0);
            Grid.SetRowSpan(frameImage, 3);

            //SetBookImageAndFrameDimensions();

            booksTopSectionGrid.Children.Add(labelTitle, 1, 0);
            booksTopSectionGrid.Children.Add(labelSubTitle, 1, 1);
            booksTopSectionGrid.Children.Add(buttonDownload, 1, 2);

            //buttonDownload.HeightRequest = booksTopSectionGrid.HeightRequest * 0.20;

            booksBottomSectionGrid.Children.Add(labelAuthor, 0, 0);
            Grid.SetColumnSpan(labelAuthor, 2);

            booksBottomSectionGrid.Children.Add(labelISBN, 0, 1);
            Grid.SetColumnSpan(labelISBN, 2);

            booksBottomSectionGrid.Children.Add(labelYear, 0, 2);
            Grid.SetColumnSpan(labelYear, 2);

            booksBottomSectionGrid.Children.Add(labelPublisher, 0, 3);
            Grid.SetColumnSpan(labelPublisher, 2);

            booksBottomSectionGrid.Children.Add(labelDescriptionCaption, 0, 4);
            Grid.SetColumnSpan(labelDescriptionCaption, 2);

            booksBottomSectionGrid.Children.Add(labelDescription, 0, 5);
            Grid.SetColumnSpan(labelDescription, 2);


            var scrollview = new ScrollView
            {
                Content = booksBottomSectionGrid
            };

            bookDetailsLayout.Children.Add(booksTopSectionGrid);
            bookDetailsLayout.Children.Add(scrollview);

        }

        private void SetBookImageAndFrameDimensions()
        {

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                imageBook.WidthRequest = Constants.BOOK_IMAGE_WIDTH;
                imageBook.HeightRequest = Constants.BOOK_IMAGE_HEIGHT;
                frameImage.WidthRequest = Constants.BOOK_FRAME_WIDTH;
                frameImage.HeightRequest = Constants.BOOK_FRAME_HEIGHT;
            }
            else
            {
                imageBook.WidthRequest = Constants.BOOK_IMAGE_WIDTH / 2;
                imageBook.HeightRequest = Constants.BOOK_IMAGE_HEIGHT / 2;
                frameImage.WidthRequest = Constants.BOOK_FRAME_WIDTH / 2;
                frameImage.HeightRequest = Constants.BOOK_FRAME_HEIGHT / 2;
            }
        }
    }
}

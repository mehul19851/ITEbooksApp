using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.DeviceInfo;
using EbooksApp.CustomViews;
using EbooksApp.Utilities;
using Xamarin.Forms;
using XLabs.Forms;
using XLabs.Forms.Controls;

namespace EbooksApp.Views
{
    public class BooksCell : ViewCell
    {
        Frame frameImage;
        Image imageBook;

        //double rdViewWidth = Device.OnPlatform(iOS: DeviceInfo.Hardware.ScreenWidth / 2, Android: display.Width, WinPhone: display.Width);
        public BooksCell()
        {
            Label labelBookTitle = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Small, typeof(Label)) - 2,
                TextColor = Color.FromHex(Constants.BOOK_TITLE_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start,
                FontAttributes = FontAttributes.Bold
            };
            labelBookTitle.SetBinding(Label.TextProperty, "Title");

            Label labelBookSubTitle = new Label
            {
                FontSize = Device.Idiom == TargetIdiom.Tablet ? Device.GetNamedSize(NamedSize.Medium, typeof(Label)) : Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                TextColor = Color.FromHex(Constants.BOOK_SUBTITLE_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start
            };
            labelBookSubTitle.SetBinding(Label.TextProperty, "SubTitle");

            Label labelBookDescription = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.FromHex(Constants.BOOK_DESCRIPTION_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.Start
            };
            labelBookDescription.SetBinding(Label.TextProperty, "Description");

            Label labelMoreDetails = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.FromHex(Constants.BOOK_DESCRIPTION_TEXT_COLOR),
                HorizontalTextAlignment = TextAlignment.End
            };
            labelMoreDetails.SetValue(Label.TextProperty, "See more >> ");
            

            imageBook = new Image
            {
                Aspect = Aspect.AspectFit,
                VerticalOptions = LayoutOptions.FillAndExpand,
                //Scale = 2
            };
            //imageBook.HeightRequest = imageBook.WidthRequest + 20;
            imageBook.SetBinding(Image.SourceProperty, "Image");

            frameImage = new Frame
            {
                //Padding = new Thickness(0),
                Content = imageBook,
                HasShadow = true,
                OutlineColor = Color.FromHex(Constants.BOOK_IMAGE_FRAME_BORDER_COLOR),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.Start
            };

            // Container Grid with 2 rows and 1 column
            Grid booksContainerGrid = new Grid
            {
                Padding = new Thickness(20),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                    //new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                }
            };

            SetBookImageAndFrameDimensions();
 

            Grid booksTopSectionGrid = new Grid
            {
                ColumnSpacing = 20,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                    {
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Auto)},
                        new RowDefinition { Height = new GridLength(1, GridUnitType.Star)},
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

            booksTopSectionGrid.Children.Add(labelBookTitle, 1, 0);
            booksTopSectionGrid.Children.Add(labelBookSubTitle, 1, 1);
            booksTopSectionGrid.Children.Add(labelMoreDetails, 0, 2);
            Grid.SetColumnSpan(labelMoreDetails, 2);

            booksContainerGrid.Children.Add(booksTopSectionGrid, 0, 0);

            

            RoundedBorderView rbBooksView = new RoundedBorderView
            {
                //WidthRequest = DeviceInfo.Hardware.ScreenWidth * 0.97,
                CornerRadius = 6, //Constants.BOOKS_ROUNDEDBORDERVIEW_BORDER_RADIUS,
                BackgroundColor = Color.FromHex(Constants.BOOKS_ROUNDEDBORDERVIEW_BORDER_BACKGROUND_COLOR),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Content = booksContainerGrid
            };

            var layout = new StackLayout
            {
                //BackgroundColor = Color.White, //Color.FromHex(Constants.BOOKLIST_BGCOLOR),
                //Padding = new Thickness(10),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                        rbBooksView
                }
            };

            View = layout;
        }

        protected override void OnBindingContextChanged()
        {
            // Fixme : this is happening because the View.Parent is getting 
            // set after the Cell gets the binding context set on it. Then it is inheriting
            // the parents binding context.
            View.BindingContext = BindingContext;
            base.OnBindingContextChanged();
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

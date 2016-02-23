using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EbooksApp.Models;
using EbooksApp.Utilities;
using Newtonsoft.Json.Linq;

namespace EbooksApp.Services
{
    public class BookDetailsService
    {
        public async Task<BookDetailsModel> FetchBookDetails(string bookID)
        {
            BookDetailsModel bookDetails = new BookDetailsModel();
            //BooksDTO results = new BooksDTO();
            //results.BooksList = new List<BooksModel>();

            try
            {
                Uri theUri = new Uri(Constants.BASE_URL + string.Format(Constants.BOOK_DETAILS_PART_URL, bookID));

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ACCEPT_HEADER_JSON));
                    httpClient.DefaultRequestHeaders.Host = theUri.Host;
                    //httpClient.DefaultRequestHeaders.TryAddWithoutValidation(Constants.AUTHORIZATION_HEADER_TEXT, Constants.BEARER_TEXT + accessToken);
                    string response = await httpClient.GetStringAsync(theUri).ConfigureAwait(false);
                    //var books = JValue.Parse(response)[Constants.RESPONSE_BOOKS_JSON_KEY];

                    var apiError = JValue.Parse(response)[Constants.RESPONSE_EROR_JSON_KEY];

                    if (apiError.ToString() == "0")
                    {
                        bookDetails = new BookDetailsModel();

                        var bookId = JValue.Parse(response)[Constants.BOOK_DETAILS_ID_JSON_KEY];
                        var bookTitle = JValue.Parse(response)[Constants.BOOK_DETAILS_TITLE_JSON_KEY];
                        var bookSubtitle = JValue.Parse(response)[Constants.BOOK_DETAILS_SUBTITLE_JSON_KEY];
                        var bookDescription = JValue.Parse(response)[Constants.BOOK_DETAILS_DESCRIPTION_JSON_KEY];
                        var bookAuthor = JValue.Parse(response)[Constants.BOOK_DETAILS_AUTHOR_JSON_KEY];
                        var bookISBN = JValue.Parse(response)[Constants.BOOK_DETAILS_ISBN_JSON_KEY];
                        var bookYear = JValue.Parse(response)[Constants.BOOK_DETAILS_YEAR_JSON_KEY];
                        var bookPage = JValue.Parse(response)[Constants.BOOK_DETAILS_PAGE_JSON_KEY];
                        var bookPublisher = JValue.Parse(response)[Constants.BOOK_DETAILS_PUBLISHER_JSON_KEY];
                        var bookImage = JValue.Parse(response)[Constants.BOOK_DETAILS_IMAGE_JSON_KEY];
                        var bookDownload = JValue.Parse(response)[Constants.BOOK_DETAILS_DOWNLOAD_JSON_KEY];

                        bookDetails.ID = bookId != null ? bookId.ToString() : string.Empty;
                        bookDetails.Title = bookTitle != null ? bookTitle.ToString() : string.Empty;
                        bookDetails.SubTitle = bookSubtitle != null ? bookSubtitle.ToString() : string.Empty;
                        bookDetails.Description = bookDescription != null ? bookDescription.ToString() : string.Empty;
                        bookDetails.Author = bookAuthor != null ? bookAuthor.ToString() : string.Empty;
                        bookDetails.ISBN = bookISBN != null ? bookISBN.ToString() : string.Empty;
                        bookDetails.Year = bookYear != null ? bookYear.ToString() : string.Empty;
                        bookDetails.Page = bookPage != null ? bookPage.ToString() : string.Empty;
                        bookDetails.Publisher = bookPublisher != null ? bookPublisher.ToString() : string.Empty;
                        bookDetails.Image = bookImage != null ? bookImage.ToString() : string.Empty;
                        bookDetails.Download = bookDownload != null ? bookDownload.ToString() : string.Empty;
                    }
                    else
                    {
                        bookDetails.BookError = apiError.ToString();
                        bookDetails.ErrorCode = ErrorConstants.NO_BOOKS_RETUNRED_CODE;
                        
                    }

                }

                //results.ErrorCode = ErrorConstants.NO_ERROR_CODE;
            }
            catch (WebException webEx)
            {
                if (webEx.Status == System.Net.WebExceptionStatus.ConnectFailure)
                {
                    bookDetails.ErrorCode = ErrorConstants.NO_NETWORK_CODE;
                }
                else
                {
                    bookDetails.ErrorCode = ErrorConstants.ERROR_WHILE_RETRIVING_DATA_CODE;
                }
            }
            catch (Exception ex)
            {
                bookDetails.ErrorCode = ErrorConstants.ERROR_WHILE_RETRIVING_DATA_CODE;
            }
            return bookDetails;
        }
    }
}

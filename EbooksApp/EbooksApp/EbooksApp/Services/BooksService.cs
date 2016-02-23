using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EbooksApp.Interfaces;
using EbooksApp.Models;
using EbooksApp.Utilities;
using Newtonsoft.Json.Linq;

namespace EbooksApp.Services
{
    public class BooksService
    {
        private readonly IHttpService _httpService;
        BooksModel book;

        //public BooksService(IHttpService httpService)
        //{
        //    _httpService = httpService;
        //}

        //public async Task<BooksDTO> GetBooksList(string searchQuery = "")
        //{
        //    //{"type":"Profile","id":"2","FullName":"hr (Henriette) Blaas","EmailAddress":"htorenbeek@vitalhealthsoftware.nl","CellPhoneNumber":"","PrimaryPhone":"","SecondaryPhone":"","_version":"2016-01-21T08:54:20:856.2259"
        //    Func<string, List<BooksModel>> deserializer = (jsonString) =>
        //    {
        //        var books = JValue.Parse(jsonString)[Constants.RESPONSE_BOOKS_JSON_KEY];
        //        var page = JValue.Parse(jsonString)[Constants.RESPONSE_PAGE_JSON_KEY];
        //        var totalBooksCount = JValue.Parse(jsonString)[Constants.RESPONSE_TOTAL_JSON_KEY];
        //        var apiError = JValue.Parse(jsonString)[Constants.RESPONSE_EROR_JSON_KEY];
        //        var time = JValue.Parse(jsonString)[Constants.RESPONSE_TIME_JSON_KEY];

        //        var booksList = new List<BooksModel>();

        //        //The API response returns "Error" as 0 when there is no error

        //        if (books != null && (int)apiError != 0)
        //        {
        //            foreach (var bookItem in books)
        //            {
        //                book = new BooksModel();
        //                var bookId = (int)bookItem[Constants.BOOK_ID_JSON_KEY];
        //                var bookTitle = bookItem[Constants.BOOK_TITLE_JSON_KEY];
        //                var bookSubtitle = bookItem[Constants.BOOK_SUBTITLE_JSON_KEY];
        //                var bookDescription = bookItem[Constants.BOOK_DESCRIPTION_JSON_KEY];
        //                var bookImage = bookItem[Constants.BOOK_IMAGE_JSON_KEY];
        //                var bookISBN = bookItem[Constants.BOOK_ISBN_JSON_KEY];

        //                book.ID = bookId;
        //                book.Title = bookTitle.ToString();
        //                book.SubTitle = bookSubtitle.ToString();
        //                book.Description = bookDescription.ToString();
        //                book.Image = bookImage.ToString();
        //                book.isbn = bookISBN.ToString();

        //                booksList.Add(book);
        //            }

        //            return booksList;
        //        }
        //        return null;
        //    };

        //    //Dictionary<string, string> queryParameters = new Dictionary<string, string>();
        //    //queryParameters.Add(Constants.INCLUDE_PARAM, Constants.PHOTO_PARAM_VALUE);
        //    //queryParameters.Add(Constants.MODIFIED_SINCE_PARAM, modifiedSince);

        //    var response = await _httpService.GetAsync<List<BooksModel>>(urlWithoutBaseUrl: string.Format(Constants.SEARCH_PART_URL, searchQuery),
        //        queryParameters: null, requiresAuthToken: false, authToken: null, deserializer: deserializer);

        //    if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //    {
        //        return response.Response;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        public async Task<BooksDTO> FetchBooks(string searchQuery)
        {
            BooksDTO results = new BooksDTO();
            results.BooksList = new List<BooksModel>();

            try
            {
                Uri theUri = new Uri(Constants.BASE_URL + string.Format(Constants.SEARCH_PART_URL, searchQuery));

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ACCEPT_HEADER_JSON));
                    httpClient.DefaultRequestHeaders.Host = theUri.Host;
                    //httpClient.DefaultRequestHeaders.TryAddWithoutValidation(Constants.AUTHORIZATION_HEADER_TEXT, Constants.BEARER_TEXT + accessToken);
                    string response = await httpClient.GetStringAsync(theUri).ConfigureAwait(false);
                    var books = JValue.Parse(response)[Constants.RESPONSE_BOOKS_JSON_KEY];

                    var total = JValue.Parse(response)[Constants.RESPONSE_TOTAL_JSON_KEY];
                    var apiError = JValue.Parse(response)[Constants.RESPONSE_EROR_JSON_KEY];

                    if ((int)total == 0 && apiError.ToString() == "0")
                    {
                        results.ErrorCode = ErrorConstants.NO_BOOKS_RETUNRED_CODE;
                    }
                    else
                    {
                        foreach (var bookItem in books)
                        {
                            book = new BooksModel();
                            var bookId = bookItem[Constants.BOOK_ID_JSON_KEY];
                            var bookTitle = bookItem[Constants.BOOK_TITLE_JSON_KEY];
                            var bookSubtitle = bookItem[Constants.BOOK_SUBTITLE_JSON_KEY];
                            var bookDescription = bookItem[Constants.BOOK_DESCRIPTION_JSON_KEY];
                            var bookImage = bookItem[Constants.BOOK_IMAGE_JSON_KEY];
                            var bookISBN = bookItem[Constants.BOOK_ISBN_JSON_KEY];

                            book.ID = bookId != null ? bookId.ToString() : string.Empty;
                            book.Title = bookTitle != null ? bookTitle.ToString() : string.Empty;
                            book.SubTitle = bookSubtitle != null ? bookSubtitle.ToString() : string.Empty;
                            book.Description = bookDescription != null ? bookDescription.ToString() : string.Empty;
                            book.Image = bookImage != null ? bookImage.ToString() : string.Empty;
                            book.isbn = bookISBN != null ? bookISBN.ToString() : string.Empty;

                            results.BooksList.Add(book);
                        }
                    }

                }

                //results.ErrorCode = ErrorConstants.NO_ERROR_CODE;
            }
            catch (WebException webEx)
            {
                if (webEx.Status == System.Net.WebExceptionStatus.ConnectFailure)
                {
                    results.ErrorCode = ErrorConstants.NO_NETWORK_CODE;
                }
                else
                {
                    results.ErrorCode = ErrorConstants.ERROR_WHILE_RETRIVING_DATA_CODE;
                }
            }
            catch (Exception ex)
            {
                results.ErrorCode = ErrorConstants.ERROR_WHILE_RETRIVING_DATA_CODE;
            }
            return results;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EbooksApp.Utilities
{
    public class Constants
    {
        public const string PAGE_BACKGROUND_COLOR = "#15605b";
        public const string BASE_URL = "http://it-ebooks-api.info/v1/";
        //public const string API_VERSION = "v1/";
        public const string SEARCH_PART_URL = "search/{0}";
        public const string BOOK_DETAILS_PART_URL = "book/{0}";
        //urlWithoutBaseUrl: string.Format(Constants.BOOK_DETAILS_PART, bookId)

        public const string ACCEPT_HEADER_JSON = "application/json";
        public const string CONTENT_TYPE_FORM_URLENCODED = "application/x-www-form-urlencoded";
        public const string AUTHORIZATION_HEADER_TEXT = "Authorization";
        public const string CONTENT_TYPE_HEADER_TEXT = "Content-Type";
        public const string BEARER_TEXT = "Bearer ";
        public const string BASIC_TEXT = "Basic ";
        public const string HTTP_POST = "POST";
        public const string HTTP_PATCH = "PATCH";
        
        //JSON KEYS
        public const string ACCESS_TOKEN_JSON_KEY = "access_token";
        public const string REFRESH_TOKEN_JSON_KEY = "refresh_token";
        public const string RESPONSE_BOOKS_JSON_KEY = "Books";
        public const string RESPONSE_EROR_JSON_KEY = "Error";
        public const string RESPONSE_TOTAL_JSON_KEY = "Total";
        public const string RESPONSE_TIME_JSON_KEY = "Time";
        public const string RESPONSE_PAGE_JSON_KEY = "Page";

        public const string RESPONSE_DATETIME_JSON_KEY = "datetime";
        public const string DATA_ID_JSON_KEY = "id";
        public const string SELF_JSON_KEY = "self";

        //JSON KEYS - BOOK MODEL
        public const string BOOK_ID_JSON_KEY = "ID";
        public const string BOOK_TITLE_JSON_KEY = "Title";
        public const string BOOK_SUBTITLE_JSON_KEY = "SubTitle";
        public const string BOOK_DESCRIPTION_JSON_KEY = "Description";
        public const string BOOK_IMAGE_JSON_KEY = "Image";
        public const string BOOK_ISBN_JSON_KEY = "isbn";

        //JSON KEYS - BOOK DETAILS MODEL
        public const string BOOK_DETAILS_ID_JSON_KEY = "ID";
        public const string BOOK_DETAILS_TITLE_JSON_KEY = "Title";
        public const string BOOK_DETAILS_SUBTITLE_JSON_KEY = "SubTitle";
        public const string BOOK_DETAILS_DESCRIPTION_JSON_KEY = "Description";
        public const string BOOK_DETAILS_AUTHOR_JSON_KEY = "Author";
        public const string BOOK_DETAILS_ISBN_JSON_KEY = "ISBN";
        public const string BOOK_DETAILS_YEAR_JSON_KEY = "Year";
        public const string BOOK_DETAILS_PAGE_JSON_KEY = "Page";
        public const string BOOK_DETAILS_PUBLISHER_JSON_KEY = "Publisher";
        public const string BOOK_DETAILS_IMAGE_JSON_KEY = "Image";
        public const string BOOK_DETAILS_DOWNLOAD_JSON_KEY = "Download";


        public const string DISPLAY_ALERT_TITLE_TEXT = "IT Ebooks";
        public const string DISPLAY_ALERT_SEARCH_EMPTY_MESSAGE_TEXT = "Please enter your search query";
        public const string DISPLAY_ALERT_CANCEL_TEXT = "Close";

        public const string LOADING_MESSAGE_TEXT = "Searching books, Please wait... ";
        public const string LOADING_BOOKDETAILS_MESSAGE_TEXT = "Getting book details, Please wait... ";

        public const string PAGE_HEADER_TEXT = "IT Ebooks";
        public const string PAGE_HEADER_LABEL_TEXT_COLOR = "#c9f3e8";

        public const string NO_NETWORK_ERROR_MESSAGE_TEXT = "Please check your internet connection and try again ... ";
        public const string NO_RECORDS_TEXT = "Oops... No books found!";
        public const string NO_RECORDS_LABEL_TEXT_COLOR = "#c9f3e8";

        public const string BOOKLIST_BGCOLOR = "#E6F2F2";
        public const string BOOK_TITLE_TEXT_COLOR = "#15605b";
        public const string BOOK_SUBTITLE_TEXT_COLOR = "#15605b";
        public const string BOOK_DESCRIPTION_TEXT_COLOR = "#267872";
        public const string BOOKS_LISTVIEW_SEPRATOR_COLOR = "#fff";

        public const string PLACEHOLDER_TEXT_COLOR = "#70dec2";
        public const string BUTTON_BACKGROUND_COLOR = "#3c908a";
        public const string BUTTON_TEXT_COLOR = "#FFFFFF";

        public const string EXTENDED_ENTRY_BACKGROUND_COLOR = "#FFFFFF";
        public const string EXTENDED_ENTRY_TEXT_COLOR = "#70dec2";
        public const string BOOK_IMAGE_FRAME_BORDER_COLOR = "#15605b";

        public const string BOOK_CELL_BACKGROUND_COLOR = "#f5fcf8";
        
        public const int BOOKS_ROUNDEDBORDERVIEW_BORDER_RADIUS = 10;
        public const string BOOKS_ROUNDEDBORDERVIEW_BORDER_BACKGROUND_COLOR = "#c9f3e8";

        public const int BOOK_IMAGE_WIDTH = 210;
        public const int BOOK_IMAGE_HEIGHT = 250;

        public const int BOOK_FRAME_WIDTH = 220;
        public const int BOOK_FRAME_HEIGHT = 260;

        public const string BOOK_DETAILS_BOOK_ID = "BookID";
        public const string BOOK_DETAILS_PAGE_BACKGROUND_COLOR = "#c9f3e8";
        public const string BOOK_DETAILS_DOWNLOAD_BUTTON_BACKGROUND_COLOR = "#15605b";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebooks.Utilities
{
    public class Constants
    {
        public const string BASE_URL = "http://it-ebooks-api.info/v1/";
        public const string API_VERSION = "v1/";
        public const string SEARCH_PART = "search/";
        public const string BOOK_DETAILS_PART = "book/{0}";
        //urlWithoutBaseUrl: string.Format(Constants.BOOK_DETAILS_PART, bookId)
    }
}

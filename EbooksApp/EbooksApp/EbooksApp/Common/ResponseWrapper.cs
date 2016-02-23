using System.Net;

namespace EbooksApp.Common
{
    public class ResponseWrapper<T>
    {
        /// <summary>
        /// Status of the HTTP request sent
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Error code, if the HTTP request was unsuccessful and throws an error
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Deserialized response in instance of type T
        /// </summary>
        public T Response { get; set; }
    }
}

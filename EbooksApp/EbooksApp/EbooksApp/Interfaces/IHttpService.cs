using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbooksApp.Common;

namespace EbooksApp.Interfaces
{
    /// <summary>
    /// Provides Api to construct HTTP request and recieve response based on the parameters provided.
    /// </summary>
    public interface IHttpService
    {
        /// <summary>
        /// Sends HTTP GET request, asynchronously, with queryParameters appended to url and returns data deserialized into instance of TReturn type wrapped into <typeparamref name="ResponseWrapper"/> 
        /// </summary>
        /// <typeparam name="TReturn">type to which response data is to be deserialized</typeparam>
        /// <param name="urlWithoutBaseUrl">url to which request is to be made. This will be appended to baseUrl.</param>
        /// <param name="queryParameters">query parameters to be appended to the url</param>
        /// <param name="requiresAuthToken">if the authorization header/basic authentication is required for successfull request</param>
        /// <param name="authToken">auth token, if authorization header/basic authentication is required</param>
        /// <param name="deserializer">function to execute to deserialize the string data to TReturn. If null, response string will be directly converted to TReturn</param>
        /// <param name="baseUrl">the base url of the request. if empty, will use the value from Constants</param>
        /// <returns>the deserialized data wrapped into instance of ResponseWrapper which also includes error code and status code</returns>
        Task<ResponseWrapper<TReturn>> GetAsync<TReturn>(string urlWithoutBaseUrl, Dictionary<string, string> queryParameters, bool requiresAuthToken, string authToken = "", Func<string, TReturn> deserializer = null, string baseUrl = "");

        /// <summary>
        /// Sends HTTP POST request, asynchronously, with postContent serialized to send with request body and returns data deserialized into instance of TReturn type wrapped into <typeparamref name="ResponseWrapper"/> 
        /// </summary>
        /// <typeparam name="T">type of the input data to send with request</typeparam>
        /// <typeparam name="TReturn">type to which response data is to be deserialized</typeparam>
        /// <param name="urlWithoutBaseUrl">url to which request is to be made. This will be appended to baseUrl.</param>
        /// <param name="postContent">patch data to serailize and send in the request body. if postContent is string, it assumes it is already JSON serialized</param>
        /// <param name="requiresAuthToken">if the authorization header/basic authentication is required for successfull request</param>
        /// <param name="contentType">value for Content-Type header. Json for application/json or else FormEncoded for application/x-www-form-urlencoded</param>
        /// <param name="authType">type of Authorization header. Bearer or basic</param>
        /// <param name="authToken">auth token, if authorization header/basic authentication is required</param>
        /// <param name="deserializer">function to execute to deserialize the string data to TReturn. If null, response string will be directly converted to TReturn</param>
        /// <param name="baseUrl">the base url of the request. if empty, will use the value from Constants</param>
        /// <returns>the deserialized data wrapped into instance of ResponseWrapper which also includes error code and status code</returns>
        Task<ResponseWrapper<TReturn>> PostAsync<T, TReturn>(string urlWithoutBaseUrl, T postContent, bool requiresAuthToken, HttpContentType contentType, AuthorizationType authType = AuthorizationType.Bearer, string authToken = "", Func<string, TReturn> deserializer = null, string baseUrl = "");

        /// <summary>
        /// Sends HTTP PATCH request, asynchronously, with patchContent serialized to send with request body and returns data deserialized into instance of TReturn type wrapped into <typeparamref name="ResponseWrapper"/> 
        /// </summary>
        /// <typeparam name="T">type of the input data to send with request</typeparam>
        /// <typeparam name="TReturn">type to which response data is to be deserialized</typeparam>
        /// <param name="urlWithoutBaseUrl">url to which request is to be made. This will be appended to baseUrl.</param>
        /// <param name="patchContent">patch data to serailize and send in the request body. if patchContent is string, it assumes it is already JSON serialized</param>
        /// <param name="requiresAuthToken">if the authorization header/basic authentication is required for successfull request</param>
        /// <param name="contentType">value for Content-Type header. Json for application/json or else FormEncoded for application/x-www-form-urlencoded</param>
        /// <param name="authType">type of Authorization header. Bearer or basic</param>
        /// <param name="authToken">auth token, if authorization header/basic authentication is required</param>
        /// <param name="deserializer">function to execute to deserialize the string data to TReturn. If null, response string will be directly converted to TReturn</param>
        /// <param name="baseUrl">the base url of the request. if empty, will use the value from Constants</param>
        /// <returns>the deserialized data wrapped into instance of ResponseWrapper which also includes error code and status code</returns>
        Task<ResponseWrapper<TReturn>> PatchAsync<T, TReturn>(string urlWithoutBaseUrl, T patchContent, bool requiresAuthToken, HttpContentType contentType, AuthorizationType authType = AuthorizationType.Bearer, string authToken = "", Func<string, TReturn> deserializer = null, string baseUrl = "");
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EbooksApp.Common;
using EbooksApp.Interfaces;
using System.Net.Http;
using EbooksApp.Utilities;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace EbooksApp.Services
{
    public class HttpService : IHttpService
    {
        /// <summary>
        /// Sends HTTP GET request, asynchronously, with queryParameters appended to url and returns data deserialized into instance of TReturn type wrapped into <typeparamref name="ResponseWrapper" />
        /// </summary>
        /// <typeparam name="TReturn">type to which response data is to be deserialized</typeparam>
        /// <param name="urlWithoutBaseUrl">url to which request is to be made. This will be appended to baseUrl.</param>
        /// <param name="queryParameters">query parameters to be appended to the url</param>
        /// <param name="requiresAuthToken">if the authorization header/basic authentication is required for successfull request</param>
        /// <param name="authToken">auth token, if authorization header/basic authentication is required</param>
        /// <param name="deserializer">function to execute to deserialize the string data to TReturn. If null, response string will be directly converted to TReturn</param>
        /// <param name="baseUrl">the base url of the request. if empty, will use the value from Constants</param>
        /// <returns>
        /// the deserialized data wrapped into instance of ResponseWrapper which also includes error code and status code
        /// </returns>
        public async Task<ResponseWrapper<TReturn>> GetAsync<TReturn>(string urlWithoutBaseUrl, Dictionary<string, string> queryParameters, bool requiresAuthToken, string authToken = "", Func<string, TReturn> deserializer = null, string baseUrl = "")
        {
            baseUrl = (baseUrl == "") ? Constants.BASE_URL : baseUrl;
            ResponseWrapper<TReturn> responseToSend = new ResponseWrapper<TReturn>();

            Uri requestUri = new Uri(baseUrl + urlWithoutBaseUrl);

            if (queryParameters != null && queryParameters.Count > 0)
            {
                //Create query params from dictionary to string "& seperated"
                string[] queryParamsKeyValue = new string[queryParameters.Count];
                var arrQueryParams = queryParameters.ToArray();
                for (int i = 0; i < arrQueryParams.Length; i++)
                {
                    queryParamsKeyValue[i] = arrQueryParams[i].Key + "=" + arrQueryParams[i].Value;
                }

                requestUri = new Uri(baseUrl + urlWithoutBaseUrl + "?" + string.Join("&", queryParamsKeyValue));
            }

            //HTTP request code
            using (var httpClient = new HttpClient(new ModernHttpClient.NativeMessageHandler()))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ACCEPT_HEADER_JSON));
                httpClient.DefaultRequestHeaders.Host = requestUri.Host;
                if (requiresAuthToken && authToken != "")
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(Constants.AUTHORIZATION_HEADER_TEXT, Constants.BEARER_TEXT + authToken);
                }
                var responseResult = await httpClient.GetAsync(requestUri);
                responseToSend.StatusCode = responseResult.StatusCode;
                if (responseResult.IsSuccessStatusCode)
                {
                    string responseString = await responseResult.Content.ReadAsStringAsync();
                    if (deserializer == null)
                    {
                        responseToSend.Response = JsonConvert.DeserializeObject<TReturn>(responseString);
                    }
                    else
                    {
                        responseToSend.Response = deserializer.Invoke(responseString);
                    }

                }
                else
                {
                    responseToSend.ErrorCode = ErrorConstants.STANDARD_ERROR;
                }
            }

            return responseToSend;
        }

        /// <summary>
        /// Sends HTTP POST request, asynchronously, with postContent serialized to send with request body and returns data deserialized into instance of TReturn type wrapped into <typeparamref name="ResponseWrapper" />
        /// </summary>
        /// <typeparam name="T">type of the input data to send with request</typeparam>
        /// <typeparam name="TReturn">type to which response data is to be deserialized</typeparam>
        /// <param name="urlWithoutBaseUrl">url to which request is to be made. This will be appended to baseUrl.</param>
        /// <param name="postContent">post data to serailize and send in the request body. if postContent is string, it assumes it is already JSON serialized</param>
        /// <param name="requiresAuthToken">if the authorization header/basic authentication is required for successfull request</param>
        /// <param name="contentType">value for Content-Type header. Json for application/json or else FormEncoded for application/x-www-form-urlencoded</param>
        /// <param name="authType">type of Authorization header. Bearer or basic</param>
        /// <param name="authToken">auth token, if authorization header/basic authentication is required</param>
        /// <param name="deserializer">function to execute to deserialize the string data to TReturn. If null, response string will be directly converted to TReturn</param>
        /// <param name="baseUrl">the base url of the request. if empty, will use the value from Constants</param>
        /// <returns>
        /// the deserialized data wrapped into instance of ResponseWrapper which also includes error code and status code
        /// </returns>
        public async Task<Common.ResponseWrapper<TReturn>> PostAsync<T, TReturn>(string urlWithoutBaseUrl, T postContent, bool requiresAuthToken, HttpContentType contentType, AuthorizationType authType = AuthorizationType.Bearer, string authToken = "", Func<string, TReturn> deserializer = null, string baseUrl = "")
        {
            var reponseToSend = await SendAsync<T, TReturn>(httpMethod: Constants.HTTP_POST,
                                                urlWithoutBaseUrl: urlWithoutBaseUrl,
                                                contentToSend: postContent,
                                                requiresAuthToken: requiresAuthToken,
                                                contentType: contentType,
                                                authType: authType,
                                                authToken: authToken,
                                                deserializer: deserializer,
                                                baseUrl: baseUrl);

            return reponseToSend;
        }

        /// <summary>
        /// Sends HTTP PATCH request, asynchronously, with patchContent serialized to send with request body and returns data deserialized into instance of TReturn type wrapped into <typeparamref name="ResponseWrapper" />
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
        /// <returns>
        /// the deserialized data wrapped into instance of ResponseWrapper which also includes error code and status code
        /// </returns>
        public async Task<Common.ResponseWrapper<TReturn>> PatchAsync<T, TReturn>(string urlWithoutBaseUrl, T patchContent, bool requiresAuthToken, HttpContentType contentType, AuthorizationType authType = AuthorizationType.Bearer, string authToken = "", Func<string, TReturn> deserializer = null, string baseUrl = "")
        {
            var reponseToSend = await SendAsync<T, TReturn>(httpMethod: Constants.HTTP_PATCH,
                                                urlWithoutBaseUrl: urlWithoutBaseUrl,
                                                contentToSend: patchContent,
                                                requiresAuthToken: requiresAuthToken,
                                                contentType: contentType,
                                                authType: authType,
                                                authToken: authToken,
                                                deserializer: deserializer,
                                                baseUrl: baseUrl);
            return reponseToSend;
        }

        private async Task<Common.ResponseWrapper<TReturn>> SendAsync<T, TReturn>(string httpMethod,
                                                                                    string urlWithoutBaseUrl,
                                                                                    T contentToSend,
                                                                                    bool requiresAuthToken,
                                                                                    HttpContentType contentType,
                                                                                    AuthorizationType authType = AuthorizationType.Bearer,
                                                                                    string authToken = "", Func<string, TReturn> deserializer = null, string baseUrl = "")
        {
            if (contentType == HttpContentType.FormEncoded && contentToSend.GetType() != typeof(List<KeyValuePair<string, string>>))
            {
                throw new ArgumentException("When content-type is application/x-www-form-urlencoded, the content must be sent of type List<KeyValuePair<string, string>> to serialize into corresponding format.");
            }

            baseUrl = (baseUrl == "") ? Constants.BASE_URL : baseUrl;
            ResponseWrapper<TReturn> responseToSend = new ResponseWrapper<TReturn>();

            Uri requestUri = new Uri(baseUrl + urlWithoutBaseUrl);

            //HTTP request code
            using (var httpClient = new HttpClient(new ModernHttpClient.NativeMessageHandler()))
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ACCEPT_HEADER_JSON));
                httpClient.DefaultRequestHeaders.Host = requestUri.Host;
                if (requiresAuthToken && authToken != "")
                {
                    string headerAuthType = (authType == AuthorizationType.Bearer) ? Constants.BEARER_TEXT : Constants.BASIC_TEXT;
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(Constants.AUTHORIZATION_HEADER_TEXT, headerAuthType + authToken);
                }

                ByteArrayContent requestBody;
                var method = new HttpMethod(httpMethod);
                if (contentType == HttpContentType.FormEncoded)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(Constants.CONTENT_TYPE_HEADER_TEXT, Constants.CONTENT_TYPE_FORM_URLENCODED);
                    requestBody = new FormUrlEncodedContent(contentToSend as IEnumerable<KeyValuePair<string, string>>);
                    //requestBody = new StringContent(contentToSend as string, System.Text.Encoding.UTF8, Constants.CONTENT_TYPE_FORM_URLENCODED);
                }
                else
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation(Constants.CONTENT_TYPE_HEADER_TEXT, Constants.ACCEPT_HEADER_JSON);
                    //if contentToSend is string, it assumes data is already JSON serialized
                    if (contentToSend.GetType() == typeof(String))
                    {
                        requestBody = new StringContent(contentToSend.ToString(), System.Text.Encoding.UTF8, Constants.ACCEPT_HEADER_JSON);
                    }
                    else
                    {
                        string postString = JsonConvert.SerializeObject(contentToSend, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                        requestBody = new StringContent(postString, System.Text.Encoding.UTF8, Constants.ACCEPT_HEADER_JSON);
                    }
                }

                var request = new HttpRequestMessage(method, requestUri) { Content = requestBody };

                var responseResult = await httpClient.SendAsync(request);
                responseToSend.StatusCode = responseResult.StatusCode;
                if (responseResult.IsSuccessStatusCode)
                {
                    string responseString = await responseResult.Content.ReadAsStringAsync();
                    if (deserializer == null)
                    {
                        responseToSend.Response = JsonConvert.DeserializeObject<TReturn>(responseString);
                    }
                    else
                    {
                        responseToSend.Response = deserializer.Invoke(responseString);
                    }

                }
                else
                {
                    responseToSend.ErrorCode = ErrorConstants.STANDARD_ERROR;
                }
            }

            return responseToSend;
        }
    }
}

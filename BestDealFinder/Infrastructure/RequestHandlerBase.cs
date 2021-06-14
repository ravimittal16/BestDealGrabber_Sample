﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BestDealFinder.Infrastructure.Contracts;
using BestDealFinder.Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BestDealFinder.Infrastructure
{
    public abstract class RequestHandlerBase
    {
        private readonly HttpClient _httpClient;
        protected RequestHandlerBase()
        {
            _httpClient = new HttpClient();
        }

        public HttpClient HttpClient => _httpClient;
        public abstract IShippingProviderApiDetails ShippingProviderApiDetails { get; }

        private string MediaType =>
            ShippingProviderApiDetails != null && ShippingProviderApiDetails.ResponseType == ResponseTypes.Json
                ? "application/json"
                : "application/xml";

        public JObject ParseToJsonObject(string json)
        {
            return JObject.Parse(json);
        }

        protected async Task MakeRequest(Func<string> requestData, Action<string> onSuccess)
        {
            try
            {
                if (ShippingProviderApiDetails == null) throw new ArgumentException("Api details are missing");
                //if we need to update the request, e.g. attaching specific request headers | credentials.
                var data = requestData?.Invoke();
                //  based on response type we can update the media type
                _httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue(MediaType));

                var stringContent = new StringContent(data, Encoding.UTF8, MediaType);

                var response = await _httpClient.PostAsync(new Uri(ShippingProviderApiDetails.ApiBaseUrl), stringContent);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    onSuccess?.Invoke(responseContent);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

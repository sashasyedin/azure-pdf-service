using Newtonsoft.Json;
using PdfService.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PdfService.Client
{
    public class PdfServiceClient : IPdfServiceClient
    {
        private readonly HttpClient _httpClient;

        public PdfServiceClient(HttpClient httpClient)
        {
            if (httpClient.BaseAddress == null)
            {
                throw new ArgumentNullException(nameof(httpClient.BaseAddress));
            }

            _httpClient = httpClient;
        }

        public async Task<ApiResponse<GenerateInvoiceResponse>> GenerateInvoice(GenerateInvoiceRequest request)
        {
            request = request ?? throw new ArgumentNullException(nameof(request));
            return await SendAsync<ApiResponse<GenerateInvoiceResponse>>(HttpMethod.Post, "/api/invoices", request);
        }

        private async Task<T> SendAsync<T>(HttpMethod method, string uri, object request) where T : class
        {
            var json = JsonConvert.SerializeObject(request);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            var message = new HttpRequestMessage(method, uri)
            {
                Content = stringContent
            };

            using (var result = await _httpClient.SendAsync(message))
            {
                var stringResponse = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<T>(stringResponse);
                return response;
            }
        }
    }
}
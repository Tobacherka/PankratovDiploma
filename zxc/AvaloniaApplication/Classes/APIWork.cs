using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication.Classes
{
    public static class APIWork
    {
        private static readonly HttpClient _httpClient = new();
        /// <summary>
        /// Статичный метод запроса к API
        /// </summary>
        public static async Task<List<string>> SendRequest(string command, string? parameter = null, string? parameter2 = null, string? parameter3 = null, string? parameter4 = null)
        {
            List<string> Answer = new List<string>();
            try
            {
                // Указываем адрес API
                var uriBuilder = Connection.ConnectionDB;

                // Создаем строку запроса
                var query = $"command={Uri.EscapeDataString(command)}";

                // Добавляем параметры, если они указаны
                if (parameter != null)
                    query += $"&parameter={Uri.EscapeDataString(parameter)}";
                if (parameter2 != null)
                    query += $"&parameter2={Uri.EscapeDataString(parameter2)}";
                if (parameter3 != null)
                    query += $"&parameter3={Uri.EscapeDataString(parameter3)}";
                if (parameter4 != null)
                    query += $"&parameter4={Uri.EscapeDataString(parameter4)}";

                uriBuilder.Query = query;

                // Создаем HTTP-запрос
                var request = new HttpRequestMessage(HttpMethod.Post, uriBuilder.Uri);
                request.Headers.Add("accept", "*/*");

                // Отправляем запрос и получаем ответ
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    Answer.Add(responseData);
                    return Answer;
                }
                else
                {
                    Debug.WriteLine("Failed to send request. Status code: " + response.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public static async Task<List<DbOrderDetail>?> GetProductsInCart()
        {
            //var uriBuilder = new UriBuilder("https://localhost:7250/Command/cart");
            //var query = $"userID = {GlobalBuffer.CurrentUserID}";
            //var request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
            //uriBuilder.Query = query;
            //var response = await _httpClient.SendAsync(request);
            //return GetFromJsonAsync<List<DbOrderDetail>?>response.Content;
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<DbOrderDetail>?>($"https://localhost:7250/Command/cart?userID={GlobalBuffer.CurrentUserID}");
                return response;
            }
            catch
            {
                return null;
            }

        }
    }
}

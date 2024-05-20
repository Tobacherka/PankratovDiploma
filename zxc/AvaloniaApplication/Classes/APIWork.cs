using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
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
        public static async Task<List<string>> SendRequest(string command, string? parameter = null, string? parameter2 = null, string? parameter3 = null, string? parameter4 = null, string? parameter5 = null, string? parameter6 = null, string? parameter7 = null)
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
                if (parameter5 != null)
                    query += $"&parameter5={Uri.EscapeDataString(parameter5)}";
                if (parameter6 != null)
                    query += $"&parameter6={Uri.EscapeDataString(parameter6)}";
                if (parameter7 != null)
                    query += $"&parameter7={Uri.EscapeDataString(parameter7)}";

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
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<DbOrderDetail>?>($"https://localhost:7250/Command/cart/products-in-cart?userID={GlobalBuffer.CurrentUserID}");
                return response;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<DbOrder?> GetUserCart()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DbOrder?>($"https://localhost:7250/Command/cart?userID={GlobalBuffer.CurrentUserID}");
                return response;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<List<DbProduct>?> GetProducts()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<DbProduct>?>($"https://localhost:7250/Command/products");
                return response;
            }
            catch 
            {
                return null;
            }
        }

        public static async Task<DbProduct?> GetProductById(int productID)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DbProduct?>($"https://localhost:7250/Command/products/product?productID={productID}");
                return response;
            }
            catch
            {
                return null;
            }
        }

        public static async Task<DbUser?> GetUserById(int userID)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<DbUser?>($"https://localhost:7250/Command/users/user?userID={userID}");
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}

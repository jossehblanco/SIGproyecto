using Newtonsoft.Json;
using ProyectoSIG.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSIG.Client
{
    public static class RestClient
    {
        public static HttpClient Client { get; set; } = new HttpClient() { BaseAddress = new Uri("http://sigdb.herokuapp.com/") };
        public static async Task<List<T>> Get<T>(string url)
        {
            try
            {
                var response = await Client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<T>>(jsonString);
                }
                else
                {
                    return default(List<T>);
                }
            }
            catch (Exception e)
            {
                return default(List<T>);
            }
        }

        public static async Task<ObjetoRespuesta> Post<T>(List<T> datos, string url)
        {
            var jsonString = JsonConvert.SerializeObject(datos);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            ObjetoRespuesta res = new ObjetoRespuesta();
            try
            {
                HttpResponseMessage response = await Client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    res.Succesful = true;
                }
                else
                {
                    res.Succesful = false;
                    res.Mensaje = await response.Content.ReadAsStringAsync();
                }
                return res;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION DEL POST = " + e.Message);
                return null;
            }
        }
    }
}

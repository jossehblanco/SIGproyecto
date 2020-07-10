using Newtonsoft.Json;
using ProyectoSIG.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSIG.Client
{
    public static class RestClient
    {
        public static HttpClient Client { get; set; } = new HttpClient() { BaseAddress = new Uri("https://sigdb.herokuapp.com/") };
        public static async Task<ObjetoRespuesta<T>> Get<T>(string url, string token)
        {
            ObjetoRespuesta<T> respuesta = new ObjetoRespuesta<T>();
            try
            {                
                AddTokenHeader(token);
                var response = await Client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    List<T> lista = JsonConvert.DeserializeObject<List<T>>(jsonString);
                    respuesta.Succesful = true;
                    respuesta.Mensaje = "Ok";
                    respuesta.ObjetosRecuperados = lista;
                }
                else
                {
                    respuesta.Succesful = false;
                    if(response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        respuesta.Mensaje = "El token ha vencido";
                    }
                    else
                    {
                        respuesta.Mensaje = "Error en la peticion " + response.StatusCode;
                    }
                }
            }
            catch (Exception e)
            {
                respuesta.Succesful = false;
                respuesta.Mensaje = e.Message;
            }
            return respuesta;
        }

        public static async Task<ObjetoRespuesta<T>> Post<T>(List<T> datos, string url)
        {
            var jsonString = JsonConvert.SerializeObject(datos);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            ObjetoRespuesta<T> res = new ObjetoRespuesta<T>();
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

        public static async Task<ObjetoRespuesta<T>> Post<T>(T dato, string url)
        {
            var jsonString = JsonConvert.SerializeObject(dato);
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            ObjetoRespuesta<T> res = new ObjetoRespuesta<T>();
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
            }
            catch (Exception e)
            {
                res.Succesful = false;
                res.Mensaje = e.Message;
            }
            return res;
        }

        public static async Task<ObjetoRespuesta<Token>> PostForToken(string url)
        {
            var jsonString = JsonConvert.SerializeObject("foo");
            StringContent content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            ObjetoRespuesta<Token> res = new ObjetoRespuesta<Token>();
            try
            {
                HttpResponseMessage response = await Client.PostAsync(url, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    res.Succesful = true;
                    res.ObjetosRecuperados = new List<Token>();
                    res.ObjetosRecuperados.Add(JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync()));
                }
                else
                {
                    res.Succesful = false;
                    res.Mensaje = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                res.Succesful = false;
                res.Mensaje = e.Message;
            }
            return res;
        }

        public static void AddUserPassHeader(LoginParameters login)
        {
            var base64String = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{login.username}:{login.password}"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64String);
        }

        public static void AddTokenHeader(string token)
        {
            //Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + token);
            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        }
    }
}

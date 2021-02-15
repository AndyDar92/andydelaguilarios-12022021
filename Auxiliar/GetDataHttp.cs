using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace miniproyecto.Auxiliar
{
    public class GetDataHttp<T> where T : class
    {
        private readonly string _urlPrincipal;
        private readonly string _api;
        public GetDataHttp(string urlPrincipal, string api)
        {
            _urlPrincipal = urlPrincipal;
            _api = api;
        }

        public IEnumerable<T> GetT(ref string error)
        {
            IEnumerable<T> lista = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_urlPrincipal);
                    var response = client.GetAsync(_api);
                    response.Wait();

                    var result = response.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var read = result.Content.ReadAsStringAsync();
                        read.Wait();

                        lista = JsonConvert.DeserializeObject<IList<T>>(read.Result);
                    }
                    else
                    {
                        lista = Enumerable.Empty<T>();
                        error = "Error al obtener datos del servidor";
                    }

                }
            }
            catch (Exception ex)
            {
                error = "Error al obtener datos del servidor" + ex.Message;
            }

            return lista;
        }
    }
}
using Livraria.Dominio.Model.Interfaces.Infraestrutura;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infraestrutura.Services.Functions
{
    public class FunctionService : IFunctionService
    {
        private readonly string _baseAddress;

        public FunctionService(string functionBaseAddress)
        {
            _baseAddress = functionBaseAddress;
        }

        public async Task InvokeAsync(object objeto)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(objeto);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");
            _ = await httpClient.PostAsync(_baseAddress, requestData);
        }
    }
}

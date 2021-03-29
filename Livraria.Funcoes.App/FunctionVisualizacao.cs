using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Livraria.Funcoes.App
{
    public static class FunctionVisualizacao
    {
        [FunctionName("FunctionVisualizacao")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            int livroId = data?.Id;

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                conn.Open();

                var textSql = $@"UPDATE [dbo].[Livro] SET [UltimaVisualizacao] = GETDATE() WHERE [Id] = {livroId};";
                var countSql = $@"UPDATE [dbo].[Livro] SET [QtdVisualizacao] = [QtdVisualizacao] + 1 WHERE [Id] = {livroId};";

                using (SqlCommand cmd = new SqlCommand(textSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }

                using (SqlCommand cmd = new SqlCommand(countSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
            }

            return new OkResult();
        }
    }
}

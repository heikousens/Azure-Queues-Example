using System;
using System.Data.SqlClient;
using Livraria.Dominio.Model.Entidades;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace Livraria.Funcoes.App
{
    public static class FunctionQueue
    {
        [FunctionName("FunctionQueue")]
        public static void Run([QueueTrigger("function-visualizacao-queue", Connection = "AzureWebJobsStorage")] LivroEntidade livro, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed.");

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Livro] SET [UltimaVisualizacao] = GETDATE() WHERE [Id] = {livro.Id};";
                var countSql = $@"UPDATE [dbo].[Livro] SET [QtdVisualizacao] = [QtdVisualizacao] + 1 WHERE [Id] = {livro.Id};";


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

        }
    }
}

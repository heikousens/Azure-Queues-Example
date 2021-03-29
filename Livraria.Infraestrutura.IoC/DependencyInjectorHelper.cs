using Livraria.Dominio.Model.Interfaces.Infraestrutura;
using Livraria.Dominio.Model.Interfaces.Repositorios;
using Livraria.Dominio.Model.Interfaces.Servicos;
using Livraria.Dominio.Services.Services;
using Livraria.Infraestrutura.Data.Context;
using Livraria.Infraestrutura.Data.Repositorios;
using Livraria.Infraestrutura.Services.Blob;
using Livraria.Infraestrutura.Services.Functions;
using Livraria.Infraestrutura.Services.Queue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Infraestrutura.IoC
{
    public class DependencyInjectorHelper
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LivroContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("LivroContext")));

            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<ILivroService, LivroService>();

            var connStringStorageAccount = configuration.GetValue<string>("ConnectionStringStorageAccount");

            services.AddScoped<IBlobService, BlobService>(provider =>
                new BlobService(connStringStorageAccount));

            services.AddScoped<IQueueService, QueueService>(provider =>
                new QueueService(connStringStorageAccount));

            services.AddScoped<IFunctionService, FunctionService>(provider =>
                new FunctionService(configuration.GetValue<string>("FunctionBaseAddress")));

        }
    }
}

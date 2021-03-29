using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Livraria.Dominio.Model.Entidades;

namespace Livraria.Apresentacao.WebApp.Data
{
    public class LivrariaApresentacaoWebAppContext : DbContext
    {
        public LivrariaApresentacaoWebAppContext (DbContextOptions<LivrariaApresentacaoWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Livraria.Dominio.Model.Entidades.LivroEntidade> LivroEntidade { get; set; }
    }
}

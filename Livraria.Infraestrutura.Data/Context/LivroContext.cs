using Livraria.Dominio.Model.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Livraria.Infraestrutura.Data.Context
{
    public class LivroContext : DbContext
    {
        public LivroContext(DbContextOptions<LivroContext> options)
            : base(options)
        {
        }

        public DbSet<LivroEntidade> LivroEntidade { get; set; }
    }
}

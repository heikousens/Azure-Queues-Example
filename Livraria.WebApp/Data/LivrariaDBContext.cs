using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Livraria.WebApp.Models;

namespace Livraria.WebApp.Data
{
    public class LivrariaDBContext : DbContext
    {
        public LivrariaDBContext (DbContextOptions<LivrariaDBContext> options)
            : base(options)
        {
        }

        public DbSet<Livraria.WebApp.Models.Livro> Livro { get; set; }
    }
}

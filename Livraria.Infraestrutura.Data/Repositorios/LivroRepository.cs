using Livraria.Dominio.Model.Entidades;
using Livraria.Dominio.Model.Interfaces.Repositorios;
using Livraria.Infraestrutura.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Infraestrutura.Data.Repositorios
{
    public class LivroRepository : ILivroRepository
    {
        private readonly LivroContext _context;

        public LivroRepository(LivroContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LivroEntidade>> GetAllAsync()
        {
            return await _context.LivroEntidade.ToListAsync();
        }

        public async Task<LivroEntidade> GetByIdAsync(int id)
        {
            //return await _context.LivroEntidade.AsNoTracking().SingleOrDefaultAsync(m => m.Id == id);
            return await _context.LivroEntidade.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task InsertAsync(LivroEntidade livroEntidade)
        {
            _context.Add(livroEntidade);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LivroEntidade livroEntidade)
        {
            _context.Update(livroEntidade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LivroEntidade livroEntidade)
        {
            _context.Remove(livroEntidade);
            await _context.SaveChangesAsync();
        }

    }
}

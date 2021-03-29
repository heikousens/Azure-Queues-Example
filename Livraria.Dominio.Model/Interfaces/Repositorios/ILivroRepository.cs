using Livraria.Dominio.Model.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Dominio.Model.Interfaces.Repositorios
{
    public interface ILivroRepository
    {
        Task<IEnumerable<LivroEntidade>> GetAllAsync();
        Task<LivroEntidade> GetByIdAsync(int id);
        Task InsertAsync(LivroEntidade livroEntidade);
        Task UpdateAsync(LivroEntidade livroEntidade);
        Task DeleteAsync(LivroEntidade livroEntidade);
    }
}

using Livraria.Dominio.Model.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Livraria.Dominio.Model.Interfaces.Servicos
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroEntidade>> GetAllAsync();
        Task<LivroEntidade> GetByIdAsync(int id);
        Task InsertAsync(LivroEntidade livroEntidade, Stream stream);
        Task UpdateAsync(LivroEntidade livroEntidade, Stream stream);
        Task DeleteAsync(LivroEntidade livroEntidade);
    }
}

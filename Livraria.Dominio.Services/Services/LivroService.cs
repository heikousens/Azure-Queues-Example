using Livraria.Dominio.Model.Entidades;
using Livraria.Dominio.Model.Interfaces.Infraestrutura;
using Livraria.Dominio.Model.Interfaces.Repositorios;
using Livraria.Dominio.Model.Interfaces.Servicos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Livraria.Dominio.Services.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;
        private readonly IBlobService _blobService;
        private readonly IQueueService _queueService;

        public LivroService(ILivroRepository repository, IBlobService blobService, IQueueService queueService)
        {
            _repository = repository;
            _blobService = blobService;
            _queueService = queueService;
        }

        public async Task<IEnumerable<LivroEntidade>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<LivroEntidade> GetByIdAsync(int id)
        {
            var livroTask = _repository.GetByIdAsync(id);

            var livroEntidade = livroTask.Result;

            var jsonSerialize = JsonSerializer.SerializeToUtf8Bytes(livroEntidade);

            string jsonLivro = Convert.ToBase64String(jsonSerialize);

            await _queueService.SendAsync(jsonLivro);

            return livroEntidade;

            //await _functionService.InvokeAsync(new { Id = id });
            //var livroTask = _repository.GetByIdAsync(id);
            //return livroTask.Result;
            //return await _repository.GetByIdAsync(id);
        }

        public async Task InsertAsync(LivroEntidade livroEntidade, Stream stream)
        {
            var newUri = await _blobService.UploadAsync(stream);
            livroEntidade.ImageUri = newUri;

            await _repository.InsertAsync(livroEntidade);
        }

        public async Task UpdateAsync(LivroEntidade livroEntidade, Stream stream)
        {

            if (stream != null) {

                //var updateUri = await _blobService.UploadAsync(stream);
                //livroEntidade.ImageUri = updateUri;
                var livro = await _repository.GetByIdAsync(livroEntidade.Id);
                await _blobService.DeleteAsync(livro.ImageUri);

                var blobUri = await _blobService.UploadAsync(stream);
                livroEntidade.ImageUri = blobUri.ToString();

            }

            await _repository.UpdateAsync(livroEntidade);

        }

        public async Task DeleteAsync(LivroEntidade livroEntidade)
        {
            await _blobService.DeleteAsync(livroEntidade.ImageUri);
            await _repository.DeleteAsync(livroEntidade);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Livraria.Apresentacao.WebApp.Data;
using Livraria.Dominio.Model.Entidades;
using Livraria.Dominio.Model.Interfaces.Servicos;

namespace Livraria.Apresentacao.WebApp.Controllers
{
    public class LivroController : Controller
    {

        private readonly ILivroService _domainService;

        public LivroController(ILivroService domainService)
        {
            _domainService = domainService;
        }

        // GET: Livro
        public async Task<IActionResult> Index()
        {
            return View(await _domainService.GetAllAsync());
        }

        // GET: Livro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroEntidade = await _domainService.GetByIdAsync(id.Value);

            if (livroEntidade == null)
            {
                return NotFound();
            }

            return View(livroEntidade);
        }

        // GET: Livro/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LivroEntidade livroEntidade)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Form.Files.SingleOrDefault();

                await _domainService.InsertAsync(livroEntidade, file?.OpenReadStream());

                return RedirectToAction(nameof(Index));
            }
            return View(livroEntidade);
        }

        // GET: Livro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroEntidade = await _domainService.GetByIdAsync(id.Value);
            if (livroEntidade == null)
            {
                return NotFound();
            }
            return View(livroEntidade);
        }

        // POST: Livro/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LivroEntidade livroEntidade)
        {
            if (id != livroEntidade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var file = Request.Form.Files.SingleOrDefault();

                    await _domainService.UpdateAsync(livroEntidade, file?.OpenReadStream());

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroEntidadeExists(livroEntidade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(livroEntidade);
        }

        // GET: Livro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livroEntidade = await _domainService.GetByIdAsync(id.Value);
            if (livroEntidade == null)
            {
                return NotFound();
            }

            return View(livroEntidade);
        }

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livroDelete = await _domainService.GetByIdAsync(id);

            if (livroDelete == null)
            {
                return NotFound();
            }

            await _domainService.DeleteAsync(livroDelete);

            return RedirectToAction(nameof(Index));
        }

        private bool LivroEntidadeExists(int id)
        {
            return _domainService.GetByIdAsync(id) != null;
        }
    }
}

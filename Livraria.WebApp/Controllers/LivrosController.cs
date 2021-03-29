using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Livraria.WebApp.Data;
using Livraria.WebApp.Models;
using Livraria.Dominio.Model.Interfaces.Servicos;
using Livraria.Dominio.Model.Entidades;

namespace Livraria.WebApp.Controllers
{
    public class LivrosController : Controller
    {
        //private readonly LivrariaDBContext _context;

        //public LivrosController(LivrariaDBContext context)
        //{
        //    _context = context;
        //}

        private readonly ILivroService _domainService;

        public LivrosController(ILivroService domainService)
        {
            _domainService = domainService;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            return View(await _domainService.GetAllAsync());
        }

        // GET: Livros/Details/5
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

        // GET: Livros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Livros/Edit/5
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

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    //TODO: improvement
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
                return RedirectToAction(nameof(Index));
            }
            return View(livroEntidade);
        }

        // GET: Livros/Delete/5
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

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //TODO: improvement

            return RedirectToAction(nameof(Index));
        }
        private bool LivroEntidadeExists(int id)
        {
            return _domainService.GetByIdAsync(id) != null;
        }
    }
}

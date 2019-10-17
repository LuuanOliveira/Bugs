using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cofin_Suporte.Models;

namespace Cofin_Suporte.Controllers
{
    public class SolucaosController : Controller
    {
        private readonly CofinSuporteDbContext _context;

        public SolucaosController(CofinSuporteDbContext context)
        {
            _context = context;
        }

        // GET: Solucaos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Solucao.ToListAsync());
        }

        // GET: Solucaos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucao = await _context.Solucao
                .FirstOrDefaultAsync(m => m.IdSolucao == id);
            if (solucao == null)
            {
                return NotFound();
            }

            return View(solucao);
        }

        // GET: Solucaos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Solucaos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSolucao,DescricaoSolução")] Solucao solucao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(solucao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(solucao);
        }

        // GET: Solucaos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucao = await _context.Solucao.FindAsync(id);
            if (solucao == null)
            {
                return NotFound();
            }
            return View(solucao);
        }

        // POST: Solucaos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSolucao,DescricaoSolução")] Solucao solucao)
        {
            if (id != solucao.IdSolucao)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(solucao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SolucaoExists(solucao.IdSolucao))
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
            return View(solucao);
        }

        // GET: Solucaos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var solucao = await _context.Solucao
                .FirstOrDefaultAsync(m => m.IdSolucao == id);
            if (solucao == null)
            {
                return NotFound();
            }

            return View(solucao);
        }

        // POST: Solucaos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var solucao = await _context.Solucao.FindAsync(id);
            _context.Solucao.Remove(solucao);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SolucaoExists(int id)
        {
            return _context.Solucao.Any(e => e.IdSolucao == id);
        }
    }
}

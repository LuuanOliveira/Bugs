using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cofin_Suporte.Models;
using Sitecore.FakeDb;
using LinqKit;

namespace Cofin_Suporte.Controllers
{
    public class CofinsController : Controller
    {
        private readonly CofinSuporteDbContext _context;

        public CofinsController(CofinSuporteDbContext context)
        {
            _context = context;
        }

        [Obsolete]
        // GET: Cofins
        public IActionResult Index(string filtro)
        {
            List<Cofin> cofinSuporteDbContext = _context.Cofin
              .Include(c => c.IdBacklogNavigation)
              .Include(c => c.IdCategoriaNavigation)
              .Include(c => c.IdTipoNavigation)
              .Include(c => c.CofinSolucao).ThenInclude(x => x.IdSolucaoNavigation).OrderByDescending(x => x.IdCofin).Take(15).ToList();
            var predicate = PredicateBuilder.True<Cofin>();

            

            if (!string.IsNullOrEmpty(filtro))
            {
                predicate = predicate.And(c => c.IdCofin == 0);
                predicate = predicate.Or(c => EF.Functions.Like(c.Descricao, filtro));
                predicate = predicate.Or(c => EF.Functions.Like(c.Observacao, filtro));
                predicate = predicate.Or(c => EF.Functions.Like(c.Assunto, filtro));
                predicate = predicate.Or(c => EF.Functions.Like(c.IdTipoNavigation.DescricaoTipo, filtro));
                predicate = predicate.Or(c => EF.Functions.Like(c.IdCategoriaNavigation.DescricaoCategoria, filtro));
                predicate = predicate.Or(c => EF.Functions.Like(c.IdBacklogNavigation.DescricaoBacklog, filtro));
            }

            var list = cofinSuporteDbContext.AsQueryable().Where(predicate).ToList();
            return View(list);
        }

        // GET: Cofins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cofin = await _context.Cofin
                .Include(c => c.IdBacklogNavigation)
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdTipoNavigation)
                .Include(c => c.CofinSolucao).ThenInclude(x => x.IdSolucaoNavigation)
                .FirstOrDefaultAsync(m => m.IdCofin == id);
            if (cofin == null)
            {
                return NotFound();
            }

            return View(cofin);
        }

        // GET: Cofins/Create
        public IActionResult Create()
        {
            Cofin cofin = new Cofin();
            cofin.Solucoes = _context.Solucao.ToList();

            var CofinSolucaos = new List<CofinSolucao>();
            foreach (Solucao s in cofin.Solucoes)
            {
                var cofinSolucao = new CofinSolucao();
                cofinSolucao.IdSolucao = s.IdSolucao;
                cofinSolucao.IdSolucaoNavigation = s;
                CofinSolucaos.Add(cofinSolucao);
            }

            cofin.CofinSolucao = CofinSolucaos;

            ViewData["IdBacklog"] = new SelectList(_context.Backlog, "IdBacklog", "DescricaoBacklog");
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "DescricaoCategoria");
            ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "DescricaoTipo");
            return View(cofin);
        }

        // POST: Cofins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCofin,Descricao,Assunto,Data,Observacao,IdTipo,IdCategoria,IdBacklog,IdSolucao,Solution")] Cofin cofin)
        {
            string msgErro = "";
            if (ModelState.IsValid)
            {
                cofin.Solucoes = _context.Solucao.ToList();

                var cofinsol = new List<CofinSolucao>();
                foreach (Solucao itens in cofin.Solucoes)
                {
                    var sol = new CofinSolucao();
                    sol.IdSolucao = itens.IdSolucao;
                    sol.IdSolucaoNavigation = itens;
                    sol.Check = cofin.Solution.Contains(itens.IdSolucao) ? true : false;

                    cofinsol.Add(sol);
                }

                cofin.CofinSolucao = cofinsol;

                _context.Add(cofin);
                await _context.SaveChangesAsync();
                return Ok(); 
            }else
            {
                msgErro = "Falta dados";  
            }

           // ViewData["IdBacklog"] = new SelectList(_context.Backlog, "IdBacklog", "DescricaoBacklog", cofin.IdBacklog);
           // ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "DescricaoCategoria", cofin.IdCategoria);
            //ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "DescricaoTipo", cofin.IdTipo);
            //ViewData["IdSolucao"] = new SelectList(_context.Solucao, "IdSolucao", "IdSolucaoNavigation", cofin.Solution);
           // return View(cofin);
            return NotFound(msgErro);
        }

        // GET: Cofins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cofin = await _context.Cofin
                .Include(c => c.IdBacklogNavigation)
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdTipoNavigation)
                .Include(c => c.CofinSolucao).ThenInclude(x => x.IdSolucaoNavigation)
                .FirstOrDefaultAsync(m => m.IdCofin == id);

            cofin.Solucoes = _context.Solucao.ToList();

            if (cofin == null)
            {
                return NotFound();
            }
            ViewData["IdBacklog"] = new SelectList(_context.Backlog, "IdBacklog", "DescricaoBacklog", cofin.IdBacklog);
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "DescricaoCategoria", cofin.IdCategoria);
            ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "DescricaoTipo", cofin.IdTipo);
            return View(cofin);
        }

        // POST: Cofins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCofin,Descricao,Assunto,Data,Observacao,IdTipo,IdCategoria,IdBacklog,IdSolucao")]Cofin cofin, int[] Solution)
        {
            if (id != cofin.IdCofin)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Cofin _cofin = _context.Cofin.Single(x => x.IdCofin == cofin.IdCofin);

                _context.Entry(_cofin).Collection(x => x.CofinSolucao).Load();

                var _solucoes = _context.Solucao.ToList();

                foreach (Solucao v in _solucoes)
                {
                    var _sol = _cofin.CofinSolucao.Where(x => x.IdSolucao == v.IdSolucao).FirstOrDefault();

                    if (_sol == null)
                    {
                        var sol = new CofinSolucao();
                        sol.IdSolucao = v.IdSolucao;
                        sol.IdSolucaoNavigation = v;
                        sol.Check = Solution.Contains(v.IdSolucao) ? true : false;

                        _cofin.CofinSolucao.Add(sol);
                    }
                }

                foreach (CofinSolucao sl in _cofin.CofinSolucao)
                {
                    if (Solution.Contains(sl.IdSolucao))
                    {
                        sl.Check = true;
                    }
                    else
                    {
                        sl.Check = false;
                    }
                }

                _cofin.Descricao = cofin.Descricao;
                _cofin.Data = cofin.Data;
                _cofin.IdTipo = cofin.IdTipo;
                _cofin.IdBacklog = cofin.IdBacklog;
                _cofin.Assunto = cofin.Assunto;
                _cofin.Observacao = cofin.Observacao;
                _cofin.IdCategoria = cofin.IdCategoria;

                try
                {
                    _context.Update(_cofin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CofinExists(cofin.IdCofin))
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
            ViewData["IdBacklog"] = new SelectList(_context.Backlog, "IdBacklog", "DescricaoBacklog", cofin.IdBacklog);
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "DescricaoCategoria", cofin.IdCategoria);
            ViewData["IdTipo"] = new SelectList(_context.Tipo, "IdTipo", "DescricaoTipo", cofin.IdTipo);
            return View(cofin);
        }

        // GET: Cofins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cofin = await _context.Cofin
                .Include(c => c.IdBacklogNavigation)
                .Include(c => c.IdCategoriaNavigation)
                .Include(c => c.IdTipoNavigation)
                .Include(c => c.CofinSolucao).ThenInclude(x => x.IdSolucaoNavigation)
                .FirstOrDefaultAsync(m => m.IdCofin == id);
            if (cofin == null)
            {
                return NotFound();
            }

            return View(cofin);
        }

        // POST: Cofins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cofin = await _context.Cofin.FindAsync(id);
            _context.Cofin.Remove(cofin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CofinExists(int id)
        {
            return _context.Cofin.Any(e => e.IdCofin == id);
        }
    }
}

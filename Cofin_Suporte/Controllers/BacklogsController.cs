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
    public class BacklogsController : Controller
    {
        private readonly CofinSuporteDbContext _context;

        public BacklogsController(CofinSuporteDbContext context)
        {
            _context = context;
        }

        // GET: Backlogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Backlog.ToListAsync());
        }

        // GET: Backlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backlog = await _context.Backlog
                .FirstOrDefaultAsync(m => m.IdBacklog == id);
            if (backlog == null)
            {
                return NotFound();
            }

            return View(backlog);
        }

        // GET: Backlogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Backlogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBacklog,Codigo,DescricaoBacklog")] Backlog backlog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(backlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(backlog);
        }

        // GET: Backlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backlog = await _context.Backlog.FindAsync(id);
            if (backlog == null)
            {
                return NotFound();
            }
            return View(backlog);
        }

        // POST: Backlogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBacklog,Codigo,DescricaoBacklog")] Backlog backlog)
        {
            if (id != backlog.IdBacklog)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(backlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BacklogExists(backlog.IdBacklog))
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
            return View(backlog);
        }

        // GET: Backlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backlog = await _context.Backlog
                .FirstOrDefaultAsync(m => m.IdBacklog == id);
            if (backlog == null)
            {
                return NotFound();
            }

            return View(backlog);
        }

        // POST: Backlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var backlog = await _context.Backlog.FindAsync(id);
            _context.Backlog.Remove(backlog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BacklogExists(int id)
        {
            return _context.Backlog.Any(e => e.IdBacklog == id);
        }
    }
}

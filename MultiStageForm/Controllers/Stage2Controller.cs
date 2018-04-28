using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiStageForm.Models;

namespace MultiStageForm.Controllers
{
    public class Stage2Controller : Controller
    {
        private readonly MultiStageFormContext _context;

        public Stage2Controller(MultiStageFormContext context)
        {
            _context = context;
        }

        // GET: Stage2
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stage2.ToListAsync());
        }

        // GET: Stage2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage2 = await _context.Stage2
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stage2 == null)
            {
                return NotFound();
            }

            return View(stage2);
        }

        // GET: Stage2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stage2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] Stage2 stage2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stage2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stage2);
        }

        // GET: Stage2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage2 = await _context.Stage2.SingleOrDefaultAsync(m => m.Id == id);
            if (stage2 == null)
            {
                return NotFound();
            }
            return View(stage2);
        }

        // POST: Stage2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Stage2 stage2)
        {
            if (id != stage2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Stage2Exists(stage2.Id))
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
            return View(stage2);
        }

        // GET: Stage2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage2 = await _context.Stage2
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stage2 == null)
            {
                return NotFound();
            }

            return View(stage2);
        }

        // POST: Stage2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage2 = await _context.Stage2.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stage2.Remove(stage2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Stage2Exists(int id)
        {
            return _context.Stage2.Any(e => e.Id == id);
        }
    }
}

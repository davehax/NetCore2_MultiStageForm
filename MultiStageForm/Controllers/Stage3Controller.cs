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
    public class Stage3Controller : Controller
    {
        private readonly MultiStageFormContext _context;

        public Stage3Controller(MultiStageFormContext context)
        {
            _context = context;
        }

        // GET: Stage3
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stage3.ToListAsync());
        }

        // GET: Stage3/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage3 = await _context.Stage3
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stage3 == null)
            {
                return NotFound();
            }

            return View(stage3);
        }

        // GET: Stage3/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stage3/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date")] Stage3 stage3)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stage3);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stage3);
        }

        // GET: Stage3/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage3 = await _context.Stage3.SingleOrDefaultAsync(m => m.Id == id);
            if (stage3 == null)
            {
                return NotFound();
            }
            return View(stage3);
        }

        // POST: Stage3/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date")] Stage3 stage3)
        {
            if (id != stage3.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage3);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Stage3Exists(stage3.Id))
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
            return View(stage3);
        }

        // GET: Stage3/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage3 = await _context.Stage3
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stage3 == null)
            {
                return NotFound();
            }

            return View(stage3);
        }

        // POST: Stage3/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage3 = await _context.Stage3.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stage3.Remove(stage3);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Stage3Exists(int id)
        {
            return _context.Stage3.Any(e => e.Id == id);
        }
    }
}

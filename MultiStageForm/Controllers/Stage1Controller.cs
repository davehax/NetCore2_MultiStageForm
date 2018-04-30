using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MultiStageForm.Models;
using MultiStageForm.Workflow;

namespace MultiStageForm.Controllers
{
    public class Stage1Controller : Controller
    {
        private readonly MultiStageFormContext _context;

        public Stage1Controller(MultiStageFormContext context)
        {
            _context = context;
        }

        // GET: Stage1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Stage1.ToListAsync());
        }

        // GET: Stage1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage1 = await _context.Stage1
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stage1 == null)
            {
                return NotFound();
            }

            return View(stage1);
        }

        // GET: Stage1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stage1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Stage1 stage1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stage1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stage1);
        }

        // GET: Stage1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage1 = await _context.Stage1.SingleOrDefaultAsync(m => m.Id == id);
            if (stage1 == null)
            {
                return NotFound();
            }

            var stagedform = await _context.Stagedform.SingleOrDefaultAsync(s => s.Stage1 == stage1.Id);
            if (stagedform == null)
            {
                //return NotFound();
                // Potentially return NotFound...
            }
            else
            {
                ViewData["Stagedform"] = stagedform;
            }

            return View(stage1);
        }

        // POST: Stage1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Stage1 stage1)
        {
            if (id != stage1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stage1);
                    await _context.SaveChangesAsync();

                    // Update the form to the second stage
                    MultiStageFormWorkflow multiStageFormWorkflow = new MultiStageFormWorkflow(_context);
                    await multiStageFormWorkflow.MoveFormToStage(stage1, MultiStageFormStages.Second);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Stage1Exists(stage1.Id))
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
            return View(stage1);
        }

        // GET: Stage1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stage1 = await _context.Stage1
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stage1 == null)
            {
                return NotFound();
            }

            return View(stage1);
        }

        // POST: Stage1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stage1 = await _context.Stage1.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stage1.Remove(stage1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Stage1Exists(int id)
        {
            return _context.Stage1.Any(e => e.Id == id);
        }
    }
}

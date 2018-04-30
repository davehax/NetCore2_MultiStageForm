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
    public class StagedFormController : Controller
    {
        private readonly MultiStageFormContext _context;

        public StagedFormController(MultiStageFormContext context)
        {
            _context = context;
        }

        // GET: StagedForm
        public async Task<IActionResult> Index()
        {
            var multiStageFormContext = _context.Stagedform.Include(s => s.Stage1Navigation).Include(s => s.Stage2Navigation).Include(s => s.Stage3Navigation);
            return View(await multiStageFormContext.ToListAsync());
        }

        // GET: StagedForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stagedform = await _context.Stagedform
                .Include(s => s.Stage1Navigation)
                .Include(s => s.Stage2Navigation)
                .Include(s => s.Stage3Navigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stagedform == null)
            {
                return NotFound();
            }

            return View(stagedform);
        }

        // GET: StagedForm/Create
        public IActionResult Create()
        {
            //ViewData["Stage1"] = new SelectList(_context.Stage1, "Id", "Name");
            //ViewData["Stage2"] = new SelectList(_context.Stage2, "Id", "Description");
            //ViewData["Stage3"] = new SelectList(_context.Stage3, "Id", "Id");
            return View();
        }

        // POST: StagedForm/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Stage1,Stage2,Stage3,CurrentStage")] Stagedform stagedform)
        {
            if (ModelState.IsValid)
            {
                MultiStageFormWorkflow multiStageFormWorkflow = new MultiStageFormWorkflow(_context);

                _context.Add(stagedform);
                await _context.SaveChangesAsync();

                await multiStageFormWorkflow.OnMultiStageFormCreate(stagedform);

                return RedirectToAction(nameof(Index));
            }
            ViewData["Stage1"] = new SelectList(_context.Stage1, "Id", "Name", stagedform.Stage1);
            ViewData["Stage2"] = new SelectList(_context.Stage2, "Id", "Description", stagedform.Stage2);
            ViewData["Stage3"] = new SelectList(_context.Stage3, "Id", "Id", stagedform.Stage3);
            return View(stagedform);
        }

        // GET: StagedForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stagedform = await _context.Stagedform.SingleOrDefaultAsync(m => m.Id == id);
            if (stagedform == null)
            {
                return NotFound();
            }
            ViewData["Stage1"] = new SelectList(_context.Stage1, "Id", "Name", stagedform.Stage1);
            ViewData["Stage2"] = new SelectList(_context.Stage2, "Id", "Description", stagedform.Stage2);
            ViewData["Stage3"] = new SelectList(_context.Stage3, "Id", "Id", stagedform.Stage3);
            return View(stagedform);
        }

        // POST: StagedForm/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Stage1,Stage2,Stage3,CurrentStage")] Stagedform stagedform)
        {
            if (id != stagedform.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stagedform);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StagedformExists(stagedform.Id))
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
            ViewData["Stage1"] = new SelectList(_context.Stage1, "Id", "Name", stagedform.Stage1);
            ViewData["Stage2"] = new SelectList(_context.Stage2, "Id", "Description", stagedform.Stage2);
            ViewData["Stage3"] = new SelectList(_context.Stage3, "Id", "Id", stagedform.Stage3);
            return View(stagedform);
        }

        // GET: StagedForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stagedform = await _context.Stagedform
                .Include(s => s.Stage1Navigation)
                .Include(s => s.Stage2Navigation)
                .Include(s => s.Stage3Navigation)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (stagedform == null)
            {
                return NotFound();
            }

            return View(stagedform);
        }

        // POST: StagedForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stagedform = await _context.Stagedform.SingleOrDefaultAsync(m => m.Id == id);
            _context.Stagedform.Remove(stagedform);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StagedformExists(int id)
        {
            return _context.Stagedform.Any(e => e.Id == id);
        }
    }
}

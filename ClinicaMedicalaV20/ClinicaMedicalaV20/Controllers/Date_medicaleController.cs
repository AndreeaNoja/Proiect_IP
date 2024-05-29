using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaMedicalaV20.Data;
using ClinicaMedicalaV20.Models;
using ClinicaMedicalaV20.Filters;

namespace ClinicaMedicalaV20.Controllers
{
    public class Date_medicaleController : Controller
    {
        private readonly AppDbContext _context;
        [AuthorizeRole("Medic")]

        public Date_medicaleController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Date_medicale
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Date_medicale.ToListAsync());
        }

        // GET: Date_medicale/Details/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date_medicale = await _context.Date_medicale
                .FirstOrDefaultAsync(m => m.Id == id);
            if (date_medicale == null)
            {
                return NotFound();
            }

            return View(date_medicale);
        }
        [AuthorizeRole("Medic")]
        // GET: Date_medicale/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Date_medicale/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Create([Bind("Id,IDPacient,IstoricMedical,Alergii,ConsultatiiCardiologice")] Date_medicale date_medicale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(date_medicale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(date_medicale);
        }

        // GET: Date_medicale/Edit/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date_medicale = await _context.Date_medicale.FindAsync(id);
            if (date_medicale == null)
            {
                return NotFound();
            }
            return View(date_medicale);
        }
        [AuthorizeRole("Medic")]
        // POST: Date_medicale/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IDPacient,IstoricMedical,Alergii,ConsultatiiCardiologice")] Date_medicale date_medicale)
        {
            if (id != date_medicale.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(date_medicale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Date_medicaleExists(date_medicale.Id))
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
            return View(date_medicale);
        }
        [AuthorizeRole("Medic")]
        // GET: Date_medicale/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date_medicale = await _context.Date_medicale
                .FirstOrDefaultAsync(m => m.Id == id);
            if (date_medicale == null)
            {
                return NotFound();
            }

            return View(date_medicale);
        }

        // POST: Date_medicale/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var date_medicale = await _context.Date_medicale.FindAsync(id);
            if (date_medicale != null)
            {
                _context.Date_medicale.Remove(date_medicale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Date_medicaleExists(int id)
        {
            return _context.Date_medicale.Any(e => e.Id == id);
        }
    }
}

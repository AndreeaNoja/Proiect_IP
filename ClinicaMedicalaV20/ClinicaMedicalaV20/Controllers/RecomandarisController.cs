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
    public class RecomandarisController : Controller
    {
        private readonly AppDbContext _context;
        [AuthorizeRole("Medic")]
        public RecomandarisController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Recomandaris
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recomandari.ToListAsync());
        }

        // GET: Recomandaris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomandari = await _context.Recomandari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recomandari == null)
            {
                return NotFound();
            }

            return View(recomandari);
        }

        // GET: Recomandaris/Create
        [AuthorizeRole("Medic")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recomandaris/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Create([Bind("Id,IDPacient,TipulRecomandarii,Data_Inceput,Data_Final,AlteIndicatii")] Recomandari recomandari)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recomandari);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recomandari);
        }

        // GET: Recomandaris/Edit/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomandari = await _context.Recomandari.FindAsync(id);
            if (recomandari == null)
            {
                return NotFound();
            }
            return View(recomandari);
        }

        // POST: Recomandaris/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IDPacient,TipulRecomandarii,Data_Inceput,Data_Final,AlteIndicatii")] Recomandari recomandari)
        {
            if (id != recomandari.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recomandari);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecomandariExists(recomandari.Id))
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
            return View(recomandari);
        }
        [AuthorizeRole("Medic")]
        // GET: Recomandaris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recomandari = await _context.Recomandari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recomandari == null)
            {
                return NotFound();
            }

            return View(recomandari);
        }
        [AuthorizeRole("Medic")]
        // POST: Recomandaris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recomandari = await _context.Recomandari.FindAsync(id);
            if (recomandari != null)
            {
                _context.Recomandari.Remove(recomandari);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecomandariExists(int id)
        {
            return _context.Recomandari.Any(e => e.Id == id);
        }
    }
}

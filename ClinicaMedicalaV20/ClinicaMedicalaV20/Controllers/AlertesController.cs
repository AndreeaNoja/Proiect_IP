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
    public class AlertesController : Controller
    {
        private readonly AppDbContext _context;

        public AlertesController(AppDbContext context)
        {
            _context = context;
        }
        [AuthorizeRole("Medic")]
        // GET: Alertes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alerte.ToListAsync());
        }

        // GET: Alertes/Details/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerte = await _context.Alerte
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alerte == null)
            {
                return NotFound();
            }

            return View(alerte);
        }

        // GET: Alertes/Create
        [AuthorizeRole("Medic")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alertes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Create([Bind("Id,IDPacient,Alarma,Avertisment,FrecventaCardiacaMax,UmiditateMax,TemperaturaMax,ECKMax")] Alerte alerte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alerte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alerte);
        }

        // GET: Alertes/Edit/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerte = await _context.Alerte.FindAsync(id);
            if (alerte == null)
            {
                return NotFound();
            }
            return View(alerte);
        }

        // POST: Alertes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IDPacient,Alarma,Avertisment,FrecventaCardiacaMax,UmiditateMax,TemperaturaMax,ECKMax")] Alerte alerte)
        {
            if (id != alerte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alerte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlerteExists(alerte.Id))
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
            return View(alerte);
        }

        // GET: Alertes/Delete/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alerte = await _context.Alerte
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alerte == null)
            {
                return NotFound();
            }

            return View(alerte);
        }

        // POST: Alertes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alerte = await _context.Alerte.FindAsync(id);
            if (alerte != null)
            {
                _context.Alerte.Remove(alerte);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlerteExists(int id)
        {
            return _context.Alerte.Any(e => e.Id == id);
        }
    }
}

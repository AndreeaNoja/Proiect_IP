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
    public class Date_senzoriController : Controller
    {
        private readonly AppDbContext _context;

        public Date_senzoriController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Date_senzori
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Date_senzori.ToListAsync());
        }

        // GET: Date_senzori/Details/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date_senzori = await _context.Date_senzori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (date_senzori == null)
            {
                return NotFound();
            }

            return View(date_senzori);
        }
        [AuthorizeRole("Medic")]
        // GET: Date_senzori/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Date_senzori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Create([Bind("Id,IDPacient,Timestamp,FrecventaCardiaca,Umiditate,TemperaturaMax,ECG_pWave,ECG_qrsWave,ECG_tWave")] Date_senzori date_senzori)
        {
            if (ModelState.IsValid)
            {
                _context.Add(date_senzori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(date_senzori);
        }

        // GET: Date_senzori/Edit/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date_senzori = await _context.Date_senzori.FindAsync(id);
            if (date_senzori == null)
            {
                return NotFound();
            }
            return View(date_senzori);
        }

        // POST: Date_senzori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IDPacient,Timestamp,FrecventaCardiaca,Umiditate,TemperaturaMax,ECG_pWave,ECG_qrsWave,ECG_tWave")] Date_senzori date_senzori)
        {
            if (id != date_senzori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(date_senzori);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Date_senzoriExists(date_senzori.Id))
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
            return View(date_senzori);
        }
        [AuthorizeRole("Medic")]
        // GET: Date_senzori/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var date_senzori = await _context.Date_senzori
                .FirstOrDefaultAsync(m => m.Id == id);
            if (date_senzori == null)
            {
                return NotFound();
            }

            return View(date_senzori);
        }
        [AuthorizeRole("Medic")]
        // POST: Date_senzori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var date_senzori = await _context.Date_senzori.FindAsync(id);
            if (date_senzori != null)
            {
                _context.Date_senzori.Remove(date_senzori);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Date_senzoriExists(int id)
        {
            return _context.Date_senzori.Any(e => e.Id == id);
        }
    }
}

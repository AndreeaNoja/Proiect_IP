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
    public class MedicsController : Controller
    {
        private readonly AppDbContext _context;

        public MedicsController(AppDbContext context)
        {
            _context = context;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Nume, string Parola)
        {

            if (Nume == "admin" && Parola == "admin")
            {
                HttpContext.Session.SetString("MedicNume", "admin");
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("Index", "Home");
            }

            var medic = _context.Medici.FirstOrDefault(m => m.Nume == Nume && m.Parola == Parola);


            if (medic != null)
            {

                HttpContext.Session.SetString("MedicNume", medic.Nume);
                HttpContext.Session.SetString("UserRole", "Medic");


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Numele sau Parola sunt incorecte!";
                return View();
            }

        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); // Șterge toate datele din sesiune
            return RedirectToAction("Login", "Medics");
        }

        // GET: Medics
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medici.ToListAsync());
        }

        // GET: Medics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medic == null)
            {
                return NotFound();
            }

            return View(medic);
        }

        // GET: Medics/Create
        [AuthorizeRole("Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ID_Medic,Nume,Prenume,Parola")] Medic medic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medic);
        }

        // GET: Medics/Edit/5
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici.FindAsync(id);
            if (medic == null)
            {
                return NotFound();
            }
            return View(medic);
        }

        // POST: Medics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ID_Medic,Nume,Prenume,Parola")] Medic medic)
        {
            if (id != medic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicExists(medic.Id))
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
            return View(medic);
        }

        // GET: Medics/Delete/5
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medic = await _context.Medici
                .FirstOrDefaultAsync(m => m.Id == id);
            if (medic == null)
            {
                return NotFound();
            }

            return View(medic);
        }

        // POST: Medics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medic = await _context.Medici.FindAsync(id);
            if (medic != null)
            {
                _context.Medici.Remove(medic);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicExists(int id)
        {
            return _context.Medici.Any(e => e.Id == id);
        }
    }
}

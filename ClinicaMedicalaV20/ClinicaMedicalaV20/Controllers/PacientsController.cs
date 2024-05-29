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
    public class PacientsController : Controller
    {
        private readonly AppDbContext _context;

        public PacientsController(AppDbContext context)
        {
            _context = context;
        }

        // Metoda GET pentru afișarea formularului de autentificare
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Nume, string CNP)
        {
            if (Nume == "admin" && CNP == "admin")
            {
                HttpContext.Session.SetString("PacientNume", "admin");
                HttpContext.Session.SetString("UserRole", "Admin");
                return RedirectToAction("Index", "Home");
            }
            var pacient = _context.Pacienti.FirstOrDefault(m => m.Nume == Nume && m.CNP == CNP);

            if (pacient != null)
            {

                HttpContext.Session.SetString("PacientNume", pacient.Nume);
                HttpContext.Session.SetString("PacientCNP", pacient.CNP);
                HttpContext.Session.SetString("UserRole", "Pacient");


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Numele sau CNP sunt incorecte!";
                return View();
            }

        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear(); // Șterge toate datele din sesiune
            return RedirectToAction("Login", "Pacients");
        }


        // GET: Pacients
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacienti.ToListAsync());
        }

        // GET: Pacients/Details/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacient = await _context.Pacienti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacient == null)
            {
                return NotFound();
            }

            return View(pacient);
        }

        // GET: Pacients/Create
        [AuthorizeRole("Medic")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pacients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IDMedicApartinator,CNP,Nume,Prenume,Varsta,Judet,Oras,Strada,Numar,Bloc,Apartament,NumarTel,Email,Profesie,LocMunca")] Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacient);
        }

        // GET: Pacients/Edit/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacient = await _context.Pacienti.FindAsync(id);
            if (pacient == null)
            {
                return NotFound();
            }
            return View(pacient);
        }

        // POST: Pacients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IDMedicApartinator,CNP,Nume,Prenume,Varsta,Judet,Oras,Strada,Numar,Bloc,Apartament,NumarTel,Email,Profesie,LocMunca")] Pacient pacient)
        {
            if (id != pacient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacientExists(pacient.Id))
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
            return View(pacient);
        }

        // GET: Pacients/Delete/5
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacient = await _context.Pacienti
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacient == null)
            {
                return NotFound();
            }

            return View(pacient);
        }

        // POST: Pacients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizeRole("Medic")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacient = await _context.Pacienti.FindAsync(id);
            if (pacient != null)
            {
                _context.Pacienti.Remove(pacient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacientExists(int id)
        {
            return _context.Pacienti.Any(e => e.Id == id);
        }


        [AuthorizeRole("Pacient")]
        public async Task<IActionResult> MyDetails()
        {

            // Retrieve the CNP from session
            var PacientCNP = HttpContext.Session.GetString("PacientCNP");

            if (string.IsNullOrEmpty(PacientCNP))
            {
                // If CNP is not in the session, redirect to login page
                return RedirectToAction("Index", "Home");
            }
            var pacient = await _context.Pacienti
                                    .FirstOrDefaultAsync(m => m.CNP == PacientCNP);
            if (pacient == null)
            {
                // Handle the case where the patient's details are not found
                // This could redirect to an error page or show a not found message
                return NotFound();
            }
            return View("Details", pacient);
        }

        // GET: Pacienti/MyRecommendations
        [AuthorizeRole("Pacient")]
        public async Task<IActionResult> MyRecommendations()
        {
            // Retrieve the CNP from session
            var PacientCNP = HttpContext.Session.GetString("PacientCNP");

            if (string.IsNullOrEmpty(PacientCNP))
            {
                // If CNP is not in the session, redirect to login page
                return RedirectToAction("Index", "Home");
            }

            // Find the patient with the given CNP
            var pacient = await _context.Pacienti
                .FirstOrDefaultAsync(m => m.CNP == PacientCNP);

            if (pacient == null)
            {
                // Handle the case where the patient's details are not found
                return NotFound();
            }

            // Find recommendations for this patient's medic apartinator
            var recomandari = await _context.Recomandari
        .Where(r => r.IDPacient == (pacient.CNP)) // Assuming IDPacient is a numerical representation of CNP
        .ToListAsync();

            return View(recomandari);
        }
        [AuthorizeRole("Pacient")]
        public async Task<IActionResult> MyAlerts()
        {
            // Retrieve the CNP from session
            var PacientCNP = HttpContext.Session.GetString("PacientCNP");

            if (string.IsNullOrEmpty(PacientCNP))
            {
                // If CNP is not in the session, redirect to login page
                return RedirectToAction("Index", "Home");
            }

            // Find the patient with the given CNP
            var pacient = await _context.Pacienti
                .FirstOrDefaultAsync(m => m.CNP == PacientCNP);

            if (pacient == null)
            {
                // Handle the case where the patient's details are not found
                return NotFound();
            }

            // Find recommendations for this patient's medic apartinator
            var alerte = await _context.Alerte
        .Where(r => r.IDPacient == (pacient.CNP)) // Assuming IDPacient is a numerical representation of CNP
        .ToListAsync();

            return View(alerte);
        }
        [AuthorizeRole("Pacient")]
        public async Task<IActionResult> MyDate_Medicale()
        {
            // Retrieve the CNP from session
            var PacientCNP = HttpContext.Session.GetString("PacientCNP");

            if (string.IsNullOrEmpty(PacientCNP))
            {
                // If CNP is not in the session, redirect to login page
                return RedirectToAction("Index", "Home");
            }

            // Find the patient with the given CNP
            var pacient = await _context.Pacienti
                .FirstOrDefaultAsync(m => m.CNP == PacientCNP);

            if (pacient == null)
            {
                // Handle the case where the patient's details are not found
                return NotFound();
            }

            // Find recommendations for this patient's medic apartinator
            var Date_medicale = await _context.Date_medicale
        .Where(r => r.IDPacient == (pacient.CNP)) // Assuming IDPacient is a numerical representation of CNP
        .ToListAsync();

            return View(Date_medicale);
        }
        [HttpGet]
        [HttpGet]
        [AuthorizeRole("Medic")]
        
        public async Task<IActionResult> GetECGData(int id)
        {
            var pacient = await _context.Pacienti.FindAsync(id);
            if (pacient == null)
            {
                return NotFound();
            }

            var dateSenzori = await _context.Date_senzori
                                             .Where(ds => ds.IDPacient == pacient.CNP)
                                             .ToListAsync();

            if (dateSenzori == null || !dateSenzori.Any())
            {
                return NotFound();
            }

            var ecgPWave = new List<float>();
            var ecgQrsWave = new List<float>();
            var ecgTWave = new List<float>();

            foreach (var data in dateSenzori)
            {
                ecgPWave.Add(data.ECG_pWave);
                ecgQrsWave.Add(data.ECG_qrsWave);
                ecgTWave.Add(data.ECG_tWave);
            }

            return Json(new
            {
                ecG_pWave = ecgPWave,
                ecG_qrsWave = ecgQrsWave,
                ecG_tWave = ecgTWave
            });
        }
        [AuthorizeRole("Medic")]
        public IActionResult ECGGraphs(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [AuthorizeRole("Pacient")]
        [HttpGet]
        
        public async Task<IActionResult> GetECGDataPacient()
        {
            var pacientCNP = HttpContext.Session.GetString("PacientCNP");

            if (string.IsNullOrEmpty(pacientCNP))
            {
                // If CNP is not in the session, redirect to login page
                return RedirectToAction("Index", "Home");
            }

            // Find the patient with the given CNP
            var pacient = await _context.Pacienti.FirstOrDefaultAsync(m => m.CNP == pacientCNP);
            if (pacient == null)
            {
                // Handle the case where the patient's details are not found
                return NotFound();
            }

            var dateSenzori = await _context.Date_senzori
                .Where(r => r.IDPacient == pacient.CNP) // Assuming IDPacient is a string representation of CNP
                .ToListAsync();

            if (dateSenzori == null || !dateSenzori.Any())
            {
                return NotFound();
            }

            var ecgPWave = dateSenzori.Select(ds => ds.ECG_pWave).ToList();
            var ecgQrsWave = dateSenzori.Select(ds => ds.ECG_qrsWave).ToList();
            var ecgTWave = dateSenzori.Select(ds => ds.ECG_tWave).ToList();

            return Json(new
            {
                ecG_pWave = ecgPWave,
                ecG_qrsWave = ecgQrsWave,
                ecG_tWave = ecgTWave
            });
        }

        [AuthorizeRole("Pacient")]
        public IActionResult ECGGraphsPacient()
        {
            return View();
        }
    }
}

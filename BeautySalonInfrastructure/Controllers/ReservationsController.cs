using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySalonDomain.Model;
using BeautySalonInfrastructure;

namespace BeautySalonInfrastructure.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly DbbeautySalonContext _context;

        public ReservationsController(DbbeautySalonContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var dbbeautySalonContext = _context.Reservations.Include(r => r.Clients).Include(r => r.Schedules).Include(r => r.Services);
            return View(await dbbeautySalonContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Schedules)
                .Include(r => r.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewBag.ClientsFirstName = new SelectList(_context.Clients, "Id", "FirstName");
            ViewBag.ClientsLastName = new SelectList(_context.Clients, "Id", "LastName");
            ViewBag.ClientsPhone = new SelectList(_context.Clients, "Id", "Phone");
            ViewBag.SchedulesDate = new SelectList(_context.Schedules, "Id", "Date");
            ViewBag.SchedulesStartTime = new SelectList(_context.Schedules, "Id", "StartTime");
            ViewBag.TypeServicesName = new SelectList(_context.TypeServices.OrderBy(ts => ts.Name), "Id", "Name");
            ViewBag.ServiceName = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Clients.FirstName,Clients.LastName,Clients.Phone,Schedules.Date,Schedules.StartTime,TypeServices.Name,Services.Name,Id")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ClientsFirstName = new SelectList(_context.Clients, "Id", "FirstName", reservation.Clients.FirstName);
            ViewBag.ClientsLastName = new SelectList(_context.Clients, "Id", "LastName", reservation.Clients.LastName);
            ViewBag.ClientsPhone = new SelectList(_context.Clients, "Id", "Phone", reservation.Clients.Phone);
            ViewBag.SchedulesDate = new SelectList(_context.Schedules, "Id", "Date", reservation.Schedules.Date);
            ViewBag.SchedulesStartTime = new SelectList(_context.Schedules, "Id", "StartTime", reservation.Schedules.StartTime);
            ViewBag.TypeServicesName = new SelectList(_context.TypeServices.OrderBy(ts => ts.Name), "Id", "Name", reservation.TypeServices.Name);
            ViewBag.ServiceName = new SelectList(_context.Services, "Id", "Name", reservation.Services.Name);
            return View(reservation);
        }




        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ClientsId"] = new SelectList(_context.Clients, "Id", "LastName", reservation.ClientsId);
            ViewData["SchedulesId"] = new SelectList(_context.Schedules, "Id", "Id", reservation.SchedulesId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServicesId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientsId,ServicesId,SchedulesId,Info,Id")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["ClientsId"] = new SelectList(_context.Clients, "Id", "LastName", reservation.ClientsId);
            ViewData["SchedulesId"] = new SelectList(_context.Schedules, "Id", "Id", reservation.SchedulesId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServicesId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Schedules)
                .Include(r => r.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}

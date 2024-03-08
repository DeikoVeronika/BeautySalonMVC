using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySalonDomain.Model;
using BeautySalonInfrastructure;
using System.Runtime.InteropServices;
using BeautySalonInfrastructure.Models;

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

        public  IActionResult Create()
        {
            var model = new ReservationViewModel();
            PopulateDropdowns();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationViewModel model)
        {
            PopulateDropdowns();

            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == model.Client.Email);

                if (client == null)
                {
                    client = new Client
                    {
                        FirstName = model.Client.FirstName,
                        LastName = model.Client.LastName,
                        Phone = model.Client.Phone,
                        Email = model.Client.Email
                    };

                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();
                }

                var reservation = new Reservation
                {
                    ClientsId = client.Id,
                    ServicesId = model.ServicesId,
                    SchedulesId = model.SchedulesId,
                    TypeServicesId = model.TypeServicesId,
                    EmployeeServicesId = model.EmployeeServicesId,
                    Info = DateTime.Now.ToString("HH:mm:ss dd.MM.yyyy") // Додаємо час та дату створення бронювання
                };
                

            if(ModelState.IsValid)
                {
                    _context.Reservations.Add(reservation);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while creating the reservation.");
                PopulateDropdowns();

                return View(model);
            }
        }


        private void PopulateDropdowns()
        {
            ViewBag.TypeServicesId = new SelectList(_context.TypeServices, "Id", "Name");
            ViewBag.ServicesId = new SelectList(_context.Services, "Id", "Name");
            ViewBag.EmployeeServicesId = _context.EmployeeServices.Select(e => new SelectListItem
            {
                Value = e.EmployeesId.ToString(),
                Text = e.Employees.Name
            }).ToList();
            ViewBag.SchedulesDate = new SelectList(_context.Schedules, "Id", "Date");
            ViewBag.SchedulesStartTime = new SelectList(_context.Schedules, "Id", "StartTime");
        }

        [HttpGet]
        public JsonResult GetTypeServices()
        {
            var typeServices = _context.TypeServices.Select(t => new
            {
                value = t.Id,
                text = t.Name
            }).ToList();

            return Json(typeServices);
        }



        public async Task<IActionResult> GetServices(int typeId)
        {
            var services = await _context.Services
                .Where(s => s.TypeServiceId == typeId)
                .Select(s => new { value = s.Id, text = s.Name })
                .ToListAsync();
            return Json(services);
        }

        public async Task<IActionResult> GetEmployees(int serviceId)
        {
            var employees = await _context.EmployeeServices
                .Where(e => e.ServicesId == serviceId)
                .Select(e => new { value = e.EmployeesId, text = e.Employees.Name })
                .ToListAsync();
            return Json(employees);
        }

        public async Task<IActionResult> GetDates(int employeeId)
        {
            var dates = await _context.Schedules
                .Where(s => s.EmployeesId == employeeId)
                .Select(s => new { value = s.Id, text = s.Date.ToString("dd.MM.yyy") }) // Format date as needed
                .ToListAsync();
            return Json(dates);
        }

        public async Task<IActionResult> GetTimes(int dateId)
        {
            var times = await _context.Schedules
                .Where(s => s.Id == dateId)
                .Select(s => new { value = s.Id, text = s.StartTime.ToString("HH:mm") }) // Format time as needed
                .ToListAsync();
            return Json(times);
        }






        //// GET: Reservations/Create
        //public IActionResult Create()
        //{
        //    ViewBag.ServicesId = new SelectList(_context.Services, "Id", "Name");
        //    ViewBag.SchedulesDate = new SelectList(_context.Schedules, "Id", "Date");
        //    ViewBag.SchedulesStartTime = new SelectList(_context.Schedules, "Id", "StartTime");
        //    ViewBag.TypeServicesId = new SelectList(_context.TypeServices, "Id", "Name");
        //    ViewBag.EmployeeServicesId = new SelectList(_context.EmployeeServices.Include(e => e.Employees), "Id", "Employees.Name");

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ReservationViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var client = new Client
        //        {
        //            FirstName = model.Client.FirstName,
        //            LastName = model.Client.LastName,
        //            Phone = model.Client.Phone,
        //            Email = model.Client.Email
        //        };

        //        _context.Add(client);
        //        await _context.SaveChangesAsync();

        //        var reservation = new Reservation
        //        {
        //            ClientsId = client.Id,
        //            ServicesId = model.ServicesId,
        //            SchedulesId = model.SchedulesId,
        //            TypeServicesId = model.TypeServicesId,
        //            EmployeeServicesId = model.EmployeeServicesId
        //        };

        //        _context.Add(reservation);
        //        await _context.SaveChangesAsync();

        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewBag.ServicesId = new SelectList(_context.Services, "Id", "Name", model.ServicesId);
        //    ViewBag.SchedulesDate = new SelectList(_context.Schedules, "Id", "Date", model.SchedulesId);
        //    ViewBag.SchedulesStartTime = new SelectList(_context.Schedules, "Id", "StartTime", model.SchedulesId);
        //    ViewBag.TypeServicesId = new SelectList(_context.TypeServices, "Id", "Name", model.TypeServicesId);
        //    ViewBag.EmployeeServicesId = new SelectList(_context.EmployeeServices.Include(e => e.Employees), "Id", "Employees.Name", model.EmployeeServicesId);

        //    return View(model);
        //}


        //public IActionResult Create()
        //{
        //    return View();
        //}


        //public IActionResult CreateStepOne()
        //{
        //    return PartialView("_StepOne", new ReservationStepOneCreatingClientViewModel());
        //}


        //// Крок 1: Створення клієнта
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateStepOne(ReservationStepOneCreatingClientViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using var transaction = _context.Database.BeginTransaction();

        //        try
        //        {
        //            var client = new Client
        //            {
        //                FirstName = model.Client.FirstName,
        //                LastName = model.Client.LastName,
        //                Phone = model.Client.Phone,
        //                Email = model.Client.Email
        //            };

        //            _context.Add(client);
        //            await _context.SaveChangesAsync();

        //            HttpContext.Session.SetString("TransactionId", transaction.TransactionId.ToString());

        //            return RedirectToAction(nameof(CreateStepTwo), new { clientId = client.Id });
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    return View(model);
        //}

        //// Крок 2: Вибір послуги
        //public IActionResult CreateStepTwo(int clientId)
        //{
        //    ViewBag.ServicesId = new SelectList(_context.Services, "Id", "Name");
        //    ViewBag.TypeServicesId = new SelectList(_context.TypeServices, "Id", "Name");

        //    return View(new ReservationStepTwoChoosingServiceViewModel { ClientId = clientId });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateStepTwo(ReservationStepTwoChoosingServiceViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction(nameof(CreateStepThree), new { clientId = model.ClientId, servicesId = model.ServicesId, typeServicesId = model.TypeServicesId });
        //    }

        //    ViewBag.ServicesId = new SelectList(_context.Services, "Id", "Name", model.ServicesId);
        //    ViewBag.TypeServicesId = new SelectList(_context.TypeServices, "Id", "Name", model.TypeServicesId);

        //    return View(model);
        //}

        //// Крок 3: Вибір працівника
        //public IActionResult CreateStepThree(int clientId, int servicesId, int typeServicesId)
        //{
        //    ViewBag.EmployeeServicesId = new SelectList(_context.EmployeeServices.Include(e => e.Employees), "Id", "Employees.Name");

        //    return View(new ReservationStepThreeChoosingEmployeeViewModel { ClientId = clientId, ServicesId = servicesId, TypeServicesId = typeServicesId });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult CreateStepThree(ReservationStepThreeChoosingEmployeeViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction(nameof(CreateStepFour), new { clientId = model.ClientId, servicesId = model.ServicesId, typeServicesId = model.TypeServicesId, employeeServicesId = model.EmployeeServicesId });
        //    }

        //    ViewBag.EmployeeServicesId = new SelectList(_context.EmployeeServices.Include(e => e.Employees), "Id", "Employees.Name", model.EmployeeServicesId);

        //    return View(model);
        //}

        //// Крок 4: Вибір часу
        //public IActionResult CreateStepFour(int clientId, int servicesId, int typeServicesId, int employeeServicesId)
        //{
        //    ViewBag.SchedulesId = new SelectList(_context.Schedules, "Id", "Date");

        //    return View(new ReservationStepFourChoosingTimeViewModel { ClientId = clientId, ServicesId = servicesId, TypeServicesId = typeServicesId, EmployeeServicesId = employeeServicesId });
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateStepFour(ReservationStepFourChoosingTimeViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        using var transaction = _context.Database.BeginTransaction();

        //        try
        //        {
        //            var reservation = new Reservation
        //            {
        //                ClientsId = model.ClientId,
        //                ServicesId = model.ServicesId,
        //                SchedulesId = model.SchedulesId,
        //                TypeServicesId = model.TypeServicesId,
        //                EmployeeServicesId = model.EmployeeServicesId
        //            };

        //            _context.Add(reservation);
        //            await _context.SaveChangesAsync();

        //            transaction.Commit();

        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    ViewBag.SchedulesId = new SelectList(_context.Schedules, "Id", "Date", model.SchedulesId);

        //    return View(model);
        //}

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


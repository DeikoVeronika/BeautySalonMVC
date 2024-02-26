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
    public class EmployeeServicesController : Controller
    {
        private readonly DbbeautySalonContext _context;

        public EmployeeServicesController(DbbeautySalonContext context)
        {
            _context = context;
        }

        // GET: EmployeeServices
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Employees", "Index");

            ViewBag.EmployeesId = id;
            ViewBag.EmployeeName = name;

            var employeeServiceByEmployee = _context.EmployeeServices.Where(b => b.EmployeesId == id).Include(b => b.Employees).Include(b => b.Services);

            return View(await employeeServiceByEmployee.ToListAsync());
        }

        // GET: EmployeeServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeService = await _context.EmployeeServices
                .Include(e => e.Employees)
                .Include(e => e.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeService == null)
            {
                return NotFound();
            }

            return RedirectToAction("Details", "Services", new { id = employeeService.ServicesId, name = employeeService.Services.Name });
        }


        // GET: EmployeeServices/Create
        public IActionResult Create()
        {
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Name");
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name"); // Додаємо цей рядок
            return View();
        }


        // POST: EmployeeServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int employeesId, [Bind("ServicesId,EmployeesId,Id")] EmployeeService employeeService)
        {

                _context.Add(employeeService);
                await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Employees", new { id =  employeesId, name = _context.Employees.Where(c => c.Id == employeesId).FirstOrDefault().Name });
        }


        [HttpGet]
        public IActionResult GetEmployees(int serviceId)
        {
            var employees = _context.EmployeeServices
                .Where(es => es.ServicesId == serviceId)
                .Select(es => new { id = es.EmployeesId, name = es.Employees.Name })
                .ToList();

            return Json(employees);
        }


        // GET: EmployeeServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeService = await _context.EmployeeServices.FindAsync(id);
            if (employeeService == null)
            {
                return NotFound();
            }
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Name", employeeService.EmployeesId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", employeeService.ServicesId); // Додаємо цей рядок
            return View(employeeService);
        }


        // POST: EmployeeServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("ServicesId,EmployeesId,Id")] EmployeeService employeeService)
        {
            if (id != employeeService.Id)
            {
                return NotFound();
            }

                try
                {
                    _context.Update(employeeService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeServiceExists(employeeService.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            return RedirectToAction("Details", "Employees", new { id = employeeService.EmployeesId });
        }

        // GET: EmployeeServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeService = await _context.EmployeeServices
                .Include(e => e.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeService == null)
            {
                return NotFound();
            }

            return View(employeeService);
        }

        // POST: EmployeeServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employeeService = await _context.EmployeeServices.FindAsync(id);
            if (employeeService != null)
            {
                _context.EmployeeServices.Remove(employeeService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeServiceExists(int id)
        {
            return _context.EmployeeServices.Any(e => e.Id == id);
        }
    }
}

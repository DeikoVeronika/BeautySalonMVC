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
        public async Task<IActionResult> Index()
        {
            var dbbeautySalonContext = _context.EmployeeServices.Include(e => e.Employees);
            return View(await dbbeautySalonContext.ToListAsync());
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
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employeeService == null)
            {
                return NotFound();
            }

            return View(employeeService);
        }

        // GET: EmployeeServices/Create
        public IActionResult Create()
        {
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Name");
            return View();
        }

        // POST: EmployeeServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicesId,EmployeesId,Id")] EmployeeService employeeService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Name", employeeService.EmployeesId);
            return View(employeeService);
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

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeesId"] = new SelectList(_context.Employees, "Id", "Name", employeeService.EmployeesId);
            return View(employeeService);
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

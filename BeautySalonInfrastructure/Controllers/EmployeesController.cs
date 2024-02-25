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
    public class EmployeesController : Controller
    {
        private readonly DbbeautySalonContext _context;

        public EmployeesController(DbbeautySalonContext context)
        {
            _context = context;
        }

   //     GET: Employees
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Employees", "Index");

            ViewBag.PositionsId = id;
            ViewBag.PositionName = name;

            var employeeByPosition = _context.Employees.Where(e => e.PositionsId == id).Include(e => e.Positions);
            return View(await employeeByPosition.ToListAsync());

        }

        //public async task<iactionresult> Index(int? id, string? name)
        //{
        //    if (id == null) return RedirectToAction("Employees", "Index");

        // ViewBag.PositionsId = id;
        //    ViewBag.PositionName = name;

        //    var employeeByPosition = _context.Employees.where(e => e.PositionsId == id).orderby(e => e.name).include(e => e.Positions);
        //    return view(await employeeByPosition.tolistasync());
        //}


        public async Task<IActionResult> AllEmployees()
        {
            var allEmployees = await _context.Employees.Include(e => e.Positions).ToListAsync();
            return View(allEmployees);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Positions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "EmployeeServices", new { id = employee.Id, name = employee.Name });
        }

        // GET: Employees/Create
        public IActionResult Create(int? positionsId)
        {
            ViewBag.PositionsId = new SelectList(_context.Positions.OrderBy(p => p.Name), "Id", "Name", positionsId);
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int positionsId, [Bind("Name,PositionsId")] Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Positions", new { id = positionsId, name = _context.Positions.Where(e => e.Id == positionsId).FirstOrDefault().Name });
        }


        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["PositionsId"] = new SelectList(_context.Positions.OrderBy(p => p.Name), "Id", "Name", employee.PositionsId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PositionsId,Id")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(employee);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "Positions", new { id = employee.PositionsId });
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Positions)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            var positionsId = employee.PositionsId; // Зберігаємо ID позиції перед видаленням
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Positions", new { id = positionsId }); // Перенаправляємо до переліку працівників конкретної послуги
        }




        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}

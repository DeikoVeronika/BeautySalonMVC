using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeautySalonDomain.Model;
using BeautySalonInfrastructure;
using System.Diagnostics.Eventing.Reader;

namespace BeautySalonInfrastructure.Controllers
{
    public class ServicesController : Controller
    {
        private readonly DbbeautySalonContext _context;

        public ServicesController(DbbeautySalonContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Services", "Index");

            ViewBag.TypeServiceId = id;
            ViewBag.TypeServiceName = name;
            var serviceByTypeService = _context.Services.Where(b => b.TypeServiceId == id).Include(b => b.TypeService);

            return View(await serviceByTypeService.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.TypeService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create(int? typeServiceId)
        {
            var typeServices = _context.TypeServices.OrderBy(ts => ts.Name).ToList();
            ViewBag.TypeServiceId = new SelectList(typeServices, "Id", "Name", typeServiceId);
            return View();
        }



        // POST: Services/Create
        [HttpPost]
         [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int typeServiceId, [Bind("Name,Description,Price,TypeServiceId")] Service service)
        {
            _context.Add(service);

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "TypeServices", new { id = typeServiceId, name = _context.TypeServices.Where(c => c.Id == typeServiceId).FirstOrDefault().Name });
        }

        [HttpGet]
        public async Task<IActionResult> GetServices(int typeServiceId)
        {
            var services = await _context.Services
                .Where(s => s.TypeServiceId == typeServiceId)
                .ToListAsync();

            return Json(services);
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceDescription(int serviceId)
        {
            var service = await _context.Services
                .Where(s => s.Id == serviceId)
                .FirstOrDefaultAsync();

            return Json(service);
        }

        [HttpGet]
        public async Task<IActionResult> GetTypeService(int serviceId)
        {
            var service = await _context.Services
              .Where(s => s.Id == serviceId)
             .FirstOrDefaultAsync();

            return Json(service);
        }





        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["TypeServiceId"] = new SelectList(_context.TypeServices.OrderBy(p => p.Name), "Id", "Name", service.TypeServiceId);
            return View(service);
        }


        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Price,TypeServiceId,Id")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(service);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(service.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "TypeServices", new { id = service.TypeServiceId });
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "TypeServices");
            }

            var service = await _context.Services
                .Include(s => s.TypeService)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return RedirectToAction("Index", "TypeServices");
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            var typeServiceId = service.TypeServiceId; // Save the TypeServiceId before deleting the service
            _context.Services.Remove(service);

            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "TypeServices", new { id = typeServiceId });
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}

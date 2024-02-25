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
    public class TypeServicesController : Controller
    {
        private readonly DbbeautySalonContext _context;

        public TypeServicesController(DbbeautySalonContext context)
        {
            _context = context;
        }

        // GET: TypeServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeServices.ToListAsync());
        }

        // GET: TypeServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeService = await _context.TypeServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeService == null)
            {
                return NotFound();
            }

            // return View(typeService);
            return RedirectToAction("Index", "Services", new { id = typeService.Id, name = typeService.Name });
        }

        // GET: TypeServices/Create
        public IActionResult Create()
        {
            ViewBag.TypeServiceId = new SelectList(_context.TypeServices, "Id", "Name");
            return View();
        }

        // POST: TypeServices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] TypeService typeService)
        {
            if (ModelState.IsValid)
            {
                // Перевірка, чи існує вже служба з такою ж назвою
                if (_context.TypeServices.Any(ts => ts.Name == typeService.Name))
                {
                    ModelState.AddModelError("Name", "Категорія з такою назвою вже існує");
                    ViewBag.TypeServiceId = new SelectList(_context.TypeServices, "Id", "Name");
                    return View(typeService);
                }

                _context.Add(typeService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeService);
        }



        // GET: TypeServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeService = await _context.TypeServices.FindAsync(id);
            if (typeService == null)
            {
                return NotFound();
            }
            return View(typeService);
        }

        // POST: TypeServices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] TypeService typeService)
        {
            if (id != typeService.Id)
            {
                //return NotFound();
                return RedirectToAction(nameof(Index));

            }

            if (ModelState.IsValid)
            {
                // Перевірка, чи існує вже служба з такою ж назвою
                if (_context.TypeServices.Any(ts => ts.Name == typeService.Name && ts.Id != typeService.Id))
                {
                    ModelState.AddModelError("Name", "Категорія з такою назвою вже існує");
                    return View(typeService);
                }

                try
                {
                    _context.Update(typeService);
                    await _context.SaveChangesAsync();
                }   
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeServiceExists(typeService.Id))
                    {
                        //return NotFound();
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(typeService);
        }


        // GET: TypeServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return RedirectToAction(nameof(Index));
            }

            var typeService = await _context.TypeServices
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeService == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(typeService);
        }

        // POST: TypeServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeService = await _context.TypeServices.FindAsync(id);
            if (typeService != null)
            {
                _context.TypeServices.Remove(typeService);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeServiceExists(int id)
        {
            return _context.TypeServices.Any(e => e.Id == id);
        }
    }
}
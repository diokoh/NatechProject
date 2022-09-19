using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NatechProject.Data;
using NatechProject.Models;

namespace NatechProject.Controllers
{
    public class SalesmenController : Controller
    {
        private readonly SaleAppContext _context;

        public SalesmenController(SaleAppContext context)
        {
            _context = context;
        }

        // GET: Salesmen
        public async Task<IActionResult> Index()
        {
            var sales =  _context.Sales.ToList();
            var salesmen = _context.Salesmen.ToList();
            Dictionary<int,double> salesPerSalesman = sales
                .GroupBy(r => r.SalesmanId)
                .Select(
                    groupedSales =>
                    { 
                    return new Tuple<int,double>(groupedSales.Key, groupedSales.Sum(x => x.SalePrice));
                    }
                )
                .ToDictionary(tuple=> tuple.Item1,tuple=> tuple.Item2);

           
            foreach (Salesman s in salesmen)
            {
                s.Commission = 0.1 * ((salesPerSalesman.ContainsKey(s.SalesmanId)) ? salesPerSalesman[s.SalesmanId] : 0);
            }
            return View(salesmen);
        }

        // GET: Salesmen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Salesmen == null)
            {
                return NotFound();
            }

            var salesman = await _context.Salesmen
                .FirstOrDefaultAsync(m => m.SalesmanId == id);
            if (salesman == null)
            {
                return NotFound();
            }
            var sales = _context.Sales.ToList();
            salesman.monthSales =  sales.Where(i => i.SalesmanId==salesman.SalesmanId)
                   .GroupBy(i => i.SaleDate.Month)
                   .Select(g => new MonthSales {
                       Month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(g.Key),
                       TotalSales = g.Sum(i => i.SalePrice),
                       TotalCommission = (g.Sum(i => i.SalePrice)) * 0.1
                   }).ToList();


            return View(salesman);
        }

        // GET: Salesmen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salesmen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesmanId,Name")] Salesman salesman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salesman);
        }

        // GET: Salesmen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Salesmen == null)
            {
                return NotFound();
            }

            var salesman = await _context.Salesmen.FindAsync(id);
            if (salesman == null)
            {
                return NotFound();
            }
            return View(salesman);
        }

        // POST: Salesmen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesmanId,Name")] Salesman salesman)
        {
            if (id != salesman.SalesmanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesmanExists(salesman.SalesmanId))
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
            return View(salesman);
        }

        // GET: Salesmen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Salesmen == null)
            {
                return NotFound();
            }

            var salesman = await _context.Salesmen
                .FirstOrDefaultAsync(m => m.SalesmanId == id);
            if (salesman == null)
            {
                return NotFound();
            }

            return View(salesman);
        }

        // POST: Salesmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Salesmen == null)
            {
                return Problem("Entity set 'SaleAppContext.Salesmen'  is null.");
            }
            var salesman = await _context.Salesmen.FindAsync(id);
            if (salesman != null)
            {
                _context.Salesmen.Remove(salesman);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesmanExists(int id)
        {
          return _context.Salesmen.Any(e => e.SalesmanId == id);
        }
    }
}

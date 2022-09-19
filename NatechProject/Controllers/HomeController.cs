using Microsoft.AspNetCore.Mvc;
using NatechProject.Data;
using NatechProject.Models;
using System.Diagnostics;

namespace NatechProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SaleAppContext _context;
        public HomeController(ILogger<HomeController> logger, SaleAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var sales = _context.Sales.ToList();
            var salesmen = _context.Salesmen.ToList();
            int salesmenCount = salesmen.Count();
            double totalSalesAmount = sales.Sum(x => x.SalePrice);
            HomeViewModel hmv = new HomeViewModel(salesmenCount, totalSalesAmount);
            
            return View(hmv);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
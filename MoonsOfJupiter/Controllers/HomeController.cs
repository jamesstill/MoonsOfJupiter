using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoonsOfJupiter.Models;

namespace MoonsOfJupiter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // new up a default date range for the upcoming week
            var vm = new DateRangeViewModel { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7) };
            return View(vm);
        }

        public IActionResult Display(DateRangeViewModel vm)
        {
            if (vm.StartDate == null)
            {
                vm.StartDate = DateTime.Today;
            }

            if (vm.EndDate == null)
            {
                vm.EndDate = DateTime.Today.AddDays(7);
            }
            
            if (vm.StartDate > vm.EndDate)
            {
                vm.StartDate = DateTime.Today;
                vm.EndDate = DateTime.Today.AddDays(7);
            }

            vm.CalculateDateRange();
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

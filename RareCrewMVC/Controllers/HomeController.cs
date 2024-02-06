using Microsoft.AspNetCore.Mvc;
using RareCrewMVC.Models;
using RareCrewMVC.Obrade;
using System.Diagnostics;
using System.Threading.Tasks; // Import this namespace for Task<T>

namespace RareCrewMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HTMLgenerator _htmlGenerator;

        public HomeController(HTMLgenerator htmlGenerator)
        {
            _htmlGenerator = htmlGenerator;
        }

        public async Task<IActionResult> GenerateReport()
        {
            await _htmlGenerator.GenerateHtmlReportAsync();
            return Content("HTML report generated successfully!"); // Or you can return a view
        }

        public async Task<IActionResult> Index()
        {
            // Call GenerateReport method
            await GenerateReport();
            // Continue with other logic
            return View();
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

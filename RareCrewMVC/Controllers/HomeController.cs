using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Mvc;
using RareCrewMVC.Models;
using RareCrewMVC.Obrade;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks; // Import this namespace for Task<T>

namespace RareCrewMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly UcitajZaPieChart _ucitajZaPieChart;
        private readonly HTMLgenerator _htmlGenerator;
        private readonly GenerisiChart _generisiPie;

        public HomeController(HTMLgenerator htmlGenerator, UcitajZaPieChart ucitajZaPieChart, GenerisiChart generisiPie)
        {
            _htmlGenerator = htmlGenerator;
            _ucitajZaPieChart = ucitajZaPieChart;
            _generisiPie = generisiPie;
        }
        
        public async Task<IActionResult> GenerateReport()
        {
            await _htmlGenerator.GenerateHtmlReportAsync();
            return Content("HTML report generated successfully!"); // Or you can return a view
        }

        private const string putanja = @"D:\ChartPng.png";
        private const int resWidth = 1920;
        private const int resLenght = 1080;
        public async Task<IActionResult> Index()
        {   
            var radnici = await _ucitajZaPieChart.LoadDataAsync();
            GenerisiChart.GenerateChartPng(
            radnici,
            putanja,
            resWidth,
            resLenght);
            await GenerateReport();
            

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

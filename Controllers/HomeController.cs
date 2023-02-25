using Assignment.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Globalization;

namespace Assignment.Controllers
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
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(IFormFile upload)
        {
            if (ModelState.IsValid)
            {

                if (upload != null && upload.Length > 0)
                {

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        DataTable dataTable = new DataTable();

                        //Stream stream = file.InputStream;
                        // Stream stream = new FileStream(file, FileMode.Create); 
                        using(var stream = upload.OpenReadStream())
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), CultureInfo.InvariantCulture, true))
                        {
                            using(var dr = new CsvDataReader(csvReader))
                            {
                                dataTable.Load(dr);
                            }
                        }
                            
                        return View(dataTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please upload a csv file");
                }
            }
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
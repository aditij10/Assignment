using Assignment.Models;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Web;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        string path = "";
      //  decimal dataPointFell = 0;
       // decimal dataPointFound = 0;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return Redirect("/Home/Upload");

            //return View();

        }

        public ActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(IFormFile uploadedFile)
        {
            string filePath = string.Empty;
            string webRootPath = _webHostEnvironment.WebRootPath;
            string contentRootPath = _webHostEnvironment.ContentRootPath;
            decimal total_metiorites_landing = 0;
            decimal total_metiorites_Fell = 0;
            decimal total_metiorites_Found = 0;

            path = "";
            path = Path.Combine(webRootPath, "Uploads");
            filePath = Path.Combine(path, uploadedFile.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                uploadedFile.CopyToAsync(fileStream);
            }

            if (ModelState.IsValid)
            {

                if (uploadedFile != null && uploadedFile.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    if (uploadedFile.FileName.EndsWith(".csv"))
                    {
                        DataTable dataTable = new DataTable();

                        using (var stream = uploadedFile.OpenReadStream())
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), CultureInfo.InvariantCulture, true))
                        {
                            using (var dr = new CsvDataReader(csvReader))
                            {
                                dataTable.Load(dr);
                            }
                        }

                        //PErform calculations to get data points for Pie chart
                        //

                        total_metiorites_landing = dataTable.AsEnumerable().Where(x => x["Fall"].ToString() == "Fell" || x["Fall"].ToString() == "Found").Count();
                        total_metiorites_Fell = dataTable.AsEnumerable().Where(x => x["Fall"].ToString() == "Fell").Count();
                        total_metiorites_Found = dataTable.AsEnumerable().Where(x => x["Fall"].ToString() == "Found").Count();

                       decimal dataPointFell = Math.Round((total_metiorites_Fell / total_metiorites_landing) * 100, 2);
                       decimal dataPointFound = Math.Round ((total_metiorites_Found / total_metiorites_landing) * 100,2);

                        ViewData["dpFell"] = dataPointFell;
                        ViewData["dpFound"] = dataPointFound;
                        
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

        public IActionResult Charts(decimal dataPointFell , decimal dataPointFound )
        {

            ViewData["dpFell"] = dataPointFell;
            ViewData["dpFound"] = dataPointFound;

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
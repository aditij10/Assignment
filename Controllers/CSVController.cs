using Assignment.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace Assignment.Controllers
{
    public class CSVController : Controller
    {
        // GET: /HelloWorld/

        string fPath = string.Empty; //"enter path"; 
        public string Index()
        {
            return "This is my default action...";
        }

        public string Welcome()
        {
            return "This is the Welcome action method...";
        }
        /*\
        public ActionResult Team(string postedFile)
        {
            List<TeamModel> team = new List<TeamModel>();
            string filePath = string.Empty;

            if (postedFile != null)
            {
                string path = *Server *.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.* FileName *);
                string extension = Path.GetExtension(postedFile.* FileName *);
                postedFile.* SaveAs * (filePath);

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                //Execute a loop over the rows.
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        team.Add(new TeamModel
                        {
                            ID = Convert.ToInt32(row.Split(',')[0]),
                            player_name = row.Split(',')[1],
                            quality = row.Split(',')[2],
                            overall = row.Split(',')[2],
                            club = row.Split(',')[2],
                            league = row.Split(',')[2],
                            nationality = row.Split(',')[2],
                            position = row.Split(',')[2],
                            age = row.Split(',')[2],
                        });
                    }
                }
            }

            return View(team);
        }

        */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
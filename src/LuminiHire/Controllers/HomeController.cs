using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LuminiHire.Models;
using System.IO;
using System.Data.SQLite;
using Microsoft.Data.Sqlite;

namespace LuminiHire.Controllers
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
            //string cs = $"URI=file:{Directory.GetCurrentDirectory()}{Path.DirectorySeparatorChar}data{Path.DirectorySeparatorChar}lumini.db;Version=3;";
            
            //using var con = new SQLiteConnection(cs);
            //con.Open();

            //using var cmd = con.CreateCommand();

            //cmd.CommandText = "CREATE TABLE cars(id INTEGER PRIMARY KEY, name TEXT, price INT)";
            //cmd.ExecuteNonQuery();

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

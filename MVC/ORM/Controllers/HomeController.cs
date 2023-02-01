using Microsoft.AspNetCore.Mvc;
using MVCLec5.Data;
using MVCLec5.Models;
using System.Diagnostics;
using System.Net.Cache;

namespace MVCLec5.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult myfun()
        {
            // create new department in database 
            //ITIDB db = new ITIDB();
            //Student std = new Student()
            //{
            //    // id is autoincremented in database 
            //    Name = "Ali",
            //    Age = 17,
            //};
            //// add student to some department directly
            //Department dept = db.Departments.FirstOrDefault(
            //    a => a.DeptId == 1
            //    );
            //dept.students.Add(std);
            //db.SaveChanges();
            //--------**//
            //Student std = new Student()
            //{
            //    // id is autoincremented in database 
            //    Name = "Ali",
            //    Age  = 17,
            //    DeptNo = 1
            //};
            //-----------**//
            //Student std = new Student()
            //{
            //    // id is autoincremented in database 
            //    Name = "Ali",
            //    Age  = 17,
            //    DeptNo = 1
            //};
            //db.Students.Add(std);
            //db.SaveChanges();
            //-----------**//
            //Department dept1 = new Department()
            //{
            //    DeptId = 1,
            //    DeptName = ".Net"
            //};
            //// db.Add(dept1);
            //db.Departments.Add(dept1); // added to memory 
            //db.SaveChanges(); // updated in database 
            return Content("Added");
        }
    }
}
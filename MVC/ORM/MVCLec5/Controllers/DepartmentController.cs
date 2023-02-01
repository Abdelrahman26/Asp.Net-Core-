using Microsoft.AspNetCore.Mvc;
using MVCLec5.Data;

namespace MVCLec5.Controllers
{
   
    public class DepartmentController : Controller
    {
        // do crud operation on department 
        // create object from ITIDB
        // create refrence of type ITIDB 
        ITIDB db;
        // ask for object of type ITIDB (dependency injection)
        public DepartmentController(ITIDB _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            // list departments in database 
            return View(db.Departments.ToList());
        }
    }
}

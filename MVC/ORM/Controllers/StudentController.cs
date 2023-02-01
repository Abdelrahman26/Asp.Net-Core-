using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCLec5.Data;
using MVCLec5.Migrations;
using MVCLec5.Models;
using System.Runtime.Intrinsics.Arm;

namespace MVCLec5.Controllers
{
    public class StudentController : Controller
    {
        ITIDB db;
        public StudentController(ITIDB _db) => db = _db;
    
        public IActionResult Index()
        {
            // to pass department data with student data to the view, there is more than one option 
            // eager load 
            // lazy load
            // expicit load 
            return View(db.Students.Include(a => a.Department).ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            // list 
            // ICollection<Department> dept = (ICollection<Department>)db.Departments.ToList();
            // send to my view
            // ViewBag.depts = dept
            // another option to show list automatically
            // new selectList(list, value field, text field)
            // when text select, value sent to server -- like<option>
            // SelectList depts = new SelectList(db.Departments.ToList(), "DeptId", "DeptName");
            //ViewBag.depts = depts;
            // so, option of <select> has created automatically 
            // relationship one to many between department and student
            ViewBag.depts = new SelectList(db.Departments.ToList(), "DeptId", "DeptName");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(std);
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.depts = new SelectList(db.Departments.ToList(), "DeptId", "DeptName");
            return View(std);
        }
        
       // Edit
       [HttpGet]
       public IActionResult Edit(int? id)
       {
            if (id == null)
                return NotFound();

            Student std = db.Students.FirstOrDefault(a => a.Id == id);
            if (std == null) // id doesn't existe 
            {
                return NotFound();
            }
            ViewBag.depts = new SelectList(db.Departments.ToList(), "DeptId", "DeptName");
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                Student oldstd = db.Students.FirstOrDefault(a => a.Id == std.Id);
                if (oldstd == null)
                    return NotFound();
                oldstd.Name = std.Name;
                oldstd.Age = std.Age;
                oldstd.DeptNo = std.DeptNo;
                oldstd.Username = std.Username;
                db.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.depts = new SelectList(db.Departments.ToList(), "DeptId", "DeptName");
            return View(std);
        }

        // Delete 
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Student std = db.Students.FirstOrDefault(a => a.Id == id);
            if (std == null) // id doesn't existe 
            {
                return NotFound();
            }
            db.Students.Remove(std);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        // Details
        public IActionResult Details(int? id) // int nullable, default of it is null 
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student std = db.Students.FirstOrDefault(a => a.Id == id);
            if (std == null) // id doesn't existe 
            {
                return NotFound();
            }

            //ViewBag.depts = new SelectList(db.Departments.ToList(), "DeptId", "DeptName");
            ViewBag.depts = db.Departments.FirstOrDefault(a => a.DeptId == std.DeptNo);
            return View(std);
        }
        
        // Check username
        // can't reach it without js files of client side (I don't know why so far)
        public IActionResult checkUserName(string Username, int Id) // ask model Binder about username and id
        {
            Student std = db.Students.FirstOrDefault(a=>a.Username == Username && 
            a.Id != Id);
            if (std == null)
                return Json(true); // you can use this username
            else
                return Json(false); // can't use this username
              //return json("username already exists");
        }

        
    }
    
   
}

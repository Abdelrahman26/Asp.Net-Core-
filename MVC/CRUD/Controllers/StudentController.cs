using CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace CRUD.Controllers
{
    public class StudentController : Controller
    {
        // IActionResult is isterface (Parent class)
        // a lot of classes implements this interface
        // view() and file(),notfound(), content() ... implements the intrface, so can return view using IActionResult
        // so, IActionResult gonna used with any action type
        // by convention, index action, used to show list of some data
        IStudent db;
        public StudentController(IStudent _db)
        {
            // dependency injection
            db = _db;
        }
        public IActionResult Index()
        {
            List<Student> model = db.GetAllStudents();
            return View(model); // return viewResult which inhert from IActionResult
        }

        // create new student
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Action selector : get, post
        [HttpPost]
        public IActionResult Create(Student std, IFormFile imgsrc)
        {
            // calc the value of new id
            std.Id = db.GetNextId();
            if(imgsrc != null)
            {
                // filename = (name.extention)
                string extention = imgsrc.FileName.Split('.')[1];//get extention
                string imgname = std.Id.ToString() + ('.') +extention;
                // create object of type filestream that gonna resote the image 
                using (var obj = new FileStream(@".\wwwroot\images\imgname", FileMode.Create))
                {
                    // copy imgsrc to hard disk
                    imgsrc.CopyTo(obj);
                }
                std.Stdimg = imgname; // name.extention
            }
            db.AddStudent(std);
            return RedirectToAction("index");// return resulte of type re-direction 301,302
            //Which return to browser to ask it to order index action 
            //return View("index", db.GetAllStudents());// which show the list, it just call index action 
        }
        
        public IActionResult Details(int? id) // int nullable, default of it is null 
        {
            if(id == null)
            {
                return BadRequest();
            }
            Student std = db.GetStudentById(id.Value);
            if (std == null) // id doesn't existe 
            {
                return NotFound();
            }

            return View(std);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Student std = db.GetStudentById(id.Value);
            if (std == null) // id doesn't existe 
            {
                return NotFound();
            }
            db.DeleteById(id.Value);
            return RedirectToAction("index");
        }
        [HttpGet]
        public IActionResult edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student std = db.GetStudentById(id.Value);
            if (std == null) // id doesn't existe 
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        public IActionResult edit(Student std)
        {
            db.EditStudent(std);
            return RedirectToAction("index");
        }

        [HttpGet]
        public IActionResult changeImage(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student std = db.GetStudentById(id.Value);
            if (std == null) // id doesn't existe 
            {
                return NotFound();
            }
            return View(std);
        }

        [HttpPost]
        public IActionResult changeImage(Student std, IFormFile imgsrc)
        {
            //clear cookies if you faces a problem here 
            // calc the value of new id
            if (imgsrc != null)
            {
                // filename = (name.extention)
                string extention = imgsrc.FileName.Split('.')[1];//get extention
                string imgname = std.Id.ToString() + ('.') + extention;
                //imgname = "5.png";
                // create object of type filestream that gonna store the image 
                using (var obj = new FileStream(@".\wwwroot\images\"+imgname, FileMode.Create))
                {
                    // copy imgsrc to hard disk
                    imgsrc.CopyTo(obj);
                }
                std.Stdimg = imgname; // name.extention
                db.EditStudentImage(std);
            }

             
            return RedirectToAction("index");// return resulte of type re-direction 301,302
            //which return to browser to ask it to order index action 
            //return View("index", db.GetAllStudents());// which show the list, it just call index action
        }



    }
}

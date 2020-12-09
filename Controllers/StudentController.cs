using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign3.Models;

namespace Assign3.Controllers
{
    public class StudentController : Controller
    {
        //Code Credit: Christine Bittle
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }
        //Get : /Student/List
        public ActionResult List()
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents();
            return View(Students);
        }
        //Get : /Student/Show/{id}
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student NewStudent = controller.FindStudent(id);
            
            return View(NewStudent);
        }
    }
}
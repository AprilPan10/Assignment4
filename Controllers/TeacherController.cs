using Org.BouncyCastle.Asn1.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign3.Models;
using System.Diagnostics;

namespace Assign3.Controllers
{
    public class TeacherController : Controller
    {
        //Code Credit: Christine Bittle
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        //Get : /Teacher/List
        public ActionResult List(string SearchKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(SearchKey);
            return View(Teachers);
        }
        //Get : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);
            return View(NewTeacher);
        }
        //Get : /Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }
        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }
        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();
        }
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string TeacherEmployeenumber, DateTime TeacherHiredate, string TeacherSalary)
        {
            // Identify that this method is running
            //Identify the inputs provided from the form
            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(TeacherEmployeenumber);
            Debug.WriteLine(TeacherHiredate);
            Debug.WriteLine(TeacherSalary);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.TeacherEmployeenumber = TeacherEmployeenumber;
            NewTeacher.TeacherHiredate = TeacherHiredate;
            NewTeacher.TeacherSalary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);
            //if (ModelState.IsValid) { }
            return RedirectToAction("List");
        }

        /// <summary>
        /// Routes to a dynamically generated "Author Update" Page. Gathers information from the database.
        /// </summary>
        /// <param name="id">Id of the Teacher</param>
        /// <returns>A dynamic "Update Teacher" webpage which provides the current information of the teacher and asks the user for new information as part of a form.</returns>
        /// <example>GET : /Teacher/Update/2</example>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }
        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "Teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="TeacherEmployeenumber">The updated Employeenumber of the teacher.</param>
        /// <param name="TeacherHiredate">The updated Hiredate of the teacher.</param>
        /// <param name="TeacherSalary">The updated Salary of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/3
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///"TeacherFname":"April",
        ///	"TeacherLname":"Pan",
        ///	"TeacherEmployeenumber":"123",
        ///	"TeacherHiredate":"11-02-2019",
        ///	"TeacherSalary":"19"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string TeacherEmployeenumber, DateTime TeacherHiredate, string TeacherSalary)
        {
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.TeacherEmployeenumber = TeacherEmployeenumber;
            TeacherInfo.TeacherHiredate = TeacherHiredate;
            TeacherInfo.TeacherSalary = TeacherSalary;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);
            return RedirectToAction("Show/" + id);
        }
    }
}
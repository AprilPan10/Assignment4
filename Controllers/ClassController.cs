using Org.BouncyCastle.Asn1.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assign3.Models;

namespace Assign3.Controllers
{
    public class ClassController : Controller
    {
        //Code Credit: Christine Bittle
        // GET: Class

        public ActionResult Index()
        {
            return View();
        }
        //Get : /Class/List
        public ActionResult List()
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses();
            return View(Classes);
        }
        //Get : /Class/Show/{id}
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class NewClass = controller.FindClass(id);


            return View(NewClass);
        }
    }
}
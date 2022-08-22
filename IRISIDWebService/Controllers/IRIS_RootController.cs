using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IRIS_Camera_web.Controllers
{
    public class IRIS_RootController : Controller
    {
        // GET: IRIS_Root
        public ActionResult IRIS_PAGES()
        {
           
            return View();
        }

        [HttpGet]
        public ActionResult IRIS_CloseApp()
        {
            //  System.Environment.Exit(1);
            return View();
        }
    }
}
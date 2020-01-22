using SecurityLab1_Starter.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecurityLab1_Starter.Controllers
{
    public class InventoryController : Controller
    {
        [Authorize(Users ="testuser2")]
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            //Log the error!!
            LogUtil logger = new LogUtil();
            using (StreamWriter w = System.IO.File.AppendText("C:\\temp\\log.txt"))
            {
                logger.LogToFile(filterContext.Exception.Message, w);
            }


            //Redirect or return a view, but not both.
            filterContext.Result = RedirectToAction("ServerError", "Error");
        }

    }
}
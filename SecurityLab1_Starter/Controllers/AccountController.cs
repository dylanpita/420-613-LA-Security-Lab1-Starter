using SecurityLab1_Starter.Infrastructure.Abstract;
using SecurityLab1_Starter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AuthDemo.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;

        public AccountController(IAuthProvider auth)
        {
            authProvider = auth;
        }
        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(model.UserName, model.Password))
                {
                    LogUtil logger = new LogUtil();
                    using (StreamWriter w = System.IO.File.AppendText("C:\\temp\\useraccess.log"))
                    {
                        logger.LogToFile(model.UserName + " logged in.", w);
                    }
                    return Redirect(Url.Action("Index", "Home"));
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect username or password");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Logout()
        {
            LogUtil logger = new LogUtil();
            using (StreamWriter w = System.IO.File.AppendText("C:\\temp\\useraccess.log"))
            {
                logger.LogToFile(User.Identity.Name + " logged out.", w);
            }
            FormsAuthentication.SignOut();
            return Redirect(Url.Action("Index", "Home"));
        }
    }
}
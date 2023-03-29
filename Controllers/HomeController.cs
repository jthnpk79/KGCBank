using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using KGCBank.Models;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.UI.WebControls;
using KGCBank.Service;

namespace KGCBank.Controllers
{
    public class HomeController : Controller
    {
        RegisterDAL Obj_registerDAL = new RegisterDAL();
        /// <summary>
        /// Hoempage
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        /// <summary>
        /// Product page
        /// </summary>
        /// <returns></returns>
        public ActionResult Products()
        {
            try
            {
                return View();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        /// <summary>
        /// About us
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            try
            {
                return View();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Contact us page
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            try
            {
                return View();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
            
        }
        [HttpPost]
        public ActionResult Contact(ContactModel Obj_Contact)
        {
            try
            {
                if (Obj_registerDAL.InsertContact(Obj_Contact))
                {
                    TempData["InsertMsg"] = "<script>alert('User saved successful')</script>";
                    return RedirectToAction("Create");
                }
                else
                {
                    TempData["InsertErrorMsg"] = "<script>alert('Data not saved')</script>";

                }
                return View();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
            
        }
    }
}
using KGCBank.Common;
using KGCBank.Models;
using KGCBank.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KGCBank.Controllers
{
    public class RegistrationController : Controller
    {
        RegisterDAL Obj_registerDAL = new RegisterDAL();

        /// <summary>
        /// Get user details
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            try
            {
                var data = Obj_registerDAL.GetUsers();
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Create User
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
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
        public ActionResult Create(RegisterModel Obj_Register)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (Obj_registerDAL.InsertUser(Obj_Register))
                    {
                        TempData["InsertMsg"] = "<script>alert('User saved successful')</script>";
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        TempData["InsertErrorMsg"] = "<script>alert('Data not saved')</script>";
                        return View();
                    }
                    
                }
                else 
                {
                    return View(); 
                }
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// List user informations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            try
            {
                var data = Obj_registerDAL.GetUsers().Find(a => a.Id == id);
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Update user informations
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            try
            {
                var data = Obj_registerDAL.GetUsers().Find(a => a.Id == id);
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Update User informations
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(RegisterModel Obj_Register)
        {
            try
            {
                if (Obj_registerDAL.UpdateUser(Obj_Register))
                {
                    TempData["UpdateMsg"] = "<script>alert('User updated successful..')</script>";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["UpdateErrorMsg"] = "<script>alert('Data not updated.')</script>";
                }
                return View();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            try
            {
                int r = Obj_registerDAL.DeleteUser(id);
                if (r > 0)
                {
                    TempData["DeleteMsg"] = "<script>alert('User deleted successful..')</script>";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["DeleteErrorMsg"] = "<script>alert('Data not deleted.')</script>";
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;
using KGCBank.Models;
using Microsoft.Win32;
using System.Web.UI.WebControls;
using KGCBank.Common;

namespace KGCBank.Controllers
{
    public class LoginController : Controller
    {
        Password EncryptData = new Password();
        /// <summary>
        /// Login page
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
        /// Login page
        /// </summary>
        /// <param name="lc"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginModel Obj_Login)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    string mainconn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection sqlconn = new SqlConnection(mainconn);

                    SqlCommand sqlcomm = new SqlCommand("[dbo].[validate_user]", sqlconn);
                    sqlconn.Open();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandType = CommandType.StoredProcedure;
                    sqlcomm.Parameters.AddWithValue("@Username", Obj_Login.Username);
                    sqlcomm.Parameters.AddWithValue("@Password", EncryptData.Encode(Obj_Login.Password));
                    var roleParam = new SqlParameter("@Role", SqlDbType.VarChar, 50);
                    roleParam.Direction = ParameterDirection.Output;
                    sqlcomm.Parameters.Add(roleParam);
                    sqlcomm.ExecuteNonQuery();
                    var role = roleParam.Value.ToString();
                    var Username = Obj_Login.Username;

                    if (role == "Admin")
                    {
                        FormsAuthentication.SetAuthCookie(Obj_Login.Username, true);
                        Session["Username"] = Obj_Login.Username.ToString();
                        return RedirectToAction("WelcomeAdmin");
                    }
                    else if (role == "User")

                    {
                        FormsAuthentication.SetAuthCookie(Obj_Login.Username, true);
                        Session["Username"] = Obj_Login.Username.ToString();
                        string username = Session["Username"].ToString();
                        return RedirectToAction("welcome");
                    }
                    else
                    {
                        // If the user's role cannot be determined, redirect to an error page.
                        ViewData["message"] = "Login details incorrect..!";
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
        /// Welcome page for admin
        /// </summary>
        /// <returns></returns>
        public ActionResult welcome()
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
        /// Welcome page for user
        /// </summary>
        /// <returns></returns>
        public ActionResult WelcomeAdmin()
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
        public ActionResult ForgotPassword()
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
    }
}
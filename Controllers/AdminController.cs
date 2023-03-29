using KGCBank.Models;
using KGCBank.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Security.Cryptography.Xml;
using KGCBank.Common;

namespace KGCBank.Controllers
{
    public class AdminController : Controller
    {
        AdminRepo Obj_AdminRepo = new AdminRepo();
        Password EncryptData = new Password();

        /// <summary>
        /// List details
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            try
            {
                var data = Obj_AdminRepo.GetBank();
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Login details
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginList()
        {
            try
            {
                var data = Obj_AdminRepo.GetUsers();
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// This function is used to list the accounts
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountList()
        {
            try
            {
                var data = Obj_AdminRepo.GetAccounts();
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        public ActionResult AdminList()
        {
            try
            {
                var data = Obj_AdminRepo.GetAdmin();
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Hompage
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("Index","Home");
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// Add Bank details and branch
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBank()
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
        /// Add Bank details and branch
        /// </summary>
        /// <param name="Obj_Bank"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddBank(BankModel Obj_Bank)
        {
            try
            {
                if (Obj_AdminRepo.InsertBank(Obj_Bank))
                {
                    TempData["InsertMsg"] = "<script>alert('User saved successful..')</script>";
                    return RedirectToAction("AddBank");
                }
                else
                {
                    TempData["InsertErrorMsg"] = "<script>alert('Data not saved.')</script>";
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
        /// Customer details
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerDetails()
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
        /// This function is used for Account verification
        /// </summary>
        /// <returns></returns>
        public ActionResult AccountVerification(int id)
        {
            try
            {
                var Data = Obj_AdminRepo.GetAccounts().Find(a => a.Id == id);
                return View(Data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        /// <summary>
        /// This function is used for Account verification
        /// </summary>
        /// <param name="Obj_Account"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AccountVerification(AccountModel Obj_Account)
        {
            try
            {
                if (Obj_AdminRepo.UpdateAccount(Obj_Account))
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
        /// This function is used to delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteAccount(int id)
        {
            try
            {
                int Read = Obj_AdminRepo.DeleteAccount(id);
                if (Read > 0)
                {
                    TempData["DeleteMsg"] = "<script>alert('Account deleted successful..')</script>";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["DeleteErrorMsg"] = "<script>alert('Account not deleted.')</script>";
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
        /// This function is used to delete admin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteAdmin(int id)
        {
            try
            {
                int Read = Obj_AdminRepo.DeleteAdmin(id);
                if (Read > 0)
                {
                    TempData["DeleteMsg"] = "<script>alert('Admin deleted successful..')</script>";
                    return RedirectToAction("List");
                }
                else
                {
                    TempData["DeleteErrorMsg"] = "<script>alert('Admin not deleted.')</script>";
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
        /// Add admin to the database
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAdmin()
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
        /// Add admin to the database
        /// </summary>
        /// <param name="Obj_Login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAdmin(LoginModel Obj_Login)
        {
            try
            {
                if (Obj_AdminRepo.InsertAdmin(Obj_Login))
                {
                    TempData["InsertMsg"] = "<script>alert('Admin saved successful')</script>";
                    return RedirectToAction("AdminList");
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

        /// <summary>
        /// This function is used to change the admin password
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
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
        /// This function is used to change the admin password
        /// </summary>
        /// <param name="Obj_ChangePassword"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordModel Obj_ChangePassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Obj_AdminRepo.UpdatePassword(Obj_ChangePassword))
                    {
                        TempData["InsertMsg"] = "<script>alert('Password changed successfully')</script>";
                        return RedirectToAction("ChangePassword");
                    }
                    else
                    {
                        TempData["InsertErrorMsg"] = "<script>alert('Password not chnaged')</script>";
                        return View();
                    }
                }
                else
                {
                    return View(Obj_ChangePassword);
                }
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// This function is used to transfer money
        /// </summary>
        /// <returns></returns>
        public ActionResult Transfer()
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
        /// This function is used to transfer money
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="IFSC"></param>
        /// <param name="AccountHolder"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Transfer(string AccountNumber, string IFSC, string AccountHolder, float Amount)
        {
            try
            {
                int Balance;
                string Obj_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection Obj_Connection = new SqlConnection(Obj_ConnectionString);
                Obj_Connection.Open();

                //Check Valid Account Details
                SqlDataAdapter Obj_DataAdapter = new SqlDataAdapter("Select * From Register C Inner Join Account A on C.Id = A.RegId " +
                    "Inner Join Branch B on A.BranchId = B.Id  Where A.AccNumber = " + $" '{AccountNumber}' and B.IFSC = '{IFSC}' and " +
                    $"C.Firstname = '{AccountHolder}'", Obj_Connection);
                DataSet Obj_DataSet = new DataSet();
                Obj_DataAdapter.Fill(Obj_DataSet);

                if (Obj_DataSet != null && Obj_DataSet.Tables[0].Rows.Count != 0)
                {
                    //Balance check
                    SqlCommand Obj_Command = new SqlCommand("Select Balance from Account A Inner Join Register C on C.Id = A.RegId " +
                        "Where C.Username = @username", Obj_Connection);
                    Obj_Command.Parameters.AddWithValue("@username", Session["Username"]);
                    SqlDataReader Obj_DataReader = Obj_Command.ExecuteReader();
                    if (Obj_DataReader.Read())
                    {
                        Balance = Convert.ToInt32(Obj_DataReader["Balance"]);

                        if (Balance >= Amount)
                        {
                            Obj_DataReader.Close();

                            SqlCommand Obj_Command2 = new SqlCommand("sp_update_transaction", Obj_Connection);
                            Obj_Command2.CommandType = CommandType.StoredProcedure;
                            Obj_Command2.Parameters.AddWithValue("@Username", Session["Username"]);
                            Obj_Command2.Parameters.AddWithValue("@AccNumber", AccountNumber);
                            Obj_Command2.Parameters.AddWithValue("@IFSC", IFSC);
                            Obj_Command2.Parameters.AddWithValue("@AccHolder", AccountHolder);
                            Obj_Command2.Parameters.AddWithValue("@Amount", Amount);
                            Obj_Command2.Parameters.Add("@text", SqlDbType.Char, 500);
                            Obj_Command2.Parameters["@text"].Direction = ParameterDirection.Output;

                            Obj_Command2.ExecuteNonQuery();
                            //ViewBag.text = (string)Cmd.Parameters["@text"].Value; //first check whether it is null or not then use this line

                            if (ViewBag.text == null)
                            {
                                SqlCommand Obj_Command3 = new SqlCommand("sp_add_transaction_record", Obj_Connection);
                                Obj_Command3.CommandType = CommandType.StoredProcedure;
                                Obj_Command3.Parameters.AddWithValue("@Username", Session["Username"]);
                                Obj_Command3.Parameters.AddWithValue("@AccNumber", AccountNumber);
                                Obj_Command3.Parameters.AddWithValue("@Amount", Amount);

                                Obj_Command3.ExecuteNonQuery();

                                ViewBag.text = "Transaction Successful.";
                            }
                        }
                        else
                        {
                            ViewBag.text = "You don't have enough Balance !";
                        }
                    }
                }
                else
                {
                    ViewBag.text = "Please Check User Credentials !!!";
                }
                Obj_Connection.Close();
                return View();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
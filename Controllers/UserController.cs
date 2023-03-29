using KGCBank.Models;
using KGCBank.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Security.Cryptography.Xml;
using KGCBank.Common;

namespace KGCBank.Controllers
{
    public class UserController : Controller
    {
        UserRepo Obj_UserRepo = new UserRepo();
        SqlConnection Obj_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand Obj_Command;
        SqlDataAdapter Obj_DataAdapter;
        DataTable Obj_DataTable;
        Password EncryptData = new Password();

        /// <summary>
        /// This function is used to list account details
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public ActionResult AccountList(string Username)
        {
            try
            {
                Username = Session["Username"].ToString();
                var data = Obj_UserRepo.GetAccount(Username);
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return View();
            }
            
        }

        /// <summary>
        /// This function is used to show balance
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public ActionResult BalanceList(string Username)
        {
            try
            {
                Username = Session["Username"].ToString();
                if (Obj_UserRepo.ApprovedAccount(Username))
                {
                    var data = Obj_UserRepo.GetBalance(Username);
                    return View(data);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return View();
            }
        }
        /// <summary>
        /// This function is used to index
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        /// <summary>
        /// This function is used to list profile
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public ActionResult ProfileList(string Username)
        {
            try
            {
                Username = Session["Username"].ToString();
                var data = Obj_UserRepo.GetUser(Username);
                return View(data);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return RedirectToAction("Index","Login");
            }
        }

        /// <summary>
        /// This function is used to create a bank account by customer.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAccount()
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
        /// This function is used to create a bank account by customer.
        /// </summary>
        /// <param name="Obj_Account"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddAccount(AccountModel Obj_Account, string Username, HttpPostedFileBase file, string Imagepath)
        {
            try
            {
                if (file != null)
                {
                    Imagepath = Path.Combine(Server.MapPath("~/Images"), Path.GetFileName(file.FileName));
                    file.SaveAs(Imagepath);
                    Username = Session["Username"].ToString();
                    if (Obj_UserRepo.InsertAccount(Obj_Account, Username, Imagepath))
                    {
                        TempData["InsertMsg"] = "<script>alert('Account created')</script>";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["InsertErrorMsg"] = "<script>alert('Account not created.')</script>";
                    }
                    return View();
                }
                return View("AddAccount");
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
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            try
            {
                string Username = Session["Username"].ToString();
                var data = Obj_UserRepo.GetUser(Username).Find(a => a.Id == id);
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
                if (Obj_UserRepo.UpdateUser(Obj_Register))
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
        /// This is a function for deposit
        /// </summary>
        /// <returns></returns>
        public ActionResult Deposit(string Username)
        {
            try
            {
                Username = Session["Username"].ToString();
                if (Obj_UserRepo.ApprovedAccount(Username))
                {
                    var Data = Obj_UserRepo.GetAccountNumber(Username);
                    return View(Data);
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
        /// This is a function for deposit
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Deposit(string AccountNumber, string Username, string Password, float Amount)
        {
            try
            {
                int Balance;
                Username = Session["Username"].ToString();
                string Obj_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection Obj_Connection = new SqlConnection(Obj_ConnectionString);
                Obj_Connection.Open();

                //Check Valid Account Details
                SqlDataAdapter Obj_DataAdapter = new SqlDataAdapter("Select * From Register C Inner Join Account A on C.Id = A.RegId Where A.AccNumber = " 
                    + $" '{AccountNumber}' and C.Username = '{Username}' and C.Password = '{EncryptData.Encode(Password)}'", Obj_Connection);
                DataSet Obj_DataSet = new DataSet();
                Obj_DataAdapter.Fill(Obj_DataSet);

                if (Obj_DataSet != null && Obj_DataSet.Tables[0].Rows.Count != 0)
                {
                    //Balance check
                    SqlCommand Obj_Command = new SqlCommand("Select Balance from Account A Inner Join Register C on C.Id = A.RegId " +
                        "Where C.Username = @username", Obj_Connection);
                    Obj_Command.Parameters.AddWithValue("@username", Username);
                    SqlDataReader Obj_DataReader = Obj_Command.ExecuteReader();
                    if (Obj_DataReader.Read())
                    {
                        Balance = Convert.ToInt32(Obj_DataReader["Balance"]);

                        Obj_DataReader.Close();

                        SqlCommand Obj_Command2 = new SqlCommand("sp_deposit", Obj_Connection);
                        Obj_Command2.CommandType = CommandType.StoredProcedure;
                        Obj_Command2.Parameters.AddWithValue("@Username", Session["Username"]);
                        Obj_Command2.Parameters.AddWithValue("@AccNumber", AccountNumber);
                        Obj_Command2.Parameters.AddWithValue("@Password", EncryptData.Encode(Password));
                        Obj_Command2.Parameters.AddWithValue("@Amount", Amount);
                        Obj_Command2.Parameters.Add("@text", SqlDbType.Char, 500);
                        Obj_Command2.Parameters["@text"].Direction = ParameterDirection.Output;

                        Obj_Command2.ExecuteNonQuery();
                        //ViewBag.text = (string)Cmd.Parameters["@text"].Value; //first check whether it is null or not then use this line

                        if (ViewBag.text == null)
                        {
                            SqlCommand Obj_Command3 = new SqlCommand("sp_add_transaction_credit", Obj_Connection);
                            Obj_Command3.CommandType = CommandType.StoredProcedure;
                            Obj_Command3.Parameters.AddWithValue("@Username", Session["Username"]);
                            Obj_Command3.Parameters.AddWithValue("@AccNumber", AccountNumber);
                            Obj_Command3.Parameters.AddWithValue("@Amount", Amount);

                            Obj_Command3.ExecuteNonQuery();

                            ViewBag.text = "Transaction Successful.";
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

        /// <summary>
        /// This function is used to withrawal
        /// </summary>
        /// <returns></returns>
        public ActionResult Withdrawal(string Username)
        {
            try
            {
                Username = Session["Username"].ToString();
                if (Obj_UserRepo.ApprovedAccount(Username))
                {
                    var Data = Obj_UserRepo.GetAccountNumber(Username);
                    return View(Data);
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
        /// This function is used to withrawal
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <param name="Amount"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Withdrawal(string AccountNumber, string Username, string Password, float Amount)
        {
            try
            {
                int Balance;
                Username = Session["Username"].ToString();
                string Obj_ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection Obj_Connection = new SqlConnection(Obj_ConnectionString);
                Obj_Connection.Open();

                //Check Valid Account Details
                SqlDataAdapter Obj_DataAdapter = new SqlDataAdapter("Select * From Register C Inner Join Account A on C.Id = A.RegId  Where A.AccNumber = " 
                    + $" '{AccountNumber}' and C.Username = '{Username}' and C.Password = '{EncryptData.Encode(Password)}'", Obj_Connection);
                DataSet Obj_DataSet = new DataSet();
                Obj_DataAdapter.Fill(Obj_DataSet);

                if (Obj_DataSet != null && Obj_DataSet.Tables[0].Rows.Count != 0)
                {
                    //Balance check
                    SqlCommand Obj_Command = new SqlCommand("Select Balance from Account A Inner Join Register C on C.Id = A.RegId " +
                        "Where C.Username = @username", Obj_Connection);
                    Obj_Command.Parameters.AddWithValue("@username", Username);
                    SqlDataReader Obj_DataReader = Obj_Command.ExecuteReader();
                    if (Obj_DataReader.Read())
                    {
                        Balance = Convert.ToInt32(Obj_DataReader["Balance"]);
                        if (Balance >= Amount)
                        {
                            Obj_DataReader.Close();
                            SqlCommand Obj_Command2 = new SqlCommand("sp_withdrawal", Obj_Connection);
                            Obj_Command2.CommandType = CommandType.StoredProcedure;
                            Obj_Command2.Parameters.AddWithValue("@Username", Session["Username"]);
                            Obj_Command2.Parameters.AddWithValue("@AccNumber", AccountNumber);
                            Obj_Command2.Parameters.AddWithValue("@Password", EncryptData.Encode(Password));
                            Obj_Command2.Parameters.AddWithValue("@Amount", Amount);
                            Obj_Command2.Parameters.Add("@text", SqlDbType.Char, 500);
                            Obj_Command2.Parameters["@text"].Direction = ParameterDirection.Output;

                            Obj_Command2.ExecuteNonQuery();
                            //ViewBag.text = (string)Cmd.Parameters["@text"].Value; //first check whether it is null or not then use this line

                            if (ViewBag.text == null)
                            {
                                SqlCommand Obj_Command3 = new SqlCommand("sp_add_transaction_debit", Obj_Connection);
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

        /// <summary>
        /// This function is used to transfer money
        /// </summary>
        /// <returns></returns>
        public ActionResult Transfer(string Username)
        {
            try
            {
                Username = Session["Username"].ToString();
                if (Obj_UserRepo.ApprovedAccount(Username))
                {
                    return View();
                }
                else
                {
                    return null;
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

        /// <summary>
        /// Transaction Statement
        /// </summary>
        /// <returns></returns>
        public ActionResult Statement()
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
        public ActionResult Statement(Statement Obj_Statement)
        {
            try
            {
                if (Obj_Statement.StartDate >= Convert.ToDateTime("1/1/0002 00:00:00 AM") && Obj_Statement.EndDate <= DateTime.Now)
                {
                    Obj_Statement.StatementList = TransactionRecord(Obj_Statement.StartDate, Obj_Statement.EndDate);
                }
                else
                {
                    ViewData["wrongdates"] = "Please Enter valid Dates !";
                    return View();
                }

                return View(Obj_Statement);
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        /// <summary>
        /// Transaction Record
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndDate"></param>
        /// <returns></returns>
        public List<Statemomentum> TransactionRecord(DateTime StartDate, DateTime EndDate)
        {
            try
            {
                SqlConnection Obj_Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings
                ["DefaultConnection"].ConnectionString);
                Obj_Connection.Open();

                SqlDataAdapter Obj_DataAdapter = new SqlDataAdapter("Select T.TranDate, T.TranType, T.Amount From Transactions T Inner Join " +
                    "Account A On T.AccId = A.Id Inner Join Register C On A.RegId = C.Id Where C.Username = " + $" '{Session["Username"]}' " +
                    $"and T.TranDate Between '{StartDate}' And '{EndDate}'", Obj_Connection);
                DataTable Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);

                List<Statemomentum> Obj_Statement = new List<Statemomentum>();
                for (int i = 0; i < Obj_DataTable.Rows.Count; i++)
                {
                    Statemomentum Obj_Statemomentum = new Statemomentum();
                    Obj_Statemomentum.TranDate = Convert.ToDateTime(Obj_DataTable.Rows[i]["TranDate"]);
                    Obj_Statemomentum.TranType = Obj_DataTable.Rows[i]["TranType"].ToString();
                    Obj_Statemomentum.Amount = Convert.ToInt32(Obj_DataTable.Rows[i]["Amount"]);
                    Obj_Statement.Add(Obj_Statemomentum);
                }
                Obj_Connection.Close();

                return Obj_Statement;
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}
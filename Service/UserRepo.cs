using KGCBank.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Security.Cryptography.Xml;
using KGCBank.Common;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace KGCBank.Service
{
    public class UserRepo
    {
        SqlConnection Obj_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand Obj_Command;
        SqlDataAdapter Obj_DataAdapter;
        DataTable Obj_DataTable;
        Password EncryptData = new Password();

        /// <summary>
        /// This function is to get account details
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public List<AccountModel> GetAccount(string Username)
        {
            try
            {
                Obj_Command = new SqlCommand("account_list", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@username", Username);
                Obj_Connection.Open();
                Obj_Command.ExecuteNonQuery();

                Obj_DataAdapter = new SqlDataAdapter(Obj_Command);
                Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);
                List<AccountModel> list = new List<AccountModel>();
                foreach (DataRow Obj_DataRow in Obj_DataTable.Rows)
                {
                    list.Add(new AccountModel
                    {
                        Id = Convert.ToInt32(Obj_DataRow["Id"]),
                        AccNumber = Obj_DataRow["AccNumber"].ToString(),
                        AccType = Obj_DataRow["AccType"].ToString(),
                        RegDate = Obj_DataRow["Reg_Date"].ToString(),
                        Balance = Obj_DataRow["Balance"].ToString(),
                        BranchId = Obj_DataRow["BranchId"].ToString()
                    });
                }
                Obj_Connection.Close();
                return list;
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }
        /// <summary>
        /// This function is to get account number
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public List<transfer> GetAccountNumber(string Username)
        {
            try
            {
                Obj_Command = new SqlCommand("account_list", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@username", Username);
                Obj_Connection.Open();
                Obj_Command.ExecuteNonQuery();

                Obj_DataAdapter = new SqlDataAdapter(Obj_Command);
                Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);
                List<transfer> list = new List<transfer>();
                foreach (DataRow Obj_DataRow in Obj_DataTable.Rows)
                {
                    list.Add(new transfer
                    {
                        Username = Obj_DataRow["Username"].ToString(),
                        AccountNumber = Obj_DataRow["AccNumber"].ToString()
                    });
                }
                Obj_Connection.Close();
                return list;
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// This function is to get user balance
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public List<AccountModel> GetBalance(string Username)
        {
            try
            {
                Obj_Command = new SqlCommand("balance_check", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@username", Username);
                Obj_Connection.Open();
                Obj_Command.ExecuteNonQuery();

                Obj_DataAdapter = new SqlDataAdapter(Obj_Command);
                Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);
                List<AccountModel> list = new List<AccountModel>();
                foreach (DataRow Obj_DataRow in Obj_DataTable.Rows)
                {
                    list.Add(new AccountModel
                    {
                        Balance = Obj_DataRow["Balance"].ToString(),
                    });
                }
                return list;
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }

        }

        /// <summary>
        /// This function is to get user details
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public List<RegisterModel> GetUser(string Username)
        {
            try
            {
                Obj_Command = new SqlCommand("sp_select_usr", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@username", Username);
                Obj_Connection.Open();
                Obj_Command.ExecuteNonQuery();

                Obj_DataAdapter = new SqlDataAdapter(Obj_Command);
                Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);
                List<RegisterModel> list = new List<RegisterModel>();
                foreach (DataRow Obj_DataRow in Obj_DataTable.Rows)
                {
                    list.Add(new RegisterModel
                    {
                        Id = Convert.ToInt32(Obj_DataRow["Id"]),
                        Firstname = Obj_DataRow["Firstname"].ToString(),
                        Lastname = Obj_DataRow["Lastname"].ToString(),
                        Dateofbirth = Obj_DataRow["Dateofbirth"].ToString(),
                        Gender = Obj_DataRow["Gender"].ToString(),
                        Phonenumber = Obj_DataRow["Phonenumber"].ToString(),
                        Email = Obj_DataRow["Email"].ToString(),
                        Address = Obj_DataRow["Address"].ToString(),
                        City = Obj_DataRow["City"].ToString(),
                        State = Obj_DataRow["State"].ToString(),
                        Pincode = Obj_DataRow["Pincode"].ToString(),
                        Username = Obj_DataRow["Username"].ToString(),
                        Password = Obj_DataRow["Password"].ToString()
                    });
                }
                return list;
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return null;
            }
        }

        /// <summary>
        /// This function is to insert user account details
        /// </summary>
        /// <param name="Obj_Account"></param>
        /// <param name="Username"></param>
        /// <returns></returns>
        public bool InsertAccount(AccountModel Obj_Account, string Username, string Imagepath)
        {
            int Read;
            try
            {
                Obj_Command = new SqlCommand("add_account", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Connection.Open();
                Obj_Command.Parameters.AddWithValue("@accType", Obj_Account.AccType);
                Obj_Command.Parameters.AddWithValue("@branchId", Obj_Account.BranchId);
                Obj_Command.Parameters.AddWithValue("@profilepic", Imagepath);
                Obj_Command.Parameters.AddWithValue("@username", Username);

                Read = Obj_Command.ExecuteNonQuery();
                if (Read > 0)
                {
                    Obj_Connection.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return false;
            }
        }

        /// <summary>
        /// This function is to update user account details
        /// </summary>
        /// <param name="Obj_Register"></param>
        /// <returns></returns>
        public bool UpdateUser(RegisterModel Obj_Register)
        {
            int Read;
            try
            {
                Obj_Command = new SqlCommand("sp_update", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@Firstname", Obj_Register.Firstname);
                Obj_Command.Parameters.AddWithValue("@Lastname", Obj_Register.Lastname);
                Obj_Command.Parameters.AddWithValue("@Dateofbirth", Obj_Register.Dateofbirth);
                Obj_Command.Parameters.AddWithValue("@Gender", Obj_Register.Gender);
                Obj_Command.Parameters.AddWithValue("@Phonenumber", Obj_Register.Phonenumber);
                Obj_Command.Parameters.AddWithValue("@Email", Obj_Register.Email);
                Obj_Command.Parameters.AddWithValue("@Address", Obj_Register.Address);
                Obj_Command.Parameters.AddWithValue("@City", Obj_Register.City);
                Obj_Command.Parameters.AddWithValue("@State", Obj_Register.State);
                Obj_Command.Parameters.AddWithValue("@Pincode", Obj_Register.Pincode);
                Obj_Command.Parameters.AddWithValue("@Username", Obj_Register.Username);
                Obj_Command.Parameters.AddWithValue("@Password", EncryptData.Encode(Obj_Register.Password));
                Obj_Command.Parameters.AddWithValue("@Id", Obj_Register.Id);
                Obj_Connection.Open();
                Read = Obj_Command.ExecuteNonQuery();
                if (Read > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return false;
            }
        }

        /// <summary>
        /// Approved account
        /// </summary>
        /// <param name="Obj_Register"></param>
        /// <returns></returns>
        public bool ApprovedAccount(string Username)
        {
            try
            {
                Obj_Command = new SqlCommand("sp_approved_account", Obj_Connection);
                Obj_Connection.Open();
                Obj_Command.Connection = Obj_Connection;
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@Username", Username);
                var roleParam = new SqlParameter("@Status", SqlDbType.VarChar, 50);
                roleParam.Direction = ParameterDirection.Output;
                Obj_Command.Parameters.Add(roleParam);
                Obj_Command.ExecuteNonQuery();
                var Status = roleParam.Value.ToString();
                Obj_Connection.Close();
                if (Status == "Approved")
                {
                    return true;                    
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return false;
            }
        }
    }
}
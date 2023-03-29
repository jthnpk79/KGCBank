using KGCBank.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Security.Cryptography.Xml;
using KGCBank.Common;
using System.Web.Mvc;

namespace KGCBank.Service
{
    public class AdminRepo
    {
        SqlConnection Obj_Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand Obj_Command;
        SqlDataAdapter Obj_DataAdapter;
        DataTable Obj_DataTable;
        Password EncryptData = new Password();

        /// <summary>
        /// Get bank details
        /// </summary>
        /// <returns></returns>
        public List<BankModel> GetBank()
        {
            try
            {
                Obj_Command = new SqlCommand("bank_list", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_DataAdapter = new SqlDataAdapter(Obj_Command);
                Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);
                List<BankModel> list = new List<BankModel>();
                foreach (DataRow Obj_DataRow in Obj_DataTable.Rows)
                {
                    list.Add(new BankModel
                    {
                        Id = Convert.ToInt32(Obj_DataRow["Id"]),
                        Name = Obj_DataRow["Name"].ToString(),
                        Description = Obj_DataRow["Description"].ToString(),
                        IFSC = Obj_DataRow["IFSC"].ToString(),
                        Phone = Obj_DataRow["Phone"].ToString(),
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
        /// Get user details
        /// </summary>
        /// <returns></returns>
        public List<LoginModel> GetUsers()
        {
            try
            {
                Obj_Command = new SqlCommand("userlogin_list", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_DataAdapter = new SqlDataAdapter(Obj_Command);
                Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);
                List<LoginModel> list = new List<LoginModel>();
                foreach (DataRow Obj_DataRow in Obj_DataTable.Rows)
                {
                    list.Add(new LoginModel
                    {
                        Id = Convert.ToInt32(Obj_DataRow["Id"]),
                        Username = Obj_DataRow["Username"].ToString(),
                        Password = Obj_DataRow["Password"].ToString(),
                        Role = Obj_DataRow["Role"].ToString(),
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
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AccountModel> GetAccounts()
        {
            try
            {
                Obj_Command = new SqlCommand("sp_account_list", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
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
                        Status = Obj_DataRow["Status"].ToString(),
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

        public List<LoginModel> GetAdmin()
        {
            try
            {
                Obj_Command = new SqlCommand("sp_admin_list", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_DataAdapter = new SqlDataAdapter(Obj_Command);
                Obj_DataTable = new DataTable();
                Obj_DataAdapter.Fill(Obj_DataTable);
                List<LoginModel> list = new List<LoginModel>();
                foreach (DataRow Obj_DataRow in Obj_DataTable.Rows)
                {
                    list.Add(new LoginModel
                    {
                        Id = Convert.ToInt32(Obj_DataRow["Id"]),
                        Username = Obj_DataRow["Username"].ToString(),
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
        /// Insert bank details and new branch
        /// </summary>
        /// <param name="Obj_Bank"></param>
        /// <returns></returns>
        public bool InsertBank(BankModel Obj_Bank)
        {
            int Read;
            try
            {
                Obj_Command = new SqlCommand("add_bankdetails", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@Name", Obj_Bank.Name);
                Obj_Command.Parameters.AddWithValue("@Description", Obj_Bank.Description);
                Obj_Command.Parameters.AddWithValue("@IFSC", Obj_Bank.IFSC);
                Obj_Command.Parameters.AddWithValue("@Phone", Obj_Bank.Phone);

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
        /// Insert admin details
        /// </summary>
        /// <param name="Obj_Login"></param>
        /// <returns></returns>
        public bool InsertAdmin(LoginModel Obj_Login)
        {
            int Read;
            try
            {
                Obj_Command = new SqlCommand("sp_insert_admin", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@Username", Obj_Login.Username);
                Obj_Command.Parameters.AddWithValue("@Password", EncryptData.Encode(Obj_Login.Password));

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
        /// Update admin password
        /// </summary>
        /// <param name="Obj_ChangePassword"></param>
        /// <returns></returns>
        public bool UpdatePassword(ChangePasswordModel Obj_ChangePassword)
        {
            try
            {
                Obj_Connection.Open();
                SqlCommand Obj_Command = new SqlCommand("sp_change_password", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@Username", Obj_ChangePassword.Username);
                Obj_Command.Parameters.AddWithValue("@OldPassword", EncryptData.Encode(Obj_ChangePassword.OldPassword));
                Obj_Command.Parameters.AddWithValue("@NewPassword", EncryptData.Encode(Obj_ChangePassword.NewPassword));
                var result = Obj_Command.ExecuteScalar();
                if (result != null && result.ToString() == "Password updated successfully.")
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
        /// 
        /// </summary>
        /// <param name="Obj_Account"></param>
        /// <returns></returns>
        public bool UpdateAccount(AccountModel Obj_Account)
        {
            int Read;
            try
            {
                Obj_Command = new SqlCommand("verify_account", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@AccNumber", Obj_Account.AccNumber);
                Obj_Command.Parameters.AddWithValue("@AccType", Obj_Account.AccType);
                Obj_Command.Parameters.AddWithValue("@Status", Obj_Account.Status);
                Obj_Command.Parameters.AddWithValue("@Id", Obj_Account.Id);
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
        /// delete account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAccount(int id)
        {
            try
            {
                Obj_Command = new SqlCommand("sp_delete_account", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@Id", id);
                Obj_Connection.Open();
                return Obj_Command.ExecuteNonQuery();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return 0;
            }
        }

        public int DeleteAdmin(int id)
        {
            try
            {
                Obj_Command = new SqlCommand("sp_delete_admin", Obj_Connection);
                Obj_Command.CommandType = CommandType.StoredProcedure;
                Obj_Command.Parameters.AddWithValue("@Id", id);
                Obj_Connection.Open();
                return Obj_Command.ExecuteNonQuery();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return 0;
            }
        }
    }
}
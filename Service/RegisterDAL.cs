using KGCBank.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using KGCBank.Common;

namespace KGCBank.Service
{
    public class RegisterDAL
    {
        SqlConnection obj_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        SqlCommand obj_command;
        SqlCommand Duplicate;
        SqlDataAdapter obj_dataadapter;
        DataTable obj_datatable;
        Password EncryptData = new Password();
        /// <summary>
        /// Calling function for List
        /// </summary>
        /// <returns></returns>
        public List<RegisterModel> GetUsers()
        {
            try
            {
                obj_command = new SqlCommand("sp_select", obj_connection);
                obj_command.CommandType = CommandType.StoredProcedure;
                obj_dataadapter = new SqlDataAdapter(obj_command);
                obj_datatable = new DataTable();
                obj_dataadapter.Fill(obj_datatable);
                List<RegisterModel> list = new List<RegisterModel>();
                foreach (DataRow dr in obj_datatable.Rows)
                {
                    list.Add(new RegisterModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Firstname = dr["Firstname"].ToString(),
                        Lastname = dr["Lastname"].ToString(),
                        Dateofbirth = dr["Dateofbirth"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Phonenumber = dr["Phonenumber"].ToString(),
                        Email = dr["Email"].ToString(),
                        Address = dr["Address"].ToString(),
                        City = dr["City"].ToString(),
                        State = dr["State"].ToString(),
                        Pincode = dr["Pincode"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString()

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
        /// Calling function for create user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool InsertUser(RegisterModel Obj_Register)
        {
            int Read;
            try
            {
                Duplicate = new SqlCommand("sp_duplicate_user", obj_connection);
                Duplicate.CommandType = CommandType.StoredProcedure;
                Duplicate.Parameters.AddWithValue("@Email", Obj_Register.Email);
                Duplicate.Parameters.AddWithValue("@Username", Obj_Register.Username);
                obj_connection.Open();
                int count = (int)Duplicate.ExecuteScalar();
                obj_connection.Close();
                if (count > 0)
                {
                    return false;
                }
                obj_command = new SqlCommand("sp_insert", obj_connection);
                obj_command.CommandType = CommandType.StoredProcedure;
                obj_command.Parameters.AddWithValue("@Firstname", Obj_Register.Firstname);
                obj_command.Parameters.AddWithValue("@Lastname", Obj_Register.Lastname);
                obj_command.Parameters.AddWithValue("@Dateofbirth", Obj_Register.Dateofbirth);
                obj_command.Parameters.AddWithValue("@Gender", Obj_Register.Gender);
                obj_command.Parameters.AddWithValue("@Phonenumber", Obj_Register.Phonenumber);
                obj_command.Parameters.AddWithValue("@Email", Obj_Register.Email);
                obj_command.Parameters.AddWithValue("@Address", Obj_Register.Address);
                obj_command.Parameters.AddWithValue("@City", Obj_Register.City);
                obj_command.Parameters.AddWithValue("@State", Obj_Register.State);
                obj_command.Parameters.AddWithValue("@Pincode", Obj_Register.Pincode);
                obj_command.Parameters.AddWithValue("@Username", Obj_Register.Username);
                obj_command.Parameters.AddWithValue("@Password", EncryptData.Encode(Obj_Register.Password));

                obj_connection.Open();
                Read = obj_command.ExecuteNonQuery();
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
        /// Function for update user
        /// </summary>
        /// <param name="Obj_Register"></param>
        /// <returns></returns>
        public bool UpdateUser(RegisterModel Obj_Register)
        {
            int Read;
            try
            {
                obj_command = new SqlCommand("sp_update", obj_connection);
                obj_command.CommandType = CommandType.StoredProcedure;
                obj_command.Parameters.AddWithValue("@Firstname", Obj_Register.Firstname);
                obj_command.Parameters.AddWithValue("@Lastname", Obj_Register.Lastname);
                obj_command.Parameters.AddWithValue("@Dateofbirth", Obj_Register.Dateofbirth);
                obj_command.Parameters.AddWithValue("@Gender", Obj_Register.Gender);
                obj_command.Parameters.AddWithValue("@Phonenumber", Obj_Register.Phonenumber);
                obj_command.Parameters.AddWithValue("@Email", Obj_Register.Email);
                obj_command.Parameters.AddWithValue("@Address", Obj_Register.Address);
                obj_command.Parameters.AddWithValue("@City", Obj_Register.City);
                obj_command.Parameters.AddWithValue("@State", Obj_Register.State);
                obj_command.Parameters.AddWithValue("@Pincode", Obj_Register.Pincode);
                obj_command.Parameters.AddWithValue("@Username", Obj_Register.Username);
                obj_command.Parameters.AddWithValue("@Password", EncryptData.Encode(Obj_Register.Password));
                obj_command.Parameters.AddWithValue("@Id", Obj_Register.Id);
                obj_connection.Open();
                Read = obj_command.ExecuteNonQuery();
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
        /// Function for delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUser(int id)
        {
            try
            {
                obj_command = new SqlCommand("sp_delete", obj_connection);
                obj_command.CommandType = CommandType.StoredProcedure;
                obj_command.Parameters.AddWithValue("@Id", id);
                obj_connection.Open();
                return obj_command.ExecuteNonQuery();
            }
            catch (Exception Obj_Exception)
            {
                ErrorLogger.Log(Obj_Exception.Message);
                return 0;
            }
        }
        public bool InsertContact(ContactModel Obj_Contact)
        {
            try
            {
                obj_command = new SqlCommand("sp_insert_contact", obj_connection);
                obj_command.CommandType = CommandType.StoredProcedure;
                obj_command.Parameters.AddWithValue("@Name", Obj_Contact.Name);
                obj_command.Parameters.AddWithValue("@Email", Obj_Contact.Email);
                obj_command.Parameters.AddWithValue("@Message", Obj_Contact.Message);
                
                obj_connection.Open();
                int r = obj_command.ExecuteNonQuery();
                if (r > 0)
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
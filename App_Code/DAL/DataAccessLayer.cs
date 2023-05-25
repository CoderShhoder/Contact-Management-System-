using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Runtime.Remoting.Messaging;
using Assessment;

namespace Assessment
{
    public class DataAccessLayer
    {
        string cs = ConfigurationManager.ConnectionStrings["assessmentDBConnectionString"].ConnectionString;

        public List<ObjContact> AllContacts()
        {
            List<ObjContact> contacts = new List<ObjContact>();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ContactDataDetails", connection);
                
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TType", "SELECT");

                cmd.Parameters.AddWithValue("@contactID", "");
                cmd.Parameters.AddWithValue("@companyName", "");
                cmd.Parameters.AddWithValue("@firstName", "");
                cmd.Parameters.AddWithValue("@lastName", "");
                cmd.Parameters.AddWithValue("@email", "");
                cmd.Parameters.AddWithValue("@category", "");
                cmd.Parameters.AddWithValue("@professionID", "");
                cmd.Parameters.AddWithValue("@isActive", "");
                cmd.Parameters.AddWithValue("@createdON", "");
                SqlDataReader read= cmd.ExecuteReader();
                while (read.Read()) 
                {
                    ObjContact contactAll = new ObjContact();
                    contactAll.ContactID = Convert.ToInt32(read["contact_id"]);
                    contactAll.CompanyName = read["company_name"].ToString();
                    contactAll.FirstName = read["first_name"].ToString();
                    contactAll.LastName = read["last_name"].ToString();
                    contactAll.Email = read["email"].ToString();
                    contactAll.Category = read["category"].ToString();
                    contactAll.ProfessionID = Convert.ToInt32(read["profession_id"]);

                    contacts.Add(contactAll);
                }

                read.Close();
            }
            return contacts;
        }
        //Saving the new contact in the database
        public void AddContact(ObjContact contact)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand("sp_ContactDataDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TType", "INSERT");
                command.Parameters.AddWithValue("@contactID", "");
                command.Parameters.AddWithValue("@companyName", contact.CompanyName);
                command.Parameters.AddWithValue("@firstName", contact.FirstName);
                command.Parameters.AddWithValue("@lastName", contact.LastName);
                command.Parameters.AddWithValue("@email", contact.Email);
                command.Parameters.AddWithValue("@category", contact.Category);
                command.Parameters.AddWithValue("@professionID", contact.ProfessionID);
                command.Parameters.AddWithValue("@isActive","" );
                command.Parameters.AddWithValue("@createdON", "");
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        //For Updating the Value in the Database
        public void UpdateContact(ObjContact contact)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {

                SqlCommand command = new SqlCommand("sp_ContactDataDetails", connection);
                command.CommandType= CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TType", "UPDATE");
                command.Parameters.AddWithValue("@companyName", contact.CompanyName);
                command.Parameters.AddWithValue("@firstName", contact.FirstName);
                command.Parameters.AddWithValue("@lastName", contact.LastName);
                command.Parameters.AddWithValue("@email", contact.Email);
                command.Parameters.AddWithValue("@category", contact.Category);
                command.Parameters.AddWithValue("@professionID", contact.ProfessionID);
                command.Parameters.AddWithValue("@contactID", contact.ContactID);
                command.Parameters.AddWithValue("@isActive", "");
                command.Parameters.AddWithValue("@createdON", "");
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        //For Deleting the Values From the Database
        public void DeleteContact(int contactID)
        {
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand command = new SqlCommand("sp_ContactDataDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TType", "DELETE");
                command.Parameters.AddWithValue("@contactID", contactID);
                command.Parameters.AddWithValue("@companyName", "");
                command.Parameters.AddWithValue("@firstName", "");
                command.Parameters.AddWithValue("@lastName", "");
                command.Parameters.AddWithValue("@email", "");
                command.Parameters.AddWithValue("@category",     "");
                command.Parameters.AddWithValue("@professionID", "");
   
                command.Parameters.AddWithValue("@isActive", "");
                command.Parameters.AddWithValue("@createdON", "");
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public ObjContact GetContactByID(int contactID)
        {
            ObjContact contact = null;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbl_Contacts WHERE contact_ID = @ContactID", con);
                cmd.Parameters.AddWithValue("@ContactID", contactID);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    contact = new ObjContact()
                    {
                        ContactID = Convert.ToInt32(reader["contact_ID"]),
                        CompanyName = reader["company_name"].ToString(),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Category = reader["category"].ToString(),
                        ProfessionID = Convert.ToInt32(reader["profession_id"])
                    };
                }

                con.Close();
            }

            return contact;
        }
    }
}
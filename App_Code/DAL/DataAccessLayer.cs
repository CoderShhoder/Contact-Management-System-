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
                cmd.Parameters.AddWithValue("@name", "");
                cmd.Parameters.AddWithValue("@phone", "");
                cmd.Parameters.AddWithValue("@email", "");
                cmd.Parameters.AddWithValue("@source", "");
                cmd.Parameters.AddWithValue("@Address", "");
                cmd.Parameters.AddWithValue("@profession", "");
                cmd.Parameters.AddWithValue("@status", "");
                cmd.Parameters.AddWithValue("@isActive", "");
                cmd.Parameters.AddWithValue("@createdON", "");
                SqlDataReader read= cmd.ExecuteReader();
                while (read.Read()) 
                {
                    ObjContact contactAll = new ObjContact();
                    contactAll.ContactID = Convert.ToInt32(read["Contact ID"]);
                    contactAll.CompanyName = read["Company"].ToString();
                    contactAll.Name = read["Name"].ToString();
                    contactAll.Phone = read["Phone"].ToString();
                    contactAll.Email = read["Email"].ToString();
                    contactAll.Source = read["Source"].ToString();
                    contactAll.Address = read["Address"].ToString();
                    contactAll.Profession = read["Profession"].ToString();
                    contactAll.Status = read["Status"].ToString();

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
                command.Parameters.AddWithValue("@name", contact.Name);
                command.Parameters.AddWithValue("@Phone", contact.Phone);
                command.Parameters.AddWithValue("@email", contact.Email);
                command.Parameters.AddWithValue("@source", contact.Source);
                command.Parameters.AddWithValue("@Address", contact.Address);
                command.Parameters.AddWithValue("@profession", contact.Profession);
                command.Parameters.AddWithValue("@status",contact.Status );
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
                command.Parameters.AddWithValue("@contactID", "");
                command.Parameters.AddWithValue("@companyName", contact.CompanyName);
                command.Parameters.AddWithValue("@name", contact.Name);
                command.Parameters.AddWithValue("@Phone", contact.Phone);
                command.Parameters.AddWithValue("@email", contact.Email);
                command.Parameters.AddWithValue("@source", contact.Source);
                command.Parameters.AddWithValue("@Address", contact.Address);
                command.Parameters.AddWithValue("@profession", contact.Profession);
                command.Parameters.AddWithValue("@status", contact.Status);
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
                command.Parameters.AddWithValue("@name", "");
                command.Parameters.AddWithValue("@Phone", "");
                command.Parameters.AddWithValue("@email", "");
                command.Parameters.AddWithValue("@source", "");
                command.Parameters.AddWithValue("@Address", "");
                command.Parameters.AddWithValue("@profession",   "");
                command.Parameters.AddWithValue("@status", "");
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
                SqlCommand cmd = new SqlCommand("SELECT * FROM tbluContact WHERE [Contact ID] = @ContactID", con);
                cmd.Parameters.AddWithValue("@contactID", contactID);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    contact = new ObjContact()
                    {
                        ContactID = Convert.ToInt32(reader["contact_ID"]),
                        CompanyName = reader["company_name"].ToString(),
                        Name = reader["Name"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Email = reader["Email"].ToString(),
                        Source = reader["Source"].ToString(),
                        Address = reader["Address"].ToString(),
                        Profession = reader["Profession"].ToString(),
                        Status = reader["status"].ToString(),
                    };
                }

                con.Close();
            }

            return contact;
        }
    }
}
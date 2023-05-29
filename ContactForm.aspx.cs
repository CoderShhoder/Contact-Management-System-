using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assessment;

namespace Assessment
{
    public partial class ContactForm : System.Web.UI.Page
    {
        private DataAccessLayer contactDAL = new DataAccessLayer();
        private int selectedContactID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContacts();
                professionDropdown();
                AddressDropdown();
                SourceDropdown();
                StatusDropdown();
            }
        }

        //To populate The GridView from the Database
        protected void BindContacts()
        {
            List<ObjContact> contacts = contactDAL.AllContacts();
            GridView1.DataSource = contacts;
            GridView1.DataBind();
        }
        //Function for bounding the Profession Value to the Dropdown
        protected void professionDropdown()
        {
            string st = ConfigurationManager.ConnectionStrings["assessmentDBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(st))
            {
                SqlCommand sql = new SqlCommand("sp_PopulateProfessionDropDown", con);

                con.Open();
                sql.CommandType = CommandType.StoredProcedure;

                ddlProfession.DataSource = sql.ExecuteReader();
                ddlProfession.DataTextField = "profession_name";
                ddlProfession.DataValueField = "profession_ID";
                ddlProfession.DataBind();
                ddlProfession.Items.Insert(0, "--Select Profession--");
            }
        }

        protected void AddressDropdown()
        {
            string st = ConfigurationManager.ConnectionStrings["assessmentDBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(st))
            {
                SqlCommand sql = new SqlCommand("sp_DropdownPopulation", con);

                con.Open();
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@TType", "ADDRESS");
                ddlAddress.DataSource = sql.ExecuteReader();
                ddlAddress.DataTextField = "Name";
                ddlAddress.DataValueField = "ID";
                ddlAddress.DataBind();
                ddlAddress.Items.Insert(0, "--Select Address--");
            }
        }
        protected void StatusDropdown()
        {
            string st = ConfigurationManager.ConnectionStrings["assessmentDBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(st))
            {
                SqlCommand sql = new SqlCommand("sp_DropdownPopulation", con);

                con.Open();
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@TType", "STATUS");
                ddlStatus.DataSource = sql.ExecuteReader();
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "StatusID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, "--Select Status--");
            }
        }
        protected void SourceDropdown()
        {
            string st = ConfigurationManager.ConnectionStrings["assessmentDBConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(st))
            {
                SqlCommand sql = new SqlCommand("sp_DropdownPopulation", con);

                con.Open();
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@TType", "SOURCE");
                ddlSource.DataSource = sql.ExecuteReader();
                ddlSource.DataTextField = "Source";
                ddlSource.DataValueField = "SourceID";
                ddlSource.DataBind();
                ddlSource.Items.Insert(0, "--Select Profession--");
            }
        }


        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditContact")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                selectedContactID = int.Parse(GridView1.Rows[rowIndex].Cells[1].Text.ToString());
                ObjContact contact = contactDAL.GetContactByID(selectedContactID);

                if (contact != null)
                {
                    PopulateFormFields(contact);

                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton edit = (ImageButton)e.Row.FindControl("imgBtnEdit");
                edit.CommandArgument = e.Row.RowIndex.ToString();
            }
        }
        //Clear Function ONce Submitted/Updated the Value 
        private void ClearForm()
        {
            txtCompanyName.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlSource.SelectedIndex = 0;
            ddlAddress.SelectedIndex = 0;
            ddlProfession.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
        }
        //Adding a New Contact to the GridView
        protected void btnAddContact_Click(object sender, EventArgs e)
        {
            try
            {
                // Add new contact...
                ObjContact contact = new ObjContact()
                {
                    CompanyName = txtCompanyName.Text,
                    Name = txtName.Text,
                    Phone = txtPhone.Text,
                    Email = txtEmail.Text,
                    Source = ddlSource.SelectedItem.Text.ToString(),
                    Profession = ddlProfession.SelectedItem.Text.ToString(),
                    Address = ddlAddress.SelectedItem.Text.ToString(),
                    Status = ddlStatus.SelectedItem.Text.ToString(),
                };

                contactDAL.AddContact(contact);
                BindContacts();
                ClearForm();
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = "Contact Saved Successfully";
            }

            catch (Exception ex)
            {
                lblError.ForeColor = System.Drawing.Color.Red;
                lblError.Text = ex.Message;
            }
        }

        //Deleting the Form Feilds on the click of the Edit Button in the GridView
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (txtName.Text == GridView1.Rows[i].Cells[3].Text.ToString())
                {
                    contactDAL.DeleteContact(int.Parse(GridView1.Rows[i].Cells[1].Text.ToString()));
                }

            }


            ClearForm();
            BindContacts();

            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "Contact Deleted Successfully";
        }
        //Updating the Form Feilds Value on the click of the Edit Button in the GridView
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                if (txtName.Text == GridView1.Rows[i].Cells[3].Text.ToString())
                {
                    ObjContact contact = new ObjContact()
                    {
                        ContactID = int.Parse(GridView1.Rows[i].Cells[1].Text.ToString()),
                        CompanyName = txtCompanyName.Text,
                        Name = txtName.Text,
                        Phone = txtPhone.Text,
                        Email = txtEmail.Text,
                        Source = ddlSource.SelectedItem.Text.ToString(),
                        Profession = ddlProfession.SelectedItem.Text.ToString(),
                        Address = ddlAddress.SelectedItem.Text.ToString(),
                        Status = ddlStatus.SelectedItem.Text.ToString(),
                    };

                    contactDAL.UpdateContact(contact);
                }
                lblError.ForeColor = System.Drawing.Color.Green;
                lblError.Text = "Contact Updated Successfuly";

            }
            ClearForm();
            BindContacts();
        }

        //Populating the Form Feilds on the click of the Edit Button in the GridView
        private void PopulateFormFields(ObjContact contact)
        {
            txtCompanyName.Text = contact.CompanyName;
            txtName.Text = contact.Name;
            txtPhone.Text = contact.Phone;
            txtEmail.Text = contact.Email;
            ddlSource.SelectedValue = contact.Source;
            ddlProfession.SelectedValue = contact.Profession;
            ddlAddress.SelectedValue = contact.Address;
            ddlStatus.SelectedValue = contact.Status;
        }
    }
}
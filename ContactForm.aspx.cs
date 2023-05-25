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
                
                ddlCategory.Items.Insert(0, "--Select Category--");
                BindContacts();
                professionDropdown();
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
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            ddlCategory.SelectedIndex = 0;
            ddlProfession.SelectedIndex = 0;
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
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Category = ddlCategory.SelectedValue,
                    ProfessionID = int.Parse(ddlProfession.SelectedValue)
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
                if (txtFirstName.Text == GridView1.Rows[i].Cells[3].Text.ToString())
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
                if (txtFirstName.Text == GridView1.Rows[i].Cells[3].Text.ToString())
                {
                    ObjContact contact = new ObjContact()
                    {
                        ContactID = int.Parse(GridView1.Rows[i].Cells[1].Text.ToString()),
                        CompanyName = txtCompanyName.Text,
                        FirstName = txtFirstName.Text,
                        LastName = txtLastName.Text,
                        Email = txtEmail.Text,
                        Category = ddlCategory.SelectedValue,
                        ProfessionID = int.Parse(ddlProfession.SelectedValue)
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
            txtFirstName.Text = contact.FirstName;
            txtLastName.Text = contact.LastName;
            txtEmail.Text = contact.Email;
            ddlCategory.SelectedValue = contact.Category;
            ddlProfession.SelectedValue = contact.ProfessionID.ToString();
        }
    }
}
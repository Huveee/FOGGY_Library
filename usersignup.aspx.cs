using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FOGGY_Library
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


		//when sign up button is clicked
		protected void Button1_Click(object sender, EventArgs e)
		{
			Response.Write("<script>alert('Button1_Click is called');</script>");

			if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox8.Text == "" || TextBox9.Text == "") /////NAME SURNAME AND EMAIL EMPTY CONTROL
			{
				Response.Write("<script>alert('Please fill all the fields');</script>");
				return;
			}

			if (TextBox4.Text.Length != 10) ///PHONE NUMBER LENGTH CONTROL
			{
				Response.Write("<script>alert('Please enter a phone number with 10 digits ');</script>");
				return;
			}

			if (checkMemberExists())
			{
				Response.Write("<script>alert('Member Already Exist with this Member ID, try other ID');</script>");
			}
			else
			{
				Response.Write("<script>alert('Signing up new member');</script>");
				signUpNewMember();
			}
		}


		bool checkMemberExists()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * from CUSTOMER where EMAIL='" + TextBox8.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }
		void signUpNewMember()
		{
			//Response.Write("<script>alert('Testing');</script>");
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("INSERT INTO CUSTOMER(NAME,SURNAME,REGISTRATION_DATE,EMAIL,PHONE_NUMBER,STATE,DATE_OF_BIRTH,PASSWORD) values(@NAME,@SURNAME,@REGISTRATION_DATE,@EMAIL,@PHONE_NUMBER,@STATE,@DATE_OF_BIRTH,@PASSWORD)", con);
				
				cmd.Parameters.AddWithValue("@NAME", TextBox1.Text.Trim().ToUpper());
				cmd.Parameters.AddWithValue("@SURNAME", TextBox2.Text.Trim().ToUpper());
				cmd.Parameters.AddWithValue("@REGISTRATION_DATE", DateTime.Now.Date);
				cmd.Parameters.AddWithValue("@EMAIL", TextBox8.Text.Trim().ToUpper());
				cmd.Parameters.AddWithValue("@PHONE_NUMBER", TextBox4.Text.Trim().ToUpper());
				cmd.Parameters.AddWithValue("@STATE", DropDownList1.SelectedItem.Value);
				cmd.Parameters.AddWithValue("@DATE_OF_BIRTH", TextBox3.Text.Trim());
				cmd.Parameters.AddWithValue("@PASSWORD", TextBox9.Text.Trim().ToUpper());
				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Sign Up Successful. Go to User Login to Login');</script>");
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}
	}
}
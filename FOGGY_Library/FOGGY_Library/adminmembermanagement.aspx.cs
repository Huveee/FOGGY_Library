using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FOGGY_Library
{
    public partial class WebForm8 : System.Web.UI.Page
    {
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }
        // Fill the all information about customer (With email address)
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
			if (TextBox1.Text == "") ////EMAIL EMPTY CONTROL
			{
				Response.Write("<script>alert('Please fill all the fields');</script>");
				return;
			}
			getCustomerByEmail();
		}
        protected void Button2_Click(object sender, EventArgs e)
        {
			if (TextBox1.Text == "") ////EMAIL EMPTY CONTROL
			{
				Response.Write("<script>alert('Please fill all the fields');</script>");
				return;
			}
			// Delete the customer
			deleteCustomerByEmail();
		}

        void getCustomerByEmail()
        {
			if (checkIfCustomerExists())
			{
				try
				{
					SqlConnection con = new SqlConnection(strcon);
					if (con.State == ConnectionState.Closed)
					{
						con.Open();

					}
					SqlCommand cmd = new SqlCommand("select * from CUSTOMER where EMAIL='" + TextBox1.Text.Trim() + "'", con);
					SqlDataReader dr = cmd.ExecuteReader();
					if (dr.HasRows)
					{
						while (dr.Read())
						{
							TextBox2.Text = dr.GetValue(1).ToString();
							TextBox7.Text = dr.GetValue(2).ToString();
							TextBox8.Text = dr.GetValue(3).ToString();
							TextBox3.Text = dr.GetValue(5).ToString();
							TextBox4.Text = dr.GetValue(8).ToString();
							TextBox9.Text = dr.GetValue(6).ToString();
							TextBox10.Text = dr.GetValue(7).ToString();

						}
					}
					else
					{
						Response.Write("<script>alert('Invalid credentials');</script>");
					}

				}
				catch (Exception ex)
				{
					Response.Write("<script>alert('" + ex.Message + "');</script>");
				}
			}
			else
			{
				Response.Write("<script>alert('Invalid Customer Email!');</script>");
			}
			
		}

		void deleteCustomerByEmail()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("delete from CUSTOMER where EMAIL='" + TextBox1.Text.Trim() + "'", con);
				cmd.ExecuteNonQuery();
				con.Close();
				Response.Write("<script>alert('Customer Deleted Successfully.');</script>");
				clearForm();
				GridView1.DataBind();
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void clearForm()
		{
			TextBox1.Text = "";
			TextBox2.Text = "";
			TextBox7.Text = "";
			TextBox8.Text = "";
			TextBox3.Text = "";
			TextBox4.Text = "";
			TextBox9.Text = "";
			TextBox10.Text = "";
		}

		bool checkIfCustomerExists()
		{
			try
			{
				SqlConnection con = new SqlConnection(strcon);
				if (con.State == ConnectionState.Closed)
				{
					con.Open();
				}
				SqlCommand cmd = new SqlCommand("SELECT * from CUSTOMER where EMAIL='" + TextBox1.Text.Trim() + "';", con);
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

    }
}
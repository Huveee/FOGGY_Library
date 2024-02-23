using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace FOGGY_Library
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        //user login
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "" || TextBox2.Text == "") /////EMAIL AND PASSWORD EMPTY CONTROL
            {
                Response.Write("<script>alert('Please fill all the fields');</script>");
                return;
            }

            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                SqlCommand cmd = new SqlCommand("select * from CUSTOMER where EMAIL='" + TextBox1.Text.Trim() + "' AND PASSWORD='" + TextBox2.Text.Trim() + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)//if any record coming in or not, if any user enters wrong ID or password, we will not have any records
                {
                    while (dr.Read())
                    {                                      //member id is in the 8th position,start from zero
                        Response.Write("<script>alert('Logged in Successfully');</script>");//pop up
                        Session["username"] = dr.GetValue(0).ToString();
                        Session["fullname"] = dr.GetValue(0).ToString();
                        Session["role"] = "user";
                        Session["status"] = dr.GetValue(10).ToString();//account status
                    }
                    Response.Redirect("homepage.aspx");//redirecting the user to the home page
                }
                else
                {
                    Response.Write("<script>alert('Invalid credentials');</script>");//pop up
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
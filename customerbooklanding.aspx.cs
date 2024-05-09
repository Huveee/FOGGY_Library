using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FOGGY_Library
{
	public partial class WebForm7 : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

		protected void Page_Load(object sender, EventArgs e)
		{
			BindLandingData();
		}

		private void BindLandingData()
		{
			GridViewLanding.DataSource = SqlDataSourceLanding;
			GridViewLanding.DataBind();
		}

		protected void Button2_Click(object sender, EventArgs e)
		{

			string bookName = TextBox1.Text.Trim();
			string authorName = TextBox2.Text.Trim();
			string authorSurname = TextBox7.Text.Trim();

			bool isUserExists = CheckIfUserExists(TextBox3.Text.Trim());

			if (!isUserExists)
			{
				Response.Write("<script>alert('This user was not found.');</script>");
				return;
			}
			DateTime pickUpDate;

			if (!DateTime.TryParse(TextBox5.Text, out pickUpDate) || pickUpDate < DateTime.Now)
			{
				Response.Write("<script>alert('Please enter a valid Pick Up Date.');</script>");
				return;
			}

			if (IsBookAlreadyPickedUp())
			{
				Response.Write("<script>alert('You already picked up this book.');</script>");
				return;
			}

			AddToLandingTable(bookName, authorName, authorSurname);
			Response.Write("<script>alert('The book is picked up succesfully.');</script>");
			GridViewLanding.DataBind();
		}

		protected void Button4_Click(object sender, EventArgs e)
		{
			string bookName = TextBox1.Text.Trim();
			string authorName = TextBox2.Text.Trim();
			string authorSurname = TextBox7.Text.Trim();

			bool isUserExists = CheckIfUserExists(TextBox3.Text.Trim());

			if (!isUserExists)
			{
				Response.Write("<script>alert('This user was not found.');</script>");
				return;
			}

			bool isBookBorrowed = CheckIfBookIsBorrowed(bookName, authorName, authorSurname);

			if (isBookBorrowed == true)
			{
				Response.Write("<script>alert('This book is already landed.');</script>");
				return;
			}

			DateTime dropOffDate;

			if (!DateTime.TryParse(TextBox6.Text, out dropOffDate) || dropOffDate < DateTime.Now)
			{
				Response.Write("<script>alert('Please enter a valid Drop Off Date.');</script>");
				return;
			}

			if (dropOffDate < GetPickUpDate())
			{
				Response.Write("<script>alert('Drop Off Date can not be before Pick Up Date.');</script>");
				return;
			}

			UpdateDropOffDateInLandingTable(bookName, authorName, authorSurname);
			GridViewLanding.DataBind();
			Response.Write("<script>alert('The book dropped off successfully.');</script>");
		}

		//Check if the book is available for loan
		private bool CheckIfBookIsBorrowed(string bookName, string authorName, string authorSurname)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();

					int customerID = GetCustomerIdByEmail(); 
					int bookID = GetBookByNameAndAuthor(); 

					if (bookID == 0)
					{
						//Book couldnt find
						return false;
					}

				//Check: If DROP_OFF_DATE column is not NULL, the book is already checked out
					string checkQuery = "SELECT DROP_OFF_DATE FROM LANDING WHERE CUSTOMER_ID = @customerID AND BOOK_ID = @bookID";

					using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
					{
						checkCmd.Parameters.AddWithValue("@customerID", customerID);
						checkCmd.Parameters.AddWithValue("@bookID", bookID);

						object dropOffDateObj = checkCmd.ExecuteScalar();

						return dropOffDateObj != null && dropOffDateObj != DBNull.Value;
					}
				}
			}
			catch (Exception ex)
			{
				
				//You can take action regarding the error situation
				// For example, logging the error message or reporting it to the user
				Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
				return false;
			}
		}


		private DateTime GetPickUpDate()
		{
			DateTime pickUpDate = DateTime.MinValue;

			using (SqlConnection con = new SqlConnection(strcon))
			{
				con.Open();
				string query = "SELECT PICK_UP_DATE FROM LANDING WHERE CUSTOMER_ID = @customerID AND BOOK_ID = @bookID";

				int customerID = GetCustomerIdByEmail();
				int bookID = GetBookByNameAndAuthor();

				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@customerID", customerID);
					cmd.Parameters.AddWithValue("@bookID", bookID);

					object result = cmd.ExecuteScalar();

					if (result != null && result != DBNull.Value)
					{
						pickUpDate = Convert.ToDateTime(result);
					}
				}
			}

			return pickUpDate;
		}

		private bool IsBookAlreadyPickedUp()
		{
			bool isPickedUp = false;

			int customerID = GetCustomerIdByEmail();
			int bookID = GetBookByNameAndAuthor();

			if (customerID != -1 && bookID != 0)
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();
					string query = "SELECT COUNT(*) FROM LANDING WHERE CUSTOMER_ID = @customerID AND BOOK_ID = @bookID AND DROP_OFF_DATE IS NULL";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@customerID", customerID);
						cmd.Parameters.AddWithValue("@bookID", bookID);

						int count = Convert.ToInt32(cmd.ExecuteScalar());
						isPickedUp = count > 0;
					}
				}
			}

			return isPickedUp;
		}

		private bool CheckIfUserExists(string email)
		{
			bool userExists = false;

			using (SqlConnection con = new SqlConnection(strcon))
			{
				con.Open();
				string query = "SELECT COUNT(*) FROM CUSTOMER WHERE EMAIL = @email";

				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@email", email);

					int count = Convert.ToInt32(cmd.ExecuteScalar());
					userExists = count > 0;
				}
			}

			return userExists;
		}

		private void UpdateBookAvailability(int bookID, bool isAvailable)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();
					string query = "UPDATE BOOK SET IS_AVAILABLE = @isAvailable WHERE ID = @bookID";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@isAvailable", isAvailable);
						cmd.Parameters.AddWithValue("@bookID", bookID);

						cmd.ExecuteNonQuery();
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		

		private void AddToLandingTable(string bookName, string authorName, string authorSurname)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();

					bool isBookAvailable = CheckBookAvailability(bookName, authorName, authorSurname);

					if (!isBookAvailable)
					{
						Response.Write("<script>alert('The Book is not available!.');</script>");
						return;
					}

					string query = "INSERT INTO LANDING (CUSTOMER_ID, BOOK_ID, PICK_UP_DATE) " +
								   "VALUES (@customerID, @bookID, @pickUpDate)";

					int customerID = GetCustomerIdByEmail();
					int bookID = GetBookByNameAndAuthor();

					if (bookID == 0)
					{
						Response.Write("<script>alert('The book was not found.');</script>");
						return;
					}
					else
					{

						using (SqlCommand cmd = new SqlCommand(query, con))
						{
							cmd.Parameters.AddWithValue("@customerID", customerID);
							cmd.Parameters.AddWithValue("@bookID", bookID);
							cmd.Parameters.AddWithValue("@pickUpDate", TextBox5.Text.Trim());

							cmd.ExecuteNonQuery();
						}

						UpdateBookAvailability(bookID, false);

					}
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		private void UpdateDropOffDateInLandingTable(string bookName, string authorName, string authorSurname)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();

					int customerID = GetCustomerIdByEmail();
					int bookID = GetBookByNameAndAuthor(); 

					if (bookID == 0)
					{
						Response.Write("<script>alert('The book was not found.');</script>");
						return;
					}
					else
					{
					
						string checkQuery = "SELECT DROP_OFF_DATE FROM LANDING WHERE CUSTOMER_ID = @customerID AND BOOK_ID = @bookID";

						using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
						{
							checkCmd.Parameters.AddWithValue("@customerID", customerID);
							checkCmd.Parameters.AddWithValue("@bookID", bookID);

							object dropOffDateObj = checkCmd.ExecuteScalar();

							if (dropOffDateObj != null && dropOffDateObj != DBNull.Value)
							{
								
								return;
							}
						}

					
						string updateQuery = "UPDATE LANDING SET DROP_OFF_DATE = @dropOffDate WHERE CUSTOMER_ID = @customerID AND BOOK_ID = @bookID";

						using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
						{
							updateCmd.Parameters.AddWithValue("@customerID", customerID);
							updateCmd.Parameters.AddWithValue("@bookID", bookID);
							updateCmd.Parameters.AddWithValue("@dropOffDate", TextBox6.Text.Trim());

							updateCmd.ExecuteNonQuery();

						}

						UpdateBookAvailability(bookID, true);
					}
				}
			}
			catch (Exception ex)
			{
				
				Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
			}
		}


		int GetCustomerIdByEmail()
		{
			int customerId = -1;

			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();
					string query = "SELECT ID FROM CUSTOMER WHERE EMAIL = @email";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@email", TextBox3.Text.Trim());

						object result = cmd.ExecuteScalar();

						if (result != null)
						{
							customerId = Convert.ToInt32(result);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write("Error: " + ex.Message);
			}

			return customerId;
		}

		int GetBookByNameAndAuthor()
		{
			int bookID = 0;

			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();
					string query = "SELECT B.ID FROM BOOK B " +
								   "INNER JOIN AUTHOR A ON B.AUTHOR_ID = A.ID " +
								   "WHERE B.NAME = @bookName AND A.NAME = @authorName";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@bookName", TextBox1.Text.Trim());
						cmd.Parameters.AddWithValue("@authorName", TextBox2.Text.Trim());

						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							if (reader.Read())
							{
								bookID = Convert.ToInt32(reader["ID"]);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write("Error: " + ex.Message);
			}

			return bookID;
		}
		private bool CheckBookAvailability(string bookName, string authorName, string authorSurname)
		{
			bool isAvailable = false;

			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();
					string query = "SELECT IS_AVAILABLE FROM BOOK B " +
								   "INNER JOIN AUTHOR A ON B.AUTHOR_ID = A.ID " +
								   "WHERE B.NAME = @bookName AND A.NAME = @authorName AND A.SURNAME = @authorSurname";

					using (SqlCommand cmd = new SqlCommand(query, con))
					{
						cmd.Parameters.AddWithValue("@bookName", bookName);
						cmd.Parameters.AddWithValue("@authorName", authorName);
						cmd.Parameters.AddWithValue("@authorSurname", authorSurname);

						object result = cmd.ExecuteScalar();
						if (result != null && result != DBNull.Value)
						{
							isAvailable = Convert.ToBoolean(result);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}

			return isAvailable;
		}

	}
}



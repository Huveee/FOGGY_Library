using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using System.Security.Policy;

namespace FOGGY_Library
{
	public partial class WebForm9 : System.Web.UI.Page
	{
		string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
		static string global_filepath;
		static int global_actual_stock, global_current_stock, global_issued_books;
		protected void Page_Load(object sender, EventArgs e)
		{
			GridViewCombined.DataBind();
		}

		//add button
		protected void Button1_Click(object sender, EventArgs e)
		{
			if ( TextBox2.Text == "" || TextBox5.Text == "" ) /////BOOK NAME AND AUTHOR NAME EMPTY CONTROL
			{
				Response.Write("<script>alert('Please fill the book name and author name fields');</script>");
				return;
			}

			Response.Write("<script>alert('Button 1 clicked.');</script>");
			if (checkIfBookExists())
			{
				Response.Write("<script>alert('Book Already Exists, try some other Book ID');</script>");
			}
			else
			{
				//Console.WriteLine("add new books'a girdi");
				Response.Write("<script>alert('add new books'a girdi');</script>");
				addNewBook();
			}
		}

		//update button
		protected void Button3_Click(object sender, EventArgs e)
		{
			if (TextBox2.Text == "" || TextBox5.Text == "") /////BOOK NAME AND AUTHOR NAME EMPTY CONTROL
			{
				Response.Write("<script>alert('Please fill the book name and author name fields');</script>");
				return;
			}

			updateBookByNameAndAuthorId();
		}

		//delete button
		protected void Button2_Click(object sender, EventArgs e)
		{
			if (TextBox2.Text == "" || TextBox5.Text == "") /////BOOK NAME AND AUTHOR NAME EMPTY CONTROL
			{
				Response.Write("<script>alert('Please fill the book name and author name fields');</script>");
				return;
			}

			deleteBookByNameAndAuthorId();
		}


		// user defined functions

		void deleteBookByNameAndAuthorId()
		{
			if (checkIfBookExists())
			{
				try
				{
					SqlConnection con = new SqlConnection(strcon);
					if (con.State == ConnectionState.Closed)
					{
						con.Open();
					}

					// First, retrieve the author ID based on the given name and surname
					int authorID = getAuthorIDByNameAndSurname(con, TextBox5.Text.Trim(), TextBox8.Text.Trim());

					// Check if the author ID is valid
					if (authorID != 0)
					{
						// Execute the DELETE query with the obtained author ID
						SqlCommand cmd = new SqlCommand("DELETE FROM BOOK WHERE NAME = @BookName AND AUTHOR_ID = @AuthorID", con);
						cmd.Parameters.AddWithValue("@BookName", TextBox2.Text.Trim());
						cmd.Parameters.AddWithValue("@AuthorID", authorID);

						cmd.ExecuteNonQuery();
						con.Close();
						Response.Write("<script>alert('Book Deleted Successfully');</script>");
						GridViewCombined.DataBind();
						clearForm();
					}
					else
					{
						// Handle the case where the author ID is not found
						Response.Write("<script>alert('There is no such author!!');</script>");
					}
				}
				catch (Exception ex)
				{
					Response.Write("<script>alert('" + ex.Message + "');</script>");
				}

			}
			else
			{
				Response.Write("<script>alert('Invalid Member ID');</script>");
			}
		}

		void updateBookByNameAndAuthorId()
		{
			try
			{
				string genres = "";
				foreach (int i in ListBox1.GetSelectedIndices())
				{
					genres = genres + ListBox1.Items[i] + ",";
				}
				genres = genres.Remove(genres.Length - 1);


				using (SqlConnection con = new SqlConnection(strcon))
				{
					if (con.State == ConnectionState.Closed)
					{
						con.Open();
					}

					int authorID = getAuthorIDByNameAndSurname(con, TextBox5.Text.Trim(), TextBox8.Text.Trim());
					int publisherID = getPublisherIdByName(con, TextBox4.Text.Trim());

					if (authorID == 0)
					{
						using (SqlCommand cmdInsertAuthor = new SqlCommand("INSERT INTO AUTHOR(NAME, SURNAME) VALUES(@NAME, @SURNAME)", con))
						{
							cmdInsertAuthor.Parameters.AddWithValue("@NAME", TextBox5.Text.Trim().ToUpper());
							cmdInsertAuthor.Parameters.AddWithValue("@SURNAME", TextBox8.Text.Trim().ToUpper());
							cmdInsertAuthor.ExecuteNonQuery();
							GridViewCombined.DataBind();
						}
					}

					if (publisherID == 0)
					{
						using (SqlCommand cmdInsertPublisher = new SqlCommand("INSERT INTO PUBLISHER(NAME) VALUES(@NAME)", con))
						{
							cmdInsertPublisher.Parameters.AddWithValue("@NAME", TextBox4.Text.Trim().ToUpper());
							cmdInsertPublisher.ExecuteNonQuery();
							GridViewCombined.DataBind();
						}
					}
					authorID = getAuthorIDByNameAndSurname(con, TextBox5.Text.Trim(), TextBox8.Text.Trim());
					publisherID = getPublisherIdByName(con, TextBox4.Text.Trim());
					using (SqlCommand cmdUpdateBook = new SqlCommand("UPDATE BOOK SET NAME=@NAME, AUTHOR_ID=@AUTHOR_ID, PUBLISHER_ID=@PUBLISHER_ID, GENRE=@GENRE, PUBLICATION_YEAR=@PUBLICATION_YEAR, PAGE_NUMBER=@PAGE_NUMBER, IS_AVAILABLE=@IS_AVAILABLE WHERE NAME=@BookName AND AUTHOR_ID=@AUTHOR_ID;", con))
					{
						cmdUpdateBook.Parameters.AddWithValue("@NAME", TextBox2.Text.Trim().ToUpper());
						cmdUpdateBook.Parameters.AddWithValue("@GENRE", genres);
						cmdUpdateBook.Parameters.AddWithValue("@AUTHOR_ID", authorID);
						cmdUpdateBook.Parameters.AddWithValue("@PUBLISHER_ID", publisherID);
						cmdUpdateBook.Parameters.AddWithValue("@PUBLICATION_YEAR", TextBox3.Text.Trim().ToUpper());
						cmdUpdateBook.Parameters.AddWithValue("@PAGE_NUMBER", TextBox11.Text.Trim().ToUpper());
						cmdUpdateBook.Parameters.AddWithValue("@IS_AVAILABLE", TextBox6.Text.Trim().ToUpper());
						cmdUpdateBook.Parameters.AddWithValue("@BookName", TextBox2.Text.Trim().ToUpper()); // Book adını kontrol et
						cmdUpdateBook.ExecuteNonQuery();
					}

					GridViewCombined.DataBind();
					Response.Write("<script>alert('Book Updated Successfully');</script>");
					clearForm();
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		void addNewBook()
		{
			try
			{

				// Önce seçili öğelerin listesini alın
				List<string> selectedGenres = new List<string>();
				foreach (ListItem listItem in ListBox1.Items)
				{
					if (listItem.Selected)
					{
						selectedGenres.Add(listItem.Text);
					}
				}

				// Şimdi seçili öğeleri virgülle birleştirin
				string genres = string.Join(",", selectedGenres);

				using (SqlConnection con = new SqlConnection(strcon))
				{
					con.Open();

					int authorID = getAuthorIDByNameAndSurname(con, TextBox5.Text.Trim(), TextBox8.Text.Trim());
					int publisherID = getPublisherIdByName(con, TextBox4.Text.Trim());

					if (authorID == 0)
					{
						using (SqlCommand cmd = new SqlCommand("INSERT INTO AUTHOR(NAME, SURNAME) VALUES(@NAME, @SURNAME)", con))
						{
							cmd.Parameters.AddWithValue("@NAME", TextBox5.Text.Trim().ToUpper());
							cmd.Parameters.AddWithValue("@SURNAME", TextBox8.Text.Trim().ToUpper());
							cmd.ExecuteNonQuery();
							GridViewCombined.DataBind();
						}
					}
					else if (publisherID == 0)
					{
						using (SqlCommand cmd = new SqlCommand("INSERT INTO PUBLISHER(NAME) VALUES(@NAME)", con))
						{
							cmd.Parameters.AddWithValue("@NAME", TextBox4.Text.Trim().ToUpper());
							cmd.ExecuteNonQuery();
							GridViewCombined.DataBind();
						}
					}

					authorID = getAuthorIDByNameAndSurname(con, TextBox5.Text.Trim(), TextBox8.Text.Trim());
					publisherID = getPublisherIdByName(con, TextBox4.Text.Trim());
					using (SqlCommand cmd = new SqlCommand("INSERT INTO BOOK(NAME, AUTHOR_ID, PUBLISHER_ID, GENRE, PUBLICATION_YEAR, PAGE_NUMBER, IS_AVAILABLE) VALUES(@NAME, @AUTHOR_ID, @PUBLISHER_ID, @GENRE, @PUBLICATION_YEAR, @PAGE_NUMBER, @IS_AVAILABLE)", con))
					{
						cmd.Parameters.AddWithValue("@NAME", TextBox2.Text.Trim().ToUpper());
						cmd.Parameters.AddWithValue("@GENRE", genres);
						cmd.Parameters.AddWithValue("@AUTHOR_ID", authorID);
						cmd.Parameters.AddWithValue("@PUBLISHER_ID", publisherID);
						cmd.Parameters.AddWithValue("@PUBLICATION_YEAR", TextBox3.Text.Trim().ToUpper());
						cmd.Parameters.AddWithValue("@PAGE_NUMBER", TextBox11.Text.Trim().ToUpper());
						cmd.Parameters.AddWithValue("@IS_AVAILABLE", TextBox6.Text.Trim().ToUpper());

						cmd.ExecuteNonQuery();
						Response.Write("<script>alert('Kitap başarıyla eklendi.');</script>");
						GridViewCombined.DataBind();
						clearForm();
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.Message + "');</script>");
			}
		}

		bool checkIfBookExists()
		{
			try
			{
				using (SqlConnection con = new SqlConnection(strcon))
				{
					if (con.State == ConnectionState.Closed)
					{
						con.Open();
					}

					int authorID = getAuthorIDByNameAndSurname(con, TextBox5.Text.Trim(), TextBox8.Text.Trim());

					if (authorID == 0)
					{
						// Eğer yazar bulunamazsa, yazarı ekleyin
						using (SqlCommand cmdInsertAuthor = new SqlCommand("INSERT INTO AUTHOR(NAME, SURNAME) VALUES(@NAME, @SURNAME)", con))
						{
							cmdInsertAuthor.Parameters.AddWithValue("@NAME", TextBox5.Text.Trim().ToUpper());
							cmdInsertAuthor.Parameters.AddWithValue("@SURNAME", TextBox8.Text.Trim().ToUpper());
							cmdInsertAuthor.ExecuteNonQuery();
						}
					}
					authorID = getAuthorIDByNameAndSurname(con, TextBox5.Text.Trim(), TextBox8.Text.Trim());
					// Kitap var mı kontrol et
					using (SqlCommand cmdCheckBook = new SqlCommand("SELECT * FROM BOOK WHERE NAME = @NAME AND AUTHOR_ID = @AUTHOR_ID", con))
					{
						cmdCheckBook.Parameters.AddWithValue("@NAME", TextBox2.Text.Trim());
						cmdCheckBook.Parameters.AddWithValue("@AUTHOR_ID", authorID);

						SqlDataAdapter da = new SqlDataAdapter(cmdCheckBook);
						DataTable dt = new DataTable();
						da.Fill(dt);

						// Kitap bulundu mu kontrol et
						if (dt.Rows.Count >= 1)
						{
							return true;
						}
						else
						{
							Response.Write("<script>alert('Kitap eklenebilir.');</script>");
							return false;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Response.Write("<script>alert('" + ex.ToString() + "');</script>");
				return false;
			}
		}


		void clearForm()
		{
			TextBox2.Text = "";
			TextBox4.Text = "";
			TextBox5.Text = "";
			TextBox8.Text = "";
			TextBox3.Text = "";
			TextBox7.Text = "";
			TextBox11.Text = "";
			TextBox6.Text = "";
			ListBox1.ClearSelection();
		}
		// Helper method to get Author ID based on name and surname

		int getAuthorIDByNameAndSurname(SqlConnection con, string authorName, string authorSurname)
		{
			int authorID = 0; // Default value if not found

			// Assuming your Author table has columns 'ID', 'Name', and 'Surname'
			using (SqlCommand cmd = new SqlCommand("SELECT ID FROM AUTHOR WHERE NAME = @AuthorName AND SURNAME = @AuthorSurname", con))
			{
				cmd.Parameters.AddWithValue("@AuthorName", authorName);
				cmd.Parameters.AddWithValue("@AuthorSurname", authorSurname);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						// Retrieve the Author ID if the author is found
						authorID = Convert.ToInt32(reader["ID"]);
					}
				}
			}

			return authorID;
		}

		int getPublisherIdByName(SqlConnection con, string publisherName)
		{
			int publisherID = 0; // Default value if not found
			using (SqlCommand cmd = new SqlCommand("SELECT ID FROM PUBLISHER WHERE NAME = @PublisherName", con))
			{
				cmd.Parameters.AddWithValue("@PublisherName", publisherName);

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						// Retrieve the Publisher ID if the publisher is found
						publisherID = Convert.ToInt32(reader["ID"]);
					}
				}
			}
			return publisherID;
		}



	}
}
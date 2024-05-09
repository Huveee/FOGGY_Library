<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="adminbookinventory.aspx.cs" Inherits="FOGGY_Library.WebForm9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
       $(document).ready(function () {
           $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
       });

       function readURL(input) {
           if (input.files && input.files[0]) {
               var reader = new FileReader();

               reader.onload = function (e) {
                   $('#imgview').attr('src', e.target.result);
               };

               reader.readAsDataURL(input.files[0]);
           }
       }

   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div class="container-fluid">
      <div class="row">
         <div class="col-md-5">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Book Details</h4>
                        </center>
                     </div> 
                  </div>
                  <div class="row">
                     <div class="col">
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  
                  <div class="row">
                     <div class="col-md-4">    <%--%8 diye düzeltcez%>--%>
                        <label>Book Name</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Book Name"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-4">                        
                        <label>Publisher Name</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Publisher Name"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>Author Name</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Author Name"></asp:TextBox>
                        </div>
                         <label>Author Surname</label>
                         <div class="form-group">
                            <asp:TextBox CssClass="form-control" ID="TextBox8" runat="server" placeholder="Author Surname"></asp:TextBox>
                        </div>
                        <label>Publish Date</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Year" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>Genre</label>
                        <div class="form-group">
                           <asp:ListBox CssClass="form-control" ID="ListBox1" runat="server" SelectionMode="Multiple" Rows="5">
                                <asp:ListItem Text="Action" Value="ACTION" />
                                <asp:ListItem Text="Adventure" Value="ADVENTURE" />
                                <asp:ListItem Text="Comic Book" Value="COMIC BOOK" />
                                <asp:ListItem Text="Self Help" Value="SELF HELP" />
                                <asp:ListItem Text="Motivation" Value="MOTIVATION" />
                                <asp:ListItem Text="Healthy Living" Value="HEALTHY LIVING" />
                                <asp:ListItem Text="Wellness" Value="WELLNESS" />
                                <asp:ListItem Text="Crime" Value="CRIME" />
                                <asp:ListItem Text="Drama" Value="DRAMA" />
                                <asp:ListItem Text="Fantasy" Value="FANTASY" />
                                <asp:ListItem Text="Horror" Value="HORROR" />
                                <asp:ListItem Text="Poetry" Value="POETRY" />
                                <asp:ListItem Text="Personal Development" Value="PERSONAL DEVELOPMENT" />
                                <asp:ListItem Text="Romance" Value="ROMANCE" />
                                <asp:ListItem Text="Science Fiction" Value="SCIENCE FICTION" />
                                <asp:ListItem Text="Suspense" Value="SUSPENSE" />
                                <asp:ListItem Text="Thriller" Value="THRILLER" />
                                <asp:ListItem Text="Art" Value="ART" />
                                <asp:ListItem Text="Autobiography" Value="AUTOBIOGRAPHY" />
                                <asp:ListItem Text="Encyclopedia" Value="ENCYCLOPEDIA" />
                                <asp:ListItem Text="Health" Value="HEALTH" />
                                <asp:ListItem Text="History" Value="HISTORY" />
                                <asp:ListItem Text="Math" Value="MATH" />
                                <asp:ListItem Text="Textbook" Value="TEXTBOOK" />
                                <asp:ListItem Text="Science" Value="SCIENCE" />
                                <asp:ListItem Text="Socialist Romanticism" Value="SOCIALIST ROMANTICISM" />                               
                                <asp:ListItem Text="Travel" Value="TRAVEL" />
                           </asp:ListBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">                                        
                     <div class="col-md-4">
                        <label>Pages</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox11" runat="server" placeholder="Pages" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     
                     
                     <div class="col-md-4">
                        <label>Issued Books</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Pages" TextMode="Number" ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                  </div>

                  <div class="row">
                     <div class="col-12">
                        <label>Is Available</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Is Available" TextMode="Number"></asp:TextBox><%-- 0 for no, 1 for yes --%>
                            
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-4">
                        <asp:Button ID="Button1" class="btn btn-lg btn-block btn-success" runat="server" Text="Add" OnClick="Button1_Click" />
                     </div>
                     <div class="col-4">
                        <asp:Button ID="Button3" class="btn btn-lg btn-block btn-warning" runat="server" Text="Update" OnClick="Button3_Click" />
                     </div>
                     <div class="col-4">
                        <asp:Button ID="Button2" class="btn btn-lg btn-block btn-danger" runat="server" Text="Delete" OnClick="Button2_Click" />
                     </div>
                  </div>
               </div>
            </div>
            <a href="homepage.aspx"><< Back to Home</a><br>
            <br>
         </div>
         <div class="col-md-7">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Book Inventory List</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row" style="overflow: auto; max-height: 500px;">
                    <asp:SqlDataSource ID="SqlDataSourceCombined" runat="server" ConnectionString="<%$ ConnectionStrings:foggyDBConnectionString %>" SelectCommand="SELECT B.ID, B.NAME, B.GENRE, B.PUBLICATION_YEAR, B.PAGE_NUMBER, B.IS_AVAILABLE, A.NAME as AUTHOR_NAME, A.SURNAME as AUTHOR_SURNAME, P.NAME as PUBLISHER_NAME FROM BOOK B INNER JOIN AUTHOR A ON B.AUTHOR_ID = A.ID INNER JOIN PUBLISHER P ON B.PUBLISHER_ID = P.ID"></asp:SqlDataSource>
                    <asp:GridView class="table table-striped table-bordered" ID="GridViewCombined" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSourceCombined">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                            <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" />
                            <asp:BoundField DataField="GENRE" HeaderText="GENRE" SortExpression="GENRE" />
                            <asp:BoundField DataField="PUBLICATION_YEAR" HeaderText="PUBLICATION_YEAR" SortExpression="PUBLICATION_YEAR" />
                            <asp:BoundField DataField="PAGE_NUMBER" HeaderText="PAGE_NUMBER" SortExpression="PAGE_NUMBER" />
                            <asp:CheckBoxField DataField="IS_AVAILABLE" HeaderText="IS_AVAILABLE" SortExpression="IS_AVAILABLE" />
                            <asp:BoundField DataField="AUTHOR_NAME" HeaderText="AUTHOR_NAME" SortExpression="AUTHOR_NAME" />
                            <asp:BoundField DataField="AUTHOR_SURNAME" HeaderText="AUTHOR_SURNAME" SortExpression="AUTHOR_SURNAME" />
                            <asp:BoundField DataField="PUBLISHER_NAME" HeaderText="PUBLISHER_NAME" SortExpression="PUBLISHER_NAME" />
                        </Columns>
                    </asp:GridView>
                </div>

                  </div>
               </div>
            </div>
         </div>
      </div>
   
</asp:Content>
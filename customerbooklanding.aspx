<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="customerbooklanding.aspx.cs" Inherits="FOGGY_Library.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
      <div class="row">
         <div class="col-md-5">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Book Landing</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="100px" src="imgs/books.png" />
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>

                  <div class="row">
                      <div class="col-md-6">
                        <label>Email</label>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Email"></asp:TextBox>
                            </div>
                        </div>
                        </div>
                     <div class="col-md-6">
                        <label>Book Name</label>
                        <div class="form-group">
                           <div class="input-group">
                              <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Book Name"></asp:TextBox>
                           </div>
                        </div>
                      </div>
                      <div class="col-md-6">
                        <label>Author Name</label>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Author Name"></asp:TextBox>
                            </div>
                        </div>
                      </div>
                      <div class="col-md-6">
                       <label>Author Surname</label>
                       <div class="form-group">
                          <div class="input-group">
                             <asp:TextBox CssClass="form-control" ID="TextBox7" runat="server" placeholder="Author Surname"></asp:TextBox>
                          </div>
                       </div>
                       </div>
                    </div>

                  <div class="row">
                     <div class="col-md-6">
                        <label>Start Date</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Start Date" TextMode="Date"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>End Date</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="End Date" TextMode="Date"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-6">
                        <asp:Button ID="Button2" class="btn btn-lg btn-block btn-primary" runat="server" Text="Pick Up" OnClick ="Button2_Click"/>
                     </div>
                     <div class="col-6">
                        <asp:Button ID="Button4" class="btn btn-lg btn-block btn-success" runat="server" Text="Drop Off" OnClick ="Button4_Click"/>
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
                           <h4>Landed Book List</h4>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>

                   <div class="row" style="overflow: auto; max-height: 500px;">
                        <asp:SqlDataSource ID="SqlDataSourceLanding" runat="server" ConnectionString="<%$ ConnectionStrings:foggyDBConnectionString %>" 
                            SelectCommand="SELECT L.ID, C.EMAIL AS CUSTOMER_EMAIL, B.NAME AS BOOK_NAME, A.NAME + ' ' + A.SURNAME AS AUTHOR_NAME_AND_SURNAME, 
                                           L.PICK_UP_DATE, L.DROP_OFF_DATE 
                                           FROM LANDING L 
                                           INNER JOIN BOOK B ON L.BOOK_ID = B.ID 
                                           INNER JOIN AUTHOR A ON B.AUTHOR_ID = A.ID 
                                           INNER JOIN CUSTOMER C ON L.CUSTOMER_ID = C.ID">
                        </asp:SqlDataSource>
                        <asp:GridView class="table table-striped table-bordered" ID="GridViewLanding" runat="server" AutoGenerateColumns="False" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <div class="container-fluid">
                                            <div class="row">
                                                <div class="col-lg-10">
                                                    <div class="row">
                                                        <div class="col-12">
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("BOOK_NAME") %>' Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-12">
                                                            &nbsp; <span><span>&nbsp;</span>Customer Email - </span>
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text='<%# Eval("CUSTOMER_EMAIL") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-12">
                                                            &nbsp; <span><span>&nbsp;</span>Author - </span>
                                                            <asp:Label ID="LabelAuthor" runat="server" Font-Bold="True" Text='<%# Eval("AUTHOR_NAME_AND_SURNAME") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-12">
                                                            &nbsp; <span><span>&nbsp;</span>Pick Up Date - </span>
                                                            <asp:Label ID="LabelPickUpDate" runat="server" Font-Bold="True" Text='<%# Eval("PICK_UP_DATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </div>
                                                        <div class="col-12">
                                                            &nbsp; <span><span>&nbsp;</span>Drop Off Date - </span>
                                                            <asp:Label ID="LabelDropOffDate" runat="server" Font-Bold="True" Text='<%# Eval("DROP_OFF_DATE", "{0:dd-MM-yyyy}") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</asp:Content>

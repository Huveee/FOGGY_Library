﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="userprofile.aspx.cs" Inherits="WebApplication3.userprofile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
       $(document).ready(function () {
           $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
       });
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
                           <img width="100px" src="imgs/generaluser.png"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Your Profile</h4>
                           <span>Account Status - </span>
                           <asp:Label class="badge badge-pill badge-info" ID="Label1" runat="server" Text="Your status"></asp:Label>
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
                        <label>Full Name</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Full Name"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Surname</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Surname"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-6">
                        <label>Contact No</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox3" runat="server" placeholder="Contact No" TextMode="Number"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-6">
                        <label>Email </label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox4" runat="server" placeholder="Email " TextMode="Email"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-4">
                        <label>State</label>
                        <div class="form-group">
                            <asp:DropDownList class="form-control" ID="DropDownList1" runat="server">
                                <asp:ListItem Text="Select" Value="select" />
                                <asp:ListItem Text="Aliağa" Value="Aliağa" />
                                <asp:ListItem Text="Balçova" Value="Balçova" />
                                <asp:ListItem Text="Bayındır" Value="Bayındır" />
                                <asp:ListItem Text="Bayraklı" Value="Bayraklı" />
                                <asp:ListItem Text="Bergama" Value="Bergama" />
                                <asp:ListItem Text="Beydağ" Value="Beydağ" />
                                <asp:ListItem Text="Bornova" Value="Bornova" />
                                <asp:ListItem Text="Buca" Value="Buca" />
                                <asp:ListItem Text="Çeşme" Value="Çeşme" />
                                <asp:ListItem Text="Çiğli" Value="Çiğli" />
                                <asp:ListItem Text="Dikili" Value="Dikili" />
                                <asp:ListItem Text="Foça" Value="Foça" />
                                <asp:ListItem Text="Gaziemir" Value="Gaziemir" />
                                <asp:ListItem Text="Güzelbahçe" Value="Güzelbahçe" />
                                <asp:ListItem Text="Karabağlar" Value="Karabağlar" />
                                <asp:ListItem Text="Karaburun" Value="Karaburun" />
                                <asp:ListItem Text="Karşıyaka" Value="Karşıyaka" />
                                <asp:ListItem Text="Kemalpaşa" Value="Kemalpaşa" />
                                <asp:ListItem Text="Kınık" Value="Kınık" />
                                <asp:ListItem Text="Kiraz" Value="Kiraz" />
                                <asp:ListItem Text="Konak" Value="Konak" />
                                <asp:ListItem Text="Menderes" Value="Menderes" />
                                <asp:ListItem Text="Menemen" Value="Menemen" />
                                <asp:ListItem Text="Narlıdere" Value="Narlıdere" />
                                <asp:ListItem Text="Ödemiş" Value="Ödemiş" />
                                <asp:ListItem Text="Seferihisar" Value="Seferihisar" />
                                <asp:ListItem Text="Selçuk" Value="Selçuk" />
                                <asp:ListItem Text="Tire" Value="Tire" />
                                <asp:ListItem Text="Torbalı" Value="Torbalı" />
                                <asp:ListItem Text="Urla" Value="Urla" />
                            </asp:DropDownList>
                        </div>
                     </div>
                       <div class="col-md-6">
                        <label>Registiration Date</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox5" runat="server" placeholder="Registiration Date" TextMode="Date" ></asp:TextBox>
                        </div>
                     </div>
                      <div class="col-md-6">
                        <label>Date Of Birth</label>
                        <div class="form-group">
                           <asp:TextBox CssClass="form-control" ID="TextBox6" runat="server" placeholder="Date Of Birth" TextMode="Date" ></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <span class="badge badge-pill badge-info">Login Credentials</span>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-md-4">
                        <label>User ID</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="TextBox8" runat="server" placeholder="User ID" ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>Old Password</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="TextBox9" runat="server" placeholder="Old Password" ReadOnly="True"></asp:TextBox>
                        </div>
                     </div>
                     <div class="col-md-4">
                        <label>New Password</label>
                        <div class="form-group">
                           <asp:TextBox class="form-control" ID="TextBox10" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
                        </div>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col-8 mx-auto">
                        <center>
                           <div class="form-group">
                              <asp:Button class="btn btn-primary btn-block btn-lg" ID="Button1" runat="server" Text="Update" OnClick="Button1_Click" />
                           </div>
                        </center>
                     </div>
                  </div>
               </div>
            </div>
            <a href="homepage.aspx"><< Back to Home</a><br><br>
         </div>
         <div class="col-md-7">
            <div class="card">
               <div class="card-body">
                  <div class="row">
                     <div class="col">
                        <center>
                           <img width="100px" src="imgs/books1.png"/>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <center>
                           <h4>Your Issued Books</h4>
                           <asp:Label class="badge badge-pill badge-info" ID="Label2" runat="server" Text="your books info"></asp:Label>
                        </center>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <hr>
                     </div>
                  </div>
                  <div class="row">
                     <div class="col">
                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"></asp:GridView>
                     </div>
                  </div>
               </div>
            </div>
         </div>
      </div>
   </div>
</asp:Content>
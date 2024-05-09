<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="FOGGY_Library.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <section>
      <img src="images/background1.png" class="img-fluid"/>
   </section>
   <section>
      <div class="container">
         <div class="row">
            <div class="col-12">
               <center>
                  <h2>The Features</h2>
                  <p><b>The 3 Primary Features </b></p>
               </center>
            </div>
         </div>
         <div class="row">
            <div class="col-md-4">
               <center>
                  <img width="230px" src="images/virtualLibrary.jpg"/>
                  <h4>Virtual Library</h4>
                  <p class="text-justify"> Access dozens of books that you want to read. </p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
                  <img width="150px" src="images/findBooks.png"/>
                  <h4>Search Books</h4>
                  <p class="text-justify"> Search and find the books that you like. </p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
                  <img width="150px" src="images/favorite.png"/>
                  <h4>Favorites</h4>
                  <p class="text-justify"> Add the books to your favorites. </p>
               </center>
            </div>
         </div>
      </div>
   </section>
   <section>
      <img src="images/mod4.png" class="img-fluid"/>
   </section>
   <section>
      <div class="container">
         <div class="row">
            <div class="col-12">
               <center>
                  <h2>The Process</h2>
                  <p><b> We have a Simple 3 Step Process </b></p>
               </center>
            </div>
         </div>
         <div class="row">
            <div class="col-md-4">
               <center>
                  <img width="150px" src="images/signUp.jpg" />
                  <h4>Sign Up</h4>
                  <p class="text-justify"> Enter your informations for signing up. Log in if you have a membership. </p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
                  <img width="150px" src="images/search.png"/>
                  <h4>Search Books</h4>
                  <p class="text-justify"> Search any book that you want to reserve for yourself. </p>
               </center>
            </div>
            <div class="col-md-4">
               <center>
                  <img width="210px" src="images/libraryBuilding.png"/>
                  <h4>Visit</h4>
                  <p class="text-justify"> Visit the library and pick up your book that you reserved. </p>
               </center>
            </div>
         </div>
      </div>
   </section>
</asp:Content>
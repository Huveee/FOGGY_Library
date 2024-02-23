<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="viewbooks.aspx.cs" Inherits="FOGGY_Library.WebForm11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <script type="text/javascript">
            $(document).ready(function () {
                $(".table").prepend($("<thead></thead>").append($(this).find("tr:first"))).dataTable();
            });
        </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="container">
            <div class="row">

                <div class="col-sm-12">
                    <center>
                        <h3>
                        Book Inventory List</h3>
                    </center>
                    <div class="row">
                        <div class="col-sm-12 col-md-12">
                            <asp:Panel class="alert alert-success" role="alert" ID="Panel1" runat="server" Visible="False">
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </asp:Panel>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="card">
                            <div class="card-body">

                                <div class="row">
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:foggyDBConnectionString %>" SelectCommand="SELECT * FROM [BOOK]"></asp:SqlDataSource>
                                    <div class="col">
                                        <asp:GridView class="table table-striped table-bordered" ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource1">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" InsertVisible="False">
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NAME" HeaderText="NAME" SortExpression="NAME" />
                                                <asp:BoundField DataField="AUTHOR_ID" HeaderText="AUTHOR_ID" SortExpression="AUTHOR_ID" />
                                                <asp:BoundField DataField="PUBLISHER_ID" HeaderText="PUBLISHER_ID" SortExpression="PUBLISHER_ID" />
                                                <asp:BoundField DataField="GENRE" HeaderText="GENRE" SortExpression="GENRE" />
                                                <asp:BoundField DataField="PUBLICATION_YEAR" HeaderText="PUBLICATION_YEAR" SortExpression="PUBLICATION_YEAR" />
                                                <asp:BoundField DataField="PAGE_NUMBER" HeaderText="PAGE_NUMBER" SortExpression="PAGE_NUMBER" />
                                                <asp:CheckBoxField DataField="IS_AVAILABLE" HeaderText="IS_AVAILABLE" SortExpression="IS_AVAILABLE" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <center>
                    <a href="home.aspx">
                        << Back to Home</a><span class="clearfix"></span>
                            <br />
                            <center>
            </div>
        </div>
    </asp:Content>

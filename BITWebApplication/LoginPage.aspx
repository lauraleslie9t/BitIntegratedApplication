<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BITWebApplication.LoginPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-6 mx-auto">
                <div class="card ">
                    <div class="card-body ">
                        <div class="row">
                            <div class="col">
                                <center>
                                    <img src="Logo_White.png" width="400" alt="BIT logo" />
                                    
                                </center>
                                <br />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <%-- <center><h3>User Login</h3></center>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="col">
                            <label>Username</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="txtUsername" runat="server" placeholder="Username"></asp:TextBox>
                            </div>
                            <label>Password</label>
                            <div class="form-group">
                                <asp:TextBox CssClass="form-control" ID="txtPassword" runat="server" placeholder="Password"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label id="lblWarning" class="text-danger" runat="server" Visible="false">Incorrect Login. Please try again.</asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Button CssClass="btn btn-dark btn-block btn-lg text-white" ID="btnLogin" runat="server" Text="Log In" OnClick="btnLogin_Click" /> 
                            </div>
                            <div class="form-group">
                                <asp:Button CssClass="btn btn-secondary btn-block btn-lg" ID="btnSignUp" runat="server" Text="Sign Up" /> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

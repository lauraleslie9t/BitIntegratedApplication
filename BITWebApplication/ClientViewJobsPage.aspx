<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientViewJobsPage.aspx.cs" Inherits="BITWebApplication.ClientViewJobsPage" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
         <row>
            <div class="col-12">
                <div style="text-align:center">
                    <H4>All Jobs</H4>
                </div>
            </div>
        </row>
        <div class="row">
            <div class="col-12">
                 <asp:GridView ID="gvClientJobs" CssClass="table table-striped table-bordered" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvClientJobs_PageIndexChanging"> 
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

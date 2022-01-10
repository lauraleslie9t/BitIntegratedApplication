<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoordinatorViewCompletedJobsPage.aspx.cs" Inherits="BITWebApplication.CoordinatorViewCompletedJobsPage" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
         <row>
            <div class="col-12">
                <div style="text-align:center">
                    <H4>All Completed Jobs</H4>
                </div>
            </div>
        </row>
        <div class="row">
            <div class="col-12">
                 <asp:GridView ID="gvAllCompletedJobs" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvAllCompletedJobs_RowCommand" AllowPaging="true" PageSize="7" OnPageIndexChanging="gvAllCompletedJobs_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Complete">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnConfirm" 
                                    runat="server" 
                                    CommandName="Complete" 
                                    Height="40px" 
                                    Text="Mark Complete" 
                                    Width="150px" 
                                    CommandArgument="<%#Container.DataItemIndex %>"
                                    />
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Incomplete">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnReject" 
                                    runat="server" 
                                    CommandName="Incomplete" 
                                    Height="40px" 
                                    Text="Mark Incomlplete" 
                                    Width="150px" 
                                    CommandArgument="<%#Container.DataItemIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

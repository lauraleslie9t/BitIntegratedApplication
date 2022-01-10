<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoordinatorViewUnassignedJobs.aspx.cs" Inherits="BITWebApplication.CoordinatorViewUnassignedJobs" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
         <row>
            <div class="col-12">
                <div style="text-align:center">
                    <H4>New Jobs</H4>
                </div>
            </div>
        </row>
        <div class="row">
            <div class="col-12">
                 <asp:GridView ID="gvUnassignedJobs" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvUnassignedJobs_RowCommand" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvUnassignedJobs_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Assign Contractor">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnSelect" 
                                    runat="server" 
                                    CommandName="Select" 
                                    Height="40px" 
                                    Text="Select" 
                                    Width="80px" 
                                    CommandArgument="<%#Container.DataItemIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
         <row>
            <div class="col-12">
                <div style="text-align:center">
                    <H4>Rejected Jobs</H4>
                </div>
            </div>
        </row>
        <div class="row">
            <div class="col-12">
                 <asp:GridView ID="gvRejectedJobs" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvRejectedJobs_RowCommand" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvRejectedJobs_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Assign Contractor">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnSelect" 
                                    runat="server" 
                                    CommandName="Select" 
                                    Height="40px" 
                                    Text="Select" 
                                    Width="80px" 
                                    CommandArgument="<%#Container.DataItemIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

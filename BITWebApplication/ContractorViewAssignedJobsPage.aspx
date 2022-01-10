<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContractorViewAssignedJobsPage.aspx.cs" Inherits="BITWebApplication.ContractorViewAssignedJobsPage" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
         <row>
            <div class="col-12">
                <div style="text-align:center">
                    <H4>All Assigned Jobs</H4>
                </div>
            </div>
        </row>
        <row>
            <div class="row">
                <div class="col-12">
                    <asp:Label id="lblWarning" class="text-danger" runat="server"  Visible="false" >Error</asp:Label>
                </div>
            </div>
        </row>
        <div class="row">
            <div class="col-12">
                 <asp:GridView ID="gvAssignedJobs" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvAssignedJobs_RowCommand" >
                    <Columns>
                         <asp:TemplateField HeaderText="Accept">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnConfirm" 
                                    runat="server" 
                                    CommandName="Confirm" 
                                    Height="40px" 
                                    Text="Confirm" 
                                    Width="80px" 
                                    CommandArgument="<%#Container.DataItemIndex %>"
                                    />
                             </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rejection Comment">
                            <ItemTemplate>
                            <asp:TextBox    ID="txtRejectionComent"
                                            runat="server" CommandName="RejectionComment"
                                            Height="40px" Text=""
                                            Width="150px"
                                            CommandArgument="<%#Container.DataItemIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Reject">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnReject" 
                                    runat="server" 
                                    CommandName="Reject" 
                                    Height="40px" 
                                    Text="Reject" 
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

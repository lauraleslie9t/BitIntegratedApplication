<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ContractorViewJobsPage.aspx.cs" Inherits="BITWebApplication.ContractorViewJobsPage" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="container-fluid">
         <row>
            <div class="col-12">
                <div style="text-align:center">
                    <H4>All Confirmed Jobs</H4>
                </div>
            </div>
        </row>
        <row>
            <div class="row">
                <div class="col-12">
                    <asp:Label id="lblWarning" class="text-danger" runat="server" ></asp:Label>
                </div>
            </div>
        </row>
        <div class="row">
            <div class="col-12">
                 <asp:GridView ID="gvContractorJobs" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvContractorJobs_RowCommand"> 
                     <Columns>
                        <asp:TemplateField HeaderText="Km Travelled">
                            <ItemTemplate>
                            <asp:TextBox    ID="txtKmTravelled"
                                            runat="server" 
                                            Height="40px" Text=""
                                            Width="150px"
                                            CommandArgument="<%#Container.DataItemIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours Taken">
                            <ItemTemplate>
                            <asp:TextBox    ID="txtHoursTaken"
                                            runat="server" 
                                            Height="40px" Text=""
                                            Width="150px"
                                            CommandArgument="<%#Container.DataItemIndex %>"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Save">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="btnSaveDetails" 
                                    runat="server" 
                                    CommandName="Save" 
                                    Height="40px" 
                                    Text="Save" 
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

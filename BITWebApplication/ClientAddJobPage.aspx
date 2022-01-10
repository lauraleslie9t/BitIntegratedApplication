<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClientAddJobPage.aspx.cs" Inherits="BITWebApplication.ClientAddJobPage"  MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-9 mx-auto">
                <div class="card">
                    <div class="card-body">
                        <row>
                            <div class="col">
                                <center>
                                    <H3>Book a New Job</H3>
                                </center>
                            </div>
                        </row>
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                         <div class="row">
                            <div class ="col-12">
                                <label>Job Locations:</label>
                                <asp:GridView ID="gvClientLocations" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvClientLocations_RowCommand" AllowPaging="true" PageSize="3" OnPageIndexChanging="gvClientLocations_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select Location">
                                            <ItemTemplate>
                                                <asp:Button 
                                                    ID="btnConfirm" 
                                                    runat="server" 
                                                    CommandName="SelectLocation" 
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
                         <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3">
                                <label>Required Completion Date:</label>
                               <div class="form-group">
                                 <asp:Calendar ID="calRequiredCompletionDate" runat="server">
                                 </asp:Calendar>
                                </div>
                            </div>
                            <div class="col-9">
                                <label>Job Description</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control align-content-lg-start" TextMode="MultiLine" ID="txtJobDescription" runat="server" Height="230" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                           
                        </div>
   
                        
                        <div class="row">
                             <div class="col-6">
                                <label>Priority:</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="ddlPriority" runat="server">
                                        <asp:ListItem Text="Select" Value="select" />
                                        <asp:ListItem Text="Low" Value="Low" />
                                        <asp:ListItem Text="Moderate" Value="Moderate" />
                                        <asp:ListItem Text="High" Value="High" />
                                        <asp:ListItem Text="Urgent" Value="Urgent" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            
                        </div>
                         <div>
                           <asp:Label id="lblWarning" class="text-danger" runat="server" ></asp:Label>
                        </div>
                        <div class ="row">
                            <div class="form-group col-6">
                                <asp:Button CssClass="btn btn-secondary btn-block btn-lg" ID="btnCancelJob" runat="server" Text="Cancel" OnClick="btnCancelJob_Click"/>
                            </div>
                            <div class="form-group col-6">
                                <asp:Button CssClass="btn btn-success btn-block btn-lg" ID="btnSaveJob" runat="server" Text="Save" OnClick="btnSaveJob_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

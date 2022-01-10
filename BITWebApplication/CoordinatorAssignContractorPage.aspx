<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoordinatorAssignContractorPage.aspx.cs" Inherits="BITWebApplication.CoordinatorAssignContractorPage" MaintainScrollPositionOnPostback="true" %>
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
                                    <H3>Assign Contractor</H3>
                                </center>
                            </div>
                        </row>
                        
                        <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>

                         <div class="row">
                            <div class ="col-6">
                                <label>Required Skills:</label>
                                <asp:GridView ID="gvSkills" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvSkills_RowCommand" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select Skills">
                                            <ItemTemplate>
                                                <asp:Button 
                                                    ID="btnConfirm" 
                                                    runat="server" 
                                                    CommandName="SelectSkill" 
                                                    Height="40px" 
                                                    Text="Add" 
                                                    Width="80px" 
                                                    CommandArgument="<%#Container.DataItemIndex %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                             <div class ="col-6">
                                <label>Selected Skills:</label>
                                <asp:GridView ID="gvSelectedSkills" CssClass="table table-striped table-bordered" runat="server" OnRowCommand="gvSelectedSkills_RowCommand" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select Skills">
                                            <ItemTemplate>
                                                <asp:Button 
                                                    ID="btnConfirm" 
                                                    runat="server" 
                                                    CommandName="SelectSkill" 
                                                    Height="40px" 
                                                    Text="Remove" 
                                                    Width="80px" 
                                                    CommandArgument="<%#Container.DataItemIndex %>"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class ="row">
                            <div class="form-group col-12">
                                <asp:Button CssClass="btn btn-secondary btn-block btn-lg" ID="btnGetSessions" runat="server" Text="Get Available Sessions" OnClick="btnGetSessions_Click"/>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                          <div class="row">
                            <div class ="col-12">
                                <label>Available Sessions:</label>
                                <asp:GridView ID="gvAvailableSessions" CssClass="table table-striped table-bordered" runat="server"  OnRowCommand="gvAvailableSessions_RowCommand" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvAvailableSessions_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select Session">
                                            <ItemTemplate>
                                                <asp:Button 
                                                    ID="btnConfirm" 
                                                    runat="server" 
                                                    CommandName="SelectSession" 
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
                            <div class="col-12">
                            <asp:Label id="lblSessionWarning" class="text-danger" runat="server" ></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <label>Expected Hours:</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control align-content-lg-start" ID="txtExpHours" runat="server" placeholder=""></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-6">
                                <label>Start Time:</label>
                                <div class="form-group">
                                    <asp:DropDownList CssClass="form-control" ID="ddlPriority" runat="server">
                                        <asp:ListItem Text="Select" Value = "Select"/>
                                        <asp:ListItem Text="6:00" Value="6:00" />
                                        <asp:ListItem Text="7:00" Value="7:00" />
                                        <asp:ListItem Text="8:00" Value="8:00" />
                                        <asp:ListItem Text="9:00" Value="9:00" />
                                        <asp:ListItem Text="10:00" Value="10:00" />
                                        <asp:ListItem Text="11:00" Value="11:00" />
                                        <asp:ListItem Text="12:00" Value="12:00" />
                                        <asp:ListItem Text="13:00" Value="13:00" />
                                        <asp:ListItem Text="14:00" Value="14:00" />
                                        <asp:ListItem Text="15:00" Value="15:00" />
                                        <asp:ListItem Text="16:00" Value="16:00" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col">
                                <hr />
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-12">
                            <asp:Label id="lblSaveWarning" class="text-danger" runat="server" ></asp:Label>
                            </div>
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

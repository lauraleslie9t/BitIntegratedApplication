using BITWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITWebApplication
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                if (Session["ClientId"] != null)
                {
                    Session.Remove("ClientId");
                    Session.Remove("Location");
                    Session.Remove("SelectedRow");
                }
                if (Session["ContractorId"] != null)
                {
                    Session.Remove("ContractorId");
                    Session.Remove("AvailabilityId");
                    Session.Remove("SelectedRow");
                    Session.Remove("ItemPage");
                }
                if (Session["StaffId"] != null)
                {
                    Session.Remove("StaffId");
                }
            }
            //client
            LinkButton addJobLink = (LinkButton)this.Master.FindControl("lbtnAddJobs");
            addJobLink.Visible = false;
            LinkButton viewJobsLink = (LinkButton)this.Master.FindControl("lbtnClientJobs");
            viewJobsLink.Visible = false;

            //contractor
            LinkButton viewContJobs = (LinkButton)this.Master.FindControl("lbtnContJobs");
            viewContJobs.Visible = false;
            LinkButton viewContAssiJobs = (LinkButton)this.Master.FindControl("lbtnAssignedJobs");
            viewContAssiJobs.Visible = false;


            //coordinator/admin
            LinkButton coordAssignJobs = (LinkButton)this.Master.FindControl("lbtnAssignJobs");
            coordAssignJobs.Visible = false;
            LinkButton coordCompJobs = (LinkButton)this.Master.FindControl("lbtnCompletedJobs");
            coordCompJobs.Visible = false;
            LinkButton coordUnassignedJobs = (LinkButton)this.Master.FindControl("lbtnUnassignedJobs");
            coordUnassignedJobs.Visible = false;


            //login
            LinkButton signInLink = (LinkButton)this.Master.FindControl("lbtnLogIn");
            signInLink.Text = "Sign In";
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            UserLogin login = new UserLogin();
            login.Email = txtUsername.Text;
            login.Password = txtPassword.Text;
            if (login.Email != "" & login.Password != "")
            {
                string userType = login.GetUserType();
                if (userType == "Client")
                {
                    Session.Add("ClientId", login.UserId);
                    Response.Redirect("ClientViewJobsPage.aspx");
                }
                else if (userType == "Contractor")
                {
                    Session.Add("ContractorId", login.UserId);
                    Response.Redirect("ContractorViewJobsPage.aspx");
                }
                else if (userType == "Coordinator" | userType == "Admin")
                {
                    Session.Add("StaffID", login.UserId);
                    Response.Redirect("CoordinatorViewCompletedJobsPage.aspx");
                }
                if (userType == "error")
                {
                    lblWarning.Visible = true;
                }



            }
            else
            {
                lblWarning.Visible = true;
            }
            
        }

       
    }
}
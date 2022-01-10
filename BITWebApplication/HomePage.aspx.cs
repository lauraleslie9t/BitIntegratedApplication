using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITWebApplication
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ClientId"] != null)
            {
                LinkButton addJobLink = (LinkButton)this.Master.FindControl("lbtnAddJobs");
                addJobLink.Visible = true;

                LinkButton viewJobsLink = (LinkButton)this.Master.FindControl("lbtnClientJobs");
                viewJobsLink.Visible = true;

                LinkButton signInLink = (LinkButton)this.Master.FindControl("lbtnLogIn");
                signInLink.Text = "Sign Out";
                
            }
            if (Session["ContractorId"] != null)
            {
                LinkButton viewContJobs = (LinkButton)this.Master.FindControl("lbtnContJobs");
                viewContJobs.Visible = true;

                LinkButton viewContAssiJobs = (LinkButton)this.Master.FindControl("lbtnAssignedJobs");
                viewContAssiJobs.Visible = true;

                LinkButton signInLink = (LinkButton)this.Master.FindControl("lbtnLogIn");
                signInLink.Text = "Sign Out";
                //RefreshGrid();
            }
            if (Session["StaffId"] != null)
            {
                LinkButton coordAssignJobs = (LinkButton)this.Master.FindControl("lbtnAssignJobs");
                coordAssignJobs.Visible = false;

                LinkButton coordCompJobs = (LinkButton)this.Master.FindControl("lbtnCompletedJobs");
                coordCompJobs.Visible = true;
                LinkButton coordUnassignedJobs = (LinkButton)this.Master.FindControl("lbtnUnassignedJobs");
                coordUnassignedJobs.Visible = true;

                LinkButton signInLink = (LinkButton)this.Master.FindControl("lbtnLogIn");
                signInLink.Text = "Sign Out";
                //RefreshGrid();
            }



        }
    }
}
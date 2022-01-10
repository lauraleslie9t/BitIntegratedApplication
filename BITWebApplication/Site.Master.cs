using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BITWebApplication
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbtnLogIn_Click(object sender, EventArgs e)
        {
            Response.Redirect("LoginPage.aspx");
        }

        protected void lbtnContJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContractorViewJobsPage.aspx");
        }

        protected void lbtnAssignedJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ContractorViewAssignedJobsPage.aspx");
        }

        protected void lbtnClientJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientViewJobsPage.aspx");
        }

        protected void lbtnAddJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClientAddJobPage.aspx");
        }

        protected void lbtnAssignJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoordinatorAssignContractorPage.aspx");
        }

        protected void lbtnCompletedJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoordinatorViewCompletedJobsPage.aspx");
        }

        protected void lbtnUnassignedJobs_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoordinatorViewUnassignedJobs.aspx");
        }
    }
}
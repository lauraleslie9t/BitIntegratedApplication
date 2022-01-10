using BITWebApplication.BLL;
using BITWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITWebApplication
{
    public partial class ContractorViewAssignedJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorId"] != null)
                {
                    LinkButton viewContJobs = (LinkButton)this.Master.FindControl("lbtnContJobs");
                    viewContJobs.Visible = true;

                    //LinkButton viewContAssiJobs = (LinkButton)this.Master.FindControl("lbtnAssignedJobs");
                    //viewContAssiJobs.Visible = true;

                    LinkButton signInLink = (LinkButton)this.Master.FindControl("lbtnLogIn");
                    signInLink.Text = "Sign Out";
                    RefreshGrid();
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }

        protected void gvAssignedJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvAssignedJobs.Rows[rowIndex];
            int jobNo = Convert.ToInt32(row.Cells[3].Text);
            if (e.CommandName == "Confirm")
            {
                Job job = new Job();
                job.JobNo = jobNo;
                job.ConfirmJob();
                RefreshGrid();

            }
            else if (e.CommandName == "Reject")
            {
                string rejectionComment = ((TextBox)row.FindControl("txtRejectionComent")).Text.Trim();
                int availId = Convert.ToInt32(row.Cells[4].Text);
                Job job = new Job();
                job.JobNo = jobNo;
                job.AvailabilityId = availId;
                job.RejectionComment = rejectionComment;
                if (job.RejectionComment.Length > 0 && job.RejectionComment.Length <= 500)
                {
                    job.RejectJob();
                    RefreshGrid();
                    lblWarning.Text = "";
                }
                else
                {
                    lblWarning.Text = "Job description must be between 0-500 characters.";
                }

            }
        }

        private void RefreshGrid()
        {
            int contractorId = Convert.ToInt32(Session["ContractorId"]);
            ContractorAssignedJobs jobList = new ContractorAssignedJobs();

            gvAssignedJobs.DataSource = jobList.GetAllJobs(contractorId);
            gvAssignedJobs.DataBind();
        }
    }
}
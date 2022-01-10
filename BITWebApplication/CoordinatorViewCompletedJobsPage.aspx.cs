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
    public partial class CoordinatorViewCompletedJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StaffId"] != null)
                {
                    LinkButton coordAssignJobs = (LinkButton)this.Master.FindControl("lbtnAssignJobs");
                    coordAssignJobs.Visible = false;

                    //LinkButton coordCompJobs = (LinkButton)this.Master.FindControl("lbtnCompletedJobs");
                    //coordCompJobs.Visible = true;
                    LinkButton coordUnassignedJobs = (LinkButton)this.Master.FindControl("lbtnUnassignedJobs");
                    coordUnassignedJobs.Visible = true;
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

        public void RefreshGrid()
        {
            int staffId = Convert.ToInt32(Session["StaffId"]);
            AllCompletedJobs jobList = new AllCompletedJobs();


            gvAllCompletedJobs.DataSource = jobList.GetAllJobs();
            gvAllCompletedJobs.DataBind();
        }

        protected void gvAllCompletedJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Complete" | e.CommandName == "Incomplete")
            {
                int rowNo = Convert.ToInt32(e.CommandArgument) - gvAllCompletedJobs.PageIndex*7;
                GridViewRow row = gvAllCompletedJobs.Rows[rowNo];
                Job completedJob = new Job();
                completedJob.JobNo = Convert.ToInt32(row.Cells[2].Text);
                if (e.CommandName == "Complete")
                {

                    completedJob.MarkVerified();
                    RefreshGrid();

                }
                else if (e.CommandName == "Incomplete")
                {
                    completedJob.MarkIncomplete();
                    RefreshGrid();
                }
            }
        }

        protected void gvAllCompletedJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshGrid();
            gvAllCompletedJobs.PageIndex = e.NewPageIndex;
            gvAllCompletedJobs.DataBind();
        }
    }
}
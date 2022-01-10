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
    public partial class ContractorViewJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ContractorId"] != null)
                {
                    //LinkButton viewContJobs = (LinkButton)this.Master.FindControl("lbtnContJobs");
                    //viewContJobs.Visible = false;

                    LinkButton viewContAssiJobs = (LinkButton)this.Master.FindControl("lbtnAssignedJobs");
                    viewContAssiJobs.Visible = true;

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
        private void RefreshGrid()
        {
            int contractorId = Convert.ToInt32(Session["ContractorId"]);
            ContractorConfirmedJobs jobList = new ContractorConfirmedJobs();
            

            gvContractorJobs.DataSource = jobList.GetAllJobs(contractorId);
            gvContractorJobs.DataBind();
        }

        protected void gvContractorJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvContractorJobs.Rows[rowIndex];
                Job selectedJob = new Job();
                string jobNo = row.Cells[3].Text;
                string kmTravelled = ((TextBox)row.FindControl("txtKmTravelled")).Text.Trim();
                string hoursTaken = ((TextBox)row.FindControl("txtHoursTaken")).Text.Trim();
                if (hoursTaken != "" && kmTravelled != "")
                {
                    if (decimal.TryParse(hoursTaken, out decimal myHourstaken) && int.TryParse(kmTravelled, out int myKmTravelled))
                    {
                        selectedJob.JobNo = Convert.ToInt32(jobNo);
                        selectedJob.HoursTaken = myHourstaken;
                        selectedJob.KmTravelled = myKmTravelled;

                        int rowsUpdated = selectedJob.AddJobDetails();
                        if (rowsUpdated > -1)
                        {
                            RefreshGrid();
                            lblWarning.Text = "";
                        }
                    }
                    else
                    {
                        lblWarning.Text = "Please check your data and try again. ";
                    }
                }
                else
                {
                    lblWarning.Text = "Please enter data on both fields.";
                }
            }
        }
    }
}
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
    public partial class ClientAddJobPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ClientId"] != null)
                {
                    //LinkButton addJobLink = (LinkButton)this.Master.FindControl("lbtnAddJobs");
                    //addJobLink.Visible = true;

                    LinkButton viewJobsLink = (LinkButton)this.Master.FindControl("lbtnClientJobs");
                    viewJobsLink.Visible = true;

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
            ClientLocations locations = new ClientLocations();
            int clientId = Convert.ToInt32(Session["ClientId"]);
            gvClientLocations.DataSource = locations.GetClientLocations(clientId);
            gvClientLocations.DataBind();
        }

        protected void gvClientLocations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectLocation")
            {
                if (Session["LocationId"] != null)
                {
                    GridViewRow oldRow = gvClientLocations.Rows[Convert.ToInt32(Session["SelectedRow"])];
                    Button oldBtn = (Button)oldRow.FindControl("btnConfirm");
                    oldBtn.Text = "Select";
                    Session.Remove("LocationId");
                    Session.Remove("SelectedRow");
                }
                int rowIndex = Convert.ToInt32(e.CommandArgument) - gvClientLocations.PageIndex * 3;
                GridViewRow row = gvClientLocations.Rows[rowIndex];
                int locationId = Convert.ToInt32(row.Cells[1].Text);
                Session.Add("LocationId", locationId);
                Session.Add("SelectedRow", rowIndex);

                //change clicked button text to Selected
                Button clickedBtn =  (Button)row.FindControl("btnConfirm");
                clickedBtn.Text = "Selected";
            }
        }

        protected void btnCancelJob_Click(object sender, EventArgs e)
        {
            if (Session["LocationId"] != null)
            {
                Session.Remove("LocationId");
            }
            Response.Redirect("ClientViewJobsPage.aspx");
        }

        protected void btnSaveJob_Click(object sender, EventArgs e)
        {
            Job job = new Job();
            job.RequiredCompDate = calRequiredCompletionDate.SelectedDate;
            job.Priority = ddlPriority.SelectedValue;
            job.Description = txtJobDescription.Text;
            string validationMessage = ValidateForm(job);
            if (validationMessage == "pass")
            {
                job.LocationId = Convert.ToInt32(Session["LocationId"].ToString());
                if(job.AddNewBooking() > -1)
                {
                    Session.Remove("Location");
                    Session.Remove("SelectedRow");
                    Response.Redirect("ClientViewJobsPage.aspx");
                }
                else
                {
                    lblWarning.Text = "Error. Failed to add job to the database.";
                }
            }
            else
            {
                lblWarning.Text = validationMessage;
            }
        }



        private string ValidateForm(Job job)
        {
            if (Session["LocationId"] == null) //works
            {
                return "Please select a job location";
            }
            if (job.RequiredCompDate == Convert.ToDateTime("1/1/0001"))
            {
                return "Please enter a required completion date.";
            }
            if (job.Description == null | job.Description == "")
            {
                return "Please enter a description for your job.";
            }
            if (job.Description.Length > 499)
            {
                return "Description must be under 500 characters.";
            }
            if (job.Priority == "select")
            {
                return "Please select a priority level. ";
            }
            if (job.RequiredCompDate <= DateTime.Today)
            {
                return "Required completion date must be after todays date. ";
            }
            return "pass";
        }

        protected void gvClientLocations_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshGrid();
            gvClientLocations.PageIndex = e.NewPageIndex;
            gvClientLocations.DataBind();
        }
    }
}
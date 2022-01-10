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
    public partial class CoordinatorAssignContractorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["StaffId"] != null)
                {
                    //LinkButton coordAssignJobs = (LinkButton)this.Master.FindControl("lbtnAssignJobs");
                    //coordAssignJobs.Visible = true;

                    LinkButton coordCompJobs = (LinkButton)this.Master.FindControl("lbtnCompletedJobs");
                    coordCompJobs.Visible = true;

                    LinkButton coordUnassignedJobs = (LinkButton)this.Master.FindControl("lbtnCompletedJobs");
                    coordUnassignedJobs.Visible = true;

                    LinkButton signInLink = (LinkButton)this.Master.FindControl("lbtnLogIn");
                    signInLink.Text = "Sign Out";
                    RefreshSkillGrid();
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }

            
        }
        private void RefreshSkillGrid()
        {
            int jobNo = Convert.ToInt32(Session["JobNo"]);
            JobSkills missingSkillList = new JobSkills();
            gvSkills.DataSource = missingSkillList.GetMissingJobSKills(jobNo);
            gvSkills.DataBind();
            JobSkills jobSkillList = new JobSkills();
            gvSelectedSkills.DataSource = jobSkillList.GetJobSKills(jobNo);
            gvSelectedSkills.DataBind();
            
        }
        private void RefreshAvailGrid()
        {
            int jobNo = Convert.ToInt32(Session["JobNo"]);
            DateTime requiredCompDate = Convert.ToDateTime(Session["CompDate"]);
            Availabilities availableSessions = new Availabilities();

            gvAvailableSessions.DataSource = availableSessions.GetAvailabilities(requiredCompDate, jobNo);
            gvAvailableSessions.DataBind();
        }

        protected void gvSkills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "SelectSkill")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSkills.Rows[rowIndex];
                JobSkill selectedSkill = new JobSkill();
                selectedSkill.JobNo = Convert.ToInt32(Session["JobNo"]);
                selectedSkill.SkillName = row.Cells[1].Text;
                selectedSkill.AddJobSkill();
                RefreshSkillGrid();
            }
            
        }

        protected void gvSelectedSkills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "SelectSkill")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSelectedSkills.Rows[rowIndex];
                JobSkill selectedSkill = new JobSkill();
                selectedSkill.JobNo = Convert.ToInt32(Session["JobNo"]);
                selectedSkill.SkillName = row.Cells[1].Text;
                selectedSkill.RemoveJobSkill();
                RefreshSkillGrid();
            }
            
        }

        protected void btnGetSessions_Click(object sender, EventArgs e)
        {

            RefreshAvailGrid();
            if (gvAvailableSessions.Rows.Count == 0)
            {
                string message = "No available sessions.";
                lblSessionWarning.Text = message;
            }
            else
            {
                lblSessionWarning.Text = "";
            }
            
        }

        protected void btnCancelJob_Click(object sender, EventArgs e)
        {
            
            if (Session["AvailabilityId"] != null)
            {
                Session.Remove("AvailabilityId");
            }
            if (Session["JobNo"] != null)
            {
                Session.Remove("JobNo");
            }
            Response.Redirect("CoordinatorViewUnassignedJobs.aspx");
            
        }

        protected void btnSaveJob_Click(object sender, EventArgs e)
        {
          
            Job currentJob = new Job();
            currentJob.JobNo = Convert.ToInt32(Session["JobNo"]);
            currentJob.AvailabilityId = Convert.ToInt32(Session["AvailabilityId"]);
            currentJob.StartTime = ddlPriority.SelectedValue;




            string errorMessage = Validate(currentJob);
            if (errorMessage == "Validated")
            {
                currentJob.ExpHours = Convert.ToInt32(txtExpHours.Text);
                if (currentJob.SaveContractorToJob() > -1)
                {
                    Response.Redirect("CoordinatorViewUnassignedJobs.aspx");
                }
                else
                {
                    lblSaveWarning.Text = "Unable to save job.";
                }
            }
            else
            {
                lblSaveWarning.Text = errorMessage;
            }
            

        }

        protected void gvAvailableSessions_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "SelectSession")
            {
                if (Session["AvailabilityId"] != null)
                {

                    GridViewRow oldRow = gvAvailableSessions.Rows[Convert.ToInt32(Session["SelectedRow"])];
                    Button oldBtn = (Button)oldRow.FindControl("btnConfirm");
                    oldBtn.Text = "Select";
                    Session.Remove("AvailabilityId");
                    Session.Remove("SelectedRow");
                    Session.Remove("ItemPage");
                }
                int itemPage = gvAvailableSessions.PageIndex;
                int rowIndex = Convert.ToInt32(e.CommandArgument) - gvAvailableSessions.PageIndex*5;

                GridViewRow row = gvAvailableSessions.Rows[rowIndex];
                int availId = Convert.ToInt32(row.Cells[1].Text);
                Session.Add("AvailabilityId", availId);
                Session.Add("SelectedRow", rowIndex);
                Session.Add("ItemPage", itemPage);

                //change clicked button text to Selected
                Button clickedBtn = (Button)row.FindControl("btnConfirm");
                clickedBtn.Text = "Selected";
            }
            
        }
        private string Validate(Job job)
        {
            if (Session["AvailabilityId"] == null)
            {
                return "Please select an availability slot";
            }
            if (!Int32.TryParse(txtExpHours.Text, out int num) )
            {
                return "Please enter a number for expected hours.";
            }
            if (job.StartTime == "Select")
            {
                return "Please enter an expected start time. ";
            }
            return "Validated"; 
        }

        protected void gvAvailableSessions_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshAvailGrid();
            gvAvailableSessions.PageIndex = e.NewPageIndex;
            //if (Convert.ToInt32(Session["ItemPage"]) == e.NewPageIndex)
            //{
            //    GridViewRow row = gvAvailableSessions.Rows[Convert.ToInt32(Session["SelectedRow"])];
            //    Button clickedBtn = (Button)row.FindControl("btnConfirm");
            //    clickedBtn.Text = "Selected";
            //}
            gvAvailableSessions.DataBind();
        }
    }
}
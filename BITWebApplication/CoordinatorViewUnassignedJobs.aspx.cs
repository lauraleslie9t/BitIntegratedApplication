using BITWebApplication.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITWebApplication
{
    public partial class CoordinatorViewUnassignedJobs : System.Web.UI.Page
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

                    //LinkButton coordUnassignedJobs = (LinkButton)this.Master.FindControl("lbtnCompletedJobs");
                    //coordUnassignedJobs.Visible = true;

                    LinkButton signInLink = (LinkButton)this.Master.FindControl("lbtnLogIn");
                    signInLink.Text = "Sign Out";
                    RefreshGrids();
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
            }
        }

        private void RefreshGrids()
        {
            UnassignedJobs unassignedJobs = new UnassignedJobs();
            gvUnassignedJobs.DataSource = unassignedJobs.GetAllJobs();
            gvUnassignedJobs.DataBind();
            RejectedJobs rejectedJobs = new RejectedJobs();
            gvRejectedJobs.DataSource = rejectedJobs.GetAllJobs();
            gvRejectedJobs.DataBind();
        }

        protected void gvUnassignedJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if(Session["JobNo"]!= null)
                {
                    Session.Remove("JobNo");
                }
                if(Session["CompDate"] != null)
                {
                    Session.Remove("CompDate");
                }
                int rowIndex = Convert.ToInt32(e.CommandArgument) - gvUnassignedJobs.PageIndex*5;
                GridViewRow row = gvUnassignedJobs.Rows[rowIndex];
                int jobNo = Convert.ToInt32(row.Cells[1].Text);
                Session.Add("JobNo", jobNo);
                DateTime compDate = Convert.ToDateTime(row.Cells[8].Text);
                Session.Add("CompDate", compDate);
                Response.Redirect("CoordinatorAssignContractorPage.aspx");

            }
        }

        protected void gvRejectedJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (Session["JobNo"] != null)
            {
                Session.Remove("JobNo");
            }
            if (Session["CompDate"] != null)
            {
                Session.Remove("CompDate");
            }
            int rowIndex = Convert.ToInt32(e.CommandArgument) - gvRejectedJobs.PageIndex*5;
            GridViewRow row = gvRejectedJobs.Rows[rowIndex];
            int jobNo = Convert.ToInt32(row.Cells[1].Text);
            Session.Add("JobNo", jobNo);
            DateTime compDate = Convert.ToDateTime(row.Cells[8].Text);
            Session.Add("CompDate", compDate);
            Response.Redirect("CoordinatorAssignContractorPage.aspx");
            }
        }

        protected void gvRejectedJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshGrids();
            gvRejectedJobs.PageIndex = e.NewPageIndex;
            gvRejectedJobs.DataBind();
            
        }

        protected void gvUnassignedJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshGrids();
            gvUnassignedJobs.PageIndex = e.NewPageIndex;
            gvUnassignedJobs.DataBind();

        }
    }
}
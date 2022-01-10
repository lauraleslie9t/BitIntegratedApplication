using BITWebApplication.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BITWebApplication
{
    public partial class ClientViewJobsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["ClientId"] != null)
                {
                    LinkButton addJobLink = (LinkButton)this.Master.FindControl("lbtnAddJobs");
                    addJobLink.Visible = true;

                    //LinkButton viewJobsLink = (LinkButton)this.Master.FindControl("lbtnClientJobs");
                    //viewJobsLink.Visible = true;

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

            int clientId = Convert.ToInt32(Session["ClientId"]);
            AllClientJobs jobList = new AllClientJobs();

            gvClientJobs.DataSource = jobList.GetAllClientJobs(clientId);
            gvClientJobs.DataBind();
        }

        protected void gvClientJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            RefreshGrid();
            gvClientJobs.PageIndex = e.NewPageIndex;
            gvClientJobs.DataBind();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudComputing
{
    public partial class RouteOptimizer : Page
    {
        public Request request;
        public static List<Request> requests;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["RouteOptimizer"] == null)
            {
                Session["Check_Page_Refresh"] = DateTime.Now.ToString();
                requests = new List<Request>();
                BindGridViewData();
                Session["RouteOptimizer"] = "RouteOptimizer";
            }
            else if (!IsPostBack && Session["RouteOptimizer"] != null)
            {
                Session["state"] = requests;
                GridView1.DataSource = requests;
                GridView1.DataBind();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["Check_Page_Refresh"] = Session["Check_Page_Refresh"];
        }


        private void BindGridViewData()
        {
            request = null;
            requests.Add(null);

            Session["state"] = requests;

            GridView1.DataSource = requests;
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
            }
        }


        public void Insert(object sender, EventArgs e)
        {
            if (ViewState["Check_Page_Refresh"].ToString() == Session["Check_Page_Refresh"].ToString())
            {
                int requestId = 0;
                requests = new List<Request>();
                requests = (List<Request>)Session["state"];
                string address = ((TextBox)GridView1.FooterRow.FindControl("txtAddress")).Text;
                if (!string.IsNullOrEmpty(address))
                {
                    if (requests[requests.Count - 1] != null)
                    {
                        requestId = requests.Count() + 1;
                    }
                    else
                    {
                        requests = new List<Request>();
                        requestId = 1;
                    }
                    request = new Request(requestId, address);
                    requests.Add(request);

                }

                Session["state"] = requests;

                Session["Check_Page_Refresh"] = DateTime.Now.ToString();

                GridView1.DataSource = requests;
                GridView1.DataBind();

            }
            else
            {
                requests = (List<Request>)Session["state"];
                GridView1.DataSource = requests;
                GridView1.DataBind();
                Response.Write("Page Refresh Detected....");
            }
        }

        protected void CalculateShortestPath(object sender, EventArgs e)
        {
            for (int i = 0; i < requests.Count; i++)
            {
                Address.Instance.AddAddress(requests[i].address);
            }
            Address.Instance.PopulateGraph();
            
        }
    }
}
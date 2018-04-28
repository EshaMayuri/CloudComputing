using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CloudComputing
{
    public partial class OptmizedRoute : System.Web.UI.Page
    {
        public static List<Data> data;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["RouteOptimizer"] == null)
            {
                Session["Check_Page_Refresh"] = DateTime.Now.ToString();
                if (Address.Instance.SortedAddresses.Count != 0)
                {
                    BindGridViewData();
                }
                Session["RouteOptimizer"] = "RouteOptimizer";
            }
            else if (!IsPostBack && Session["RouteOptimizer"] != null)
            {
                
                if (Address.Instance.SortedAddresses.Count != 0)
                {
                    BindGridViewData();
                }
                Session["state"] = data;
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            ViewState["Check_Page_Refresh"] = Session["Check_Page_Refresh"];
        }

        // need to check for duplicacies
        private void BindGridViewData()
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            data = new List<Data>();
            foreach(KeyValuePair<int, string> pair in Address.Instance.SortedAddresses)
            {
                data.Add(new Data(pair.Key, pair.Value));
            }
            Session["state"] = data;

            GridView2.DataSource = data;
            GridView2.DataBind();
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

        protected void BtnShowMaps_Click(object sender, EventArgs e)
        {
            Response.Redirect("Route.aspx");
        }
    }


    public partial class Data
    {
        //Class variable
        //Dictionary<string, List<>> graph;
        //Dictionary<string, List<int>> vehicleByType;
        //Dictionary<string, int> result = new Dictionary<string, int>();
        public int id;
        public string address;

        /// <summary>
        /// Getter and Setter for Request Id
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        /// <summary>
        /// Getter and Setter for Zip Code
        /// </summary>
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public Data(int id, string address)
        {
            try
            {

                this.id = id;
                this.address = address;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
            }

        }
    }
}
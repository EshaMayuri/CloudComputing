using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using System.Xml;

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
            if (requests.Count > 0)
            {
                for (int i = 0; i < requests.Count; i++)
                {
                    Address.Instance.AddAddress(requests[i].address);
                }
                int noOfPointsToProcess = 0;
                if (!txtCount.Text.ToString().Equals(""))
                {
                    noOfPointsToProcess = Int32.Parse(txtCount.Text.ToString());
                }
                Address.Instance.PopulateGraph(noOfPointsToProcess);
                Response.Redirect("OptmizedRoute.aspx");
            }
            else
            {
                string myStringVariable = "Please add the addresses!";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + myStringVariable + "');", true);
            }
        }

        protected void BtnLoadData_Click(object sender, EventArgs e)
        {
            if (FileUpload.HasFile)
            {
                string fileExtension = System.IO.Path.GetExtension(FileUpload.FileName);
                if (fileExtension.ToLower() != ".xml")
                {
                    lblMessage.Text = "Only files with .xml extension are allowed";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    int fileSize = FileUpload.PostedFile.ContentLength;
                    if (fileSize > 2097152)
                    {
                        lblMessage.Text = "Maximum file size(2MB) exceeded";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        string path = @"/Data/"+FileUpload.FileName;
                        FileUpload.SaveAs(Server.MapPath(path));
                        lblMessage.Text = "Data uploaded";
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        PopulateData(FileUpload.FileName);
                    }
                }
            }
            else
            {
                lblMessage.Text = "Please select a file to upload";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void PopulateData(string fileName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlNodeList;
            string path = AppDomain.CurrentDomain.BaseDirectory +"Data\\" + fileName;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            xmlDoc.Load(fs);
            xmlNodeList = xmlDoc.GetElementsByTagName("Addresses");
            for (int i = 0; i < xmlNodeList.Count; i++)
            {
                xmlNodeList[i].ChildNodes.Item(0).InnerText.Trim();
                string address = xmlNodeList[i].ChildNodes.Item(1).InnerText.Trim().ToString();
                InsertData(address);
            }
            fs.Close();
        }

        private void InsertData(string address)
        {
            int requestId = 0;
            requests = new List<Request>();
            requests = (List<Request>)Session["state"];
            //string address = ((TextBox)GridView1.FooterRow.FindControl("txtAddress")).Text;
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
    }
}
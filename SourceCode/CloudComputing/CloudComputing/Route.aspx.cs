using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace CloudComputing
{
    public partial class Route : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["RouteOptimizer"] == null)
            {
                Session["Check_Page_Refresh"] = DateTime.Now.ToString();

                Session["RouteOptimizer"] = "RouteOptimizer";
            }
            else if (!IsPostBack && Session["RouteOptimizer"] != null)
            {

            }
        }

        protected void BtnShowMaps_Click(object sender, EventArgs e)
        {
            if (Address.Instance.SortedAddresses.Count() != 0)
            {
                List<Maps> list = new List<Maps>();
                string latlng = "";
                string[] address;

                address = new string[Address.Instance.SortedAddresses.Count()];
                int n = 0;
                foreach (KeyValuePair<int, string> pair in Address.Instance.SortedAddresses)
                {
                    address[n] = pair.Value;
                    n = n + 1;
                }

                for (int i = 0; i < address.Length; i++)
                {
                    latlng = GetLatLng(address[i]);
                    if (latlng != "")
                    {
                        Maps map = new Maps((i + 1).ToString(), latlng.Split(' ')[0], latlng.Split(' ')[1], address[i]);
                        list.Add(map);
                    }
                }
                /*Maps maps;

                maps = new Maps("1", "39.015970", "-94.673924", "Home");
                list.Add(maps);

                maps = new Maps("2", "39.015898", "-94.740834", "Huhot Shawnee");
                list.Add(maps);

                maps = new Maps("3", "39.035039", "-94.576526", "Miller Nichols Library");
                list.Add(maps);

                maps = new Maps("4", "39.015970", "-94.673924", "Home");
                list.Add(maps);*/

                rptMarkers.DataSource = list;
                rptMarkers.DataBind();
            }
        }

        public string GetLatLng(string address)
        {
            string lat = "";
            string lng = "";
            string url = "https://maps.googleapis.com/maps/api/geocode/xml?address=" + address + "&sensor=false&key=AIzaSyBYF1u3tTYq6RzlH0Gh75MIzv8w6WhHl5A";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)httpRequest.GetResponse();
            var receiveStream = response.GetResponseStream();
            XmlDocument mySourceDoc = new XmlDocument();
            mySourceDoc.Load(receiveStream);
            lat = mySourceDoc.GetElementsByTagName("lat").Item(0).InnerText;
            lng = mySourceDoc.GetElementsByTagName("lng").Item(0).InnerText;
            receiveStream.Close();
            return lat + " " + lng;
        }
    }
    public class Maps
    {
        public string id;
        public string lat;
        public string lng;
        public string addr;
        public Maps(string id, string lat, string lng, string addr)
        {
            this.id = id;
            this.lat = lat;
            this.lng = lng;
            this.addr = addr;
        }

        /// <summary>
        /// Setter and getter for Id
        /// </summary>
        public string Id
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
        /// Setter and getter for Lat
        /// </summary>
        public string Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
            }
        }

        /// <summary>
        /// Setter and getter for Lng
        /// </summary>
        public string Lng
        {
            get
            {
                return lng;
            }
            set
            {
                lng = value;
            }
        }

        /// <summary>
        /// Setter and getter for Address
        /// </summary>
        public string Addr
        {
            get
            {
                return addr;
            }
            set
            {
                addr = value;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace CloudComputing
{
    public class Address
    {
        public string addressData;
        public string lat;
        public string lng;

        //Addresses hashmap with unique address Id and pysical address
        Dictionary<int, string> addresses = new Dictionary<int, string>();
        public Dictionary<int, string> SortedAddresses = new Dictionary<int, string>();

        //Distance between all the available addresses based on address Id from the addresses hashmap
        double[,] distanceMatrix;

        public string AddressData
        {
            get { return addressData; }
            set { addressData = value; }
        }

        public string Lat
        {
            get { return lat; }
            set { lat = value; }
        }

        public string Lng
        {
            get { return lng; }
            set { lng = value; }
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        private Address()
        {
        }

        private static Address instance = null;

        /// <summary>
        /// Singleton class instance
        /// </summary>
        public static Address Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Address();
                }
                return instance;
            }
        }

        /// <summary>
        /// Adds address to addresses hashmap
        /// </summary>
        /// <param name="address"></param>
        public void AddAddress(string address)
        {
            this.addressData = address;
            int count = addresses.Count();
            addresses.Add(count, address);
        }

        /// <summary>
        /// Populates the distance matrix
        /// </summary>
        public void PopulateGraph()
        {
            int rows = addresses.Count();
            int columns = addresses.Count();
            distanceMatrix = new double[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == j)
                    {
                        distanceMatrix[i, j] = 0;
                    }
                    if (i != j & distanceMatrix[i,j] == 0 && distanceMatrix[j,i] == 0)
                    {
                        distanceMatrix[i, j] = FetchDistance(addresses[i].ToString(), addresses[j].ToString());
                        distanceMatrix[j, i] = distanceMatrix[i,j];
                    }
                }
            }
            if (SortedAddresses != addresses)
            {
                SortedAddresses = addresses;
            }
        }
        /// <summary>
        /// Fetches the distance between two addresses
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public double FetchDistance(string origin, string destination)
        {
            string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&mode=driving&sensor=false&language=en-EN&units=imperial&api_key=AIzaSyDfWaD6yDIuntnmDfllvVhqOikJotTPCi4";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            XmlDocument xmldoc = new XmlDocument();
            Double dist = 0.0;
            xmldoc.LoadXml(responsereader);
            if (xmldoc.GetElementsByTagName("status")[0].ChildNodes[0].InnerText == "OK")
            {
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                if (distance[0].ChildNodes[1].InnerText.Contains("km"))
                {
                    dist = Convert.ToDouble(distance[0].ChildNodes[1].InnerText.Replace(" km", ""));
                    dist = dist * 0.621371192;
                }
                else if(distance[0].ChildNodes[1].InnerText.Contains("mi"))
                {
                    dist = Convert.ToDouble(distance[0].ChildNodes[1].InnerText.Replace(" mi", ""));
                }
            }
            return dist;
        }
        public void FetchLatLng(string addressData)
        {
            string latitude = "";
            string longitude = "";
            string urlAddress = "http://maps.googleapis.com/maps/api/geocode/xml?address=" + addressData + "&sensor=false";
            try
            {
                XmlDocument objXmlDocument = new XmlDocument();
                objXmlDocument.Load(urlAddress);
                XmlNodeList objXmlNodeList = objXmlDocument.SelectNodes("/GeocodeResponse/result/geometry/location");
                foreach (XmlNode objXmlNode in objXmlNodeList)
                {
                    // GET LONGITUDE 
                    latitude = objXmlNode.ChildNodes.Item(0).InnerText;

                    // GET LATITUDE 
                    longitude = objXmlNode.ChildNodes.Item(1).InnerText;
                }
            }
            catch
            {
                // Process an error action here if needed  
            }
            
        }
    }
}
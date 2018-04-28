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
        public void PopulateGraph(int count)
        {
            
            if(count == 0)
            {
                ComputeShortestPath(addresses);
            }
            else
            {
                SortedSet<Distance> minHeap = new SortedSet<Distance>(new CustomComparer());
                Dictionary<int, string> addressList = new Dictionary<int, string>();
                for (int i = 1; i < addresses.Count; i++)
                {
                    minHeap.Add(new Distance(Convert.ToInt32(FetchDistance(addresses[0].ToString(), addresses[i].ToString())), addresses[i]));
                }
                for (int j = 0; j < count; j++)
                {
                    if (j == 0)
                    {
                        addressList.Add(0, addresses[0]);
                    }
                    else
                    {
                        addressList.Add(j, minHeap.ElementAt(j).address1);
                    }
                }
                ComputeShortestPath(addressList);
            }

        }

        public void ComputeShortestPath(Dictionary<int, string> addressList)
        {
            List<Int32> output = new List<int>();
            int rows = addressList.Count();
            int columns = addressList.Count();
            distanceMatrix = new double[rows, columns];


            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (i == j)
                    {
                        distanceMatrix[i, j] = 0;
                    }
                    if (i != j & distanceMatrix[i, j] == 0 && distanceMatrix[j, i] == 0)
                    {
                        distanceMatrix[i, j] = FetchDistance(addressList[i].ToString(), addressList[j].ToString());
                        distanceMatrix[j, i] = distanceMatrix[i, j];
                    }
                }
            }

            if (SortedAddresses != addressList)
            {
                //SortedAddresses = addressList;
                TSP tsp = new TSP();
                tsp.MinCost(distanceMatrix, output);
                for (int index = 0; index < output.Count; index++)
                {
                    if (index != output.Count - 1)
                    {
                        SortedAddresses.Add(index, addressList[output[index]]);
                    }
                    else
                    {
                        SortedAddresses.Add(index, addressList[0]);
                    }
                }
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
            string url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&mode=driving&sensor=false&language=en-EN&units=imperial&key=AIzaSyBYF1u3tTYq6RzlH0Gh75MIzv8w6WhHl5A";
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

    public class Distance
    {
        public int dist;
        public string address1;
        public string address2;

        public int Dist { get; set; }
        public int Address1 { get; set; }
        public string Address2 { get; set; }

        public Distance(int dist, string address1)
        {
            this.dist = dist;
            this.address1 = address1;
            //this.address2 = address2;
        }
    }

    public class CustomComparer : IComparer<Distance>
    {

        public int Compare(Distance x, Distance y)
        {
            if ((x.dist - y.dist) == 0)
            {
                return x.address1.CompareTo(y.address1);
            }
            return x.dist - y.dist;
        }
    }
}
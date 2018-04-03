using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudComputing
{
    public partial class Request
    {
        //Class variable
        //Dictionary<string, List<>> graph;
        //Dictionary<string, List<int>> vehicleByType;
        //Dictionary<string, int> result = new Dictionary<string, int>();
        public int requestId;
        public string address;

        /// <summary>
        /// Getter and Setter for Request Id
        /// </summary>
        public int RequestId
        {
            get
            {
                return requestId;
            }
            set
            {
                requestId = value;
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

        public Request(int requestId, string address)
        {
            try
            {

                this.requestId = requestId;
                this.address = address;
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
            }

        }
    }
}
using System.Collections.Generic;

namespace XHConfig
{
    public class Address
    {
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
    }

    public class Link
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public partial class JsonTest
    {
        public string name { get; set; }
        public string url { get; set; }
        public int page { get; set; }
        public bool isNonProfit { get; set; }
        public Address address { get; set; }
        public List<Link> links { get; set; }


    }


}


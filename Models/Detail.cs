using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jQueryAjaxInAsp.NETMVC.Models
{
    public class Detail
    {
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string Unit { get; set; }
        public string Quantity { get; set; }
        public string GoodReturnQuantity { get; set; }
        public string BadReturnQuantity { get; set; }
        public string Description { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using jQueryAjaxInAsp.NETMVC.Attribute;
using jQueryAjaxInAsp.NETMVC.Utility;

namespace jQueryAjaxInAsp.NETMVC.Models
{
    public class ConsumableOutModel
    {

        public string OrderId { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string DateCreate { get; set; }
        public string Applicant { get; set; }
        public string ApplicantCode { get; set; }
        public List<Detail> Detail { get; set; }
    }
}
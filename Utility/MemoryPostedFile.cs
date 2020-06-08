using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace jQueryAjaxInAsp.NETMVC.Utility
{
    public class MemoryPostedFile : HttpPostedFileBase
    {
        private string FilePath;

        public MemoryPostedFile()
        {
            this.FilePath = "~/AppFiles/Images/";
            this._FileName = "default.png";
        }

        public override String FileName { get { return _FileName; } }
        private String _FileName;
       
        public override void SaveAs(string filename)
        {
            System.IO.File.WriteAllBytes(filename, System.IO.File.ReadAllBytes(FilePath));
        }
    }
}
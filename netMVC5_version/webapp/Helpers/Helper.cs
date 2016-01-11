using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Helpers
{
    public class Helper
    {
        public static string ImgHtml(string v)
        {
            return string.IsNullOrWhiteSpace(v) ? "" : @"<img src='\uploads\images\" + v + "'style='height:120px;width:120px;' />";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAdminMvc.Helpers
{
    public class Helper
    {

        public static string movePhoto(string name, string to)
        {
            if (String.IsNullOrEmpty(name)) return null;

            string nameId = name + DateTime.Now.Millisecond + ".jpg";
            string toDir = (new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName) + "\\img\\" + to;
            string path = AppDomain.CurrentDomain.BaseDirectory + "uploads\\images\\" + name;

            System.IO.Directory.CreateDirectory(toDir);

            System.IO.File.Move(path, toDir + "\\" + nameId);

            return "http://app.crl.pe/img/" + to + "/" + nameId;
        }
    }
}
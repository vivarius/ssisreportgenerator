using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceReference
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        public int Add(int param1, int param2)
        {
            return param1 + param2;
        }

        [WebMethod]
        public DataSet GetNameObject(DataSet blabla)
        {
            blabla.Tables.Add("ZSD");
            return blabla;
        }

        [WebMethod]
        public ArrayList GetArray(ArrayList blabla)
        {
            blabla.Add("ZSD");
            return blabla;
        }

        [WebMethod]
        public int BlaBla(ArrayList blabla, string Test)
        {
            blabla.Add("ZSD");
            return 2;
        }

        [WebMethod]
        public void JustDoIt(string Test)
        {
            
        }
    }
}

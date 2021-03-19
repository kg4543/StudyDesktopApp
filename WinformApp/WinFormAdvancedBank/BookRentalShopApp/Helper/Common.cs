using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookRentalShopApp.Helper
{
    public static class Common
    {
        public static string ConnString = "Data Source=127.0.0.1;" +
                                          "Initial Catalog=bookrentalshop;" +
                                          "Persist Security Info=True;" +
                                          "User ID = sa; PassWord=mssql_p@ssw0rd!";
        public static string LoginUserId = string.Empty;

        /// <summary>
        /// Ip주소 받아오기
        /// </summary>
        /// <returns></returns>
        internal static string GetLocalIp()
        {
            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach  (IPAddress ip in host.AddressList)
            {
                localIP = ip.ToString();
                break;
            }
            return localIP;
        }

        internal static string ReplaceCmdText(string strSource)
        {
            var result = strSource.Replace("'","＇");
            result = result.Replace("--","");
            result = result.Replace(";","");

            return result;
        }
    }
}

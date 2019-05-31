using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class TheCardsbranch : S_TheCardsList
    {
        //转换时间格式
        private string releaseTimeStr;
        public string ReleaseTimeStr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStr = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStr = value;
                }
            }
            get
            {
                return releaseTimeStr;
            }
        }

        private string releaseTimeStrt;
        public string ReleaseTimeStrr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrt = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrt = value;
                }
            }
            get
            {
                return releaseTimeStrt;
            }
        }

        public string HostContractNumber { get; set; }
        public string MethodOfSettlingAccounts { get; set; }
        public string MethodOfSettlingAccountsID { get; set; }
        public string OrderFormPactID { get; set; }
        public string SupplierName { get; set; }
        public string TheCardsID { get; set; }
        public new string Kahao { get; set; }
        public new string TheCardsNumber { get; set; }
    }
}
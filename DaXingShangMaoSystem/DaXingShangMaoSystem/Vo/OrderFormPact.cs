using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class OrderFormPact: B_OrderFormPactList
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

        //查询合同基本信息
        public string SupplierName { get; set; }
        public string AdjustAccountsFashion { get; set; }
        public string SpouseBRanchName { get; set; }
        public string AgreementCode { get; set; }
        public string AgreementTypeName { get; set; }

        //商品设置合同
        public int OrderFormPactIDs { get; set; }

        //自营合同经营商品定义
        public string MethodOfSettlingAccounts { get; set; }
    }
}
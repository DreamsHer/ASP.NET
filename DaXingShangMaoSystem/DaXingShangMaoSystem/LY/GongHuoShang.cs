using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.LY
{
    public class GongHuoShang : B_SupplierList
    {
        public string Remember { get; set; }

        public string ContractNumber { get; set; }

        public string MethodOfSettlingAccounts { get; set; }

        public string StaffCode { get; set; }

        public string StaffName { get; set; }

        public string QuFenLeiXingMC { get; set; }

        public int QuFenLeiXingID { get; set; }


        public string ExamineNot { get; set; }

        public string Status { get; set; }

        public string CrushRedNot { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        private string registerTime;
        public string RegisterTime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    registerTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    registerTime = value;
                }
            }
            get
            {
                return registerTime;
            }
        }

    }
}
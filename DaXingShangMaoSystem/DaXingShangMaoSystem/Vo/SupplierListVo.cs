using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class SupplierListVo  : B_SupplierList
    {
        public string UnitCharacterName { get; set; }
        public string PaymentMC { get; set; }
        public string CompanyMC { get; set; }

        public string SupplierRankName { get; set; }
        public string AgreementState { get; set; }
        public string AgreementTypeName { get; set; }
        public string EnterpriseNatureName { get; set; }
        public string EnterpriseTypeName { get; set; }


        //时间转化
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
        /// <summary>
        /// 合同有效期始
        /// </summary>
        private string releaseTimeStrr;
        public string ReleaseTimeStrr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrr = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrr = value;
                }
            }
            get
            {
                return releaseTimeStrr;
            }
        }
        /// <summary>
        /// 合同有效期终
        /// </summary>
        private string releaseTimeStrrr;
        public string ReleaseTimeStrrr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrrr = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrrr = value;
                }
            }
            get
            {
                return releaseTimeStrrr;
            }
        }
    }
}
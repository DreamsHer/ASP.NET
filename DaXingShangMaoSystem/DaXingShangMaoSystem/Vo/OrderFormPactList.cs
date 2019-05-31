using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class OrderFormPactList : B_OrderFormPactList
    {
        public string SpouseBRanchName { get; set; }
        public string AgreementTypeName { get; set; }
        public string MethodOfSettlingAccounts { get; set; }

        public string AgreementManagerName { get; set; }

        public string AgreementManagerZong { get; set; }
        public string AgreementManagerGaoJi { get; set; }
        public string AgreementState { get; set; }
        public string SupplierNumber { get; set; }

        public string GoodsChopName { get; set; }
        public string GoodsGenreName { get; set; }
        /// <summary>
        /// 
        /// </summary>
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
        /// <summary>
        /// 签订时间
        /// </summary>
        private string releaseTimeqiandingtime;
        public string ReleaseTimeqiandingtime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeqiandingtime = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeqiandingtime = value;
                }
            }
            get
            {
                return releaseTimeqiandingtime;
            }
        }
        /// <summary>
        /// 进场时间
        /// </summary>
        private string releaseTimejinchangtime;
        public string ReleaseTimejinchangtime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimejinchangtime = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimejinchangtime = value;
                }
            }
            get
            {
                return releaseTimejinchangtime;
            }
        }
        /// <summary>
        /// 退场时间
        /// </summary>
        private string releaseTimetuichangchangtime;
        public string ReleaseTimetuichangchangtime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimetuichangchangtime = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimetuichangchangtime = value;
                }
            }
            get
            {
                return releaseTimetuichangchangtime;
            }
        }
        /// <summary>
        /// 票期天数
        /// </summary>
        private string releaseTimepiaoqitime;
        public string ReleaseTimepiaoqitime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimepiaoqitime = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimepiaoqitime = value;
                }
            }
            get
            {
                return releaseTimepiaoqitime;
            }
        }

        /// <summary>
        /// 账期天数
        /// </summary>
        private string releaseTimezhangqitime;
        public string ReleaseTimezhangqitime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimezhangqitime = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimezhangqitime = value;
                }
            }
            get
            {
                return releaseTimezhangqitime;
            }
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        private string registranttimea;
        public string Registranttimea
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    registranttimea = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    registranttimea = value;
                }
            }
            get
            {
                return registranttimea;
            }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        private string registrantAuditortime;
        public string RegistrantAuditortime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    registrantAuditortime = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    registrantAuditortime = value;
                }
            }
            get
            {
                return registrantAuditortime;
            }
        }
        /// <summary>
        /// 注销时间
        /// </summary>
        private string registrantExecutortime;
        public string RegistrantExecutortime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    registrantExecutortime = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    registrantExecutortime = value;
                }
            }
            get
            {
                return registrantExecutortime;
            }
        }
        /// <summary>
        /// 计划启动时间
        /// </summary>
        private string registrantPlanFiringData;
        public string RegistrantPlanFiringData
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    registrantPlanFiringData = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    registrantPlanFiringData = value;
                }
            }
            get
            {
                return registrantPlanFiringData;
            }
        }
        /// <summary>
        /// 计划启动时间
        /// </summary>
        private string registrantFiringdata;
        public string RegistrantFiringdata
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    registrantFiringdata = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    registrantFiringdata = value;
                }
            }
            get
            {
                return registrantFiringdata;
            }
        }
    }
}
using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.LY
{
    public class SelectOrderPact : B_OrderFormPactList
    {

        /// <summary>
        /// 进仓ID
        /// </summary>
        public Nullable<int> WareHouseID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public Nullable<int> DefaulGoodsID { get; set; }
        /// <summary>
        /// 合同类型
        /// </summary>
        public string AgreementTypeName { get; set; }


        /// <summary>
        /// 进货部门ID
        /// </summary>
        //public Nullable<int> SpouseBRanchID { get; set; }


        /// <summary>
        /// 合同ID
        /// </summary>
        public Nullable<int> OrderFormPactIDs { get; set; }
        /// <summary>
        /// 进货部门ID
        /// </summary>
        public Nullable<int> StockPlaceID { get; set; }


        /// <summary>
        /// 销售部门ID
        /// </summary>
        public Nullable<int> CountersellID { get; set; }

        /// <summary>
        ///进货部门
        /// </summary>
        public string SpouseBRanchName { get; set; }
        /// <summary>
        ///库存地点
        /// </summary>
        public string StockPlaceName { get; set; }

        /// <summary>
        ///附件
        /// </summary>
        public string Appendix { get; set; }
        /// <summary>
        ///原始单号
        /// </summary>
        public string Evidences { get; set; }


        /// <summary>
        ///员工编号
        /// </summary>
        public string StaffCode { get; set; }
        /// <summary>
        ///员工姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        ///登记人姓名
        /// </summary>
        public string RegisterName { get; set; }
        /// <summary>
        ///进仓编码
        /// </summary>
        public string Remember { get; set; }
        /// <summary>
        ///结算方式
        /// </summary>
        public string MethodOfSettlingAccounts { get; set; }

        /// <summary>
        /// 供货商名称
        /// </summary>
        public string SupplierCHName { get; set; }

        /// <summary>
        ///审核人
        /// </summary>
        public string ExamineName { get; set; }



        /// <summary>
        /// 进仓状态（审核）
        /// </summary>
        public Nullable<bool> ExamineNot { get; set; }

        /// <summary>
        /// 进仓状态（生效否）
        /// </summary>
        public Nullable<bool> Status { get; set; }

        /// <summary>
        /// 合同起始时间
        /// </summary>
        private string contractValidRised;
        public string ContractValidRised
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    contractValidRised = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    contractValidRised = value;
                }
            }
            get
            {
                return contractValidRised;
            }
        }


        /// <summary>
        /// 合同终止始时间
        /// </summary>
        private string contractValidBegins;
        public string ContractValidBegins
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    contractValidBegins = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    contractValidBegins = value;
                }
            }
            get
            {
                return contractValidBegins;
            }
        }

        /// <summary>
        /// 签订时间
        /// </summary>
        private string conclusionTime;
        public string ConclusionTimes
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    conclusionTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    conclusionTime = value;
                }
            }
            get
            {
                return conclusionTime;
            }
        }

        /// <summary>
        /// 签订时间
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


        /// <summary>
        /// 审核时间
        /// </summary>
        private string examineTime;
        public string ExamineTime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    examineTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    examineTime = value;
                }
            }
            get
            {
                return examineTime;
            }
        }


        /// <summary>
        ///进货部门
        /// </summary>
        public string PurchaseSectionName { get; set; }
        /// <summary>
        ///销售部门
        /// </summary>
        public string CountersellName { get; set; }

        public int GoodsBrandID { get; set; }
        public string GoodsBrandName { get; set; }
        public string GoodsBrandZhuPinPai { get; set; }
        public string GoodsGenreName { get; set; }

    }
}
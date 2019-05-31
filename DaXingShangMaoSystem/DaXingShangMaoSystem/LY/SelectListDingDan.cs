using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.LY
{
    public class SelectListDingDan : B_OrderFormList
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public Nullable<int> OrdernFromIDs { get; set; }


        /// <summary>
        /// 订单明细ID
        /// </summary>
        public Nullable<int> OrderFormDetailIDs { get; set; }

        /// <summary>
        /// 商品明细ID
        /// </summary>
        public Nullable<int> GoodsDetailID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Nullable<int> GoodsIDs { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// 供货编号
        /// </summary>
        public string SupplierNumber { get; set; }
        
        /// <summary>
        /// 结算方式
        /// </summary>
        public string MethodOfSettlingAccounts { get; set; }

        /// <summary>
        /// 进货部门
        /// </summary>
        public string SpouseBRanchName { get; set; }

        /// <summary>
        /// 供货商名称
        /// </summary>
        public string SupplierCHName { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        public string OrderFormTypeName { get; set; }

        /// <summary>
        /// 商品代码
        /// </summary>
        public string GoodsCodes { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string GoodsTiaoMas { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsNames { get; set; }

        /// <summary>
        /// 商品货号
        /// </summary>
        public string ArtNos { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string SpecificationsModels { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string EstimateUnitNames { get; set; }

        /// <summary>
        /// 包装含量
        /// </summary>
        public string PackInfos { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int SpouseBRanchIDs { get; set; }


        /// <summary>
        /// 含税进价
        /// </summary>
        public Nullable<decimal> TaxBids { get; set; }





        /// <summary>
        /// 进货件数
        /// </summary>
        public Nullable<decimal> MumberOfPackagesDing { get; set; }

        /// <summary>
        /// 进货细数
        /// </summary>
        public Nullable<decimal> Subdivision { get; set; }





        /// <summary>
        /// 零售单价
        /// </summary>
        public Nullable<decimal> RetailMonovalents { get; set; }


        /// <summary>
        /// 合同有效时间(起)
        /// </summary>
        private string validBegin;
        public string ValidBegin
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    validBegin = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    validBegin = value;
                }
            }
            get
            {
                return validBegin;
            }
        }
        /// <summary>
        /// 合同有效时间
        /// </summary>
        private string validTip;
        public string ValidTip
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    validTip = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    validTip = value;
                }
            }
            get
            {
                return validTip;
            }
        }


    }
}
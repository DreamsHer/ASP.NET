using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.LY
{
    public class PeiHuoDan: B_ConverList
    {
        /// <summary>
        /// 审核否
        /// </summary>
        public string ExamineNots { get; set; }
        /// <summary>
        /// 配货部门
        /// </summary>
        public string SpouseBRanchName { get; set; }

        /// <summary>
        /// 配货部门（副）
        /// </summary>
        public string SpouseBRanchNsame { get; set; }

        /// <summary>
        /// 收货部门
        /// </summary>
        public string StockPlaceName { get; set; }
        
         public string ExamineNotSt { get; set; }
        /// <summary>
        /// 收货部门（副）
        /// </summary>
        public string StockPlaceNametwo { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        private string egisterTime;
        public string registerTime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    egisterTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    egisterTime = value;
                }
            }
            get
            {
                return egisterTime;
            }
        }


        /// <summary>
        /// 启动计划时间
        /// </summary>
        private string panlDate;
        public string PanlDate
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    panlDate = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    panlDate = value;
                }
            }
            get
            {
                return panlDate;
            }
        }


        /// <summary>
        /// 启动时间
        /// </summary>
        private string qiDongTime;
        public string QiDongTime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    qiDongTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    qiDongTime = value;
                }
            }
            get
            {
                return qiDongTime;
            }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        private string amineTime;
        public string examineTime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    amineTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    amineTime = value;
                }
            }
            get
            {
                return amineTime;
            }
        }

        /// <summary>
        ///转库ID
        /// </summary>
        public Nullable<int> ChangeID { get; set; }

        /// <summary>
        ///要货ID
        /// </summary>
        public Nullable<int> WanManifestID { get; set; }
        
        /// <summary>
        ///进仓明细ID
        /// </summary>
        public Nullable<int> WareHouseDetiailID { get; set; }
        /// <summary>
        ///员工
        /// </summary>
        public Nullable<int> StaffID { get; set; }

        public Nullable<int> StaffIDtwo { get; set; }

        
        /// <summary>
        ///要货明细ID
        /// </summary>
        public Nullable<int> WanManifestDetailID { get; set; }

        /// <summary>
        ///销售ID
        /// </summary>
        public Nullable<int> SellID { get; set; }

        
        /// <summary>
        ///是否勾选
        /// </summary>
        public Nullable<bool> ShiFouGou { get; set; }

        /// <summary>
        ///返仓ID
        /// </summary>
        public Nullable<int> SellRetuerID { get; set; }

        /// <summary>
        ///返仓明细ID
        /// </summary>
        public Nullable<int> SellRetuerDateilID { get; set; }

        /// <summary>
        /// 销售明细ID
        /// </summary>
        public Nullable<int> SellDeTaliID { get; set; }



        /// <summary>
        /// 配货明细ID
        /// </summary>
        public Nullable<int> ConverDeTaiID { get; set; }

        /// <summary>
        /// 返厂明细ID
        /// </summary>
        public Nullable<int> RetureFactoryDeTailID { get; set; }

        /// <summary>
        /// 调拨ID 
        /// </summary>
        public Nullable<int> AllocateID { get; set; }

        /// <summary>
        /// 调拨中的配货ID 
        /// </summary>
        public Nullable<int> converIDs { get; set; }
        
         public Nullable<int> GoodsDingYiQuID { get; set; }
        /// <summary>
        /// 退货原因ID
        /// </summary>
        public Nullable<int> ReturnofID { get; set; }

        /// <summary>
        /// 返厂ID
        /// </summary>
        public Nullable<int> RetureFactoryID { get; set; }


        /// <summary>
        /// 入库件数
        /// </summary>
        public Nullable<decimal> MumberOfPackages { get; set; }


        /// <summary>
        /// 上一单的所剩件数
        /// </summary>
        public Nullable<decimal> UnotMumberOfPa { get; set; }

        /// <summary>
        /// 上一单的所剩细数
        /// </summary>
        public Nullable<decimal> UnotXiShu { get; set; }


       



        /// <summary>
        /// 原库件数
        /// </summary>
        public Nullable<decimal> YuanMumberOfPack { get; set; }

        /// <summary>
        /// 原件细数
        /// </summary>
        public Nullable<decimal> YuanSubdivision { get; set; }


        /// <summary>
        /// 原库细数
        /// </summary>
        public Nullable<decimal> MumberOfPackagesDing { get; set; }

        /// 价格
        /// </summary>
        public Nullable<decimal> TaxBid { get; set; }
        
        /// <summary>
        /// 入库件数_返厂
        /// </summary>
        public Nullable<decimal> ReMumberOfPackages { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public Nullable<decimal> Number { get; set; }

        /// <summary>
        /// 入库细数
        /// </summary>
        public Nullable<decimal> Subdivision { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Nullable<int> GoodsIDs { get; set; }

        /// <summary>
        /// 调拨明细ID
        /// </summary>
        public Nullable<int> AllocateDetailID { get; set; }

        /// <summary>
        /// 部门IDtwo
        /// </summary>
        public Nullable<int> SpouseBRanchIDtwo { get; set; }
        
        /// <summary>
        /// 库存IDtwo
        /// </summary>
        public Nullable<int> StockPlaceIDtwo { get; set; }
        /// <summary>
        /// 商品代码
        /// </summary>
        public string GoodsCodes { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string GoodsTiaoMas { get; set; }
        
        /// <summary>
        /// 启动人
        /// </summary>
        public string QiDongName { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsNames { get; set; }


        public int DrugTypeID { get; set; }
     

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
        /// 附件
        /// </summary>
        public string Appendix { get; set; }

        /// <summary>
        /// 包装信息中（包装含量）
        /// </summary>
        public string PackContents { get; set; }

        /// <summary>
        /// 含税进价
        /// </summary>
        public Nullable<decimal> TaxBids { get; set; }

        /// <summary>
        /// 零售单价
        /// </summary>
        public Nullable<decimal> RetailMonovalents { get; set; }


        /// <summary>
        /// 合同号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// 供货商
        /// </summary>
        public string SupplierCHName { get; set; }

        /// <summary>
        /// 核算
        /// </summary>
        public string AdjustAccountsFashion { get; set; }




        /// <summary>
        /// 返厂编号
        /// </summary>
        public string Remember { get; set; }


        /// <summary>
        /// 退货原因
        /// </summary>
        public string Returnof { get; set; }
        /// <summary>
        /// 维修
        /// </summary>
        public string Settlement { get; set; }


        /// <summary>
        /// 商品代码
        /// </summary>
        public string GoodsCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }
        /// <summary>
        /// 商品商标
        /// </summary>
        public string GoodsChopName { get; set; }

    }
}
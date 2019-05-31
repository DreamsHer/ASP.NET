using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class Goods: B_GoodsList
    {
        //自营商品信息定义
        public string BarCodTypeName { get; set; }
        public string GoodsStampName { get; set; }
        public string HostContractNumber { get; set; }
        public string SupplierCHName { get; set; }
        public string ManufacturerName { get; set; }
        public string GoodsChopName { get; set; }
        public string GoodsClassifyName { get; set; }
        public string Code { get; set; }
        public string EstimateUnitName { get; set; }
        public string ApplyTarget { get; set; }
        public string GoodsSeasonName { get; set; }
        public string SourceName { get; set; }
        public string GoodsStatusName { get; set; }
        public string BurdenStockSellStatusName { get; set; }
        public string BurdenStockDataNumber { get; set; }
        public string CommodityType { get; set; }
        public string ZhuanGuiCommodityType { get; set; }
        public string GoodsColours { get; set; }
        public string PackWeight { get; set; }
        public bool DefaultPack { get; set; }

        //商品明细
        public int PackInfoID { get; set; }
       
        public string PackContent { get; set; }
        public string PricePlace { get; set; }
        public string BarCode { get; set; }
        public string GoodsDetailName { get; set; }
        public Nullable<decimal> GoodsFixAPrice { get; set; }
        public Nullable<decimal> TaxBid { get; set; }

        //供货商
        public string ContractNumber { get; set; }
        public string SupplierNumber { get; set; }
        public string SupplierDependency { get; set; }
        public string SupplierRegisteredAddress { get; set; }

        //生产厂家
        public string ManufacturerCode { get; set; }
        public string ManufacturerPC { get; set; }
        public string ManufacturerAddress { get; set; }

        //自营商品状态变更
        public string SupplierName { get; set; }
        public Nullable<decimal> RetailMonovalent { get; set; }

        public Nullable<decimal> RateSale { get; set; }
        public Nullable<decimal> NewRateSale { get; set; }

        //自营合同经营商品定义
        public int GoodsDetailID { get; set; }
        public int PurchaseID { get; set; }
        public Nullable<decimal> AdvanceBid { get; set; }
        public Nullable<decimal> NoAdvanceBid { get; set; }
        public Nullable<decimal> PurchasePriceMarkup { get; set; }

        //订货清单处理
        public int StockID { get; set; }
        public string StockNumber { get; set; }
        public int OrderFormID { get; set; }       
        public string OrderNumber { get; set; }
        public int MethodOfSettlingAccountsID { get; set; }
        public string MethodOfSettlingAccounts { get; set; }
        public int AdjustAccountsFashionID { get; set; }
        public string AdjustAccountsFashion { get; set; }
        public int SpouseBRanchID { get; set; }
        public string SpouseBRanchName { get; set; }
        public string PurchasingAgent { get; set; }
        public string SpouPurchasingAgentseBRanchName { get; set; }       
        public string Place { get; set; }
        public string DeliveryFashion { get; set; }       
        public Nullable<decimal> Value { get; set; }
        public Nullable<decimal> ExpensesOTtaxation { get; set; }       
        public Nullable<decimal> ValueTotal { get; set; }
        public int OrderFormTypeID { get; set; }
        public string OrderFormTypeName { get; set; }
        public string Register { get; set; }
        public string CheckRen { get; set; }
        public string DingJiaRen { get; set; }
        public string WuJiaCheckRen { get; set; }
        public int IndentNumberStatusID { get; set; }
        public string IndentNumberStatusName { get; set; }
        public int IndentCircumstancesID { get; set; }
        public string IndentCircumstancesName { get; set; }
        public int NewGoodsManageID { get; set; }

        public int OrderFormDetailID { get; set; }


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

        private string releaseTimeStrf;
        public string ReleaseTimeStrf
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrf = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrf = value;
                }
            }
            get
            {
                return releaseTimeStrf;
            }
        }

        private string releaseTimeStrd;
        public string ReleaseTimeStrd
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrd = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrd = value;
                }
            }
            get
            {
                return releaseTimeStrd;
            }
        }

        private string releaseTimeStrh;
        public string ReleaseTimeStrh
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrh = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrh = value;
                }
            }
            get
            {
                return releaseTimeStrh;
            }
        }        



        //自定义合同经营商品
        public int GoodsDetailIDs { get; set; }

        //设置商品最低售价
        public int GoodsBottomPriceID { get; set; }
        public Nullable<decimal> LastBid { get; set; }

        public Nullable<decimal> FactPrice { get; set; }
        public Nullable<decimal> MemberPrice { get; set; }

        //打包商品定价
        public int PackGoodsID { get; set; }
        public string GoodsPurchasemoneyStatus { get; set; }
        public Nullable<decimal> ReferencePrice { get; set; }
        public string PackCode { get; set; }
        public string PackName { get; set; }
        public string PackTiaoMa { get; set; }

        //物价变动曲线
        public int GoodsBianJiaID { get; set; }

        //采价分析
        public Nullable<decimal> Price1 { get; set; }
        public Nullable<decimal> Price2 { get; set; }
        public Nullable<decimal> Price3 { get; set; }
        public Nullable<decimal> Price4 { get; set; }

        //设置折扣时段
        public int AgioTimeIntervalID { get; set; }
        public string FenDanHao { get; set; }
        public string TimeInterval { get; set; }
        public int SaleAdjustID { get; set; }
        public string AdjustCode { get; set; }
        public int AdjustTypeID { get; set; }
        public string AdjustTypeName { get; set; }
        public Nullable<decimal> Sale { get; set; }
        public string StopRen { get; set; }

        //定义价签信息
        public int GoodsSignID { get; set; }
        public string JiaQianCode { get; set; }
        public string GoodsSignName { get; set; }
        public int SignTypeID { get; set; }
        public string SignTypeName { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Broadwise { get; set; }
        public string Ordinate { get; set; }
        public string EveryRow { get; set; }
        public string EveryRank { get; set; }

        //采购变价处理       
        public string BianJiaBianHao { get; set; }
        public int ChangeWhyID { get; set; }
        public string ChangeWhy { get; set; }
        public int ChangeTypeID { get; set; }
        public string ChangeTypeName { get; set; }



        public Nullable<decimal> MumberOfPackages { get; set; }
        public Nullable<decimal> Subdivision { get; set; }        
        public string Quantity { get; set; }
        public bool ConsigneeNot { get; set; }

        private string releaseTimeStrx;
        public string ReleaseTimeStrx
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrx = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrx = value;
                }
            }
            get
            {
                return releaseTimeStrh;
            }
        }

        public Nullable<decimal> NewMonovalent { get; set; }

        //联营租赁扣率调整单   
        public string XingQi { get; set; }
        public bool DiaoZhengDanShenHeFou { get; set; }

        public bool ShiFouShouHuo { get; set; }


        //前台销售单查询
        public int ShooppingDateilIDOrderFounID { get; set; }
        public int ShooppingOrderFountID { get; set; }    
        public string ShooppingNumber { get; set; }
        public Nullable<bool>  ExamineNot { get; set; }
        public Nullable<int> ShoppingID { get; set; }
        public Nullable<int> ConverDateilID { get; set; }
        public int GoodsListedDetailID { get; set; }




        //时间转化

        public Nullable<System.DateTime> ShooppingTime { get; set; }
       
        public string releaseTimeSW;
        public string ReleaseTimeSW
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeSW = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception)
                {
                    releaseTimeSW = value;
                }
            }
            get
            {
                return releaseTimeSW;
            }
        }

        //public Nullable<System.DateTime> ShooppingTime { get; set; }
        //时间转化
        public string releaseTimeStrbb;
        public string ReleaseTimeStrbb
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrbb = dt.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception)
                {
                    releaseTimeStrbb = value;
                }
            }
            get
            {
                return releaseTimeStrbb;
            }
        }


    }
}
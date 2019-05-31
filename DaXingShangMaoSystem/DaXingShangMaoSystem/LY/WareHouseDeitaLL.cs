using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.LY
{
    public class WareHouseDeitaLL : B_WareHouseDetiailList
    {
        /// <summary>
        /// 进仓编号
        /// </summary>
        public string Remember { get; set; }

        /// <summary>
        /// 供货商编号
        /// </summary>
         public string SupplierNumber { get; set; }

        public string EstimateUnitName { get; set; }
        /// <summary>
        /// </summary>
        public string departmentName { get; set; }

        /// <summary>
        /// </summary>
        public string departmentCodes { get; set; }


        /// <summary>
        /// 盘点部门
        /// </summary>
        public Nullable<int> SpouseBRancfdhID { get; set; }

        /// <summary>
        /// 进货件数
        /// </summary>
        public Nullable<decimal> MumberOfPackagesDing { get; set; }

        /// <summary>
        /// 供货商Id
        /// </summary>
        public Nullable<int> SupplierID { get; set; }
        public Nullable<int> staffId { get; set; }
        
        /// <summary>
        /// 经销ID
        /// </summary>
        public Nullable<int> AdjustAccountsFashionID { get; set; }

        public Nullable<int> DfrugTypeID { get; set; }

        /// <summary>
        /// 商品分类编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 商品分类名称
        /// </summary>
        public string GoodsClassifyName { get; set; }
        /// <summary>
        /// 商品分类ID
        /// </summary>
        public Nullable<int> GoodsClassifyID { get; set; }

        /// <summary>
        /// 盘点计划明细ID
        /// </summary>
        public Nullable<int> CheckRemerbenDetillD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Nullable<int> departmentID { get; set; }


        /// <summary>
        /// 商品商标ID
        /// </summary>
        public Nullable<int> GoodsChopID { get; set; }


        /// <summary>
        ///损益ID
        /// </summary>
        public Nullable<int> IncreaseID { get; set; }

        /// <summary>
        ///损益明细ID
        /// </summary>
        public Nullable<int> IncreaseDetailID { get; set; }

        /// <summary>
        /// 盘点计划ID
        /// </summary>
        public Nullable<int> CheckRermeberID { get; set; }
        /// <summary>
        /// 商品商标编码
        /// </summary>
        public string GoodsChopCode { get; set; }


        /// <summary>
        /// 损益原因
        /// </summary>
        public string IncreaseCause { get; set; }


       
        /// </summary>
        public string Remter { get; set; }



        /// <summary>
        /// 商品商标名称
        /// </summary>
        public string GoodsChopName { get; set; }


        /// <summary>
        /// 合同ID
        /// </summary>
        public Nullable<int> OrderFormPactID { get; set; }

        /// <summary>
        /// 盘点类型ID
        /// </summary>
        public Nullable<int> CommodityTypeID { get; set; }

        /// <summary>
        /// 转库明细ID
        /// </summary>
        public Nullable<int> ChangeDetailID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public Nullable<int> OrdernFromIDs { get; set; }

        /// <summary>
        /// 订单明细ID
        /// </summary>
        public Nullable<int> OrderFormDetailIDs { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }


        /// <summary>
        /// 库存地点
        /// </summary>
        public string StockPlaceName { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public string EnteringTime { get; set; }

        /// <summary>
        /// 盘点类型
        /// </summary>
        public string CommodityType { get; set; }

         /// <summary>
        /// 计划部门
        /// </summary>
        public string DrugType { get; set; }

        /// <summary>
        /// 库存地点ID
        /// </summary>
        public int StockPlaceID { get; set; }

        /// <summary>
        /// 盘点部门
        /// </summary>
        public int DrugTypeID { get; set; }

        /// <summary>
        /// 盘点部门(小写Id)区分冲突
        /// </summary>
        public Nullable<int> drugTypeID { get; set; }

        public Nullable<int> DrugTypefID { get; set; }


        /// <summary>
        /// 区分ID
        /// </summary>
        public int QuFenLeiXingID { get; set; }
        /// <summary>
        /// 收货人ID
        /// </summary>
        public int StaffID { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 区分单类型
        /// </summary>
        public string QuFenLeiXingMC { get; set; }
        /// <summary>
        /// 收货人编号
        /// </summary>
        public string StaffCode { get; set; }

        /// <summary>
        /// 商品明细ID
        /// </summary>
        public int GoodsDetailID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Nullable<int> GoodsIDs { get; set; }


        /// <summary>
        /// 合同ID
        /// </summary>
        public Nullable<int> OrderFormPactIDs { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        /// 结算方式
        /// </summary>
        public string MethodOfSettlingAccounts { get; set; }

        /// <summary>
        /// 进货部门
        /// </summary>
        public int SpouseBRanchID { get; set; }

        /// <summary>
        /// 进货部门
        /// </summary>
        public string SpouseBRanchName { get; set; }

        /// <summary>
        /// 供货商名称
        /// </summary>
        public string SupplierCHName { get; set; }

        /// <summary>
        /// 核算
        /// </summary>
        public string AdjustAccountsFashion { get; set; }

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
        /// （订单明细）包装含量
        /// </summary>
        public string PackInfos { get; set; }

        /// <summary>
        /// 包装信息中（包装含量）
        /// </summary>
        public string PackContents { get; set; }

        /// <summary>
        /// 含税进价
        /// </summary>
        public Nullable<decimal> TaxBids { get; set; }
        
        /// <summary>
        /// 不含税进价
        /// </summary>
        public Nullable<decimal> AdvanceBid { get; set; }

        public Nullable<decimal> HaoSunLv { get; set; }

        //试一下
        public Nullable<decimal> HeLiHaoSunLv { get; set; }
        public Nullable<decimal> ChaE { get; set; }
        public Nullable<decimal> ShiJiHaoSunLv { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public Nullable<decimal> Number { get; set; }

        /// <summary>
        /// 盈亏
        /// </summary>
        public Nullable<decimal> YingKuiMoney { get; set; }


        /// <summary>
        /// 批次
        /// </summary>
        public Nullable<decimal> PiCiHao { get; set; }

        /// <summary>
        /// 零售单价
        /// </summary>
        public Nullable<decimal> RetailMonovalents { get; set; }


        /// <summary>
        ///转库件数
        /// </summary>
        public Nullable<decimal> MumberOfPackagesf { get; set; }

        /// <summary>
        /// 转库细数
        /// </summary>
        public Nullable<decimal> Subdivisionkl { get; set; }


        /// <summary>
        ///价款
        /// </summary>
        public Nullable<decimal> Value { get; set; }

        /// <summary>
        /// 税金
        /// </summary>
        public Nullable<decimal> ExpensesOTtaxation { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        public Nullable<decimal> ValueTotal { get; set; }



      
        public Nullable<decimal> FactPrice { get; set; }


        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisterName { get; set; }

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

        
        /// <summary>
        /// 盘点计划日期
        /// </summary>
        private string pablData;
        public string PablData
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    pablData = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    pablData = value;
                }
            }
            get
            {
                return pablData;
            }
        }
        /// <summary>
        /// 审核人
        /// </summary>
        public string ExamineName { get; set; }

        /// <summary>
        ///审核时间
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

        ///// <summary>
        ///// 进仓状态（审核）
        ///// </summary>
        public Nullable<bool> ExamineNot { get; set; }

        /// <summary>
        /// 加载否（审核）
        /// </summary>
        public Nullable<bool> JiaZaiFou { get; set; }

        /// <summary>
        /// 进仓状态（审核）
        /// </summary>
       // public string ExamineNot { get; set; }

        /// <summary>
        /// 进仓状态（生效否）
        /// </summary>
        public Nullable<bool> Status { get; set; }

        /// <summary>
        /// 进仓状态（冲红）
        /// </summary>
        public Nullable<bool> CrushRedNot { get; set; }


        /// <summary>
        /// 盘点计划（状态）
        /// </summary>
        public Nullable<bool> Entering { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi
{
    public class JinXiaoCunShuJuController : Controller
    {
        // GET: GoodsZhangGuanLi/JinXiaoCunShuJu
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult JinXiaoSelect()
        {
            return View();
        }




        /// <summary>
        /// 查询商标
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectBangDingPeiHuo(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSellct in MyModels.B_SellList
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                    
                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCode = tbGoods.GoodsCode,
                            GoodsChopName = tbShangBiao.GoodsChopName,

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.ConverID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 绑定商标相关信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult angDingPeiHuo(int goodsIDs)
        {
            var linq = (from tbGoods in MyModels.B_GoodsList//商品
                       join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                       join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                       join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                       join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                       select new LY.PeiHuoDan
                        {
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                          
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        public ActionResult QiDongSelectSellctdGoods(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSellct in MyModels.B_SellList
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBuMen.SpouseBRanchID
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单

                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsChopName = tbShangBiao.GoodsChopName,
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号

                            SpouseBRanchName = tbBuMen.SpouseBRanchName,
                            PanlDate = tbSellct.PanlDate.ToString(),
                            RegisterName = tbSellct.RegisterName,
                        }).ToList();
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.SellID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 查询对应商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectSellctdGoods(Vo.BsgridPage bsgridPage, int GooddsIDs)
        {
            var linq = (from tbSellct in MyModels.B_SellList
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBuMen.SpouseBRanchID
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单

                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                    
                        where tbGoods.GoodsID == GooddsIDs
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsChopName = tbShangBiao.GoodsChopName,
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号

                            SpouseBRanchName=tbBuMen.SpouseBRanchName,
                            PanlDate=tbSellct.PanlDate.ToString(),
                            RegisterName = tbSellct.RegisterName,
                        }).ToList();
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.SellID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }






        //第二部分
        //(商品分批次进销存数据)
        //

        public ActionResult GoodsPiCiXiao()
        {
            return View();
        }


        /// <summary>
        /// 下拉框（采购部门）
        /// </summary>
        /// <returns></returns>
        public ActionResult CaiGouXiadLa(int spouseBRanchID)
        {
            var linq = (from tb in MyModels.S_SpouseBRanchList
                        where tb.SpouseBRanchID == spouseBRanchID
                        select new LY.WareHouseDeitaLL
                        {

                            SpouseBRanchName = tb.SpouseBRanchName
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进货部门
        /// </summary>
        public ActionResult CaiGouXiafffLa(int goodsChopID)
        {
            var linq = (from tb in MyModels.S_SpouseBRanchList
                        where tb.SpouseBRanchID == goodsChopID
                        select new LY.WareHouseDeitaLL
                        {

                            StockPlaceName = tb.SpouseBRanchName
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 查询销售（启动时查询）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult QiDongSelectBSellHuo(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSellct in MyModels.B_SellList
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbConver in MyModels.B_ConverList on tbSellDatelst.ConverID equals tbConver.ConverID//配货单

                        join tbWanHout in MyModels.B_WareHouseList on tbConver.WareHouseID equals tbWanHout.WareHouseID//进仓单
                        join tbHeTong in MyModels.B_OrderFormPactList on tbWanHout.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbSupp in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbSupp.SupplierID//供货
                        join tbJieSuan in MyModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbHeTong.AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID//核算

                        join tbBuMne in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBuMne.SpouseBRanchID//部门
                        where tbSellct.ExamineNot == true

                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbSellct.SellID,
                            Remember = tbSellct.Remember,
                            SpouseBRanchName = tbBuMne.SpouseBRanchName,
                            MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,
                            SupplierNumber = tbSupp.SupplierNumber,
                            SupplierCHName = tbSupp.SupplierName,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion,
                            ContractNumber = tbHeTong.ContractNumber,
                            RegisterName = tbSellct.RegisterName,
                            RegisterTime = tbSellct.RegisterTime.ToString(),
                            ExamineName = tbSellct.ExamineName,
                            PablData = tbSellct.ExecuteTime.ToString(),

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.IncreaseID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).ToList();

            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 查询销售（起）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectBSellHuo(Vo.BsgridPage bsgridPage,int spouseBRanchID)
        {
            var linq = (from tbSellct in MyModels.B_SellList
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbConver in MyModels.B_ConverList on tbSellDatelst.ConverID equals tbConver.ConverID//配货单

                        join tbWanHout in MyModels.B_WareHouseList on tbConver.WareHouseID equals tbWanHout.WareHouseID//进仓单
                        join tbHeTong in MyModels.B_OrderFormPactList on tbWanHout.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbSupp in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbSupp.SupplierID//供货
                        join tbJieSuan in MyModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbHeTong .AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID//核算

                        join tbBuMne in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBuMne.SpouseBRanchID//部门
                        where tbBuMne.SpouseBRanchID== spouseBRanchID && tbSellct.ExamineNot==true

                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbSellct.SellID,
                            Remember = tbSellct.Remember,
                            SpouseBRanchName = tbBuMne.SpouseBRanchName,
                            MethodOfSettlingAccounts=tbJieSuan.MethodOfSettlingAccounts,
                            SupplierNumber = tbSupp.SupplierNumber,
                            SupplierCHName = tbSupp.SupplierName,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion,
                            ContractNumber = tbHeTong.ContractNumber,
                            RegisterName = tbSellct.RegisterName,
                            RegisterTime = tbSellct.RegisterTime.ToString(),
                            ExamineName = tbSellct.ExamineName,
                            PablData = tbSellct.ExecuteTime.ToString(),

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.IncreaseID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).ToList();

            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 查询销售商品（起）
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectGoodso(Vo.BsgridPage bsgridPage, int SellID)
        {
            var linq = (from tbSellct in MyModels.B_SellList
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbConver in MyModels.B_ConverList on tbSellDatelst.ConverID equals tbConver.ConverID//配货单

                        join tbWanHout in MyModels.B_WareHouseList on tbConver.WareHouseID equals tbWanHout.WareHouseID//进仓单
                        join tbWanHoutDetail in MyModels.B_WareHouseDetiailList on tbWanHout.WareHouseID equals tbWanHoutDetail.WareHouseID//进仓单明细

                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表


                        where tbSellct.SellID == SellID

                        select new LY.WareHouseDeitaLL
                        {
                         
                            GoodsChopName = tbShangBiao.GoodsChopName,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            MumberOfPackages=tbWanHoutDetail.MumberOfPackages,//进仓件数

                            HaoSunLv= tbWanHoutDetail.MumberOfPackages * tbGoodDetail.RetailMonovalent,//进仓价格

                            Subdivision = tbConverDaTe.MumberOfPackages,//配货件数
                            HeLiHaoSunLv= tbConverDaTe.MumberOfPackages* tbGoodDetail.RetailMonovalent//配货金额
                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.IncreaseID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).ToList();

            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }


    }
}
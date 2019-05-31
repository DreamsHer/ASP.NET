using DaXingShangMaoSystem.LY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.FanCangDan.Controllers
{
    public class SelectChanghController : Controller
    {
        // GET: FanCangDan/SelectChangh
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult Changht()
        {
            return View();
        }


        /// <summary>
        /// 查询 转库单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult ChaXunZhuanKu(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbZhuanKu in myModels.B_ChangeList//转库单ID
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbZhuanKu.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//部门
                        join tbKuCuDiDian in myModels.S_StockPlaceList on tbZhuanKu.StockPlaceID equals tbKuCuDiDian.StockPlaceID//原库存地点
                        join tbNewKuCuDiDian in myModels.S_StockPlaceList on tbZhuanKu.StockPlaceIDtwo equals tbNewKuCuDiDian.StockPlaceID//新库存地点
                        where tbZhuanKu.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            ChangeID = tbZhuanKu.ChangeID,
                            WareHouseID = tbZhuanKu.WareHouseID,
                            Remember = tbZhuanKu.Remember,
                            SpouseBRanchName = tbPeiHuoBuMen.SpouseBRanchName,
                            StockPlaceName = tbKuCuDiDian.StockPlaceName,
                            StockPlaceNametwo = tbNewKuCuDiDian.StockPlaceName,
                            payName = tbZhuanKu.payName,
                            furlName = tbZhuanKu.furlName,
                            RegisterName = tbZhuanKu.RegisterName,
                            registerTime = tbZhuanKu.RegisterTime.ToString(),
                            ExamineName = tbZhuanKu.ExamineName,
                            examineTime = tbZhuanKu.ExamineTime.ToString(),
                            ExamineNot = tbZhuanKu.ExamineNot

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.ChangeID).//noboer表达式
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
        /// 绑定商品到（主界面）全部选（二）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingZhuanKuGoodel(Vo.BsgridPage bsgridPage, int changeID)
        {
            var Linq = (from tbChangeDetiai in myModels.B_ChangeDetailList//转库明细
                        join tbWareHouseDetiai in myModels.B_WareHouseDetiailList on tbChangeDetiai.WareHouseDetiailID equals tbWareHouseDetiai.WareHouseDetiailID//进仓明细
                        join tbSelectGoods in myModels.B_GoodsList on tbWareHouseDetiai.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                        join tbJiSuanDan in myModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbChangeDetiai.ChangeID == changeID
                        select new LY.WareHouseDeitaLL
                        {
                            ChangeDetailID = tbChangeDetiai.ChangeDetailID,
                            WareHouseDetiailID = tbWareHouseDetiai.WareHouseDetiailID,
                            GoodsIDs = tbSelectGoods.GoodsID,//商品ID
                            GoodsCodes = tbSelectGoods.GoodsCode,//商品代码
                            GoodsTiaoMas = tbSelectGoods.GoodsTiaoMa,//商品条码
                            GoodsNames = tbSelectGoods.GoodsName,//商品名称
                            ArtNos = tbSelectGoods.ArtNo,//商品货号

                            SpecificationsModels = tbSelectGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiSuanDan.EstimateUnitName,//计量单位
                            PackContents = tbPackln.PackContent,//包装含量
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

                            MumberOfPackages = tbWareHouseDetiai.MumberOfPackages,//件数
                            Subdivision = tbWareHouseDetiai.Subdivision,//入库小数

                            MumberOfPackagesf = tbChangeDetiai.MumberOfPackages,
                            Subdivisionkl = tbChangeDetiai.Subdivision,


                        }).ToList();
            int totalRow = Linq.Count();
            List<LY.WareHouseDeitaLL> notices = Linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 浏览器打印调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult DaYinConverList()
        {

            var Linq = (from tbZhuanKu in myModels.B_ChangeList//转库单ID
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbZhuanKu.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//部门
                        join tbKuCuDiDian in myModels.S_StockPlaceList on tbZhuanKu.StockPlaceID equals tbKuCuDiDian.StockPlaceID//原库存地点
                        join tbNewKuCuDiDian in myModels.S_StockPlaceList on tbZhuanKu.StockPlaceIDtwo equals tbNewKuCuDiDian.StockPlaceID//新库存地点
                        where tbZhuanKu.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            ChangeID = tbZhuanKu.ChangeID,
                            WareHouseID = tbZhuanKu.WareHouseID,
                            Remember = tbZhuanKu.Remember,
                            SpouseBRanchName = tbPeiHuoBuMen.SpouseBRanchName,
                            StockPlaceName = tbKuCuDiDian.StockPlaceName,
                            StockPlaceNametwo = tbNewKuCuDiDian.StockPlaceName,
                            payName = tbZhuanKu.payName,
                            furlName = tbZhuanKu.furlName,
                            RegisterName = tbZhuanKu.RegisterName,
                            registerTime = tbZhuanKu.RegisterTime.ToString(),
                            ExamineName = tbZhuanKu.ExamineName,
                            examineTime = tbZhuanKu.ExamineTime.ToString(),
                            ExamineNots = tbZhuanKu.ExamineNot.ToString(),//审核否

                        }).ToList();

            List<PeiHuoDan> listWareHouseDeitaLL = new List<PeiHuoDan>();

            for (int i = 0; i < Linq.Count; i++)
            {
                PeiHuoDan myWareHouseDeitaLL = new PeiHuoDan();
                if (Convert.ToBoolean(Linq[i].ExamineNots) == true)
                {
                    myWareHouseDeitaLL.ExamineNots = "已审核";
                }
                else
                {
                    myWareHouseDeitaLL.ExamineNots = "未审核";
                }

                myWareHouseDeitaLL.Remember = Linq[i].Remember;
                myWareHouseDeitaLL.SpouseBRanchName = Linq[i].SpouseBRanchName;
                myWareHouseDeitaLL.StockPlaceName = Linq[i].StockPlaceName;
                myWareHouseDeitaLL.StockPlaceNametwo = Linq[i].StockPlaceNametwo;
                myWareHouseDeitaLL.payName = Linq[i].payName;
                myWareHouseDeitaLL.furlName = Linq[i].furlName;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;

                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);

        }



        /// <summary>
        /// 要货单
        /// </summary>
        /// <returns></returns>
        public ActionResult YaoHuoDan()
        {
            return View();
        }


        /// <summary>
        /// 查询要货单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectWanMetr(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbWanMetre in myModels.B_WanManifestList//要货单
                        join tbSpatouKrdg in myModels.S_SpouseBRanchList on tbWanMetre.PurchaseSectionID equals tbSpatouKrdg.SpouseBRanchID
                       
                        select new LY.PeiHuoDan
                        {
                            WanManifestID = tbWanMetre.WanManifestID,
                            SellID = tbWanMetre.SellID,//要货id
                            Remember = tbWanMetre.Remember,//编号
                            SpouseBRanchName = tbSpatouKrdg.SpouseBRanchName,//部门
                            RegisterName = tbWanMetre.RegisterName,//制单人
                            registerTime = tbWanMetre.RegisterTime.ToString(),//制单时间
                            ExamineName = tbWanMetre.ExamineName,//制单人
                            examineTime = tbWanMetre.ExamineTime.ToString(),//制单时间
                            ExamineNot = tbWanMetre.ExamineNot//
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
        /// 绑定要货单商品（二）
        /// </summary>
     
        public ActionResult SelectSellctGoods(Vo.BsgridPage bsgridPage, int wanManifestID)
        {
            var linq = (from tbSellct in myModels.B_WanManifestList//要货单
                        join tbSellDatelst in myModels.B_WanManifestDetailList on tbSellct.WanManifestID equals tbSellDatelst.WanManifestID//要货明细单

                        join tbGoods in myModels.B_GoodsList on tbSellDatelst.GoodsID equals tbGoods.GoodsID//商品

                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbSellDatelst.WanManifestID == wanManifestID
                        select new LY.PeiHuoDan
                        {
                            WanManifestDetailID = tbSellDatelst.WanManifestDetailID,//要货明细id
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            MumberOfPackages = tbSellDatelst.MumberOfPackages,//
                            Subdivision = tbSellDatelst.Subdivision,//
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号
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
        /// 浏览器打印要货单
        /// </summary>
        /// <returns></returns>
        public ActionResult DaYinConverListYaoHuo()
        {

            var Linq = (from tbWanMetre in myModels.B_WanManifestList//要货单
                        join tbSpatouKrdg in myModels.S_SpouseBRanchList on tbWanMetre.PurchaseSectionID equals tbSpatouKrdg.SpouseBRanchID

                        select new LY.PeiHuoDan
                        {
                            WanManifestID = tbWanMetre.WanManifestID,
                            SellID = tbWanMetre.SellID,//要货id
                            Remember = tbWanMetre.Remember,//编号
                            SpouseBRanchName = tbSpatouKrdg.SpouseBRanchName,//部门
                            RegisterName = tbWanMetre.RegisterName,//制单人
                            registerTime = tbWanMetre.RegisterTime.ToString(),//制单时间
                            ExamineName = tbWanMetre.ExamineName,//制单人
                            examineTime = tbWanMetre.ExamineTime.ToString(),//制单时间
                            ExamineNots = tbWanMetre.ExamineNot.ToString(),//审核否
                        }).ToList();

            List<PeiHuoDan> listWareHouseDeitaLL = new List<PeiHuoDan>();

            for (int i = 0; i < Linq.Count; i++)
            {
                PeiHuoDan myWareHouseDeitaLL = new PeiHuoDan();
                if (Convert.ToBoolean(Linq[i].ExamineNots) == true)
                {
                    myWareHouseDeitaLL.ExamineNots = "已审核";
                }
                else
                {
                    myWareHouseDeitaLL.ExamineNots = "未审核";
                }

                myWareHouseDeitaLL.Remember = Linq[i].Remember;
                myWareHouseDeitaLL.SpouseBRanchName = Linq[i].SpouseBRanchName;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi
{
    public class XiaoShouShuJUController : Controller
    {
        // GET: GoodsZhangGuanLi/XiaoShouShuJU

        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult YingYeYuan()
        {
            return View();
        }


        /// <summary>
        /// 查询销售（起）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult QiDongSelectBSellHuo(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbYaoHuo in MyModels.B_WanManifestList//要货单
                        join tbBuMne in MyModels.S_SpouseBRanchList on tbYaoHuo.PurchaseSectionID equals tbBuMne.SpouseBRanchID
                        join tbSellDatelst in MyModels.B_WanManifestDetailList on tbYaoHuo.WanManifestID equals tbSellDatelst.WanManifestID//销售明细单
                        join tbGoods in MyModels.B_GoodsList on tbSellDatelst.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        select new LY.WareHouseDeitaLL
                        {
                            SupplierID = tbYaoHuo.WanManifestID,
                            SpouseBRanchName = tbBuMne.SpouseBRanchName,
                            RegisterName = tbYaoHuo.RegisterName,
                            //FactPrice = tbGoodDetail.FactPrice * tbSellDatelst.MumberOfPackages,//商品单价（销售价）
                            RegisterTime = tbYaoHuo.RegisterTime.ToString(),
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
        /// 查询销售（起）条件
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectBSellHuo(Vo.BsgridPage bsgridPage, int spouseBRanchID)
        {
            var linq = (from tbYaoHuo in MyModels.B_WanManifestList//要货单
                       join tbBuMne in MyModels.S_SpouseBRanchList on tbYaoHuo.PurchaseSectionID equals tbBuMne.SpouseBRanchID
                        join tbSellDatelst in MyModels.B_WanManifestDetailList on tbYaoHuo.WanManifestID equals tbSellDatelst.WanManifestID//销售明细单
                        join tbGoods in MyModels.B_GoodsList on tbSellDatelst.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        where tbYaoHuo.PurchaseSectionID == spouseBRanchID

                        select new LY.WareHouseDeitaLL
                        {
                            SupplierID = tbYaoHuo.WanManifestID,
                            SpouseBRanchName = tbBuMne.SpouseBRanchName,
                            RegisterName = tbYaoHuo.RegisterName,
                            //FactPrice = tbGoodDetail.FactPrice * tbSellDatelst.MumberOfPackages,//商品单价（销售价）
                            RegisterTime = tbYaoHuo.RegisterTime.ToString(),
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
        /// 查询销售（交易次数）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectBSellHuoCiShu(int spouseBRanchID)
        {
            var linq = (from tbYaoHuo in MyModels.B_WanManifestList//要货单

                        join tbSellDatelst in MyModels.B_WanManifestDetailList on tbYaoHuo.WanManifestID equals tbSellDatelst.WanManifestID//销售明细单
                        join tbGoods in MyModels.B_GoodsList on tbSellDatelst.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        where tbYaoHuo.PurchaseSectionID == spouseBRanchID

                        select new LY.WareHouseDeitaLL
                        {
                         
                            RegisterName = tbYaoHuo.RegisterName,
                            //FactPrice = tbGoodDetail.FactPrice * tbSellDatelst.MumberOfPackages,//商品单价（销售价）
                          
                        }).ToList();
            var hfdgf = 0;
            for (int i = 0; i < linq.Count(); i++)
            {
                hfdgf = hfdgf+1;
            }

            return Json(hfdgf, JsonRequestBehavior.AllowGet);

        }


        //查询返厂单
        public ActionResult SelectRetlre(string RegisterName)
        {
            var linq = (from tbFanChan in MyModels.B_RetureFactoryList
                        where tbFanChan.RegisterName == RegisterName || tbFanChan.ExamineName== RegisterName
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryID = tbFanChan.RetureFactoryID,
                            Remember = tbFanChan.Remember,

                        }).ToList();
            var hfdgf = 0;
            for (int i = 0; i < linq.Count(); i++)
            {
                hfdgf = hfdgf + 1;
            }

            return Json(hfdgf, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 查询返厂附属商品
        /// </summary>
        /// <returns></returns>
        public ActionResult BangFanChangGoodel(string RegisterName)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_RetureFactoryList//返厂ID
                        join tbWanHofDeaill in MyModels.B_RetureFactoryDeTailList on tbSelectWanHtsList.RetureFactoryID equals tbWanHofDeaill.RetureFactoryID//返厂明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                      
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbSelectWanHtsList.RegisterName == RegisterName || tbSelectWanHtsList.ExamineName == RegisterName
                        select new LY.PeiHuoDan
                        {
                            

                            ReMumberOfPackages = tbWanHofDeaill.ReMumberOfPackages,//返厂件数
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            Number= tbWanHofDeaill.ReMumberOfPackages* tbGoodDetail.RetailMonovalent//退货金额

                        }).ToList();

            var hfdgf = 0;
            for (int i = 0; i < linq.Count(); i++)
            {
                hfdgf = hfdgf + Convert.ToInt32(linq[i].Number);
            }


            return Json(hfdgf, JsonRequestBehavior.AllowGet);
        }


    }
}
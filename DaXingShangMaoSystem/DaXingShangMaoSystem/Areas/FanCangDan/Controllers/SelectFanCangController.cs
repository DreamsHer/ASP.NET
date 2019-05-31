using DaXingShangMaoSystem.LY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.FanCangDan.Controllers
{
    public class SelectFanCangController : Controller
    {
        // GET: FanCangDan/SelectFanCang
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult SelectFanCangDan()
        {
            return View();
        }


        /// <summary>
        /// 查询返仓单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectSellctRect(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSellct in MyModels.B_SellRetuerList//返仓单
                        join tbSpuBuMen in MyModels.S_StockPlaceList on tbSellct.StockPlaceID equals tbSpuBuMen.StockPlaceID
                        join tbSpudfdBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpudfdBuMen.SpouseBRanchID
                        where tbSellct.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            SellRetuerID = tbSellct.SellRetuerID,//返仓id
                            SellID = tbSellct.SellID,//销售id
                            Remember = tbSellct.Remember,//返仓单编号
                            StockPlaceName = tbSpuBuMen.StockPlaceName,//收货部门
                            SpouseBRanchName = tbSpudfdBuMen.SpouseBRanchName,//收货部门
                            payName = tbSellct.payName,//实物付
                            furlName = tbSellct.furlName,//实物收
                            Remarks = tbSellct.Remarks,//备注

                            RegisterName = tbSellct.RegisterName,//制单
                            registerTime = tbSellct.RegisterTime.ToString(),//时间
                            ExamineName = tbSellct.ExamineName,//审核
                            examineTime = tbSellct.ExamineTime.ToString(),//时间
                            ExamineNot = tbSellct.ExamineNot,//审核否

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.SellRetuerID).//noboer表达式
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


        // <summary>
        /// 商品到
        /// </summary>
        /// <returns></returns>
        public ActionResult gBangDingPiGoodel(Vo.BsgridPage bsgridPage, int sellRetuerID)
        {

            var linq = (from tbSellct in MyModels.B_SellRetuerList//返仓单
                        join tbSellDatelst in MyModels.B_SellRetuerDateiList on tbSellct.SellRetuerID equals tbSellDatelst.SellRetuerID//返仓明细单
                        join tbBumen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBumen.SpouseBRanchID

                        join tbXiaoShouMingXi in MyModels.B_SellDeTaLsit on tbSellDatelst.SellDeTaliID equals tbXiaoShouMingXi.SellDeTaliID
                        join tbConverDate in MyModels.B_ConverDeTailList on tbXiaoShouMingXi.ConverDeTaiID equals tbConverDate.ConverDeTaiID//配货明细


                        join tbGoods in MyModels.B_GoodsList on tbConverDate.GoodsID equals tbGoods.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        where tbSellct.SellRetuerID == sellRetuerID
                        select new LY.PeiHuoDan
                        {
                            SellRetuerID = tbSellct.SellRetuerID,
                            SellRetuerDateilID = tbSellDatelst.SellRetuerDateilID,//返仓明细id
                            SellDeTaliID = tbXiaoShouMingXi.SellDeTaliID,//销售明细id
                            Number = tbSellDatelst.Number,//批次
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            MumberOfPackages = tbSellDatelst.MumberOfPackages,//发货件数
                            Subdivision = tbSellDatelst.Subdivision,//发货细数
                          
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号
                            SpouseBRanchName = tbBumen.SpouseBRanchName,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                        }).ToList();


            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 浏览器打印调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult DaYinConverList()
        {

            var Linq = (from tbSellct in MyModels.B_SellRetuerList//返仓单
                        join tbSpuBuMen in MyModels.S_StockPlaceList on tbSellct.StockPlaceID equals tbSpuBuMen.StockPlaceID
                        join tbSpudfdBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpudfdBuMen.SpouseBRanchID
                        where tbSellct.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            SellRetuerID = tbSellct.SellRetuerID,//返仓id
                            SellID = tbSellct.SellID,//销售id
                            Remember = tbSellct.Remember,//返仓单编号
                            StockPlaceName = tbSpuBuMen.StockPlaceName,//收货部门
                            SpouseBRanchName = tbSpudfdBuMen.SpouseBRanchName,//收货部门
                            payName = tbSellct.payName,//实物付
                            furlName = tbSellct.furlName,//实物收
                            Remarks = tbSellct.Remarks,//备注

                            RegisterName = tbSellct.RegisterName,//制单
                            registerTime = tbSellct.RegisterTime.ToString(),//时间
                            ExamineName = tbSellct.ExamineName,//审核
                            examineTime = tbSellct.ExamineTime.ToString(),//时间
                            ExamineNots = tbSellct.ExamineNot.ToString(),//审核否

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
                myWareHouseDeitaLL.StockPlaceName = Linq[i].StockPlaceName;
                myWareHouseDeitaLL.SpouseBRanchName = Linq[i].SpouseBRanchName;
                myWareHouseDeitaLL.payName = Linq[i].payName;
                myWareHouseDeitaLL.furlName = Linq[i].furlName;
                myWareHouseDeitaLL.Remarks = Linq[i].Remarks;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;

                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);

        }


    }
}
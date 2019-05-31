using DaXingShangMaoSystem.LY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DiaoBaDan.Controllers
{
    public class SelectXiaoShouController : Controller
    {
        // GET: DiaoBaDan/SelectXiaoShou
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult SelectXiaoShou()
        {
            return View();
        }


        /// <summary>
        /// 查询销售单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectSellct(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSellct in MyModels.B_SellList//销售单

                        join tbSpuBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpuBuMen.SpouseBRanchID
                        where tbSellct.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            SellID = tbSellct.SellID,//销售id
                            Remember = tbSellct.Remember,//销售单编号
                            SpouseBRanchName = tbSpuBuMen.SpouseBRanchName,//新部门
                            PanlDate = tbSellct.PanlDate.ToString(),//启动计划日期
                            RegisterName = tbSellct.RegisterName,//制单人
                            registerTime = tbSellct.RegisterTime.ToString(),//制单时间

                            ExamineName = tbSellct.ExamineName,//审核人
                            examineTime = tbSellct.ExecuteTime.ToString(),//审核时间
                            QiDongName = tbSellct.QiDongName,//启动人
                            QiDongTime = tbSellct.QiDongTime.ToString(),//启动时间
                            ExamineNot = tbSellct.ExamineNot,//启动时间

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
        /// 查询对应商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectSellctGoods(Vo.BsgridPage bsgridPage, int sellID)
        {
            var linq = (from tbSellct in MyModels.B_SellList//销售单
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        join tbConverlist in MyModels.B_ConverList on tbSellDatelst.ConverID equals tbConverlist.ConverID//配货单
                        join tbJinCang in MyModels.B_WareHouseList on tbConverlist.WareHouseID equals tbJinCang.WareHouseID//进仓
                        join tbHeTong in MyModels.B_OrderFormPactList on tbJinCang.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbGongHuo in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuo.SupplierID//供货
                        where tbSellDatelst.SellID == sellID
                        select new LY.PeiHuoDan
                        {
                            SellID = tbSellct.SellID,//销售id
                            SellDeTaliID = tbSellDatelst.SellDeTaliID,//销售明细id
                            ConverID = tbConverlist.ConverID,//配货id
                            ConverDeTaiID = tbConverDaTe.ConverDeTaiID,//配货明细id
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsChopName = tbShangBiao.GoodsChopName,

                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号
                            SupplierCHName = tbGongHuo.SupplierCHName

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
        /// 浏览器打印
        /// </summary>
        /// <returns></returns>
        public ActionResult DaYinConverList()
        {
            var Linq = (from tbSellct in MyModels.B_SellList//销售单

                        join tbSpuBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpuBuMen.SpouseBRanchID
                        where tbSellct.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            SellID = tbSellct.SellID,//销售id
                            Remember = tbSellct.Remember,//销售单编号
                            SpouseBRanchName = tbSpuBuMen.SpouseBRanchName,//新部门
                            PanlDate = tbSellct.PanlDate.ToString(),//启动计划日期
                            RegisterName = tbSellct.RegisterName,//制单人
                            registerTime = tbSellct.RegisterTime.ToString(),//制单时间

                            ExamineName = tbSellct.ExamineName,//审核人
                            examineTime = tbSellct.ExecuteTime.ToString(),//审核时间
                            QiDongName = tbSellct.QiDongName,//启动人
                            QiDongTime = tbSellct.QiDongTime.ToString(),//启动时间
                            ExamineNots = tbSellct.ExamineNot.ToString(),//启动时间

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
                myWareHouseDeitaLL.PanlDate = Linq[i].PanlDate;
                myWareHouseDeitaLL.Remarks = Linq[i].Remarks;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;

                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                myWareHouseDeitaLL.QiDongName = Linq[i].QiDongName;
                myWareHouseDeitaLL.QiDongTime = Linq[i].QiDongTime;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);

        }


    }
}
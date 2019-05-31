using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.FanCangDan.Controllers
{
    public class FanCangDanController : Controller
    {
        // GET: FanCangDan/FanCangDan
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult FanCang()
        {
            return View();
        }

        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCodef()
        {
            string Std = "";
            var listy = (from tbem in MyModels.B_SellRetuerList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_SellRetuerList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.Remember.Substring(1, 8));
                inemp++;
                Std = inemp.ToString();
                for (int i = 0; i < 8; i++)
                {
                    Std = Std.Length < 8 ? "0" + Std : Std;
                }
            }
            else
            {
                Std = "00000001";
            }
            return Json(Std, JsonRequestBehavior.AllowGet);

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
        /// 绑定销售单商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectSellctGoods(Vo.BsgridPage bsgridPage, int sellIDQuan)
        {
            var linq = (from tbSellct in MyModels.B_SellList//销售单
                        join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbSellDatelst.SellID == sellIDQuan
                        select new LY.PeiHuoDan
                        {
                            SellDeTaliID = tbSellDatelst.SellDeTaliID,//销售明细id
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//
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
        /// 绑定商品到（主界面）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, Array jiLuChaekID)
        {
            List<PeiHuoDan> list = new List<PeiHuoDan>();
            // int goodsIDs
            string Z = ((string[])jiLuChaekID)[0];
            string[] intsZ = Z.Split(',');

            for (int P = 0; P < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[P]);
                var linq = (from tbSellct in MyModels.B_SellList//销售单
                            join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                            join tbBumen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBumen.SpouseBRanchID
                            join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                            join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                            join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                            where tbSellDatelst.SellDeTaliID == goodsIDs
                            select new LY.PeiHuoDan
                            {
                                SellDeTaliID = tbSellDatelst.SellDeTaliID,//销售明细id
                                GoodsIDs = tbGoods.GoodsID,//商品id
                                GoodsCode = tbGoods.GoodsCode,//代码
                                GoodsNames = tbGoods.GoodsName,//商品名称
                                PackContents = tbPackln.PackContent,//包装含量
                                GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                                MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                                RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                                SpecificationsModels = tbGoods.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                ArtNos = tbGoods.ArtNo,//商品货号
                                SpouseBRanchName = tbBumen.SpouseBRanchName,//商品货号
                                TaxBids = tbGoodDetail.TaxBid,//含税进价
                            }).ToList();
                list.AddRange(linq);
                P++;
            }
            int totalRow = list.Count();
            List<LY.PeiHuoDan> notices = list.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 绑定商品到（批量选商品）右
        /// </summary>
        /// <returns></returns>
        public ActionResult PiPeiGoodelright(Vo.BsgridPage bsgridPage, int jiLuChaekID)
        {
           
                var linq = (from tbSellct in MyModels.B_SellList//销售单
                            join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                            join tbBumen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBumen.SpouseBRanchID
                            join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                            join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                            join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                            where tbSellDatelst.SellDeTaliID == jiLuChaekID
                            select new LY.PeiHuoDan
                            {
                                SellDeTaliID = tbSellDatelst.SellDeTaliID,//销售明细id
                                GoodsIDs = tbGoods.GoodsID,//商品id
                                GoodsCode = tbGoods.GoodsCode,//代码
                                GoodsNames = tbGoods.GoodsName,//商品名称
                                PackContents = tbPackln.PackContent,//包装含量
                                GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                                MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                                RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                                SpecificationsModels = tbGoods.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                ArtNos = tbGoods.ArtNo,//商品货号
                                SpouseBRanchName = tbBumen.SpouseBRanchName,//商品货号
                                TaxBids = tbGoodDetail.TaxBid,//含税进价

                            }).ToList();
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.SellDeTaliID).//noboer表达式
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
        /// 随机（自动生成批号）
        /// </summary>
        /// <returns></returns>
        public ActionResult ZiDongPiHao(Array jiLuChaekID)
        {

            List<PeiHuoDan> list = new List<PeiHuoDan>();
            // int goodsIDs
            string Z = ((string[])jiLuChaekID)[0];
            string[] intsZ = Z.Split(',');

            for (int P = 0; P < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[P]);
                var linq = (from tbSellct in MyModels.B_SellList//销售单
                            join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                            join tbBumen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBumen.SpouseBRanchID
                            join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                            join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                            join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                            where tbSellDatelst.SellDeTaliID == goodsIDs
                            select new LY.PeiHuoDan
                            {
                                SellDeTaliID = tbSellDatelst.SellDeTaliID,//销售明细id
                                GoodsIDs = tbGoods.GoodsID,//商品id
                                GoodsCode = tbGoods.GoodsCode,//代码
                                GoodsNames = tbGoods.GoodsName,//商品名称
                                PackContents = tbPackln.PackContent,//包装含量
                                GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                                MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                                RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                                SpecificationsModels = tbGoods.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                ArtNos = tbGoods.ArtNo,//商品货号
                                SpouseBRanchName = tbBumen.SpouseBRanchName,//商品货号
                            }).ToList();
                list.AddRange(linq);
                P++;
            }
            int totalRow = list.Count();
            int ZongShu = totalRow + 1;
            int ShuZiLenht = 1;
            Random randomBuilder = new Random();
            for (int MoRenMiMaa = 1; MoRenMiMaa < ZongShu; MoRenMiMaa++)
            {
                ShuZiLenht = randomBuilder.Next(ZongShu);
                if (ShuZiLenht == 0)
                {
                    return RedirectToAction("ZiDongPiHao");
                }
            }
            return Json(ShuZiLenht, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 开始 新增 保存（返仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectPeiZaiDan(B_SellRetuerList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array PiHaoShuZu)
        {

            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JiShangPiID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JiRuJianShu)[0];
                string[] intsRuJian = M.Split(',');
                //三
                string N = ((string[])JiRuXiShu)[0];
                string[] intsNXiShu = N.Split(',');
                //四
                string C = ((string[])PiHaoShuZu)[0];
                string[] intsNPiHao = C.Split(',');


                
                int oldWareHouseRows = (from tb in MyModels.B_SellRetuerList
                                        where tb.Remember == OK.Remember
                                        select tb).Count();

                if (oldWareHouseRows == 0)
                {
                    B_SellRetuerList MyB_ConverList = new B_SellRetuerList();

                    MyB_ConverList.SellID = OK.SellID;
                    MyB_ConverList.Remember = OK.Remember;
                    MyB_ConverList.payName = OK.payName;
                    MyB_ConverList.furlName = OK.furlName;
                    MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                    MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                   
                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;
                    MyB_ConverList.Remarks = OK.Remarks;

                    MyModels.B_SellRetuerList.Add(MyB_ConverList);
                    if (MyModels.SaveChanges() > 0)
                    {
                        strMag = "succsee";

                        B_SellRetuerDateiList ConverDeTailList = new B_SellRetuerDateiList();

                        for (int H = 0; H < intsNXiShu.Length;)
                        {
                            for (int E = 0; E < intsRuJian.Length;)
                            {
                                for (int d = 0; d < intsid.Length;)
                                {
                                    for (int Pi = 0; Pi < intsNPiHao.Length; Pi++)
                                    {
                                        ConverDeTailList.SellRetuerID = MyB_ConverList.SellRetuerID;//返仓ID
                                        ConverDeTailList.SellDeTaliID = Convert.ToInt32(intsid[d]); ;//商品ID
                                        ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                        ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                        ConverDeTailList.Number = Convert.ToDecimal(intsNPiHao[Pi]);//批号
                                        MyModels.B_SellRetuerDateiList.Add(ConverDeTailList);
                                        MyModels.SaveChanges();//保存
                                        H++;
                                        E++;
                                        d++;
                                    }

                                }
                            }
                        }


                    }
                }
                //return Json(strMag, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag,JsonRequestBehavior.AllowGet);
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
                        where tbSellct.ExamineNot == false
                        select new LY.PeiHuoDan
                        {
                            SellRetuerID = tbSellct.SellRetuerID,//返仓id
                            SellID = tbSellct.SellID,//销售id
                            Remember = tbSellct.Remember,//返仓单编号
                            StockPlaceName = tbSpuBuMen.StockPlaceName,//收货部门
                            payName = tbSellct.payName,//实物付
                            furlName = tbSellct.furlName,//实物收
                            Remarks = tbSellct.Remarks,//备注

                            RegisterName = tbSellct.RegisterName,//制单
                            registerTime = tbSellct.RegisterTime.ToString(),//时间

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



        /// <summary>
        /// 绑定返仓单(一)
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult BangDingellctRect(Vo.BsgridPage bsgridPage,int sellRetuerIDQuan)
        {
            var linq = (from tbSellct in MyModels.B_SellRetuerList//返仓单
                        join tbSpuBuMen in MyModels.S_StockPlaceList on tbSellct.StockPlaceID equals tbSpuBuMen.StockPlaceID
                        join tbSpuBuMesdn in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpuBuMesdn.SpouseBRanchID
                        where tbSellct.SellRetuerID == sellRetuerIDQuan
                        select new LY.PeiHuoDan
                        {
                            SellRetuerID = tbSellct.SellRetuerID,//返仓id
                            SellID = tbSellct.SellID,//销售id
                            Remember = tbSellct.Remember,//返仓单编号
                            StockPlaceID = tbSpuBuMen.StockPlaceID,//收货部门
                            payName = tbSellct.payName,//实物付
                            furlName = tbSellct.furlName,//实物收
                            Remarks = tbSellct.Remarks,//备注
                            SpouseBRanchID=tbSpuBuMesdn.SpouseBRanchID,
                            RegisterName = tbSellct.RegisterName,//制单
                            registerTime = tbSellct.RegisterTime.ToString(),//时间

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        // <summary>
        /// 绑定商品到（主界面）
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
                            join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                            where tbSellct.SellRetuerID == sellRetuerID
                            select new LY.PeiHuoDan
                            {
                                SellRetuerID=tbSellct.SellRetuerID,
                                SellRetuerDateilID = tbSellDatelst.SellRetuerDateilID,//返仓明细id
                                SellDeTaliID = tbXiaoShouMingXi.SellDeTaliID,//销售明细id
                                Number = tbSellDatelst.Number,//批次
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
        /// 开始 修改 保存（返仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptoctuFanDan(B_SellRetuerList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu,Array JieMingXiId,Array MinfXiPiHao)
        {

            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JiShangPiID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JiRuJianShu)[0];
                string[] intsRuJian = M.Split(',');
                //三
                string N = ((string[])JiRuXiShu)[0];
                string[] intsNXiShu = N.Split(',');
                //四
                string idd = ((string[])JieMingXiId)[0];
                string[] intsNXiShuid = idd.Split(',');
                //四
                string PiH = ((string[])MinfXiPiHao)[0];
                string[] insPiHaoind = PiH.Split(',');


                B_SellRetuerList MyB_ConverList = new B_SellRetuerList();

                    MyB_ConverList.SellRetuerID = OK.SellRetuerID;
                    MyB_ConverList.SellID = OK.SellID;
                    MyB_ConverList.Remember = OK.Remember;
                    MyB_ConverList.payName = OK.payName;
                    MyB_ConverList.furlName = OK.furlName;
                    MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                    MyB_ConverList.StockPlaceID = OK.StockPlaceID;

                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;
                    MyB_ConverList.Remarks = OK.Remarks;

                    MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                    if (MyModels.SaveChanges() > 0)
                    {
                        strMag = "succsee";

                        B_SellRetuerDateiList ConverDeTailList = new B_SellRetuerDateiList();

                    for (int H = 0; H < intsNXiShu.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length;)
                            {
                                for (int i = 0; i < intsNXiShuid.Length;)
                                {
                                    for (int Pi = 0; Pi < insPiHaoind.Length; Pi++)
                                    {
                                        ConverDeTailList.SellRetuerDateilID = Convert.ToInt32(intsNXiShuid[i]);//返仓明细ID

                                        ConverDeTailList.SellRetuerID = MyB_ConverList.SellRetuerID;//返仓ID
                                        ConverDeTailList.SellDeTaliID = Convert.ToInt32(intsid[d]);//商品ID
                                        ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                        ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                        ConverDeTailList.Number = Convert.ToDecimal(insPiHaoind[Pi]);//批号
                                        MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                                        MyModels.SaveChanges();//保存
                                        H++;
                                        E++;
                                        d++;
                                        i++;
                                    }
                                }
                            }
                        }
                    }

                }
               
              
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 开始 审核（返仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShneheanDan(B_SellRetuerList OK, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";

            try
            {
                B_SellRetuerList MyB_ConverList = new B_SellRetuerList();

                MyB_ConverList.SellRetuerID = OK.SellRetuerID;
                MyB_ConverList.SellID = OK.SellID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.payName = OK.payName;
                MyB_ConverList.furlName = OK.furlName;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExamineTime = OK.ExamineTime;
                MyB_ConverList.Remarks = OK.Remarks;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";
                    B_SellRetuerList wafrtbool = (from tbbool in MyModels.B_SellRetuerList
                                                  where tbbool.SellRetuerID == MyB_ConverList.SellRetuerID
                                              select tbbool).Single();//查询原状态
                    wafrtbool.ExamineNot = state;//改变是否为冲红单状态
                    MyModels.Entry(wafrtbool).State = EntityState.Modified;


                    if (MyModels.SaveChanges() > 0)//保存
                    {
                        returnJson.State = true;
                        returnJson.Text = "修改成功";
                    }
                    else
                    {
                        returnJson.State = false;
                        returnJson.Text = "修改失败";
                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 删除（返仓单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int sellRetuerID)
        {
            string strMsg = "fail";
            try
            {

                B_SellRetuerList conver = (from tbWarHouser in MyModels.B_SellRetuerList
                                       where tbWarHouser.SellRetuerID == sellRetuerID
                                       select tbWarHouser).Single();
                MyModels.B_SellRetuerList.Remove(conver);

                int waDetialid = (int)conver.SellRetuerID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_SellRetuerDateiList
                                    where tbWarHouserDetial.SellRetuerID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_SellRetuerDateiList.Remove(converDetial[i]);
                        MyModels.SaveChanges();
                        strMsg = "success";
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

    }
}
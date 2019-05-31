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
    public class ZhuanKuDanController : Controller
    {
        // GET: FanCangDan/ZhuanKuDan
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult ZhuanKu()
        {
            return View();
        }


        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCode()
        {
            string Std = "";
            var listy = (from tbem in myModels.B_ChangeList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_ChangeList mymodell = listy[intcoun - 1];
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
        /// 点击 查询 进仓单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult ChaXunJinCang(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSelectWanHtsList in myModels.B_WareHouseList//进仓单ID
                        join tbHeTong in myModels.B_OrderFormPactList on tbSelectWanHtsList.OrderFormPactID equals tbHeTong.OrderFormPactID//合同ID
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbSelectWanHtsList.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//配货部门
                        join tbKuCuDiDian in myModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCuDiDian.StockPlaceID//库存地点
                        where tbSelectWanHtsList.ExamineNot == true && tbSelectWanHtsList.QuFenLeiXingID == 1 && tbSelectWanHtsList.CrushRedNot == false||
                        tbSelectWanHtsList.ExamineNot == true && tbSelectWanHtsList.QuFenLeiXingID == 2 && tbSelectWanHtsList.CrushRedNot == false
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                            Remember = tbSelectWanHtsList.Remember,//进仓编号
                            ContractNumber = tbHeTong.ContractNumber,//合同编号
                            SpouseBRanchName = tbPeiHuoBuMen.SpouseBRanchName,//进货部门
                            StockPlaceName = tbKuCuDiDian.StockPlaceName,//库存地点

                            RegisterName = tbSelectWanHtsList.RegisterName,//
                            RegisterTime = tbSelectWanHtsList.RegisterTime.ToString(),//
                            ExamineName = tbSelectWanHtsList.ExamineName,//
                            ExamineTime = tbSelectWanHtsList.ExamineTime.ToString(),//

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
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
        /// 绑定进仓单
        /// </summary>
        /// <param name="wareHouseID"></param>
        /// <returns></returns>
        public ActionResult BangDingJinCang(int wareHouseID)
        {
            var linq = (from tbSelectWanHtsList in myModels.B_WareHouseList//进仓单ID
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbSelectWanHtsList.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//配货部门
                        join tbKuCuDiDian in myModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCuDiDian.StockPlaceID//库存地点
                        where tbSelectWanHtsList.ExamineNot == true && tbSelectWanHtsList.QuFenLeiXingID == 1 && tbSelectWanHtsList.CrushRedNot == false ||
                        tbSelectWanHtsList.ExamineNot == true && tbSelectWanHtsList.QuFenLeiXingID == 2 && tbSelectWanHtsList.CrushRedNot == false
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                            SpouseBRanchID = tbPeiHuoBuMen.SpouseBRanchID,//进货部门
                            StockPlaceID = tbKuCuDiDian.StockPlaceID,//库存地点

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询对应商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunShangPin(Vo.BsgridPage bsgridPage, int wareHouseID)
        {
            var Linq = (from tbwangHdID in myModels.B_WareHouseList
                        join tbWareHouseDetiai in myModels.B_WareHouseDetiailList on tbwangHdID.WareHouseID equals tbWareHouseDetiai.WareHouseID//进仓明细
                        join tbSelectGoods in myModels.B_GoodsList on tbWareHouseDetiai.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                        join tbJiSuanDan in myModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWareHouseDetiai.WareHouseID == wareHouseID
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbwangHdID.WareHouseID,
                            WareHouseDetiailID =tbWareHouseDetiai.WareHouseDetiailID,
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
                            Subdivision = tbWareHouseDetiai.Subdivision//入库小数


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
        /// 绑定商品到（主界面）批量选（一）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, Array jiLuChaekID)
        {
            List<WareHouseDeitaLL> list = new List<WareHouseDeitaLL>();
            // int goodsIDs
            string Z = ((string[])jiLuChaekID)[0];
            string[] intsZ = Z.Split(',');

            for (int P = 0; P < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[P]);
                var linq = (from tbWareHouseDetiai in myModels.B_WareHouseDetiailList//进仓明细
                            join tbSelectGoods in myModels.B_GoodsList on tbWareHouseDetiai.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                            join tbJiSuanDan in myModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                            join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                            where tbWareHouseDetiai.WareHouseDetiailID == goodsIDs
                            select new LY.WareHouseDeitaLL
                            {
                              
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
                                Subdivision = tbWareHouseDetiai.Subdivision//入库小数


                            }).ToList();
                list.AddRange(linq);
                P++;
            }
            int totalRow = list.Count();
            List<LY.WareHouseDeitaLL> notices = list.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 绑定商品到（主界面）全部选（二）
        /// </summary>
        /// <returns></returns>
        public ActionResult QuanBangDingPiGoodel(Vo.BsgridPage bsgridPage,int waretyuseID)
        {
            var Linq = (from tbWareHous in myModels.B_WareHouseList
                        join tbWareHouseDetiai in myModels.B_WareHouseDetiailList on tbWareHous.WareHouseID equals tbWareHouseDetiai.WareHouseID//进仓明细
                        join tbSelectGoods in myModels.B_GoodsList on tbWareHouseDetiai.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                        join tbJiSuanDan in myModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWareHouseDetiai.WareHouseID== waretyuseID
                        select new LY.WareHouseDeitaLL
                        {

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
                            Subdivision = tbWareHouseDetiai.Subdivision//入库小数


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
        /// 开始 新增 保存（转库单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectPeiZaiDan(B_ChangeList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu)
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


                int oldWareHouseRows = (from tb in myModels.B_ChangeList
                                        where tb.Remember == OK.Remember
                                        select tb).Count();

                if (oldWareHouseRows == 0)
                {
                    B_ChangeList MyB_ConverList = new B_ChangeList();
                    MyB_ConverList.WareHouseID = OK.WareHouseID;
                    MyB_ConverList.Remember = OK.Remember;
                    MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                    MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                    MyB_ConverList.StockPlaceIDtwo = OK.StockPlaceIDtwo;
                    MyB_ConverList.payName = OK.payName;
                    MyB_ConverList.furlName = OK.furlName;
                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;
                    MyB_ConverList.ShiFouGou = OK.ShiFouGou;

                    myModels.B_ChangeList.Add(MyB_ConverList);
                    if (myModels.SaveChanges() > 0)
                    {
                        strMag = "succsee";

                        B_ChangeDetailList ConverDeTailList = new B_ChangeDetailList();

                        for (int H = 0; H < intsNXiShu.Length;)
                        {
                            for (int E = 0; E < intsRuJian.Length;)
                            {
                                for (int d = 0; d < intsid.Length; d++)
                                {

                                    ConverDeTailList.ChangeID = MyB_ConverList.ChangeID;//返仓ID
                                    ConverDeTailList.WareHouseDetiailID = Convert.ToInt32(intsid[d]); ;//进仓明细ID
                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数

                                    myModels.B_ChangeDetailList.Add(ConverDeTailList);
                                    myModels.SaveChanges();//保存
                                    H++;
                                    E++;
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
                        where tbZhuanKu.ExamineNot == false
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
                            registerTime = tbZhuanKu.RegisterTime.ToString()

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
        /// 绑定转库单（一）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult BangDingZhuanKu(int changeID)
        {
            var linq = (from tbZhuanKu in myModels.B_ChangeList//转库单ID
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbZhuanKu.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//部门
                        join tbKuCuDiDian in myModels.S_StockPlaceList on tbZhuanKu.StockPlaceID equals tbKuCuDiDian.StockPlaceID//原库存地点
                        join tbNewKuCuDiDian in myModels.S_StockPlaceList on tbZhuanKu.StockPlaceIDtwo equals tbNewKuCuDiDian.StockPlaceID//新库存地点
                        where tbZhuanKu.ChangeID == changeID
                        select new LY.PeiHuoDan
                        {
                            ChangeID = tbZhuanKu.ChangeID,
                            Remember = tbZhuanKu.Remember,
                            SpouseBRanchID = tbPeiHuoBuMen.SpouseBRanchID,
                            StockPlaceID = tbKuCuDiDian.StockPlaceID,
                            StockPlaceIDtwo = tbNewKuCuDiDian.StockPlaceID,
                            payName = tbZhuanKu.payName,
                            furlName = tbZhuanKu.furlName,
                            RegisterName = tbZhuanKu.RegisterName,
                            registerTime = tbZhuanKu.RegisterTime.ToString(),
                            ShiFouGou = tbZhuanKu.ShiFouGou,

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
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
                            Subdivisionkl= tbChangeDetiai.Subdivision,


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
        /// 开始 修改 保存（转库单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptoctPeiZaiDan(B_ChangeList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array JiZhuanidShu)
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
                //四id
                string idc = ((string[])JiZhuanidShu)[0];
                string[] intsidcXiShu = idc.Split(',');


                B_ChangeList MyB_ConverList = new B_ChangeList();
                MyB_ConverList.ChangeID = OK.ChangeID;
                MyB_ConverList.WareHouseID = OK.WareHouseID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                MyB_ConverList.StockPlaceIDtwo = OK.StockPlaceIDtwo;
                MyB_ConverList.payName = OK.payName;
                MyB_ConverList.furlName = OK.furlName;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.ShiFouGou = OK.ShiFouGou;

                myModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMag = "succsee";

                    B_ChangeDetailList ConverDeTailList = new B_ChangeDetailList();

                    for (int H = 0; H < intsNXiShu.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length;)
                            {
                                for (int idx = 0; idx < intsidcXiShu.Length; idx++)
                                {
                                    ConverDeTailList.ChangeDetailID = Convert.ToInt32(intsidcXiShu[idx]);//转库ID
                                    ConverDeTailList.ChangeID = MyB_ConverList.ChangeID;//返仓ID
                                    ConverDeTailList.WareHouseDetiailID = Convert.ToInt32(intsid[d]); ;//进仓明细ID
                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                    myModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                                    myModels.SaveChanges();//保存
                                    d++;
                                    H++;
                                    E++;
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
        /// 开始 修改 保存（转库单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenheaiDan(B_ChangeList OK, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";
            try
            {
                B_ChangeList MyB_ConverList = new B_ChangeList();
                MyB_ConverList.ChangeID = OK.ChangeID;
                MyB_ConverList.WareHouseID = OK.WareHouseID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                MyB_ConverList.StockPlaceIDtwo = OK.StockPlaceIDtwo;
                MyB_ConverList.payName = OK.payName;
                MyB_ConverList.furlName = OK.furlName;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.ShiFouGou = OK.ShiFouGou;
                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExamineTime = OK.ExamineTime;

                myModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();

                B_ChangeList wafrtbool = (from tbbool in myModels.B_ChangeList
                                          where tbbool.ChangeID == MyB_ConverList.ChangeID
                                             select tbbool).Single();//查询原状态
                wafrtbool.ExamineNot = state;
                myModels.Entry(wafrtbool).State = EntityState.Modified;

                if (myModels.SaveChanges() > 0)//保存
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 删除（转库单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteChanger(int changeID)
        {
            string strMsg = "fail";
            try
            {

                B_ChangeList conver = (from tbWarHouser in myModels.B_ChangeList
                                       where tbWarHouser.ChangeID == changeID
                                       select tbWarHouser).Single();
                myModels.B_ChangeList.Remove(conver);

                int waDetialid = (int)conver.ChangeID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in myModels.B_ChangeDetailList
                                    where tbWarHouserDetial.ChangeID == changeID
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        myModels.B_ChangeDetailList.Remove(converDetial[i]);
                        myModels.SaveChanges();
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
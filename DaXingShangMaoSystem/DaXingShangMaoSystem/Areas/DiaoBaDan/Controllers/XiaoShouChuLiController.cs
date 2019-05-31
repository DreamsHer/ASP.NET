using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DiaoBaDan.Controllers
{
    public class XiaoShouChuLiController : Controller
    {
        // GET: DiaoBaDan/XiaoShouChuLi
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult XiaoShouDan()
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
            var listy = (from tbem in MyModels.B_SellList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_SellList mymodell = listy[intcoun - 1];
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
        /// 商品分类（下拉）
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodFeiLei()
        {

           List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tbGooFeiLeiLiet in MyModels.B_GoodsClassifyList
                                                 where tbGooFeiLeiLiet.F_GoodsClassifyID == 0
                                                 select new SelectXiaLa
                                                 {
                                                     id = tbGooFeiLeiLiet.GoodsClassifyID,
                                                     text = tbGooFeiLeiLiet.GoodsClassifyName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 查询配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectConverList(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID
                        where tbConverlist.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            P_Remember = tbConverlist.P_Remember,//编号
                            payName = tbConverlist.payName,
                            furlName = tbConverlist.furlName,
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,
                            StockPlaceName = tbShouHuoBuMen.StockPlaceName,
                            Remarks = tbConverlist.Remarks
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
        /// 绑定配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPeiHuo(int converID)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbShouHuoBuMen in MyModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID

                        where tbConverlist.ConverID == converID
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            StockPlaceID = tbShouHuoBuMen.StockPlaceID,
                          
                        }).ToList();

          
            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 查询商标
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectBangDingPeiHuo(Vo.BsgridPage bsgridPage, int stockPlaceID,int converID)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbConverlist.ConverID equals tbConverDaTe.ConverID//配货明细
                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        where tbConverlist.StockPlaceID == stockPlaceID && tbConverlist.ConverID== converID
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
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
        /// 绑定商标
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult BangDingDingPeiHuo(int converID)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbConverlist.ConverID equals tbConverDaTe.ConverID//配货明细
                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        where tbConverlist.ConverID == converID
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            GoodsChopName = tbShangBiao.GoodsChopName,

                        }).ToList();
         
            return Json(linq, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        ///（通过）绑定配货单。查商品（商标、商品）第一种
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBangDingZhuJie(Vo.BsgridPage bsgridPage, int stockPlaceID, string goodsChopName)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbConverlist.ConverID equals tbConverDaTe.ConverID//配货明细
                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        join tbJinCang in MyModels.B_WareHouseList on tbConverlist.WareHouseID equals tbJinCang.WareHouseID//进仓
                        join tbHeTong in MyModels.B_OrderFormPactList on tbJinCang.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbGongHuo in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuo.SupplierID//供货

                        where tbConverlist.StockPlaceID == stockPlaceID && tbShangBiao.GoodsChopName == goodsChopName
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            ConverDeTaiID = tbConverDaTe.ConverDeTaiID,//id
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                          
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号
                            SupplierCHName = tbGongHuo.SupplierCHName,//商品货号

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
        /// （通过）绑定配货单。查商品（商标、商品）第二种
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBangDingZhuJieXinXi(Vo.BsgridPage bsgridPage, int stockPlaceID, string goodsChopName, string goodsName, string goodsCode, string supplierCHName)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbConverDaTe in MyModels.B_ConverDeTailList on tbConverlist.ConverID equals tbConverDaTe.ConverID//配货明细
                        join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        join tbJinCang in MyModels.B_WareHouseList on tbConverlist.WareHouseID equals tbJinCang.WareHouseID//进仓
                        join tbHeTong in MyModels.B_OrderFormPactList on tbJinCang.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbGongHuo in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuo.SupplierID//供货

                        where tbConverlist.StockPlaceID == stockPlaceID && tbShangBiao.GoodsChopName == goodsChopName && tbGoods.GoodsName== goodsName &&
                        tbGoods.GoodsCode== goodsCode && tbGongHuo.SupplierCHName== supplierCHName

                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            ConverDeTaiID = tbConverDaTe.ConverDeTaiID,//id
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码

                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号


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
        /// 新增 保存（销售单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InterDiaoBa(B_SellList OK, Array JieShouID, Array JieShouJianShu)
        {
            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JieShouID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JieShouJianShu)[0];
                string[] intsRuJian = M.Split(',');

                B_SellList MyB_ConverList = new B_SellList();

                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.PanlDate = OK.PanlDate;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;


                MyModels.B_SellList.Add(MyB_ConverList);

                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";

                    B_SellDeTaLsit ConverDeTailList = new B_SellDeTaLsit();

                    for (int H = 0; H < intsid.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;E++)
                        {
                            ConverDeTailList.SellID = MyB_ConverList.SellID;
                            ConverDeTailList.ConverDeTaiID = Convert.ToInt32(intsid[H]);//配货ID
                            ConverDeTailList.ConverID = Convert.ToInt32(intsRuJian[E]);//配货明细ID
                            MyModels.B_SellDeTaLsit.Add(ConverDeTailList);
                            MyModels.SaveChanges();//保存
                            H++;
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
        /// 查询销售单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectSellct(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSellct in MyModels.B_SellList//销售单

                        join tbSpuBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpuBuMen.SpouseBRanchID
                        where tbSellct.ExamineNot==false
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
        /// 绑定销售单（一）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult BangDingSellct(int sellID)
        {
            var linq = (from tbSellct in MyModels.B_SellList//销售单
                     
                        join tbSpuBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpuBuMen.SpouseBRanchID
                        where tbSellct.SellID == sellID
                        select new LY.PeiHuoDan
                        {
                            SellID = tbSellct.SellID,//销售id

                            Remember = tbSellct.Remember,//销售单编号
                            SpouseBRanchID = tbSpuBuMen.SpouseBRanchID,//新部门
                            PanlDate = tbSellct.PanlDate.ToString(),//启动计划日期
                            RegisterName = tbSellct.RegisterName,//制单人
                            registerTime = tbSellct.RegisterTime.ToString(),//制单时间

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 绑定销售单商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectSellctGoods(Vo.BsgridPage bsgridPage,int sellID)
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
                        where tbSellDatelst.SellID== sellID
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
                            SupplierCHName=tbGongHuo.SupplierCHName

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
        /// 修改 保存（销售单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptuSelllist(B_SellList OK, Array JieShouID, Array JieShouJianShu,Array JieSellDateIDShu)
        {
            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JieShouID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JieShouJianShu)[0];
                string[] intsRuJian = M.Split(',');

                //三
                string B = ((string[])JieSellDateIDShu)[0];
                string[] intsSellDeTal = B.Split(',');

                B_SellList MyB_ConverList = new B_SellList();
                MyB_ConverList.SellID = OK.SellID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.PanlDate = OK.PanlDate;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;

                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";

                    B_SellDeTaLsit ConverDeTailList = new B_SellDeTaLsit();

                    for (int H = 0; H < intsid.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int S = 0; S < intsSellDeTal.Length; S++)
                            {
                                ConverDeTailList.SellID = MyB_ConverList.SellID;
                                ConverDeTailList.ConverDeTaiID = Convert.ToInt32(intsid[H]);//配货ID
                                ConverDeTailList.ConverID = Convert.ToInt32(intsRuJian[E]);//配货明细ID
                                ConverDeTailList.SellDeTaliID = Convert.ToInt32(intsSellDeTal[S]);//配货明细ID
                                MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                                MyModels.SaveChanges();//保存
                                E++;
                                H++;
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
        /// 审核（销售单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenHeSelllist(B_SellList OK,bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";

            try
            {
                B_SellList MyB_ConverList = new B_SellList();
                MyB_ConverList.SellID = OK.SellID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.PanlDate = OK.PanlDate;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExecuteTime = OK.ExecuteTime;

                MyB_ConverList.QiDongName = OK.QiDongName;
                MyB_ConverList.QiDongTime = OK.QiDongTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;

                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";
                    //审核
                    B_SellList wafrtbool = (from tbbool in MyModels.B_SellList
                                            where tbbool.SellID == MyB_ConverList.SellID
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
                    //启动
                    B_SellList wafrtboolf = (from tbbool in MyModels.B_SellList
                                            where tbbool.SellID == MyB_ConverList.SellID
                                            select tbbool).Single();//查询原状态
                    wafrtboolf.QiDongmineNot = state;//改变是否为冲红单状态
                    MyModels.Entry(wafrtboolf).State = EntityState.Modified;

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
        /// 删除（销售单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int sellID)
        {
            string strMsg = "fail";
            try
            {

                B_SellList conver = (from tbWarHouser in MyModels.B_SellList
                                     where tbWarHouser.SellID == sellID
                                     select tbWarHouser).Single();
                MyModels.B_SellList.Remove(conver);

                int waDetialid = (int)conver.SellID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_SellDeTaLsit
                                    where tbWarHouserDetial.SellID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_SellDeTaLsit.Remove(converDetial[i]);
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
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.PanDianShuYiChuLi.Controllers
{
    public class PanlYuShenController : Controller
    {
        // GET: PanDianShuYiChuLi/PanlYuShen
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult YuShenChuLi()
        {
            return View();
        }


        // <summary>
        ///查询盘点计划(记录列表)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckgh(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.Entering == true
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,//盘点ID
                            Remember = tbGoodBiao.Remter,//编号
                            PablData = tbGoodBiao.PablData.ToString(),//盘点日期
                            CommodityType = tbGoodBiao.CommodityType,//类型
                            DrugType = tbGoodBiao.DrugType,//部门
                            StockPlaceName = tbStock.StockPlaceName,//库存
                            Entering = tbGoodBiao.Entering,//状态

                            StockPlaceID = tbStock.StockPlaceID,//库存ID
                            OrderFormPactID = tbGoodBiao.OrderFormPactID,//合同ID
                            GoodsClassifyID = tbGoodBiao.GoodsClassifyID,//商品分类
                            GoodsChopID = tbGoodBiao.GoodsChopID,//商标
                            ExamineName = tbGoodBiao.ExamineName,//登记人
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),//登记时间
                            EnteringTime = tbGoodBiao.EnteringTime,//录入时间

                            SupplierID = tbGoodBiao.DrugTypeID,//盘点部门ID
                            staffId = tbGoodBiao.StaffID,//
                            TaxBids = tbGoodBiao.YingKuiMoney,//

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
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



        // <summary>
        ///查询盘点计划(记录列表)
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingCheckgh(int checkRermeberID)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.CheckRermeberID == checkRermeberID
                        select new LY.WareHouseDeitaLL
                        {


                            CheckRermeberID = tbGoodBiao.CheckRermeberID,//盘点ID
                            Remember = tbGoodBiao.Remter,//编号
                            PablData = tbGoodBiao.PablData.ToString(),//盘点日期
                            DrugType = tbGoodBiao.DrugType,//部门

                            YingKuiMoney = tbGoodBiao.YingKuiMoney,//盈亏金额
                           
                        }).ToList();
           
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        ///查询(绑定)盘点计划(商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsffGoodsj(Vo.BsgridPage bsgridPage, int checkRermeberID)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChredgRe.CheckRermeberID == checkRermeberID
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbChredgRe.CheckRermeberID,
                            CheckRemerbenDetillD = tbChredgReDateil.CheckRemerbenDetillD,
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            ArtNos = tbGoods.ArtNo,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            Subdivision = tbChredgReDateil.Subdivision,//细数
                            MumberOfPackages = tbChredgReDateil.MumberOfPackages//件数
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();
            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        ///查询(绑定)盘点计划(商品)右
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsffGoodsjRight(Vo.BsgridPage bsgridPage, int checkRemerbenDetillD)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChredgReDateil.CheckRemerbenDetillD == checkRemerbenDetillD
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbChredgRe.CheckRermeberID,
                            CheckRemerbenDetillD = tbChredgReDateil.CheckRemerbenDetillD,
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            ArtNos = tbGoods.ArtNo,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            Subdivision = tbChredgReDateil.Subdivision,//细数
                            MumberOfPackages = tbChredgReDateil.MumberOfPackages,//件数
                            PiCiHao = tbChredgReDateil.PiCiHao,//批号
                          
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();
            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }



        // <summary>
        ///保存设置完成
        /// </summary>
        /// <returns></returns>
        public ActionResult SheDingCheckgh(B_CheckRemerbenList OK, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";
            try
            {
                B_CheckRemerbenList ConverDeTailList = new B_CheckRemerbenList();

                ConverDeTailList.CheckRermeberID = OK.CheckRermeberID;//盘点ID
                ConverDeTailList.Remter = OK.Remter;//盘点ID
                ConverDeTailList.PablData = OK.PablData;//盘点ID
                ConverDeTailList.CommodityType = OK.CommodityType;//盘点ID
                ConverDeTailList.DrugType = OK.DrugType;//盘点ID
                ConverDeTailList.StockPlaceID = OK.StockPlaceID;//盘点ID
                ConverDeTailList.OrderFormPactID = OK.OrderFormPactID;//盘点ID
                ConverDeTailList.GoodsClassifyID = OK.GoodsClassifyID;//盘点ID
                ConverDeTailList.GoodsChopID = OK.GoodsChopID;//盘点ID
                ConverDeTailList.ExamineName = OK.ExamineName;//盘点ID
                ConverDeTailList.ExamineTime = OK.ExamineTime;//盘点ID
                ConverDeTailList.EnteringTime = OK.EnteringTime;//录入时间
                ConverDeTailList.YingKuiMoney = OK.YingKuiMoney;//盈亏金额
                ConverDeTailList.StaffID = OK.StaffID;//员工

                ConverDeTailList.DrugTypeID = OK.DrugTypeID;//
              

                MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;

                if (MyModels.SaveChanges() > 0)
                {
                    //保存
                    strMag = "succsee";

                    //
                    B_CheckRemerbenList wafrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                     where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                     select tbbool).Single();//查询原状态
                    wafrtbool.Entering = state;//改变是否为状态
                    MyModels.Entry(wafrtbool).State = EntityState.Modified;

                    B_CheckRemerbenList Goofrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                      where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                      select tbbool).Single();//查询原状态
                    Goofrtbool.GoodsNot = state;//改变是否为商品状态
                    MyModels.Entry(Goofrtbool).State = EntityState.Modified;


                    B_CheckRemerbenList YuShenrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                      where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                      select tbbool).Single();//查询原状态
                    YuShenrtbool.YuShenNot = state;//改变是否为商品状态
                    MyModels.Entry(YuShenrtbool).State = EntityState.Modified;

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



        // <summary>
        ///取消预审
        /// </summary>
        /// <returns></returns>
        public ActionResult QuSheDingCheckgh(B_CheckRemerbenList OK, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";
            try
            {
                B_CheckRemerbenList ConverDeTailList = new B_CheckRemerbenList();

                ConverDeTailList.CheckRermeberID = OK.CheckRermeberID;//盘点ID
                ConverDeTailList.Remter = OK.Remter;//盘点ID
                ConverDeTailList.PablData = OK.PablData;//盘点ID
                ConverDeTailList.CommodityType = OK.CommodityType;//盘点ID
                ConverDeTailList.DrugType = OK.DrugType;//盘点ID
                ConverDeTailList.StockPlaceID = OK.StockPlaceID;//盘点ID
                ConverDeTailList.OrderFormPactID = OK.OrderFormPactID;//盘点ID
                ConverDeTailList.GoodsClassifyID = OK.GoodsClassifyID;//盘点ID
                ConverDeTailList.GoodsChopID = OK.GoodsChopID;//盘点ID
                ConverDeTailList.ExamineName = OK.ExamineName;//盘点ID
                ConverDeTailList.ExamineTime = OK.ExamineTime;//盘点ID
                ConverDeTailList.EnteringTime = OK.EnteringTime;//录入时间

                ConverDeTailList.DrugTypeID = OK.DrugTypeID;//
                ConverDeTailList.StaffID = OK.StaffID;//
                ConverDeTailList.YingKuiMoney = OK.YingKuiMoney;//

                MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;

                if (MyModels.SaveChanges() > 0)
                {
                    //保存
                    strMag = "succsee";

                    //
                    B_CheckRemerbenList wafrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                     where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                     select tbbool).Single();//查询原状态
                    wafrtbool.Entering = state;//改变是否为状态
                    MyModels.Entry(wafrtbool).State = EntityState.Modified;

                    B_CheckRemerbenList Goofrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                      where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                      select tbbool).Single();//查询原状态
                    Goofrtbool.GoodsNot = state;//改变是否为商品状态
                    MyModels.Entry(Goofrtbool).State = EntityState.Modified;


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




        //审核部分
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult ShenHePanl()
        {
            return View();
        }


        // <summary>
        ///查询盘点计划(记录列表)
        /// </summary>
        /// <returns></returns>
        public ActionResult YuSelectCheckgh(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        join tbStall in MyModels.B_StaffList on tbGoodBiao.StaffID equals tbStall.StaffID
                        where tbGoodBiao.YuShenNot == true
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,//盘点ID
                            Remember = tbGoodBiao.Remter,//编号
                            PablData = tbGoodBiao.PablData.ToString(),//盘点日期
                            CommodityType = tbGoodBiao.CommodityType,//类型
                            DrugType = tbGoodBiao.DrugType,//部门
                            StockPlaceName = tbStock.StockPlaceName,//库存
                            Entering = tbGoodBiao.Entering,//状态

                            StockPlaceID = tbStock.StockPlaceID,//库存ID
                            OrderFormPactID = tbGoodBiao.OrderFormPactID,//合同ID
                            GoodsClassifyID = tbGoodBiao.GoodsClassifyID,//商品分类
                            GoodsChopID = tbGoodBiao.GoodsChopID,//商标
                            ExamineName = tbGoodBiao.ExamineName,//登记人
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),//登记时间
                            EnteringTime = tbGoodBiao.EnteringTime,//录入时间

                            YingKuiMoney = tbGoodBiao.YingKuiMoney,//盈亏数
                            StaffID = tbStall.StaffID,//员工ID

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
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


        ///查询(绑定)盘点计划(商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult SunYintCheckewsffGoodsj(Vo.BsgridPage bsgridPage, int checkRermeberIDQuan)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChredgRe.CheckRermeberID == checkRermeberIDQuan
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbChredgRe.CheckRermeberID,
                            CheckRemerbenDetillD = tbChredgReDateil.CheckRemerbenDetillD,
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            ArtNos = tbGoods.ArtNo,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            Subdivision = tbChredgReDateil.Subdivision,//细数
                            MumberOfPackages = tbChredgReDateil.MumberOfPackages//件数
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();
            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }



        // <summary>
        ///审核完成
        /// </summary>
        /// <returns></returns>
        public ActionResult SheDingPanl(B_CheckRemerbenList OK, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";
            try
            {
                B_CheckRemerbenList ConverDeTailList = new B_CheckRemerbenList();

                ConverDeTailList.CheckRermeberID = OK.CheckRermeberID;//盘点ID
                ConverDeTailList.Remter = OK.Remter;//盘点ID
                ConverDeTailList.PablData = OK.PablData;//盘点ID
                ConverDeTailList.CommodityType = OK.CommodityType;//盘点ID
                ConverDeTailList.DrugType = OK.DrugType;//盘点ID
                ConverDeTailList.StockPlaceID = OK.StockPlaceID;//盘点ID
                ConverDeTailList.OrderFormPactID = OK.OrderFormPactID;//盘点ID
                ConverDeTailList.GoodsClassifyID = OK.GoodsClassifyID;//盘点ID
                ConverDeTailList.GoodsChopID = OK.GoodsChopID;//盘点ID
                ConverDeTailList.ExamineName = OK.ExamineName;//盘点ID
                ConverDeTailList.ExamineTime = OK.ExamineTime;//盘点ID
                ConverDeTailList.EnteringTime = OK.EnteringTime;//录入时间
                ConverDeTailList.YingKuiMoney = OK.YingKuiMoney;//盈亏金额
                ConverDeTailList.StaffID = OK.StaffID;//员工
                ConverDeTailList.ShenHeName = OK.ShenHeName;//审核人

                MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;

                if (MyModels.SaveChanges() > 0)
                {
                    //保存
                    strMag = "succsee";

                    //
                    B_CheckRemerbenList wafrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                     where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                     select tbbool).Single();//查询原状态
                    wafrtbool.Entering = state;//改变是否为状态
                    MyModels.Entry(wafrtbool).State = EntityState.Modified;

                    B_CheckRemerbenList Goofrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                      where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                      select tbbool).Single();//查询原状态
                    Goofrtbool.GoodsNot = state;//改变是否为商品状态
                    MyModels.Entry(Goofrtbool).State = EntityState.Modified;


                    B_CheckRemerbenList YuShenrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                        where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                        select tbbool).Single();//查询原状态
                    YuShenrtbool.YuShenNot = state;//改变是否为商品状态
                    MyModels.Entry(YuShenrtbool).State = EntityState.Modified;

                    B_CheckRemerbenList ShYuShenrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                        where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                        select tbbool).Single();//查询原状态
                    ShYuShenrtbool.ExamineNot = state;//改变是否为商品状态
                    MyModels.Entry(ShYuShenrtbool).State = EntityState.Modified;

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



        //删除部分
        /// <summary>
        /// 视图
        /// </summary>
        /// <returns></returns>
        public ActionResult DelectShenHePanl()
        {
            return View();
        }

        // <summary>
        ///记录列表(查询审核通过)
        /// <returns></returns>
        public ActionResult SelectCheckghShen(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.ExamineNot == true 
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,//盘点ID
                            Remember = tbGoodBiao.Remter,//编号
                            PablData = tbGoodBiao.PablData.ToString(),//盘点日期
                            CommodityType = tbGoodBiao.CommodityType,//类型
                            DrugType = tbGoodBiao.DrugType,//部门
                            StockPlaceName = tbStock.StockPlaceName,//库存
                            Entering = tbGoodBiao.Entering,//状态

                            StockPlaceID = tbStock.StockPlaceID,//库存ID
                            OrderFormPactID = tbGoodBiao.OrderFormPactID,//合同ID
                            GoodsClassifyID = tbGoodBiao.GoodsClassifyID,//商品分类
                            GoodsChopID = tbGoodBiao.GoodsChopID,//商标
                            ExamineName = tbGoodBiao.ExamineName,//登记人
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),//登记时间
                            EnteringTime = tbGoodBiao.EnteringTime,//录入时间

                            RegisterName = tbGoodBiao.ShenHeName,//

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
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
        /// 删除（盘点）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int checkRermeberIDQuan)
        {
            string strMsg = "fail";
            try
            {

                B_CheckRemerbenList conver = (from tbWarHouser in MyModels.B_CheckRemerbenList
                                              where tbWarHouser.CheckRermeberID == checkRermeberIDQuan
                                              select tbWarHouser).Single();
                MyModels.B_CheckRemerbenList.Remove(conver);

                int waDetialid = (int)conver.CheckRermeberID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_CheckRemerbenDetillList
                                    where tbWarHouserDetial.CheckRermeberID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_CheckRemerbenDetillList.Remove(converDetial[i]);
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
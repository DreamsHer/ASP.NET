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
    public class LuRuBiaoZhiController : Controller
    {
        // GET: PanDianShuYiChuLi/LuRuBiaoZhi
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        //选择报告视图一
        public ActionResult XuanZeBaoGao()
        {
            return View();
        }

        //一
        // <summary>
        ///查询盘点计划(记录列表)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckgh(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.GoodsNot == true
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
        ///时间
        /// </summary>
        public ActionResult SelectTime()
        {

            string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");//获取当前时间

            return Json(strTime, JsonRequestBehavior.AllowGet);
        }



        // <summary>
        ///保存设置完成
        /// </summary>
        /// <returns></returns>
        public ActionResult SheDingCheckgh(B_CheckRemerbenList OK,bool state)
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

                if (MyModels.SaveChanges()>0)
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




        // <summary>
        ///取消设置完成
        /// </summary>
        /// <returns></returns>
        public ActionResult QuXiaogCheckgh(B_CheckRemerbenList OK, bool state)
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

                ConverDeTailList.DrugTypeID = OK.DrugTypeID;//
                ConverDeTailList.StaffID = OK.StaffID;//
                ConverDeTailList.YingKuiMoney = OK.YingKuiMoney;//


                MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;

                if (MyModels.SaveChanges() > 0)
                {
                    //保存
                    strMag = "succsee";

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




        //一
        // <summary>
        ///条件查询盘点计划(数据)
        /// </summary>
        /// <returns></returns>
        public ActionResult TiaoJianSelectCheckgh(Vo.BsgridPage bsgridPage,string remter)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.Remter == remter
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


    }
}
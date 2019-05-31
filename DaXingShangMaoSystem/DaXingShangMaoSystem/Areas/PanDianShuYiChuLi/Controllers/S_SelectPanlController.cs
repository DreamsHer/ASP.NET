using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.PanDianShuYiChuLi.Controllers
{
    public class S_SelectPanlController : Controller
    {
        // GET: PanDianShuYiChuLi/S_SelectPanl
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult SelectPanl()
        {
            return View();
        }


        /// <summary>
        /// 查询盘点表
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectHoaSun(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbCheckRemerben in myModels.B_CheckRemerbenList//计划盘点表
                        join tbPanlBuMen in myModels.B_PanlList on tbCheckRemerben.DrugTypeID equals tbPanlBuMen.DrugTypeID//盘点部门表 
                        join tbDerterm in myModels.B_DepatmenList on tbPanlBuMen.DrugTypeID equals tbDerterm.DrugTypeID
                     

                      

                        where tbCheckRemerben.Entering==true
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbCheckRemerben.CheckRermeberID,//
                            Remter = tbCheckRemerben.Remter,//
                            EnteringTime = tbCheckRemerben.PablData.ToString(),//
                            RegisterTime = tbCheckRemerben.EnteringTime.ToString(),//
                            departmentCodes = tbDerterm.departmentCodes,//
                            departmentName = tbDerterm.departmentName,//
                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).
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
        /// 查询盘点表(条件)
        /// </summary>
        /// <returns></returns>
        public ActionResult TiaoSelectHoaSun(Vo.BsgridPage bsgridPage,DateTime chertempatd)
        {
            var linq = (from tbCheckRemerben in myModels.B_CheckRemerbenList//计划盘点表
                        join tbPanlBuMen in myModels.B_PanlList on tbCheckRemerben.DrugTypeID equals tbPanlBuMen.DrugTypeID//盘点部门表 
                        join tbDerterm in myModels.B_DepatmenList on tbPanlBuMen.DrugTypeID equals tbDerterm.DrugTypeID
                        where tbCheckRemerben.Entering == true && tbCheckRemerben.PablData== chertempatd
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbCheckRemerben.CheckRermeberID,//
                            Remter = tbCheckRemerben.Remter,//
                            EnteringTime = tbCheckRemerben.PablData.ToString(),//
                            RegisterTime = tbCheckRemerben.EnteringTime.ToString(),//
                            departmentCodes = tbDerterm.departmentCodes,//
                            departmentName = tbDerterm.departmentName,//
                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).
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


        ///查询商品
        /// 
        public ActionResult SelectGoods(Vo.BsgridPage bsgridPage,int checkRermeberID)
        {
            var linq = (from tbCheckRemerben in myModels.B_CheckRemerbenList//计划盘点表
                        join tbtbCheckReDetill in myModels.B_CheckRemerbenDetillList on tbCheckRemerben.CheckRermeberID equals tbtbCheckReDetill.CheckRermeberID//计划盘点明细表
                        join tbGoods in myModels.B_GoodsList on tbtbCheckReDetill.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbShangBiao in myModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        join tbFenLei in myModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbFenLei.GoodsClassifyID//分类
                        join tbJiLaign in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLaign.EstimateUnitID
                        where tbtbCheckReDetill.CheckRermeberID== checkRermeberID
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbtbCheckReDetill.CheckRermeberID,//
                            GoodsIDs = tbGoods.GoodsID,//
                            GoodsCodes = tbGoods.GoodsCode,//
                            GoodsNames = tbGoods.GoodsName,//
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//
                            GoodsChopName=tbShangBiao.GoodsChopName,
                            GoodsClassifyName=tbFenLei.GoodsClassifyName,
                            EstimateUnitName=tbJiLaign.EstimateUnitName

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).
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

        //查询商品帐余
        public ActionResult SelectZhangMian(Vo.BsgridPage bsgridPage, int checkRermeberID)
        {
            var linq = (from tbCheckRemerben in myModels.B_CheckRemerbenList//计划盘点表
                        join tbPanlBuMen in myModels.B_PanlList on tbCheckRemerben.DrugTypeID equals tbPanlBuMen.DrugTypeID//盘点部门表 
                        join tbDerterm in myModels.B_DepatmenList on tbPanlBuMen.DrugTypeID equals tbDerterm.DrugTypeID
                        join tbHaoSunLv in myModels.B_DepatmenList on tbPanlBuMen.DrugTypeID equals tbHaoSunLv.DrugTypeID//耗损率 (主要表)

                        join tbtbCheckReDetill in myModels.B_CheckRemerbenDetillList on tbCheckRemerben.CheckRermeberID equals tbtbCheckReDetill.CheckRermeberID//计划盘点明细表
                        join tbGoods in myModels.B_GoodsList on tbtbCheckReDetill.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbFenLei in myModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbFenLei.GoodsClassifyID//分类
                        where tbtbCheckReDetill.CheckRermeberID == checkRermeberID
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbtbCheckReDetill.CheckRermeberID,//
                            Remter = tbCheckRemerben.Remter,//
                            EnteringTime = tbCheckRemerben.PablData.ToString(),//
                            RegisterTime = tbCheckRemerben.EnteringTime.ToString(),//
                            departmentCodes = tbDerterm.departmentCodes,//
                            departmentName = tbDerterm.departmentName,//

                            GoodsCodes = tbGoods.GoodsCode,//
                            GoodsNames = tbGoods.GoodsName,//
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//
                            GoodsClassifyName = tbFenLei.GoodsClassifyName,
                            HaoSunLv = tbHaoSunLv.HaoSunLv,//耗损率
                            //FactPrice = tbGoodDetail.FactPrice,//商品单价（销售价）

                            //HeLiHaoSunLv = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice,//合理耗损
                            //ChaE = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice - tbHaoSunLv.HaoSunLv,//差额
                            //ShiJiHaoSunLv = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice - tbHaoSunLv.HaoSunLv / tbGoodDetail.FactPrice,

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).
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









        //第二部分（查询损益单）

        public ActionResult SelecSuanYi()
        {
            return View();
        }


        ///查询损益
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectInsfdr(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbIncrea in myModels.B_IncreaseList
                        join tbbumen in myModels.B_PanlList on tbIncrea.DrugTypeID equals tbbumen.DrugTypeID
                        join tbKuCun in myModels.S_StockPlaceList on tbIncrea.StockPlaceID equals tbKuCun.StockPlaceID
                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbIncrea.IncreaseID,
                            Remember = tbIncrea.Remember,
                            IncreaseCause = tbIncrea.IncreaseCause,
                            DrugType = tbbumen.DrugType,
                            StockPlaceName = tbKuCun.StockPlaceName,
                            RegisterName = tbIncrea.RegisterName,
                            RegisterTime = tbIncrea.RegisterTime.ToString(),
                            ExamineName = tbIncrea.ExamineName,
                            ExamineTime = tbIncrea.ExamineTime.ToString(),
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



        ///查询(绑定)盘点计划(商品)二
        /// </summary>
        /// <returns></returns>
        public ActionResult BangCheckewsffGoodsj(Vo.BsgridPage bsgridPage, int increaseID)
        {
            var linq = (from tbChredgRe in myModels.B_IncreaseDetailList
                        join tbGoods in myModels.B_GoodsList on tbChredgRe.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in myModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in myModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                     
                        where tbChredgRe.IncreaseID == increaseID
                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbChredgRe.IncreaseID,
                            IncreaseDetailID = tbChredgRe.IncreaseDetailID,//损益明细
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            Subdivision = tbChredgRe.Subdivision,//细数
                            MumberOfPackages = tbChredgRe.MumberOfPackages,//件数
                            Number = tbChredgRe.Number//批次
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




        ///查询损益
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectInsfdrTiao(Vo.BsgridPage bsgridPage,DateTime chertempatd,DateTime panlpatd)
        {

            var linq = (from tbIncrea in myModels.B_IncreaseList
                        join tbbumen in myModels.B_PanlList on tbIncrea.DrugTypeID equals tbbumen.DrugTypeID
                        join tbKuCun in myModels.S_StockPlaceList on tbIncrea.StockPlaceID equals tbKuCun.StockPlaceID
                        where tbIncrea.RegisterTime== chertempatd || tbIncrea.ExamineTime == panlpatd
                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbIncrea.IncreaseID,
                            Remember = tbIncrea.Remember,
                            IncreaseCause = tbIncrea.IncreaseCause,
                            DrugType = tbbumen.DrugType,
                            StockPlaceName = tbKuCun.StockPlaceName,
                            RegisterName = tbIncrea.RegisterName,
                            RegisterTime = tbIncrea.RegisterTime.ToString(),
                            ExamineName = tbIncrea.ExamineName,
                            ExamineTime = tbIncrea.ExamineTime.ToString(),
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





        //第三部分
        //查询部门库存余额帐(视图一)
        public ActionResult SelectBuMenYuE()
        {
            return View();
        }

        /// <summary>
        /// 查询盘点部门
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectDernrt()
        {
            List<SelectVo> listGrade = (from tbCharecer in myModels.B_CheckRemerbenList
                                           join tbDrufg in myModels.B_PanlList on tbCharecer.DrugTypeID equals tbDrufg.DrugTypeID
                                           join tbGrade in myModels.B_DepatmenList on tbDrufg.DrugTypeID equals tbGrade.DrugTypeID
                                           select new SelectVo
                                           {
                                               id = tbGrade.departmentID,
                                               text = tbGrade.departmentName
                                           }).ToList();
           
            listGrade = Common.Tools.SetSelectJson(listGrade);
            return Json(listGrade, JsonRequestBehavior.AllowGet);

           
        }

        /// <summary>
        /// 查询盘点部门
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectDernrtName(int drugTypeID)
        {
            List<SelectVo> listGrade = (from tbDrufg in myModels.B_PanlList
                                           where tbDrufg.DrugTypeID == drugTypeID
                                           select new SelectVo
                                           {
                                               id = tbDrufg.DrugTypeID,
                                               text = tbDrufg.DrugType,
                                           }).ToList();
            Session["DrugTypeID"] = drugTypeID; // 传递
            listGrade = Common.Tools.SetSelectJson(listGrade);
            return Json(listGrade, JsonRequestBehavior.AllowGet);

           
        }


        //查询部门库存余额帐(视图二)
        public ActionResult SelectBuMenNei()
        {
            return View();
        }


        /// <summary>
        /// 选中的部门
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectDefrtName()
        {
            string CheckRermeberId = Session["DrugTypeID"].ToString();//
            int intdepartmentIDeId = Convert.ToInt32(CheckRermeberId);
            var linq = (from tbDrufg in myModels.B_DepatmenList
                        where tbDrufg.departmentID == intdepartmentIDeId
                        select new LY.WareHouseDeitaLL
                        {
                            departmentID = tbDrufg.departmentID,
                            departmentName = tbDrufg.departmentName,
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        ///查询对应部门(左)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelecBuMnes(Vo.BsgridPage bsgridPage)
        {

            var linq = (from tbChterm in myModels.B_CheckRemerbenList
                        join tbDruyt in myModels.B_PanlList on tbChterm.DrugTypeID equals tbDruyt.DrugTypeID
                        join tbIncrea in myModels.B_DepatmenList on tbDruyt.DrugTypeID equals tbIncrea.DrugTypeID
                      
                        select new LY.WareHouseDeitaLL
                        {
                            PablData=tbChterm.PablData.ToString(),
                            departmentCodes = tbIncrea.departmentCodes,
                            departmentName = tbIncrea.departmentName,

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


        ///查询对应部门（右)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelecBuMnesright(Vo.BsgridPage bsgridPage)
        {

            var linq = (from tbChterm in myModels.B_CheckRemerbenList
                        join tbDruyt in myModels.B_PanlList on tbChterm.DrugTypeID equals tbDruyt.DrugTypeID
                        select new LY.WareHouseDeitaLL
                        {
                            PablData = tbChterm.PablData.ToString(),
                            DrugType = tbDruyt.DrugType,
                            DrugTypeID = tbDruyt.DrugTypeID

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


        ///对应部门（点击)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelecBuMnesrighght(int dfgghyid)
        {

            var linq = (from tbDruyt in myModels.B_PanlList
                        where tbDruyt.DrugTypeID== dfgghyid
                        select new LY.WareHouseDeitaLL
                        {
                            DrugType = tbDruyt.DrugType,

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


    }
}
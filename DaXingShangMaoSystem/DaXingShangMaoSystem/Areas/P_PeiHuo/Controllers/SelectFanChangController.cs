using DaXingShangMaoSystem.LY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.P_PeiHuo.Controllers
{
    public class SelectFanChangController : Controller
    {
        // GET: P_PeiHuo/SelectFanChang
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult SelectFanChang()
        {
            return View();
        }



        //查询返厂单
        public ActionResult SelectRetlre(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbFanChan in MyModels.B_RetureFactoryList
                        join tbConver in MyModels.B_ConverList on tbFanChan.ConverID equals tbConver.ConverID
                        join tbWareHou in MyModels.B_WareHouseList on tbConver.WareHouseID equals tbWareHou.WareHouseID
                        join tbOrderFormPactI in MyModels.B_OrderFormPactList on tbWareHou.OrderFormPactID equals tbOrderFormPactI.OrderFormPactID
                        join tbSupptoc in MyModels.B_SupplierList on tbOrderFormPactI.SupplierID equals tbSupptoc.SupplierID
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbOrderFormPactI.AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID
                        join tbTuiHuo in MyModels.S_SpouseBRanchList on tbFanChan.SpouseBRanchID equals tbTuiHuo.SpouseBRanchID
                        join tbKuCun in MyModels.S_StockPlaceList on tbFanChan.StockPlaceID equals tbKuCun.StockPlaceID
                        join tbTuiYuanYin in MyModels.S_ReturnofList on tbFanChan.ReturnofID equals tbTuiYuanYin.ReturnofID
                        where tbFanChan.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryID = tbFanChan.RetureFactoryID,
                            ConverID = tbConver.ConverID,
                            Remember = tbFanChan.Remember,
                            SpouseBRanchName = tbTuiHuo.SpouseBRanchName,
                            StockPlaceName = tbKuCun.StockPlaceName,
                            Returnof = tbTuiYuanYin.Returnof,

                            SupplierCHName = tbSupptoc.SupplierCHName,
                            ContractNumber = tbOrderFormPactI.ContractNumber,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion,
                            RegisterName = tbFanChan.RegisterName,
                            registerTime = tbFanChan.RegisterTime.ToString(),
                            ExamineName = tbFanChan.ExamineName,
                            examineTime = tbFanChan.ExamineTime.ToString(),
                            Settlement = tbFanChan.Settlement,
                            ExamineNot = tbFanChan.ExamineNot,
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
        /// 绑定商品到（主界面）左(二)附属商品
        /// </summary>
        /// <returns></returns>
        public ActionResult BangFanChangGoodel(Vo.BsgridPage bsgridPage, int retureFactoryID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_RetureFactoryList//返厂ID
                        join tbWanHofDeaill in MyModels.B_RetureFactoryDeTailList on tbSelectWanHtsList.RetureFactoryID equals tbWanHofDeaill.RetureFactoryID//返厂明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细

                        where tbWanHofDeaill.RetureFactoryID == retureFactoryID//根据进仓明细中的“返厂ID”
                        select new LY.PeiHuoDan
                        {
                           
                            RetureFactoryID = tbSelectWanHtsList.RetureFactoryID,//返厂ID
                            GoodsIDs = tbShangPin.GoodsID,//商品ID
                            GoodsCodes = tbShangPin.GoodsCode,//商品代码
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            ArtNos = tbShangPin.ArtNo,//商品货号

                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ReMumberOfPackages = tbWanHofDeaill.ReMumberOfPackages,//返厂件数
                            MumberOfPackages = tbSelectWanHtsList.MumberOfPackages,//配货件数
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

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
            var Linq = (from tbFanChan in MyModels.B_RetureFactoryList
                        join tbConver in MyModels.B_ConverList on tbFanChan.ConverID equals tbConver.ConverID
                        join tbWareHou in MyModels.B_WareHouseList on tbConver.WareHouseID equals tbWareHou.WareHouseID
                        join tbOrderFormPactI in MyModels.B_OrderFormPactList on tbWareHou.OrderFormPactID equals tbOrderFormPactI.OrderFormPactID
                        join tbSupptoc in MyModels.B_SupplierList on tbOrderFormPactI.SupplierID equals tbSupptoc.SupplierID
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbOrderFormPactI.AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID
                        join tbTuiHuo in MyModels.S_SpouseBRanchList on tbFanChan.SpouseBRanchID equals tbTuiHuo.SpouseBRanchID
                        join tbKuCun in MyModels.S_StockPlaceList on tbFanChan.StockPlaceID equals tbKuCun.StockPlaceID
                        join tbTuiYuanYin in MyModels.S_ReturnofList on tbFanChan.ReturnofID equals tbTuiYuanYin.ReturnofID
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryID = tbFanChan.RetureFactoryID,
                            ConverID = tbConver.ConverID,
                            Remember = tbFanChan.Remember,
                            SpouseBRanchName = tbTuiHuo.SpouseBRanchName,
                            StockPlaceName = tbKuCun.StockPlaceName,
                            Returnof = tbTuiYuanYin.Returnof,

                            SupplierCHName = tbSupptoc.SupplierCHName,
                            ContractNumber = tbOrderFormPactI.ContractNumber,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion,
                            RegisterName = tbFanChan.RegisterName,
                            registerTime = tbFanChan.RegisterTime.ToString(),
                            ExamineName = tbFanChan.ExamineName,
                            examineTime = tbFanChan.ExamineTime.ToString(),
                            Settlement = tbFanChan.Settlement,
                            ExamineNots = tbFanChan.ExamineNot.ToString(),
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
                myWareHouseDeitaLL.Returnof = Linq[i].Returnof;
                myWareHouseDeitaLL.SupplierCHName = Linq[i].SupplierCHName;
                myWareHouseDeitaLL.ContractNumber = Linq[i].ContractNumber;
                myWareHouseDeitaLL.AdjustAccountsFashion = Linq[i].AdjustAccountsFashion;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                myWareHouseDeitaLL.Settlement = Linq[i].Settlement;
                myWareHouseDeitaLL.ExamineNot = Linq[i].ExamineNot;
                
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);

        }


    }
}
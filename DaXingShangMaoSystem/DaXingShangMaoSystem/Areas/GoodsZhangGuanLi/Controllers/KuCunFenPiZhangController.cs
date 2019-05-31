using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi.Controllers
{
    public class KuCunFenPiZhangController : Controller
    {
        // GET: GoodsZhangGuanLi/KuCunFenPiZhang

        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult ZhangFenPiGuanLi()
        {
            return View();
        }


        /// <summary>
        /// 查询进仓单（一）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunWareFon(Vo.BsgridPage bsgridPage, int quFenLeiXingID)
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime d = Convert.ToDateTime(dateTimeNow);

            //进仓、员工、类型、合同、供货商、结算方式
            var Linq = from tbWaerHouse in myModels.B_WareHouseList
                       join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                       join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                       join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                       join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                       join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                       where tbWaerHouse.ExamineNot==true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false && tbWaerHouse.ExamineTime == d
                       select new LY.WareHouseDeitaLL
                       {
                           WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                           Remember = tbWaerHouse.Remember,//进仓编号
                           OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                           ContractNumber = tbHeTong.ContractNumber,//合同编号
                           MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                           SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                           StaffID = tbStall.StaffID,//员工ID
                           StaffCode = tbStall.StaffCode,//员工编号
                           StaffName = tbStall.StaffName,//姓名
                           RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                           ExamineNot = tbWaerHouse.ExamineNot,//审核状态
                           Status = tbWaerHouse.Status,//生效状态
                           CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态
                           QuFenLeiXingID = tbQuFen.QuFenLeiXingID,//区分ID
                           QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
                       };

            //区分ID不为空（因为是下拉框）
            if (quFenLeiXingID > 0)
            {
                Linq = Linq.Where(p => p.QuFenLeiXingID == quFenLeiXingID);
            }

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








        //第二部分
        /// <summary>
        /// 查询往来单位余额
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSupptYuE()
        {
            return View();
        }


        /// <summary>
        /// 查询进仓单（一）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunWareFonRi(Vo.BsgridPage bsgridPage)
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");//获取当前时间
            DateTime d = Convert.ToDateTime(dateTimeNow);

            //进仓、员工、类型、合同、供货商、结算方式
            var Linq = from tbWaerHouse in myModels.B_WareHouseList
                       join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                       join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                       join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                       join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                       join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                       where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false && tbWaerHouse.ExamineTime== d
                       select new LY.WareHouseDeitaLL
                       {
                           WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                           Remember = tbWaerHouse.Remember,//进仓编号
                           OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                           ContractNumber = tbHeTong.ContractNumber,//合同编号
                           MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                           SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                           StaffID = tbStall.StaffID,//员工ID
                           StaffCode = tbStall.StaffCode,//员工编号
                           StaffName = tbStall.StaffName,//姓名
                           RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                           ExamineNot = tbWaerHouse.ExamineNot,//审核状态
                           Status = tbWaerHouse.Status,//生效状态
                           CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态
                           QuFenLeiXingID = tbQuFen.QuFenLeiXingID,//区分ID
                           QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
                       };
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
        /// 下拉框 (查询供货商编号)
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaXunSeleSuppout()
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");//获取当前时间
            DateTime d = Convert.ToDateTime(dateTimeNow);

            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tbWaerHouse in myModels.B_WareHouseList
                                                 join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                                                 join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                                                 where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false && tbWaerHouse.ExamineTime == d
                                                 select new SelectXiaLa
                                                 {
                                                     id = tbSupplier.SupplierID,
                                                     text = tbSupplier.SupplierNumber
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 供货商名称
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectDernrtNamefd(int supplierID)
        {
            var linq = (from tbSupplier in myModels.B_SupplierList//供货商
                        where tbSupplier.SupplierID== supplierID
                        select new LY.WareHouseDeitaLL
                        {
                            SupplierNumber = tbSupplier.SupplierCHName
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 下拉框 (经销存类型)
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaXunSeleLeiXing()
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime d = Convert.ToDateTime(dateTimeNow);

            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tbWaerHouse in myModels.B_WareHouseList
                                                 join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                                                 join tbAdjLeiXing in myModels.S_AdjustAccountsFashionList on tbHeTong.AdjustAccountsFashionID equals tbAdjLeiXing.AdjustAccountsFashionID
                                                 where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false && tbWaerHouse.ExamineTime == d
                                                 select new SelectXiaLa
                                                 {
                                                     id = tbAdjLeiXing.AdjustAccountsFashionID,
                                                     text = tbAdjLeiXing.AdjustAccountsFashion
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 查询进仓单（一）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunWareFonf(Vo.BsgridPage bsgridPage, int GongHuoBianHao, int AdjLeiXing)
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime d = Convert.ToDateTime(dateTimeNow);
        
            var Linq = from tbWaerHouse in myModels.B_WareHouseList
                       join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                       join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                       join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                       join tbAdjLeiXing in myModels.S_AdjustAccountsFashionList on tbHeTong.AdjustAccountsFashionID equals tbAdjLeiXing.AdjustAccountsFashionID
                       join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                       join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                       where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false
                       && tbWaerHouse.ExamineTime == d
                       select new LY.WareHouseDeitaLL
                       {
                           WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                           Remember = tbWaerHouse.Remember,//进仓编号
                           OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                           ContractNumber = tbHeTong.ContractNumber,//合同编号
                           MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                           SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                           StaffID = tbStall.StaffID,//员工ID
                           StaffCode = tbStall.StaffCode,//员工编号
                           StaffName = tbStall.StaffName,//姓名
                           RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                           ExamineNot = tbWaerHouse.ExamineNot,//审核状态
                           Status = tbWaerHouse.Status,//生效状态
                           CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态
                           QuFenLeiXingID = tbQuFen.QuFenLeiXingID,//区分ID
                           QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC,//区分
                           SupplierID = tbSupplier.SupplierID,
                           AdjustAccountsFashionID = tbAdjLeiXing.AdjustAccountsFashionID,
                       };

            //区分ID不为空（因为是下拉框）
            if (GongHuoBianHao > 0)
            {
                Linq = Linq.Where(p => p.SupplierID == GongHuoBianHao);
            }
            if (AdjLeiXing > 0)
            {
                Linq = Linq.Where(p => p.AdjustAccountsFashionID == AdjLeiXing);
            }


            int totalRow = Linq.Count();
            List<LY.WareHouseDeitaLL> notices = Linq.OrderByDescending(p => p.Remember).//noboer表达式
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


       
        public ActionResult ChaXunWareFondf()
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");//获取当前时间
            DateTime d = Convert.ToDateTime(dateTimeNow);
          
            var Linq = (from tbWaerHouse in myModels.B_WareHouseList
                        join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbAdjLeiXing in myModels.S_AdjustAccountsFashionList on tbHeTong.AdjustAccountsFashionID equals tbAdjLeiXing.AdjustAccountsFashionID
                        join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                                                                                                                      //商品
                        join tbWaerHouseDateil in myModels.B_WareHouseDetiailList on tbWaerHouse.WareHouseID equals tbWaerHouseDateil.WareHouseID
                        join tbSelectGoods in myModels.B_GoodsList on tbWaerHouseDateil.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false
                        && tbWaerHouse.ExamineTime == d
                        //&& tbSupplier.SupplierID== GongHuoBianHao&& tbAdjLeiXing.AdjustAccountsFashionID== AdjLeiXing
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                            SupplierID = tbSupplier.SupplierID,
                            AdjustAccountsFashionID = tbAdjLeiXing.AdjustAccountsFashionID,
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                        }).ToList();

            var hfdf = 0;
            for (int i = 0; i < Linq.Count; i++)
            {
                var ShuLiang = Linq[i].TaxBids;
                hfdf = hfdf + Convert.ToInt32(ShuLiang);
            }


            return Json(hfdf, JsonRequestBehavior.AllowGet);
        }


      
        public ActionResult BuHanChaXunWareFondf()
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");//获取当前时间
            DateTime d = Convert.ToDateTime(dateTimeNow);

            var Linq = (from tbWaerHouse in myModels.B_WareHouseList
                          
                        join tbWaerHouseDateil in myModels.B_WareHouseDetiailList on tbWaerHouse.WareHouseID equals tbWaerHouseDateil.WareHouseID
                        join tbSelectGoods in myModels.B_GoodsList on tbWaerHouseDateil.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tb in myModels.B_PurchaseList on tbGoodDetail.PurchaseID equals tb.PurchaseID
                        where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false
                        && tbWaerHouse.ExamineTime == d
                     
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                            AdvanceBid = tb.NoAdvanceBid,//不含税进价
                        }).ToList();

            var hfdf = 0;
            for (int i = 0; i < Linq.Count; i++)
            {
                var ShuLiang = Linq[i].TaxBids;
                hfdf = hfdf + Convert.ToInt32(ShuLiang);
            }


            return Json(hfdf, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询对应商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunShangPin(Vo.BsgridPage bsgridPage, int wareHouseID)
        {
            var Linq = (from tbWareHouseDetiai in myModels.B_WareHouseDetiailList//进仓明细
                        join tbSelectGoods in myModels.B_GoodsList on tbWareHouseDetiai.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                        join tbJiSuanDan in myModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWareHouseDetiai.WareHouseID == wareHouseID
                        select new LY.WareHouseDeitaLL
                        {
                            OrderFormDetailID = tbWareHouseDetiai.OrderFormDetailID,
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



        //第三部分
        /// <summary>
        ///查询库存商品总帐（商品进、出、调拨情况）
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsuE()
        {
            return View();
        }



        /// <summary>
        /// 查询进仓单（）, ,string releaseTime
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunWareFonGoodf(Vo.BsgridPage bsgridPage, int XiaoShouBu, int Sputtdct)
        {

            var Linq = from tbWaerHouse in myModels.B_WareHouseList

                       join tbBuMen in myModels.S_SpouseBRanchList on tbWaerHouse.SpouseBRanchID equals tbBuMen.SpouseBRanchID//有无订单部门
                       join tbStockPl in myModels.S_StockPlaceList on tbWaerHouse.StockPlaceID equals tbStockPl.StockPlaceID//销售部门

                       join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                       join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型
                       where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false && tbWaerHouse.PeiHuoNot == true

                       select new LY.WareHouseDeitaLL
                       {
                           WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                           SpouseBRanchID = tbBuMen.SpouseBRanchID,
                           StockPlaceID = tbStockPl.StockPlaceID,

                           Remember = tbWaerHouse.Remember,//进仓编号
                           StaffID = tbStall.StaffID,//员工ID
                           StaffCode = tbStall.StaffCode,//员工编号
                           StaffName = tbStall.StaffName,//姓名
                           RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                           ExamineNot = tbWaerHouse.ExamineNot,//审核状态
                           Status = tbWaerHouse.Status,//生效状态
                           CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态
                           QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC,//区分
                           ExamineTime = tbWaerHouse.ExamineTime.ToString(),//审核时间\
                       };


            if (XiaoShouBu > 0)
            {
                Linq = Linq.Where(p => p.StockPlaceID == XiaoShouBu);
            }
            if (Sputtdct > 0)
            {
                Linq = Linq.Where(p => p.SpouseBRanchID == Sputtdct);
            }

            //if (!string.IsNullOrEmpty(releaseTime))
            //{
            //    try
            //    {
            //        DateTime dtreleseTrime = Convert.ToDateTime(releaseTime);
            //        Linq = Linq.Where(p => p.ExamineTime == dtreleseTrime);// 让发布时间等于传递的时间
            //    }
            //    catch (Exception e)
            //    {
            //        Console.WriteLine(e);
            //    }
            //}

            int totalRow = Linq.Count();
            List<LY.WareHouseDeitaLL> notices = Linq.OrderByDescending(p => p.Remember).//noboer表达式
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
        /// 查询配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectConverList(int wareHouseDetiailID)
        {
            var linq = (from tbConverlist in myModels.B_ConverList
                      
                        join tbWangHou in myModels.B_WareHouseList on tbConverlist.WareHouseID equals tbWangHou.WareHouseID//进仓
                        join tbwangHouDateil in myModels.B_WareHouseDetiailList on tbWangHou.WareHouseID equals tbwangHouDateil.WareHouseID//进仓明细
                        join tbConverDateil in myModels.B_ConverDeTailList on tbConverlist.ConverID equals tbConverDateil.ConverID//配货明细
                        join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodsDateil in myModels.B_GoodsDetailList on tbConverDateil.GoodsID equals tbGoodsDateil.GoodsID
                        where tbwangHouDateil.WareHouseDetiailID== wareHouseDetiailID
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            WareHouseID = tbWangHou.WareHouseID,//id


                            MumberOfPackages= tbConverDateil.YuanMumberOfPack,//进仓件数
                            TaxBid =tbGoodsDateil.TaxBid,//含税进价

                            Number = tbConverDateil.MumberOfPackages,//配货件数

                        }).ToList();

            var JinCangMumber = 0;
            var PeiHuoNumber = 0;

            var JinCangJiaE=0;
            var PeiHuoJiaE = 0;

            var SunYiShu = 0;

            for (int i = 0; i < linq.Count; i++)
            {
                var JinShuLiang = linq[i].MumberOfPackages;
                JinCangMumber = JinCangMumber + Convert.ToInt32(JinShuLiang);

                var PeiShuLiang = linq[i].Number;
                PeiHuoNumber = PeiHuoNumber + Convert.ToInt32(PeiShuLiang);

                var JiaGe = linq[i].TaxBid;//进仓金额
                JinCangJiaE = JinCangMumber * Convert.ToInt32(JiaGe);

                 
                var JiaGepe = linq[i].TaxBid;//配货金额
                PeiHuoJiaE = PeiHuoNumber * (Convert.ToInt32(JiaGepe));

            }


            return Json(new { JinCangMumber, PeiHuoNumber, JinCangJiaE, PeiHuoJiaE, SunYiShu }, JsonRequestBehavior.AllowGet);

        }




        //第四部分(保管帐管理)

        //综合查询库存商品
        public ActionResult ZongHeSelectKuCun()
        {
            return View();
        }


        /// 下拉框 (库存地点)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectKuCunDian()
        {

            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
          
            List<SelectXiaLa> listNoticeTypeS = (from tbAdjLeiXing in myModels.S_StockPlaceList

                                                 select new SelectXiaLa
                                                 {
                                                     id = tbAdjLeiXing.StockPlaceID,
                                                     text = tbAdjLeiXing.StockPlaceName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
          
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 查询进仓单（一）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunWareFonZongHe(Vo.BsgridPage bsgridPage, int quFenLeiXingID,int stockPlaceID)
        {
            //进仓、员工、类型、合同、供货商、结算方式
            var Linq = from tbWaerHouse in myModels.B_WareHouseList
                       join tbStoucRcu in myModels.S_StockPlaceList on tbWaerHouse.StockPlaceID equals tbStoucRcu.StockPlaceID
                       join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                       join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                       join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                       join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                       join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                       where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false
                       select new LY.WareHouseDeitaLL
                       {
                           WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                           StockPlaceID=tbStoucRcu.StockPlaceID,
                           Remember = tbWaerHouse.Remember,//进仓编号
                           OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                           ContractNumber = tbHeTong.ContractNumber,//合同编号
                           MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                           SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                           StaffID = tbStall.StaffID,//员工ID
                           StaffCode = tbStall.StaffCode,//员工编号
                           StaffName = tbStall.StaffName,//姓名
                           RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                           ExamineNot = tbWaerHouse.ExamineNot,//审核状态
                           Status = tbWaerHouse.Status,//生效状态
                           CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态
                           QuFenLeiXingID = tbQuFen.QuFenLeiXingID,//区分ID
                           QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
                       };

          
            if (quFenLeiXingID > 0)
            {
                Linq = Linq.Where(p => p.QuFenLeiXingID == quFenLeiXingID);
            }

            if (stockPlaceID > 0)
            {
                Linq = Linq.Where(p => p.StockPlaceID == stockPlaceID);
            }

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




        //第五部分(保管帐管理)
        //柜台商品列表
        public ActionResult GuiTaiGoods()
        {
            return View();
        }


        /// <summary>
        /// 查询进仓单（柜台的）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunWareFonGuiTai(Vo.BsgridPage bsgridPage)
        {
            
            var Linq = from tbWaerHouse in myModels.B_WareHouseList
                       join tbStoucRcu in myModels.S_StockPlaceList on tbWaerHouse.StockPlaceID equals tbStoucRcu.StockPlaceID
                       join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                       join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                       join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                       join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                       join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                       where tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false && tbStoucRcu.StockPlaceID == 4
                       select new LY.WareHouseDeitaLL
                       {
                           WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                           Remember = tbWaerHouse.Remember,//进仓编号
                           OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                           ContractNumber = tbHeTong.ContractNumber,//合同编号
                           MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                           SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                           StaffCode = tbStall.StaffCode,//员工编号
                           StaffName = tbStall.StaffName,//姓名
                           RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                           ExamineNot = tbWaerHouse.ExamineNot,//审核状态
                           Status = tbWaerHouse.Status,//生效状态
                           CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态

                           QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
                       };
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
        //不用了
        //public ActionResult ChaXunWareFonGuiTai(Vo.BsgridPage bsgridPage)
        //{
        //    //tbWaerHouse.ExamineNot == true && tbWaerHouse.Status == true && tbWaerHouse.CrushRedNot == false && 
        //    var Linq = from tbWaerHouse in myModels.B_WareHouseList
        //               join tbStoucRcu in myModels.S_StockPlaceList on tbWaerHouse.StockPlaceID equals tbStoucRcu.StockPlaceID
        //               join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
        //               join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

        //               join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
        //               join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
        //               join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算

        //               join tbWanHouDateil in myModels.B_WareHouseDetiailList on tbWaerHouse.WareHouseID equals tbWanHouDateil.WareHouseID//明细
        //               join tbGoods in myModels.B_GoodsList on tbWanHouDateil.OrderFormDetailID equals tbGoods.GoodsID
        //               join tbCheckemDateil in myModels.B_CheckRemerbenDetillList on tbGoods.GoodsID equals tbCheckemDateil.GoodsID//明细
        //               join tbChemckem in myModels.B_CheckRemerbenList on tbCheckemDateil.CheckRermeberID equals tbChemckem.CheckRermeberID

        //               where tbStoucRcu.StockPlaceID == 4
        //               select new LY.WareHouseDeitaLL
        //               {
        //                   WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
        //                   Remember = tbWaerHouse.Remember,//进仓编号
        //                   OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
        //                   ContractNumber = tbHeTong.ContractNumber,//合同编号
        //                   MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
        //                   SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
        //                   StaffCode = tbStall.StaffCode,//员工编号
        //                   StaffName = tbStall.StaffName,//姓名
        //                   RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
        //                   ExamineNot = tbWaerHouse.ExamineNot,//审核状态
        //                   Status = tbWaerHouse.Status,//生效状态
        //                   CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态

        //                   QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
        //               };
        //    int totalRow = Linq.Count();
        //    List<LY.WareHouseDeitaLL> notices = Linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
        //        Skip(bsgridPage.GetStartIndex()).//F12（看）
        //        Take(bsgridPage.pageSize).
        //        ToList();

        //    Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
        //    bsgrid.success = true;
        //    bsgrid.totalRows = totalRow;//总的行数
        //    bsgrid.curPage = bsgridPage.curPage;//请求当前页
        //    bsgrid.data = notices;//查出的数据
        //    return Json(bsgrid, JsonRequestBehavior.AllowGet);
        //}


        /// <summary>
        /// 查询对应商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunShangPinPanl(Vo.BsgridPage bsgridPage, int goodsID)
        {
            var Linq = (from tbSelectGoods in myModels.B_GoodsList//商品
                        join tbPanlDateil in myModels.B_CheckRemerbenDetillList on tbSelectGoods.GoodsID equals tbPanlDateil.GoodsID
                        join tbCheckRem in myModels.B_CheckRemerbenList on tbPanlDateil.CheckRermeberID equals tbCheckRem.CheckRermeberID
                        where tbSelectGoods.GoodsID == goodsID
                        select new LY.PeiHuoDan
                        {
                         
                            GoodsIDs = tbSelectGoods.GoodsID,//商品ID
                            MumberOfPackages=tbPanlDateil.MumberOfPackages,
                            Subdivision=tbPanlDateil.Subdivision,
                            Number = tbPanlDateil.SunYiShu,
                            SpouseBRanchNsame = tbCheckRem.CommodityType,
                            registerTime=tbCheckRem.PablData.ToString(),
                        }).ToList();

            int totalRow = Linq.Count();
            List<LY.PeiHuoDan> notices = Linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
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





       
    }
}
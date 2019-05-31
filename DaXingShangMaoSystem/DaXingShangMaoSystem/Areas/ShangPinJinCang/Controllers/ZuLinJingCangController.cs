using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.ShangPinJinCang.Controllers
{
    public class ZuLinJingCangController : Controller
    {
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: ShangPinJinCang/ZuLinJingCang
        public ActionResult ZuLinJingCang()
        {
            return View();
        }



        /// <summary>
        /// 租赁合同查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ZuLinHeTong(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSelectOrPac in myModels.B_OrderFormPactList//合同表
                        join tbHeTongLeiXing in myModels.S_AgreementTypeList on tbSelectOrPac.AgreementTypeID equals tbHeTongLeiXing.AgreementTypeID//合同类型
                        join tbJieSuanFangShi in myModels.S_MethodOfSettlingAccountsList on tbSelectOrPac.MethodOfSettlingAccountsID equals tbJieSuanFangShi.MethodOfSettlingAccountsID//结算方式
                        join tbSutall in myModels.B_SupplierList on tbSelectOrPac.SupplierID equals tbSutall.SupplierID//供货商
                        where tbHeTongLeiXing.AgreementTypeID == 4
                        select new LY.SelectOrderPact
                        {
                            OrderFormPactID = tbSelectOrPac.OrderFormPactID,//合同ID
                            ContractNumber = tbSelectOrPac.ContractNumber,//合同号
                            AgreementTypeName = tbHeTongLeiXing.AgreementTypeName,//合同类型
                            MethodOfSettlingAccounts = tbJieSuanFangShi.MethodOfSettlingAccounts,//结算方式
                            SupplierCHName = tbSutall.SupplierCHName,//供货商名称
                            ContractValidRised = tbSelectOrPac.ValidBegin.ToString(),//合同有效期(起)
                            ContractValidBegins = tbSelectOrPac.ValidTip.ToString(),//合同有效期(始)
                            ConclusionTimes = tbSelectOrPac.ConclusionTime.ToString(),//签订时间
                            ConclusionSite = tbSelectOrPac.ConclusionSite,//签订地点

                        }).ToList();
            int totalRow = linq.Count();
            List<LY.SelectOrderPact> notices = linq.OrderByDescending(p => p.OrderFormPactID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.SelectOrderPact> bsgrid = new Vo.Bsgrid<LY.SelectOrderPact>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 租赁合同（绑定）一
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingLianYingHeTong(int orderFormPactID)
        {

            var linq = (from tbOrderForPac in myModels.B_OrderFormPactList //合同
                        join tbJieSuanFangShi in myModels.S_MethodOfSettlingAccountsList on tbOrderForPac.MethodOfSettlingAccountsID equals tbJieSuanFangShi.MethodOfSettlingAccountsID//结算方式
                        join tbSutall in myModels.B_SupplierList on tbOrderForPac.SupplierID equals tbSutall.SupplierID//供货商
                        where tbOrderForPac.OrderFormPactID == orderFormPactID
                        select new LY.SelectOrderPact
                        {
                            OrderFormPactID = tbOrderForPac.OrderFormPactID,//合同ID
                            ContractNumber = tbOrderForPac.ContractNumber,//合同号
                            MethodOfSettlingAccounts = tbJieSuanFangShi.MethodOfSettlingAccounts,//结算方式
                            SupplierCHName = tbSutall.SupplierCHName,//供货商名称
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 租赁合同(附带商品)（绑定）二
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingLianYingGoodes(Vo.BsgridPage bsgridPage, int orderFormPactID1)
        {
            //商品、计量单位、合同
            var linq = (from tbGoodesList in myModels.B_GoodsList//商品表
                        join tbJiSuanDan in myModels.S_EstimateUnitList on tbGoodesList.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoodesList.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        join tbOrderForPac in myModels.B_OrderFormPactList on tbGoodesList.OrderFormPactID equals tbOrderForPac.OrderFormPactID//合同
                        where tbGoodesList.OrderFormPactID == orderFormPactID1

                        select new LY.WareHouseDeitaLL
                        {
                            OrderFormPactIDs = tbGoodesList.OrderFormPactID,//合同ID
                            GoodsIDs = tbGoodesList.GoodsID,//商品ID
                            GoodsCodes = tbGoodesList.GoodsCode,//商品代码
                            GoodsTiaoMas = tbGoodesList.GoodsTiaoMa,//商品条码
                            GoodsNames = tbGoodesList.GoodsName,//商品名称
                            ArtNos = tbGoodesList.ArtNo,//商品货号
                            SpecificationsModels = tbGoodesList.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiSuanDan.EstimateUnitName,//计量单位
                            PackContents = tbPackln.PackContent,//包装含量
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.OrderFormPactIDs).//noboer表达式
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
        /// 开始 新增 保存（进仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectJingCangDan(B_WareHouseList WareHouse, Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu)
        {
            string strMag = "fali";
            //添加（这个类（bit 类型类））
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                //一
                string Z = ((string[])JieShouID)[0];
                string[] intsZ = Z.Split(',');
                //二
                string M = ((string[])JieShouJianShu)[0];
                string[] intsM = M.Split(',');
                //三
                string N = ((string[])JieShouRuKuXiShu)[0];
                string[] intsN = N.Split(',');


                //判断记录编号是否存在
                int oldWareHouseRows = (from tbWareHouse in myModels.B_WareHouseList
                                        where tbWareHouse.Remember == WareHouse.Remember
                                        select tbWareHouse).Count();
                if (oldWareHouseRows == 0)
                {

                    WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                    WareHouse.Remember = Request.Form["Remember"];//记录编号
                    WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);//进货部门（采购）
                    WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                    WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["StockPlaceID"]);//库存地点（销售部门）
                    WareHouse.Appendix = Request.Form["Appendix"];//附件张数
                
                    WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人
                    WareHouse.RegisterName = Request.Form["RegisterName"];//登记人
                    WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间


                    if (WareHouse.QuFenLeiXingID > 0 && WareHouse.SpouseBRanchID > 0 && WareHouse.StockPlaceID > 0 && WareHouse.StaffID > 0
                        && WareHouse.RegisterName != null && WareHouse.RegisterTime != null && WareHouse.OrderFormPactID > 0)
                    {
                        myModels.B_WareHouseList.Add(WareHouse);
                        myModels.SaveChanges();
                        B_WareHouseDetiailList WareHouseDetiail = new B_WareHouseDetiailList();

                        for (int H = 0; H < intsN.Length;)
                        {
                            for (int E = 0; E < intsM.Length;)
                            {
                                for (int d = 0; d < intsZ.Length; d++)
                                {
                                    WareHouseDetiail.WareHouseID = WareHouse.WareHouseID;//进货单ID
                                    WareHouseDetiail.OrderFormDetailID = Convert.ToInt32(intsZ[d]); ;//订单明细ID
                                    WareHouseDetiail.Subdivision = Convert.ToDecimal(intsN[H]);//入库细数
                                    WareHouseDetiail.MumberOfPackages = Convert.ToDecimal(intsM[E]);//入库件数
                                    myModels.B_WareHouseDetiailList.Add(WareHouseDetiail);
                                    myModels.SaveChanges();//保存
                                    E++;
                                    H++;

                                }
                            }
                        }


                        strMag = "success";
                    }
                    else
                    {
                        return Json("fali", JsonRequestBehavior.AllowGet);
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
        /// 点击“”查询 进仓单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult ChaXunJinCang(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSelctWanrHon in myModels.B_WareHouseList//进仓单

                        join tbJinHuoBumen in myModels.S_SpouseBRanchList on tbSelctWanrHon.SpouseBRanchID equals tbJinHuoBumen.SpouseBRanchID//进货部门
                        join tbKuCun in myModels.S_StockPlaceList on tbSelctWanrHon.StockPlaceID equals tbKuCun.StockPlaceID//库存地点

                        join tbStall in myModels.B_StaffList on tbSelctWanrHon.StaffID equals tbStall.StaffID//员工

                        join tbHeTong in myModels.B_OrderFormPactList on tbSelctWanrHon.OrderFormPactID equals tbHeTong.OrderFormPactID//合同单
                        join tbGongHuoShang in myModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuoShang.SupplierID//供货商
                        join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算方式
                        where tbSelctWanrHon.QuFenLeiXingID == 4 && tbSelctWanrHon.ExamineNot == false && tbSelctWanrHon.CrushRedNot == false
                        select new LY.SelectOrderPact
                        {
                            WareHouseID = tbSelctWanrHon.WareHouseID,//进仓单
                            Remember = tbSelctWanrHon.Remember,//记录编号

                            SpouseBRanchName = tbJinHuoBumen.SpouseBRanchName,//进货部门
                            StockPlaceName = tbKuCun.StockPlaceName,//库存地点

                            Appendix = tbSelctWanrHon.Appendix,//附件
                        
                            StaffCode = tbStall.StaffCode,//收货人编号
                            StaffName = tbStall.StaffName,//收货人姓名
                            RegisterTime = tbSelctWanrHon.RegisterTime.ToString(),//登记时间

                            OrderFormPactIDs = tbSelctWanrHon.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同号
                            MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方法
                            SupplierCHName = tbGongHuoShang.SupplierCHName,//供货商单位
                            ExamineNot = tbSelctWanrHon.ExamineNot,//审核否
                            Status = tbSelctWanrHon.Status//生效否

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.SelectOrderPact> notices = linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.SelectOrderPact> bsgrid = new Vo.Bsgrid<LY.SelectOrderPact>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 点击绑定 进仓单 （一）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult BangDingLianYing(int wareHouseID1)
        {
            var linq = (from tbSelctWanrHon in myModels.B_WareHouseList//进仓单

                        join tbJinHuoBumen in myModels.S_SpouseBRanchList on tbSelctWanrHon.SpouseBRanchID equals tbJinHuoBumen.SpouseBRanchID//进货部门
                        join tbKuCun in myModels.S_StockPlaceList on tbSelctWanrHon.StockPlaceID equals tbKuCun.StockPlaceID//库存地点

                        join tbStall in myModels.B_StaffList on tbSelctWanrHon.StaffID equals tbStall.StaffID//员工

                        join tbHeTong in myModels.B_OrderFormPactList on tbSelctWanrHon.OrderFormPactID equals tbHeTong.OrderFormPactID//合同单
                        join tbGongHuoShang in myModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuoShang.SupplierID//供货商
                        join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算方式
                        where tbSelctWanrHon.QuFenLeiXingID == 4 && tbSelctWanrHon.ExamineNot == false && tbSelctWanrHon.WareHouseID == wareHouseID1
                        select new LY.SelectOrderPact
                        {
                            WareHouseID = tbSelctWanrHon.WareHouseID,//进仓单
                            Remember = tbSelctWanrHon.Remember,//记录编号
                            SpouseBRanchID = tbJinHuoBumen.SpouseBRanchID,//进货部门
                            StockPlaceID = tbKuCun.StockPlaceID,//库存地点

                            Appendix = tbSelctWanrHon.Appendix,//附件
                        
                            StaffID = tbSelctWanrHon.StaffID,//登记人ID
                            StaffCode = tbStall.StaffCode,//收货人编号
                            StaffName = tbStall.StaffName,//收货人姓名
                            RegisterName = tbSelctWanrHon.RegisterName,//登记人
                            RegisterTime = tbSelctWanrHon.RegisterTime.ToString(),//登记时间

                            OrderFormPactIDs = tbSelctWanrHon.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同号
                            MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方法
                            SupplierCHName = tbGongHuoShang.SupplierCHName,//供货商单位

                            ExamineNot = tbSelctWanrHon.ExamineNot,//审核否
                            Status = tbSelctWanrHon.Status//生效否

                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 点击“”查询 绑定进仓单(二)商品
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="WareHouseID1"></param>
        /// <returns></returns>
        public ActionResult SelectShangPinZhuf(Vo.BsgridPage bsgridPage, int wareHousefID1)
        {
            var linq = (from tbSelectWanHtsList in myModels.B_WareHouseList
                        join tbWanHofDeaill in myModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                        join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbWanHofDeaill.WareHouseID == wareHousefID1//根据进仓明细中的“进仓ID”
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID
                            WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID

                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//进仓件数
                            Subdivision = tbWanHofDeaill.Subdivision,//入库细数

                            GoodsIDs = tbShangPin.GoodsID,//商品ID
                            GoodsCodes = tbShangPin.GoodsCode,//商品代码
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            ArtNos = tbShangPin.ArtNo,//商品货号

                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            PackContents = tbPackln.PackContent,//包装含量
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 开始 修改 保存（进仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]

        public ActionResult UptaleJingCangDan(Array JieShouMingXiID, Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu)
        {
            string strMag = "fali";

            //一（订单明细ID）
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');
            //二（入库件数）
            string M = ((string[])JieShouJianShu)[0];
            string[] intsM = M.Split(',');
            //三（入库细数）
            string N = ((string[])JieShouRuKuXiShu)[0];
            string[] intsN = N.Split(',');
            //四(进仓明细ID)
            string V = ((string[])JieShouMingXiID)[0];
            string[] intsV = V.Split(',');

            try
            {
                B_WareHouseList WareHouse = new B_WareHouseList();
                WareHouse.WareHouseID = Convert.ToInt32(Request.Form["WareHouseID"]);//进仓单ID

                WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                WareHouse.Remember = Request.Form["Remember"];//记录编号
                WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);//进货部门
                WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["StockPlaceID"]);//库存地点
                WareHouse.Appendix = Request.Form["Appendix"];//附件张数
             
                WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人
                WareHouse.RegisterName = Request.Form["RegisterName"];//登记人
                WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间

                myModels.Entry(WareHouse).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();


                if (WareHouse.WareHouseID > 0)
                {
                    var ling = (from tbWarHreDiell in myModels.B_WareHouseDetiailList
                                where tbWarHreDiell.WareHouseID == WareHouse.WareHouseID
                                select tbWarHreDiell).Count();
                    if (ling > 0)
                    {
                        if (V.Length > 0)
                        {
                            for (int H = 0; H < intsN.Length;)
                            {
                                for (int E = 0; E < intsM.Length;)
                                {
                                    for (int d = 0; d < intsZ.Length; d++)
                                    {
                                        for (int m = 0; m < intsV.Length; m++)
                                        {
                                            B_WareHouseDetiailList WareHouseDetial = new B_WareHouseDetiailList();

                                            WareHouseDetial.WareHouseDetiailID = Convert.ToInt32(intsV[m]);//进仓单明细ID
                                            WareHouseDetial.WareHouseID = Convert.ToInt32(Request.Form["WareHouseID"]);//进仓单ID
                                            WareHouseDetial.OrderFormDetailID = Convert.ToInt32(intsZ[d]);//商品ID
                                            WareHouseDetial.Subdivision = Convert.ToDecimal(intsN[H]);//入库细数
                                            WareHouseDetial.MumberOfPackages = Convert.ToDecimal(intsM[E]);//入库件数

                                            myModels.Entry(WareHouseDetial).State = System.Data.Entity.EntityState.Modified;
                                            myModels.SaveChanges();//保存
                                            H++;
                                            E++;
                                            d++;
                                        }
                                    }
                                }
                            }
                        }
                        return Json("sussec", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {

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
        /// 点击审核（进仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenHeWareHouss(bool state)
        {
            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                B_WareHouseList WareHouse = new B_WareHouseList();
                WareHouse.WareHouseID = Convert.ToInt32(Request.Form["WareHouseID"]);//进仓单ID

                WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                WareHouse.Remember = Request.Form["Remember"];//记录编号
                WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["PurchaseSectionID"]);//进货部门
                WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["CountersellID"]);//库存地点
                WareHouse.Appendix = Request.Form["Appendix"];//附件张数
             
                WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人
                WareHouse.RegisterName = Request.Form["RegisterName"];//登记人
                WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间
                WareHouse.ExamineName = Request.Form["ExamineName"];//审核人
                WareHouse.ExamineTime = Convert.ToDateTime(Request.Form["ExamineTime"]);//审核时间

                myModels.Entry(WareHouse).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();

                if (WareHouse.WareHouseID > 0)
                {
                    B_WareHouseList wafrtbool = (from tbbool in myModels.B_WareHouseList
                                                 where tbbool.WareHouseID == WareHouse.WareHouseID
                                                 select tbbool).Single();//查询原状态
                    wafrtbool.ExamineNot = state;//改变审核
                    myModels.Entry(wafrtbool).State = EntityState.Modified;

                    B_WareHouseList wafrtboodl = (from tbbool in myModels.B_WareHouseList
                                                  where tbbool.WareHouseID == WareHouse.WareHouseID
                                                  select tbbool).Single();//查询原状态
                    wafrtboodl.Status = state;//改变生效
                    myModels.Entry(wafrtboodl).State = EntityState.Modified;

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

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }


    }
}
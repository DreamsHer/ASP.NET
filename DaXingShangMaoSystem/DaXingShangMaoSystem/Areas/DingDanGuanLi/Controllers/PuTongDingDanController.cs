using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DingDanGuanLi.Controllers
{
    public class PuTongDingDanController : Controller
    {
        // GET: DingDanGuanLi/PuTongDingDan
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        /// <summary>
        /// 普通订单处理
        /// </summary>
        /// <returns></returns>
        public ActionResult PuTongDingDanChuLi()
        {
            return View();
        }
        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectOrderNumber()
        {
            string strOrderNumber = "";
            var listDep = (from tbDmp in MyModels.B_OrderFormList orderby tbDmp.OrderNumber select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_OrderFormList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.OrderNumber.Substring(0, 8));
                intCode++;
                strOrderNumber = intCode.ToString();
                for (int i = 0; i < 8; i++)
                {
                    strOrderNumber = strOrderNumber.Length < 8 ? "0" + strOrderNumber : strOrderNumber;
                }
            }
            else
            {
                strOrderNumber = "00000001";
            }
            return Json(strOrderNumber, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进货部门
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSpouseBRanchID()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必填----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listSpouseBRanchID = (from tbOrderFormType in MyModels.S_SpouseBRanchList
                                                 where tbOrderFormType.SpouseBRanchID == 1 || tbOrderFormType.SpouseBRanchID == 2
                                                 select new SelectVo
                                                 {
                                                     id = tbOrderFormType.SpouseBRanchID,
                                                     text = tbOrderFormType.SpouseBRanchName.Trim(),
                                                 }).ToList();

            listOrderFormType.AddRange(listSpouseBRanchID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 订单类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectOrderFormTypeID()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必填----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listOrderFormTypeID = (from tbOrderFormType in MyModels.S_OrderFormTypeList
                                                  select new SelectVo
                                                  {
                                                      id = tbOrderFormType.OrderFormTypeID,
                                                      text = tbOrderFormType.OrderFormTypeName.Trim(),
                                                  }).ToList();

            listOrderFormType.AddRange(listOrderFormTypeID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 选择经销、代销合同
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectOrderFormPact(BsgridPage bsgridPage)
        {
            var listOrderFormPact = from tbOrderFormPact in MyModels.B_OrderFormPactList
                                    join tbMethodOfSettlingAccounts in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID
                                    join tbAdjustAccountsFashion in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashion.AdjustAccountsFashionID
                                    join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID
                                    orderby tbOrderFormPact.OrderFormPactID
                                    where tbOrderFormPact.AgreementTypeID != 3 && tbOrderFormPact.AgreementTypeID != 4
                                    select new Vo.OrderFormPact
                                    {
                                        OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                        ContractNumber = tbOrderFormPact.ContractNumber.Trim(),
                                        MethodOfSettlingAccountsID = tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID,
                                        MethodOfSettlingAccounts = tbMethodOfSettlingAccounts.MethodOfSettlingAccounts.Trim(),
                                        AdjustAccountsFashionID = tbAdjustAccountsFashion.AdjustAccountsFashionID,
                                        AdjustAccountsFashion = tbAdjustAccountsFashion.AdjustAccountsFashion.Trim(),
                                        SupplierID = tbSupplier.SupplierID,
                                        SupplierName = tbSupplier.SupplierName.Trim(),
                                        PurchasingAgent = tbOrderFormPact.PurchasingAgent.Trim(),
                                        ReleaseTimeStr = tbOrderFormPact.ValidTip.ToString(),
                                        ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                    };
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPact> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPact> bsgrid = new Bsgrid<OrderFormPact>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取合同信息
        /// </summary>
        /// <param name="HeTongID"></param>
        /// <returns></returns>
        public ActionResult HuoQuHeTongXinXi(int HeTongID)
        {
            if (HeTongID > 0)
            {
                var HeTong = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                              join tbMethodOfSettlingAccounts in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID
                              join tbAdjustAccountsFashion in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashion.AdjustAccountsFashionID
                              join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID
                              where tbOrderFormPact.OrderFormPactID == HeTongID
                              select new Vo.OrderFormPact
                              {
                                  OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                  ContractNumber = tbOrderFormPact.ContractNumber.Trim(),
                                  MethodOfSettlingAccountsID = tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID,
                                  MethodOfSettlingAccounts = tbMethodOfSettlingAccounts.MethodOfSettlingAccounts.Trim(),
                                  AdjustAccountsFashionID = tbAdjustAccountsFashion.AdjustAccountsFashionID,
                                  AdjustAccountsFashion = tbAdjustAccountsFashion.AdjustAccountsFashion.Trim(),
                                  SupplierID = tbSupplier.SupplierID,
                                  SupplierName = tbSupplier.SupplierName.Trim(),
                                  PurchasingAgent = tbOrderFormPact.PurchasingAgent.Trim(),
                                  ReleaseTimeStr = tbOrderFormPact.ValidTip.ToString(),
                                  ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                              }).Single();
                return Json(HeTong, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 选择商品信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectShangPinXinXi(BsgridPage bsgridPage)
        {
            var listOrderFormPact = from tbGoods in MyModels.B_GoodsList
                                    join tbb in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbb.GoodsID
                                    join tbGoodsDetail in MyModels.B_GoodsDetailList on tbb.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                    join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                                    join tbc in MyModels.S_PackInfoIDList on tbb.GoodsDetailID equals tbc.GoodsDetailID
                                    join tbPackInfo in MyModels.S_PackInfoIDList on tbc.PackInfoID equals tbPackInfo.PackInfoID
                                    orderby tbGoods.GoodsID
                                    where tbGoods.AuditingBit == true
                                    select new Vo.Goods
                                    {
                                        GoodsID = tbGoods.GoodsID,
                                        GoodsName = tbGoods.GoodsName,
                                        GoodsCode = tbGoods.GoodsCode,
                                        GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                        AdvanceCess = tbGoods.AdvanceCess, /*进项税率*/
                                        RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/
                                        NoAdvanceBid = tbPurchase.NoAdvanceBid,/*不含税进价*/
                                        AdvanceBid = tbPurchase.AdvanceBid,   /*含税进价*/
                                        PackContent = tbPackInfo.PackContent, /*包装含量*/
                                    };
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<Goods> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取商品内容
        /// </summary>
        /// <param name="ShangPinID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPinXinXi(int ShangPinID)
        {
            if (ShangPinID > 0)
            {
                var ShangPin = (from tbGoods in MyModels.B_GoodsList
                                join tbb in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbb.GoodsID
                                join tbGoodsDetail in MyModels.B_GoodsDetailList on tbb.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                                join tbc in MyModels.S_PackInfoIDList on tbb.GoodsDetailID equals tbc.GoodsDetailID
                                join tbPackInfo in MyModels.S_PackInfoIDList on tbc.PackInfoID equals tbPackInfo.PackInfoID
                                where tbGoods.GoodsID == ShangPinID
                                select new Vo.Goods
                                {
                                    GoodsID = tbGoods.GoodsID,
                                    GoodsName = tbGoods.GoodsName,
                                    GoodsCode = tbGoods.GoodsCode,
                                    GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                    AdvanceCess = tbGoods.AdvanceCess, /*进项税率*/
                                    RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/
                                    NoAdvanceBid = tbPurchase.NoAdvanceBid,/*不含税进价*/
                                    AdvanceBid = tbPurchase.AdvanceBid,   /*含税进价*/
                                    PackContent = tbPackInfo.PackContent, /*包装含量*/
                                }).Single();
                return Json(ShangPin, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 查询全部订单信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult DingDanChanXunQuanBu(BsgridPage bsgridPage)
        {
            var listStock = (from tbOrderFormDetail in MyModels.B_OrderFormDetailList
                             join tbGoods in MyModels.B_GoodsList on tbOrderFormDetail.GoodsID equals tbGoods.GoodsID
                             join tbb in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbb.GoodsID
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbb.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                             join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                             //join tbPackInfo in MyModels.S_PackInfoIDList on tbGoodsDetail.PackInfoID equals tbPackInfo.PackInfoID
                             join tbOrderForm in MyModels.B_OrderFormList on tbOrderFormDetail.OrderFormID equals tbOrderForm.OrderFormID
                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbMethodOfSettlingAccounts in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID
                             join tbAdjustAccountsFashion in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashion.AdjustAccountsFashionID
                             join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID
                             join tbOrderFormType in MyModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbOrderFormType.OrderFormTypeID
                             join tbSpouseBRanch in MyModels.S_SpouseBRanchList on tbOrderForm.SpouseBRanchID equals tbSpouseBRanch.SpouseBRanchID
                             orderby tbOrderFormDetail.OrderFormDetailID
                             select new Vo.Goods
                             {
                                 OrderFormDetailID = tbOrderFormDetail.OrderFormDetailID,
                                 //商品信息
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsName = tbGoods.GoodsName,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                 AdvanceCess = tbGoods.AdvanceCess, /*进项税率*/
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/
                                 NoAdvanceBid = tbPurchase.NoAdvanceBid,/*不含税进价*/
                                 AdvanceBid = tbPurchase.AdvanceBid,   /*含税进价*/
                                 //PackContent = tbPackInfo.PackContent, /*包装含量*/

                                 MumberOfPackages = tbOrderFormDetail.MumberOfPackages,
                                 Subdivision = tbOrderFormDetail.Subdivision,
                                 Quantity = tbOrderFormDetail.Quantity,
                                 Remarks = tbOrderFormDetail.Remarks,

                                 OrderFormID = tbOrderForm.OrderFormID,
                                 OrderNumber = tbOrderForm.OrderNumber,
                                 //合同信息
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 ContractNumber = tbOrderFormPact.ContractNumber,
                                 MethodOfSettlingAccountsID = tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID,
                                 MethodOfSettlingAccounts = tbMethodOfSettlingAccounts.MethodOfSettlingAccounts.Trim(),
                                 AdjustAccountsFashionID = tbAdjustAccountsFashion.AdjustAccountsFashionID,
                                 AdjustAccountsFashion = tbAdjustAccountsFashion.AdjustAccountsFashion.Trim(),
                                 SupplierID = tbSupplier.SupplierID,
                                 SupplierName = tbSupplier.SupplierName.Trim(),
                                 PurchasingAgent = tbOrderFormPact.PurchasingAgent.Trim(),
                                 ReleaseTimeStrx = tbOrderFormPact.ValidTip.ToString(),
                                 ReleaseTimeStrh = tbOrderFormPact.ValidBegin.ToString(),

                                 SpouseBRanchID = tbSpouseBRanch.SpouseBRanchID,
                                 SpouseBRanchName = tbSpouseBRanch.SpouseBRanchName,
                                 ReleaseTimeStr = tbOrderFormPact.ValidTip.ToString(),
                                 ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                 ReleaseTimeStrrr = tbOrderForm.OrderGoodsData.ToString(),
                                 Place = tbOrderForm.Place,
                                 DeliveryFashion = tbOrderForm.DeliveryFashion,
                                 Value = tbOrderForm.Value,
                                 ExpensesOTtaxation = tbOrderForm.ExpensesOTtaxation,
                                 ValueTotal = tbOrderForm.ValueTotal,
                                 OrderFormTypeID = tbOrderFormType.OrderFormTypeID,
                                 OrderFormTypeName = tbOrderFormType.OrderFormTypeName,
                                 Register = tbOrderForm.Register,
                                 ReleaseTimeStrf = tbOrderForm.RegisterTime.ToString(),
                                 CheckRen = tbOrderForm.CheckRen,
                                 ReleaseTimeStrd = tbOrderForm.Checktime.ToString(),
                                 ConsigneeNot = tbOrderForm.ConsigneeNot,
                                 ShiFouShouHuo = tbOrderForm.ShiFouShouHuo,
                             });
            int intTotalRow = listStock.Count();//总行数
            List<Goods> listNotices = listStock.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增保存
        /// </summary>
        /// <param name="pwOrderForm"></param>
        /// <returns></returns>
        public ActionResult XinZengDingDanXinXi(B_OrderFormList pwOrderForm)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldOrderFormRows = (from tbGoodsMoneyRule in MyModels.B_OrderFormList
                                        where tbGoodsMoneyRule.OrderNumber == pwOrderForm.OrderNumber
                                        select tbGoodsMoneyRule).Count();
                if (oldOrderFormRows == 0)
                {
                    try
                    {
                        B_OrderFormList Dep = new B_OrderFormList();
                        Dep.OrderNumber = Request.Form["OrderNumber"];
                        Dep.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                        Dep.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                        Dep.OrderGoodsData = Convert.ToDateTime(Request.Form["OrderGoodsData"]);
                        Dep.Place = Request.Form["Place"];
                        Dep.DeliveryFashion = Request.Form["DeliveryFashion"];
                        Dep.Value = Convert.ToDecimal(Request.Form["Value"]);
                        Dep.ExpensesOTtaxation = Convert.ToDecimal(Request.Form["ExpensesOTtaxation"]);
                        Dep.ValueTotal = Convert.ToDecimal(Request.Form["ValueTotal"]);
                        Dep.OrderFormTypeID = Convert.ToInt32(Request.Form["OrderFormTypeID"]);
                        Dep.Register = Request.Form["Register"];
                        Dep.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                        Dep.CheckRen = Request.Form["CheckRen"];
                        Dep.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                        Dep.ConsigneeNot = Convert.ToBoolean(Request.Form["ConsigneeNot"]);
                        if (Dep.OrderNumber != null && Dep.SpouseBRanchID != null && Dep.OrderGoodsData != null && Dep.OrderFormPactID != null
                        && Dep.Place != null && Dep.DeliveryFashion != null && Dep.Value != null && Dep.ExpensesOTtaxation != null
                        && Dep.ValueTotal != null && Dep.OrderFormTypeID != null && Dep.Register != null && Dep.RegisterTime != null
                        && Dep.CheckRen != null && Dep.Checktime != null)
                        {
                            MyModels.B_OrderFormList.Add(Dep);
                            MyModels.SaveChanges();

                            B_OrderFormDetailList KK = new B_OrderFormDetailList();
                            KK.OrderFormID = Dep.OrderFormID;
                            KK.GoodsID = Convert.ToInt32(Request.Form["GoodsID"]);
                            KK.MumberOfPackages = Convert.ToDecimal(Request.Form["MumberOfPackages"]);
                            KK.Subdivision = Convert.ToDecimal(Request.Form["Subdivision"]);
                            KK.Quantity = Request.Form["Quantity"];
                            KK.Remarks = Request.Form["Remarks"];
                            if (KK.OrderFormID != null && KK.GoodsID != null && KK.MumberOfPackages != null && KK.Subdivision != null
                                && KK.Quantity != null && KK.Remarks != null)
                            {
                                MyModels.B_OrderFormDetailList.Add(KK);
                                MyModels.SaveChanges();

                                strMsg = "success";
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return Json(strMsg, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    strMsg = "exsit";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        //修改
        /// <summary>
        /// 回填订单类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectOrderFormTypeIDD()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必填----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listOrderFormTypeID = (from tbOrderFormType in MyModels.S_OrderFormTypeList
                                                  select new SelectVo
                                                  {
                                                      id = tbOrderFormType.OrderFormTypeID,
                                                      text = tbOrderFormType.OrderFormTypeName.Trim(),
                                                  }).ToList();

            listOrderFormType.AddRange(listOrderFormTypeID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 进货部门
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSpouseBRanchIDD()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必填----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listSpouseBRanchID = (from tbOrderFormType in MyModels.S_SpouseBRanchList
                                                 where tbOrderFormType.SpouseBRanchID == 1 || tbOrderFormType.SpouseBRanchID == 2
                                                 select new SelectVo
                                                 {
                                                     id = tbOrderFormType.SpouseBRanchID,
                                                     text = tbOrderFormType.SpouseBRanchName.Trim(),
                                                 }).ToList();

            listOrderFormType.AddRange(listSpouseBRanchID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询修改数据
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult DingDanChanXun(BsgridPage bsgridPage)
        {
            var listStock = (from tbOrderFormDetail in MyModels.B_OrderFormDetailList
                             join tbGoods in MyModels.B_GoodsList on tbOrderFormDetail.GoodsID equals tbGoods.GoodsID
                             join tbb in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbb.GoodsID
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbb.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                             join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                             //join tbPackInfo in MyModels.S_PackInfoIDList on tbGoodsDetail.PackInfoID equals tbPackInfo.PackInfoID

                             join tbOrderForm in MyModels.B_OrderFormList on tbOrderFormDetail.OrderFormID equals tbOrderForm.OrderFormID

                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbMethodOfSettlingAccounts in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID
                             join tbAdjustAccountsFashion in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashion.AdjustAccountsFashionID
                             join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID

                             join tbOrderFormType in MyModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbOrderFormType.OrderFormTypeID
                             join tbSpouseBRanch in MyModels.S_SpouseBRanchList on tbOrderForm.SpouseBRanchID equals tbSpouseBRanch.SpouseBRanchID
                             orderby tbOrderFormDetail.OrderFormDetailID
                             where tbOrderForm.ConsigneeNot == false
                             select new Vo.Goods
                             {
                                 OrderFormDetailID = tbOrderFormDetail.OrderFormDetailID,
                                 //商品信息
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsName = tbGoods.GoodsName,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                 AdvanceCess = tbGoods.AdvanceCess, /*进项税率*/
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/
                                 NoAdvanceBid = tbPurchase.NoAdvanceBid,/*不含税进价*/
                                 AdvanceBid = tbPurchase.AdvanceBid,   /*含税进价*/
                                 /*PackContent = tbPackInfo.PackContent, *//*包装含量*/

                                 MumberOfPackages = tbOrderFormDetail.MumberOfPackages,
                                 Subdivision = tbOrderFormDetail.Subdivision,
                                 Quantity = tbOrderFormDetail.Quantity,
                                 Remarks = tbOrderFormDetail.Remarks,

                                 OrderFormID = tbOrderForm.OrderFormID,
                                 OrderNumber = tbOrderForm.OrderNumber,
                                 //合同信息
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 ContractNumber = tbOrderFormPact.ContractNumber,
                                 MethodOfSettlingAccountsID = tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID,
                                 MethodOfSettlingAccounts = tbMethodOfSettlingAccounts.MethodOfSettlingAccounts.Trim(),
                                 AdjustAccountsFashionID = tbAdjustAccountsFashion.AdjustAccountsFashionID,
                                 AdjustAccountsFashion = tbAdjustAccountsFashion.AdjustAccountsFashion.Trim(),
                                 SupplierID = tbSupplier.SupplierID,
                                 SupplierName = tbSupplier.SupplierName.Trim(),
                                 PurchasingAgent = tbOrderFormPact.PurchasingAgent.Trim(),
                                 ReleaseTimeStrx = tbOrderFormPact.ValidTip.ToString(),
                                 ReleaseTimeStrh = tbOrderFormPact.ValidBegin.ToString(),

                                 SpouseBRanchID = tbSpouseBRanch.SpouseBRanchID,
                                 SpouseBRanchName = tbSpouseBRanch.SpouseBRanchName,
                                 ReleaseTimeStr = tbOrderFormPact.ValidTip.ToString(),
                                 ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                 ReleaseTimeStrrr = tbOrderForm.OrderGoodsData.ToString(),
                                 Place = tbOrderForm.Place,
                                 DeliveryFashion = tbOrderForm.DeliveryFashion,
                                 Value = tbOrderForm.Value,
                                 ExpensesOTtaxation = tbOrderForm.ExpensesOTtaxation,
                                 ValueTotal = tbOrderForm.ValueTotal,
                                 OrderFormTypeID = tbOrderFormType.OrderFormTypeID,
                                 OrderFormTypeName = tbOrderFormType.OrderFormTypeName,
                                 Register = tbOrderForm.Register,
                                 ReleaseTimeStrf = tbOrderForm.RegisterTime.ToString(),
                                 CheckRen = tbOrderForm.CheckRen,
                                 ReleaseTimeStrd = tbOrderForm.Checktime.ToString(),
                                 ConsigneeNot = tbOrderForm.ConsigneeNot,
                                 ShiFouShouHuo = tbOrderForm.ShiFouShouHuo,
                             });
            int intTotalRow = listStock.Count();//总行数
            List<Goods> listNotices = listStock.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 回填信息
        /// </summary>
        /// <param name="DDID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShuJuXiuGaiShenHe(int DDID)
        {
            if (DDID > 0)
            {
                var listOrderFormDetail = (from tbOrderFormDetail in MyModels.B_OrderFormDetailList
                                           join tbGoods in MyModels.B_GoodsList on tbOrderFormDetail.GoodsID equals tbGoods.GoodsID
                                           join tbb in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbb.GoodsID
                                           join tbGoodsDetail in MyModels.B_GoodsDetailList on tbb.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                           join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                                           //join tbPackInfo in MyModels.S_PackInfoIDList on tbGoodsDetail.PackInfoID equals tbPackInfo.PackInfoID

                                           join tbOrderForm in MyModels.B_OrderFormList on tbOrderFormDetail.OrderFormID equals tbOrderForm.OrderFormID

                                           join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                                           join tbMethodOfSettlingAccounts in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID
                                           join tbAdjustAccountsFashion in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashion.AdjustAccountsFashionID
                                           join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID

                                           join tbOrderFormType in MyModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbOrderFormType.OrderFormTypeID
                                           join tbSpouseBRanch in MyModels.S_SpouseBRanchList on tbOrderForm.SpouseBRanchID equals tbSpouseBRanch.SpouseBRanchID
                                           where tbOrderFormDetail.OrderFormDetailID == DDID
                                           where tbOrderForm.ConsigneeNot == false
                                           select new Vo.Goods
                                           {
                                               OrderFormDetailID = tbOrderFormDetail.OrderFormDetailID,
                                               //商品信息
                                               GoodsID = tbGoods.GoodsID,
                                               GoodsName = tbGoods.GoodsName,
                                               GoodsCode = tbGoods.GoodsCode,
                                               GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                               AdvanceCess = tbGoods.AdvanceCess, /*进项税率*/
                                               RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/
                                               NoAdvanceBid = tbPurchase.NoAdvanceBid,/*不含税进价*/
                                               AdvanceBid = tbPurchase.AdvanceBid,   /*含税进价*/
                                                                                     //PackContent = tbPackInfo.PackContent, /*包装含量*/

                                               MumberOfPackages = tbOrderFormDetail.MumberOfPackages,
                                               Subdivision = tbOrderFormDetail.Subdivision,
                                               Quantity = tbOrderFormDetail.Quantity,
                                               Remarks = tbOrderFormDetail.Remarks,

                                               OrderFormID = tbOrderForm.OrderFormID,
                                               OrderNumber = tbOrderForm.OrderNumber,
                                               //合同信息
                                               OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                               ContractNumber = tbOrderFormPact.ContractNumber,
                                               MethodOfSettlingAccountsID = tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID,
                                               MethodOfSettlingAccounts = tbMethodOfSettlingAccounts.MethodOfSettlingAccounts.Trim(),
                                               AdjustAccountsFashionID = tbAdjustAccountsFashion.AdjustAccountsFashionID,
                                               AdjustAccountsFashion = tbAdjustAccountsFashion.AdjustAccountsFashion.Trim(),
                                               SupplierID = tbSupplier.SupplierID,
                                               SupplierName = tbSupplier.SupplierName.Trim(),
                                               PurchasingAgent = tbOrderFormPact.PurchasingAgent.Trim(),
                                               ReleaseTimeStrx = tbOrderFormPact.ValidTip.ToString(),
                                               ReleaseTimeStrh = tbOrderFormPact.ValidBegin.ToString(),

                                               SpouseBRanchID = tbSpouseBRanch.SpouseBRanchID,
                                               SpouseBRanchName = tbSpouseBRanch.SpouseBRanchName,
                                               ReleaseTimeStr = tbOrderFormPact.ValidTip.ToString(),
                                               ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                               ReleaseTimeStrrr = tbOrderForm.OrderGoodsData.ToString(),
                                               Place = tbOrderForm.Place,
                                               DeliveryFashion = tbOrderForm.DeliveryFashion,
                                               Value = tbOrderForm.Value,
                                               ExpensesOTtaxation = tbOrderForm.ExpensesOTtaxation,
                                               ValueTotal = tbOrderForm.ValueTotal,
                                               OrderFormTypeID = tbOrderFormType.OrderFormTypeID,
                                               OrderFormTypeName = tbOrderFormType.OrderFormTypeName,
                                               Register = tbOrderForm.Register,
                                               ReleaseTimeStrf = tbOrderForm.RegisterTime.ToString(),
                                               CheckRen = tbOrderForm.CheckRen,
                                               ReleaseTimeStrd = tbOrderForm.Checktime.ToString(),
                                               ConsigneeNot = tbOrderForm.ConsigneeNot,
                                               ShiFouShouHuo = tbOrderForm.ShiFouShouHuo,
                                           }).Single();
                return Json(listOrderFormDetail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <returns></returns>       
        public ActionResult DingDanXiuGaiBaoCun()
        {
            string styMy = "fail";
            try
            {
                B_OrderFormList KK = new B_OrderFormList();
                KK.OrderFormID = Convert.ToInt32(Request.Form["OrderFormID"]);
                KK.OrderNumber = Request.Form["OrderNumber"];
                KK.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                KK.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                KK.OrderGoodsData = Convert.ToDateTime(Request.Form["OrderGoodsData"]);
                KK.Place = Request.Form["Place"];
                KK.DeliveryFashion = Request.Form["DeliveryFashion"];
                KK.Value = Convert.ToDecimal(Request.Form["Value"]);
                KK.ExpensesOTtaxation = Convert.ToDecimal(Request.Form["ExpensesOTtaxation"]);
                KK.ValueTotal = Convert.ToDecimal(Request.Form["ValueTotal"]);
                KK.OrderFormTypeID = Convert.ToInt32(Request.Form["OrderFormTypeID"]);
                KK.Register = Request.Form["Register"];
                KK.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                KK.CheckRen = Request.Form["CheckRen"];
                KK.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                KK.ConsigneeNot = Convert.ToBoolean(Request.Form["ConsigneeNot"]);

                MyModels.Entry(KK).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();

                B_OrderFormDetailList JJ = new B_OrderFormDetailList();
                JJ.OrderFormID = KK.OrderFormID;
                JJ.OrderFormDetailID = Convert.ToInt32(Request.Form["OrderFormDetailID"]);
                JJ.GoodsID = Convert.ToInt32(Request.Form["GoodsID"]);
                JJ.MumberOfPackages = Convert.ToDecimal(Request.Form["MumberOfPackages"]);
                JJ.Subdivision = Convert.ToDecimal(Request.Form["Subdivision"]);
                JJ.Quantity = Request.Form["Quantity"];
                JJ.Remarks = Request.Form["Remarks"];

                MyModels.Entry(JJ).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();

                styMy = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(styMy, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除未审核订单
        /// </summary>
        /// <param name="orderFormId"></param>
        /// <returns></returns>
        public ActionResult DeleteOrderForm(int orderFormId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_OrderFormList dbGoods = (from tbOrderForm in MyModels.B_OrderFormList
                                           where tbOrderForm.OrderFormID == orderFormId
                                           where tbOrderForm.ConsigneeNot == false
                                           select tbOrderForm).Single();

                MyModels.B_OrderFormList.Remove(dbGoods);

                if (MyModels.SaveChanges() > 0)
                {
                    B_OrderFormDetailList dbGoodsDetail = (from tbOrderFormDetail in MyModels.B_OrderFormDetailList
                                                           where tbOrderFormDetail.OrderFormID == orderFormId
                                                           select tbOrderFormDetail).Single();

                    MyModels.B_OrderFormDetailList.Remove(dbGoodsDetail);

                    if (MyModels.SaveChanges() > 0)
                    {
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
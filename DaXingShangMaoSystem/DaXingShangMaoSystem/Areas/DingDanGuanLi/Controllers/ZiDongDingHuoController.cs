using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DingDanGuanLi.Controllers
{
    public class ZiDongDingHuoController : Controller
    {
        // GET: DingDanGuanLi/ZiDongDingHuo
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities(); 
               
        /// <summary>
        ///自动订货处理 
        /// </summary>
        /// <returns></returns>
        public ActionResult ZiDongDingHuoChuLi()
        {
            return View();
        }
        /// <summary>
        /// 选择进货部门
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSpouseBRanch()
        {
            List<SelectVo> listStatusID = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listStatusID.Add(selectVo);

            List<SelectVo> listStatusIDD = (from tbStatusID in MyModels.S_SpouseBRanchList
                                            select new SelectVo
                                            {
                                                id = tbStatusID.SpouseBRanchID,
                                                text = tbStatusID.SpouseBRanchName
                                            }).ToList();

            listStatusID.AddRange(listStatusIDD);

            return Json(listStatusID, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  选择供货商
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectSupplier(BsgridPage bsgridPage)
        {
            var listSupplier = from tbSupplier in MyModels.B_SupplierList
                               orderby tbSupplier.SupplierID
                               select new Vo.Goods
                               {
                                   SupplierID = tbSupplier.SupplierID,
                                   SupplierNumber = tbSupplier.SupplierNumber,
                                   SupplierName = tbSupplier.SupplierName,
                               };
            int intTotalRow = listSupplier.Count();//总行数
            List<Goods> listNotices = listSupplier.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取供货商
        /// </summary>
        /// <param name="GongHuoID"></param>
        /// <returns></returns>
        public ActionResult HuoQuGongHuoShangMingCheng(int GongHuoID)
        {
            if (GongHuoID > 0)
            {
                var GongHuo = (from tbSupplier in MyModels.B_SupplierList
                               where tbSupplier.SupplierID == GongHuoID
                               select new Vo.Goods
                               {
                                   SupplierID = tbSupplier.SupplierID,
                                   SupplierNumber = tbSupplier.SupplierNumber,
                                   SupplierName = tbSupplier.SupplierName,
                               }).Single();
                return Json(GongHuo, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 选择合同
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectOrderFormPactID(BsgridPage bsgridPage)
        {
            var listOrderFormPact = from tbOrderFormPact in MyModels.B_OrderFormPactList
                                    join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                    orderby tbOrderFormPact.OrderFormPactID
                                    select new Vo.OrderFormPact
                                    {
                                        OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                        AgreementTypeName = tbAgreementType.AgreementTypeName.Trim(),
                                        ContractNumber = tbOrderFormPact.ContractNumber.Trim(),
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
        /// 获取合同名称
        /// </summary>
        /// <param name="HeTongID"></param>
        /// <returns></returns>
        public ActionResult HuoQuHeTongMingCheng(int HeTongID)
        {
            if (HeTongID > 0)
            {
                var HeTong = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                              join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                              where tbOrderFormPact.OrderFormPactID == HeTongID
                              select new Vo.OrderFormPact
                              {
                                  OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                  AgreementTypeName = tbAgreementType.AgreementTypeName.Trim(),
                                  ContractNumber = tbOrderFormPact.ContractNumber.Trim(),
                              }).Single();
                return Json(HeTong, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="supplierID"></param>
        /// <param name="orderFormPactID"></param>
        /// <param name="spouseBRanchID"></param>
        /// <returns></returns>
        public ActionResult SelectOrderFormDetailList(BsgridPage bsgridPage, string supplierName, string contractNumber, int spouseBRanchID)
        {
            var items = (from tbOrderFormDetail in MyModels.B_OrderFormDetailList
                         join tbGoods in MyModels.B_GoodsList on tbOrderFormDetail.GoodsID equals tbGoods.GoodsID                         
                         join tbGoodsss in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsss.GoodsID
                         //join tbStock in MyModels.B_StockList on tbOrderFormDetail.StockID equals tbStock.StockID
                         join tbOrderForm in MyModels.B_OrderFormList on tbOrderFormDetail.OrderFormID equals tbOrderForm.OrderFormID
                         join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                         join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                         join tbSpouseBRanch in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanch.SpouseBRanchID
                         orderby tbOrderFormDetail.OrderFormDetailID
                         select new Vo.Goods
                         {
                             OrderFormDetailID = tbOrderFormDetail.OrderFormDetailID,
                             GoodsID = tbGoods.GoodsID,
                             GoodsCode = tbGoods.GoodsCode,/*商品代码*/
                             GoodsTiaoMa = tbGoods.GoodsTiaoMa,/*商品条码*/
                             GoodsName = tbGoods.GoodsName,/*商品名称*/
                             SpecificationsModel = tbGoods.SpecificationsModel,/*规格型号*/
                             ArtNo = tbGoods.ArtNo,/*货号*/
                             RetailMonovalent = tbGoodsss.RetailMonovalent,/*零售单价*/
                             //StockNumber = tbStock.StockNumber, /*库存数量*/
                             //条件
                             SupplierID = tbSupplier.SupplierID,
                             SupplierName = tbSupplier.SupplierName,
                             OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                             ContractNumber = tbOrderFormPact.ContractNumber,
                             SpouseBRanchID = tbSpouseBRanch.SpouseBRanchID,
                             SpouseBRanchName = tbSpouseBRanch.SpouseBRanchName,
                         });
            //如果查询条件不为空 
            if (!string.IsNullOrEmpty(supplierName))
            {
                supplierName = supplierName.Trim();
                items = items.Where(p => p.SupplierName.Contains(supplierName));
            }
            if (!string.IsNullOrEmpty(contractNumber))
            {
                contractNumber = contractNumber.Trim();
                items = items.Where(p => p.ContractNumber.Contains(contractNumber));
            }
            //if (supplierID > 0)           
            //{
            //    items = items.Where(p => p.SupplierID == supplierID);
            //}
            //if (orderFormPactID > 0)
            //{
            //    items = items.Where(p => p.OrderFormPactID == orderFormPactID);
            //}
            if (spouseBRanchID > 0)
            {
                items = items.Where(p => p.SpouseBRanchID == spouseBRanchID);
            }
            int intTotalRow = items.Count();//总行数
            List<Goods> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        ///// <summary>
        ///// 删除自动订货处理
        ///// </summary>
        ///// <param name="orderFormDetailId"></param>
        ///// <returns></returns>
        //public ActionResult DeleteOrderFormDetail(int orderFormDetailId)
        //{
        //    //定义返回
        //    string strMsg = "fail";
        //    try
        //    {
        //        B_OrderFormDetailList dbGoods = (from tbGoods in MyModels.B_OrderFormDetailList
        //                               where tbGoods.OrderFormDetailID == orderFormDetailId
        //                                         select tbGoods).Single();

        //        MyModels.B_OrderFormDetailList.Remove(dbGoods);

        //        if (MyModels.SaveChanges() > 0)
        //        {
        //            strMsg = "success";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return Json(strMsg, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult UnpticoIncrea(Array JieId, Array JiePanlId ,Array JieSpatckId, Array JieDaiMa, 
        //    Array JieMingCheng, Array JieHaoSunLv)
        //{
        //    string strMag = "fali";

        //    try
        //    {
        //        //一 
        //        string Z = ((string[])JieId)[0];
        //        string[] intsid = Z.Split(',');
        //        //二
        //        string M = ((string[])JiePanlId)[0];
        //        string[] intsPanlId = M.Split(',');
        //        //三
        //        string N = ((string[])JieSpatckId)[0];
        //        string[] intSpatckId = N.Split(',');
        //        //四
        //        string C = ((string[])JieDaiMa)[0];
        //        string[] intsDaiMa = C.Split(',');

        //        //五
        //        string Pic = ((string[])JieMingCheng)[0];
        //        string[] intsName = Pic.Split(',');

        //        //六
        //        string kj = ((string[])JieHaoSunLv)[0];
        //        string[] intsSunYinLv = kj.Split(',');


        //        for (int A = 0; A < intsid.Length;)
        //        {
        //            for (int B = 0; B < intsPanlId.Length;)
        //            {
        //                for (int D = 0; D < intSpatckId.Length;)
        //                {
        //                    for (int F = 0; F < intsDaiMa.Length;)
        //                    {
        //                        for (int H = 0; H < intsName.Length;)
        //                        {
        //                            for (int G = 0; G < intsSunYinLv.Length; G++)
        //                            {
        //                                B_VoluntarilyOrderGoodsList OK = new B_VoluntarilyOrderGoodsList();

        //                                OK.OrderFormDetailID = Convert.ToInt32(intsid[A]);//订单明细ID
        //                                OK.GoodsDetailID = Convert.ToInt32(intsPanlId[B]);//商品ID
        //                                OK.Pack = intsName[H];//订货包装                                                                                                                     
        //                                OK.AverageNumber = Convert.ToDecimal(intsSunYinLv[G]);//平均销售数量
        //                                OK.AdviseNumber = Convert.ToDecimal(intSpatckId[D]);//建议订货数量

        //                                MyModels.Entry(OK).State = System.Data.Entity.EntityState.Modified;
        //                                MyModels.SaveChanges();//保存
        //                                H++;
        //                                F++;
        //                                D++;
        //                                B++;
        //                                A++;
        //                                strMag = "succsee";
        //                            }
        //                        }
        //                    }

        //                }
        //            }


        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return Json(strMag, JsonRequestBehavior.AllowGet);
        //}



        /// <summary>
        /// 新增自动订货清单
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengZiDongDingHuoQingDan()
        {
            B_VoluntarilyOrderGoodsList Dep = new B_VoluntarilyOrderGoodsList();
            Dep.ZiDongDingDanHao = Request.Form["ZiDongDingDanHao"];
            Dep.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
            Dep.OrderFormDetailID = Convert.ToInt32(Request.Form["OrderFormDetailID"]);
            Dep.Pack = Request.Form["Pack"];
            Dep.AdviseNumber = Convert.ToDecimal(Request.Form["AdviseNumber"]);
            Dep.AverageNumber = Convert.ToDecimal(Request.Form["AverageNumber"]);

            if (Dep.ZiDongDingDanHao != null && Dep.GoodsDetailID != null && Dep.OrderFormDetailID != null
                && Dep.Pack != null && Dep.AdviseNumber != null && Dep.AverageNumber != null)
            {
                MyModels.B_VoluntarilyOrderGoodsList.Add(Dep);
                MyModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

    }
}
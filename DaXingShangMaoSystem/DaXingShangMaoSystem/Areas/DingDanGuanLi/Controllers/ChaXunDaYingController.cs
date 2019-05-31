using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DingDanGuanLi.Controllers
{
    public class ChaXunDaYingController : Controller
    {
        // GET: DingDanGuanLi/ChaXunDaYing
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        //查询打印订货清单

        public ActionResult ChaXunDaYingQingDan()
        {
            return View();
        }
        /// <summary>
        /// 查询打印订货清单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunDaYing(BsgridPage bsgridPage)
        {
            var listStock = (from tbOrderForm in MyModels.B_OrderFormList
                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbOrderFormType in MyModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbOrderFormType.OrderFormTypeID                            
                             orderby tbOrderForm.OrderFormID
                             select new Vo.Goods
                             {
                                 OrderFormID = tbOrderForm.OrderFormID,
                                 OrderNumber = tbOrderForm.OrderNumber,
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 ContractNumber = tbOrderFormPact.ContractNumber,
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
        /// 浏览器打印部分试图
        /// </summary>
        /// <returns></returns>
        public ActionResult LiuLanQiDaYingQingDan()
        {
            var linqItem = from tbOrderForm in MyModels.B_OrderFormList
                           join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                           join tbOrderFormType in MyModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbOrderFormType.OrderFormTypeID                           
                           select new Vo.Goods
                           {
                               OrderFormID = tbOrderForm.OrderFormID,
                               OrderNumber = tbOrderForm.OrderNumber,
                               OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                               ContractNumber = tbOrderFormPact.ContractNumber,
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
                           };
            //查询数据
            List<Goods> listExaminee = linqItem.ToList();

            return PartialView(listExaminee);
        }

        /// <summary>
        /// 查询修改数据
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult DingDanChanXun(BsgridPage bsgridPage)
        {
            var listStock = (from tbOrderForm in MyModels.B_OrderFormList
                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbOrderFormType in MyModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbOrderFormType.OrderFormTypeID                           
                             orderby tbOrderForm.OrderFormID
                             select new Vo.Goods
                             {
                                 OrderFormID = tbOrderForm.OrderFormID,
                                 OrderNumber = tbOrderForm.OrderNumber,
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 ContractNumber = tbOrderFormPact.ContractNumber,
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
                var listOrderForm = (from tbOrderForm in MyModels.B_OrderFormList
                                     join tbOrderFormPact in MyModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                                     join tbOrderFormType in MyModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbOrderFormType.OrderFormTypeID                                     
                                     where tbOrderForm.OrderFormID == DDID
                                     select new Vo.Goods
                                     {
                                         OrderFormID = tbOrderForm.OrderFormID,
                                         OrderNumber = tbOrderForm.OrderNumber,
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
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
                                     }).Single();
                return Json(listOrderForm, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

    }
}
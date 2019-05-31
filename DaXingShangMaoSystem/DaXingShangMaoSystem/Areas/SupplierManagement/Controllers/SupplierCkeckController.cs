using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.SupplierManagement.Controllers
{
    public class SupplierCkeckController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: SupplierManagement/SupplierCkeck
        /// <summary>
        /// 业务往来清退
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierCkeck()
        {
            return View();
        }
        public ActionResult selectData1(BsgridPage bsgridPage,string supplierNumber)
        {
            var listOrderFormPact = (from tbSupplierId in MyModels.B_SupplierList
                                     orderby tbSupplierId.SupplierID
                                     select new SupplierListVo
                           {
                               SupplierID = tbSupplierId.SupplierID,
                               SupplierNumber = tbSupplierId.SupplierNumber,
                               SupplierName = tbSupplierId.SupplierName,

                           });

            if (!string.IsNullOrEmpty(supplierNumber))
            {
                listOrderFormPact = listOrderFormPact.Where(s => s.SupplierNumber.Contains(supplierNumber.Trim()));
            }
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<SupplierListVo> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<SupplierListVo> bsgrid = new Bsgrid<SupplierListVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        //业务往来单位查询
        public ActionResult YeWuWangLaiDanWei()
        {
            return View();
        }
        //业务往来单位过期证书查询
        public ActionResult YeWuWangLaiDanWeiGuoQiZhengShu()
        {
            return View();
        }
        //生产信息
        public ActionResult Selectzhengshu(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.S_ContactCertificate
                                     join tbCompany in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbCompany.SupplierID
                                     orderby tbOrderFormPact.ContactCertificateID
                                     select new ContactCertificateVo
                                     {
                                         ContactCertificateID = tbOrderFormPact.ContactCertificateID,
                                         ReleaseTimeStr = tbOrderFormPact.ContactCertificateTime.ToString(),
                                         ContactCertificateName = tbOrderFormPact.ContactCertificateName,
                                         ContactCertificateHM = tbOrderFormPact.ContactCertificateHM,
                                         ReleaseTimeStrr = tbOrderFormPact.ContactCertificateTimeless.ToString(),
                                         SupplierID=tbOrderFormPact.SupplierID,
                                         SupplierName = tbCompany.SupplierName,
                                         SupplierCHName = tbCompany.SupplierCHName,
                                     }
                                    );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<ContactCertificateVo> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<ContactCertificateVo> bsgrid = new Bsgrid<ContactCertificateVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
    }
}
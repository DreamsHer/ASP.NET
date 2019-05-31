using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaXinXiGuanLi.Controllers
{
    public class HuiYuanKaHuanKaController : Controller
    {
        // GET: HuiYuanKaXinXiGuanLi/HuiYuanKaHuanKa
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult HuanKa()
        {
            return View();
        }
        #region  下拉框
        public ActionResult SelectHuiLeiXi()
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MembershipCardList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.MembershipCardID,
                                              text = tbAcademe.MembershipCardMC
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectHuiKaZhong()
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MembershipCardTypeList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.MembershipCardTypeID,
                                              text = tbAcademe.MembershipCardTypeMC
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 信息获取
        public ActionResult SelectCXSummary(BsgridPage bsgridPage)//选择信息
        {
            var listGoods = (from tbCXSummary in MyModels.S_CXSummaryList
                             orderby tbCXSummary.CXSummaryID
                             select new CXSummaryVo
                             {
                                 CXSummaryID = tbCXSummary.CXSummaryID,
                                 JiLuBianHao = tbCXSummary.JiLuBianHao,
                                 HuiYuanXinMing = tbCXSummary.HuiYuanXinMing,
                                 YuanKaHao = tbCXSummary.YuanKaHao,
                                 XinKaHao = tbCXSummary.XinKaHao,
                                 UXsable = tbCXSummary.UXsable,
                                 Amount = tbCXSummary.Amount,
                                 ZhaiYao = tbCXSummary.ZhaiYao,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<CXSummaryVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<CXSummaryVo> bsgrid = new Bsgrid<CXSummaryVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectHuoQuKaXinXi(int HTID)//获取信息
        {
            if (HTID > 0)
            {
                var listGoods = (from tbCXSummary in MyModels.S_CXSummaryList
                                 where tbCXSummary.CXSummaryID == HTID
                                 select new Vo.CXSummaryVo
                                 {
                                     CXSummaryID = tbCXSummary.CXSummaryID,
                                     JiLuBianHao = tbCXSummary.JiLuBianHao,
                                     HuiYuanXinMing = tbCXSummary.HuiYuanXinMing,
                                     YuanKaHao = tbCXSummary.YuanKaHao,
                                     XinKaHao = tbCXSummary.XinKaHao,
                                     UXsable = tbCXSummary.UXsable,
                                     Amount = tbCXSummary.Amount,
                                     ZhaiYao = tbCXSummary.ZhaiYao,
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        #endregion

        public ActionResult Select()
        {
            var listSeltct = (from tbSelect in MyModels.S_CXSummaryList
                              select new Vo.CXSummaryVo
                              {
                                  JiLuBianHao = tbSelect.JiLuBianHao,
                                  HuiYuanXinMing = tbSelect.HuiYuanXinMing,
                                  YuanKaHao = tbSelect.YuanKaHao,
                                  XinKaHao = tbSelect.XinKaHao,
                                  UXsable = tbSelect.UXsable,
                                  Amount = tbSelect.Amount,
                                  ZhaiYao = tbSelect.ZhaiYao,
                              }).ToList();
            return Json(listSeltct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BtnInser()//保存
        {
            Models.S_CXSummaryList Delst = new Models.S_CXSummaryList();
            Delst.JiLuBianHao = Request.Form["JiLuBianHao"];
            Delst.HuiYuanXinMing = Request.Form["HuiYuanXinMing"];
            Delst.YuanKaHao = Request.Form["YuanKaHao"];
            Delst.XinKaHao = Request.Form["XinKaHao"];
            Delst.UXsable = Request.Form["UXsable"];
            Delst.Amount = Request.Form["Amount"];
            Delst.ZhaiYao = Request.Form["ZhaiYao"];
            if (Delst.JiLuBianHao != null && Delst.YuanKaHao != null && Delst.HuiYuanXinMing != null && Delst.XinKaHao != null && Delst.UXsable != null && Delst.Amount != null && Delst.ZhaiYao != null)
            {
                MyModels.S_CXSummaryList.Add(Delst);
                MyModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DeleteCXSummar(int CXSummaryId)//删除信息
        {
            string strMsg = "fail";//定义返回

            //查询数据
            Models.S_CXSummaryList dbCXSummary = (from tbCXSummary in MyModels.S_CXSummaryList
                                                  where tbCXSummary.CXSummaryID == CXSummaryId
                                                  select tbCXSummary).Single();
            MyModels.S_CXSummaryList.Remove(dbCXSummary);//删除
            if (MyModels.SaveChanges() > 0)
            {
                strMsg = "success";
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
    }
}
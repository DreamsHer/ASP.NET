using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaJiChuXinXi.Controllers
{
    public class HuiYuanKaLeiController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: HuiYuanKaJiChuXinXi/HuiYuanKaLei
        public ActionResult KaZhongXuanZhe()
        {
            return View();
        }
        public ActionResult KaLeiXingDingYi()
        {
            return View();
        }
        public ActionResult SelectKaZhobg(BsgridPage bsgridPage)//选择卡种信息
        {
            var listGoods = (from tbCXSummary in MyModels.S_MembershipCardList
                             orderby tbCXSummary.MembershipCardID
                             select new MembershipCardVo
                             {
                                 MembershipCardID = tbCXSummary.MembershipCardID,
                                 MembershipCardMC = tbCXSummary.MembershipCardMC,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<MembershipCardVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<MembershipCardVo> bsgrid = new Bsgrid<MembershipCardVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectKaLeiXing(BsgridPage bsgridPage)//选择卡类信息
        {
            var listGoods = (from tbMembershipCardType in MyModels.S_MembershipCardTypeList
                             orderby tbMembershipCardType.MembershipCardTypeID
                             select new MembershipCardTypeVo
                             {
                                 MembershipCardTypeID = tbMembershipCardType.MembershipCardTypeID,
                                 MembershipCardTypeMC = tbMembershipCardType.MembershipCardTypeMC,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<MembershipCardTypeVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<MembershipCardTypeVo> bsgrid = new Bsgrid<MembershipCardTypeVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Select()
        {
            var listSeltct = (from tbSelect in MyModels.S_MembershipCardTypeList
                              select new Vo.MembershipCardTypeVo
                              {
                                  MembershipCardTypeID = tbSelect.MembershipCardTypeID,
                                  MembershipCardTypeMC = tbSelect.MembershipCardTypeMC
                              }).ToList();
            return Json(listSeltct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BtnInser()//保存
        {
            Models.S_MembershipCardTypeList Delst = new Models.S_MembershipCardTypeList();
            Delst.MembershipCardTypeMC = Request.Form["MembershipCardTypeMC"];
            if (Delst.MembershipCardTypeMC != null)
            {
                MyModels.S_MembershipCardTypeList.Add(Delst);
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
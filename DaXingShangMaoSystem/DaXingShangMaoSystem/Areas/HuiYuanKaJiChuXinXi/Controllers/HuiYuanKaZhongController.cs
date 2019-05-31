using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DaXingShangMaoSystem.Areas.HuiYuanKaJiChuXinXi.Controllers
{
    public class HuiYuanKaZhongController : Controller
    {
        // GET: HuiYuanKaJiChuXinXi/HuiYuanKaZhong
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult KaZhongDinYi()
        {
            return View();
        }

        public ActionResult SelectKaZhobg(BsgridPage bsgridPage)//选择信息
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

        public ActionResult Select()
        {
            var listSeltct = (from tbSelect in MyModels.S_MembershipCardList
                              select new Vo.MembershipCardVo
                              {
                                  MembershipCardID = tbSelect.MembershipCardID,
                                  MembershipCardMC = tbSelect.MembershipCardMC
                              }).ToList();
            return Json(listSeltct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BtnInser()//保存
        {
            Models.S_MembershipCardList Delst = new Models.S_MembershipCardList();
            Delst.MembershipCardMC = Request.Form["MembershipCardMC"];
            if (Delst.MembershipCardMC != null)
            {
                MyModels.S_MembershipCardList.Add(Delst);
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
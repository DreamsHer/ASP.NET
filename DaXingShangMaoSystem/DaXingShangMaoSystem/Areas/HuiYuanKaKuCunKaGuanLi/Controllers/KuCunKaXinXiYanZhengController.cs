using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaKuCunKaGuanLi.Controllers
{
    public class KuCunKaXinXiYanZhengController : Controller
    {
        // GET: HuiYuanKaKuCunKaGuanLi/KuCunKaXinXiYanZheng
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult XinXiYanZheng()
        {
            return View();
        }
        public ActionResult SelectCard(BsgridPage bsgridPage)//查询库存卡信息
        {
            var linqItme = from tbMember in MyModels.S_StoreCardList
                           orderby tbMember.StoreCardID
                           select tbMember;

            int intTotalRow = linqItme.Count();

            List<Models.S_StoreCardList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_StoreCardList> bsgrid = new Bsgrid<Models.S_StoreCardList>();//实例化 Bsgrid的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
    }
}
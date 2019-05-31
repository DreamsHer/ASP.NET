using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanJianKaYuFaFang.Controllers
{
    public class HuiYuanKaJianKaController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: HuiYuanJianKaYuFaFang/HuiYuanKaJianKa
        public ActionResult JianKa()
        {
            return View();
        }
        #region 查询
        public ActionResult SelectJianka(BsgridPage bsgridPage)//查询表
        {
            var linqItme = from tbCardsList in MyModels.S_TheCardsList
                           orderby tbCardsList.TheCardsID
                           select tbCardsList;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_TheCardsList> listnTypeTables = linqItme
                                      .Skip(bsgridPage.GetStartIndex())
                                      .Take(bsgridPage.pageSize)
                                      .ToList();
            Bsgrid<Models.S_TheCardsList> bsgrid = new Bsgrid<Models.S_TheCardsList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新增
        public ActionResult InsertTGE(string Kahao, string CardPersonalizationNumber, string CardPersonalizationStatus,string CardPeriodOfValidity)//新增卡号信息
        {
            string strTGE = "falied";
            try
            {
                //实例化对象
                Models.S_TheCardsList myTGE = new Models.S_TheCardsList();
                //给表对象属性赋值
                myTGE.Kahao = Kahao.Trim();
                myTGE.CardPersonalizationNumber = CardPersonalizationNumber.Trim();
                myTGE.CardPersonalizationStatus = CardPersonalizationStatus.Trim();
                myTGE.CardPeriodOfValidity = CardPeriodOfValidity.Trim();
                //把表对象实体模型中
                MyModels.S_TheCardsList.Add(myTGE);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strTGE = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strTGE, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 下拉框  
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
        public ActionResult SelectZhiKaBuMeng()
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_TheCardsbranchList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.TheCardsbranchID,
                                              text = tbAcademe.Store
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectZhiKaMingCheng()
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_TheCardsbranchList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.TheCardsbranchID,
                                              text = tbAcademe.TheCardsbranchMC
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 信息获取
        public ActionResult SelectHeTheCard(BsgridPage bsgridPage)//选择信息
        {
            var listGoods = (from tbTheCards in MyModels.S_TheCardsList
                             orderby tbTheCards.TheCardsID
                             select new TheCardVo
                             {
                                 TheCardsID = tbTheCards.TheCardsID,
                                 Ccustodian  = tbTheCards.Ccustodian ,
                                 Kahao = tbTheCards.Kahao,
                                 TheCardsNumber = tbTheCards.TheCardsNumber,
                                 CardPeriodOfValidity = tbTheCards.CardPeriodOfValidity,
                                 CardPersonalizationNumber = tbTheCards.CardPersonalizationNumber,
                                 Recordnumber = tbTheCards.Recordnumber
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<TheCardVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<TheCardVo> bsgrid = new Bsgrid<TheCardVo>();
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
                var listGoods = (from tbTheCards in MyModels.S_TheCardsList
                                 where tbTheCards.TheCardsID == HTID
                                 select new Vo.TheCardVo
                                 {
                                     TheCardsID = tbTheCards.TheCardsID,
                                     Ccustodian = tbTheCards.Ccustodian,
                                     Kahao = tbTheCards.Kahao,
                                     TheCardsNumber = tbTheCards.TheCardsNumber,
                                     CardPeriodOfValidity = tbTheCards.CardPeriodOfValidity,
                                     CardPersonalizationNumber = tbTheCards.CardPersonalizationNumber,
                                     Recordnumber = tbTheCards.Recordnumber
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaKuCunKaGuanLi.Controllers
{
    public class KuCunKaYouXiaoQiBianGengController : Controller
    {
        // GET: HuiYuanKaKuCunKaGuanLi/KuCunKaYouXiaoQiBianGeng
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult YouXiaoQiGengGai()
        {
            return View();
        }
        #region 查询表
        public ActionResult InsertCardInfoEffectiveList(BsgridPage bsgridPage)
        {
            var linqItme = from tbMember in MyModels.B_EffectiveList
                           orderby tbMember.EffectiveID
                           select tbMember;

            int intTotalRow = linqItme.Count();

            List<Models.B_EffectiveList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.B_EffectiveList> bsgrid = new Bsgrid<Models.B_EffectiveList>();//实例化 Bsgrid的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新增
        public ActionResult InsertStore(string CardNber, string YEffective,string XEffective, string Sum, string EctMoney)//新增信息
        {
            string strStore = "falied";
            try
            {
                //实例化对象
                Models.B_EffectiveList myStore = new Models.B_EffectiveList();
                //给表对象属性赋值
                myStore.CardNber = CardNber.Trim();
                myStore.YEffective = YEffective.Trim();
                myStore.XEffective = XEffective.Trim();
                myStore.Sum = Sum.Trim();
                myStore.EctMoney = EctMoney.Trim();
                //把表对象实体模型中
                MyModels.B_EffectiveList.Add(myStore);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strStore = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strStore, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除
        public ActionResult DeleteEffective(int EffectiveId)//删除
        {
            string strMsg = "fail";//定义返回

            //查询数据
            Models.B_EffectiveList dbMemberAge = (from tbEffective in MyModels.B_EffectiveList
                                                  where tbEffective.EffectiveID == EffectiveId
                                                  select tbEffective).Single();
            MyModels.B_EffectiveList.Remove(dbMemberAge);//删除
            if (MyModels.SaveChanges() > 0)
            {
                strMsg = "success";
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
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
                                 YouXiaoQiKa = tbCXSummary.YouXiaoQiKa,
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
                                 select new CXSummaryVo
                                 {
                                     CXSummaryID = tbCXSummary.CXSummaryID,
                                     JiLuBianHao = tbCXSummary.JiLuBianHao,
                                     YouXiaoQiKa = tbCXSummary.YouXiaoQiKa,
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

        #region 新增信息
        public ActionResult Select()
        {
            var listSeltct = (from tbSelect in MyModels.S_CXSummaryList
                              select new Vo.CXSummaryVo
                              {
                                  JiLuBianHao = tbSelect.JiLuBianHao,
                                  YouXiaoQiKa = tbSelect.YouXiaoQiKa,
                                  ZhaiYao = tbSelect.ZhaiYao,
                              }).ToList();
            return Json(listSeltct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BtnInser()//保存
        {
            Models.S_CXSummaryList Delst = new Models.S_CXSummaryList();
            Delst.JiLuBianHao = Request.Form["JiLuBianHao"];
            Delst.YouXiaoQiKa = Request.Form["YouXiaoQiKa"];
            Delst.ZhaiYao = Request.Form["ZhaiYao"];
            if (Delst.JiLuBianHao != null && Delst.YouXiaoQiKa !=null && Delst.ZhaiYao != null)
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
        #endregion

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
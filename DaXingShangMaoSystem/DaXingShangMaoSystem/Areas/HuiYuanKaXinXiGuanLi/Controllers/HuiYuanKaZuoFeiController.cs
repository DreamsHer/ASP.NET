using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaXinXiGuanLi.Controllers
{
    public class HuiYuanKaZuoFeiController : Controller
    {
        // GET: HuiYuanKaXinXiGuanLi/HuiYuanKaZuoFei
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult HuiYuanKaZuoFei()
        {
            return View();
        }
        #region  查询表
        public ActionResult InsertMembershipCardInfo(BsgridPage bsgridPage)
        {
            var linqItme = from tbMember in MyModels.S_MembershipCardInfoList
                           orderby tbMember.MembershipCardInfoID
                           select tbMember;

            int intTotalRow = linqItme.Count();

            List<Models.S_MembershipCardInfoList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MembershipCardInfoList> bsgrid = new Bsgrid<Models.S_MembershipCardInfoList>();//实例化 Bsgrid的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新增
        public ActionResult InsertStore(string CardCode, string Ccustodian, string CredentialsNumber, string GrandTotalAmount, string Sum)//新增信息
        {
            string strStore = "falied";
            try
            {
                //实例化对象
                Models.S_MembershipCardInfoList myStore = new Models.S_MembershipCardInfoList();
                //给表对象属性赋值
                myStore.CardCode = CardCode.Trim();
                myStore.Ccustodian = Ccustodian.Trim();
                myStore.CredentialsNumber = CredentialsNumber.Trim();
                myStore.GrandTotalAmount = GrandTotalAmount.Trim();
                myStore.Sum = Sum.Trim();
                //把表对象实体模型中
                MyModels.S_MembershipCardInfoList.Add(myStore);
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

        #region 删除表
        public ActionResult DeleteMembershipCardInfo(int MembershipCardInfoId)//删除
        {
            string strMsg = "fail";//定义返回

            //查询数据
            Models.S_MembershipCardInfoList dbMemberAge = (from tbMembershipCardInfo in MyModels.S_MembershipCardInfoList
                                                           where tbMembershipCardInfo.MembershipCardInfoID == MembershipCardInfoId
                                                           select tbMembershipCardInfo).Single();
            MyModels.S_MembershipCardInfoList.Remove(dbMemberAge);//删除
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
                                 HuiYuanXinMing = tbCXSummary.HuiYuanXinMing,
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

        #region 新增信息
        public ActionResult Select()
        {
            var listSeltct = (from tbSelect in MyModels.S_CXSummaryList
                              select new Vo.CXSummaryVo
                              {
                                  JiLuBianHao = tbSelect.JiLuBianHao,
                                  HuiYuanXinMing = tbSelect.HuiYuanXinMing,
                                  Amount = tbSelect.Amount,
                                  ZhaiYao = tbSelect.ZhaiYao
                              }).ToList();
            return Json(listSeltct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BtnInsert()//保存
        {
            Models.S_CXSummaryList Delst = new Models.S_CXSummaryList();
            Delst.JiLuBianHao = Request.Form["JiLuBianHao"];
            Delst.HuiYuanXinMing = Request.Form["HuiYuanXinMing"];
            Delst.Amount = Request.Form["Amount"];
            Delst.ZhaiYao = Request.Form["ZhaiYao"];
            if (Delst.JiLuBianHao != null && Delst.HuiYuanXinMing != null && Delst.Amount != null && Delst.ZhaiYao != null)
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
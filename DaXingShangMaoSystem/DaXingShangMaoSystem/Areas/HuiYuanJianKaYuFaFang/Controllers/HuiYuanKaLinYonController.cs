using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanJianKaYuFaFang.Controllers
{
    public class HuiYuanKaLinYonController : Controller
    {
        // GET: HuiYuanJianKaYuFaFang/HuiYuanKaLinYon
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult LinYon()
        {
            return View();
        }
        public ActionResult ChaXunFuHeTianJianKa()
        {
            return View();
        }
        #region 查询表
        public ActionResult InsertCardInfo(BsgridPage bsgridPage)
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
        public ActionResult InsertInfo(string CardNber, string Ccustodian, string StartCardNbumer, string EndCardNber,string CardNumber,string GrandTotalAmount)//新增卡号信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象
                Models.S_MembershipCardInfoList myInfo = new Models.S_MembershipCardInfoList();
                //给表对象属性赋值
                myInfo.CardNber = CardNber.Trim();
                myInfo.Ccustodian = Ccustodian.Trim();
                myInfo.StartCardNbumer = StartCardNbumer.Trim();
                myInfo.EndCardNber = EndCardNber.Trim();
                myInfo.CardNumber = CardNumber.Trim();
                myInfo.GrandTotalAmount = GrandTotalAmount.Trim();
                //把表对象实体模型中
                MyModels.S_MembershipCardInfoList.Add(myInfo);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除
        public ActionResult DeleteMemberInfo(int MembershipCardInfoId)//删除
        {
            string strMsg = "fail";//定义返回

            //查询数据
            DaXingShangMaoSystem.Models.S_MembershipCardInfoList dbMemberAge = (from tbMemberAge in MyModels.S_MembershipCardInfoList
                                                      where tbMemberAge.MembershipCardInfoID == MembershipCardInfoId
                                                           select tbMemberAge).Single();
            MyModels.S_MembershipCardInfoList.Remove(dbMemberAge);//删除
            if (MyModels.SaveChanges() > 0)
            {
                strMsg = "success";
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 下拉框
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
        public ActionResult SelectHuiKaZhong()
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
        public ActionResult SelectSouKaMingCheng()
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
        public ActionResult SelectSouKaBuMeng()
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
        public ActionResult SelectKaLEi()
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
        public ActionResult SelectSouKaZhong()
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
                                 ZuoFeiShuLiang = tbCXSummary.ZuoFeiShuLiang,
                                 HuiYuanXinMing = tbCXSummary.HuiYuanXinMing,
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
                                     ZuoFeiShuLiang = tbCXSummary.ZuoFeiShuLiang,
                                     HuiYuanXinMing = tbCXSummary.HuiYuanXinMing,
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        #endregion

        #region 卡信息获取
        public ActionResult SelectMembershipCardInf(BsgridPage bsgridPage)//选择卡信息
        {
            var listGoods = (from tbMembershipCardInfo in MyModels.S_MembershipCardInfoList
                             orderby tbMembershipCardInfo.MembershipCardInfoID
                             select new MembershipCardInfoVo
                             {
                                 MembershipCardInfoID = tbMembershipCardInfo.MembershipCardInfoID,
                                 CardNumber = tbMembershipCardInfo.CardNumber,
                                 StartCardNbumer = tbMembershipCardInfo.StartCardNbumer,
                                 EndCardNber = tbMembershipCardInfo.EndCardNber,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<MembershipCardInfoVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<MembershipCardInfoVo> bsgrid = new Bsgrid<MembershipCardInfoVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectMembershipCardInfo(int HTID)//获取卡信息
        {
            if (HTID > 0)
            {
                var listGoods = (from tbMembershipCardInfo in MyModels.S_MembershipCardInfoList
                                 where tbMembershipCardInfo.MembershipCardInfoID == HTID
                                 select new Vo.MembershipCardInfoVo
                                 {
                                     MembershipCardInfoID = tbMembershipCardInfo.MembershipCardInfoID,
                                     CardNumber = tbMembershipCardInfo.CardNumber,
                                     StartCardNbumer = tbMembershipCardInfo.StartCardNbumer,
                                     EndCardNber = tbMembershipCardInfo.EndCardNber,
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
            #endregion
        }
    }
}
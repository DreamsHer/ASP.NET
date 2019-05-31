using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanJianKaYuFaFang.Controllers
{
    public class HuiYuanPiLiangFaFangController : Controller
    {
        // GET: HuiYuanJianKaYuFaFang/HuiYuanPiLiangFaFang
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult PiLiangFaFang()
        {
            return View();
        }
        #region 查询表
        public ActionResult SelectFaFang()
        {
            var listFaFang = from tbFaFang in MyModels.S_MakeCollectionsList
                             select new
                             {
                                 tbFaFang.MakeCollectionsID,
                                 tbFaFang.MakeCollectionsMC,
                                 tbFaFang.Amount
                             };
            return Json(listFaFang,JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectMembershipCardInfo(BsgridPage bsgridPage)//查询
        {
            var linqItme = from tbMember in MyModels.S_MembershipCardInfoList
                           orderby tbMember.MembershipCardInfoID 
                           select tbMember;

            int intTotalRow = linqItme.Count();

            List<Models.S_MembershipCardInfoList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MembershipCardInfoList> bsgrid = new Vo.Bsgrid<DaXingShangMaoSystem.Models.S_MembershipCardInfoList>();//实例化 Bsgrid的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新增
        public ActionResult InsertTGE(string StartCardNbumer, string EndCardNber, string CardNumber)//新增卡号信息
        {
            string strTGE = "falied";
            try
            {
                //实例化对象
                Models.S_MembershipCardInfoList myTGE = new Models.S_MembershipCardInfoList();
                //给表对象属性赋值
                myTGE.StartCardNbumer = StartCardNbumer.Trim();
                myTGE.EndCardNber = EndCardNber.Trim();
                myTGE.CardNumber = CardNumber.Trim();
                //把表对象实体模型中
                MyModels.S_MembershipCardInfoList.Add(myTGE);
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
        public ActionResult SelectHeellingInfo(BsgridPage bsgridPage)//选择信息
        {
            var listGoods = (from tbSellingInfo in MyModels.S_SellingInfoList
                             orderby tbSellingInfo.SellingInfoID
                             select new SellingInfoVo
                             {
                                 SellingInfoID = tbSellingInfo.SellingInfoID,
                                 Gtotal = tbSellingInfo.Gtotal,
                                 SingleFee = tbSellingInfo.SingleFee,
                                 FeeAmount = tbSellingInfo.FeeAmount,
                                 RAmount  = tbSellingInfo.RAmount ,
                                 XAmount  = tbSellingInfo.XAmount ,
                                 Summary = tbSellingInfo.Summary
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<SellingInfoVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<SellingInfoVo> bsgrid = new Bsgrid<SellingInfoVo>();
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
                var listGoods = (from tbSellingInfo in MyModels.S_SellingInfoList
                                 where tbSellingInfo.SellingInfoID == HTID
                                 select new Vo.SellingInfoVo
                                 {
                                     SellingInfoID = tbSellingInfo.SellingInfoID,
                                     Gtotal = tbSellingInfo.Gtotal,
                                     SingleFee = tbSellingInfo.SingleFee,
                                     FeeAmount = tbSellingInfo.FeeAmount,
                                     RAmount = tbSellingInfo.RAmount,
                                     XAmount = tbSellingInfo.XAmount,
                                     Summary = tbSellingInfo.Summary
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        public ActionResult SelectCXSummary(BsgridPage bsgridPage)//选择信息
        {
            var listGoods = (from tbCXSummary in MyModels.S_CXSummaryList
                             orderby tbCXSummary.CXSummaryID
                             select new CXSummaryVo
                             {
                                 CXSummaryID = tbCXSummary.CXSummaryID,
                                 HuiYuanXinMing = tbCXSummary.HuiYuanXinMing,
                                 JiLuBianHao = tbCXSummary.JiLuBianHao
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

        public ActionResult SelectHuoQuKaXinX(int HTID)//获取信息
        {
            if (HTID > 0)
            {
                var listGoods = (from tbCXSummary in MyModels.S_CXSummaryList
                                 where tbCXSummary.CXSummaryID == HTID
                                 select new Vo.CXSummaryVo
                                 {
                                     CXSummaryID = tbCXSummary.CXSummaryID,
                                     HuiYuanXinMing = tbCXSummary.HuiYuanXinMing,
                                     JiLuBianHao = tbCXSummary.JiLuBianHao
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
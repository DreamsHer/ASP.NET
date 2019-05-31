using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Common;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaJiChuXinXi.Controllers
{
    public class DiYiBaoGuanDiDianController : Controller
    {
        // GET: HuiYuanKaJiChuXinXi/DiYiBaoGuanDiDian
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult BaoGuanDiDian()
        {
            return View();
        }
        #region 下拉框
        public ActionResult SelectHuiZhiKa()
        {
            List<SelectVo> listbranch = (from tbbranch in MyModels.S_TheCardsbranchList
                                          select new SelectVo
                                          {
                                              id = tbbranch.TheCardsbranchID,
                                              text = tbbranch.Store
                                          }).ToList();
            listbranch = Common.Tools.SetSelectJson(listbranch);//设置selectjson
            return Json(listbranch, JsonRequestBehavior.AllowGet);
        }
        #endregion

        

        #region 
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        //public ActionResult ChaXunShuXing()
        //{
        //    string strJsonNewGoodsManage = MyModels.S_TheCardsbranchList.ToList().GetJSONTreeData1
        //        ("TheCardsbranchID", "TheCardsbranchMC", "S_TheCardsbranchID");
        //    return Content(strJsonNewGoodsManage);
        //}


        #endregion

        #region 信息获取
        public ActionResult SelectTheCardsbranch(BsgridPage bsgridPage)//选择信息
        {
            var listGoods = (from tbAreaCode in MyModels.S_TheCardsbranchList
                             orderby tbAreaCode.TheCardsbranchID
                             select new TheCardsbranchVo
                             {
                                 TheCardsbranchID = tbAreaCode.TheCardsbranchID,
                                 TheCardsbranchMC = tbAreaCode.TheCardsbranchMC,
                                 CodeRule = tbAreaCode.CodeRule,
                                 Code = tbAreaCode.Code,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<TheCardsbranchVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<TheCardsbranchVo> bsgrid = new Bsgrid<TheCardsbranchVo>();
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
                var listGoods = (from tbAreaCode in MyModels.S_TheCardsbranchList
                                 where tbAreaCode.TheCardsbranchID == HTID
                                 select new Vo.TheCardsbranchVo
                                 {
                                     TheCardsbranchID = tbAreaCode.TheCardsbranchID,
                                     TheCardsbranchMC = tbAreaCode.TheCardsbranchMC,
                                     CodeRule = tbAreaCode.CodeRule,
                                     Code = tbAreaCode.Code,
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
            var listAreaCode = (from tbAreaCode in MyModels.S_TheCardsbranchList
                                select new Vo.TheCardsbranchVo 
                                {
                                    TheCardsbranchMC = tbAreaCode.TheCardsbranchMC,
                                    Code = tbAreaCode.Code,
                                    CodeRule = tbAreaCode.CodeRule,
                                }).ToList();
            return Json(listAreaCode, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BtnInsert()//保存
        {
            Models.S_TheCardsbranchList Delst = new Models.S_TheCardsbranchList();
            Delst.TheCardsbranchMC = Request.Form["TheCardsbranchMC"];
            Delst.Code = Request.Form["Code"];
            Delst.CodeRule = Request.Form["CodeRule"];
            if (Delst.TheCardsbranchMC != null && Delst.Code != null && Delst.CodeRule != null)
            {
                MyModels.S_TheCardsbranchList.Add(Delst);
                MyModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
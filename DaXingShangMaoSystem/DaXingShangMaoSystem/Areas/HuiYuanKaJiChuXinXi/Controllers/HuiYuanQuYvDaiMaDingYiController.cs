using DaXingShangMaoSystem.Common;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaJiChuXinXi.Controllers
{
    public class HuiYuanQuYvDaiMaDingYiController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: HuiYuanKaJiChuXinXi/HuiYuanQuYvDaiMaDingYi
        public ActionResult QuYvDaiMa()
        {
            return View();
        }

        #region 
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        //public ActionResult ChaXunShuXinger()
        //{
        //    string strJsonNewGoodsManage = MyModels.S_AreaCodeList.ToList().GetJSONTreeData1
        //        ("AreaID", "AreaMC", "S_AreaID");
        //    return Content(strJsonNewGoodsManage);
        //}

       
        #endregion

        #region 新增信息
        public ActionResult Select()
        {
            var listAreaCode = (from tbAreaCode in MyModels.S_AreaCodeList
                              select new Vo.AreaCodeVo
                              {
                                  AreaMC = tbAreaCode.AreaMC,
                                  Postalcode = tbAreaCode.Postalcode,
                                  CodeRule = tbAreaCode.CodeRule,
                                  DaiMa = tbAreaCode.DaiMa 
                              }).ToList();
            return Json(listAreaCode, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BtnInsert()//保存
        {
            Models.S_AreaCodeList Delst = new Models.S_AreaCodeList();
            Delst.AreaMC = Request.Form["AreaMC"];
            Delst.Postalcode = Request.Form["Postalcode"];
            Delst.CodeRule = Request.Form["CodeRule"];
            Delst.DaiMa = Request.Form["DaiMa"];
            if (Delst.AreaMC != null && Delst.Postalcode != null && Delst.CodeRule != null)
            {
                MyModels.S_AreaCodeList.Add(Delst);
                MyModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 信息获取
        public ActionResult SelectAreaCode(BsgridPage bsgridPage)//选择信息
        {
            var listGoods = (from tbAreaCode in MyModels.S_AreaCodeList
                             orderby tbAreaCode.AreaID
                             select new AreaCodeVo
                             {
                                 AreaID = tbAreaCode.AreaID,
                                 AreaMC = tbAreaCode.AreaMC,
                                 Postalcode = tbAreaCode.Postalcode,
                                 CodeRule = tbAreaCode.CodeRule,
                                 DaiMa = tbAreaCode.DaiMa,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<AreaCodeVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<AreaCodeVo> bsgrid = new Bsgrid<AreaCodeVo>();
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
                var listGoods = (from tbAreaCode in MyModels.S_AreaCodeList
                                 where tbAreaCode.AreaID == HTID
                                 select new Vo.AreaCodeVo
                                 {
                                     AreaID = tbAreaCode.AreaID,
                                     AreaMC = tbAreaCode.AreaMC,
                                     Postalcode = tbAreaCode.Postalcode,
                                     CodeRule = tbAreaCode.CodeRule,
                                     DaiMa = tbAreaCode.DaiMa,
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
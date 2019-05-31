using DaXingShangMaoSystem.Common;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.ShangPinGuanLi.Controllers
{  
    public class JiChuXinXiController : Controller
    {
        // GET: ShangPinGuanLi/JiChuXinXi
        //商品基础信息、打包商品管理
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        #region 定义商品分类      
        public ActionResult ShangPinFeiLei()
        {
            return View();
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllGoodsClassify()
        {    
            string strJsonGoodsClassify = MyModels.B_GoodsClassifyList.ToList().GetJSONTreeData("GoodsClassifyID", "GoodsClassifyName", "F_GoodsClassifyID");
            return Content(strJsonGoodsClassify);
        }

        /// <summary>
        ///  把list结构转化为json字符串
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fid"></param>
        /// <returns></returns>
        public string GetGoodsClassifyTreeData(List<Models.B_GoodsClassifyList> list, int fid)
        {
            StringBuilder sbTree = new StringBuilder();
            List<Models.B_GoodsClassifyList> listNode = list.Where(m => m.F_GoodsClassifyID == fid).ToList();
            if (listNode.Count > 0)
            {
                //有节点存在
                sbTree.Append("[");
                for (int i = 0; i < listNode.Count; i++)
                {
                    //获取子节点
                    int proid = listNode[i].GoodsClassifyID;
                    //判断当前节点是否有子节点
                    string sbChild = GetGoodsClassifyTreeData(list, proid);

                    //获取json格式
                    if (sbChild.ToString() != "")
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].GoodsClassifyID + "\",\"text\":\"" + listNode[i].GoodsClassifyName + "\",\"children\":");
                        sbTree.Append(sbChild);
                        sbTree.Append("},");
                    }
                    else
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].GoodsClassifyID + "\",\"text\":\"" + listNode[i].GoodsClassifyName + "\"},");
                    }
                }
                //没有子节点            
                sbTree.Remove(sbTree.Length - 1, 1);
                sbTree.Append("]");
            }
            return sbTree.ToString();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="intTargetId"></param>
        /// <param name="intSourceId"></param>
        /// <returns></returns>
        public ActionResult UpdateFather(int intTargetId, int intSourceId)
        {
            var modelDrugType = MyModels.B_GoodsClassifyList.Find(intSourceId);
            modelDrugType.F_GoodsClassifyID = intTargetId;
            MyModels.SaveChanges();
            return new EmptyResult();
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="modelClassify"></param>
        /// <returns></returns>
        public ActionResult UpdateDrug(Models.B_GoodsClassifyList modelClassify)
        {
            MyModels.Entry(modelClassify).State = System.Data.Entity.EntityState.Modified;
            MyModels.SaveChanges();
            return Content("OK");
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="modelClassify"></param>
        /// <returns></returns>
        public ActionResult InsertClassify(Models.B_GoodsClassifyList modelClassify)
        {
            MyModels.Entry(modelClassify).State = System.Data.Entity.EntityState.Added;
            MyModels.SaveChanges();
            int goodsclassifyid = modelClassify.GoodsClassifyID;

            return Json(goodsclassifyid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="intClassifyId"></param>
        /// <returns></returns>
        public ActionResult RemoveRoute(int intClassifyId)
        {
            var modelRoute = MyModels.B_GoodsClassifyList.Find(intClassifyId);
            MyModels.B_GoodsClassifyList.Remove(modelRoute);
            MyModels.SaveChanges();
            return Content("Ok");
        }

        #endregion

        #region 定义商品商标
        /// <summary>
        /// 定义商品商标界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShangPinShangBiao()
        {
            return View();
        }
        /// <summary>
        /// 商标状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectTrademarkStatusID()
        {           
            List<SelectVo> listTrademarkStatus = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listTrademarkStatus.Add(selectVo);

            List<SelectVo> listTrademarkStatusID = (from tbTrademarkStatus in MyModels.S_TrademarkStatusList
                                               select new SelectVo
                                               {
                                                   id = tbTrademarkStatus.TrademarkStatusID,
                                                   text = tbTrademarkStatus.TrademarkStatusName.Trim(),
                                               }).ToList();

            listTrademarkStatus.AddRange(listTrademarkStatusID);

            return Json(listTrademarkStatus, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 商标级别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectTrademarkRankID()
        {
            List<SelectVo> listTrademarkRank = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择---"
            };
            listTrademarkRank.Add(selectVo);
            List<SelectVo> listTrademarkRankID = (from tbTrademarkRank in MyModels.S_TrademarkRankList
                                                  select new SelectVo
                                                  {
                                                      id = tbTrademarkRank.TrademarkRankID,
                                                      text = tbTrademarkRank.TrademarkRankName.Trim()
                                                  }).ToList();
            listTrademarkRank.AddRange(listTrademarkRankID);
            return Json(listTrademarkRank, JsonRequestBehavior.AllowGet);                     
        }
        /// <summary>
        /// 商标地域
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectTrademarkLocalID()
        {           
            List<SelectVo> listTrademarkLocal = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listTrademarkLocal.Add(selectVo);

            List<SelectVo> listTrademarkLocalID = (from tbTrademarkLocal in MyModels.S_TrademarkLocalList
                                               select new SelectVo
                                               {
                                                   id = tbTrademarkLocal.TrademarkLocalID,
                                                   text = tbTrademarkLocal.TrademarkLocalName.Trim()
                                               }).ToList();

            listTrademarkLocal.AddRange(listTrademarkLocalID);

            return Json(listTrademarkLocal, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 生成商标代码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsChopCode()
        {
            string strGoodsChopCode = "";
            var listDep = (from tbDmp in MyModels.B_GoodsChopList orderby tbDmp.GoodsChopCode select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_GoodsChopList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.GoodsChopCode.Substring(2, 5));
                intCode++;
                strGoodsChopCode = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {
                    strGoodsChopCode = strGoodsChopCode.Length < 4 ? "0" + strGoodsChopCode : strGoodsChopCode;
                }
                strGoodsChopCode = "SB" + strGoodsChopCode;
            }
            else
            {
                strGoodsChopCode = "SB0001";
            }
            return Json(strGoodsChopCode, JsonRequestBehavior.AllowGet);
            //return View();           

        }
        /// <summary>
        /// 查询新增商标信息
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertGoodsChop()
        {
            var listGoodsChop = (from tbGoodsChop in MyModels.B_GoodsChopList
                                 select new Vo.GoodsChop
                                 {
                                     GoodsChopName = tbGoodsChop.GoodsChopName,
                                     SpellCod = tbGoodsChop.SpellCod,
                                     RegisteredAddress = tbGoodsChop.RegisteredAddress,                                     
                                     ReleaseTimeStr = tbGoodsChop.DiplomaValid.ToString(),
                                     RegisteredDiploma = tbGoodsChop.RegisteredDiploma,
                                     Proprietor = tbGoodsChop.Proprietor,
                                     AgentCondition = tbGoodsChop.AgentCondition,
                                 }).ToList();
            return Json(listGoodsChop, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增保存
        /// </summary>
        /// <returns></returns>
        public ActionResult btnInsert(B_GoodsChopList pwGoodsChop)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.B_GoodsChopList
                                             where tbGoodsMoneyRule.GoodsChopCode == pwGoodsChop.GoodsChopCode
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    B_GoodsChopList JJ = new B_GoodsChopList();
                    JJ.GoodsChopName = Request.Form["GoodsChopName"];
                    JJ.SpellCod = Request.Form["SpellCod"];
                    JJ.GoodsChopCode = Request.Form["GoodsChopCode"];
                    JJ.RegisteredAddress = Request.Form["RegisteredAddress"];
                    JJ.DiplomaValid = Convert.ToDateTime(Request.Form["DiplomaValid"]);
                    JJ.RegisteredDiploma = Request.Form["RegisteredDiploma"];
                    JJ.Proprietor = Request.Form["Proprietor"];
                    JJ.AgentCondition = Request.Form["AgentCondition"];
                    JJ.TrademarkRankID = Convert.ToInt32(Request.Form["TrademarkRankID"]);
                    JJ.TrademarkStatusID = Convert.ToInt32(Request.Form["TrademarkStatusID"]);
                    JJ.TrademarkLocalID = Convert.ToInt32(Request.Form["TrademarkLocalID"]);
                    if (JJ.GoodsChopName != null && JJ.SpellCod != null && JJ.GoodsChopCode != null && JJ.RegisteredAddress != null &&
                        JJ.RegisteredDiploma != null && JJ.DiplomaValid != null && JJ.Proprietor != null && JJ.AgentCondition != null
                        && JJ.TrademarkRankID != null && JJ.TrademarkStatusID != null && JJ.TrademarkLocalID != null)
                    {
                        MyModels.B_GoodsChopList.Add(JJ);
                        MyModels.SaveChanges();
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    strMsg = "exsit";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询数据放在table中
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectGoodsChopID(BsgridPage bsgridPage)
        {
            var items = from tbGoodsChop in MyModels.B_GoodsChopList
                        orderby tbGoodsChop.GoodsChopID
                        select new Vo.GoodsChop
                        {
                            GoodsChopID = tbGoodsChop.GoodsChopID,
                            GoodsChopName = tbGoodsChop.GoodsChopName,
                            SpellCod = tbGoodsChop.SpellCod,
                            GoodsChopCode = tbGoodsChop.GoodsChopCode,
                            RegisteredAddress = tbGoodsChop.RegisteredAddress,
                            ReleaseTimeStr = tbGoodsChop.DiplomaValid.ToString(),
                            RegisteredDiploma = tbGoodsChop.RegisteredDiploma,
                        };
            int intTotalRow = items.Count();//总行数
            List<GoodsChop> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<GoodsChop> bsgrid = new Bsgrid<GoodsChop>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult BangDingGoodsChop(int GPID)
        {
            if (GPID > 0)
            {
                var GoodsChop = (from tbEmp in MyModels.B_GoodsChopList
                                 where tbEmp.GoodsChopID == GPID
                                 select new Vo.GoodsChop
                                 {
                                     GoodsChopID = tbEmp.GoodsChopID,
                                     GoodsChopName = tbEmp.GoodsChopName,
                                     SpellCod = tbEmp.SpellCod,
                                     GoodsChopCode = tbEmp.GoodsChopCode,
                                     RegisteredAddress = tbEmp.RegisteredAddress,
                                     ReleaseTimeStr = tbEmp.DiplomaValid.ToString(),
                                     RegisteredDiploma = tbEmp.RegisteredDiploma
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 修改保存
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateGoodsChop()
        {
            string styMy = "fail";
            try
            {
                B_GoodsChopList JJ = new B_GoodsChopList();
                JJ.GoodsChopID = Convert.ToInt32(Request.Form["GoodsChopID"]);
                JJ.GoodsChopName = Request.Form["GoodsChopName"];
                JJ.SpellCod = Request.Form["SpellCod"];
                JJ.GoodsChopCode = Request.Form["GoodsChopCode"];
                JJ.RegisteredAddress = Request.Form["RegisteredAddress"];
                JJ.DiplomaValid = Convert.ToDateTime(Request.Form["DiplomaValid"]);
                JJ.RegisteredDiploma = Request.Form["RegisteredDiploma"];

                MyModels.Entry(JJ).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                styMy = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(styMy, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 定义商品计量单位和使用对象      
        public ActionResult DanWeiHeShuXingDingYi()
        {
            return View();
        }
        /// <summary>
        /// 查询计量单位
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectEstimateUnitID(BsgridPage bsgridPage)
        {
            var linqItme = from tbEstimateUnit in MyModels.S_EstimateUnitList
                           orderby tbEstimateUnit.EstimateUnitID
                           select tbEstimateUnit;
            //查询总行数
            int intTotalRow = linqItme.Count();
            //查询公告类型列表
            //使用Skip...Take...必须使用orderby
            List<S_EstimateUnitList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            //实例化 Bsgrid的返回实体类
            Bsgrid<S_EstimateUnitList> bsgrid = new Bsgrid<S_EstimateUnitList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增计量单位保存
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertEstimateUnit(S_EstimateUnitList pwEstimateUnit)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.S_EstimateUnitList
                                             where tbGoodsMoneyRule.EstimateUnitName == pwEstimateUnit.EstimateUnitName
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    S_EstimateUnitList JJ = new S_EstimateUnitList();
                    JJ.EstimateUnitName = Request.Form["EstimateUnitName"];
                    if (JJ.EstimateUnitName != null)
                    {
                        MyModels.S_EstimateUnitList.Add(JJ);
                        MyModels.SaveChanges();
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    strMsg = "exsit";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);


           
        }

        /// <summary>
        /// 修改计量单位
        /// </summary>
        /// <param name="estimateUnit"></param>
        /// <returns></returns>
        public ActionResult UpdataEstimateUnit(int UnitID)
        {
            if (UnitID > 0)
            {
                var GoodsChop = (from tbEmp in MyModels.S_EstimateUnitList
                                 where tbEmp.EstimateUnitID == UnitID
                                 select new
                                 {
                                     tbEmp.EstimateUnitID,
                                     tbEmp.EstimateUnitName
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 保存计量单位
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdataEstimateUnitBaoCun()
        {
            string styMy = "fail";
            try
            {
                S_EstimateUnitList JJ = new S_EstimateUnitList();
                JJ.EstimateUnitID = Convert.ToInt32(Request.Form["EstimateUnitID"]);
                JJ.EstimateUnitName = Request.Form["EstimateUnitName"];
                MyModels.Entry(JJ).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                styMy = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(styMy, JsonRequestBehavior.AllowGet);
        }       

        /// <summary>
        /// 查询使用对象
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectApplyTargetID(BsgridPage bsgridPage)
        {
            var linqItme = from tbApplyTarget in MyModels.S_ApplyTargetList
                           orderby tbApplyTarget.ApplyTargetID
                           select tbApplyTarget;
            //查询总行数
            int intTotalRow = linqItme.Count();
            //查询公告类型列表
            //使用Skip...Take...必须使用orderby
            List<S_ApplyTargetList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            //实例化 Bsgrid的返回实体类
            Bsgrid<S_ApplyTargetList> bsgrid = new Bsgrid<S_ApplyTargetList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 新增使用对象保存
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertApplyTarget(S_ApplyTargetList pwApplyTarget)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.S_ApplyTargetList
                                             where tbGoodsMoneyRule.ApplyTarget == pwApplyTarget.ApplyTarget
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    S_ApplyTargetList JJ = new S_ApplyTargetList();
                    JJ.ApplyTarget = Request.Form["ApplyTarget"];
                    if (JJ.ApplyTarget != null)
                    {
                        MyModels.S_ApplyTargetList.Add(JJ);
                        MyModels.SaveChanges();
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    strMsg = "exsit";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);

           
        }

        /// <summary>
        /// 修改计量单位
        /// </summary>
        /// <param name="estimateUnit"></param>
        /// <returns></returns>
        public ActionResult UpdataApplyTarget(int applyTargetID)
        {
            if (applyTargetID > 0)
            {
                var GoodsChop = (from tbEmp in MyModels.S_ApplyTargetList
                                 where tbEmp.ApplyTargetID == applyTargetID
                                 select new
                                 {
                                     tbEmp.ApplyTargetID,
                                     tbEmp.ApplyTarget
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 保存使用对象
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdataApplyTargetBaoCun()
        {
            string styMy = "fail";
            try
            {
                S_ApplyTargetList JJ = new S_ApplyTargetList();
                JJ.ApplyTargetID = Convert.ToInt32(Request.Form["ApplyTargetID"]);
                JJ.ApplyTarget = Request.Form["ApplyTarget"];
                MyModels.Entry(JJ).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                styMy = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(styMy, JsonRequestBehavior.AllowGet);
        }       

        #endregion

        #region 定义商品经营季节属性
        public ActionResult ShangPinJingYinJiJie()
        {
            return View();
        }
        /// <summary>
        /// 查询经营季节
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectGoodsSeasonID(BsgridPage bsgridPage)
        {
            var linqItme = from tbGoodsSeason in MyModels.S_GoodsSeasonList
                           orderby tbGoodsSeason.GoodsSeasonID
                           select tbGoodsSeason;
            //查询总行数
            int intTotalRow = linqItme.Count();
            //查询公告类型列表
            //使用Skip...Take...必须使用orderby
            List<S_GoodsSeasonList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            //实例化 Bsgrid的返回实体类
            Bsgrid<S_GoodsSeasonList> bsgrid = new Bsgrid<S_GoodsSeasonList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增经营季节保存
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertGoodsSeason(S_GoodsSeasonList pwApplyTarget)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.S_GoodsSeasonList
                                             where tbGoodsMoneyRule.GoodsSeasonName == pwApplyTarget.GoodsSeasonName
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    S_GoodsSeasonList JJ = new S_GoodsSeasonList();
                    JJ.GoodsSeasonName = Request.Form["GoodsSeasonName"];
                    JJ.KaiShiYue = Request.Form["KaiShiYue"];
                    JJ.KaiShiRi = Request.Form["KaiShiRi"];
                    JJ.JieShuYue = Request.Form["JieShuYue"];
                    JJ.jieShuRi = Request.Form["jieShuRi"];
                    if (JJ.GoodsSeasonName != null && JJ.KaiShiYue != null && JJ.KaiShiRi != null && JJ.JieShuYue != null && JJ.jieShuRi != null)
                    {
                        MyModels.S_GoodsSeasonList.Add(JJ);
                        MyModels.SaveChanges();
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    strMsg = "exsit";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除经营季节
        /// </summary>
        /// <param name="goodsSeasonId"></param>
        /// <returns></returns>
        public ActionResult DeleteGoodsSeason(int goodsSeasonId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                S_GoodsSeasonList dbNoticeTypeDetail =
                (from tbNoticeTypeDetail in MyModels.S_GoodsSeasonList
                 where tbNoticeTypeDetail.GoodsSeasonID == goodsSeasonId
                 select tbNoticeTypeDetail).Single();
                MyModels.S_GoodsSeasonList.Remove(dbNoticeTypeDetail);
                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception e)
            {
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 定义商品分类经营数据
        public ActionResult ShangPinFenLeiJingYinShuJu()
        {
            return View();
        }
        #endregion

        #region 打包商品管理
        /// <summary>
        /// 定义打包商品
        /// </summary>
        /// <returns></returns>
        public ActionResult DingYiDaBaoShangPin()
        {
            return View();
        }       
        /// <summary>
        /// 自动生成打包代码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPackCode()
        {
            string strPackCode = "";
            var listDep = (from tbDmp in MyModels.B_PackGoodsList orderby tbDmp.PackCode select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_PackGoodsList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.PackCode.Substring(0, 5));
                intCode++;
                strPackCode = intCode.ToString();
                for (int i = 0; i < 5; i++)
                {
                    strPackCode = strPackCode.Length < 5 ? "0" + strPackCode : strPackCode;
                }
            }
            else
            {
                strPackCode = "00001";
            }
            return Json(strPackCode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自动生成打包条码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPackTiaoMa()
        {
            string strPackTiaoMa = "";
            var listDep = (from tbDmp in MyModels.B_PackGoodsList orderby tbDmp.PackTiaoMa select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_PackGoodsList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.PackTiaoMa.Substring(8, 5));
                intCode++;
                strPackTiaoMa = intCode.ToString();
                for (int i = 0; i < 5; i++)
                {
                    strPackTiaoMa = strPackTiaoMa.Length < 5 ? "0" + strPackTiaoMa : strPackTiaoMa;
                }
                strPackTiaoMa = "21000025" + strPackTiaoMa;
            }
            else
            {
                strPackTiaoMa = "2100002500001";
            }
            return Json(strPackTiaoMa, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectEstimateUnitName()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.S_EstimateUnitList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.EstimateUnitID,
                                                     text = tbEstimateUnit.EstimateUnitName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询商品组成
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectGoodsDetail(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                             join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                             join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                             orderby tbGoodsDetail.GoodsDetailID
                             select new Vo.GoodsDetail                            
                             {
                                 GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 GoodsID = tbGoodsDetail.GoodsID,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                 GoodsName = tbGoods.GoodsName,
                                 SpecificationsModel = tbGoods.SpecificationsModel,
                                 EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                 EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 MemberPrice = tbGoodsDetail.MemberPrice,
                             });
          
            int intTotalRow = listGoods.Count();//总行数
            List<GoodsDetail> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<GoodsDetail> bsgrid = new Bsgrid<GoodsDetail>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取商品组成
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="JieShouID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPinXinXi(BsgridPage bsgridPage, Array JieShouID)
        {
            List<GoodsDetail> list = new List<GoodsDetail>();
            string Q = ((string[])JieShouID)[0];
            string[] intsQ = Q.Split(',');

            for (int i = 0; i < intsQ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsQ[i]);
                var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                 join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                 join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                                 orderby tbGoodsDetail.GoodsDetailID
                                 where tbGoodsDetail.GoodsDetailID == goodsIDs
                                 select new Vo.GoodsDetail                                                                 
                                 {
                                     GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                     GoodsID = tbGoodsDetail.GoodsID,
                                     GoodsCode = tbGoods.GoodsCode,
                                     GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                     GoodsName = tbGoods.GoodsName,
                                     SpecificationsModel = tbGoods.SpecificationsModel,
                                     EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                     EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                     MemberPrice = tbGoodsDetail.MemberPrice,
                                 }).ToList();
                list.AddRange(listGoods);
                i++;
            }

            int intTotalRow = list.Count();//总行数
            List<GoodsDetail> listNotices = list.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<GoodsDetail> bsgrid = new Bsgrid<GoodsDetail>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

            //int totalRow = list.Count();
            //List<Vo.GoodsDetail> listNotices = list.OrderByDescending(p => p.GoodsDetailID).Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            //Vo.Bsgrid<Vo.GoodsDetail> bsgrid = new Vo.Bsgrid<Vo.GoodsDetail>();
            //bsgrid.success = true;
            //bsgrid.totalRows = totalRow;//总的行数
            //bsgrid.curPage = bsgridPage.curPage;//请求当前页
            //bsgrid.data = listNotices;//查出的数据
            //return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增保存
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengDaBao(B_PackGoodsList pwApplyTarget)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.B_PackGoodsList
                                             where tbGoodsMoneyRule.PackCode == pwApplyTarget.PackCode
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    B_PackGoodsList JJ = new B_PackGoodsList();
                    JJ.PackCode = Request.Form["PackCode"];
                    JJ.PackTiaoMa = Request.Form["PackTiaoMa"];
                    JJ.EstimateUnitID = Convert.ToInt32(Request.Form["EstimateUnitID"]);
                    JJ.PackName = Request.Form["PackName"];
                    JJ.PackSpellCod = Request.Form["PackSpellCod"];
                    JJ.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                    JJ.PackDengJiRen = Request.Form["PackDengJiRen"];
                    JJ.PackDengJiShi = Convert.ToDateTime(Request.Form["PackDengJiShi"]);
                    JJ.PackGengXinRen = Request.Form["PackGengXinRen"];
                    JJ.PackGengXinShi = Convert.ToDateTime(Request.Form["PackGengXinShi"]);
                    if (JJ.PackName != null && JJ.EstimateUnitID != null && JJ.GoodsDetailID != null && JJ.PackDengJiShi != null && JJ.PackGengXinShi != null)
                    {
                        MyModels.B_PackGoodsList.Add(JJ);
                        MyModels.SaveChanges();
                        return Json("success", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json("fail", JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    strMsg = "exsit";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);

           
        }

        /// <summary>
        /// 查询修改
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunXiuGaiDaBao(BsgridPage bsgridPage)
        {
            var listGoods = (from tbPackGoods in MyModels.B_PackGoodsList
                             //join tbGoodsDetail in MyModels.B_GoodsDetailList on tbPackGoods.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                             join tbEstimateUnit in MyModels.S_EstimateUnitList on tbPackGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                             orderby tbPackGoods.PackGoodsID                            
                             select new Vo.GoodsDetail
                             {
                                 //PackGoodsID=tbPackGoods.PackGoodsID,
                                 PackCode = tbPackGoods.PackCode,
                                 PackTiaoMa = tbPackGoods.PackTiaoMa,
                                 EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                 EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                 PackName = tbPackGoods.PackName,
                                 PackSpellCod = tbPackGoods.PackSpellCod,
                                 //GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 PackDengJiRen = tbPackGoods.PackDengJiRen,
                                 ReleaseTimeStr = tbPackGoods.PackDengJiShi.ToString(),
                                 PackGengXinRen = tbPackGoods.PackGengXinRen,
                                 ReleaseTimeStrr = tbPackGoods.PackGengXinShi.ToString(),                                
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<GoodsDetail> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<GoodsDetail> bsgrid = new Bsgrid<GoodsDetail>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 回填计量单位
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectEstimateUnitName1()
        {
            var listEstimateUnit = (from tbEstimateUnit in MyModels.S_EstimateUnitList
                                    select new SelectVo
                                    {
                                        id = tbEstimateUnit.EstimateUnitID,
                                        text = tbEstimateUnit.EstimateUnitName.Trim(),
                                    }).ToList();
            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 回填打包信息
        /// </summary>
        /// <param name="DBID"></param>
        /// <returns></returns>
        public ActionResult HuiTianDaBaoXinXi(int DBID)
        {
            if (DBID > 0)
            {
                var listGoods = (from tbPackGoods in MyModels.B_PackGoodsList
                                 //join tbGoodsDetail in MyModels.B_GoodsDetailList on tbPackGoods.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                 join tbEstimateUnit in MyModels.S_EstimateUnitList on tbPackGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID                                 
                                 where tbPackGoods.PackGoodsID == DBID
                                 select new Vo.GoodsDetail                                                               
                                 {
                                     //PackGoodsID = tbPackGoods.PackGoodsID,
                                     PackCode = tbPackGoods.PackCode,
                                     PackTiaoMa = tbPackGoods.PackTiaoMa,
                                     EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                     EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                     PackName = tbPackGoods.PackName,
                                     PackSpellCod = tbPackGoods.PackSpellCod,
                                     //GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                     PackDengJiRen = tbPackGoods.PackDengJiRen,
                                     ReleaseTimeStr = tbPackGoods.PackDengJiShi.ToString(),
                                     PackGengXinRen = tbPackGoods.PackGengXinRen,
                                     ReleaseTimeStrr = tbPackGoods.PackGengXinShi.ToString(),
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <returns></returns>
        public ActionResult XiuGaiBaoCunDaBao()
        {
            string styMy = "fail";
            try
            {
                B_PackGoodsList KK = new B_PackGoodsList();
                KK.PackGoodsID = Convert.ToInt32(Request.Form["PackGoodsID"]);
                KK.PackCode = Request.Form["PackCode"];
                KK.PackTiaoMa = Request.Form["PackTiaoMa"];                
                KK.EstimateUnitID = Convert.ToInt32(Request.Form["EstimateUnitID"]);
                KK.PackName = Request.Form["PackName"];
                KK.PackSpellCod = Request.Form["PackSpellCod"];
                //KK.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                KK.PackDengJiRen = Request.Form["PackDengJiRen"];
                KK.PackDengJiShi = Convert.ToDateTime(Request.Form["PackDengJiShi"]);
                KK.PackGengXinRen = Request.Form["PackGengXinRen"];
                KK.PackGengXinShi = Convert.ToDateTime(Request.Form["PackGengXinShi"]);
                             
                MyModels.Entry(KK).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                styMy = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(styMy, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
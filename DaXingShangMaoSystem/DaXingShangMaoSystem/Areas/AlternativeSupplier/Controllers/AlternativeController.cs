using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.AlternativeSupplier.Controllers
{
    public class AlternativeController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: AlternativeSupplier/Alternative
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 备选供货商暂未批准原因定义
        /// </summary>
        /// <returns></returns>
        public ActionResult alternative()
        {
            return View();
        }

        /// <summary>
        /// 备选供货商转为正式供货商
        /// </summary>
        /// <returns></returns>

        public ActionResult alternativeSupplier()
        {
            return View();
        }
        /// <summary>
        /// 生产厂家管理
        /// </summary>
        /// <returns></returns>
        public ActionResult manufacturer()
        {
            return View();
        }
        /// <summary>
        /// 定义统计属性
        /// </summary>
        /// <returns></returns>
        public ActionResult DingYiShuXing()
        {

            return View();
        }
        public ActionResult DingYiShuXing1()
        {
            //获取整张表的数据，转化成
            string strJsonDrugType = MyModels.S_StatisticAttribute.ToList().GetJSONTreeData("StatisticAttributeID", "StatisticAttributeMC", "DrugTypeID");//调用下面的方法，转换SJons 格式字符串;
            //string strDrugType = GetDrugTypeTreeData(lstJsonDrugType, 0);
            return Content(strJsonDrugType);//返回
        }
        public string GetGoodsClassifyTreeData(List<Models.S_StatisticAttribute> list, int fid)
        {
            StringBuilder sbTree = new StringBuilder();
            List<Models.S_StatisticAttribute> listNode = list.Where(m => m.DrugTypeID == fid).ToList();
            if (listNode.Count > 0)
            {
                //有节点存在
                sbTree.Append("[");
                for (int i = 0; i < listNode.Count; i++)
                {
                    //获取子节点
                    int proid = listNode[i].StatisticAttributeID;
                    //判断当前节点是否有子节点
                    string sbChild = GetGoodsClassifyTreeData(list, proid);

                    //获取json格式
                    if (sbChild.ToString() != "")
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].StatisticAttributeID + "\",\"text\":\"" + listNode[i].StatisticAttributeMC + "\",\"children\":");
                        sbTree.Append(sbChild);
                        sbTree.Append("},");
                    }
                    else
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].StatisticAttributeID + "\",\"text\":\"" + listNode[i].StatisticAttributeMC + "\"},");
                    }
                }
                //没有子节点            
                sbTree.Remove(sbTree.Length - 1, 1);
                sbTree.Append("]");
            }
            return sbTree.ToString();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="modelClassify"></param>
        /// <returns></returns>
        public ActionResult InsertClassify(Models.S_StatisticAttribute modelClassify)
        {
            MyModels.Entry(modelClassify).State = System.Data.Entity.EntityState.Added;
            MyModels.SaveChanges();
            int goodsclassifyid = modelClassify.StatisticAttributeID;

            return Json(goodsclassifyid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="intTargetId"></param>
        /// <param name="intSourceId"></param>
        /// <returns></returns>
        public ActionResult UpdateFather(int intTargetId, int intSourceId)
        {
            var modelDrugType = MyModels.S_StatisticAttribute.Find(intSourceId);
            modelDrugType.DrugTypeID = intTargetId;
            MyModels.SaveChanges();
            return new EmptyResult();
        }

        /// <summary>
        /// 修改保存
        /// </summary>
        /// <param name="modelClassify"></param>
        /// <returns></returns>
        public ActionResult UpdateDrug(Models.S_StatisticAttribute modelClassify)
        {
            MyModels.Entry(modelClassify).State = System.Data.Entity.EntityState.Modified;
            MyModels.SaveChanges();
            return Content("OK");
        }



        /// <summary>
        /// 往来集团定义
        /// </summary>
        /// <returns></returns>
        public ActionResult WangLaiJiTuanDingYi()
        {
            return View();
        }
        /// <summary>
        /// 往来单位属性定义
        /// </summary>
        /// <returns></returns>
        public ActionResult WangLaiDanWeiShuXingDingYi()
        {
            return View();
        }
        /// <summary>
        /// 往来单位证书模板定义
        /// </summary>
        /// <returns></returns>
        public ActionResult WangLaiDanWeiDanWeiMuBanDingYi()
        {
            return View();
        }
        /// <summary>
        /// 查询备选供货商综合评审项目定义
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectNoticeTypeAll(BsgridPage bsgridPage)
        {
            var linqdata = from tbAlternativeProject in MyModels.S_AlternativeProjectList
                           orderby tbAlternativeProject.AlternativeProjectID
                           select tbAlternativeProject;

            int intTotalRow = linqdata.Count();
            List<S_AlternativeProjectList> listnAlternativeProjectList = linqdata
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize)
                .ToList();
            //实例化  Bsgrid的返回实体类
            Bsgrid<S_AlternativeProjectList> bsgrid = new Bsgrid<S_AlternativeProjectList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnAlternativeProjectList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询备选供货商综合评审项目定义类型 By Id
        /// </summary>
        /// <param name="noticeTypeId"></param>
        /// <returns></returns>
        public ActionResult SelectNoticeTypeById(int noticeTypeId)
        {
            try
            {
                S_AlternativeProjectList noticeType = (from tbNoticeType in MyModels.S_AlternativeProjectList
                                                       where tbNoticeType.AlternativeProjectID == noticeTypeId
                                                       select tbNoticeType).Single();
                return Json(noticeType, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 新增备选供货商综合评审项目定义
        /// </summary>
        /// <param name="AlternativeProjectMC"></param>
        /// <returns></returns>
        public ActionResult InsertNoticeType(S_AlternativeProjectList sysNoticeType)
        {
            string strMsg = "falied";
            if (!string.IsNullOrEmpty(sysNoticeType.AlternativeProjectMC))
            {
                int oldCount = (from tbNoticeType in MyModels.S_AlternativeProjectList
                                where tbNoticeType.AlternativeProjectMC == sysNoticeType.AlternativeProjectMC.Trim()
                                select tbNoticeType).Count();
                if (oldCount == 0)
                {
                    try
                    {
                        MyModels.S_AlternativeProjectList.Add(sysNoticeType);
                        if (MyModels.SaveChanges() > 0)
                        {
                            strMsg = "success";
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strMsg = "exist";//已经存在
                }
            }
            else
            {
                strMsg = "nofull";//数据不完整
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改备选供货商综合评审项目定义
        /// </summary>
        /// <param name="AlternativeProjectID"></param>
        /// <param name="AlternativeProjectMC"></param>
        /// <returns></returns>
        public ActionResult UpdateNoticeType(S_AlternativeProjectList sysNoticeType)
        {
            string strMsg = "falied";
            if (!string.IsNullOrEmpty(sysNoticeType.AlternativeProjectMC))
            {
                int oldCount = (from tbNoticeType in MyModels.S_AlternativeProjectList
                                where tbNoticeType.AlternativeProjectID != sysNoticeType.AlternativeProjectID
                                && tbNoticeType.AlternativeProjectMC == sysNoticeType.AlternativeProjectMC.Trim()
                                select tbNoticeType).Count();
                if (oldCount == 0)
                {
                    try
                    {
                        MyModels.Entry(sysNoticeType).State = System.Data.Entity.EntityState.Modified;
                        if (MyModels.SaveChanges() > 0)
                        {
                            strMsg = "success";
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strMsg = "exist";//已经存在
                }
            }

            else
            {
                strMsg = "nofull";//数据不完整
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteNoticeType(int noticeTypeId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                S_AlternativeProjectList dbNoticeType = (from tbNoticeTypeDetails in MyModels.S_AlternativeProjectList
                                                         where tbNoticeTypeDetails.AlternativeProjectID == noticeTypeId
                                                         select tbNoticeTypeDetails).Single();
                MyModels.S_AlternativeProjectList.Remove(dbNoticeType);//删除
                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception)
            {

            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 企业性质绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult selectSupplierRank()
        {
            List<SelectVo> lisSupplierRank = (from tbSupplierRank in MyModels.S_SupplierRankList
                                              select new SelectVo
                                              {
                                                  id = tbSupplierRank.SupplierRankID,
                                                  text = tbSupplierRank.SupplierRankName
                                              }).ToList();
            lisSupplierRank = Common.Tools.SetSelectJson(lisSupplierRank);//设置selectjson
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 绑定下拉框
        /// </summary>
        /// <returns></returns>
        public ActionResult selectEnterpriseNature()
        {
            List<SelectVo> lisEnterpriseNature = (from tbEnterpriseNature in MyModels.S_EnterpriseNatureList
                                                  select new SelectVo
                                                  {
                                                      id = tbEnterpriseNature.EnterpriseNatureID,
                                                      text = tbEnterpriseNature.EnterpriseNatureName
                                                  }).ToList();
            lisEnterpriseNature = Common.Tools.SetSelectJson(lisEnterpriseNature);//设置selectjson
            return Json(lisEnterpriseNature, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectEnterpriseType()
        {
            List<SelectVo> lisEnterpriseType = (from tbEnterpriseType in MyModels.S_EnterpriseTypeList
                                                select new SelectVo
                                                {
                                                    id = tbEnterpriseType.EnterpriseTypeID,
                                                    text = tbEnterpriseType.EnterpriseTypeName
                                                }).ToList();
            lisEnterpriseType = Common.Tools.SetSelectJson(lisEnterpriseType);//设置selectjson
            return Json(lisEnterpriseType, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectData()
        {
            return Json("");
        }




        /// <summary>
        /// 查询备选供货商综合评审项目定义
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectAlternativeList(BsgridPage bsgridPage)
        {
            var linqdata = from tbAlternativeProject in MyModels.S_AlternativeList
                           orderby tbAlternativeProject.AlternativeID
                           select tbAlternativeProject;

            int intTotalRow = linqdata.Count();
            List<S_AlternativeList> listnAlternativeProjectList = linqdata
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize)
                .ToList();
            //实例化  Bsgrid的返回实体类
            Bsgrid<S_AlternativeList> bsgrid = new Bsgrid<S_AlternativeList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnAlternativeProjectList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询备选供货商综合评审项目定义类型 By Id
        /// </summary>
        /// <param name="noticeTypeId"></param>
        /// <returns></returns>
        public ActionResult SelectAlternativeListId(int noticeTypeId)
        {
            try
            {
                S_AlternativeList noticeType = (from tbNoticeType in MyModels.S_AlternativeList
                                                where tbNoticeType.AlternativeID == noticeTypeId
                                                select tbNoticeType).Single();
                return Json(noticeType, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 新增备选供货商综合评审项目定义
        /// </summary>
        /// <param name="AlternativeProjectMC"></param>
        /// <returns></returns>
        public ActionResult InsertAlternativeList(S_AlternativeList sysNoticeType)
        {
            string strMsg = "falied";
            if (!string.IsNullOrEmpty(sysNoticeType.AlternativeName))
            {
                int oldCount = (from tbNoticeType in MyModels.S_AlternativeList
                                where tbNoticeType.AlternativeName == sysNoticeType.AlternativeName.Trim()
                                select tbNoticeType).Count();
                if (oldCount == 0)
                {
                    try
                    {
                        MyModels.S_AlternativeList.Add(sysNoticeType);
                        if (MyModels.SaveChanges() > 0)
                        {
                            strMsg = "success";
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strMsg = "exist";//已经存在
                }
            }
            else
            {
                strMsg = "nofull";//数据不完整
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改备选供货商综合评审项目定义
        /// </summary>
        /// <param name="AlternativeProjectID"></param>
        /// <param name="AlternativeProjectMC"></param>
        /// <returns></returns>
        public ActionResult UpdateAlternativeList(S_AlternativeList sysNoticeType)
        {
            string strMsg = "falied";
            if (!string.IsNullOrEmpty(sysNoticeType.AlternativeName))
            {
                int oldCount = (from tbNoticeType in MyModels.S_AlternativeList
                                where tbNoticeType.AlternativeID != sysNoticeType.AlternativeID
                                && tbNoticeType.AlternativeName == sysNoticeType.AlternativeName.Trim()
                                select tbNoticeType).Count();
                if (oldCount == 0)
                {
                    try
                    {
                        MyModels.Entry(sysNoticeType).State = System.Data.Entity.EntityState.Modified;
                        if (MyModels.SaveChanges() > 0)
                        {
                            strMsg = "success";
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strMsg = "exist";//已经存在
                }
            }

            else
            {
                strMsg = "nofull";//数据不完整
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteAlternativeList(int noticeTypeId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                S_AlternativeList dbNoticeType = (from tbNoticeTypeDetails in MyModels.S_AlternativeList
                                                  where tbNoticeTypeDetails.AlternativeID == noticeTypeId
                                                  select tbNoticeTypeDetails).Single();
                MyModels.S_AlternativeList.Remove(dbNoticeType);//删除
                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception)
            {


            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }




        ////新增生产厂家信息
        public ActionResult SelectGoods(B_ManufacturerList ManufacturerList)
        {
            ManufacturerList.ManufacturerCode = Request.Form["ManufacturerCode"];
            ManufacturerList.ManufacturerPC = Request.Form["ManufacturerPC"];
            ManufacturerList.ManufacturerName = Request.Form["ManufacturerName"];
            ManufacturerList.ManufacturerPYCode = Request.Form["ManufacturerPYCode"];
            ManufacturerList.ManufacturerJCName = Request.Form["ManufacturerJCName"];
            ManufacturerList.ManufacturerProduct = Request.Form["ManufacturerProduct"];
            ManufacturerList.ManufacturerAddress = Request.Form["ManufacturerAddress"];
            ManufacturerList.ManufacturerBarCode = Request.Form["ManufacturerBarCode"];
            if (ManufacturerList.ManufacturerCode != "" && ManufacturerList.ManufacturerPC != ""
                && ManufacturerList.ManufacturerName != "" && ManufacturerList.ManufacturerPYCode != ""
                && ManufacturerList.ManufacturerJCName != "" && ManufacturerList.ManufacturerProduct != ""
                && ManufacturerList.ManufacturerAddress != "" && ManufacturerList.ManufacturerBarCode != "")
            {
                MyModels.B_ManufacturerList.Add(ManufacturerList);
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Update(B_ManufacturerList ManufacturerList)
        {
            string MSG = "fail";
            try
            {
                ManufacturerList.ManufacturerID = Convert.ToInt32(Request.Form["ManufacturerID"]);
                ManufacturerList.ManufacturerCode = Request.Form["ManufacturerCode"];
                ManufacturerList.ManufacturerPC = Request.Form["ManufacturerPC"];
                ManufacturerList.ManufacturerName = Request.Form["ManufacturerName"];
                ManufacturerList.ManufacturerPYCode = Request.Form["ManufacturerPYCode"];
                ManufacturerList.ManufacturerJCName = Request.Form["ManufacturerJCName"];
                ManufacturerList.ManufacturerProduct = Request.Form["ManufacturerProduct"];
                ManufacturerList.ManufacturerAddress = Request.Form["ManufacturerAddress"];
                ManufacturerList.ManufacturerBarCode = Request.Form["ManufacturerBarCode"];



                MyModels.Entry(ManufacturerList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                MSG = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }

        public ActionResult selectManufacturerList(BsgridPage bsgridPage, string code)
        {
            var linqItme = from tbManufacturerList in MyModels.B_ManufacturerList

                           orderby tbManufacturerList.ManufacturerID descending
                           select new ManufacturerListVo
                           {
                               ManufacturerCode = tbManufacturerList.ManufacturerCode,
                               ManufacturerID = tbManufacturerList.ManufacturerID,
                               ManufacturerName = tbManufacturerList.ManufacturerName
                           };
            if (!string.IsNullOrEmpty(code))
            {
                linqItme = linqItme.Where(s => s.ManufacturerCode.Contains(code.Trim()));
            }
            int intTotalRow = linqItme.Count();
            List<ManufacturerListVo> listManufacturerList = linqItme
                 .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<ManufacturerListVo> bsgrid = new Bsgrid<ManufacturerListVo>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listManufacturerList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectManufacturer(BsgridPage bsgridPage)
        {
            var linqItme = (from tbManufacturer in MyModels.B_ManufacturerList
                            orderby tbManufacturer.ManufacturerID
                            select new ManufacturerListVo
                            {
                                ManufacturerID = tbManufacturer.ManufacturerID,
                                ManufacturerCode = tbManufacturer.ManufacturerCode,
                                ManufacturerPC = tbManufacturer.ManufacturerPC,
                                ManufacturerName = tbManufacturer.ManufacturerName,
                                ManufacturerPYCode = tbManufacturer.ManufacturerPYCode,
                                ManufacturerJCName = tbManufacturer.ManufacturerJCName,
                                ManufacturerProduct = tbManufacturer.ManufacturerProduct,
                                ManufacturerAddress = tbManufacturer.ManufacturerAddress,
                                ManufacturerBarCode = tbManufacturer.ManufacturerBarCode,

                            });
            int intTotalRow = linqItme.Count();
            List<ManufacturerListVo> listManufacturerList = linqItme
                 .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<ManufacturerListVo> bsgrid = new Bsgrid<ManufacturerListVo>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listManufacturerList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectManufacturerID(int id)
        {
            if (id > 0)
            {
                var linqItme = (from tbManufacturer in MyModels.B_ManufacturerList
                                where tbManufacturer.ManufacturerID == id
                                select new ManufacturerListVo
                                {
                                    ManufacturerID = tbManufacturer.ManufacturerID,
                                    ManufacturerCode = tbManufacturer.ManufacturerCode,
                                    ManufacturerPC = tbManufacturer.ManufacturerPC,
                                    ManufacturerName = tbManufacturer.ManufacturerName,
                                    ManufacturerPYCode = tbManufacturer.ManufacturerPYCode,
                                    ManufacturerJCName = tbManufacturer.ManufacturerJCName,
                                    ManufacturerProduct = tbManufacturer.ManufacturerProduct,
                                    ManufacturerAddress = tbManufacturer.ManufacturerAddress,
                                    ManufacturerBarCode = tbManufacturer.ManufacturerBarCode,

                                }).Single();
                return Json(linqItme, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }

        }
        /// <summary>
        ///  往来集团定义
        /// </summary>
        /// <returns></returns>
        public ActionResult selectSupplierGroup()
        {
            try
            {
                var linqdata = (from tbSupplierGroupName in MyModels.S_SupplierGroupList
                                orderby tbSupplierGroupName.SupplierGroupID descending
                                select new
                                {
                                    Id = tbSupplierGroupName.SupplierGroupID,
                                    Name = tbSupplierGroupName.SupplierGroupName.Trim(),
                                    Code = tbSupplierGroupName.SupplierGroupPYCode.Trim()
                                }).ToList();

                return Json(linqdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("failed", JsonRequestBehavior.AllowGet);
            }

        }
        /// <summary>
        /// 往来集团定义新增
        /// </summary>
        /// <param name="SupplierGroupName"></param>
        /// <param name="SupplierGroupPYCode"></param>
        /// <returns></returns>
        public ActionResult getDepartment(string SupplierGroupName, string SupplierGroupPYCode)
        {
            string mag = "falied";
            try
            {
                Models.S_SupplierGroupList myModel = new S_SupplierGroupList();
                myModel.SupplierGroupName = SupplierGroupName.Trim();
                myModel.SupplierGroupPYCode = SupplierGroupPYCode;
                MyModels.S_SupplierGroupList.Add(myModel);
                MyModels.SaveChanges();
                mag = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(mag, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 往来集团定义修改
        /// </summary>
        /// <param name="SupplierGroupID"></param>
        /// <param name="SupplierGroupName"></param>
        /// <param name="SupplierGroupPYCode"></param>
        /// <returns></returns>
        //public ActionResult Update(int SupplierGroupID, string SupplierGroupName, string SupplierGroupPYCode)
        //{
        //    string strMsg = "falied";
        //    try
        //    {
        //        //实例化表对象
        //        Models.S_SupplierGroupList myAcademe = new Models.S_SupplierGroupList();
        //        //给对象属性赋值
        //        myAcademe.SupplierGroupID = SupplierGroupID;
        //        myAcademe.SupplierGroupName = SupplierGroupName.Trim();
        //        myAcademe.SupplierGroupPYCode = SupplierGroupPYCode;
        //        //获取和实质对象实体的状态=EntityState描述实体状态的枚举类
        //        MyModels.Entry(myAcademe).State = EntityState.Modified;
        //        MyModels.SaveChanges();
        //        strMsg = "success";

        //    }
        //    catch
        //    {
        //        strMsg = "failed";
        //    }
        //    return Json(strMsg, JsonRequestBehavior.AllowGet);

        //}
        /// <summary>
        /// 往来集团定义删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public ActionResult DeleteAcademe(int ID)
        {
            try
            {
                string strMsg = "failed";//返回的字符串
                var listAcademe = MyModels.S_SupplierGroupList.Where(m => m.SupplierGroupID == ID).ToList();//学院表
                if (listAcademe.Count != 0)
                {
                    //删除表
                    MyModels.S_SupplierGroupList.Remove(listAcademe[0]);
                    var dd = MyModels.SaveChanges();
                    if (dd == 1)
                    {
                        strMsg = "success";
                    }
                    else
                    {
                        strMsg = "删除失败!";
                    }

                }
                return Json(strMsg, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 往来单位属性定义
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult Select(BsgridPage bsgridPage)
        {
            var linqdata = from tbAlternativeProject in MyModels.S_SupplierRankList
                           orderby tbAlternativeProject.SupplierRankID
                           select tbAlternativeProject;

            int intTotalRow = linqdata.Count();

            List<S_SupplierRankList> listnAlternativeProjectList = linqdata
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize)
                .ToList();
            //实例化  Bsgrid的返回实体类
            Bsgrid<S_SupplierRankList> bsgrid = new Bsgrid<S_SupplierRankList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnAlternativeProjectList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 往来单位属性定义
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult Select1(BsgridPage bsgridPage)
        {
            var linqdata = from tbAlternativeProject in MyModels.S_UnitCharacterList
                           orderby tbAlternativeProject.UnitCharacterID
                           select tbAlternativeProject;

            int intTotalRow = linqdata.Count();

            List<S_UnitCharacterList> listnAlternativeProjectList = linqdata
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize)
                .ToList();
            //实例化  Bsgrid的返回实体类
            Bsgrid<S_UnitCharacterList> bsgrid = new Bsgrid<S_UnitCharacterList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnAlternativeProjectList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 往来单位属性定义
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult Select3(BsgridPage bsgridPage)
        {
            var linqdata = from tbAlternativeProject in MyModels.S_AgreementTypeList
                           orderby tbAlternativeProject.AgreementTypeID
                           select tbAlternativeProject;

            int intTotalRow = linqdata.Count();

            List<S_AgreementTypeList> listnAlternativeProjectList = linqdata
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize)
                .ToList();
            //实例化  Bsgrid的返回实体类
            Bsgrid<S_AgreementTypeList> bsgrid = new Bsgrid<S_AgreementTypeList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnAlternativeProjectList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 往来单位证书模板定义查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectContactCertificate(BsgridPage bsgridPage)
        {
            var linqdata = from tbContactCertificate in MyModels.S_ContactCertificate
                           orderby tbContactCertificate.ContactCertificateID
                           select tbContactCertificate;
            int intTotalRow = linqdata.Count();

            List<S_ContactCertificate> listnAlternativeProjectList = linqdata
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize)
                .ToList();
            //实例化  Bsgrid的返回实体类
            Bsgrid<S_ContactCertificate> bsgrid = new Bsgrid<S_ContactCertificate>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnAlternativeProjectList;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectContactCertificateID(int noticeTypeId)
        {
            try
            {
                S_ContactCertificate noticeType = (from tbNoticeType in MyModels.S_ContactCertificate
                                                   where tbNoticeType.ContactCertificateID == noticeTypeId
                                                   select tbNoticeType).Single();
                return Json(noticeType, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult InsertContactCertificatet(S_ContactCertificate sysNoticeType)
        {
            string strMsg = "falied";
            if (!string.IsNullOrEmpty(sysNoticeType.ContactCertificateName))
            {
                int oldCount = (from tbNoticeType in MyModels.S_ContactCertificate
                                where tbNoticeType.ContactCertificateName == sysNoticeType.ContactCertificateName.Trim()
                                select tbNoticeType).Count();
                if (oldCount == 0)
                {
                    try
                    {
                        MyModels.S_ContactCertificate.Add(sysNoticeType);
                        if (MyModels.SaveChanges() > 0)
                        {
                            strMsg = "success";
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strMsg = "exist";//已经存在
                }
            }
            else
            {
                strMsg = "nofull";//数据不完整
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateContactCertificate(S_ContactCertificate sysNoticeType)
        {
            string strMsg = "falied";
            if (!string.IsNullOrEmpty(sysNoticeType.ContactCertificateName))
            {
                int oldCount = (from tbNoticeType in MyModels.S_ContactCertificate
                                where tbNoticeType.ContactCertificateID != sysNoticeType.ContactCertificateID
                                && tbNoticeType.ContactCertificateName == sysNoticeType.ContactCertificateName.Trim()
                                select tbNoticeType).Count();
                if (oldCount == 0)
                {
                    try
                    {
                        MyModels.Entry(sysNoticeType).State = System.Data.Entity.EntityState.Modified;
                        if (MyModels.SaveChanges() > 0)
                        {
                            strMsg = "success";
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strMsg = "exist";//已经存在
                }
            }

            else
            {
                strMsg = "nofull";//数据不完整
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteContactCertificate(int noticeTypeId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                S_ContactCertificate dbNoticeType = (from tbNoticeTypeDetails in MyModels.S_ContactCertificate
                                                     where tbNoticeTypeDetails.ContactCertificateID == noticeTypeId
                                                     select tbNoticeTypeDetails).Single();
                MyModels.S_ContactCertificate.Remove(dbNoticeType);//删除
                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception)
            {


            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 往来单位备选单位转为正式单位
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="contractNumber"></param>
        /// <returns></returns>
        public ActionResult SelectHeTongChuLi(BsgridPage bsgridPage, int EnterpriseNatureId, int EnterpriseTypeId, string supplierDependency)
        {
            var listOrderFormPact = from tbOrderFormPact in MyModels.B_SupplierList
                                    join tbEnterpriseNatureID in MyModels.S_EnterpriseNatureList on tbOrderFormPact.EnterpriseNatureID equals tbEnterpriseNatureID.EnterpriseNatureID
                                    join tbEnterpriseTypeID in MyModels.S_EnterpriseTypeList on tbOrderFormPact.EnterpriseTypeID equals tbEnterpriseTypeID.EnterpriseTypeID
                                    orderby tbOrderFormPact.SupplierID
                                    select new SupplierListVo
                                    {

                                        SupplierID = tbOrderFormPact.SupplierID,
                                        SupplierName = tbOrderFormPact.SupplierName,
                                        SupplierTelephone = tbOrderFormPact.SupplierTelephone,
                                        EnterpriseNatureName = tbEnterpriseNatureID.EnterpriseNatureName,
                                        EnterpriseNatureID = tbOrderFormPact.EnterpriseNatureID,
                                        EnterpriseTypeName = tbEnterpriseTypeID.EnterpriseTypeName,
                                        EnterpriseTypeID = tbOrderFormPact.EnterpriseTypeID,
                                        SupplierContacts = tbOrderFormPact.SupplierContacts,
                                        SupplierAddress = tbOrderFormPact.SupplierAddress,
                                        SupplierZipCode = tbOrderFormPact.SupplierZipCode,
                                        SupplierDependency = tbOrderFormPact.SupplierDependency
                                    }
                                   ;
            if (EnterpriseNatureId > 0)
            {
                listOrderFormPact = listOrderFormPact.Where(p => p.EnterpriseNatureID == EnterpriseNatureId);
            }
            if (EnterpriseTypeId > 0)
            {
                listOrderFormPact = listOrderFormPact.Where(p => p.EnterpriseTypeID == EnterpriseTypeId);
            }
            if (!string.IsNullOrEmpty(supplierDependency))
            {
                listOrderFormPact = listOrderFormPact.Where(s => s.SupplierDependency.Contains(supplierDependency.Trim()));
            }
            //查询数据总条数
            int intTotalRows = listOrderFormPact.Count();
            //查询分页数据
            List<SupplierListVo> students = listOrderFormPact
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize)
                .ToList();

            //封装Json
            Bsgrid<SupplierListVo> bsgrid = new Bsgrid<SupplierListVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRows;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = students;

            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public ActionResult DeleteHeTong(int goodsId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_ManufacturerList dbGoodsDetail = (from tbGoodsDetail in MyModels.B_ManufacturerList
                                                    where tbGoodsDetail.ManufacturerID == goodsId
                                                    select tbGoodsDetail).Single();

                MyModels.B_ManufacturerList.Remove(dbGoodsDetail);

                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception)
            {
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

    }
}
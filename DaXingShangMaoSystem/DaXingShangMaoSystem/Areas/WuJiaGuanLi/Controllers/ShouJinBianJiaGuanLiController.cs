using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.WuJiaGuanLi.Controllers
{
    public class ShouJinBianJiaGuanLiController : Controller
    {
        // GET: WuJiaGuanLi/ShouJinBianJiaGuanLi
        //售进变价管理
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        #region 变价原因定义 
        public ActionResult BianJiaYuanYin()
        {
            return View();
        }
        /// <summary>
        /// 查询变价原因
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectChangeWhy(BsgridPage bsgridPage)
        {
            var linqItme = from tbChangeWhy in MyModels.S_ChangeWhyList
                           orderby tbChangeWhy.ChangeWhyID
                           select tbChangeWhy;
            //查询总行数
            int intTotalRow = linqItme.Count();

            //使用Skip...Take...必须使用orderby
            List<S_ChangeWhyList> listnChangeWhy = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            //实例化 Bsgrid的返回实体类
            Bsgrid<S_ChangeWhyList> bsgrid = new Bsgrid<S_ChangeWhyList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnChangeWhy;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增变价原因
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertChangeWhy(S_ChangeWhyList pwApplyTarget)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.S_ChangeWhyList
                                             where tbGoodsMoneyRule.ChangeWhy == pwApplyTarget.ChangeWhy
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    S_ChangeWhyList JJ = new S_ChangeWhyList();
                    JJ.ChangeWhy = Request.Form["ChangeWhy"];
                    if (JJ.ChangeWhy != null)
                    {
                        MyModels.S_ChangeWhyList.Add(JJ);
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
        /// 修改变价原因
        /// </summary>
        /// <param name="changeWhyId"></param>
        /// <returns></returns>
        public ActionResult UpdataChangeWhy(int changeWhyId)
        {
            if (changeWhyId > 0)
            {
                var ListChangeWhy = (from tbEmp in MyModels.S_ChangeWhyList
                                     where tbEmp.ChangeWhyID == changeWhyId
                                     select new
                                     {
                                         tbEmp.ChangeWhyID,
                                         tbEmp.ChangeWhy,
                                     }).Single();
                return Json(ListChangeWhy, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 保存变价原因
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdataChangeWhyBaoCun()
        {
            string styMy = "fail";
            try
            {
                S_ChangeWhyList JJ = new S_ChangeWhyList();
                JJ.ChangeWhyID = Convert.ToInt32(Request.Form["ChangeWhyID"]);
                JJ.ChangeWhy = Request.Form["ChangeWhy"];

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

        #region 售价变价单处理
        public ActionResult BianShouJiaShenQing()
        {
            return View();
        }
        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBianJiaBianHao()
        {
            string strOrderNumber = "";
            var listDep = (from tbDmp in MyModels.B_GoodsBianJiaList orderby tbDmp.BianJiaBianHao select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_GoodsBianJiaList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.BianJiaBianHao.Substring(0, 8));
                intCode++;
                strOrderNumber = intCode.ToString();
                for (int i = 0; i < 8; i++)
                {
                    strOrderNumber = strOrderNumber.Length < 8 ? "0" + strOrderNumber : strOrderNumber;
                }
            }
            else
            {
                strOrderNumber = "00000001";
            }
            return Json(strOrderNumber, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 变价类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectChangeTypeID()
        {        
            List<SelectVo> listChangeTypeID = (from tbChangeTypeName in MyModels.S_ChangeTypeList
                                                  where tbChangeTypeName.ChangeTypeID == 3
                                                  select new SelectVo
                                                  {
                                                      id = tbChangeTypeName.ChangeTypeID,
                                                      text = tbChangeTypeName.ChangeTypeName.Trim()
                                                  }).ToList();

            return Json(listChangeTypeID, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 变价原因
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectChangeWhyID()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必选择----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listOrderFormTypeID = (from tbOrderFormType in MyModels.S_ChangeWhyList
                                                  select new SelectVo
                                                  {
                                                      id = tbOrderFormType.ChangeWhyID,
                                                      text = tbOrderFormType.ChangeWhy.Trim(),
                                                  }).ToList();

            listOrderFormType.AddRange(listOrderFormTypeID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询商品明细
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunShangPingMingXi(BsgridPage bsgridPage)
        {
            var listGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                    join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID                                                                     
                                    orderby tbGoodsDetail.GoodsDetailID                                    
                                    select new Vo.Goods
                                    {
                                        GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                        GoodsID = tbGoods.GoodsID,
                                        GoodsCode = tbGoods.GoodsCode,
                                        GoodsName = tbGoods.GoodsName,
                                        RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                        //FactPrice = tbGoodsDetail.FactPrice,
                                        MemberPrice = tbGoodsDetail.MemberPrice,                                     
                                    });
            int intTotalRow = listGoodsDetail.Count();//总行数
            List<Goods> listNotices = listGoodsDetail.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取商品明细信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="JieShouID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPinMingXiXinXi(BsgridPage bsgridPage, Array JieShouID)
        {
            List<GoodsDetail> list = new List<GoodsDetail>();
            string Q = ((string[])JieShouID)[0];
            string[] intsQ = Q.Split(',');

            for (int i = 0; i < intsQ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsQ[i]);
                var listGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                       join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                       orderby tbGoodsDetail.GoodsDetailID
                                       where tbGoodsDetail.GoodsDetailID == goodsIDs
                                    select new Vo.GoodsDetail
                                    {
                                        GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                        GoodsID = tbGoods.GoodsID,
                                        GoodsCode = tbGoods.GoodsCode,
                                        GoodsName = tbGoods.GoodsName,
                                        RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                        //FactPrice = tbGoodsDetail.FactPrice,
                                        MemberPrice = tbGoodsDetail.MemberPrice,
                                    }).ToList();
                list.AddRange(listGoodsDetail);
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
        }

        /// <summary>
        /// 新增售价变价
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengShouJiaBianJia()
        {
            B_GoodsBianJiaList QQ = new B_GoodsBianJiaList();
            QQ.BianJiaBianHao = Request.Form["BianJiaBianHao"];
            QQ.ChangeTypeID = Convert.ToInt32(Request.Form["ChangeTypeID"]);
            QQ.ChangeWhyID = Convert.ToInt32(Request.Form["ChangeWhyID"]);
            QQ.ChangeData = Convert.ToDateTime(Request.Form["ChangeData"]);
            //QQ.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
            //QQ.NewAdvanceBid = Convert.ToDecimal(Request.Form["NewAdvanceBid"]);
            //QQ.NewNoAdvanceBid = Convert.ToDecimal(Request.Form["NewNoAdvanceBid"]);
            QQ.Registrant = Request.Form["Registrant"];
            QQ.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
            QQ.Auditor = Request.Form["Auditor"];
            QQ.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
            QQ.Executor = Request.Form["Executor"];
            QQ.ExecuteTime = Convert.ToDateTime(Request.Form["ExecuteTime"]);

            if (QQ.BianJiaBianHao != null && QQ.ChangeTypeID != null && QQ.ChangeWhyID != null && QQ.ChangeData != null && QQ.Registrant != null &&
                QQ.RegisterTime != null && QQ.Auditor != null && QQ.Checktime != null && QQ.Executor != null && QQ.ExecuteTime != null)
            {
                MyModels.B_GoodsBianJiaList.Add(QQ);
                MyModels.SaveChanges();

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// 查询修改售价变价
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunShouJiaBianJia(BsgridPage bsgridPage)
        {
            var listGoodsDetail = (from tbGoodsBianJia in MyModels.B_GoodsBianJiaList
                                   //join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoodsBianJia.GoodsDetailID equals tbGoodsDetail.GoodsDetailID                                 
                                   join tbChangeWhy in MyModels.S_ChangeWhyList on tbGoodsBianJia.ChangeWhyID equals tbChangeWhy.ChangeWhyID
                                   join tbChangeType in MyModels.S_ChangeTypeList on tbGoodsBianJia.ChangeTypeID equals tbChangeType.ChangeTypeID
                                   where tbGoodsBianJia.BeginChangeData != null
                                   orderby tbGoodsBianJia.GoodsBianJiaID
                                   select new Vo.Goods
                                   {
                                       GoodsBianJiaID = tbGoodsBianJia.GoodsBianJiaID,
                                       BianJiaBianHao = tbGoodsBianJia.BianJiaBianHao,
                                       ChangeTypeID = tbChangeType.ChangeTypeID,
                                       ChangeTypeName = tbChangeType.ChangeTypeName,                                     
                                       ChangeWhyID = tbChangeWhy.ChangeWhyID,
                                       ChangeWhy = tbChangeWhy.ChangeWhy,
                                       ReleaseTimeStrf = tbGoodsBianJia.ChangeData.ToString(),
                                       //GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                       //NewAdvanceBid = tbGoodsBianJia.NewAdvanceBid,
                                       //NewNoAdvanceBid = tbGoodsBianJia.NewNoAdvanceBid,
                                       Registrant = tbGoodsBianJia.Registrant,
                                       ReleaseTimeStr = tbGoodsBianJia.RegisterTime.ToString(),
                                       Auditor = tbGoodsBianJia.Auditor,
                                       ReleaseTimeStrr = tbGoodsBianJia.Checktime.ToString(),
                                       //Executor = tbGoodsBianJia.Executor,
                                       ReleaseTimeStrrr = tbGoodsBianJia.ExecuteTime.ToString(),

                                   });
            int intTotalRow = listGoodsDetail.Count();//总行数
            List<Goods> listNotices = listGoodsDetail.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 进价变价单处理
        public ActionResult BianJinJiaShenQing()
        {
            return View();
        }
        /// <summary>
        /// 变价原因
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectChangeWhyID1()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必选择----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listChangeWhyID = (from tbChangeWhy in MyModels.S_ChangeWhyList
                                                  select new SelectVo
                                                  {
                                                      id = tbChangeWhy.ChangeWhyID,
                                                      text = tbChangeWhy.ChangeWhy.Trim(),
                                                  }).ToList();

            listOrderFormType.AddRange(listChangeWhyID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);          
        }

        /// <summary>
        /// 选择部门
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSpouseBRanchID()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必选择----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listChangeWhyID = (from tbChangeWhy in MyModels.S_SpouseBRanchList
                                              select new SelectVo
                                              {
                                                  id = tbChangeWhy.SpouseBRanchID,
                                                  text = tbChangeWhy.SpouseBRanchName.Trim(),
                                              }).ToList();

            listOrderFormType.AddRange(listChangeWhyID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);         
        }

        /// <summary>
        /// 选择合同
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectOrderFormPactID(BsgridPage bsgridPage)
        {
            var listOrderFormPact = from tbOrderFormPact in MyModels.B_OrderFormPactList
                                    join tbSupplierL in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierL.SupplierID
                                    join tbMethodOfSettlingAccounts in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID  equals tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID
                                    orderby tbOrderFormPact.OrderFormPactID                                  
                                    select new Vo.OrderFormPact
                                    {
                                        OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                        HostContractNumber = tbOrderFormPact.HostContractNumber,
                                        SupplierID = tbSupplierL.SupplierID,
                                        SupplierName = tbSupplierL.SupplierName,
                                        MethodOfSettlingAccountsID = tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID,
                                        MethodOfSettlingAccounts = tbMethodOfSettlingAccounts.MethodOfSettlingAccounts,
                                    };
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPact> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPact> bsgrid = new Bsgrid<OrderFormPact>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取合同名称
        /// </summary>
        /// <param name="HeTongID"></param>
        /// <returns></returns>
        public ActionResult HuoQuHeTongMingCheng(int HeTongID)
        {
            if (HeTongID > 0)
            {
                var HeTong = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                              join tbSupplierL in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierL.SupplierID
                              join tbMethodOfSettlingAccounts in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID
                              where tbOrderFormPact.OrderFormPactID == HeTongID                             
                              select new Vo.OrderFormPact
                              {
                                  OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                  HostContractNumber = tbOrderFormPact.HostContractNumber,
                                  SupplierID = tbSupplierL.SupplierID,
                                  SupplierName = tbSupplierL.SupplierName,
                                  MethodOfSettlingAccountsID = tbMethodOfSettlingAccounts.MethodOfSettlingAccountsID,
                                  MethodOfSettlingAccounts = tbMethodOfSettlingAccounts.MethodOfSettlingAccounts,
                              }).Single();
                return Json(HeTong, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }


        #endregion

        #region 采购价变价单处理
        public ActionResult CaiGouJiaBianJia()
        {
            return View();
        }
        /// <summary>
        /// 变价原因
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectChangeWhyID2()
        {
            List<SelectVo> listOrderFormType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---必选择----"
            };
            listOrderFormType.Add(selectVo);

            List<SelectVo> listChangeWhyID = (from tbChangeWhy in MyModels.S_ChangeWhyList
                                              select new SelectVo
                                              {
                                                  id = tbChangeWhy.ChangeWhyID,
                                                  text = tbChangeWhy.ChangeWhy.Trim(),
                                              }).ToList();

            listOrderFormType.AddRange(listChangeWhyID);

            return Json(listOrderFormType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询商品明细
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ShangPinXinXiTianXie(BsgridPage bsgridPage)
        {
            var listGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                   join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                   join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                                   orderby tbGoodsDetail.GoodsDetailID
                                   select new Vo.Goods
                                   {
                                       GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                       GoodsID = tbGoods.GoodsID,
                                       GoodsCode = tbGoods.GoodsCode,
                                       GoodsName = tbGoods.GoodsName,
                                       AdvanceCess = tbGoods.AdvanceCess,
                                       NoAdvanceBid = tbPurchase.NoAdvanceBid,
                                       AdvanceBid = tbPurchase.AdvanceBid,
                                   });
            int intTotalRow = listGoodsDetail.Count();//总行数
            List<Goods> listNotices = listGoodsDetail.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取商品明细信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="JieShouID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPinXinXiTianXie(BsgridPage bsgridPage, Array JieShouID)
        {
            List<Goods> list = new List<Goods>();
            string Q = ((string[])JieShouID)[0];
            string[] intsQ = Q.Split(',');

            for (int i = 0; i < intsQ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsQ[i]);
                var listGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                       join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                       join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                                       orderby tbGoodsDetail.GoodsDetailID
                                       where tbGoodsDetail.GoodsDetailID == goodsIDs
                                       select new Vo.Goods
                                       {
                                           GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                           GoodsID = tbGoods.GoodsID,
                                           GoodsCode = tbGoods.GoodsCode,
                                           GoodsName = tbGoods.GoodsName,
                                           AdvanceCess = tbGoods.AdvanceCess,
                                           NoAdvanceBid = tbPurchase.NoAdvanceBid,
                                           AdvanceBid = tbPurchase.AdvanceBid,
                                       }).ToList();
                list.AddRange(listGoodsDetail);
                i++;
            }

            int intTotalRow = list.Count();//总行数
            List<Goods> listNotices = list.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增采购价变价
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengCaiGouJiaBianJia(B_GoodsBianJiaList pwGoodsBianJia)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.B_GoodsBianJiaList
                                             where tbGoodsMoneyRule.BianJiaBianHao == pwGoodsBianJia.BianJiaBianHao
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    B_GoodsBianJiaList QQ = new B_GoodsBianJiaList();
                    QQ.BianJiaBianHao = Request.Form["BianJiaBianHao"];
                    QQ.BeginChangeData = Convert.ToDateTime(Request.Form["BeginChangeData"]);
                    QQ.ChangeWhyID = Convert.ToInt32(Request.Form["ChangeWhyID"]);
                    //QQ.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                    //QQ.NewAdvanceBid = Convert.ToDecimal(Request.Form["NewAdvanceBid"]);
                    //QQ.NewNoAdvanceBid = Convert.ToDecimal(Request.Form["NewNoAdvanceBid"]);
                    QQ.Registrant = Request.Form["Registrant"];
                    QQ.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                    QQ.Auditor = Request.Form["Auditor"];
                    QQ.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                    QQ.Executor = Request.Form["Executor"];
                    QQ.ExecuteTime = Convert.ToDateTime(Request.Form["ExecuteTime"]);

                    if (QQ.BianJiaBianHao != null && QQ.BeginChangeData != null && QQ.ChangeWhyID != null && QQ.Registrant != null &&
                        QQ.RegisterTime != null && QQ.Auditor != null && QQ.Checktime != null && QQ.Executor != null && QQ.ExecuteTime != null)
                    {
                        MyModels.B_GoodsBianJiaList.Add(QQ);
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
        /// 查询采购价变价
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunCaiJiaBianJia(BsgridPage bsgridPage)
        {
            var listGoodsDetail = (from tbGoodsBianJia in MyModels.B_GoodsBianJiaList
                                   //join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoodsBianJia.GoodsDetailID equals tbGoodsDetail.GoodsDetailID                                 
                                   join tbChangeWhy in MyModels.S_ChangeWhyList on tbGoodsBianJia.ChangeWhyID equals tbChangeWhy.ChangeWhyID                                  
                                   where tbGoodsBianJia.BeginChangeData != null
                                   orderby tbGoodsBianJia.GoodsBianJiaID
                                   select new Vo.Goods
                                   {
                                       GoodsBianJiaID = tbGoodsBianJia.GoodsBianJiaID,
                                       BianJiaBianHao = tbGoodsBianJia.BianJiaBianHao,
                                       ReleaseTimeStrf = tbGoodsBianJia.BeginChangeData.ToString(),
                                       ChangeWhyID = tbChangeWhy.ChangeWhyID,
                                       ChangeWhy = tbChangeWhy.ChangeWhy,
                                       //GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                       //NewAdvanceBid = tbGoodsBianJia.NewAdvanceBid,
                                       //NewNoAdvanceBid = tbGoodsBianJia.NewNoAdvanceBid,
                                       Registrant = tbGoodsBianJia.Registrant,
                                       ReleaseTimeStr = tbGoodsBianJia.RegisterTime.ToString(),
                                       Auditor = tbGoodsBianJia.Auditor,
                                       ReleaseTimeStrr = tbGoodsBianJia.Checktime.ToString(),
                                       //Executor = tbGoodsBianJia.Executor,
                                       ReleaseTimeStrrr = tbGoodsBianJia.ExecuteTime.ToString(),

                                   });
            int intTotalRow = listGoodsDetail.Count();//总行数
            List<Goods> listNotices = listGoodsDetail.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 采购价变价绑定
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public ActionResult HuoQuCaiGouBianJiaXinXi(int GID)
        {
            if (GID > 0)
            {
                var listGoods = (from tbGoodsBianJia in MyModels.B_GoodsBianJiaList
                                 //join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoodsBianJia.GoodsDetailID equals tbGoodsDetail.GoodsDetailID                                 
                                 join tbChangeWhy in MyModels.S_ChangeWhyList on tbGoodsBianJia.ChangeWhyID equals tbChangeWhy.ChangeWhyID
                                 where tbGoodsBianJia.GoodsBianJiaID == GID
                                 select new Vo.Goods
                                 {
                                     GoodsBianJiaID = tbGoodsBianJia.GoodsBianJiaID,
                                     BianJiaBianHao = tbGoodsBianJia.BianJiaBianHao,
                                     ReleaseTimeStrf = tbGoodsBianJia.BeginChangeData.ToString(),
                                     ChangeWhyID = tbChangeWhy.ChangeWhyID,
                                     ChangeWhy = tbChangeWhy.ChangeWhy,
                                     //GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                     //NewAdvanceBid = tbGoodsBianJia.NewAdvanceBid,
                                     //NewNoAdvanceBid = tbGoodsBianJia.NewNoAdvanceBid,
                                     Registrant = tbGoodsBianJia.Registrant,
                                     ReleaseTimeStr = tbGoodsBianJia.RegisterTime.ToString(),
                                     Auditor = tbGoodsBianJia.Auditor,
                                     ReleaseTimeStrr = tbGoodsBianJia.Checktime.ToString(),
                                     //Executor = tbGoodsBianJia.Executor,
                                     ReleaseTimeStrrr = tbGoodsBianJia.ExecuteTime.ToString(),
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
        public ActionResult CaiJiaBianJiaXiuGaiBaoCun()
        {
            string styMy = "fail";
            try
            {
                B_GoodsBianJiaList KK = new B_GoodsBianJiaList();
                KK.GoodsBianJiaID = Convert.ToInt32(Request.Form["GoodsBianJiaID"]);
                KK.BianJiaBianHao = Request.Form["BianJiaBianHao"];
                KK.BeginChangeData = Convert.ToDateTime(Request.Form["BeginChangeData"]);               
                KK.ChangeWhyID = Convert.ToInt32(Request.Form["ChangeWhyID"]);
                //KK.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                //KK.NewAdvanceBid = Convert.ToDateTime(Request.Form["NewAdvanceBid"]); 
                //KK.NewNoAdvanceBid = Convert.ToDateTime(Request.Form["NewNoAdvanceBid"]); 
                KK.Registrant = Request.Form["Registrant"];
                KK.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                KK.Auditor = Request.Form["Auditor"];
                KK.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                KK.Executor = Request.Form["Executor"];
                KK.ExecuteTime = Convert.ToDateTime(Request.Form["ExecuteTime"]);               

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

        /// <summary>
        /// 删除采购价变价
        /// </summary>
        /// <param name="saleAdjustId"></param>
        /// <returns></returns>
        public ActionResult DeleteCaiJiaBianJia(int goodsBianJiaId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_GoodsBianJiaList dbGoods = (from tbGoods in MyModels.B_GoodsBianJiaList
                                            where tbGoods.GoodsBianJiaID == goodsBianJiaId
                                              select tbGoods).Single();

                MyModels.B_GoodsBianJiaList.Remove(dbGoods);

                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.WuJiaGuanLi.Controllers
{
    public class TaiZhangFenXiJiaQianController : Controller
    {
        // GET: WuJiaGuanLi/TaiZhangFenXiJiaQian
        //台账分析价签
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();        

        #region 物价变动曲线
        public ActionResult WuJiaBianDongQuXian()
        {
            return View();
        }
        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectGoodsDetail(BsgridPage bsgridPage)
        {
            var listOrderFormPact = from tbGoodsBianJia in MyModels.B_GoodsBianJiaList
                                    join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoodsBianJia.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                    join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                    orderby tbGoodsBianJia.GoodsBianJiaID                                   
                                    select new Vo.Goods
                                    {
                                        GoodsBianJiaID = tbGoodsBianJia.GoodsBianJiaID,
                                        GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                        RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                        NewMonovalent = tbGoodsBianJia.NewMonovalent,
                                        GoodsCode = tbGoods.GoodsCode.Trim(),
                                        GoodsTiaoMa = tbGoods.GoodsTiaoMa.Trim(),
                                        GoodsName = tbGoods.GoodsName.Trim(),
                                    };
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<Goods> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
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
            List<Goods> list = new List<Goods>();
            string Q = ((string[])JieShouID)[0];
            string[] intsQ = Q.Split(',');

            for (int i = 0; i < intsQ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsQ[i]);
                var listGoods = (from tbGoodsBianJia in MyModels.B_GoodsBianJiaList
                                 join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoodsBianJia.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                 join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                 orderby tbGoodsBianJia.GoodsBianJiaID
                                 where tbGoodsBianJia.GoodsBianJiaID == goodsIDs
                                 select new Vo.Goods
                                 {
                                     GoodsBianJiaID = tbGoodsBianJia.GoodsBianJiaID,
                                     GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                     NewMonovalent = tbGoodsBianJia.NewMonovalent,
                                     GoodsCode = tbGoods.GoodsCode.Trim(),
                                     GoodsTiaoMa = tbGoods.GoodsTiaoMa.Trim(),
                                     GoodsName = tbGoods.GoodsName.Trim(),
                                 }).ToList();
                list.AddRange(listGoods);
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
        /// 删除
        /// </summary>
        /// <param name="goodsChopID"></param>
        /// <returns></returns>
        public ActionResult DeleteGoodsBianJia(int goodsBianJiaID)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_GoodsBianJiaList dbNoticeTypeDetail = (from tbNoticeTypeDetail in MyModels.B_GoodsBianJiaList
                                                         where tbNoticeTypeDetail.GoodsBianJiaID == goodsBianJiaID
                                                         select tbNoticeTypeDetail).Single();
                MyModels.B_GoodsBianJiaList.Remove(dbNoticeTypeDetail);
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

        #region 联营租赁扣率调整单
        public ActionResult KouLvDiaoZhengDan()
        {
            return View();
        }
        /// <summary>
        /// 调整单编码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectAdjustCode()
        {
            string strGoodsCode = "";
            var listDep = (from tbDmp in MyModels.B_SaleAdjustList orderby tbDmp.AdjustCode select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_SaleAdjustList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.AdjustCode.Substring(0, 6));
                intCode++;
                strGoodsCode = intCode.ToString();
                for (int i = 0; i < 6; i++)
                {
                    strGoodsCode = strGoodsCode.Length < 6 ? "0" + strGoodsCode : strGoodsCode;
                }
            }
            else
            {
                strGoodsCode = "000001";
            }
            return Json(strGoodsCode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 调整单状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectAdjustTypeName()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.S_AdjustTypeList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.AdjustTypeID,
                                                     text = tbEstimateUnit.AdjustTypeName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询处理时段
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectAgioTimeInterval(BsgridPage bsgridPage)
        {
            var listGoodsDetail = (from tbAgioTimeInterval in MyModels.S_AgioTimeIntervalList 
                                   join tbAgioTimeIntervalType in MyModels.S_AgioTimeIntervalTypeList on tbAgioTimeInterval.AgioTimeIntervalTypeID equals tbAgioTimeIntervalType.AgioTimeIntervalTypeID                                 
                                   orderby tbAgioTimeInterval.AgioTimeIntervalID                                  
                                   select new Vo.Goods
                                   {
                                       AgioTimeIntervalID = tbAgioTimeInterval.AgioTimeIntervalID,
                                       FenDanHao = tbAgioTimeInterval.FenDanHao,
                                       TimeInterval = tbAgioTimeInterval.TimeInterval,
                                       XingQi = tbAgioTimeInterval.XingQi,                                                                             
                                   });
            int intTotalRow = listGoodsDetail.Count();//总行数
            List<Goods> listNotices = listGoodsDetail.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取时段信息
        /// </summary>
        /// <param name="ShiDuanID"></param>
        /// <returns></returns>
        public ActionResult HuoQuAgioTimeInterval(int ShiDuanID)
        {
            if (ShiDuanID > 0)
            {
                var listAgioTimeInterval = (from tbAgioTimeInterval in MyModels.S_AgioTimeIntervalList
                                           join tbAgioTimeIntervalType in MyModels.S_AgioTimeIntervalTypeList on tbAgioTimeInterval.AgioTimeIntervalTypeID equals tbAgioTimeIntervalType.AgioTimeIntervalTypeID
                                           where tbAgioTimeInterval.AgioTimeIntervalID == ShiDuanID
                                             select new Vo.Goods
                                             {
                                                 AgioTimeIntervalID = tbAgioTimeInterval.AgioTimeIntervalID,
                                                 FenDanHao = tbAgioTimeInterval.FenDanHao,
                                                 TimeInterval = tbAgioTimeInterval.TimeInterval,
                                                 XingQi = tbAgioTimeInterval.XingQi,
                                             }).Single();
                return Json(listAgioTimeInterval, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 选择商品信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult XuanZeSangPinMingCheng(BsgridPage bsgridPage)
        {
            var listGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                   join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                   join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                                   join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                                   join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                                   join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID
                                   join tbCommodityType in MyModels.S_CommodityTypeList on tbGoods.CommodityTypeID equals tbCommodityType.CommodityTypeID
                                   orderby tbGoodsDetail.GoodsDetailID
                                   where tbCommodityType.CommodityTypeID != 1
                                   select new Vo.Goods
                                   {
                                       GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                       GoodsID = tbGoods.GoodsID,
                                       GoodsName = tbGoods.GoodsName,
                                       RateSale = tbGoodsDetail.RateSale,
                                       OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                       ContractNumber = tbOrderFormPact.ContractNumber,
                                       GoodsChopID = tbGoodsChop.GoodsChopID,
                                       GoodsChopName = tbGoodsChop.GoodsChopName,
                                       SupplierID = tbSupplier.SupplierID,
                                       SupplierNumber = tbSupplier.SupplierNumber,
                                       SupplierName = tbSupplier.SupplierName,
                                       EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                       EstimateUnitName = tbEstimateUnit.EstimateUnitName,                                     
                                   });
            int intTotalRow = listGoodsDetail.Count();//总行数
            List<Goods> listNotices = listGoodsDetail.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 绑定商品信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="JieShouID"></param>
        /// <returns></returns>
        public ActionResult HuoQuSangPinMingCheng(int ShangPinID)
        {
            if (ShangPinID > 0)
            {
                var listGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                            join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                            join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                                            join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                                            join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                                            join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID
                                            join tbCommodityType in MyModels.S_CommodityTypeList on tbGoods.CommodityTypeID equals tbCommodityType.CommodityTypeID
                                            where tbGoodsDetail.GoodsDetailID == ShangPinID
                                            select new Vo.Goods
                                            {
                                                GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                                GoodsID = tbGoods.GoodsID,
                                                GoodsName = tbGoods.GoodsName,
                                                RateSale = tbGoodsDetail.RateSale,
                                                OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                                ContractNumber = tbOrderFormPact.ContractNumber,
                                                GoodsChopID = tbGoodsChop.GoodsChopID,
                                                GoodsChopName = tbGoodsChop.GoodsChopName,
                                                SupplierID = tbSupplier.SupplierID,
                                                SupplierNumber = tbSupplier.SupplierNumber,
                                                SupplierName = tbSupplier.SupplierName,
                                                EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                                EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                            }).Single();
                return Json(listGoodsDetail, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 新增调整单信息
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengKouLvDiaoZhengDan(B_SaleAdjustList pwSaleAdjust)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldSaleAdjustRows = (from tbSaleAdjust in MyModels.B_SaleAdjustList
                                             where tbSaleAdjust.AdjustCode == pwSaleAdjust.AdjustCode
                                             select tbSaleAdjust).Count();
                if (oldSaleAdjustRows == 0)
                {
                    B_SaleAdjustList KK = new B_SaleAdjustList();

                    KK.BeginTime = Convert.ToDateTime(Request.Form["BeginTime"]);
                    KK.EndTime = Convert.ToDateTime(Request.Form["EndTime"]);
                    KK.AgioTimeIntervalID = Convert.ToInt32(Request.Form["AgioTimeIntervalID"]);
                    KK.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                    KK.AdjustCode = Request.Form["AdjustCode"];
                    KK.AdjustTypeID = Convert.ToInt32(Request.Form["AdjustTypeID"]);
                    KK.Sale = Convert.ToDecimal(Request.Form["Sale"]);
                    KK.NewRateSale = Convert.ToDecimal(Request.Form["NewRateSale"]);
                    KK.Registrant = Request.Form["Registrant"];
                    KK.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                    KK.Auditor = Request.Form["Auditor"];
                    KK.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                    KK.Executor = Request.Form["Executor"];
                    KK.ExecuteTime = Convert.ToDateTime(Request.Form["ExecuteTime"]);
                    KK.StopRen = Request.Form["StopRen"];
                    KK.StopShiJian = Convert.ToDateTime(Request.Form["StopShiJian"]);

                    if (KK.BeginTime != null && KK.EndTime != null && KK.AgioTimeIntervalID != null && KK.GoodsDetailID != null && KK.AdjustCode != null
                        && KK.AdjustTypeID != null && KK.Sale != null && KK.NewRateSale != null && KK.Registrant != null && KK.RegisterTime != null &&
                        KK.Auditor != null && KK.Checktime != null && KK.Executor != null && KK.ExecuteTime != null &&
                        KK.StopRen != null && KK.StopShiJian != null)
                    {
                        MyModels.B_SaleAdjustList.Add(KK);
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
        /// 调整单状态 回填
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectAdjustTypeName1()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.S_AdjustTypeList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.AdjustTypeID,
                                                     text = tbEstimateUnit.AdjustTypeName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询修改删除
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunXiuGaiShanChu(BsgridPage bsgridPage)
        {
            var listGoods = (from tbSaleAdjust in MyModels.B_SaleAdjustList
                             join tbAdjustType in MyModels.S_AdjustTypeList on tbSaleAdjust.AdjustTypeID equals tbAdjustType.AdjustTypeID
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbSaleAdjust.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                             join tbAgioTimeInterval in MyModels.S_AgioTimeIntervalList on tbSaleAdjust.AgioTimeIntervalID equals tbAgioTimeInterval.AgioTimeIntervalID
                             orderby tbSaleAdjust.AdjustTypeID
                             select new Vo.Goods
                             {
                                 SaleAdjustID = tbSaleAdjust.SaleAdjustID,                               
                                 ReleaseTimeStrd = tbSaleAdjust.BeginTime.ToString(),
                                 ReleaseTimeStrh = tbSaleAdjust.EndTime.ToString(),
                                 AgioTimeIntervalID = tbAgioTimeInterval.AgioTimeIntervalID,
                                 TimeInterval = tbAgioTimeInterval.TimeInterval,
                                 GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 GoodsDetailName = tbGoodsDetail.GoodsDetailName,
                                 AdjustCode = tbSaleAdjust.AdjustCode,
                                 AdjustTypeID = tbAdjustType.AdjustTypeID,
                                 AdjustTypeName = tbAdjustType.AdjustTypeName,
                                 Sale = tbSaleAdjust.Sale,
                                 NewRateSale = tbSaleAdjust.NewRateSale,                                                                
                                 Registrant = tbSaleAdjust.Registrant,
                                 ReleaseTimeStr = tbSaleAdjust.RegisterTime.ToString(),
                                 Auditor = tbSaleAdjust.Auditor,
                                 ReleaseTimeStrr = tbSaleAdjust.Checktime.ToString(),
                                 //Executor = tbSaleAdjust.Executor,
                                 ReleaseTimeStrrr = tbSaleAdjust.ExecuteTime.ToString(),
                                 StopRen = tbSaleAdjust.StopRen,
                                 ReleaseTimeStrf = tbSaleAdjust.StopShiJian.ToString(),
                                 DiaoZhengDanShenHeFou = tbSaleAdjust.DiaoZhengDanShenHeFou,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<Goods> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 选择回填数据
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShuJuXiuGaiShanChu(int GID)
        {
            if (GID > 0)
            {
                var listGoods = (from tbSaleAdjust in MyModels.B_SaleAdjustList
                                 join tbAdjustType in MyModels.S_AdjustTypeList on tbSaleAdjust.AdjustTypeID equals tbAdjustType.AdjustTypeID
                                 join tbGoodsDetail in MyModels.B_GoodsDetailList on tbSaleAdjust.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                 join tbAgioTimeInterval in MyModels.S_AgioTimeIntervalList on tbSaleAdjust.AgioTimeIntervalID equals tbAgioTimeInterval.AgioTimeIntervalID

                                 join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                 join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                                 join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                                 join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                                 join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID

                                 where tbSaleAdjust.SaleAdjustID == GID                                
                                 select new Vo.Goods
                                 {
                                     SaleAdjustID = tbSaleAdjust.SaleAdjustID,
                                     ReleaseTimeStrd = tbSaleAdjust.BeginTime.ToString(),
                                     ReleaseTimeStrh = tbSaleAdjust.EndTime.ToString(),

                                     AgioTimeIntervalID = tbAgioTimeInterval.AgioTimeIntervalID,
                                     FenDanHao = tbAgioTimeInterval.FenDanHao,
                                     TimeInterval = tbAgioTimeInterval.TimeInterval,
                                     XingQi = tbAgioTimeInterval.XingQi,  
                                                                        
                                     GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                     GoodsDetailName = tbGoodsDetail.GoodsDetailName,
                                     GoodsName = tbGoods.GoodsName,
                                     RateSale = tbGoodsDetail.RateSale,
                                     OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                     ContractNumber = tbOrderFormPact.ContractNumber,
                                     GoodsChopID = tbGoodsChop.GoodsChopID,
                                     GoodsChopName = tbGoodsChop.GoodsChopName,
                                     SupplierID = tbSupplier.SupplierID,
                                     SupplierNumber = tbSupplier.SupplierNumber,
                                     SupplierName = tbSupplier.SupplierName,
                                     EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                     EstimateUnitName = tbEstimateUnit.EstimateUnitName,

                                     AdjustCode = tbSaleAdjust.AdjustCode,
                                     AdjustTypeID = tbAdjustType.AdjustTypeID,
                                     AdjustTypeName = tbAdjustType.AdjustTypeName,
                                     Sale = tbSaleAdjust.Sale,
                                     NewRateSale = tbSaleAdjust.NewRateSale,
                                     Registrant = tbSaleAdjust.Registrant,
                                     ReleaseTimeStr = tbSaleAdjust.RegisterTime.ToString(),
                                     Auditor = tbSaleAdjust.Auditor,
                                     ReleaseTimeStrr = tbSaleAdjust.Checktime.ToString(),
                                     //Executor = tbSaleAdjust.Executor,
                                     ReleaseTimeStrrr = tbSaleAdjust.ExecuteTime.ToString(),
                                     StopRen = tbSaleAdjust.StopRen,
                                     ReleaseTimeStrf = tbSaleAdjust.StopShiJian.ToString(),
                                     DiaoZhengDanShenHeFou = tbSaleAdjust.DiaoZhengDanShenHeFou,
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
        public ActionResult DiaoZhengDanXiuGaiBaoCun()
        {          
            string styMy = "fail";
            try
            {
                B_SaleAdjustList KK = new B_SaleAdjustList();
                KK.SaleAdjustID = Convert.ToInt32(Request.Form["SaleAdjustID"]);                
                KK.BeginTime = Convert.ToDateTime(Request.Form["BeginTime"]);
                KK.EndTime = Convert.ToDateTime(Request.Form["EndTime"]);
                KK.AgioTimeIntervalID = Convert.ToInt32(Request.Form["AgioTimeIntervalID"]);
                KK.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                KK.AdjustTypeID = Convert.ToInt32(Request.Form["AdjustTypeID"]);
                KK.AdjustCode = Request.Form["AdjustCode"];
                KK.Sale = Convert.ToDecimal(Request.Form["Sale"]);
                KK.NewRateSale = Convert.ToDecimal(Request.Form["NewRateSale"]);
                KK.Registrant = Request.Form["Registrant"];
                KK.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                KK.Auditor = Request.Form["Auditor"];
                KK.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                KK.Executor = Request.Form["Executor"];
                KK.ExecuteTime = Convert.ToDateTime(Request.Form["ExecuteTime"]);
                KK.StopRen = Request.Form["StopRen"];
                KK.StopShiJian = Convert.ToDateTime(Request.Form["StopShiJian"]);
                KK.DiaoZhengDanShenHeFou = Convert.ToBoolean(Request.Form["DiaoZhengDanShenHeFou"]);
                
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
        /// 删除调整单信息
        /// </summary>
        /// <param name="saleAdjustId"></param>
        /// <returns></returns>
        public ActionResult DeleteSaleAdjust(int saleAdjustId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_SaleAdjustList dbGoods = (from tbSaleAdjust in MyModels.B_SaleAdjustList
                                            where tbSaleAdjust.DiaoZhengDanShenHeFou == false
                                            where tbSaleAdjust.SaleAdjustID == saleAdjustId
                                            select tbSaleAdjust).Single();

                MyModels.B_SaleAdjustList.Remove(dbGoods);

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

        #region 竞争对手定义
        public ActionResult JingZhengDuiShou()
        {
            return View();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectVieOpponent(BsgridPage bsgridPage)
        {
            var linqItme = from tbVieOpponent in MyModels.S_VieOpponentList
                           orderby tbVieOpponent.VieOpponentID
                           select tbVieOpponent;
            //查询总行数
            int intTotalRow = linqItme.Count();

            //使用Skip...Take...必须使用orderby
            List<S_VieOpponentList> listnChangeWhy = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            //实例化 Bsgrid的返回实体类
            Bsgrid<S_VieOpponentList> bsgrid = new Bsgrid<S_VieOpponentList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnChangeWhy;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自动生成竞争代码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectVieOpponentCode()
        {
            string strVieOpponentCode = "";
            var listDep = (from tbDmp in MyModels.S_VieOpponentList orderby tbDmp.VieOpponentCode select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                S_VieOpponentList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.VieOpponentCode.Substring(2, 5));
                intCode++;
                strVieOpponentCode = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {
                    strVieOpponentCode = strVieOpponentCode.Length < 4 ? "0" + strVieOpponentCode : strVieOpponentCode;
                }
            }
            else
            {
                strVieOpponentCode = "0001";
            }
            return Json(strVieOpponentCode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增保存
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertVieOpponent(S_VieOpponentList pwVieOpponent)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.S_VieOpponentList
                                             where tbGoodsMoneyRule.VieOpponentCode == pwVieOpponent.VieOpponentCode
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    S_VieOpponentList JJ = new S_VieOpponentList();
                    JJ.VieOpponentCode = Request.Form["VieOpponentCode"];
                    JJ.VieOpponentName = Request.Form["VieOpponentName"];
                    JJ.Area = Convert.ToDecimal(Request.Form["Area"]);
                    JJ.SalesVolume = Convert.ToDecimal(Request.Form["SalesVolume"]);

                    if (JJ.VieOpponentCode != null && JJ.VieOpponentName != null && JJ.Area != null && JJ.SalesVolume != null)
                    {
                        MyModels.S_VieOpponentList.Add(JJ);
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
        /// 修改查询
        /// </summary>
        /// <param name="vieOpponentId"></param>
        /// <returns></returns>
        public ActionResult UpdataVieOpponent(int vieOpponentId)
        {
            if (vieOpponentId > 0)
            {
                var ListVieOpponent = (from tbEmp in MyModels.S_VieOpponentList
                                       where tbEmp.VieOpponentID == vieOpponentId
                                       select new
                                       {
                                           tbEmp.VieOpponentID,
                                           tbEmp.VieOpponentCode,
                                           tbEmp.VieOpponentName,
                                           tbEmp.Area,
                                           tbEmp.SalesVolume,
                                       }).Single();
                return Json(ListVieOpponent, JsonRequestBehavior.AllowGet);
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
        public ActionResult XiuGaiBaoCun()
        {
            string styMy = "fail";
            try
            {
                S_VieOpponentList JJ = new S_VieOpponentList();
                JJ.VieOpponentID = Convert.ToInt32(Request.Form["VieOpponentID"]);
                JJ.VieOpponentCode = Request.Form["VieOpponentCode"];
                JJ.VieOpponentName = Request.Form["VieOpponentName"];
                JJ.Area = Convert.ToDecimal(Request.Form["Area"]);
                JJ.SalesVolume = Convert.ToDecimal(Request.Form["SalesVolume"]);

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

        #region 采价分析_录入
        public ActionResult CaiJiaFenXi()
        {
            return View();
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsDetailName()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.B_GoodsDetailList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.GoodsDetailID,
                                                     text = tbEstimateUnit.GoodsDetailName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增采购信息
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengCaiGouBiao(B_PurchaseList pwPurchase)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.B_PurchaseList
                                             where tbGoodsMoneyRule.PurchasingAgent == pwPurchase.PurchasingAgent
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    B_PurchaseList KK = new B_PurchaseList();
                    KK.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                    KK.PurchasingAgent = Request.Form["PurchasingAgent"];
                    KK.PriceData = Convert.ToDateTime(Request.Form["PriceData"]);
                    KK.PricePlace = Request.Form["PricePlace"];
                    KK.PricePlace2 = Request.Form["PricePlace2"];
                    KK.PricePlace3 = Request.Form["PricePlace3"];
                    KK.PricePlace4 = Request.Form["PricePlace4"];
                    KK.Price1 = Convert.ToDecimal(Request.Form["Price1"]);
                    KK.Price2 = Convert.ToDecimal(Request.Form["Price2"]);
                    KK.Price3 = Convert.ToDecimal(Request.Form["Price3"]);
                    KK.Price4 = Convert.ToDecimal(Request.Form["Price4"]);
                    KK.NoAdvanceBid = Convert.ToDecimal(Request.Form["NoAdvanceBid"]);
                    KK.AdvanceBid = Convert.ToDecimal(Request.Form["AdvanceBid"]);
                    KK.PurchasePriceMarkup = Convert.ToDecimal(Request.Form["PurchasePriceMarkup"]);
                    KK.LastBid = Convert.ToDecimal(Request.Form["LastBid"]);
                    if (KK.GoodsDetailID != null && KK.PurchasingAgent != null && KK.PriceData != null && KK.PricePlace != null &&
                        KK.PricePlace2 != null && KK.PricePlace3 != null && KK.PricePlace4 != null && KK.Price1 != null &&
                        KK.Price2 != null && KK.Price3 != null && KK.Price4 != null && KK.NoAdvanceBid != null &&
                        KK.AdvanceBid != null && KK.PurchasePriceMarkup != null && KK.LastBid != null)
                    {
                        MyModels.B_PurchaseList.Add(KK);
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
        /// 查询采购信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectCaiGouFenXi(BsgridPage bsgridPage)
        {
            var listGoods = (from tbPurchase in MyModels.B_PurchaseList
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbPurchase.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                             join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                             join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID
                             join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                             orderby tbPurchase.PurchaseID
                             select new Vo.Goods
                             {
                                 PurchaseID = tbPurchase.PurchaseID,
                                 GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsName = tbGoods.GoodsName,
                                 SpecificationsModel = tbGoods.SpecificationsModel,
                                 GoodsChopID = tbGoodsChop.GoodsChopID,
                                 GoodsChopName = tbGoodsChop.GoodsChopName,
                                 EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                 EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 Price1 = tbPurchase.Price1,
                                 Price2 = tbPurchase.Price2,
                                 Price3 = tbPurchase.Price3,
                                 Price4 = tbPurchase.Price4,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<Goods> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除采购信息
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <returns></returns>
        public ActionResult DeletePurchase(int purchaseId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_PurchaseList dbPurchase = (from tbGoods in MyModels.B_PurchaseList
                                             where tbGoods.PurchaseID == purchaseId
                                             select tbGoods).Single();

                MyModels.B_PurchaseList.Remove(dbPurchase);

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

        #region 定义价签类型
        public ActionResult JiaQianLeiXing()
        {
            return View();
        }
        /// <summary>
        /// 价签类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSignTypeName()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.S_SignTypeList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.SignTypeID,
                                                     text = tbEstimateUnit.SignTypeName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增价签信息
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengJiaQianXinXi(B_GoodsSignList pwGoodsSign)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.B_GoodsSignList
                                             where tbGoodsMoneyRule.JiaQianCode == pwGoodsSign.JiaQianCode
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    B_GoodsSignList JJ = new B_GoodsSignList();
                    JJ.JiaQianCode = Request.Form["JiaQianCode"];
                    JJ.GoodsSignName = Request.Form["GoodsSignName"];
                    JJ.SignTypeID = Convert.ToInt32(Request.Form["SignTypeID"]);
                    JJ.Width = Request.Form["Width"];
                    JJ.Height = Request.Form["Height"];
                    JJ.Broadwise = Request.Form["Broadwise"];
                    JJ.Ordinate = Request.Form["Ordinate"];
                    JJ.EveryRow = Request.Form["EveryRow"];
                    JJ.EveryRank = Request.Form["EveryRank"];

                    if (JJ.JiaQianCode != null && JJ.GoodsSignName != null && JJ.SignTypeID != null && JJ.Width != null && JJ.Height != null
                        && JJ.Broadwise != null && JJ.Ordinate != null && JJ.EveryRow != null && JJ.EveryRank != null)
                    {
                        MyModels.B_GoodsSignList.Add(JJ);
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
        /// 查询价签信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectJiaQianXinXi(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoodsSign in MyModels.B_GoodsSignList
                             join tbSignType in MyModels.S_SignTypeList on tbGoodsSign.SignTypeID equals tbSignType.SignTypeID                           
                             orderby tbGoodsSign.GoodsSignID
                             select new Vo.Goods
                             {
                                 GoodsSignID = tbGoodsSign.GoodsSignID,
                                 JiaQianCode = tbGoodsSign.JiaQianCode,
                                 GoodsSignName = tbGoodsSign.GoodsSignName,
                                 SignTypeID = tbSignType.SignTypeID,
                                 SignTypeName = tbSignType.SignTypeName,
                                 Width = tbGoodsSign.Width,
                                 Height = tbGoodsSign.Height,
                                 Broadwise = tbGoodsSign.Broadwise,
                                 Ordinate = tbGoodsSign.Ordinate,
                                 EveryRow = tbGoodsSign.EveryRow,
                                 EveryRank = tbGoodsSign.EveryRank,                                 
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<Goods> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 价签类型 回填
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSignTypeName1()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.S_SignTypeList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.SignTypeID,
                                                     text = tbEstimateUnit.SignTypeName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取价签信息
        /// </summary>
        /// <param name="JiaQianID"></param>
        /// <returns></returns>
        public ActionResult HuoQuJiaQianMingCheng(int JiaQianID)
        {
            if (JiaQianID > 0)
            {
                var JiaQian = (from tbGoodsSign in MyModels.B_GoodsSignList
                              join tbSignType in MyModels.S_SignTypeList on tbGoodsSign.SignTypeID equals tbSignType.SignTypeID
                              where tbGoodsSign.GoodsSignID == JiaQianID                           
                              select new Vo.Goods
                              {
                                  GoodsSignID = tbGoodsSign.GoodsSignID,
                                  JiaQianCode = tbGoodsSign.JiaQianCode,
                                  GoodsSignName = tbGoodsSign.GoodsSignName,
                                  SignTypeID = tbSignType.SignTypeID,
                                  SignTypeName = tbSignType.SignTypeName,
                                  Width = tbGoodsSign.Width,
                                  Height = tbGoodsSign.Height,
                                  Broadwise = tbGoodsSign.Broadwise,
                                  Ordinate = tbGoodsSign.Ordinate,
                                  EveryRow = tbGoodsSign.EveryRow,
                                  EveryRank = tbGoodsSign.EveryRank,
                              }).Single();
                return Json(JiaQian, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 修改价签信息
        /// </summary>
        /// <returns></returns>
        public ActionResult XiuGaiJiaQianXinXi()
        {
            string styMy = "fail";
            try
            {
                B_GoodsSignList JJ = new B_GoodsSignList();
                JJ.GoodsSignID = Convert.ToInt32(Request.Form["GoodsSignID"]);
                JJ.JiaQianCode = Request.Form["JiaQianCode"];
                JJ.GoodsSignName = Request.Form["GoodsSignName"];
                JJ.SignTypeID = Convert.ToInt32(Request.Form["SignTypeID"]);
                JJ.Width = Request.Form["Width"];
                JJ.Height = Request.Form["Height"];
                JJ.Broadwise = Request.Form["Broadwise"];
                JJ.Ordinate = Request.Form["Ordinate"];
                JJ.EveryRow = Request.Form["EveryRow"];
                JJ.EveryRank = Request.Form["EveryRank"];

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
        /// 删除价签信息
        /// </summary>
        /// <param name="goodsSignId"></param>
        /// <returns></returns>
        public ActionResult DeleteGoodsSignID(int goodsSignId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_GoodsSignList dbPurchase = (from tbGoods in MyModels.B_GoodsSignList
                                             where tbGoods.GoodsSignID == goodsSignId
                                              select tbGoods).Single();

                MyModels.B_GoodsSignList.Remove(dbPurchase);

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

        #region 打印商品价签
        public ActionResult DaYingShangPinJiaQian()
        {
            return View();
        }
        /// <summary>
        /// 查询价签信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunJiaQianXinXi(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoodsSign in MyModels.B_GoodsSignList
                             join tbSignType in MyModels.S_SignTypeList on tbGoodsSign.SignTypeID equals tbSignType.SignTypeID
                             orderby tbGoodsSign.GoodsSignID
                             select new Vo.Goods
                             {                               
                                 GoodsSignID = tbGoodsSign.GoodsSignID,
                                 JiaQianCode = tbGoodsSign.JiaQianCode,
                                 GoodsSignName = tbGoodsSign.GoodsSignName,
                                 SignTypeID = tbSignType.SignTypeID,
                                 SignTypeName = tbSignType.SignTypeName,
                                 Width = tbGoodsSign.Width,
                                 Height = tbGoodsSign.Height,
                                 Broadwise = tbGoodsSign.Broadwise,
                                 Ordinate = tbGoodsSign.Ordinate,
                                 EveryRow = tbGoodsSign.EveryRow,
                                 EveryRank = tbGoodsSign.EveryRank,

                             });
            int intTotalRow = listGoods.Count();//总行数
            List<Goods> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 售价组别
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSellAssembleName()
        {
            List<SelectVo> listCommodityTypeID = (from tbCommodityType in MyModels.S_SellAssemblelist                                                 
                                                  select new SelectVo
                                                  {
                                                      id = tbCommodityType.SellAssembleID,
                                                      text = tbCommodityType.SellAssembleName.Trim()
                                                  }).ToList();

            return Json(listCommodityTypeID, JsonRequestBehavior.AllowGet);          
        }


        #endregion

    }
}
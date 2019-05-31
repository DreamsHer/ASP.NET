using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.WuJiaGuanLi.Controllers
{
    public class ShangPinDingJiaGuanLiController : Controller
    {
        // GET: WuJiaGuanLi/ShangPinDingJiaGuanLi
        //商品定价管理
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        #region 打包商品定价
        public ActionResult DaBaoShangPinDingJia()
        {
            return View();
        }
        /// <summary>
        /// 商品定价状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsPurchasemoneyStatusID()
        {
            List<SelectVo> listPriceType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listPriceType.Add(selectVo);

            List<SelectVo> listPriceTypeID = (from tbGoodsBarCod in MyModels.S_GoodsPurchasemoneyStatusList
                                              select new SelectVo
                                              {
                                                  id = tbGoodsBarCod.GoodsPurchasemoneyStatusID,
                                                  text = tbGoodsBarCod.GoodsPurchasemoneyStatus.Trim()
                                              }).ToList();

            listPriceType.AddRange(listPriceTypeID);

            return Json(listPriceType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 左边条件查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="goodsPurchasemoneyStatusID"></param>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public ActionResult ChaXunXuDingJiaShangPin(BsgridPage bsgridPage, int goodsPurchasemoneyStatusID, string goodsCode)
        {
            var items = (from tbPackGoods in MyModels.B_PackGoodsList
                         join tbGoodsDetail in MyModels.B_GoodsDetailList on tbPackGoods.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                         join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                         join tbGoodsPurchasemoneyStatus in MyModels.S_GoodsPurchasemoneyStatusList on tbPackGoods.GoodsPurchasemoneyStatusID equals tbGoodsPurchasemoneyStatus.GoodsPurchasemoneyStatusID
                         join tbEstimateUnit in MyModels.S_EstimateUnitList on tbPackGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                         orderby tbPackGoods.PackGoodsID
                         select new Vo.Goods
                         {
                             PackGoodsID = tbPackGoods.PackGoodsID,
                             PackCode = tbPackGoods.PackCode, /*打包代码*/
                             PackName = tbPackGoods.PackName,   /*打包名称*/
                             PackTiaoMa = tbPackGoods.PackTiaoMa, /*打包条码*/
                             GoodsID = tbGoods.GoodsID,
                             GoodsCode = tbGoods.GoodsCode,/*商品代码*/
                             GoodsName = tbGoods.GoodsName,/*商品名称*/
                             GoodsPurchasemoneyStatusID = tbGoodsPurchasemoneyStatus.GoodsPurchasemoneyStatusID,/*商品定价状态*/
                             GoodsPurchasemoneyStatus = tbGoodsPurchasemoneyStatus.GoodsPurchasemoneyStatus,
                             GoodsTiaoMa = tbGoods.GoodsTiaoMa,/*商品条码*/
                             RetailMonovalent = tbGoodsDetail.RetailMonovalent, /* 零售单价*/
                             //FactPrice = tbGoodsDetail.FactPrice, /*实际零售单价*/
                             EstimateUnitID = tbEstimateUnit.EstimateUnitID,  /* 计量单位*/
                             EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                             SpecificationsModel = tbGoods.SpecificationsModel,  /*规格模型*/
                             ReferencePrice = tbGoodsDetail.ReferencePrice,     /* 参考零售单价*/
                         });
            //如果查询条件不为空
            if (goodsPurchasemoneyStatusID > 0)
            {
                items = items.Where(p => p.GoodsPurchasemoneyStatusID == goodsPurchasemoneyStatusID);
            }
            if (!string.IsNullOrEmpty(goodsCode))
            {
                goodsCode = goodsCode.Trim();
                items = items.Where(p => p.GoodsCode.Contains(goodsCode));
            }
            int intTotalRow = items.Count();//总行数
            List<Goods> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取相应的商品信息
        /// </summary>
        /// <param name="GGPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPinXinXi(int GGPID)
        {
            if (GGPID > 0)
            {
                var listGoods = (from tbPackGoods in MyModels.B_PackGoodsList
                                 join tbGoodsDetail in MyModels.B_GoodsDetailList on tbPackGoods.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                                 join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                 join tbEstimateUnit in MyModels.S_EstimateUnitList on tbPackGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                                 where tbPackGoods.PackGoodsID == GGPID
                                 select new Vo.Goods
                                 {
                                     PackGoodsID = tbPackGoods.PackGoodsID,
                                     PackCode = tbPackGoods.PackCode,   /*打包代码*/
                                     PackName = tbPackGoods.PackName,  /*打包名称*/
                                     PackTiaoMa = tbPackGoods.PackTiaoMa,  /*打包条码*/
                                     GoodsID = tbGoods.GoodsID,
                                     GoodsCode = tbGoods.GoodsCode,/*商品代码*/
                                     GoodsTiaoMa = tbGoods.GoodsTiaoMa,/*商品条码*/
                                     GoodsName = tbGoods.GoodsName,/*商品名称*/
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent, /* 零售单价*/
                                     EstimateUnitID = tbEstimateUnit.EstimateUnitID,  /* 计量单位*/
                                     EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                     SpecificationsModel = tbGoods.SpecificationsModel,  /*规格模型*/
                                     ReferencePrice = tbGoodsDetail.ReferencePrice,     /* 参考零售单价*/
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 查询打包商品定价
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult XiuGaiDingJiaXinXi(BsgridPage bsgridPage)
        {
            var listGoods = (from tbPackGoods in MyModels.B_PackGoodsList
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbPackGoods.GoodsDetailID equals tbGoodsDetail.GoodsDetailID
                             join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                             orderby tbPackGoods.PackGoodsID
                             select new Vo.Goods
                             {
                                 PackGoodsID = tbPackGoods.PackGoodsID,
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
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

        #endregion

        #region 定义商品定价规则
        public ActionResult ShangPinDingJiaGuiZe()
        {
            return View();
        }
        /// <summary>
        /// 价格类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPriceTypeID()
        {
            List<SelectVo> listPriceType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listPriceType.Add(selectVo);

            List<SelectVo> listPriceTypeID = (from tbGoodsBarCod in MyModels.S_PriceTypeList
                                              select new SelectVo
                                              {
                                                  id = tbGoodsBarCod.PriceTypeID,
                                                  text = tbGoodsBarCod.PriceTypeMC.Trim()
                                              }).ToList();

            listPriceType.AddRange(listPriceTypeID);

            return Json(listPriceType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询定价规则
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectGoodsMoneyRule(BsgridPage bsgridPage)
        {
            var linqItme = from tbGoodsMoneyRule in MyModels.S_GoodsMoneyRuleList
                           orderby tbGoodsMoneyRule.GoodsMoneyRuleID
                           select tbGoodsMoneyRule;
            //查询总行数
            int intTotalRow = linqItme.Count();

            //使用Skip...Take...必须使用orderby
            List<S_GoodsMoneyRuleList> listnGoodsMoneyRule = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            //实例化 Bsgrid的返回实体类
            Bsgrid<S_GoodsMoneyRuleList> bsgrid = new Bsgrid<S_GoodsMoneyRuleList>();
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnGoodsMoneyRule;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增定价规则
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertGoodsMoneyRule(S_GoodsMoneyRuleList pwGoodsMoneyRule)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in MyModels.S_GoodsMoneyRuleList
                                             where tbGoodsMoneyRule.ShangXianMoney == pwGoodsMoneyRule.ShangXianMoney
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    S_GoodsMoneyRuleList JJ = new S_GoodsMoneyRuleList();
                    JJ.ShangXianMoney = Convert.ToDecimal(Request.Form["ShangXianMoney"]);
                    JJ.XiaXianMoney = Convert.ToDecimal(Request.Form["XiaXianMoney"]);
                    JJ.DecimalDigits = Request.Form["DecimalDigits"];
                    JJ.PriceTypeID = Convert.ToInt32(Request.Form["PriceTypeID"]);

                    MyModels.S_GoodsMoneyRuleList.Add(JJ);
                    MyModels.SaveChanges();

                    strMsg = "success";
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
        /// 删除定价规则
        /// </summary>
        /// <param name="GoodsMoneyRuleId"></param>
        /// <returns></returns>
        public ActionResult DeleteGoodsMoneyRule(int GoodsMoneyRuleId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                S_GoodsMoneyRuleList dbGoodsMoneyRule = (from tbGoodsMoneyRule in MyModels.S_GoodsMoneyRuleList
                                                         where tbGoodsMoneyRule.GoodsMoneyRuleID == GoodsMoneyRuleId
                                                         select tbGoodsMoneyRule).Single();
                MyModels.S_GoodsMoneyRuleList.Remove(dbGoodsMoneyRule);
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
               
        #region 设定商品最低售价
        public ActionResult ShangPinZuiDiShouJia()
        {
            return View();
        }
        /// <summary>
        /// 商品分类
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaXunShangPinFenLei()
        {
            List<SelectVo> listPriceType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listPriceType.Add(selectVo);

            List<SelectVo> listPriceTypeID = (from tbGoodsBarCod in MyModels.B_GoodsClassifyList
                                              select new SelectVo
                                              {
                                                  id = tbGoodsBarCod.GoodsClassifyID,
                                                  text = tbGoodsBarCod.GoodsClassifyName.Trim()
                                              }).ToList();

            listPriceType.AddRange(listPriceTypeID);

            return Json(listPriceType, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 商标品牌
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaXunShangBiaoPinPai()
        {
            List<SelectVo> listPriceType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listPriceType.Add(selectVo);

            List<SelectVo> listPriceTypeID = (from tbGoodsBarCod in MyModels.B_GoodsChopList
                                              select new SelectVo
                                              {
                                                  id = tbGoodsBarCod.GoodsChopID,
                                                  text = tbGoodsBarCod.GoodsChopName.Trim()
                                              }).ToList();

            listPriceType.AddRange(listPriceTypeID);

            return Json(listPriceType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="goodsCode"></param>
        /// <param name="goodsClassifyName"></param>
        /// <param name="goodsChopName"></param>
        /// <returns></returns>       
        public ActionResult ChaXunZuiDiShouJia(BsgridPage bsgridPage, string goodsCode, int goodsClassifyName, int goodsChopName)
        {
            var items = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                         join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID                         
                         join tbGoodsClassify in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodsClassify.GoodsClassifyID /*商品分类*/
                         join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID /*商标品牌*/
                         join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID /*计量单位*/ 
                         join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID /*采购表(最后进价)*/                                               
                         orderby tbGoodsDetail.GoodsDetailID
                         select new Vo.Goods
                         {
                             GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                             GoodsID = tbGoods.GoodsID,
                             GoodsCode = tbGoods.GoodsCode,/*商品代码*/
                             GoodsClassifyID = tbGoodsClassify.GoodsClassifyID,/*商品分类*/
                             GoodsClassifyName = tbGoodsClassify.GoodsClassifyName,
                             GoodsChopID = tbGoodsChop.GoodsChopID,/*商标品牌*/
                             GoodsChopName = tbGoodsChop.GoodsChopName,
                             GoodsName = tbGoods.GoodsName,/*商品名称*/
                             SpecificationsModel = tbGoods.SpecificationsModel,/*规格型号*/
                             EstimateUnitID = tbEstimateUnit.EstimateUnitID, /*计量单位*/
                             EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                             LastBid = tbPurchase.LastBid,/*最后进价*/
                            
                             RetailMonovalent = tbGoodsDetail.RetailMonovalent,  /*零售单价\原最低售价单价*/
                         });
            //如果查询条件不为空
            if (!string.IsNullOrEmpty(goodsCode))
            {
                goodsCode = goodsCode.Trim();
                items = items.Where(p => p.GoodsCode.Contains(goodsCode));
            }
            if (goodsClassifyName > 0)
            {
                items = items.Where(p => p.GoodsClassifyID == goodsClassifyName);
            }
            if (goodsChopName > 0)
            {
                items = items.Where(p => p.GoodsChopID == goodsChopName);
            }
            //if (!string.IsNullOrEmpty(goodsClassifyName))
            //{
            //    goodsClassifyName = goodsClassifyName.Trim();
            //    items = items.Where(p => p.GoodsClassifyName.Contains(goodsClassifyName));
            //}
            //if (!string.IsNullOrEmpty(goodsChopName))
            //{
            //    goodsChopName = goodsChopName.Trim();
            //    items = items.Where(p => p.GoodsChopName.Contains(goodsChopName));
            //}
            int intTotalRow = items.Count();//总行数
            List<Goods> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///  新增商品最低售价
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengZuiDiShouJia()
        {          
            S_GoodsBottomPriceList QQ = new S_GoodsBottomPriceList();            
            QQ.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
            QQ.DiscountFrons =Convert.ToDecimal(Request.Form["DiscountFrons"]);
            QQ.DiscountRate = Convert.ToDecimal(Request.Form["DiscountRate"]);
            QQ.LowestRetail = Convert.ToDecimal(Request.Form["LowestRetail"]);
            if (QQ.GoodsDetailID != null && QQ.DiscountFrons != null && QQ.DiscountRate != null && QQ.LowestRetail != null)
            {
                MyModels.S_GoodsBottomPriceList.Add(QQ);
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
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi.Controllers
{
    public class QianTaiXiaoShouChaXunController : Controller
    {
        // GET: GoodsZhangGuanLi/QianTaiXiaoShouChaXun
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        #region 前台销售单查询
        /// <summary>
        ///查询界面
        /// </summary>
        /// <returns></returns>
        public ActionResult QianTaiXiaoShouChaXunDan()
        {
            return View();
        }

        /// <summary>
        /// 前台销售订单查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="shooppingnumber"></param>
        /// <param name="shooppingTime"></param>
        /// <returns></returns>
        public ActionResult SelectShooppingOrderFount(BsgridPage bsgridPage, string shooppingnumber, string shooppingTime)
        {
            var items = from tbShooppingOrderFount in MyModels.B_ShooppingOrderFountList
                        join tbShooppingDateilIDOrderFoun in MyModels.B_ShooppingDateilIDOrderFounList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShooppingDateilIDOrderFoun.ShooppingOrderFountID
                        join tbShopping in MyModels.B_ShoppingList on tbShooppingDateilIDOrderFoun.ShoppingID equals tbShopping.ShoppingID
                        join tbGoodsListedDetail in MyModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbGoodsListedDetail.GoodsListedDetailID

                        join tbGoodsListed in MyModels.B_GoodsListedList on tbGoodsListedDetail.GoodsListedID equals tbGoodsListed.GoodsListedID
                        join tbLeiXing in MyModels.S_HuoDongZhuanQu on tbGoodsListed.GoodsDingYiQuID equals tbLeiXing.GoodsDingYiQuID
                        join tbConverDeTail in MyModels.B_ConverDeTailList on tbGoodsListedDetail.ConverDateilID equals tbConverDeTail.ConverDeTaiID

                        join tbGoods in MyModels.B_GoodsList on tbConverDeTail.GoodsID equals tbGoods.GoodsID
                        join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                        join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID //零售单价
                        join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID //商标品牌

                        where tbShooppingOrderFount.ExamineNot == false

                        orderby tbShooppingOrderFount.ShooppingOrderFountID descending
                        select new Vo.Goods
                        {
                            //订单明细信息
                            ShooppingDateilIDOrderFounID = tbShooppingDateilIDOrderFoun.ShooppingDateilIDOrderFounID,

                            //订单信息
                            ShooppingOrderFountID = tbShooppingOrderFount.ShooppingOrderFountID,
                            ShooppingNumber = tbShooppingOrderFount.ShooppingNumber,/* 订单号*/
                            ShooppingTime = tbShooppingOrderFount.ShooppingTime,  /*生成订单号时间*/
                            ReleaseTimeSW = tbShooppingOrderFount.ShooppingTime.ToString(),  /*生成订单号时间*/
                            ExamineNot = tbShooppingOrderFount.ExamineNot,//订单审核否

                            //购物单信息
                            ShoppingID = tbShopping.ShoppingID,
                            MumberOfPackages = tbShopping.MumberOfPackages, /*购物单信息*/

                            XingQi = tbShopping.BitColer, /*购物颜色*/
                            Subdivision = tbShopping.Money, /*购物金额*/

                            //上市明细信息
                            GoodsListedDetailID = tbGoodsListedDetail.GoodsListedDetailID,

                            Quantity = tbGoodsListed.ConsigneeShop,
                            ChangeWhy = tbLeiXing.GoodsDingYiQuMC,

                            //配货明细信息
                            ConverDateilID = tbConverDeTail.ConverDeTaiID,

                            //商品信息
                            GoodsID = tbGoods.GoodsID,
                            GoodsCode = tbGoods.GoodsCode, //商品编号*/
                            GoodsName = tbGoods.GoodsName,/*商品名称*/
                            EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                            GoodsChopID = tbGoodsChop.GoodsChopID,
                            GoodsChopName = tbGoodsChop.GoodsChopName,/*商标品牌*/
                            SpecificationsModel = tbGoods.SpecificationsModel,/*规格型号*/
                            RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/

                        };
            //如果查询条件不为空
            if (!string.IsNullOrEmpty(shooppingnumber))
            {
                shooppingnumber = shooppingnumber.Trim();
                items = items.Where(p => p.ShooppingNumber.Contains(shooppingnumber));
            }
            if (!string.IsNullOrEmpty(shooppingTime))
            {
                try
                {
                    DateTime dtreleaseTime = Convert.ToDateTime(shooppingTime);
                    items = items.Where(p => p.ShooppingTime == dtreleaseTime);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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
        /// 点击id
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShooppingOrderFountId(int shoppingID)
        {
            var items = (from tbShooppingOrderFount in MyModels.B_ShooppingOrderFountList
                         join tbShooppingDateilIDOrderFoun in MyModels.B_ShooppingDateilIDOrderFounList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShooppingDateilIDOrderFoun.ShooppingOrderFountID
                         join tbShopping in MyModels.B_ShoppingList on tbShooppingDateilIDOrderFoun.ShoppingID equals tbShopping.ShoppingID
                         join tbGoodsListedDetail in MyModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbGoodsListedDetail.GoodsListedDetailID
                         join tbUname in MyModels.B_Unamelist on tbShopping.UnameID equals tbUname.UnameID
                         join tbPersonalIfor in MyModels.S_PersonalIformationList on tbShopping.UnameID equals tbPersonalIfor.UnameID//个人信息

                         join tbGoodsListed in MyModels.B_GoodsListedList on tbGoodsListedDetail.GoodsListedID equals tbGoodsListed.GoodsListedID
                         join tbLeiXing in MyModels.S_HuoDongZhuanQu on tbGoodsListed.GoodsDingYiQuID equals tbLeiXing.GoodsDingYiQuID

                         join tbConverDeTail in MyModels.B_ConverDeTailList on tbGoodsListedDetail.ConverDateilID equals tbConverDeTail.ConverDeTaiID

                         join tbGoods in MyModels.B_GoodsList on tbConverDeTail.GoodsID equals tbGoods.GoodsID
                         join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                         join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID //零售单价
                         join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID //商标品牌
                         where tbShopping.ShoppingID == shoppingID
                         select new Vo.Goods
                         {
                             //订单明细信息
                             ShooppingDateilIDOrderFounID = tbShooppingDateilIDOrderFoun.ShooppingDateilIDOrderFounID,

                             //订单信息
                             ShooppingOrderFountID = tbShooppingOrderFount.ShooppingOrderFountID,
                             ShooppingNumber = tbShooppingOrderFount.ShooppingNumber,/* 订单号*/
                             ReleaseTimeStrx = tbShooppingOrderFount.ShooppingTime.ToString(),  /*生成订单号时间*/
                             ExamineNot = tbShooppingOrderFount.ExamineNot,//订单审核否
                             EveryRank = tbShooppingOrderFount.Message,//留言
                             //购物单信息
                             ShoppingID = tbShopping.ShoppingID,
                             MumberOfPackages = tbShopping.MumberOfPackages, /*购物单信息*/

                             XingQi = tbShopping.BitColer, /*购物颜色*/
                             Subdivision = tbShopping.Money, /*购物金额*/
                             //上市明细信息
                             GoodsListedDetailID = tbGoodsListedDetail.GoodsListedDetailID,
                             Quantity = tbGoodsListed.ConsigneeShop,
                             ChangeWhy = tbLeiXing.GoodsDingYiQuMC,
                             NewMonovalent = tbGoodsListedDetail.MumberOfPackages,//库存量商品

                             //配货明细信息
                             ConverDateilID = tbConverDeTail.ConverDeTaiID,
                             //商品信息
                             GoodsID = tbGoods.GoodsID,
                             GoodsCode = tbGoods.GoodsCode, //商品编号*/
                             GoodsName = tbGoods.GoodsName,/*商品名称*/
                             EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                             GoodsChopID = tbGoodsChop.GoodsChopID,
                             GoodsChopName = tbGoodsChop.GoodsChopName,/*商标品牌*/
                             SpecificationsModel = tbGoods.SpecificationsModel,/*规格型号*/
                             RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/

                             //用户
                             EveryRow = tbUname.uname,
                             Ordinate = tbPersonalIfor.Phone,//手机
                             Broadwise = tbPersonalIfor.Location,//地区
                             Height = tbPersonalIfor.Zipcode,//邮政编码
                             Width = tbPersonalIfor.Detailedaddress,//地址

                         }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// select(上市明细)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsListedDeta(int goodsListedDetailId)
        {
            var items = (from tbGoodsListedDetail in MyModels.B_GoodsListedDetailList
                         where tbGoodsListedDetail.GoodsListedDetailID == goodsListedDetailId
                         select new Vo.Goods
                         {
                             GoodsListedDetailID = tbGoodsListedDetail.GoodsListedDetailID,  //上市明细信息
                             ShoppingID = tbGoodsListedDetail.GoodsListedID,  //上市ID
                             ConverDateilID = tbGoodsListedDetail.ConverDateilID,  //配货明细ID

                             NewMonovalent = tbGoodsListedDetail.SongHuoJianShu,//送货件数
                             Sale = tbGoodsListedDetail.SongHuoXiShu,//送货细数

                             MumberOfPackages = tbGoodsListedDetail.MumberOfPackages,//上市件数
                             Subdivision = tbGoodsListedDetail.Subdivision,//上市细数
                             RetailMonovalent = tbGoodsListedDetail.ShouChuLiang,//售出量
                         }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// shenhe
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Uptcon_FountList(B_GoodsListedDetailList OK, bool state)
        {
            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {


                B_GoodsListedDetailList MyB_ConverList = new B_GoodsListedDetailList();


                MyB_ConverList.GoodsListedDetailID = OK.GoodsListedDetailID;
                MyB_ConverList.GoodsListedID = OK.GoodsListedID;
                MyB_ConverList.ConverDateilID = OK.ConverDateilID;
                MyB_ConverList.SongHuoJianShu = OK.SongHuoJianShu;
                MyB_ConverList.SongHuoXiShu = OK.SongHuoXiShu;
                MyB_ConverList.MumberOfPackages = OK.MumberOfPackages;
                MyB_ConverList.Subdivision = OK.Subdivision;
                MyB_ConverList.ShouChuLiang = OK.ShouChuLiang;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;

                if (MyModels.SaveChanges() > 0)
                {
                    if (MyB_ConverList.MumberOfPackages == 0 || MyB_ConverList.MumberOfPackages < 0)
                    {
                        B_GoodsListedDetailList wafrtbool = (from tbbool in MyModels.B_GoodsListedDetailList
                                                             where tbbool.GoodsListedDetailID == MyB_ConverList.GoodsListedDetailID
                                                             select tbbool).Single();//
                        wafrtbool.ShiFouNot = state;//购物
                        MyModels.Entry(wafrtbool).State = EntityState.Modified;

                        if (MyModels.SaveChanges() > 0)//保存
                        {
                            returnJson.State = true;
                            returnJson.Text = "修改成功";
                        }
                        else
                        {
                            returnJson.State = false;
                            returnJson.Text = "修改失败";
                        }
                    }
                    else
                    {
                        returnJson.State = false;
                        returnJson.Text = "修改失败";
                    }
                    strMag = "succsee";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// dinfdan
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Uptcon_gfdf(bool state, int ShooppingOrderFountID)
        {
            Session["ShooppingOrderFtID"] = ShooppingOrderFountID;

            ReturnJsonVo retfdurnJson = new ReturnJsonVo();
            try
            {

                B_ShooppingOrderFountList wafrtbool = (from tbbool in MyModels.B_ShooppingOrderFountList
                                                       where tbbool.ShooppingOrderFountID == ShooppingOrderFountID
                                                       select tbbool).Single();//
                wafrtbool.ExamineNot = state;
                MyModels.Entry(wafrtbool).State = EntityState.Modified;

                if (MyModels.SaveChanges() > 0)//保存
                {
                    retfdurnJson.State = true;
                    retfdurnJson.Text = "修改成功";
                }
                else
                {
                    retfdurnJson.State = false;
                    retfdurnJson.Text = "修改失败";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(retfdurnJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmptime()
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return Json(dateTimeNow, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 销售单
        /// </summary>
        /// <returns></returns>
        public ActionResult Inser_gfdf(B_ShooppingSellList OK)
        {
            string strMag = "fali";

            try
            {
                B_ShooppingSellList MyB_ConverList = new B_ShooppingSellList();

                MyB_ConverList.ShooppingOrderFountID = OK.ShooppingOrderFountID;
                MyB_ConverList.ShoopSexExamineName = OK.ShoopSexExamineName;
                MyB_ConverList.ShoopSexExamineTime = OK.ShoopSexExamineTime;

                MyModels.B_ShooppingSellList.Add(MyB_ConverList);

                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region 销售单打印
        /// <summary>
        /// 销售单打印界面
        /// </summary>
        /// <returns></returns>
        public ActionResult QianTaiXiaoShouDanDaYin()
        {
            return View();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShooppingOrderSexId()
        {

            string intGoodrmeId = Session["ShooppingOrderFtID"].ToString();
            int intUnameid = Convert.ToInt32(intGoodrmeId);

            var items = (from tbShooppingOrderFount in MyModels.B_ShooppingOrderFountList
                         join tbShooppingDateilIDOrderFoun in MyModels.B_ShooppingDateilIDOrderFounList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShooppingDateilIDOrderFoun.ShooppingOrderFountID
                         join tbShopping in MyModels.B_ShoppingList on tbShooppingDateilIDOrderFoun.ShoppingID equals tbShopping.ShoppingID

                         join tbShoppingSell in MyModels.B_ShooppingSellList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShoppingSell.ShooppingOrderFountID

                         join tbGoodsListedDetail in MyModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbGoodsListedDetail.GoodsListedDetailID
                         join tbUname in MyModels.B_Unamelist on tbShopping.UnameID equals tbUname.UnameID
                         join tbPersonalIfor in MyModels.S_PersonalIformationList on tbShopping.UnameID equals tbPersonalIfor.UnameID//个人信息

                         join tbGoodsListed in MyModels.B_GoodsListedList on tbGoodsListedDetail.GoodsListedID equals tbGoodsListed.GoodsListedID
                         join tbLeiXing in MyModels.S_HuoDongZhuanQu on tbGoodsListed.GoodsDingYiQuID equals tbLeiXing.GoodsDingYiQuID

                         join tbConverDeTail in MyModels.B_ConverDeTailList on tbGoodsListedDetail.ConverDateilID equals tbConverDeTail.ConverDeTaiID

                         join tbGoods in MyModels.B_GoodsList on tbConverDeTail.GoodsID equals tbGoods.GoodsID
                         join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                         join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID //零售单价
                         join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID //商标品牌
                         where tbShoppingSell.ShooppingOrderFountID == intUnameid
                         select new Vo.Goods
                         {

                             //订单信息
                             ShooppingNumber = tbShooppingOrderFount.ShooppingNumber,/* 订单号*/
                             ShooppingTime = tbShooppingOrderFount.ShooppingTime,  /*生成订单号时间*/
                             ReleaseTimeStrbb = tbShooppingOrderFount.ShooppingTime.ToString(),  /*生成订单号时间*/

                             ReleaseTimeStrd = tbShoppingSell.ShoopSexExamineTime.ToString(),  /*审核时间*/
                             BianJiaBianHao = tbShoppingSell.ShoopSexExamineName,  /*审核人*/
                             EveryRank = tbShooppingOrderFount.Message,//留言

                             //购物单信息
                             MumberOfPackages = tbShopping.MumberOfPackages, /*购物件数*/
                             XingQi = tbShopping.BitColer, /*购物颜色*/
                             Subdivision = tbShopping.Money, /*购物金额*/

                             //上市明细信息
                             Quantity = tbGoodsListed.ConsigneeShop,
                             ChangeWhy = tbLeiXing.GoodsDingYiQuMC,

                             //商品信息
                             GoodsID = tbGoods.GoodsID,
                             GoodsCode = tbGoods.GoodsCode, //商品编号*/
                             GoodsName = tbGoods.GoodsName,/*商品名称*/
                             EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                             GoodsChopID = tbGoodsChop.GoodsChopID,
                             GoodsChopName = tbGoodsChop.GoodsChopName,/*商标品牌*/

                             RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/

                             //用户
                             EveryRow = tbUname.uname,
                             Ordinate = tbPersonalIfor.Phone,//手机
                             Broadwise = tbPersonalIfor.Location,//地区
                             Height = tbPersonalIfor.Zipcode,//邮政编码
                             Width = tbPersonalIfor.Detailedaddress,//地址

                         }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region 已审核销售单
         /// <summary>
        /// 审核过的订单
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaXunShenHeGuoXinXi()
        {
            return View();
        }

        /// <summary>
        /// 前台销售订单查询
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="shooppingnumber"></param>
        /// <param name="shooppingTime"></param>
        /// <returns></returns>
        public ActionResult SelectShooppingOrderFount2(BsgridPage bsgridPage, string shooppingnumber, string shooppingTime)
        {
            var items = from tbShooppingOrderFount in MyModels.B_ShooppingOrderFountList
                        join tbShooppingDateilIDOrderFoun in MyModels.B_ShooppingDateilIDOrderFounList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShooppingDateilIDOrderFoun.ShooppingOrderFountID
                        join tbShopping in MyModels.B_ShoppingList on tbShooppingDateilIDOrderFoun.ShoppingID equals tbShopping.ShoppingID
                        join tbGoodsListedDetail in MyModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbGoodsListedDetail.GoodsListedDetailID

                        join tbGoodsListed in MyModels.B_GoodsListedList on tbGoodsListedDetail.GoodsListedID equals tbGoodsListed.GoodsListedID
                        join tbLeiXing in MyModels.S_HuoDongZhuanQu on tbGoodsListed.GoodsDingYiQuID equals tbLeiXing.GoodsDingYiQuID
                        join tbConverDeTail in MyModels.B_ConverDeTailList on tbGoodsListedDetail.ConverDateilID equals tbConverDeTail.ConverDeTaiID

                        join tbGoods in MyModels.B_GoodsList on tbConverDeTail.GoodsID equals tbGoods.GoodsID
                        join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                        join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID //零售单价
                        join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID //商标品牌

                        where tbShooppingOrderFount.ExamineNot == true

                        orderby tbShooppingOrderFount.ShooppingOrderFountID descending
                        select new Vo.Goods
                        {
                            //订单明细信息
                            ShooppingDateilIDOrderFounID = tbShooppingDateilIDOrderFoun.ShooppingDateilIDOrderFounID,

                            //订单信息
                            ShooppingOrderFountID = tbShooppingOrderFount.ShooppingOrderFountID,
                            ShooppingNumber = tbShooppingOrderFount.ShooppingNumber,/* 订单号*/
                            ShooppingTime = tbShooppingOrderFount.ShooppingTime,  /*生成订单号时间*/
                            ReleaseTimeSW = tbShooppingOrderFount.ShooppingTime.ToString(),  /*生成订单号时间*/
                            ExamineNot = tbShooppingOrderFount.ExamineNot,//订单审核否

                            //购物单信息
                            ShoppingID = tbShopping.ShoppingID,
                            MumberOfPackages = tbShopping.MumberOfPackages, /*购物单信息*/

                            XingQi = tbShopping.BitColer, /*购物颜色*/
                            Subdivision = tbShopping.Money, /*购物金额*/

                            //上市明细信息
                            GoodsListedDetailID = tbGoodsListedDetail.GoodsListedDetailID,

                            Quantity = tbGoodsListed.ConsigneeShop,
                            ChangeWhy = tbLeiXing.GoodsDingYiQuMC,

                            //配货明细信息
                            ConverDateilID = tbConverDeTail.ConverDeTaiID,

                            //商品信息
                            GoodsID = tbGoods.GoodsID,
                            GoodsCode = tbGoods.GoodsCode, //商品编号*/
                            GoodsName = tbGoods.GoodsName,/*商品名称*/
                            EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                            GoodsChopID = tbGoodsChop.GoodsChopID,
                            GoodsChopName = tbGoodsChop.GoodsChopName,/*商标品牌*/
                            SpecificationsModel = tbGoods.SpecificationsModel,/*规格型号*/
                            RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/

                        };
            //如果查询条件不为空
            if (!string.IsNullOrEmpty(shooppingnumber))
            {
                shooppingnumber = shooppingnumber.Trim();
                items = items.Where(p => p.ShooppingNumber.Contains(shooppingnumber));
            }
            if (!string.IsNullOrEmpty(shooppingTime))
            {
                try
                {
                    DateTime dtreleaseTime = Convert.ToDateTime(shooppingTime);
                    items = items.Where(p => p.ShooppingTime == dtreleaseTime);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
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


        public ActionResult GFDSFD(int ShooppingOrderFountID)
        {
            Session["ShooppingOrderFtID"] = ShooppingOrderFountID;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        

        #endregion












    }
}
using DaXingShangMaoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoClient.Areas.MyZiLiao.Controllers
{
    public class MyZiLiaoController : Controller
    {
        // GET: MyZiLiao/MyZiLiao
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();

        /// <summary>
        /// 我的资料信息界面
        /// </summary>
        /// <returns></returns>
        public ActionResult MyGeRenZiliao()
        {
            return View();
        }

        /// <summary>
        /// 用户
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectUaneZiLiao()
        {

            string Unameid = Session["UnameID"].ToString();
            int intUnamedid = Convert.ToInt32(Unameid);

            var listEmp = (from tbUname in myModels.B_Unamelist//用户
                           join tbDiZhi in myModels.S_PersonalIformationList on tbUname.UnameID equals tbDiZhi.UnameID
                           where tbUname.UnameID == intUnamedid
                           select new DaXingShangMaoSystem.LY.PeiHuoDan
                           {
                               Remarks=tbUname.uname,
                               GoodsDetailName = tbDiZhi.Detailedaddress,//地址

                           }).ToList();
            return Json(listEmp, JsonRequestBehavior.AllowGet);
        }

        /// 新增地址
        /// </summary>
        /// <param name="pwConsignee"></param>
        /// <returns></returns>
        public ActionResult XinZengDiZhi(S_PersonalIformationList pwConsignee)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldGoodsMoneyRuleRows = (from tbGoodsMoneyRule in myModels.S_PersonalIformationList
                                             where tbGoodsMoneyRule.Consignee == pwConsignee.Consignee
                                             select tbGoodsMoneyRule).Count();
                if (oldGoodsMoneyRuleRows == 0)
                {
                    S_PersonalIformationList JJ = new S_PersonalIformationList();
                    JJ.Location = Request.Form["Location"];
                    JJ.Detailedaddress = Request.Form["Detailedaddress"];
                    JJ.Zipcode = Request.Form["Zipcode"];
                    JJ.Consignee = Request.Form["Consignee"];
                    JJ.Phone = Request.Form["Phone"];
                    JJ.Phonecode = Request.Form["Phonecode"];

                    if (JJ.Location != null && JJ.Detailedaddress != null && JJ.Zipcode != null && JJ.Consignee != null &&
                        JJ.Phone != null && JJ.Phonecode != null)
                    {
                        myModels.S_PersonalIformationList.Add(JJ);
                        myModels.SaveChanges();
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
        /// 所有
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShooppingOrderFountId()
        {
            string Unameid = Session["UnameID"].ToString();
            int intUnamedid = Convert.ToInt32(Unameid);

            var items = (from tbShooppingOrderFount in myModels.B_ShooppingOrderFountList
                         join tbShooppingDateilIDOrderFoun in myModels.B_ShooppingDateilIDOrderFounList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShooppingDateilIDOrderFoun.ShooppingOrderFountID
                         join tbShopping in myModels.B_ShoppingList on tbShooppingDateilIDOrderFoun.ShoppingID equals tbShopping.ShoppingID
                         join tbGoodsListedDetail in myModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbGoodsListedDetail.GoodsListedDetailID
                         join tbUname in myModels.B_Unamelist on tbShopping.UnameID equals tbUname.UnameID
                         join tbPersonalIfor in myModels.S_PersonalIformationList on tbShopping.UnameID equals tbPersonalIfor.UnameID//个人信息

                         join tbGoodsListed in myModels.B_GoodsListedList on tbGoodsListedDetail.GoodsListedID equals tbGoodsListed.GoodsListedID
                         join tbConverDeTail in myModels.B_ConverDeTailList on tbGoodsListedDetail.ConverDateilID equals tbConverDeTail.ConverDeTaiID

                         join tbGoods in myModels.B_GoodsList on tbConverDeTail.GoodsID equals tbGoods.GoodsID
                         join tbEstimateUnit in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID //零售单价
                         join tbGoodsChop in myModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID //商标品牌
                         where tbUname.UnameID == intUnamedid
                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {

                             WareHouseID = tbShopping.ShoppingID,
                             GoodsListedID = tbGoodsListedDetail.GoodsListedID,
                             GoodsListedDetailID = tbGoodsListedDetail.GoodsListedDetailID,//上市明细   
                             ConverDeTaiID = tbConverDeTail.ConverDeTaiID,
                             RetureFactoryID = tbConverDeTail.GoodsID,
                             GoodsName = tbGoods.GoodsName,//商品名称
                             GoodsDetailName = tbGoodsDetail.GoodsDetailName,//商品明细名称
                             StockPlaceName = tbShopping.BitColer,//购物颜色
                             Subdivision = tbShopping.Money,//购物金额
                             MumberOfPackages = tbShopping.MumberOfPackages,//购物件数

                             GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                             TaxBid = tbGoodsDetail.TaxBid,//价格

                         }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 待发 weida
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShooppinhgfrFountId()
        {
            string Unameid = Session["UnameID"].ToString();
            int intUnamedid = Convert.ToInt32(Unameid);

            var items = (from tbShooppingOrderFount in myModels.B_ShooppingOrderFountList
                         join tbShooppingDateilIDOrderFoun in myModels.B_ShooppingDateilIDOrderFounList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShooppingDateilIDOrderFoun.ShooppingOrderFountID
                         join tbShopping in myModels.B_ShoppingList on tbShooppingDateilIDOrderFoun.ShoppingID equals tbShopping.ShoppingID
                         join tbGoodsListedDetail in myModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbGoodsListedDetail.GoodsListedDetailID
                         join tbUname in myModels.B_Unamelist on tbShopping.UnameID equals tbUname.UnameID
                         join tbPersonalIfor in myModels.S_PersonalIformationList on tbShopping.UnameID equals tbPersonalIfor.UnameID//个人信息

                         join tbGoodsListed in myModels.B_GoodsListedList on tbGoodsListedDetail.GoodsListedID equals tbGoodsListed.GoodsListedID
                         join tbConverDeTail in myModels.B_ConverDeTailList on tbGoodsListedDetail.ConverDateilID equals tbConverDeTail.ConverDeTaiID

                         join tbGoods in myModels.B_GoodsList on tbConverDeTail.GoodsID equals tbGoods.GoodsID
                         join tbEstimateUnit in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID //零售单价
                         join tbGoodsChop in myModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID //商标品牌
                         where tbUname.UnameID == intUnamedid && tbShooppingOrderFount.ExamineNot == false
                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {

                             WareHouseID = tbShopping.ShoppingID,
                             GoodsListedID = tbGoodsListedDetail.GoodsListedID,
                             GoodsListedDetailID = tbGoodsListedDetail.GoodsListedDetailID,//上市明细   
                             ConverDeTaiID = tbConverDeTail.ConverDeTaiID,
                             RetureFactoryID = tbConverDeTail.GoodsID,
                             GoodsName = tbGoods.GoodsName,//商品名称
                             GoodsDetailName = tbGoodsDetail.GoodsDetailName,//商品明细名称
                             StockPlaceName = tbShopping.BitColer,//购物颜色
                             Subdivision = tbShopping.Money,//购物金额
                             MumberOfPackages = tbShopping.MumberOfPackages,//购物件数

                             GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                             TaxBid = tbGoodsDetail.TaxBid,//价格

                         }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 代收
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSDaiShouuntId()
        {
            string Unameid = Session["UnameID"].ToString();
            int intUnamedid = Convert.ToInt32(Unameid);

            var items = (from tbShooppingOrderFount in myModels.B_ShooppingOrderFountList
                         join tbShooppingDateilIDOrderFoun in myModels.B_ShooppingDateilIDOrderFounList on tbShooppingOrderFount.ShooppingOrderFountID equals tbShooppingDateilIDOrderFoun.ShooppingOrderFountID
                         join tbShopping in myModels.B_ShoppingList on tbShooppingDateilIDOrderFoun.ShoppingID equals tbShopping.ShoppingID
                         join tbGoodsListedDetail in myModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbGoodsListedDetail.GoodsListedDetailID
                         join tbUname in myModels.B_Unamelist on tbShopping.UnameID equals tbUname.UnameID
                         join tbPersonalIfor in myModels.S_PersonalIformationList on tbShopping.UnameID equals tbPersonalIfor.UnameID//个人信息

                         join tbGoodsListed in myModels.B_GoodsListedList on tbGoodsListedDetail.GoodsListedID equals tbGoodsListed.GoodsListedID
                         join tbConverDeTail in myModels.B_ConverDeTailList on tbGoodsListedDetail.ConverDateilID equals tbConverDeTail.ConverDeTaiID

                         join tbGoods in myModels.B_GoodsList on tbConverDeTail.GoodsID equals tbGoods.GoodsID
                         join tbEstimateUnit in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID //零售单价
                         join tbGoodsChop in myModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID //商标品牌
                         where tbUname.UnameID == intUnamedid && tbShooppingOrderFount.ExamineNot == true
                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {

                             WareHouseID = tbShopping.ShoppingID,
                             GoodsListedID = tbGoodsListedDetail.GoodsListedID,
                             GoodsListedDetailID = tbGoodsListedDetail.GoodsListedDetailID,//上市明细   
                             ConverDeTaiID = tbConverDeTail.ConverDeTaiID,
                             RetureFactoryID = tbConverDeTail.GoodsID,
                             GoodsName = tbGoods.GoodsName,//商品名称
                             GoodsDetailName = tbGoodsDetail.GoodsDetailName,//商品明细名称
                             StockPlaceName = tbShopping.BitColer,//购物颜色
                             Subdivision = tbShopping.Money,//购物金额
                             MumberOfPackages = tbShopping.MumberOfPackages,//购物件数

                             GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                             TaxBid = tbGoodsDetail.TaxBid,//价格

                         }).ToList();
            return Json(items, JsonRequestBehavior.AllowGet);
        }

        

    }
}
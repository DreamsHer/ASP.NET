using DaXingShangMaoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoClient.Areas.YongHuZhuJieMian.Controllers
{
    public class GoodesUILController : Controller
    {
        // GET: YongHuZhuJieMian/GoodesUIL

        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();


        //一界面控制器（部分）

        /// <summary>
        ///1、购物车界面 (电器)
        /// </summary>
        /// <returns></returns>
        public ActionResult DanYiShangPinJieMian()
        {
            return View();
        }

        /// <summary>
        /// 2、购物车界面（衣服）
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart()
        {
            return View();
        }

        /// <summary>
        /// 3、购物车界面 （食品）
        /// </summary>
        /// <returns></returns>
        public ActionResult ShiPinJieMian()
        {
            return View();
        }



        //二代码（部分）


        //1、电器代码层
        #region MyRegion
        //docqmoen
        public ActionResult SelectGoodeseilId()
        {
            if (Session["CheckRermeber"] != null)
            {
                string CheckRermeberId = Session["CheckRermeber"].ToString();//id
                int intGoodrmeId = Convert.ToInt32(CheckRermeberId);

                var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                             join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                             join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                             join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                             join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                             //join tbJiLiang in myModels.S_PackInfoIDList on tbGoodsDetail.PackInfoID equals tbJiLiang.PackInfoID
                             where tbSectGoo.GoodsListedID == intGoodrmeId

                             select new DaXingShangMaoSystem.LY.PeiHuoDan
                             {
                                 GoodsListedID = tbShangShiDateil.GoodsListedID,
                                 WareHouseID = tbShangShiDateil.GoodsListedDetailID,
                                 GoodsIDs = tbGoods.GoodsID,
                                 GoodsName = tbGoods.GoodsName,
                                 Remember = tbSectGoo.ConsigneeShop,
                                 TaxBid = tbGoodsDetail.TaxBid,
                                 Remarks = tbGoods.ProducingArea,
                                 GoodsDetailName = tbGoodsDetail.GoodsDetailName,
                                 //QiDongName = tbJiLiang.PackWeight,

                             }).ToList();
                return Json(linqt, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <returns></returns>
        public ActionResult AddGouCatr(B_ShoppingList DuDao)
        {
            string StrinM = "fail";
            B_ShoppingList KK = new B_ShoppingList();

            KK.UnameID = DuDao.UnameID;
            KK.GoodsListedDetailID = DuDao.GoodsListedDetailID;
            KK.Money = DuDao.Money;
            KK.BitColer = DuDao.BitColer;
            KK.MumberOfPackages = DuDao.MumberOfPackages;

            if (KK.UnameID != null && KK.GoodsListedDetailID != null && KK.Money != null && KK.BitColer != null)
            {
                myModels.B_ShoppingList.Add(KK);
                myModels.SaveChanges();

                StrinM = "success";
                return Json(StrinM, JsonRequestBehavior.AllowGet);
            }
            return Json(StrinM,JsonRequestBehavior.AllowGet);
        }


        #endregion




    }
}                                                 
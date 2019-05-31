using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DaJianUIL.Controllers
{
    public class ZhuMimeController : Controller
    {
        // GET: DaJianUIL/ZhuMime
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();

        /// <summary>
        /// 主界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Mian()
        {
            return View();
        }


        public ActionResult ZhuMian()
        {
            return View();
        }


        /// <summary>
        /// 购物车界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingCart()
        {
            return View();
        }


        //


        /// <summary>
        /// 上市(活动)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodeseilt()
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                         join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                         join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                         join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                         join tbJiLiang in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiang.EstimateUnitID
                         join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbSectGoo.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID

                         join tbGoodsDateil in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDateil.GoodsID

                         where tbSectGoo.ExamineNot == true && tbSectGoo.AbateNot == false

                         select new LY.PeiHuoDan
                         {
                             converIDs=tbSectGoo.GoodsListedID,
                             GoodsIDs = tbGoods.GoodsID,
                             GoodsName = tbGoods.GoodsName,
                             TaxBid = tbGoodsDetail.TaxBid,
                             Remarks=tbJiLiang.EstimateUnitName,
                             Remember = tbGoodsDateil.GoodsDetailName,

                         }).ToList();

            return Json(linqt, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 原标
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodeseiltYuan()
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                         join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                         join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                         join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                         join tbJiLiang in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiang.EstimateUnitID
                         join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbSectGoo.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                         where tbSectGoo.ExamineNot == true && tbSectGoo.AbateNot == false

                         select new LY.PeiHuoDan
                         {
                             converIDs = tbSectGoo.GoodsListedID,
                             GoodsIDs = tbGoods.GoodsID,
                             GoodsName = tbGoods.GoodsName,
                             TaxBid = tbGoodsDetail.TaxBid,
                             Remarks = tbJiLiang.EstimateUnitName,

                         }).ToList();

            return Json(linqt, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult SehglectGoodeseilt()
        //{
        //    var gfd = 4;
        //    var C = 0;
        //    byte[] imageData=null;
        //    if (C< gfd)
        //    {
        //        C = C + 1;

        //        var linqt = (from tbSectGoo in myModels.B_GoodsListedList
        //                     join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
        //                     join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
        //                     join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
        //                     where tbSectGoo.ExamineNot == true && tbSectGoo.AbateNot == false
        //                     select new
        //                     {
        //                         tbGoods.GoodsPicture,
        //                         tbSectGoo.GoodsListedID

        //                     }).ToList();
        //       imageData = linqt.GoodsPicture;
        //    }
        //    return File(imageData, @"image/jpg");
        //}



        public ActionResult SelectGoffodeseilt(int goodsListedID)
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                         join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                         join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                         join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                         where tbSectGoo.ExamineNot == true && tbSectGoo.AbateNot == false && tbSectGoo.GoodsListedID== goodsListedID
                         select new
                         {
                             tbGoods.GoodsPicture

                         }).Single();
            byte[] imageData = linqt.GoodsPicture;
            return File(imageData, @"image/jpg");
        }





    }
}
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;



namespace DaXingShangMaoClient.Controllers
{
    public class ZhuMianController : Controller
    {
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: ZhuMian
        public ActionResult ZhuMian()
        {
            return View();
        }

        //
        public ActionResult HuoQuUnNameID()
        {
            if (Session["UnameID"] != null)
            {
                string Unameid = Session["UnameID"].ToString();
                int intUnameid = Convert.ToInt32(Unameid);
                return Json(intUnameid, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }


        //清Session
        //public ActionResult SetClear()
        //{
        //    Session.Clear();
        //    return Json(true, JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// 上市(chaoshi)
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
                         where tbSectGoo.ExamineNot == true && tbSectGoo.StoopSellNot == false && tbSectGoo.AbateNot == false && tbSectGoo.GoodsDingYiQuID==2
                       
                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {
                             converIDs = tbSectGoo.GoodsListedID,

                             GoodsIDs = tbGoods.GoodsID,
                             GoodsName = tbGoods.GoodsName,
                             GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                             TaxBid = tbGoodsDetail.TaxBid,
                             Remarks = tbJiLiang.EstimateUnitName,
                             Remember = tbGoodsDateil.GoodsDetailName,

                         }).ToList();

            return Json(linqt, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 上市(liang)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodeseiltliang()
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                         join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                         join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                         join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                         join tbJiLiang in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiang.EstimateUnitID
                         join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbSectGoo.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                         join tbGoodsDateil in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDateil.GoodsID
                         where tbSectGoo.ExamineNot == true && tbSectGoo.StoopSellNot == false && tbSectGoo.AbateNot == false && tbSectGoo.GoodsDingYiQuID == 3

                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {
                             converIDs = tbSectGoo.GoodsListedID,
                             GoodsIDs = tbGoods.GoodsID,
                             GoodsName = tbGoods.GoodsName,
                             GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                             TaxBid = tbGoodsDetail.TaxBid,
                             Remarks = tbJiLiang.EstimateUnitName,
                             Remember = tbGoodsDateil.GoodsDetailName,

                         }).ToList();

            return Json(linqt, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 上市(衣服girl)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodeseiltYiFu()
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                         join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                         join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                         join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                         join tbJiLiang in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiang.EstimateUnitID
                         join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbSectGoo.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                         join tbGoodsDateil in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDateil.GoodsID

                         join tbQuFen in myModels.S_ApplyTargetList on tbGoods.ApplyTargetID equals tbQuFen.ApplyTargetID

                         where tbSectGoo.ExamineNot == true && tbSectGoo.StoopSellNot == false && tbSectGoo.AbateNot == false && tbSectGoo.GoodsDingYiQuID == 4&&
                         tbQuFen.ApplyTargetID==3

                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {
                             converIDs = tbSectGoo.GoodsListedID,
                             GoodsIDs = tbGoods.GoodsID,
                             GoodsName = tbGoods.GoodsName,
                             GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                             TaxBid = tbGoodsDetail.TaxBid,
                             Remarks = tbJiLiang.EstimateUnitName,
                             Remember = tbGoodsDateil.GoodsDetailName,

                         }).ToList();

            return Json(linqt, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 上市(衣服boy)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodeseiltYiFuboy()
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                         join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                         join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                         join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                         join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                         join tbJiLiang in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiang.EstimateUnitID
                         join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbSectGoo.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                         join tbGoodsDateil in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDateil.GoodsID

                         join tbQuFen in myModels.S_ApplyTargetList on tbGoods.ApplyTargetID equals tbQuFen.ApplyTargetID

                         where tbSectGoo.ExamineNot == true && tbSectGoo.StoopSellNot == false && tbSectGoo.AbateNot == false && tbSectGoo.GoodsDingYiQuID == 4 &&
                         tbQuFen.ApplyTargetID == 2

                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {
                             converIDs = tbSectGoo.GoodsListedID,
                             GoodsIDs = tbGoods.GoodsID,
                             GoodsName = tbGoods.GoodsName,
                             GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                             TaxBid = tbGoodsDetail.TaxBid,
                             Remarks = tbJiLiang.EstimateUnitName,
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

                         select new DaXingShangMaoSystem.LY.PeiHuoDan
                         {
                             converIDs = tbSectGoo.GoodsListedID,
                             GoodsIDs = tbGoods.GoodsID,
                             GoodsName = tbGoods.GoodsName,
                             TaxBid = tbGoodsDetail.TaxBid,
                             Remarks = tbJiLiang.EstimateUnitName,

                         }).ToList();

            return Json(linqt, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SelectGoffodeseilt(int goodsListedID)
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                         join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                         join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                         join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                         where tbSectGoo.ExamineNot == true && tbSectGoo.AbateNot == false && tbSectGoo.GoodsListedID == goodsListedID
                         select new
                         {
                             tbGoods.GoodsPicture

                         }).Single();
            byte[] imageData = linqt.GoodsPicture;
            return File(imageData, @"image/jpg");
        }


        public ActionResult hanlectCheckgh(int checkRermeberId)
        {
            var linq = (from tbGoodBiao in myModels.B_GoodsListedList
                        where tbGoodBiao.GoodsListedID == checkRermeberId
                        select new DaXingShangMaoSystem.LY.PeiHuoDan
                        {
                            converIDs = tbGoodBiao.GoodsListedID,
                        }).ToList();
            Session["CheckRermeber"] = linq[0].converIDs; 
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        //搜部分
        public ActionResult SouSuoKuang(string ShuRuZhi)
        {
            var linq = (from tbGoodBiao in myModels.B_GoodsClassifyList
                        where tbGoodBiao.GoodsClassifyName == ShuRuZhi
                        select new DaXingShangMaoSystem.LY.PeiHuoDan
                        {
                            Remarks = tbGoodBiao.GoodsClassifyName//商品名
                        }).ToList();
            if (linq.Count > 0)
            {
                Session["LeiBie"] = linq[0].Remarks;
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            return Json("fail", JsonRequestBehavior.AllowGet);

        }


    }
}
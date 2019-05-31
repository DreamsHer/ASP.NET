using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi.Controllers
{
    public class DaYinBuMenBiaoController : Controller
    {
        // GET: GoodsZhangGuanLi/DaYinBuMenBiao
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult BuMneJingYing()
        {
            return View();
        }

        /// <summary>
        /// 下拉框（盘点部门）一
        /// </summary>
        /// <returns></returns>
        public ActionResult CaiGouXiaLa()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.B_PanlList
                                                
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.DrugTypeID,
                                                     text = tb.DrugType
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 下拉框（盘点部门）二
        /// </summary>
        /// <returns></returns>
        public ActionResult BuMen(int DrugTypeID)
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.B_PanlList
                                                 where tb.F_DrugTypeID== DrugTypeID
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.DrugTypeID,
                                                     text = tb.DrugType
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 下拉框（配送部门）一
        /// </summary>
        /// <returns></returns>
        public ActionResult PeiSong()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_SpouseBRanchList
                                                 where tb.SpouseBRanchID==1 || tb.SpouseBRanchID ==2
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.SpouseBRanchID,
                                                     text = tb.SpouseBRanchName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 下拉框（配送部门）二
        /// </summary>
        /// <returns></returns>
        public ActionResult PeiSonger()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_StockPlaceList
                                                 where tb.StockPlaceID == 1 || tb.StockPlaceID == 2|| tb.StockPlaceID == 3
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.StockPlaceID,
                                                     text = tb.StockPlaceName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 下拉框（）一
        /// </summary>
        /// <returns></returns>
        public ActionResult PeiSongd()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_SpouseBRanchList
                                                 where tb.SpouseBRanchID == 3 || tb.SpouseBRanchID == 4
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.SpouseBRanchID,
                                                     text = tb.SpouseBRanchName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 下拉框（配送部门）二
        /// </summary>
        /// <returns></returns>
        public ActionResult PeiSongdser()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_StockPlaceList
                                                 where tb.StockPlaceID == 4 || tb.StockPlaceID == 5
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.StockPlaceID,
                                                     text = tb.StockPlaceName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }
    }
}
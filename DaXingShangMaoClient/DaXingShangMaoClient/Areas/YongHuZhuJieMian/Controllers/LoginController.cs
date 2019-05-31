using DaXingShangMaoClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoClient.Areas.YongHuZhuJieMian.Controllers
{
    public class LoginController : Controller
    {
        // GET: YongHuZhuJieMian/Login
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();

        /// <summary>
        ///登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }


        /// <summary>
        /// 商品分类（部分界面）
        /// </summary>
        /// <returns></returns>
        public ActionResult BuFenJieMian()
        {
            return View();
        }



        //二 代码

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ok"></param>
        /// <returns></returns>
        public ActionResult UserLogin(B_Unamelist Ok)
        {
            string strMsg = "fail";
            B_Unamelist MyUL = new B_Unamelist();
            MyUL.uname = Ok.uname;
            MyUL.pwd = Ok.pwd;

            var dbStaff = (from tbStaff in myModels.B_Unamelist
                           where tbStaff.uname == MyUL.uname
                           && tbStaff.pwd == MyUL.pwd||tbStaff.phone == MyUL.uname && tbStaff.pwd == MyUL.pwd
                           select new DaXingShangMaoSystem.LY.PeiHuoDan
                           {
                               UnameID = tbStaff.UnameID,
                               QiDongName=tbStaff.uname,
                               Remarks=tbStaff.pwd,
                               P_Remember=tbStaff.phone
                           }).ToList();
            
            if (dbStaff.Count>0)
            {
                Session["UnameID"] = dbStaff[0].UnameID;
                strMsg = "login";
            }
            else
            {
                strMsg = "UnNimeHuoPawssFail";
            }

            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SelectGoodeseiltliang()
        {
            if (Session["LeiBie"] != null)
            {
                string genLei = Session["LeiBie"].ToString();

                var linqt = (from tbSectGoo in myModels.B_GoodsListedList
                             join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbSectGoo.GoodsListedID equals tbShangShiDateil.GoodsListedID
                             join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                             join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                             join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                             join tbJiLiang in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiang.EstimateUnitID
                             join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbSectGoo.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                             join tbGoodsClassifyL in myModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodsClassifyL.GoodsClassifyID
                             where tbSectGoo.ExamineNot == true && tbSectGoo.StoopSellNot == false && tbSectGoo.AbateNot == false&& tbGoodsClassifyL.GoodsClassifyName == genLei

                             select new DaXingShangMaoSystem.LY.PeiHuoDan
                             {
                                 converIDs = tbSectGoo.GoodsListedID,
                                 GoodsIDs = tbGoods.GoodsID,
                                 GoodsName = tbGoods.GoodsName,
                                 GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                                 TaxBid = tbGoodsDetail.TaxBid,
                                 Remarks = tbJiLiang.EstimateUnitName,
                                 Remember = tbGoodsDetail.GoodsDetailName,
                                

                             }).ToList();

                return Json(linqt, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);

        }

    }
}
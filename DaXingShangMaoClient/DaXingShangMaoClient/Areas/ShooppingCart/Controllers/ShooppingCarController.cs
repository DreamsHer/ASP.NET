using DaXingShangMaoClient.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoClient.Areas.ShooppingCart.Controllers
{
    public class ShooppingCarController : Controller
    {
        // GET: ShooppingCart/ShooppingCar
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();

        /// <summary>
        /// 购物车界面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoopingCarUIl()
        {
            return View();
        }


        /// <summary>
        /// 结算订单界面
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderFormCarUIl()
        {
            return View();
        }


        /// <summary>
        /// 支付订单界面
        /// </summary>
        /// <returns></returns>
        public ActionResult TiaoJiaoDingDan()
        {
            return View();
        }




        //二：购物车 代码
        #region
        /// <summary>
        ///查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShooppingCart()
        {
            string CheckRermeberId = Session["UnameID"].ToString();//id
            int intGoodrmeId = Convert.ToInt32(CheckRermeberId);

            var listEmp = (from tbUname in myModels.B_Unamelist
                           join tbShopping in myModels.B_ShoppingList on tbUname.UnameID equals tbShopping.UnameID
                           join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbShangShiDateil.GoodsListedDetailID
                           join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                           join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                           join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                           where tbShopping.UnameID == intGoodrmeId && tbShopping.SubmitNot==false && tbShopping.AffirmNot==false
                           select new DaXingShangMaoSystem.LY.PeiHuoDan
                           {
                               WareHouseID = tbShopping.ShoppingID,
                               GoodsListedID = tbShangShiDateil.GoodsListedID,
                               GoodsListedDetailID = tbShangShiDateil.GoodsListedDetailID,//上市明细
                               ConverDeTaiID = tbConverDateil.ConverDeTaiID,
                               RetureFactoryID = tbConverDateil.GoodsID,
                               GoodsName = tbGoods.GoodsName,//商品名称
                               GoodsDetailName = tbGoodsDetail.GoodsDetailName,//商品明细名称
                               StockPlaceName = tbShopping.BitColer,//购物颜色
                               Subdivision = tbShopping.Money,//购物金额
                               MumberOfPackages = tbShopping.MumberOfPackages,//购物件数

                               GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                               TaxBid = tbGoodsDetail.TaxBid,//价格


                           }).ToList();
            return Json(listEmp, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SelectGoffodeseilt(int goodsListedID)
        {
            var linqt = (from tbSectGoo in myModels.B_GoodsList
                         
                         where tbSectGoo.GoodsID == goodsListedID
                         select new
                         {
                             tbSectGoo.GoodsPicture

                         }).Single();
            byte[] imageData = linqt.GoodsPicture;
            return File(imageData, @"image/jpg");
        }

        /// <summary>
        ///移除 
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteShooppingCart(int GouCarId)
        {
            string strMsg = "fail";
            B_ShoppingList WarHouser = (from tbShopping in myModels.B_ShoppingList
                                        where tbShopping.ShoppingID == GouCarId
                                        select tbShopping).Single();
            myModels.B_ShoppingList.Remove(WarHouser);
            myModels.SaveChanges();
            strMsg = "success";

            return Json(strMsg, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 传结算数据Id
        /// </summary>
        /// <param name="GouCarId"></param>
        /// <returns></returns>
        public ActionResult JieSuanShooppingCart(Array ShuZuGouXuanId)
        {
            string Z = ((string[])ShuZuGouXuanId)[0];
            int intsid = Convert.ToInt32(Z);

            Session["JieSuanShooppinghg"] = intsid;

            return Json("", JsonRequestBehavior.AllowGet);

        }

        #endregion



        //三:结算界面（代码）
        #region

        //public ActionResult ChuangShuZu()
        //{
        //    List<string> TiMuShuZu = Session["JieSuanShooppin"] as List<string>;

        //    return Json(TiMuShuZu, JsonRequestBehavior.AllowGet);
        //}


        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectJieSuanShooppingCart()
        {

            string intGoodrmeId = Session["JieSuanShooppinghg"].ToString();//id
            int intUnameid = Convert.ToInt32(intGoodrmeId);

            var listEmp = (from tbUname in myModels.B_Unamelist
                           join tbShopping in myModels.B_ShoppingList on tbUname.UnameID equals tbShopping.UnameID
                           join tbShangShiDateil in myModels.B_GoodsListedDetailList on tbShopping.GoodsListedDetailID equals tbShangShiDateil.GoodsListedDetailID
                           join tbConverDateil in myModels.B_ConverDeTailList on tbShangShiDateil.ConverDateilID equals tbConverDateil.ConverDeTaiID
                           join tbGoods in myModels.B_GoodsList on tbConverDateil.GoodsID equals tbGoods.GoodsID
                           join tbGoodsDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                           where tbShopping.ShoppingID == intUnameid
                           select new DaXingShangMaoSystem.LY.PeiHuoDan
                           {
                               WareHouseID = tbShopping.ShoppingID,
                               GoodsListedID = tbShangShiDateil.GoodsListedID,
                               GoodsListedDetailID = tbShangShiDateil.GoodsListedDetailID,//上市明细
                               ConverDeTaiID = tbConverDateil.ConverDeTaiID,
                               RetureFactoryID = tbConverDateil.GoodsID,
                               GoodsName = tbGoods.GoodsName,//商品名称
                               GoodsDetailName = tbGoodsDetail.GoodsDetailName,//商品明细名称
                               StockPlaceName = tbShopping.BitColer,//购物颜色
                               Subdivision = tbShopping.Money,//购物金额
                               MumberOfPackages = tbShopping.MumberOfPackages,//购物件数

                               GoodsClassifyID = tbGoods.GoodsClassifyID,//类
                               TaxBid = tbGoodsDetail.TaxBid,//价格


                           }).ToList();
            return Json(listEmp, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 地址
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
                               ExamineName=tbUname.uname,
                               GoodsDetailName = tbDiZhi.Detailedaddress,//地址

                           }).ToList();
            return Json(listEmp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCodef()
        {
            string Std = "";
            var listy = (from tbem in myModels.B_ShooppingOrderFountList
                         orderby tbem.ShooppingNumber
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_ShooppingOrderFountList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.ShooppingNumber.Substring(2, 8));
                inemp++;
                Std = inemp.ToString();
                for (int i = 0; i < 8; i++)
                {
                    Std = Std.Length < 8 ? "0" + Std : Std;
                }
                Std = "SP" + Std;
            }
            else
            {
                Std = "SP00000001";
            }
            return Json(Std, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 时间
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmptime()
        {
            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return Json(dateTimeNow, JsonRequestBehavior.AllowGet);
        }
       


        /// <summary>
        /// 、
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Insect_ShooppingOrderFountList(B_ShooppingOrderFountList OK,int ShoppingID, bool state)
        {
            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                //
                int oldWareHouseRows = (from tb in myModels.B_ShooppingOrderFountList
                                        where tb.ShooppingNumber == OK.ShooppingNumber
                                        select tb).Count();

                if (oldWareHouseRows == 0)
                {
                    B_ShooppingOrderFountList MyB_ConverList = new B_ShooppingOrderFountList();


                    MyB_ConverList.ShooppingNumber = OK.ShooppingNumber;
                    MyB_ConverList.ShooppingTime = OK.ShooppingTime;
                    MyB_ConverList.Message = OK.Message;

                    myModels.B_ShooppingOrderFountList.Add(MyB_ConverList);

                    if (myModels.SaveChanges() > 0)
                    {
                        B_ShoppingList wafrtbool = (from tbbool in myModels.B_ShoppingList
                                                     where tbbool.ShoppingID == ShoppingID
                                                     select tbbool).Single();//
                        wafrtbool.SubmitNot = state;//购物
                        myModels.Entry(wafrtbool).State = EntityState.Modified;

                        if (myModels.SaveChanges() > 0)//保存
                        {
                            returnJson.State = true;
                            returnJson.Text = "修改成功";
                        }
                        else
                        {
                            returnJson.State = false;
                            returnJson.Text = "修改失败";
                        }

                        strMag = "succsee";
                        B_ShooppingDateilIDOrderFounList ShooppingDeTailList = new B_ShooppingDateilIDOrderFounList();

                        ShooppingDeTailList.ShooppingOrderFountID = MyB_ConverList.ShooppingOrderFountID;//定单ID
                        ShooppingDeTailList.ShoppingID = ShoppingID;//ShoppingID
                        myModels.B_ShooppingDateilIDOrderFounList.Add(ShooppingDeTailList);
                        myModels.SaveChanges();


                    }


                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag , returnJson }, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}
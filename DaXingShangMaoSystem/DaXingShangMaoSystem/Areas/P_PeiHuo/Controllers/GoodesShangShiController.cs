using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.P_PeiHuo.Controllers
{
    public class GoodesShangShiController : Controller
    {
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: P_PeiHuo/GoodesShangShi

        /// <summary>
        /// 商品上市单
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodesShangShi()
        {
            return View();
        }



        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCode()
        {
            string Std = "";
            var listy = (from tbem in myModels.B_GoodsListedList
                         orderby tbem.ListedNumber
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_GoodsListedList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.ListedNumber.Substring(1, 8));
                inemp++;
                Std = inemp.ToString();
                for (int i = 0; i < 8; i++)
                {
                    Std = Std.Length < 8 ? "0" + Std : Std;
                }
            }
            else
            {
                Std = "00000001";
            }
            return Json(Std, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 售卖优惠
        /// </summary>
        /// <returns></returns>
        public ActionResult ShouMaiYouHui()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_HuoDongZhuanQu
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.GoodsDingYiQuID,
                                                     text = tb.GoodsDingYiQuMC
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectConverList(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbConverlist in myModels.B_ConverList
                        join tbFaHuoBuMen in myModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in myModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID

                        join tbWanHour in myModels.B_WareHouseList on tbConverlist.WareHouseID equals tbWanHour.WareHouseID
                        join tbOrerHovg in myModels.B_OrderFormPactList on tbWanHour.OrderFormPactID equals tbOrerHovg.OrderFormPactID
                        join tbGongHuo in myModels.B_SupplierList on tbOrerHovg.SupplierID equals tbGongHuo.SupplierID

                        where tbConverlist.ExamineNot == true && tbConverlist.HaveNot==false
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            P_Remember = tbConverlist.P_Remember,//编号
                            payName = tbConverlist.payName,
                            furlName = tbConverlist.furlName,
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,
                            StockPlaceName = tbShouHuoBuMen.StockPlaceName,
                            Remarks = tbConverlist.Remarks,
                            QiDongName=tbGongHuo.SupplierCHName,
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.ConverID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 绑定配货单（1）
        /// </summary>
        public ActionResult BangConverList(int converID)
        {
            var linq = (from tbConverlist in myModels.B_ConverList
                        join tbFaHuoBuMen in myModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in myModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID
                        where tbConverlist.ConverID == converID
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            StockPlaceID = tbConverlist.StockPlaceID,//编号
                        
                        }).ToList();

           
            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 根据Id查询 商品(2)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShangPinZhuf(Vo.BsgridPage bsgridPage, int converID)
        {
            var linq = (from tbWanHofDeaill in myModels.B_ConverDeTailList//配货明细
                        join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细

                        where tbWanHofDeaill.ConverID == converID && tbWanHofDeaill.YiKongNot==false//根据配货明细中的“配货ID”
                        select new LY.PeiHuoDan
                        {
                            ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                            WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID

                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//配货件数
                            Subdivision = tbWanHofDeaill.Subdivision,//配货细数
                            YuanMumberOfPack = tbWanHofDeaill.YuanMumberOfPack,//原（件数）
                            MumberOfPackagesDing = tbWanHofDeaill.YuanSubdivision,//原（细数）
                            Number = tbWanHofDeaill.Number,//批次号

                            GoodsIDs = tbShangPin.GoodsID,//商品ID
                            GoodsCodes = tbShangPin.GoodsCode,//商品代码
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            ArtNos = tbShangPin.ArtNo,//商品货号

                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

                        }).ToList();

            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.GoodsIDs).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }



        /// <summary>
        /// 绑定商品到（主界面）(3)
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, Array jiLuChaekID)
        {
            List<PeiHuoDan> list = new List<PeiHuoDan>();
            // int goodsIDs
            string Z = ((string[])jiLuChaekID)[0];
            string[] intsZ = Z.Split(',');

            for (int P = 0; P < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[P]);
                var linq = (from tbWanHofDeaill in myModels.B_ConverDeTailList//配货明细
                            join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细

                            where tbWanHofDeaill.ConverDeTaiID == goodsIDs
                            select new LY.PeiHuoDan
                            {
                                ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                                WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID

                                MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//配货件数
                                Subdivision = tbWanHofDeaill.Subdivision,//配货细数
                                YuanMumberOfPack = tbWanHofDeaill.YuanMumberOfPack,//原（件数）
                                MumberOfPackagesDing = tbWanHofDeaill.YuanSubdivision,//原（细数）
                                Number = tbWanHofDeaill.Number,//批次号

                                GoodsIDs = tbShangPin.GoodsID,//商品ID
                                GoodsCodes = tbShangPin.GoodsCode,//商品代码
                                GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                GoodsNames = tbShangPin.GoodsName,//商品名称
                                ArtNos = tbShangPin.ArtNo,//商品货号

                                SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                TaxBids = tbGoodDetail.TaxBid,//含税进价
                                RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

                            }).ToList();
                list.AddRange(linq);
                P++;
            }
            int totalRow = list.Count();
            List<LY.PeiHuoDan> notices = list.OrderByDescending(p => p.ConverDeTaiID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        //附属查询（配货）
        public ActionResult PeiHuoDeMingXi(int converDeTaiID)
        {
            var Linq = (from tbConverDeil in myModels.B_ConverDeTailList
                        where tbConverDeil.ConverDeTaiID == converDeTaiID
                        select new LY.PeiHuoDan
                        {
                            ConverDeTaiID = tbConverDeil.ConverDeTaiID,
                            converIDs = tbConverDeil.ConverID,
                            GoodsIDs = tbConverDeil.GoodsID,
                            WareHouseDetiailID = tbConverDeil.WareHouseDetiailID,
                            MumberOfPackages = tbConverDeil.MumberOfPackages,
                            Subdivision = tbConverDeil.Subdivision,

                            Number = tbConverDeil.Number,
                            YuanMumberOfPack = tbConverDeil.YuanMumberOfPack,
                            YuanSubdivision = tbConverDeil.YuanSubdivision,



                        }).ToList();

            return Json(Linq,JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 开始 新增 保存（上市单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectPeiZaiDan(B_GoodsListedList OK, Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu, Array JieShouYuanJian, bool state,
            Array JieShouYuanXi, Array GoodsIDShuZu, Array WareHouseDetiailIDShuZu, Array MumberOfPackagesShuZu, Array SubdivisionShuZu,
            Array NumberShuZu, Array YuanMumberOfPackShuZu, Array YuanSubdivisionShuZu)
        {

            ReturnJsonVo returnJsonGoodsNot = new ReturnJsonVo();

            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JieShouID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JieShouJianShu)[0];
                string[] intsRuJian = M.Split(',');
                //三
                string N = ((string[])JieShouRuKuXiShu)[0];
                string[] intsNXiShu = N.Split(',');
                //四
                string C = ((string[])JieShouYuanJian)[0];
                string[] intsYuanPeiJian = C.Split(',');
                //五
                string YuJ = ((string[])JieShouYuanXi)[0];
                string[] intsYuanPeiXi = YuJ.Split(',');

                


                //配 一

                string PeiGoods = ((string[])GoodsIDShuZu)[0];
                string[] intPeiGoods = PeiGoods.Split(',');
                //二
                string PeiWaHouId = ((string[])WareHouseDetiailIDShuZu)[0];
                string[] intPeiWaHouId = PeiWaHouId.Split(',');

                //三
                string RuJian = ((string[])MumberOfPackagesShuZu)[0];
                string[] intRuJian = RuJian.Split(',');

                //四
                string RuXi = ((string[])SubdivisionShuZu)[0];
                string[] intRuXi = RuXi.Split(',');

                //五
                string PeiNuber = ((string[])NumberShuZu)[0];
                string[] intPeiNuber = PeiNuber.Split(',');

                //六
                string JinYuJian = ((string[])YuanMumberOfPackShuZu)[0];
                string[] intJinYuJian = JinYuJian.Split(',');

                //七
                string JinYuXi = ((string[])YuanSubdivisionShuZu)[0];
                string[] intJinYuXi = JinYuXi.Split(',');


                



                //判断记录编号是否存在
                int oldWareHouseRows = (from tb in myModels.B_GoodsListedList
                                        where tb.ListedNumber == OK.ListedNumber
                                        select tb).Count();

                if (oldWareHouseRows == 0)
                {
                    B_GoodsListedList MyB_ConverList = new B_GoodsListedList();

                    MyB_ConverList.ConverID = OK.ConverID;
                    MyB_ConverList.ListedNumber = OK.ListedNumber;
                    MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                    MyB_ConverList.ConsigneeShop = OK.ConsigneeShop;
                    MyB_ConverList.ConsigneeShopDiZhi = OK.ConsigneeShopDiZhi;
                    MyB_ConverList.payName = OK.payName;
                    MyB_ConverList.furlName = OK.furlName;
                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;
                    MyB_ConverList.GoodsDingYiQuID = OK.GoodsDingYiQuID;

                    myModels.B_GoodsListedList.Add(MyB_ConverList);
                    if (myModels.SaveChanges() > 0)
                    {
                        strMag = "succsee";

                        B_GoodsListedDetailList ConverDeTailList = new B_GoodsListedDetailList();

                        //上市
                        for (int i = 0; i < intsid.Length; )//配明细
                        {
                            for (int A = 0; A < intsRuJian.Length;)//送件
                            {
                                for (int B = 0; B < intsNXiShu.Length;)//送细
                                {
                                    for (int E = 0; E < intsYuanPeiJian.Length;)//原件
                                    {
                                        for (int D = 0; D < intsYuanPeiXi.Length; D++)//原细
                                        {
                                            B_GoodsListedDetailList BaoDateil = new B_GoodsListedDetailList();
                                            BaoDateil.GoodsListedID = MyB_ConverList.GoodsListedID;//上市Id

                                            BaoDateil.ConverDateilID = Convert.ToInt32(intsid[i]); ;//配ID
                                            BaoDateil.SongHuoJianShu = Convert.ToDecimal(intsRuJian[A]);//件数
                                            BaoDateil.SongHuoXiShu = Convert.ToDecimal(intsNXiShu[B]);//细数
                                            BaoDateil.MumberOfPackages = Convert.ToDecimal(intsYuanPeiJian[E]);//配原件数
                                            BaoDateil.Subdivision = Convert.ToDecimal(intsYuanPeiXi[D]);//配原细数
                                            BaoDateil.ShouChuLiang = 0;//配原细数
                                            myModels.B_GoodsListedDetailList.Add(BaoDateil);
                                            myModels.SaveChanges();

                                            E++;
                                            B++;
                                            A++;
                                            i++;
                                        }
                                    }
                                }
                            }
                        }


                        for (int PA = 0; PA < intsid.Length;)//配
                        {
                            for (int PB = 0; PB < intPeiGoods.Length;)//配（商品）
                            {
                                for (int PW = 0; PW < intPeiWaHouId.Length;)//配进明细
                                {
                                    for (int PC = 0; PC < intRuJian.Length;)//配件数
                                    {
                                        for (int PD = 0; PD < intRuXi.Length;)//配细数
                                        {
                                            for (int PF = 0; PF < intPeiNuber.Length;)//批次号
                                            {
                                                for (int PJ = 0; PJ < intJinYuJian.Length;)//配原件（进仓的）
                                                {
                                                    for (int PK = 0; PK < intJinYuXi.Length; PK++)//配细件（进仓的）
                                                    {
                                                        B_ConverDeTailList GaiBian = new B_ConverDeTailList();

                                                        GaiBian.ConverDeTaiID = Convert.ToInt32(intsid[PA]);//配明细Id
                                                        GaiBian.ConverID = MyB_ConverList.ConverID;//配货Id
                                                        GaiBian.GoodsID = Convert.ToInt32(intPeiGoods[PB]); ;//商品ID
                                                        GaiBian.WareHouseDetiailID = Convert.ToInt32(intPeiWaHouId[PW]);//进仓明细
                                                        GaiBian.MumberOfPackages = Convert.ToDecimal(intRuJian[PC]);//件数
                                                        GaiBian.Subdivision = Convert.ToDecimal(intRuXi[PD]);//细数
                                                        GaiBian.Number = Convert.ToDecimal(intPeiNuber[PF]);//批次号
                                                        GaiBian.YuanMumberOfPack = Convert.ToDecimal(intJinYuJian[PJ]);//件数（进仓的）
                                                        GaiBian.YuanSubdivision = Convert.ToDecimal(intJinYuXi[PK]);//细数（进仓的）

                                                        myModels.Entry(GaiBian).State = System.Data.Entity.EntityState.Modified;
                                                        myModels.SaveChanges();//保存

                                                        Session["WareHouseIDgfh"] = GaiBian.ConverID; //  配ID  

                                                        if (GaiBian.MumberOfPackages == 0)
                                                        {
                                                            B_ConverDeTailList wafrtboolDeti = (from tbbool in myModels.B_ConverDeTailList
                                                                                                where tbbool.ConverDeTaiID == GaiBian.ConverDeTaiID
                                                                                                select tbbool).Single();//查询原状态
                                                            wafrtboolDeti.YiKongNot = state;//改变是否商品为空
                                                            myModels.Entry(wafrtboolDeti).State = EntityState.Modified;
                                                            if (myModels.SaveChanges() > 0)//保存
                                                            {
                                                                returnJsonGoodsNot.State = true;
                                                                returnJsonGoodsNot.Text = "修改成功";

                                                            }
                                                            else
                                                            {
                                                                returnJsonGoodsNot.State = false;
                                                                returnJsonGoodsNot.Text = "修改失败";
                                                            }
                                                        }


                                                        PJ++;
                                                        PF++;
                                                        PD++;
                                                        PC++;
                                                        PW++;
                                                        PB++;
                                                        PA++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag , returnJsonGoodsNot }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 改变配货状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GAiZhaungTaiNot(bool state)
        {
            ReturnJsonVo returnJsonJin = new ReturnJsonVo();
            if (Session["WareHouseIDgfh"]!= null)
            {
                string converID = Session["WareHouseIDgfh"].ToString();

                if (converID != null)
                {
                    int fdf = Convert.ToInt32(converID);

                    var linq = (from tbWanHourDetiai in myModels.B_ConverDeTailList
                                where tbWanHourDetiai.ConverID == fdf
                                select new LY.WareHouseDeitaLL
                                {
                                    ExamineNot = tbWanHourDetiai.YiKongNot
                                }).ToList();

                    int zong = linq.Count();
                    int JiLei = 0;

                    for (int i = 0; i < zong; i++)
                    {
                        if (linq[0].ExamineNot == true)
                        {
                            JiLei = JiLei + 1;
                        }
                    }
                    if (JiLei == zong)
                    {
                        B_ConverList wafrtbool = (from tbbool in myModels.B_ConverList
                                                  where tbbool.ConverID == fdf
                                                  select tbbool).Single();
                        wafrtbool.HaveNot = state;
                        myModels.Entry(wafrtbool).State = EntityState.Modified;
                        if (myModels.SaveChanges() > 0)//保存
                        {
                            returnJsonJin.State = true;
                            returnJsonJin.Text = "修改成功";
                        }
                        else
                        {
                            returnJsonJin.State = false;
                            returnJsonJin.Text = "修改失败";
                        }
                    }
                    else
                    {
                        B_ConverList wafrtbool = (from tbbool in myModels.B_ConverList
                                                  where tbbool.ConverID == fdf
                                                  select tbbool).Single();
                        wafrtbool.HaveNot = false;
                        myModels.Entry(wafrtbool).State = EntityState.Modified;
                        if (myModels.SaveChanges() > 0)//保存
                        {
                            returnJsonJin.State = false;
                            returnJsonJin.Text = "修改失败";
                        }
                        else
                        {
                            returnJsonJin.State = true;
                            returnJsonJin.Text = "修改成功";
                        }
                    }
                    Session.Clear();
                    return Json(returnJsonJin, JsonRequestBehavior.AllowGet);
                }

            }

            return Json(returnJsonJin, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 查询上市单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsedrList(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodsListedList in myModels.B_GoodsListedList
                        join tbFaHuoBuMen in myModels.S_StockPlaceList on tbGoodsListedList.SpouseBRanchID equals tbFaHuoBuMen.StockPlaceID
                        join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbGoodsListedList.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                        join tbConver in myModels.B_ConverList on tbGoodsListedList.ConverID equals tbConver.ConverID
                        join tbWanHour in myModels.B_WareHouseList on tbConver.WareHouseID equals tbWanHour.WareHouseID
                        join tbOrerHovg in myModels.B_OrderFormPactList on tbWanHour.OrderFormPactID equals tbOrerHovg.OrderFormPactID
                        join tbGongHuo in myModels.B_SupplierList on tbOrerHovg.SupplierID equals tbGongHuo.SupplierID
                     
                        where tbGoodsListedList.ExamineNot == false
                        select new LY.PeiHuoDan
                        {
                            WareHouseID = tbGoodsListedList.GoodsListedID,//id
                            converIDs = tbGoodsListedList.ConverID,//id
                            P_Remember = tbGoodsListedList.ListedNumber,//编号
                            SpouseBRanchID = tbFaHuoBuMen.StockPlaceID,
                            SpouseBRanchName = tbFaHuoBuMen.StockPlaceName,
                            StockPlaceName = tbGoodsListedList.ConsigneeShop,
                            Remarks = tbGoodsListedList.ConsigneeShopDiZhi,
                            payName = tbGoodsListedList.payName,
                            furlName = tbGoodsListedList.furlName,
                            RegisterName = tbGoodsListedList.RegisterName,
                            registerTime = tbGoodsListedList.RegisterTime.ToString(),
                            Remember = tbHuoDongZhuan.GoodsDingYiQuMC,
                            QiDongName=tbGongHuo.SupplierCHName
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.ConverID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 绑定上市单(1)
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingGoodsedrList(int goodsListedID)
        {
            var linq = (from tbGoodsListedList in myModels.B_GoodsListedList
                        join tbFaHuoBuMen in myModels.S_StockPlaceList on tbGoodsListedList.SpouseBRanchID equals tbFaHuoBuMen.StockPlaceID
                        join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbGoodsListedList.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                        where tbGoodsListedList.GoodsListedID == goodsListedID
                        select new LY.PeiHuoDan
                        {
                            WareHouseID = tbGoodsListedList.GoodsListedID,//id
                            converIDs = tbGoodsListedList.ConverID,//id
                            P_Remember = tbGoodsListedList.ListedNumber,//编号
                            SpouseBRanchID = tbFaHuoBuMen.StockPlaceID,
                            SpouseBRanchName = tbFaHuoBuMen.StockPlaceName,
                            StockPlaceName = tbGoodsListedList.ConsigneeShop,
                            Remarks = tbGoodsListedList.ConsigneeShopDiZhi,
                            payName = tbGoodsListedList.payName,
                            furlName = tbGoodsListedList.furlName,
                            RegisterName = tbGoodsListedList.RegisterName,
                            registerTime = tbGoodsListedList.RegisterTime.ToString(),
                            StaffID=tbHuoDongZhuan.GoodsDingYiQuID,
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 绑定商品（2
        /// </summary>
        public ActionResult BangDingPiGoodelsedrlis(Vo.BsgridPage bsgridPage, int goodsListedID)
        {
            
                var linq = (from tbShangShi in myModels.B_GoodsListedList
                            join tbGoodsListedDetailL in myModels.B_GoodsListedDetailList on tbShangShi.GoodsListedID equals tbGoodsListedDetailL.GoodsListedID//上市明细
                            join tbWanHofDeaill in myModels.B_ConverDeTailList on tbGoodsListedDetailL.ConverDateilID equals tbWanHofDeaill.ConverDeTaiID//配货明细
                            join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            where tbGoodsListedDetailL.GoodsListedID == goodsListedID
                            select new LY.PeiHuoDan
                            {
                                ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                                WareHouseDetiailID = tbGoodsListedDetailL.GoodsListedDetailID,//上市明细ID
                                converIDs = tbShangShi.ConverID,//id
                                 YuanMumberOfPack= tbGoodsListedDetailL.SongHuoJianShu,//送货件数
                                Subdivision = tbGoodsListedDetailL.SongHuoXiShu,//送货细数
                                MumberOfPackages = tbGoodsListedDetailL.MumberOfPackages,//原（件数）
                                MumberOfPackagesDing = tbGoodsListedDetailL.Subdivision,//原（细数）
                            
                               
                                GoodsCodes = tbShangPin.GoodsCode,//商品代码
                                GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                GoodsNames = tbShangPin.GoodsName,//商品名称
                                ArtNos = tbShangPin.ArtNo,//商品货号

                                SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                TaxBids = tbGoodDetail.TaxBid,//含税进价
                                RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

                            }).ToList();      
            
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.ConverDeTaiID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.PeiHuoDan> bsgrid = new Vo.Bsgrid<LY.PeiHuoDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 开始 修改 保存（上市单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptocitPeiZaiDan(B_GoodsListedList OK, Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu, Array JieShouYuanJian, bool state,
            Array JieShouYuanXi, Array GoodsIDShuZu, Array WareHouseDetiailIDShuZu, Array MumberOfPackagesShuZu, Array SubdivisionShuZu,
            Array NumberShuZu, Array YuanMumberOfPackShuZu, Array YuanSubdivisionShuZu, Array JieShouGoodsselisDateilID)
        {

            ReturnJsonVo returnJsonGoodsNot = new ReturnJsonVo();

            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JieShouID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JieShouJianShu)[0];
                string[] intsRuJian = M.Split(',');
                //三
                string N = ((string[])JieShouRuKuXiShu)[0];
                string[] intsNXiShu = N.Split(',');
                //四
                string C = ((string[])JieShouYuanJian)[0];
                string[] intsYuanPeiJian = C.Split(',');
                //五
                string YuJ = ((string[])JieShouYuanXi)[0];
                string[] intsYuanPeiXi = YuJ.Split(',');
                //liu
                string Goodse = ((string[])JieShouGoodsselisDateilID)[0];
                string[] intsGoodsseilDateil = Goodse.Split(',');



                //配 一

                string PeiGoods = ((string[])GoodsIDShuZu)[0];
                string[] intPeiGoods = PeiGoods.Split(',');
                //二
                string PeiWaHouId = ((string[])WareHouseDetiailIDShuZu)[0];
                string[] intPeiWaHouId = PeiWaHouId.Split(',');

                //三
                string RuJian = ((string[])MumberOfPackagesShuZu)[0];
                string[] intRuJian = RuJian.Split(',');

                //四
                string RuXi = ((string[])SubdivisionShuZu)[0];
                string[] intRuXi = RuXi.Split(',');

                //五
                string PeiNuber = ((string[])NumberShuZu)[0];
                string[] intPeiNuber = PeiNuber.Split(',');

                //六
                string JinYuJian = ((string[])YuanMumberOfPackShuZu)[0];
                string[] intJinYuJian = JinYuJian.Split(',');

                //七
                string JinYuXi = ((string[])YuanSubdivisionShuZu)[0];
                string[] intJinYuXi = JinYuXi.Split(',');


                B_GoodsListedList MyB_ConverList = new B_GoodsListedList();

                MyB_ConverList.GoodsListedID = OK.GoodsListedID;
                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.ListedNumber = OK.ListedNumber;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.ConsigneeShop = OK.ConsigneeShop;
                MyB_ConverList.ConsigneeShopDiZhi = OK.ConsigneeShopDiZhi;
                MyB_ConverList.payName = OK.payName;
                MyB_ConverList.furlName = OK.furlName;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.GoodsDingYiQuID = OK.GoodsDingYiQuID;

                myModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {
                    strMag = "succsee";

                    B_GoodsListedDetailList ConverDeTailList = new B_GoodsListedDetailList();

                    //上市
                    for (int i = 0; i < intsid.Length;)//配明细
                    {
                        for (int A = 0; A < intsRuJian.Length;)//送件
                        {
                            for (int B = 0; B < intsNXiShu.Length;)//送细
                            {
                                for (int E = 0; E < intsYuanPeiJian.Length;)//原件
                                {
                                    for (int D = 0; D < intsYuanPeiXi.Length;)//原细
                                    {
                                        for (int Date = 0; Date < intsGoodsseilDateil.Length; Date++)
                                        {
                                            B_GoodsListedDetailList BaoDateil = new B_GoodsListedDetailList();
                                            BaoDateil.GoodsListedID = MyB_ConverList.GoodsListedID;//上市Id
                                            BaoDateil.GoodsListedDetailID = Convert.ToInt32(intsGoodsseilDateil[Date]); ;//上市明细ID
                                            BaoDateil.ConverDateilID = Convert.ToInt32(intsid[i]); ;//配ID
                                            BaoDateil.SongHuoJianShu = Convert.ToDecimal(intsRuJian[A]);//件数
                                            BaoDateil.SongHuoXiShu = Convert.ToDecimal(intsNXiShu[B]);//细数
                                            BaoDateil.MumberOfPackages = Convert.ToDecimal(intsYuanPeiJian[E]);//配原件数
                                            BaoDateil.Subdivision = Convert.ToDecimal(intsYuanPeiXi[D]);//配原细数

                                            myModels.Entry(BaoDateil).State = System.Data.Entity.EntityState.Modified;
                                            myModels.SaveChanges();//保存

                                            D++;
                                            E++;
                                            B++;
                                            A++;
                                            i++;
                                        }

                                    }
                                }
                            }
                        }
                    }

                    if (intsid.Length > 0)
                    {
                        for (int PA = 0; PA < intsid.Length;)//配
                        {
                            for (int PB = 0; PB < intPeiGoods.Length;)//配（商品）
                            {
                                for (int PW = 0; PW < intPeiWaHouId.Length;)//配进明细
                                {
                                    for (int PC = 0; PC < intRuJian.Length;)//配件数
                                    {
                                        for (int PD = 0; PD < intRuXi.Length;)//配细数
                                        {
                                            for (int PF = 0; PF < intPeiNuber.Length;)//批次号
                                            {
                                                for (int PJ = 0; PJ < intJinYuJian.Length;)//配原件（进仓的）
                                                {
                                                    for (int PK = 0; PK < intJinYuXi.Length; PK++)//配细件（进仓的）
                                                    {
                                                        B_ConverDeTailList GaiBian = new B_ConverDeTailList();

                                                        GaiBian.ConverDeTaiID = Convert.ToInt32(intsid[PA]);//配明细Id
                                                        GaiBian.ConverID = MyB_ConverList.ConverID;//配货Id
                                                        GaiBian.GoodsID = Convert.ToInt32(intPeiGoods[PB]); ;//商品ID
                                                        GaiBian.WareHouseDetiailID = Convert.ToInt32(intPeiWaHouId[PW]);//进仓明细
                                                        GaiBian.MumberOfPackages = Convert.ToDecimal(intRuJian[PC]);//件数
                                                        GaiBian.Subdivision = Convert.ToDecimal(intRuXi[PD]);//细数
                                                        GaiBian.Number = Convert.ToDecimal(intPeiNuber[PF]);//批次号
                                                        GaiBian.YuanMumberOfPack = Convert.ToDecimal(intJinYuJian[PJ]);//件数（进仓的）
                                                        GaiBian.YuanSubdivision = Convert.ToDecimal(intJinYuXi[PK]);//细数（进仓的）

                                                        myModels.Entry(GaiBian).State = System.Data.Entity.EntityState.Modified;
                                                        myModels.SaveChanges();//保存

                                                        if (GaiBian.ConverID > 0)
                                                        {
                                                            Session["WareHouseIDgfh"] = GaiBian.ConverID; //  配ID
                                                        }


                                                        if (GaiBian.MumberOfPackages == 0)
                                                        {
                                                            B_ConverDeTailList wafrtboolDeti = (from tbbool in myModels.B_ConverDeTailList
                                                                                                where tbbool.ConverDeTaiID == GaiBian.ConverDeTaiID
                                                                                                select tbbool).Single();//查询原状态
                                                            wafrtboolDeti.YiKongNot = state;//改变是否商品为空
                                                            myModels.Entry(wafrtboolDeti).State = EntityState.Modified;
                                                            if (myModels.SaveChanges() > 0)//保存
                                                            {
                                                                returnJsonGoodsNot.State = true;
                                                                returnJsonGoodsNot.Text = "修改成功";

                                                            }
                                                            else
                                                            {
                                                                returnJsonGoodsNot.State = false;
                                                                returnJsonGoodsNot.Text = "修改失败";
                                                            }
                                                        }


                                                        PJ++;
                                                        PF++;
                                                        PD++;
                                                        PC++;
                                                        PW++;
                                                        PB++;
                                                        PA++;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                  
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag , returnJsonGoodsNot }, JsonRequestBehavior.AllowGet);
        }





        //删除部分
        /// <summary>
        /// 查询明细（1
        /// </summary>
        public ActionResult SeleingPiGoodelsedrlis(int goodsListedID)
        {
            var linq = (from tbShangShi in myModels.B_GoodsListedList
                        join tbGoodsListedDetailL in myModels.B_GoodsListedDetailList on tbShangShi.GoodsListedID equals tbGoodsListedDetailL.GoodsListedID//上市明细
                        join tbWanHofDeaill in myModels.B_ConverDeTailList on tbGoodsListedDetailL.ConverDateilID equals tbWanHofDeaill.ConverDeTaiID//配货明细
                      
                        where tbGoodsListedDetailL.GoodsListedID == goodsListedID
                        select new LY.PeiHuoDan
                        {
                            WareHouseDetiailID = tbGoodsListedDetailL.GoodsListedDetailID,//上市明细ID
                            YuanMumberOfPack = tbGoodsListedDetailL.SongHuoJianShu,//送货件数
                            YuanSubdivision = tbGoodsListedDetailL.SongHuoXiShu,//送货细数


                            ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                            converIDs = tbShangShi.ConverID,//配货id
                            GoodsIDs = tbWanHofDeaill.GoodsID,
                            WanManifestID = tbWanHofDeaill.WareHouseDetiailID,
                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//（件数）配
                            Subdivision = tbWanHofDeaill.Subdivision,//（细数）配
                            Number = tbWanHofDeaill.Number,
                            MumberOfPackagesDing = tbWanHofDeaill.YuanMumberOfPack,//原进仓件
                            TaxBid = tbWanHofDeaill.YuanSubdivision,//原进仓细


                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 删除  保存（配货单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptocitPeiHuoDan(Array ConvDateilShuZuk, Array ConverShuZuk, Array GoodsIDShuZu, Array WareHouseDetiailIDShuZu, bool state,
            Array MumberOfPackagesShuZu, Array SubdivisionShuZu, Array NumberShuZu, Array YuanMumberOfPackShuZu, Array YuanSubdivisionShuZu)
        {

            ReturnJsonVo returnJsonGoodsNot = new ReturnJsonVo();
            string strMag = "fali";

            try
            {

                //配 一

                string PeiGoods = ((string[])GoodsIDShuZu)[0];
                string[] intPeiGoods = PeiGoods.Split(',');
                //二
                string PeiWaHouId = ((string[])WareHouseDetiailIDShuZu)[0];
                string[] intPeiWaHouId = PeiWaHouId.Split(',');

                //三
                string RuJian = ((string[])MumberOfPackagesShuZu)[0];
                string[] intRuJian = RuJian.Split(',');

                //四
                string RuXi = ((string[])SubdivisionShuZu)[0];
                string[] intRuXi = RuXi.Split(',');

                //五
                string PeiNuber = ((string[])NumberShuZu)[0];
                string[] intPeiNuber = PeiNuber.Split(',');

                //六
                string JinYuJian = ((string[])YuanMumberOfPackShuZu)[0];
                string[] intJinYuJian = JinYuJian.Split(',');

                //七
                string JinYuXi = ((string[])YuanSubdivisionShuZu)[0];
                string[] intJinYuXi = JinYuXi.Split(',');


                //八
                string PeiMingXi = ((string[])ConvDateilShuZuk)[0];
                string[] intPeiMingXi = PeiMingXi.Split(',');

                
                 //九
                string PeiId = ((string[])ConverShuZuk)[0];
                string[] intPeiId = PeiId.Split(',');

             

                //配
                for (int Id = 0; Id < intPeiId.Length;)
                {
                    for (int i = 0; i < intPeiMingXi.Length;)//配明细
                    {
                        for (int A = 0; A < intPeiGoods.Length;)//商品
                        {
                            for (int B = 0; B < intPeiWaHouId.Length;)//进仓明细
                            {
                                for (int E = 0; E < intRuJian.Length;)//件
                                {
                                    for (int D = 0; D < intRuXi.Length;)//细
                                    {
                                        for (int Date = 0; Date < intPeiNuber.Length; )//批号
                                        {
                                            for (int YJ = 0; YJ < intJinYuJian.Length;)//原件
                                            {
                                                for (int YX = 0; YX < intJinYuXi.Length; YX++)//原细
                                                {
                                                    B_ConverDeTailList BaoDateil = new B_ConverDeTailList();

                                                    BaoDateil.ConverID = Convert.ToInt32(intPeiId[Id]); //配Id
                                                    BaoDateil.ConverDeTaiID = Convert.ToInt32(intPeiMingXi[i]); //配明细ID
                                                    BaoDateil.GoodsID = Convert.ToInt32(intPeiGoods[A]); //商品
                                                    BaoDateil.WareHouseDetiailID = Convert.ToInt32(intPeiWaHouId[B]);//进仓明细
                                                    BaoDateil.MumberOfPackages = Convert.ToDecimal(intRuJian[E]);//件数
                                                    BaoDateil.Subdivision = Convert.ToDecimal(intRuXi[D]);//细数
                                                    BaoDateil.Number = Convert.ToDecimal(intPeiNuber[Date]);//批号
                                                    BaoDateil.YuanMumberOfPack = Convert.ToDecimal(intJinYuJian[YJ]);//原件
                                                    BaoDateil.YuanSubdivision = Convert.ToDecimal(intJinYuXi[YX]);//原系

                                                    myModels.Entry(BaoDateil).State = System.Data.Entity.EntityState.Modified;
                                                    myModels.SaveChanges();//保存
                                                    strMag = "succsee";

                                                    if (BaoDateil.ConverID > 0)
                                                    {
                                                        Session["WareHouseIDgfh"] = BaoDateil.ConverID; //  配ID
                                                    }

                                                    if (BaoDateil.MumberOfPackages > 0)
                                                    {
                                                        B_ConverDeTailList wafrtboolDeti = (from tbbool in myModels.B_ConverDeTailList
                                                                                            where tbbool.ConverDeTaiID == BaoDateil.ConverDeTaiID
                                                                                            select tbbool).Single();//查询原状态
                                                        wafrtboolDeti.YiKongNot = state;//改变是否商品为空
                                                        myModels.Entry(wafrtboolDeti).State = EntityState.Modified;
                                                        if (myModels.SaveChanges() > 0)//保存
                                                        {
                                                            returnJsonGoodsNot.State = false;
                                                            returnJsonGoodsNot.Text = "修改成功";

                                                        }
                                                        else
                                                        {
                                                            returnJsonGoodsNot.State = true;
                                                            returnJsonGoodsNot.Text = "修改失败";
                                                        }
                                                    }
                                                   


                                                    YJ++;
                                                    Date++;
                                                    D++;
                                                    E++;
                                                    B++;
                                                    A++;
                                                    i++;
                                                    Id++;


                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJsonGoodsNot }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 删除（上市单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeletegoodsListedt(int goodsListedID)
        {
            string strMsg = "fail";
            try
            {

                B_GoodsListedList conver = (from tbWarHouser in myModels.B_GoodsListedList
                                            where tbWarHouser.GoodsListedID == goodsListedID
                                            select tbWarHouser).Single();
                myModels.B_GoodsListedList.Remove(conver);

                int waDetialid = (int)conver.GoodsListedID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in myModels.B_GoodsListedDetailList
                                    where tbWarHouserDetial.GoodsListedID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        myModels.B_GoodsListedDetailList.Remove(converDetial[i]);
                        myModels.SaveChanges();
                        strMsg = "success";
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        ///  审核 保存（上市单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenHePeiZaiDan(B_GoodsListedList OK, bool state)
        {
            ReturnJsonVo returnJsonGoodsNot = new ReturnJsonVo();

            string strMag = "fali";

            try
            {


                B_GoodsListedList MyB_ConverList = new B_GoodsListedList();

                MyB_ConverList.GoodsListedID = OK.GoodsListedID;
                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.ListedNumber = OK.ListedNumber;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.ConsigneeShop = OK.ConsigneeShop;
                MyB_ConverList.ConsigneeShopDiZhi = OK.ConsigneeShopDiZhi;
                MyB_ConverList.payName = OK.payName;
                MyB_ConverList.furlName = OK.furlName;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.GoodsDingYiQuID = OK.GoodsDingYiQuID;
                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExamineTime = OK.ExamineTime;


                myModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (myModels.SaveChanges() > 0)
                {

                    B_GoodsListedList wafrtboolDeti = (from tbbool in myModels.B_GoodsListedList
                                                       where tbbool.GoodsListedID == MyB_ConverList.GoodsListedID
                                                       select tbbool).Single();//查询原状态
                    wafrtboolDeti.ExamineNot = state;//审核
                    myModels.Entry(wafrtboolDeti).State = EntityState.Modified;
                    if (myModels.SaveChanges() > 0)//保存
                    {
                        returnJsonGoodsNot.State = true;
                        returnJsonGoodsNot.Text = "修改成功";

                    }
                    else
                    {
                        returnJsonGoodsNot.State = false;
                        returnJsonGoodsNot.Text = "修改失败";
                    }

                    strMag = "succsee";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJsonGoodsNot }, JsonRequestBehavior.AllowGet);
        }



    }
}
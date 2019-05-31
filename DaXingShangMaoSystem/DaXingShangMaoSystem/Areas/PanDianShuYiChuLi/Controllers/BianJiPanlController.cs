using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.PanDianShuYiChuLi.Controllers
{
    public class BianJiPanlController : Controller
    {
        // GET: PanDianShuYiChuLi/BianJiPanl
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        //视图一
        public ActionResult XuanZePanl()
        {
            return View();
        }


        //一
        // <summary>
        ///查询盘点计划(记录列表)
        /// </summary>
        public ActionResult SelectCheckgh(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                       
                        where tbGoodBiao.GoodsNot==true && tbGoodBiao.YiJiaZaiGoodsNot==false
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
                            Remember = tbGoodBiao.Remter,
                            PablData = tbGoodBiao.PablData.ToString(),
                            CommodityType = tbGoodBiao.CommodityType,
                            DrugType = tbGoodBiao.DrugType,

                            StockPlaceName = tbStock.StockPlaceName,
                            ExamineName = tbGoodBiao.ExamineName,
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }



        // <summary>
        ///选中(记录列表)Id
        /// </summary>
        /// <returns></returns>
        public ActionResult ChuanlectCheckgh(int checkRermeberId)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.CheckRermeberID == checkRermeberId
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
                            Remember = tbGoodBiao.Remter,
                            PablData = tbGoodBiao.PablData.ToString(),
                            CommodityType = tbGoodBiao.CommodityType,
                            DrugType = tbGoodBiao.DrugType,
                            StockPlaceName = tbStock.StockPlaceName,

                        }).ToList();
            Session["CheckRermeberID"] = linq[0].CheckRermeberID; // 传递
            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        //视图二
        public ActionResult JinRuZePanl()
        {
            return View();
        }

        // <summary>
        ///绑定界面信息)Id
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDlectCheckgh()
        {
            string CheckRermeberId = Session["CheckRermeberID"].ToString();//计划盘点id   
            int intCheckRermeId = Convert.ToInt32(CheckRermeberId);

            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.CheckRermeberID == intCheckRermeId
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
                            Remember = tbGoodBiao.Remter,
                            PablData = tbGoodBiao.PablData.ToString(),
                            DrugType = tbGoodBiao.DrugType,
                            StockPlaceName = tbStock.StockPlaceName,

                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        ///查询(绑定)盘点计划(商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsffGoodsj(Vo.BsgridPage bsgridPage, int checkRermeberID)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChredgRe.CheckRermeberID == checkRermeberID
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID=tbChredgRe.CheckRermeberID,
                            CheckRemerbenDetillD = tbChredgReDateil.CheckRemerbenDetillD,
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            ArtNos = tbGoods.ArtNo,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            Subdivision=tbChredgReDateil.Subdivision,//细数
                            MumberOfPackages = tbChredgReDateil.MumberOfPackages//件数
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();
            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        ///查询(绑定)盘点计划(右商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsffGoodsjRight(Vo.BsgridPage bsgridPage, int checkRermeberID,int goodsIDs)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChredgRe.CheckRermeberID == checkRermeberID && tbChredgReDateil.GoodsID== goodsIDs
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbChredgRe.CheckRermeberID,
                            CheckRemerbenDetillD = tbChredgReDateil.CheckRemerbenDetillD,
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            ArtNos = tbGoods.ArtNo,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            Subdivision = tbChredgReDateil.Subdivision//细数

                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();
            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 盈亏数
        /// </summary>
        /// <returns></returns>
        public ActionResult MoRenMiMaq()//strTime
        {
            int ShuZiLenht = 0;
            int ZuiDa = 9;
            int ChuShi = 1;
            Random randomBuilder = new Random();
            for (int yi = ChuShi; yi < ZuiDa; yi++)
            {
                ShuZiLenht = randomBuilder.Next(ChuShi, ZuiDa);
            }
            return Json(ShuZiLenht, JsonRequestBehavior.AllowGet);

        }


        ///查询(绑定)盘点计划(定位商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult DingWeiGoodsjRight(Vo.BsgridPage bsgridPage, string coodsCodes)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbGoods.GoodsCode == coodsCodes
                        select new LY.WareHouseDeitaLL
                        {
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            ArtNos = tbGoods.ArtNo,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            Subdivision = tbChredgReDateil.Subdivision//细数

                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();
            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }






        /// <summary>
        /// 开始 保存（商品批次）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptuarPiCiBaoCun(Array JieGoodsShuZu, Array JiePiCiChShuZu, Array CheckReIDShuZu,
            Array CheckReDetailIDShuZu, Array MumberOfPacShuZu, Array SubdivisiShuZu, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            ReturnJsonVo ChreturnJson = new ReturnJsonVo();
            string strMag = "fali";
            try
            {
                //一
                string Z = ((string[])JieGoodsShuZu)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JiePiCiChShuZu)[0];
                string[] intsPiCi = M.Split(',');
                //三
                string X = ((string[])CheckReIDShuZu)[0];
                string[] intsFanChangID = X.Split(',');
                //四
                string D = ((string[])CheckReDetailIDShuZu)[0];
                string[] intsFanChangMingXiID = D.Split(',');
                //五
                string Mun = ((string[])MumberOfPacShuZu)[0];
                string[] intMumberJianShu = Mun.Split(',');
                //六
                string Su = ((string[])SubdivisiShuZu)[0];
                string[] intSubdiviShu = Su.Split(',');



                B_CheckRemerbenDetillList ConverDeTailList = new B_CheckRemerbenDetillList();
                strMag = "succsee";
                for (int H = 0; H < intsFanChangMingXiID.Length;)
                {
                    for (int E = 0; E < intsFanChangID.Length;)
                    {
                        for (int d = 0; d < intsid.Length;)
                        {
                            for (int i = 0; i < intsPiCi.Length;)
                            {
                                for (int JianShu = 0; JianShu < intMumberJianShu.Length;)
                                {
                                    for (int XiShu = 0; XiShu < intSubdiviShu.Length;)
                                    {

                                        ConverDeTailList.CheckRemerbenDetillD = Convert.ToInt32(intsFanChangMingXiID[H]);//返仓明细ID

                                        ConverDeTailList.CheckRermeberID = Convert.ToInt32(intsFanChangID[E]);//返仓ID
                                        ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]);//商品ID
                                        ConverDeTailList.PiCiHao = Convert.ToDecimal(intsPiCi[i]);//批号
                                        ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intMumberJianShu[JianShu]);//入库件数
                                        ConverDeTailList.Subdivision = Convert.ToDecimal(intSubdiviShu[H]);//入库细数

                                        MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                                        MyModels.SaveChanges();//保存

                                        XiShu++;
                                        JianShu++;
                                        i++;
                                        d++;
                                        E++;
                                        H++;


                                        B_CheckRemerbenDetillList Goofrtbool = (from tbbool in MyModels.B_CheckRemerbenDetillList
                                                                                where tbbool.CheckRemerbenDetillD == ConverDeTailList.CheckRemerbenDetillD
                                                                                select tbbool).Single();//查询原状态
                                        Goofrtbool.JiaZaiFou = state;//改变是否为商品状态
                                        MyModels.Entry(Goofrtbool).State = EntityState.Modified;

                                        B_CheckRemerbenList CherGoofrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                                                              where tbbool.CheckRermeberID == ConverDeTailList.CheckRermeberID
                                                                              select tbbool).Single();//查询原状态
                                        CherGoofrtbool.YiJiaZaiGoodsNot = state;//改变是否为主单状态
                                        MyModels.Entry(CherGoofrtbool).State = EntityState.Modified;

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
            return Json(new { strMag, returnJson}, JsonRequestBehavior.AllowGet);
        }


    }
}
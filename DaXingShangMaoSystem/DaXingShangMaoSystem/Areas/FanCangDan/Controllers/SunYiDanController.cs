using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.FanCangDan.Controllers
{
    public class SunYiDanController : Controller
    {
        // GET: FanCangDan/SunYiDan
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult SunYin()
        {
            return View();
        }


        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCodef()
        {
            string Std = "";
            var listy = (from tbem in MyModels.B_IncreaseList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_IncreaseList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.Remember.Substring(1, 8));
                inemp++;
                Std = inemp.ToString();
                for (int i = 0; i < 6; i++)
                {
                    Std = Std.Length < 6 ? "0" + Std : Std;
                }
            }
            else
            {
                Std = "000001";
            }
            return Json(Std, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 查询部门
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBuMen(int spouseBRanchID)
        {
            var Linq = (from tbBumen in MyModels.B_PanlList
                        where tbBumen.DrugTypeID == spouseBRanchID
                        select new LY.WareHouseDeitaLL
                        {
                            DrugType = tbBumen.DrugType,
                            DrugTypeID = tbBumen.DrugTypeID
                        }).ToList();

            return Json(Linq, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询库存
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectKuCun(int stockPlaceID)
        {
            var Linq = (from tbBumen in MyModels.S_StockPlaceList
                        where tbBumen.StockPlaceID == stockPlaceID
                        select new LY.WareHouseDeitaLL
                        {
                            StockPlaceName = tbBumen.StockPlaceName,
                            StockPlaceID = tbBumen.StockPlaceID
                        }).ToList();

            return Json(Linq, JsonRequestBehavior.AllowGet);
        }




        ///查询(绑定)盘点计划(商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsffGoodsj(Vo.BsgridPage bsgridPage, string serugType)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChredgRe.DrugType == serugType
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

                            Subdivision = tbChredgReDateil.Subdivision,//细数
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


        ///查询盘点计划(商品(条件))
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckeGoodsj(Vo.BsgridPage bsgridPage,string goodsCodea, string pinYinMa, string goodsTiaoMa)
        {
            var linq = (from tbChredgRe in MyModels.B_CheckRemerbenList
                        join tbChredgReDateil in MyModels.B_CheckRemerbenDetillList on tbChredgRe.CheckRermeberID equals tbChredgReDateil.CheckRermeberID
                        join tbGoods in MyModels.B_GoodsList on tbChredgReDateil.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbGoods.GoodsCode == goodsCodea|| tbGoods.PLUCod == pinYinMa || tbGoods.GoodsTiaoMa == goodsTiaoMa||
                        tbGoods.GoodsCode == goodsCodea && tbGoods.PLUCod == pinYinMa|| tbGoods.GoodsCode == goodsCodea && tbGoods.GoodsTiaoMa == goodsTiaoMa||
                        tbGoods.GoodsCode == goodsCodea && tbGoods.PLUCod == pinYinMa && tbGoods.GoodsTiaoMa == goodsTiaoMa
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

                            Subdivision = tbChredgReDateil.Subdivision,//细数
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




        /// <summary>
        /// 绑定商品到（主界面）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, Array jiLuChaekID)
        {
            List<WareHouseDeitaLL> list = new List<WareHouseDeitaLL>();
            // int goodsIDs
            string Z = ((string[])jiLuChaekID)[0];
            string[] intsZ = Z.Split(',');

            for (int P = 0; P < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[P]);
                var linq = (from tbGoods in MyModels.B_GoodsList//商品
                            join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                            join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                            join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                            where tbGoods.GoodsID == goodsIDs

                            select new LY.WareHouseDeitaLL
                            {
                                //CheckRermeberID = tbChredgRe.CheckRermeberID,
                                //CheckRemerbenDetillD = tbChredgReDateil.CheckRemerbenDetillD,
                                GoodsIDs = tbGoods.GoodsID,
                                GoodsCodes = tbGoods.GoodsCode,//代码
                                GoodsNames = tbGoods.GoodsName,//商品名称

                                GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            
                                ArtNos = tbGoods.ArtNo,//商品货号
                               
                                PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                               
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            }).ToList();
                list.AddRange(linq);
                P++;
            }
            int totalRow = list.Count();
            List<LY.WareHouseDeitaLL> notices = list.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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




        /// <summary>
        /// 开始 新增 保存（损益单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectPeiZaiDan(B_IncreaseList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array HuoQuPiHao)
        {

            string strMag = "fali";
            try
            {
                //一
                string Z = ((string[])JiShangPiID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JiRuJianShu)[0];
                string[] intsRuJian = M.Split(',');
                //三
                string N = ((string[])JiRuXiShu)[0];
                string[] intsNXiShu = N.Split(',');
                //四
                string C = ((string[])HuoQuPiHao)[0];
                string[] intsNPiHao = C.Split(',');

                int oldWareHouseRows = (from tb in MyModels.B_IncreaseList
                                        where tb.Remember == OK.Remember
                                        select tb).Count();

                if (oldWareHouseRows == 0)
                {
                    B_IncreaseList MyB_ConverList = new B_IncreaseList();

                    MyB_ConverList.Remember = OK.Remember;
                    MyB_ConverList.IncreaseCause = OK.IncreaseCause;
                    MyB_ConverList.DrugTypeID = OK.DrugTypeID;
                    MyB_ConverList.StockPlaceID = OK.StockPlaceID;

                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;

                    MyModels.B_IncreaseList.Add(MyB_ConverList);
                    MyModels.SaveChanges();

                    for (int H = 0; H < intsNXiShu.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length;)
                            {
                                for (int Pi = 0; Pi < intsNPiHao.Length; Pi++)
                                {
                                    B_IncreaseDetailList ConverDeTailList = new B_IncreaseDetailList();
                                    ConverDeTailList.IncreaseID = MyB_ConverList.IncreaseID;
                                    ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID
                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                    ConverDeTailList.Number = Convert.ToDecimal(intsNPiHao[Pi]);//批号
                                    MyModels.B_IncreaseDetailList.Add(ConverDeTailList);
                                    MyModels.SaveChanges();//保存
                                    strMag = "succsee";
                                    d++;
                                    E++;
                                    H++;
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
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }



        ///查询损益
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectInsfdr(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbIncrea in MyModels.B_IncreaseList
                        join tbbumen in MyModels.B_PanlList on tbIncrea.DrugTypeID equals tbbumen.DrugTypeID
                        join tbKuCun in MyModels.S_StockPlaceList on tbIncrea.StockPlaceID equals tbKuCun.StockPlaceID
                        where tbIncrea.ExamineNot==false
                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbIncrea.IncreaseID,
                            Remember = tbIncrea.Remember,
                            IncreaseCause = tbIncrea.IncreaseCause,
                            DrugType = tbbumen.DrugType,
                            StockPlaceName = tbKuCun.StockPlaceName,
                            RegisterName = tbIncrea.RegisterName,
                            RegisterTime = tbIncrea.RegisterTime.ToString(),
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


        ///绑定损益(一)
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingtInsfdr(int increaseID)
        {
            var linq = (from tbIncrea in MyModels.B_IncreaseList
                        join tbbumen in MyModels.B_PanlList on tbIncrea.DrugTypeID equals tbbumen.DrugTypeID
                        join tbKuCun in MyModels.S_StockPlaceList on tbIncrea.StockPlaceID equals tbKuCun.StockPlaceID
                        where tbIncrea.IncreaseID == increaseID
                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbIncrea.IncreaseID,
                            Remember = tbIncrea.Remember,
                            IncreaseCause = tbIncrea.IncreaseCause,
                            DrugTypeID = tbbumen.DrugTypeID,
                            StockPlaceID = tbKuCun.StockPlaceID,
                            RegisterName = tbIncrea.RegisterName,
                            RegisterTime = tbIncrea.RegisterTime.ToString(),
                        }).ToList();

           
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        ///查询(绑定)盘点计划(商品)二
        /// </summary>
        /// <returns></returns>
        public ActionResult BangCheckewsffGoodsj(Vo.BsgridPage bsgridPage,int increaseID)
        {
            var linq = (from tbChredgRe in MyModels.B_IncreaseDetailList
                        join tbGoods in MyModels.B_GoodsList on tbChredgRe.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChredgRe.IncreaseID == increaseID
                        select new LY.WareHouseDeitaLL
                        {
                            IncreaseID = tbChredgRe.IncreaseID,
                            IncreaseDetailID = tbChredgRe.IncreaseDetailID,//损益明细
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            ArtNos = tbGoods.ArtNo,//商品货号
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            Subdivision = tbChredgRe.Subdivision,//细数
                            MumberOfPackages = tbChredgRe.MumberOfPackages,//件数
                            Number = tbChredgRe.Number//批次
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
        /// 开始 修改 保存（损益单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UnpticoIncrea(B_IncreaseList OK, Array JiShangPiID, Array JiRuJianShu,
            Array JiRuXiShu, Array HuoQuSunYiDetailShu, Array HuoQNumerlShu)
        {

            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JiShangPiID)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JiRuJianShu)[0];
                string[] intsRuJian = M.Split(',');
                //三
                string N = ((string[])JiRuXiShu)[0];
                string[] intsNXiShu = N.Split(',');
                //四
                string C = ((string[])HuoQuSunYiDetailShu)[0];
                string[] intsDetail = C.Split(',');

                //五
                string Pic = ((string[])HuoQNumerlShu)[0];
                string[] intsNPiHao = Pic.Split(',');


                B_IncreaseList MyB_ConverList = new B_IncreaseList();

                MyB_ConverList.IncreaseID = OK.IncreaseID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.IncreaseCause = OK.IncreaseCause;
                MyB_ConverList.DrugTypeID = OK.DrugTypeID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;

                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";

                    B_IncreaseDetailList ConverDeTailList = new B_IncreaseDetailList();

                    for (int H = 0; H < intsNXiShu.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length;)
                            {
                                for (int Pi = 0; Pi < intsNPiHao.Length;)
                                {
                                    for (int Deti = 0; Deti < intsDetail.Length; Deti++)
                                    {
                                        ConverDeTailList.IncreaseDetailID = Convert.ToInt32(intsDetail[Deti]);
                                        ConverDeTailList.IncreaseID = MyB_ConverList.IncreaseID;
                                        ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]);//商品ID
                                        ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                        ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                        ConverDeTailList.Number = Convert.ToDecimal(intsNPiHao[Pi]);//批号
                                        MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                                        MyModels.SaveChanges();//保存
                                        H++;
                                        E++;
                                        d++;
                                        Pi++;
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
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 开始 修改 保存（损益单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenHecoIncrea(B_IncreaseList OK, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";

            try
            {
                

                B_IncreaseList MyB_ConverList = new B_IncreaseList();
                MyB_ConverList.IncreaseID = OK.IncreaseID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.IncreaseCause = OK.IncreaseCause;
                MyB_ConverList.DrugTypeID = OK.DrugTypeID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExamineTime = OK.ExamineTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";

                    B_IncreaseList wafrtbool = (from tbbool in MyModels.B_IncreaseList
                                                      where tbbool.IncreaseID == MyB_ConverList.IncreaseID
                                                      select tbbool).Single();//查询原状态
                    wafrtbool.ExamineNot = state;//改变是否为冲红单状态
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 删除（损益单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int increaseID)
        {
            string strMsg = "fail";
            try
            {

                B_IncreaseList conver = (from tbWarHouser in MyModels.B_IncreaseList
                                         where tbWarHouser.IncreaseID == increaseID
                                         select tbWarHouser).Single();
                MyModels.B_IncreaseList.Remove(conver);

                int waDetialid = (int)conver.IncreaseID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_IncreaseDetailList
                                    where tbWarHouserDetial.IncreaseID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_IncreaseDetailList.Remove(converDetial[i]);
                        MyModels.SaveChanges();
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

    }
}
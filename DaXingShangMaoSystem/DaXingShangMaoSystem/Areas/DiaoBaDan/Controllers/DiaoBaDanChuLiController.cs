using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DiaoBaDan.Controllers
{
    public class DiaoBaDanChuLiController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: DiaoBaDan/DiaoBaDanChuLi
        public ActionResult DiaoBaDan()
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
            var listy = (from tbem in MyModels.B_AllocateList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_AllocateList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.Remember.Substring(1, 8));
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
        /// 查询配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectConverList(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID
                        where tbConverlist.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            P_Remember = tbConverlist.P_Remember,//编号
                            payName = tbConverlist.payName,
                            furlName = tbConverlist.furlName,
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,
                            StockPlaceName = tbShouHuoBuMen.StockPlaceName,
                            Remarks = tbConverlist.Remarks
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
        /// 绑定配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingConver(int converID)
        {
            var linq = (from tbconverIDlist in MyModels.B_ConverList
                        where tbconverIDlist.ConverID == converID

                        select new LY.PeiHuoDan
                        {
                            ConverID = tbconverIDlist.ConverID,
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询商品批量设置
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPiGoodel(Vo.BsgridPage bsgridPage, int converIDGoodelID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_ConverList
                        join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbWanHofDeaill.ConverID == converIDGoodelID//根据配货明细中的“配货ID”
                        select new LY.PeiHuoDan
                        {
                            ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                            ConverID = tbSelectWanHtsList.ConverID,//配货ID

                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//进仓件数
                            Subdivision = tbWanHofDeaill.Subdivision,//入库细数

                            GoodsIDs = tbShangPin.GoodsID,//商品ID
                            GoodsCodes = tbShangPin.GoodsCode,//商品代码
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            ArtNos = tbShangPin.ArtNo,//商品货号

                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            PackContents = tbPackln.PackContent,//包装含量
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

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
        /// 绑定商品到（主界面）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, Array JieShouID, Array JieWarhou)
        {
            List<PeiHuoDan> list = new List<PeiHuoDan>();
            // int goodsIDs
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');

            string K = ((string[])JieWarhou)[0];
            string[] intsK = K.Split(',');

            for (int P = 0; P < intsK.Length;)
            {
                for (int i = 0; i < intsZ.Length;)
                {
                    var goodsIDs = Convert.ToInt32(intsZ[i]);
                    var converID = Convert.ToInt32(intsK[P]);
                    var linq = (from tbSelectWanHtsList in MyModels.B_ConverList//配货单
                                join tbKuCun in MyModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCun.StockPlaceID//库存地点
                                join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                                join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                                where tbWanHofDeaill.GoodsID == goodsIDs && tbWanHofDeaill.ConverID == converID//根据进仓明细中的“进仓ID”
                                select new LY.PeiHuoDan
                                {
                                    ConverID = tbSelectWanHtsList.ConverID,//配货ID
                                    ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                                    GoodsIDs = tbShangPin.GoodsID,//商品ID
                                    GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                    GoodsNames = tbShangPin.GoodsName,//商品名称

                                    PackContents = tbPackln.PackContent,//包装含量

                                    MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//进仓件数
                                    Subdivision = tbWanHofDeaill.Subdivision,//入库细数

                                    StockPlaceName = tbKuCun.StockPlaceName,//发货地点

                                    TaxBids = tbGoodDetail.TaxBid,//含税进价
                                    RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                                    SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                                    EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                    ArtNos = tbShangPin.ArtNo,//商品货号

                                }).ToList();
                    list.AddRange(linq);
                    P++;
                    i++;
                }
            }




            int totalRow = list.Count();
            List<LY.PeiHuoDan> notices = list.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 绑定商品到（批量选商品）左
        /// </summary>
        /// <returns></returns>
        public ActionResult PiPeiGoodel(Vo.BsgridPage bsgridPage, Array JieShouID, Array JieWarhou)
        {
            List<PeiHuoDan> list = new List<PeiHuoDan>();
            // int goodsIDs
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');

            string K = ((string[])JieWarhou)[0];
            string[] intsK = K.Split(',');

            for (int P = 0; P < intsK.Length;)
            {
                for (int i = 0; i < intsZ.Length;)
                {
                    var goodsIDs = Convert.ToInt32(intsZ[i]);
                    var wareHouID = Convert.ToInt32(intsK[P]);
                    var linq = (from tbSelectWanHtsList in MyModels.B_ConverList//进仓单
                                join tbKuCun in MyModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCun.StockPlaceID//库存地点
                                join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//进仓明细
                                join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                                where tbWanHofDeaill.GoodsID == goodsIDs && tbWanHofDeaill.ConverID == wareHouID//根据进仓明细中的“进仓ID”
                                select new LY.PeiHuoDan
                                {
                                    ConverID = tbSelectWanHtsList.ConverID,//进仓ID
                                    ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//进仓明细ID
                                    GoodsIDs = tbShangPin.GoodsID,//商品ID

                                    GoodsNames = tbShangPin.GoodsName,//商品名称
                                    MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//入库件数
                                    StockPlaceName = tbKuCun.StockPlaceName,//发货地点
                                    PackContents = tbPackln.PackContent,//包装含量
                                    GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                    TaxBids = tbGoodDetail.TaxBid,//含税进价
                                    RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                                    SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                                    EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                    ArtNos = tbShangPin.ArtNo,//商品货号

                                }).ToList();
                    list.AddRange(linq);
                    P++;
                    i++;
                }
            }
            int totalRow = list.Count();
            List<LY.PeiHuoDan> notices = list.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 绑定商品到（批量选商品）右
        /// </summary>
        /// <returns></returns>
        public ActionResult PiPeiGoodelright(Vo.BsgridPage bsgridPage, int wareHouID, int goodsIDs)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_ConverList//进仓单
                        join tbKuCun in MyModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCun.StockPlaceID//库存地点
                        join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//进仓明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbWanHofDeaill.GoodsID == goodsIDs && tbWanHofDeaill.ConverID == wareHouID//根据进仓明细中的“进仓ID”
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbSelectWanHtsList.ConverID,//进仓ID
                            ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//进仓明细ID
                            GoodsIDs = tbShangPin.GoodsID,//商品ID

                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//入库件数
                            StockPlaceName = tbKuCun.StockPlaceName,//发货地点
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbShangPin.ArtNo,//商品货号

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
        /// 随机（自动生成批号）
        /// </summary>
        /// <returns></returns>
        public ActionResult ZiDongPiHao(Array JieShouID, Array JieWarhou)
        {

            List<PeiHuoDan> list = new List<PeiHuoDan>();
            // int goodsIDs
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');

            string K = ((string[])JieWarhou)[0];
            string[] intsK = K.Split(',');

            for (int P = 0; P < intsK.Length;)
            {
                for (int i = 0; i < intsZ.Length;)
                {
                    var goodsIDs = Convert.ToInt32(intsZ[i]);
                    var wareHouID = Convert.ToInt32(intsK[P]);
                    var linq = (from tbSelectWanHtsList in MyModels.B_ConverList//配货单
                                join tbKuCun in MyModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCun.StockPlaceID//库存地点
                                join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                                join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                                where tbWanHofDeaill.GoodsID == goodsIDs && tbWanHofDeaill.ConverID == wareHouID//根据进仓明细中的“进仓ID”
                                select new LY.PeiHuoDan
                                {
                                    ConverID = tbSelectWanHtsList.ConverID,//配货ID
                                    ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                                    GoodsIDs = tbShangPin.GoodsID,//商品ID
                                    StockPlaceName = tbKuCun.StockPlaceName,//发货地点
                                    PackContents = tbPackln.PackContent,//包装含量
                                    GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                    TaxBids = tbGoodDetail.TaxBid,//含税进价
                                    EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位


                                }).ToList();
                    list.AddRange(linq);
                    P++;
                    i++;
                }
            }
            int totalRow = list.Count();
            int ZongShu = totalRow + 1;
            int ShuZiLenht = 0;
            Random randomBuilder = new Random();
            for (int MoRenMiMaa = 1; MoRenMiMaa < ZongShu; MoRenMiMaa++)
            {
                ShuZiLenht = randomBuilder.Next(ZongShu);
                if (ShuZiLenht == 0)
                {
                    return RedirectToAction("ZiDongPiHao");
                }
            }
            return Json(ShuZiLenht, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 新增 保存（调拨单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InterDiaoBa(B_AllocateList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array PiHaoShuZu)
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
                //四(批号)
                string C = ((string[])PiHaoShuZu)[0];
                string[] intsNPiHao = C.Split(',');



                B_AllocateList MyB_ConverList = new B_AllocateList();

                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.payNamel = OK.payNamel;
                MyB_ConverList.furlNamel = OK.furlNamel;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.SpouseBRanchIDtwo = OK.SpouseBRanchIDtwo;
                MyB_ConverList.Remarks = OK.Remarks;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyModels.B_AllocateList.Add(MyB_ConverList);
               
                if (MyModels.SaveChanges() > 0)
                {
                   

                    B_AllocateDetailList ConverDeTailList = new B_AllocateDetailList();

                    for (int H = 0; H < intsNXiShu.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length;)
                            {
                                for (int Pi = 0; Pi < intsNPiHao.Length; Pi++)
                                {
                                    ConverDeTailList.AllocateID = MyB_ConverList.AllocateID;//调拨单ID
                                    ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID
                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数

                                    ConverDeTailList.Number = Convert.ToDecimal(intsNPiHao[E]);//入库件数
                                    MyModels.B_AllocateDetailList.Add(ConverDeTailList);
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


        /// <summary>
        /// 查询调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectAlltoc(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbConverlist in MyModels.B_AllocateList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchIDtwo equals tbShouHuoBuMen.SpouseBRanchID
                        where tbConverlist.ExamineNot == false
                        select new LY.PeiHuoDan
                        {
                            AllocateID = tbConverlist.AllocateID,//id
                            converIDs = tbConverlist.ConverID,//配货id
                            Remember = tbConverlist.Remember,//编号
                            payName = tbConverlist.payNamel,
                            furlName = tbConverlist.furlNamel,
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,//发
                            SpouseBRanchNsame = tbShouHuoBuMen.SpouseBRanchName,//收
                            Remarks = tbConverlist.Remarks,

                            RegisterName = tbConverlist.RegisterName,
                            registerTime = tbConverlist.RegisterTime.ToString(),

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
        /// 绑定调拨单(一)
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingAlltoc(int allocateID)
        {
            var linq = (from tbConverlist in MyModels.B_AllocateList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchIDtwo equals tbShouHuoBuMen.SpouseBRanchID
                        where tbConverlist.AllocateID == allocateID
                        select new LY.PeiHuoDan
                        {
                            AllocateID = tbConverlist.AllocateID,//id
                            converIDs= tbConverlist.ConverID,//id
                            Remember = tbConverlist.Remember,//编号
                            payName = tbConverlist.payNamel,
                            furlName = tbConverlist.furlNamel,
                            SpouseBRanchID = tbFaHuoBuMen.SpouseBRanchID,//发
                            SpouseBRanchIDtwo = tbShouHuoBuMen.SpouseBRanchID,//收
                            Remarks = tbConverlist.Remarks,

                            RegisterName = tbConverlist.RegisterName,
                            registerTime = tbConverlist.RegisterTime.ToString(),

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 点击“”查询 绑定调拨单(二)商品
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShangPinZhuf(Vo.BsgridPage bsgridPage, int allocateID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_AllocateList
                        join tbWanHofDeaill in MyModels.B_AllocateDetailList on tbSelectWanHtsList.AllocateID equals tbWanHofDeaill.AllocateID//调拨明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbWanHofDeaill.AllocateID == allocateID//根据配货明细中的“调拨ID”
                        select new LY.PeiHuoDan
                        {
                            AllocateDetailID = tbWanHofDeaill.AllocateDetailID,//调拨明细ID
                            AllocateID = tbSelectWanHtsList.AllocateID,//调拨ID

                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//进仓件数
                            Subdivision = tbWanHofDeaill.Subdivision,//入库细数

                            GoodsIDs = tbShangPin.GoodsID,//商品ID
                            GoodsCodes = tbShangPin.GoodsCode,//商品代码
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            ArtNos = tbShangPin.ArtNo,//商品货号

                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            PackContents = tbPackln.PackContent,//包装含量
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
        /// 修改 保存（调拨单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptrnPeiZaiDan(B_AllocateList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array JieMingXiIDShuZu)
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
                string C = ((string[])JieMingXiIDShuZu)[0];
                string[] intsNPiHao = C.Split(',');
              

                B_AllocateList MyB_ConverList = new B_AllocateList();

                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.AllocateID = OK.AllocateID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.payNamel = OK.payNamel;
                MyB_ConverList.furlNamel = OK.furlNamel;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.SpouseBRanchIDtwo = OK.SpouseBRanchIDtwo;
                MyB_ConverList.Remarks = OK.Remarks;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                if (MyB_ConverList.AllocateID > 0)
                {
                    strMag = "succsee";

                    B_AllocateDetailList ConverDeTailList = new B_AllocateDetailList();

                    for (int H = 0; H < intsNXiShu.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length; d++)
                            {
                                for (int Pi = 0; Pi < intsNPiHao.Length; Pi++)
                                {
                                   
                                        ConverDeTailList.AllocateID = MyB_ConverList.AllocateID;//配货单ID
                                        ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID
                                        ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                        ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                        ConverDeTailList.AllocateDetailID = Convert.ToInt32(intsNPiHao[Pi]);//明细ID
                                        ConverDeTailList.Number = Convert.ToInt32(intsid[d]);//批号ID
                                        MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                                        MyModels.SaveChanges();//保存
                                        d++;
                                        H++;
                                        E++;
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
        /// 审核（调拨单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenHeZaiDan(B_AllocateList OK,bool state)
        {
            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                B_AllocateList MyB_ConverList = new B_AllocateList();

                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.AllocateID = OK.AllocateID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.payNamel = OK.payNamel;
                MyB_ConverList.furlNamel = OK.furlNamel;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.SpouseBRanchIDtwo = OK.SpouseBRanchIDtwo;
                MyB_ConverList.Remarks = OK.Remarks;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExecuteTime = OK.ExecuteTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                if (MyB_ConverList.AllocateID > 0)
                {
                    strMag = "succsee";
                    B_AllocateList wafrtbool = (from tbbool in MyModels.B_AllocateList
                                                where tbbool.AllocateID == MyB_ConverList.AllocateID
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
        /// 删除（反厂单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int allocateID)
        {
            string strMsg = "fail";
            try
            {

                B_AllocateList conver = (from tbWarHouser in MyModels.B_AllocateList
                                         where tbWarHouser.AllocateID == allocateID
                                         select tbWarHouser).Single();
                MyModels.B_AllocateList.Remove(conver);

                int waDetialid = (int)conver.AllocateID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_AllocateDetailList
                                    where tbWarHouserDetial.AllocateDetailID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_AllocateDetailList.Remove(converDetial[i]);
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
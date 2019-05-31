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
    public class YaoHuoDanController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: DiaoBaDan/YaoHuoDan
        public ActionResult WantGoods()
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
            var listy = (from tbem in MyModels.B_WanManifestList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_WanManifestList mymodell = listy[intcoun - 1];
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
        /// 查询销售单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectSellctXiang(Vo.BsgridPage bsgridPage,int sellID)
        {
            var linq = (from tbSellct in MyModels.B_SellList//销售单

                        join tbSpuBuMen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbSpuBuMen.SpouseBRanchID
                        where tbSellct.SellID == sellID
                        select new LY.PeiHuoDan
                        {
                            SellID = tbSellct.SellID,//销售id
                            Remember = tbSellct.Remember,//销售单编号
                            SpouseBRanchName = tbSpuBuMen.SpouseBRanchName,//新部门
                            PanlDate = tbSellct.PanlDate.ToString(),//启动计划日期
                            RegisterName = tbSellct.RegisterName,//制单人
                            registerTime = tbSellct.RegisterTime.ToString(),//制单时间
                            ExamineName = tbSellct.ExamineName,//审核人
                            examineTime = tbSellct.ExecuteTime.ToString(),//审核时间
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
        /// 点击商品到（主界面）看详细
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, int jiLuChaekID)
        {
          
                var linq = (from tbSellct in MyModels.B_SellList//销售单
                            join tbSellDatelst in MyModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                            join tbBumen in MyModels.S_SpouseBRanchList on tbSellct.SpouseBRanchID equals tbBumen.SpouseBRanchID
                            join tbConverDaTe in MyModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                            join tbGoods in MyModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                            join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                            where tbSellDatelst.SellDeTaliID == jiLuChaekID
                            select new LY.PeiHuoDan
                            {
                                SellDeTaliID = tbSellDatelst.SellDeTaliID,//销售明细id
                                GoodsIDs = tbGoods.GoodsID,//商品id
                                GoodsCode = tbGoods.GoodsCode,//代码
                                GoodsNames = tbGoods.GoodsName,//商品名称
                                PackContents = tbPackln.PackContent,//包装含量
                                GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                                MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                                Subdivision = tbConverDaTe.Subdivision,//
                                RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                                SpecificationsModels = tbGoods.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                ArtNos = tbGoods.ArtNo,//商品货号
                                SpouseBRanchName = tbBumen.SpouseBRanchName,//商品货号
                                TaxBids = tbGoodDetail.TaxBid,//含税进价
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
        /// 开始 新增 保存（返仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectPeiZaiDan(B_WanManifestList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu)
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


                int oldWareHouseRows = (from tb in MyModels.B_WanManifestList
                                        where tb.Remember == OK.Remember
                                        select tb).Count();

                if (oldWareHouseRows == 0)
                {
                    B_WanManifestList MyB_ConverList = new B_WanManifestList();

                    MyB_ConverList.SellID = OK.SellID;
                    MyB_ConverList.Remember = OK.Remember;

                    MyB_ConverList.PurchaseSectionID = OK.PurchaseSectionID;


                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;


                    MyModels.B_WanManifestList.Add(MyB_ConverList);
                    if (MyModels.SaveChanges() > 0)
                    {
                       

                        B_WanManifestDetailList ConverDeTailList = new B_WanManifestDetailList();

                        for (int H = 0; H < intsNXiShu.Length;)
                        {
                            for (int E = 0; E < intsRuJian.Length;)
                            {
                                for (int d = 0; d < intsid.Length;d++)
                                {
                                    ConverDeTailList.WanManifestID = MyB_ConverList.WanManifestID;//要货ID
                                    ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID
                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                    MyModels.B_WanManifestDetailList.Add(ConverDeTailList);
                                    MyModels.SaveChanges();//保存
                                    strMag = "succsee";
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
        /// 查询要货单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectWanMetr(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbWanMetre in MyModels.B_WanManifestList//要货单
                        join tbSpatouKrdg in MyModels.S_SpouseBRanchList on tbWanMetre.PurchaseSectionID equals tbSpatouKrdg.SpouseBRanchID

                        select new LY.PeiHuoDan
                        {
                            WanManifestID=tbWanMetre.WanManifestID,
                            SellID = tbWanMetre.SellID,//要货id
                            Remember = tbWanMetre.Remember,//编号
                            SpouseBRanchName = tbSpatouKrdg.SpouseBRanchName,//部门
                            RegisterName = tbWanMetre.RegisterName,//制单人
                            registerTime = tbWanMetre.RegisterTime.ToString(),//制单时间
                            ExamineNot = tbWanMetre.ExamineNot//
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
        /// 绑定要货单
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult BangDingWanMetr(int wanManifestID)
        {
            var linq = (from tbWanMetre in MyModels.B_WanManifestList//要货单
                        join tbSpatouKrdg in MyModels.S_SpouseBRanchList on tbWanMetre.PurchaseSectionID equals tbSpatouKrdg.SpouseBRanchID
                        where tbWanMetre.WanManifestID== wanManifestID
                        select new LY.PeiHuoDan
                        {
                            WanManifestID = tbWanMetre.WanManifestID,
                            SellID = tbWanMetre.SellID,//要货id
                            Remember = tbWanMetre.Remember,//编号
                            SpouseBRanchID = tbSpatouKrdg.SpouseBRanchID,//部门
                            RegisterName = tbWanMetre.RegisterName,//制单人
                            registerTime = tbWanMetre.RegisterTime.ToString(),//制单时间
                            ExamineNot = tbWanMetre.ExamineNot//
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }



        /// <summary>
        /// 绑定要货单商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="stockPlaceID"></param>
        /// <returns></returns>
        public ActionResult SelectSellctGoods(Vo.BsgridPage bsgridPage, int wanManifestID)
        {
            var linq = (from tbSellct in MyModels.B_WanManifestList//要货单
                        join tbSellDatelst in MyModels.B_WanManifestDetailList on tbSellct.WanManifestID equals tbSellDatelst.WanManifestID//要货明细单

                        join tbGoods in MyModels.B_GoodsList on tbSellDatelst.GoodsID equals tbGoods.GoodsID//商品

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbSellDatelst.WanManifestID == wanManifestID
                        select new LY.PeiHuoDan
                        {
                            WanManifestDetailID = tbSellDatelst.WanManifestDetailID,//要货明细id
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            MumberOfPackages = tbSellDatelst.MumberOfPackages,//
                            Subdivision = tbSellDatelst.Subdivision,//
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号
                        }).ToList();
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.SellID).//noboer表达式
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
        /// 开始 修改 保存（返仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptoctuFanDan(B_WanManifestList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array JieMingXiId)
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
                string idd = ((string[])JieMingXiId)[0];
                string[] intsNXiShuid = idd.Split(',');


                B_WanManifestList MyB_ConverList = new B_WanManifestList();
                MyB_ConverList.WanManifestID = OK.WanManifestID;
                MyB_ConverList.SellID = OK.SellID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.PurchaseSectionID = OK.PurchaseSectionID;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;


                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
                {


                    B_WanManifestDetailList ConverDeTailList = new B_WanManifestDetailList();

                    for (int H = 0; H < intsNXiShu.Length;)
                    {
                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length;)
                            {
                                for (int id = 0; id < intsNXiShuid.Length; id++)
                                {
                                    ConverDeTailList.WanManifestID = MyB_ConverList.WanManifestID;//要货ID
                                    ConverDeTailList.WanManifestDetailID = Convert.ToInt32(intsNXiShuid[id]); ;//要货明细ID
                                    ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID
                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                    MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
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
        /// 开始 审核（返仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShneheanDan(B_WanManifestList OK, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";

            try
            {
                B_WanManifestList MyB_ConverList = new B_WanManifestList();
                MyB_ConverList.WanManifestID = OK.WanManifestID;
                MyB_ConverList.SellID = OK.SellID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.PurchaseSectionID = OK.PurchaseSectionID;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExamineTime = OK.ExamineTime;


                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";
                    B_WanManifestList wafrtbool = (from tbbool in MyModels.B_WanManifestList
                                                   where tbbool.WanManifestID == MyB_ConverList.WanManifestID
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
        /// 删除（返仓单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int sellRetuerID)
        {
            string strMsg = "fail";
            try
            {

                B_WanManifestList conver = (from tbWarHouser in MyModels.B_WanManifestList
                                            where tbWarHouser.WanManifestID == sellRetuerID
                                           select tbWarHouser).Single();
                MyModels.B_WanManifestList.Remove(conver);

                int waDetialid = (int)conver.WanManifestID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_WanManifestDetailList
                                    where tbWarHouserDetial.WanManifestID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_WanManifestDetailList.Remove(converDetial[i]);
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
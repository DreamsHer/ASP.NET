using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi.Controllers
{
    public class GoodsSellTiaoZhengController : Controller
    {
        // GET: GoodsZhangGuanLi/GoodsSellTiaoZheng
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult TiaoZhengSell()
        {
            return View();
        }



        /// <summary>
        /// 下拉框 (买卖场)
        /// </summary>
        /// <returns></returns>
        public ActionResult ChaXunSeleSuppout()
        {
           
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
        
            List<SelectXiaLa> listNoticeTypeS = (from tbWaerHouse in myModels.S_MaiChangList
                                                 select new SelectXiaLa
                                                 {
                                                     id = tbWaerHouse.MaiChangID,
                                                     text = tbWaerHouse.MaiChangMC
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
        
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询盘点部门
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectDernrtName()
        {
            List<SelectVo> listGrade = (from tbDrufg in myModels.B_PanlList

                                           select new SelectVo
                                           {
                                               id = tbDrufg.DrugTypeID,
                                               text = tbDrufg.DrugType,
                                           }).ToList();

            listGrade = Common.Tools.SetSelectJson(listGrade);
            return Json(listGrade, JsonRequestBehavior.AllowGet);


        }


        /// <summary>
        /// 记录编号
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCodef()
        {
            string Std = "";
            var listy = (from tbem in myModels.B_SellCheckBatchList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_SellCheckBatchList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.Remember.Substring(1, 8));
                inemp++;
                Std = inemp.ToString();
                for (int i = 0; i < 5; i++)
                {
                    Std = Std.Length < 5 ? "0" + Std : Std;
                }
            }
            else
            {
                Std = "00001";
            }
            return Json(Std, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 查询商品代码（一）
        public ActionResult SelectSellctGoods(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSellct in myModels.B_SellList//销售单
                        join tbSellDatelst in myModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in myModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in myModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
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
        /// 绑定销售单商品（二）
        /// </summary>
        public ActionResult SelectSeBangllctGoods(Vo.BsgridPage bsgridPage,string goodsCode)
        {
            var linq = (from tbSellct in myModels.B_SellList//销售单
                        join tbSellDatelst in myModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in myModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in myModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品

                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        join tbConverlist in myModels.B_ConverList on tbSellDatelst.ConverID equals tbConverlist.ConverID//配货单

                        where tbGoods.GoodsCode== goodsCode
                        select new LY.PeiHuoDan
                        {
                            SellID = tbSellct.SellID,//销售id
                            SellDeTaliID = tbSellDatelst.SellDeTaliID,//销售明细id
                            ConverID = tbConverlist.ConverID,//配货id
                            ConverDeTaiID = tbConverDaTe.ConverDeTaiID,//配货明细id
                            GoodsIDs = tbGoods.GoodsID,//商品id

                            GoodsCode = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            PackContents = tbPackln.PackContent,//包装含量
                     
                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            Number=tbConverDaTe.Number,

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
        /// 查询商品代码（三）
        public ActionResult SelectSellctGoodCodes(string goodsCode)
        {
            var linq = (from tbGoods in myModels.B_GoodsList//商品
                        where tbGoods.GoodsCode== goodsCode
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbGoods.GoodsID,//商品id
                            GoodsCode = tbGoods.GoodsCode,//代码
                         
                        }).ToList();
          
            return Json(linq, JsonRequestBehavior.AllowGet);

        }



        /// <summary>
        /// 绑定销售单商品（四（新旧批次））
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSellctGoodsfd(int converDeTaiID)
        {
            var linq = (from tbSellct in myModels.B_SellList//销售单
                        join tbSellDatelst in myModels.B_SellDeTaLsit on tbSellct.SellID equals tbSellDatelst.SellID//销售明细单
                        join tbConverDaTe in myModels.B_ConverDeTailList on tbSellDatelst.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in myModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品
                      

                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbConverlist in myModels.B_ConverList on tbSellDatelst.ConverID equals tbConverlist.ConverID//配货单

                        where tbConverDaTe.ConverDeTaiID == converDeTaiID
                        select new LY.PeiHuoDan
                        {
                         
                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            Number = tbConverDaTe.Number,
                        }).ToList();
         
            return Json(linq, JsonRequestBehavior.AllowGet);

        }




        /// <summary>
        /// 新增 保存（调拨单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InterDiaoBa(B_SellCheckBatchList OK, B_SellCheckBatchDetaiList OKDetai)
        {
            string strMag = "fali";

            try
            {

                B_SellCheckBatchList MyB_ConverList = new B_SellCheckBatchList();

              
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.MaiChangID = OK.MaiChangID;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
        
                MyB_ConverList.DrugTypeID = OK.DrugTypeID;
                MyB_ConverList.StaffID = OK.StaffID;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                myModels.B_SellCheckBatchList.Add(MyB_ConverList);

                if (myModels.SaveChanges() > 0)
                {

                    B_SellCheckBatchDetaiList ConverDeTailList = new B_SellCheckBatchDetaiList();

                    ConverDeTailList.SellCheckBatchID = MyB_ConverList.SellCheckBatchID;
                    ConverDeTailList.MumberOfPackages = OKDetai.MumberOfPackages;
                    ConverDeTailList.ConverDeTaiID = OKDetai.ConverDeTaiID;
                    myModels.B_SellCheckBatchDetaiList.Add(ConverDeTailList);
                    myModels.SaveChanges();
                    strMag = "succsee";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 查询销售调整单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectAlltoc(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbConverlist in myModels.B_SellCheckBatchList
                        join tbFaHuoBuMen in myModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbMaiChang in myModels.S_MaiChangList on tbConverlist.MaiChangID equals tbMaiChang.MaiChangID
                        join tbPanBuMen in myModels.B_PanlList on tbConverlist.DrugTypeID equals tbPanBuMen.DrugTypeID
                        join tbSatll in myModels.B_StaffList on tbConverlist.StaffID equals tbSatll.StaffID//员工
                        join tbSeChdeDetail in myModels.B_SellCheckBatchDetaiList on tbConverlist.SellCheckBatchID equals tbSeChdeDetail.SellCheckBatchID//明细
                        where tbConverlist.ExamineNot == false
                        select new LY.PeiHuoDan
                        {
                            AllocateID = tbConverlist.SellCheckBatchID,//id
                            converIDs = tbSeChdeDetail.SellCheckBatchDetailD,//明细id
                            ConverDeTaiID = tbSeChdeDetail.ConverDeTaiID,//配货明细id

                            Remember = tbConverlist.Remember,//编号
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,
                            furlName = tbMaiChang.MaiChangMC,
                            payName = tbPanBuMen.DrugType,//盘点部门
                            MumberOfPackages = tbSeChdeDetail.MumberOfPackages,//调整数
                            Remarks = tbSatll.StaffName,
                            StaffID = tbSatll.StaffID,
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
        /// 绑定销售调整单
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingctAlltoc(int sellCheckBatchID)
        {
            var linq = (from tbConverlist in myModels.B_SellCheckBatchList
                        join tbFaHuoBuMen in myModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbMaiChang in myModels.S_MaiChangList on tbConverlist.MaiChangID equals tbMaiChang.MaiChangID
                        join tbPanBuMen in myModels.B_PanlList on tbConverlist.DrugTypeID equals tbPanBuMen.DrugTypeID
                        join tbSatll in myModels.B_StaffList on tbConverlist.StaffID equals tbSatll.StaffID//员工
                        join tbSeChdeDetail in myModels.B_SellCheckBatchDetaiList on tbConverlist.SellCheckBatchID equals tbSeChdeDetail.SellCheckBatchID//明细

                        join tbConverDaTe in myModels.B_ConverDeTailList on tbSeChdeDetail.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in myModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品

                        where tbConverlist.SellCheckBatchID == sellCheckBatchID
                        select new LY.PeiHuoDan
                        {
                            AllocateID = tbConverlist.SellCheckBatchID,//id
                            converIDs = tbSeChdeDetail.SellCheckBatchDetailD,//明细id
                            GoodsCode=tbGoods.GoodsCode,
                            Remember = tbConverlist.Remember,//编号

                            SpouseBRanchID = tbFaHuoBuMen.SpouseBRanchID,
                            GoodsIDs = tbMaiChang.MaiChangID,
                            DrugTypeID = tbPanBuMen.DrugTypeID,//盘点部门

                            MumberOfPackages = tbSeChdeDetail.MumberOfPackages,//调整数
                            Remarks = tbSatll.StaffName,
                            registerTime = tbConverlist.RegisterTime.ToString(),

                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);

        }



        /// <summary>
        /// 绑定调整单商品（
        /// </summary>
        /// <returns></returns>
        public ActionResult SdSellctGoodsfd(Vo.BsgridPage bsgridPage,int converDeTaiID)
        {
            var linq = (from tbConverlist in myModels.B_SellCheckBatchList
                        join tbSeChdeDetail in myModels.B_SellCheckBatchDetaiList on tbConverlist.SellCheckBatchID equals tbSeChdeDetail.SellCheckBatchID//明细
                        join tbConverDaTe in myModels.B_ConverDeTailList on tbSeChdeDetail.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in myModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品


                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                     

                        where tbSeChdeDetail.SellCheckBatchDetailD == converDeTaiID
                        select new LY.PeiHuoDan
                        {
                            GoodsNames=tbGoods.GoodsName,
                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            Number = tbConverDaTe.Number,
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
        /// 绑定调整单商品（旧批次）
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSedllctGoodsdsfd(int converDeTaiID)
        {
            var linq = (from tbConverlist in myModels.B_SellCheckBatchList
                        join tbSeChdeDetail in myModels.B_SellCheckBatchDetaiList on tbConverlist.SellCheckBatchID equals tbSeChdeDetail.SellCheckBatchID//明细
                        join tbConverDaTe in myModels.B_ConverDeTailList on tbSeChdeDetail.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in myModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品


                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细


                        where tbSeChdeDetail.SellCheckBatchDetailD == converDeTaiID
                        select new LY.PeiHuoDan
                        {

                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            Number = tbConverDaTe.Number,
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);

        }



        /// <summary>
        /// 修改 保存（销售调整单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UpterDiaoBa(B_SellCheckBatchList OK, B_SellCheckBatchDetaiList OKDetai)
        {
            string strMag = "fali";

            try
            {

                B_SellCheckBatchList MyB_ConverList = new B_SellCheckBatchList();

                MyB_ConverList.SellCheckBatchID = OK.SellCheckBatchID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.MaiChangID = OK.MaiChangID;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.DrugTypeID = OK.DrugTypeID;
                MyB_ConverList.StaffID = OK.StaffID;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                myModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();

               
                    B_SellCheckBatchDetaiList ConverDeTailList = new B_SellCheckBatchDetaiList();

                    ConverDeTailList.SellCheckBatchDetailD = OKDetai.SellCheckBatchDetailD;
                    ConverDeTailList.SellCheckBatchID = OK.SellCheckBatchID;
                    ConverDeTailList.MumberOfPackages = OKDetai.MumberOfPackages;
                    ConverDeTailList.ConverDeTaiID = OKDetai.ConverDeTaiID;

                    myModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                    myModels.SaveChanges();
                    strMag = "succsee";
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 删除（调整单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int allocateID)
        {
            string strMsg = "fail";
            try
            {

                B_SellCheckBatchList conver = (from tbWarHouser in myModels.B_SellCheckBatchList
                                         where tbWarHouser.SellCheckBatchID == allocateID
                                         select tbWarHouser).Single();
                myModels.B_SellCheckBatchList.Remove(conver);

                int waDetialid = (int)conver.SellCheckBatchID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in myModels.B_SellCheckBatchDetaiList
                                    where tbWarHouserDetial.SellCheckBatchID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        myModels.B_SellCheckBatchDetaiList.Remove(converDetial[i]);
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
        /// 审核（调拨单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenHeZaiDan(B_SellCheckBatchList OK, B_SellCheckBatchDetaiList OKDetai, bool state)
        {
            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                B_SellCheckBatchList MyB_ConverList = new B_SellCheckBatchList();

                MyB_ConverList.SellCheckBatchID = OK.SellCheckBatchID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.MaiChangID = OK.MaiChangID;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.DrugTypeID = OK.DrugTypeID;
                MyB_ConverList.StaffID = OK.StaffID;
                MyB_ConverList.RegisterTime = OK.RegisterTime;
                MyB_ConverList.StaffIDTwo = OK.StaffIDTwo;
                MyB_ConverList.ExamineTime = OK.ExamineTime;

                myModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();

                if (MyB_ConverList.SellCheckBatchID > 0)
                {
                    strMag = "succsee";
                    B_SellCheckBatchList wafrtbool = (from tbbool in myModels.B_SellCheckBatchList
                                                where tbbool.SellCheckBatchID == OK.SellCheckBatchID
                                                select tbbool).Single();//查询原状态
                    wafrtbool.ExamineNot = state;//改变是否为冲红单状态
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

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }


      //第二部分
      /// <summary>
      /// 查询已审核的销售调整单
      /// </summary>
      /// <returns></returns>
        public ActionResult TiaoZhengSellJieMian()
        {
            return View();
        }

        /// <summary>
        /// 查询销售调整单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectAlltocYiShen(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbConverlist in myModels.B_SellCheckBatchList
                        join tbFaHuoBuMen in myModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbMaiChang in myModels.S_MaiChangList on tbConverlist.MaiChangID equals tbMaiChang.MaiChangID
                        join tbPanBuMen in myModels.B_PanlList on tbConverlist.DrugTypeID equals tbPanBuMen.DrugTypeID
                        join tbSatll in myModels.B_StaffList on tbConverlist.StaffID equals tbSatll.StaffID//员工
                        join tbSatlltw in myModels.B_StaffList on tbConverlist.StaffIDTwo equals tbSatlltw.StaffID//员工2
                        join tbSeChdeDetail in myModels.B_SellCheckBatchDetaiList on tbConverlist.SellCheckBatchID equals tbSeChdeDetail.SellCheckBatchID//明细
                        where tbConverlist.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            AllocateID = tbConverlist.SellCheckBatchID,//id
                            Remember = tbConverlist.Remember,//编号
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,
                            furlName = tbMaiChang.MaiChangMC,
                            payName = tbPanBuMen.DrugType,//盘点部门
                            MumberOfPackages = tbSeChdeDetail.MumberOfPackages,//调整数
                            Remarks = tbSatll.StaffName,
                            StaffID = tbSatll.StaffID,
                            registerTime = tbConverlist.RegisterTime.ToString(),

                            StaffIDtwo = tbSatlltw.StaffID,
                            examineTime = tbConverlist.RegisterTime.ToString(),
                            ExamineNot = tbConverlist.ExamineNot,
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
        /// 绑定调整单商品（
        /// </summary>
        /// <returns></returns>
        public ActionResult DuiYingSdSellctGoodsfd(Vo.BsgridPage bsgridPage, int AllocateID)
        {
            var linq = (from tbConverlist in myModels.B_SellCheckBatchList
                        join tbSeChdeDetail in myModels.B_SellCheckBatchDetaiList on tbConverlist.SellCheckBatchID equals tbSeChdeDetail.SellCheckBatchID//明细
                        join tbConverDaTe in myModels.B_ConverDeTailList on tbSeChdeDetail.ConverDeTaiID equals tbConverDaTe.ConverDeTaiID//配货明细单
                        join tbGoods in myModels.B_GoodsList on tbConverDaTe.GoodsID equals tbGoods.GoodsID//商品


                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细


                        where tbSeChdeDetail.SellCheckBatchID == AllocateID
                        select new LY.PeiHuoDan
                        {
                            GoodsNames = tbGoods.GoodsName,
                            MumberOfPackages = tbConverDaTe.MumberOfPackages,//
                            Subdivision=tbSeChdeDetail.MumberOfPackages,
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            Number = tbConverDaTe.Number,
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

    }
}
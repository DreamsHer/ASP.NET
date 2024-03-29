﻿using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.JinCangChongHong.Controllers
{
    public class ZuLinController : Controller
    {
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: JinCangChongHong/ZuLin
        public ActionResult ZuLin()
        {
            return View();
        }


        /// <summary>
        /// 点击“”查询 进仓单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult ChaXunJinCang(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSelctWanrHon in myModels.B_WareHouseList//进仓单

                        join tbJinHuoBumen in myModels.S_SpouseBRanchList on tbSelctWanrHon.SpouseBRanchID equals tbJinHuoBumen.SpouseBRanchID//进货部门
                        join tbKuCun in myModels.S_StockPlaceList on tbSelctWanrHon.StockPlaceID equals tbKuCun.StockPlaceID//库存地点

                        join tbStall in myModels.B_StaffList on tbSelctWanrHon.StaffID equals tbStall.StaffID//员工

                        join tbHeTong in myModels.B_OrderFormPactList on tbSelctWanrHon.OrderFormPactID equals tbHeTong.OrderFormPactID//合同单
                        join tbGongHuoShang in myModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuoShang.SupplierID//供货商
                        join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算方式
                        where tbSelctWanrHon.QuFenLeiXingID == 4 && tbSelctWanrHon.ExamineNot == true && tbSelctWanrHon.CrushRedNot == false
                        select new LY.SelectOrderPact
                        {
                            WareHouseID = tbSelctWanrHon.WareHouseID,//进仓单
                            Remember = tbSelctWanrHon.Remember,//记录编号

                            SpouseBRanchName = tbJinHuoBumen.SpouseBRanchName,//进货部门
                            StockPlaceName = tbKuCun.StockPlaceName,//库存地点

                            Appendix = tbSelctWanrHon.Appendix,//附件
                            Evidences = tbSelctWanrHon.Evidences,//原始单号
                            StaffCode = tbStall.StaffCode,//收货人编号
                            StaffName = tbStall.StaffName,//收货人姓名
                            RegisterTime = tbSelctWanrHon.RegisterTime.ToString(),//登记时间

                            OrderFormPactIDs = tbSelctWanrHon.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同号
                            MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方法
                            SupplierCHName = tbGongHuoShang.SupplierCHName,//供货商单位
                            ExamineNot = tbSelctWanrHon.ExamineNot,//审核否
                            Status = tbSelctWanrHon.Status//生效否

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.SelectOrderPact> notices = linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.SelectOrderPact> bsgrid = new Vo.Bsgrid<LY.SelectOrderPact>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 点击绑定 进仓单 （一）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult BangDingLianYing(int wareHouseID1)
        {
            var linq = (from tbSelctWanrHon in myModels.B_WareHouseList//进仓单

                        join tbJinHuoBumen in myModels.S_SpouseBRanchList on tbSelctWanrHon.SpouseBRanchID equals tbJinHuoBumen.SpouseBRanchID//进货部门
                        join tbKuCun in myModels.S_StockPlaceList on tbSelctWanrHon.StockPlaceID equals tbKuCun.StockPlaceID//库存地点

                        join tbStall in myModels.B_StaffList on tbSelctWanrHon.StaffID equals tbStall.StaffID//员工

                        join tbHeTong in myModels.B_OrderFormPactList on tbSelctWanrHon.OrderFormPactID equals tbHeTong.OrderFormPactID//合同单
                        join tbGongHuoShang in myModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuoShang.SupplierID//供货商
                        join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算方式
                        where tbSelctWanrHon.QuFenLeiXingID == 4 && tbSelctWanrHon.ExamineNot == true && tbSelctWanrHon.WareHouseID == wareHouseID1
                        select new LY.SelectOrderPact
                        {
                            WareHouseID = tbSelctWanrHon.WareHouseID,//进仓单
                            Remember = tbSelctWanrHon.Remember,//记录编号
                            SpouseBRanchID = tbJinHuoBumen.SpouseBRanchID,//进货部门
                            StockPlaceID = tbKuCun.StockPlaceID,//库存地点

                            Appendix = tbSelctWanrHon.Appendix,//附件
                            Evidences = tbSelctWanrHon.Evidences,//原始单号
                            StaffID = tbSelctWanrHon.StaffID,//登记人ID
                            StaffCode = tbStall.StaffCode,//收货人编号
                            StaffName = tbStall.StaffName,//收货人姓名
                            RegisterName = tbSelctWanrHon.RegisterName,//登记人
                            RegisterTime = tbSelctWanrHon.RegisterTime.ToString(),//登记时间

                            OrderFormPactIDs = tbSelctWanrHon.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同号
                            MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方法
                            SupplierCHName = tbGongHuoShang.SupplierCHName,//供货商单位

                            ExamineName = tbSelctWanrHon.ExamineName,//审核人
                            ExamineTime = tbSelctWanrHon.ExamineTime.ToString(),//审核人时间

                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 点击“修改”查询 绑定进仓单(二)商品
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="WareHouseID1"></param>
        /// <returns></returns>
        public ActionResult SelectShangPinZhuf(Vo.BsgridPage bsgridPage, int wareHousefID1)
        {
            var linq = (from tbSelectWanHtsList in myModels.B_WareHouseList
                        join tbWanHofDeaill in myModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                        join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbWanHofDeaill.WareHouseID == wareHousefID1//根据进仓明细中的“进仓ID”
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID
                            WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID

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
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 点击保存冲红（进仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ChongHongWareHouss(Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu, bool state)
        {
            //一
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');
            //二
            string M = ((string[])JieShouJianShu)[0];
            string[] intsM = M.Split(',');
            //三
            string N = ((string[])JieShouRuKuXiShu)[0];
            string[] intsN = N.Split(',');


            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                B_WareHouseList WareHouse = new B_WareHouseList();

                WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                WareHouse.Remember = Request.Form["Remember"];//记录编号
                WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["PurchaseSectionID"]);//进货部门
                WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["CountersellID"]);//库存地点
                WareHouse.Appendix = Request.Form["Appendix"];//附件张数
                WareHouse.Evidences = Request.Form["Evidences"];//原始单号
                WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人ID
                WareHouse.RegisterName = Request.Form["RegisterName"];//登记人
                WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间
                WareHouse.ExamineName = Request.Form["ExamineName"];//审核人
                WareHouse.ExamineTime = Convert.ToDateTime(Request.Form["ExamineTime"]);//审核时间

                myModels.B_WareHouseList.Add(WareHouse);
                myModels.SaveChanges();

                if (WareHouse.WareHouseID > 0)
                {
                    B_WareHouseDetiailList WareHouseDetiail = new B_WareHouseDetiailList();

                    for (int H = 0; H < intsN.Length;)
                    {
                        for (int E = 0; E < intsM.Length;)
                        {
                            for (int d = 0; d < intsZ.Length; d++)
                            {
                                WareHouseDetiail.WareHouseID = WareHouse.WareHouseID;//进货单ID
                                WareHouseDetiail.OrderFormDetailID = Convert.ToInt32(intsZ[d]); ;//商品ID
                                WareHouseDetiail.Subdivision = Convert.ToDecimal(intsN[H]);//入库细数
                                WareHouseDetiail.MumberOfPackages = Convert.ToDecimal(intsM[E]);//入库件数
                                myModels.B_WareHouseDetiailList.Add(WareHouseDetiail);
                                myModels.SaveChanges();//保存
                                H++;
                                E++;
                            }
                        }
                    }

                    B_WareHouseList wafrtbool = (from tbbool in myModels.B_WareHouseList
                                                 where tbbool.WareHouseID == WareHouse.WareHouseID
                                                 select tbbool).Single();//查询原状态
                    wafrtbool.ExamineNot = state;//改变审核
                    myModels.Entry(wafrtbool).State = EntityState.Modified;

                    B_WareHouseList wafrtboodl = (from tbbool in myModels.B_WareHouseList
                                                  where tbbool.WareHouseID == WareHouse.WareHouseID
                                                  select tbbool).Single();//查询原状态
                    wafrtboodl.Status = state;//改变生效
                    myModels.Entry(wafrtboodl).State = EntityState.Modified;


                    B_WareHouseList wafrtboosdl = (from tbbool in myModels.B_WareHouseList
                                                   where tbbool.WareHouseID == WareHouse.WareHouseID
                                                   select tbbool).Single();//查询原状态
                    wafrtboosdl.CrushRedNot = state;//改变冲红
                    myModels.Entry(wafrtboosdl).State = EntityState.Modified;

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
                    strMag = "success";
                }

                //return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        ///冲红状态（bit）（二）原来的 单号
        /// </summary>
        public ActionResult UpdateExamineNot(int WarHouserid, bool state)
        {
            //添加（这个类（bit 类型类））
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                B_WareHouseList wafrtbool = (from tbbool in myModels.B_WareHouseList
                                             where tbbool.WareHouseID == WarHouserid
                                             select tbbool).Single();//查询原状态
                wafrtbool.CrushRedNot = state;//改变是否为冲红单状态
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                returnJson.State = false;
                returnJson.Text = "数据异常";
            }
            return Json(returnJson, JsonRequestBehavior.AllowGet);
        }

    }
}
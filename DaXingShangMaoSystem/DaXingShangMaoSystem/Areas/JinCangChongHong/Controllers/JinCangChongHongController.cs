using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.JinCangChongHong.Controllers
{
    public class JinCangChongHongController : Controller
    {

        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        /// <summary>
        /// 冲红界面
        /// </summary>
        /// <returns></returns>
        public ActionResult JinCangDanChongHong()
        {
            return View();
        }

        /// <summary>
        /// 点击 查询 进仓单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult ChaXunJinCang(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSelectWanHtsList in myModels.B_WareHouseList//进仓单ID
                        join tbSelectOrderForm in myModels.B_OrderFormList on tbSelectWanHtsList.OrderFormID equals tbSelectOrderForm.OrderFormID//订单ID
                        join tbHeTong in myModels.B_OrderFormPactList on tbSelectWanHtsList.OrderFormPactID equals tbHeTong.OrderFormPactID//合同ID

                        join tbJieSuanFangShi in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuanFangShi.MethodOfSettlingAccountsID//结算方式
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbSelectWanHtsList.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//配货部门
                        join tbDingDanLeiXing in myModels.S_OrderFormTypeList on tbSelectOrderForm.OrderFormTypeID equals tbDingDanLeiXing.OrderFormTypeID//订单类别
                        join tbKuCuDiDian in myModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCuDiDian.StockPlaceID//库存地点
                        join tbStaill in myModels.B_StaffList on tbSelectWanHtsList.StaffID equals tbStaill.StaffID//收货人

                        where tbSelectWanHtsList.ExamineNot == true && tbSelectWanHtsList.QuFenLeiXingID == 1 && tbSelectWanHtsList.CrushRedNot == false && tbSelectWanHtsList.PeiHuoNot==false
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                            Remember = tbSelectWanHtsList.Remember,//进仓编号
                            OrdernFromIDs = tbSelectOrderForm.OrderFormID,//订单ID

                            OrderNumber = tbSelectOrderForm.OrderNumber,//订单编号
                            OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同编号
                            OrderFormTypeName = tbDingDanLeiXing.OrderFormTypeName,//订单类型
                            MethodOfSettlingAccounts = tbJieSuanFangShi.MethodOfSettlingAccounts,//结算方式
                            SpouseBRanchName = tbPeiHuoBuMen.SpouseBRanchName,//进货部门
                            StockPlaceName = tbKuCuDiDian.StockPlaceName,//库存地点
                            StaffID = tbStaill.StaffID,//收货人ID
                            StaffCode = tbStaill.StaffCode,//收货人编号
                            StaffName = tbStaill.StaffName,//收货人姓名

                            Value = tbSelectOrderForm.Value,//价款
                            ExpensesOTtaxation = tbSelectOrderForm.ExpensesOTtaxation,//税金
                            ValueTotal = tbSelectOrderForm.ValueTotal,//价税合计
                            ExamineNot = tbSelectWanHtsList.ExamineNot,//审核否

                            Status = tbSelectWanHtsList.Status//生效否（状态）
                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
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
        /// 点击选择“进仓单”绑定到界面（一）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult BangSahfnJinCang(int WareHouseID)
        {
            var linq = (from tbWanHtsList in myModels.B_WareHouseList//进仓
                        join tbSelectOrderForm in myModels.B_OrderFormList on tbWanHtsList.OrderFormID equals tbSelectOrderForm.OrderFormID//订单
                        join tbHeTong in myModels.B_OrderFormPactList on tbWanHtsList.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbGongHuoShang in myModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuoShang.SupplierID//供货商ID
                        join tbJieSuanFangShi in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuanFangShi.MethodOfSettlingAccountsID//结算方式
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbWanHtsList.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//配货部门
                        join tbDingDanLeiXing in myModels.S_OrderFormTypeList on tbSelectOrderForm.OrderFormTypeID equals tbDingDanLeiXing.OrderFormTypeID//订单类别
                        join tbKuCuDiDian in myModels.S_StockPlaceList on tbWanHtsList.StockPlaceID equals tbKuCuDiDian.StockPlaceID//库存地点
                        join tbStaill in myModels.B_StaffList on tbWanHtsList.StaffID equals tbStaill.StaffID//收货人
                        where tbWanHtsList.QuFenLeiXingID == 1 && tbWanHtsList.ExamineNot == true && tbWanHtsList.WareHouseID == WareHouseID
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbWanHtsList.WareHouseID,//进仓ID
                            Remember = tbWanHtsList.Remember,//进仓编号
                            OrdernFromIDs = tbSelectOrderForm.OrderFormID,//订单ID
                            OrderNumber = tbSelectOrderForm.OrderNumber,//订单编号
                            OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同编号
                            OrderFormTypeName = tbDingDanLeiXing.OrderFormTypeName,//订单类型
                            MethodOfSettlingAccounts = tbJieSuanFangShi.MethodOfSettlingAccounts,//结算方式
                            SupplierCHName = tbGongHuoShang.SupplierCHName,//供货商名称
                            SpouseBRanchID = tbPeiHuoBuMen.SpouseBRanchID,//进货部门
                            StockPlaceID = tbKuCuDiDian.StockPlaceID,//库存地点
                            StaffID = tbStaill.StaffID,//收货人ID
                            StaffCode = tbStaill.StaffCode,//收货人编号
                            StaffName = tbStaill.StaffName,//收货人姓名
                            Value = tbSelectOrderForm.Value,//价款
                            ExpensesOTtaxation = tbSelectOrderForm.ExpensesOTtaxation,//税金
                            ValueTotal = tbSelectOrderForm.ValueTotal,//价税合计
                            RegisterName = tbWanHtsList.RegisterName,//登记人
                            RegisterTime = tbWanHtsList.RegisterTime.ToString(),//登记时间
                            ExamineName = tbWanHtsList.ExamineName,//审核人
                            ExamineTime = tbWanHtsList.ExamineTime.ToString(),//审核时间
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);



        }

        /// <summary>
        ///  点击选择“进仓单”绑定到界面( 附 带 商品table)（二）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangSahfnJinCangShangPin(Vo.BsgridPage bsgridPage, int WareHouseID1)
        {
            try
            {
                var linq = (from tbSelectWanHtsList in myModels.B_WareHouseList//进仓单ID
                            join tbWanHofDeaill in myModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                            join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品

                            join tbShangPinDeill in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbShangPinDeill.GoodsID//商品明细
                            join tbBaoZhuangXin in myModels.S_PackInfoIDList on tbShangPinDeill.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位

                            where tbWanHofDeaill.WareHouseID == WareHouseID1//根据进仓明细中的“进仓ID”
                            select new LY.WareHouseDeitaLL
                            {
                                WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID
                                WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                                GoodsIDs = tbShangPin.GoodsID,//商品ID
                                GoodsCodes = tbShangPin.GoodsCode,//商品代码
                                GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                GoodsNames = tbShangPin.GoodsName,//商品名称
                                ArtNos = tbShangPin.ArtNo,//商品货号
                                SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                TaxBids = tbShangPinDeill.TaxBid,//含税进价
                                PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                                RetailMonovalents = tbShangPinDeill.RetailMonovalent,//零售单价

                                MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//进仓件数
                                Subdivision = tbWanHofDeaill.Subdivision,//入库细数

                            }).ToList();

                int totalRow = linq.Count();
                List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
                     Skip(bsgridPage.GetStartIndex()).//F12（看）
                    Take(bsgridPage.pageSize).
                    ToList();

                Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
                bsgrid.success = true;
                bsgrid.totalRows = totalRow;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.data = notices;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 开始 新增 保存（进仓冲红单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectJingCangDan(B_WareHouseList WareHouse, Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu, bool state)
        {
            string strMag = "fali";
            //添加（这个类（bit 类型类））
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {

                //声明字符串接收“数组的信息”
                //一
                string Z = ((string[])JieShouID)[0];
                string[] intsZ = Z.Split(',');//剪切出来
                //二
                string M = ((string[])JieShouJianShu)[0];
                string[] intsM = M.Split(',');
                //三
                string N = ((string[])JieShouRuKuXiShu)[0];
                string[] intsN = N.Split(',');



                //判断记录编号是否存在
                int oldWareHouseRows = (from tbWareHouse in myModels.B_WareHouseList
                                        where tbWareHouse.Remember == WareHouse.Remember
                                        select tbWareHouse).Count();
                if (oldWareHouseRows == 0)
                {
                    WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                    WareHouse.Remember = Request.Form["CrRemember"];//记录编号
                    WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);//进货部门
                    WareHouse.OrderFormID = Convert.ToInt32(Request.Form["OrdernFromIDs"]);//订单ID
                    WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                    WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["StockPlaceID"]);//库存地点
                    WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人
                    WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间
                    WareHouse.RegisterName = Request.Form["RegisterName"];//登记人
                    WareHouse.ExamineName = Request.Form["ExamineName"];//审核人
                    WareHouse.ExamineTime = Convert.ToDateTime(Request.Form["ExamineTime"]);//审核时间
                    WareHouse.Appendix = Request.Form["Appendix"];//附件
                    WareHouse.Evidences = Request.Form["Evidences"];//原始单号

                    if (WareHouse.OrderFormID > 0 && WareHouse.SpouseBRanchID > 0 && WareHouse.StockPlaceID > 0 && WareHouse.StaffID > 0
                        && WareHouse.RegisterName != null && WareHouse.RegisterTime != null && WareHouse.ExamineName != null && WareHouse.ExamineTime != null)
                    {
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
                                        WareHouseDetiail.OrderFormDetailID = Convert.ToInt32(intsZ[d]); ;//订单明细ID
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
                            wafrtbool.CrushRedNot = state;//改变是否为冲红单状态
                            myModels.Entry(wafrtbool).State = EntityState.Modified;


                            B_WareHouseList wafrtboodl = (from tbbool in myModels.B_WareHouseList
                                                          where tbbool.WareHouseID == WareHouse.WareHouseID
                                                          select tbbool).Single();//查询原状态
                            wafrtboodl.ExamineNot = state;//改变审核状态
                            myModels.Entry(wafrtboodl).State = EntityState.Modified;


                            B_WareHouseList wafrtboole = (from tbbool in myModels.B_WareHouseList
                                                          where tbbool.WareHouseID == WareHouse.WareHouseID
                                                          select tbbool).Single();//查询原状态
                            wafrtboole.Status = state;//改变生效状态
                            myModels.Entry(wafrtboole).State = EntityState.Modified;

                            B_WareHouseList wafrtboojgfle = (from tbbool in myModels.B_WareHouseList
                                                             where tbbool.WareHouseID == WareHouse.WareHouseID
                                                             select tbbool).Single();//查询原状态
                            wafrtboojgfle.HoveNot = state;//改变是否订单
                            myModels.Entry(wafrtboojgfle).State = EntityState.Modified;


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
                    else
                    {
                        return Json("fail", JsonRequestBehavior.AllowGet);
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
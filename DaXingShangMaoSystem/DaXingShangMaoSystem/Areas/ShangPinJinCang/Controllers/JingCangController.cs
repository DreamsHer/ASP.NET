using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.ShangPinJinCang.Controllers
{
    public class JingCangController : Controller
    {
        // GET: ShangPinJinCang/JingCang

        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult ZiYingJinCang()
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
            var listy = (from tbem in myModels.B_WareHouseList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_WareHouseList mymodell = listy[intcoun - 1];
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
        /// 下拉框（采购部门）
        /// </summary>
        /// <returns></returns>
        public ActionResult CaiGouXiaLa()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_SpouseBRanchList
                                                 where tb.SpouseBRanchID == 1 || tb.SpouseBRanchID == 2
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.SpouseBRanchID,
                                                     text = tb.SpouseBRanchName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 下拉框（库存地点）
        /// </summary>
        /// <returns></returns>
        public ActionResult KuCunDiDian()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_StockPlaceList
                                                 where tb.StockPlaceID==1 || tb.StockPlaceID == 2 || tb.StockPlaceID == 3
                                                 select new SelectXiaLa
                                                 
                                                 {
                                                     id = tb.StockPlaceID,
                                                     text = tb.StockPlaceName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询（员工表）
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectYuanGong(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbYuanGong in myModels.B_StaffList
                        join tbStaffName in myModels.S_StaffClassList on tbYuanGong.StaffClassID equals tbStaffName.StaffClassID
                        where tbYuanGong.StaffClassID == 3
                        select new Vo.Staffl
                        {
                            StaffID = tbYuanGong.StaffID,
                            StaffName = tbYuanGong.StaffName,
                            StaffCode = tbYuanGong.StaffCode
                        }).ToList();

            int totalRow = linq.Count();
            List<Vo.Staffl> notices = linq.OrderByDescending(p => p.StaffID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<Vo.Staffl> bsgrid = new Vo.Bsgrid<Vo.Staffl>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 选择（员工表）
        /// </summary>
        /// <returns></returns>
        public ActionResult XuanZeYuanGong(int StaffID)
        {
            var linq = (from tbYuanGong in myModels.B_StaffList
                        join tbStaffName in myModels.S_StaffClassList on tbYuanGong.StaffClassID equals tbStaffName.StaffClassID
                        where tbYuanGong.StaffID == StaffID
                        select new
                        {
                            StaffID = tbYuanGong.StaffID,
                            StaffName = tbYuanGong.StaffName,
                            StaffCode = tbYuanGong.StaffCode
                        }).Single();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 选择员工绑定（）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult BangDingYuanGong(int StaffID)
        {
            var linq = (from tbYuanGong in myModels.B_StaffList
                        join tbStaffName in myModels.S_StaffClassList on tbYuanGong.StaffClassID equals tbStaffName.StaffClassID
                        where tbYuanGong.StaffClassID == 3
                        select new
                        {
                            StaffID = tbYuanGong.StaffID,
                            StaffName = tbYuanGong.StaffName,//姓名
                            StaffCode = tbYuanGong.StaffCode//编号
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 根据订单查询（数据）
        /// </summary>
        /// <param name="OrderFormID"></param>
        /// <returns></returns>
        public ActionResult SelectDingDan(Vo.BsgridPage bsgridPage)
        {

            var linq = (from tbSelectOrderForm in myModels.B_OrderFormList
                        join tbHeTong in myModels.B_OrderFormPactList on tbSelectOrderForm.OrderFormPactID equals tbHeTong.OrderFormPactID
                        join tbGongHuoShang in myModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuoShang.SupplierID
                        join tbJieSuanFangShi in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuanFangShi.MethodOfSettlingAccountsID
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbSelectOrderForm.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID
                        join tbDingDanLeiXing in myModels.S_OrderFormTypeList on tbSelectOrderForm.OrderFormTypeID equals tbDingDanLeiXing.OrderFormTypeID
                        where tbSelectOrderForm.ConsigneeNot == true && tbSelectOrderForm.ShiFouShouHuo==false
                        select new LY.SelectListDingDan
                        {
                            OrdernFromIDs = tbSelectOrderForm.OrderFormID,//订单ID
                            OrderNumber = tbSelectOrderForm.OrderNumber,//订单编号
                            OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同编号
                            SpouseBRanchIDs = tbPeiHuoBuMen.SpouseBRanchID,//进货部门ID
                            SpouseBRanchName = tbPeiHuoBuMen.SpouseBRanchName,//进货部门
                            MethodOfSettlingAccounts = tbJieSuanFangShi.MethodOfSettlingAccounts,//结算方式

                            SupplierCHName = tbGongHuoShang.SupplierCHName,//供货商名称
                            OrderFormTypeName = tbDingDanLeiXing.OrderFormTypeName//订单类型

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.SelectListDingDan> notices = linq.OrderByDescending(p => p.OrderFormID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.SelectListDingDan> bsgrid = new Vo.Bsgrid<LY.SelectListDingDan>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 选择订单绑定（订单ID）到页面（1）
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult BangDingDingDan(int orderFormID)
        {
            var linq = (from tbOrderForm in myModels.B_OrderFormList
                        join tbHeTong in myModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                        join tbPeiHuoBuMen in myModels.S_SpouseBRanchList on tbOrderForm.SpouseBRanchID equals tbPeiHuoBuMen.SpouseBRanchID//进货部门
                        join tbDingDanLeiXing in myModels.S_OrderFormTypeList on tbOrderForm.OrderFormTypeID equals tbDingDanLeiXing.OrderFormTypeID//订单类型
                        join tbJieSuanFa in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuanFa.MethodOfSettlingAccountsID//结算方法
                        where tbOrderForm.OrderFormID == orderFormID
                        select new LY.SelectListDingDan
                        {
                            OrderFormID = tbOrderForm.OrderFormID,//订单ID
                            OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID

                            ContractNumber = tbHeTong.ContractNumber,//合同号
                            OrderNumber = tbOrderForm.OrderNumber,//订单号
                            MethodOfSettlingAccounts = tbJieSuanFa.MethodOfSettlingAccounts,//结算方法
                            SupplierCHName = tbSupplier.SupplierCHName,//供货商
                            SpouseBRanchIDs = tbPeiHuoBuMen.SpouseBRanchID,//进货部门
                            OrderFormTypeName = tbDingDanLeiXing.OrderFormTypeName,//订单类型

                            Value = tbOrderForm.Value,//价款
                            ExpensesOTtaxation = tbOrderForm.ExpensesOTtaxation,//税金
                            ValueTotal = tbOrderForm.ValueTotal,//价税合计
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 选择绑定 订单（商品）到页面（2）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingShangPin(Vo.BsgridPage bsgridPage, int OrdernFromID1)
        {
            try
            {
                var linq = (from tbOrderFormDeill in myModels.B_OrderFormDetailList//订单明细   
                            join tbShangPin in myModels.B_GoodsList on tbOrderFormDeill.GoodsID equals tbShangPin.GoodsID//商品
                            join tbShangPinDeill in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbShangPinDeill.GoodsID//商品明细

                            join tbBaoZhuangXin in myModels.S_PackInfoIDList on tbShangPinDeill.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息

                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            where tbOrderFormDeill.OrderFormID == OrdernFromID1
                            select new LY.SelectListDingDan
                            {
                                OrderFormDetailIDs = tbOrderFormDeill.OrderFormDetailID,
                                OrdernFromIDs = tbOrderFormDeill.OrderFormID,
                                GoodsIDs = tbShangPinDeill.GoodsID,
                                GoodsCodes = tbShangPin.GoodsCode,//商品代码
                                GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                GoodsNames = tbShangPin.GoodsName,//商品名称
                                ArtNos = tbShangPin.ArtNo,//商品货号
                                SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                TaxBids = tbShangPinDeill.TaxBid,//含税进价
                                PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                                RetailMonovalents = tbShangPinDeill.RetailMonovalent,//零售单价
                            }).ToList();

                int totalRow = linq.Count();
                List<LY.SelectListDingDan> notices = linq.OrderByDescending(p => p.OrderFormID).//noboer表达式
                     Skip(bsgridPage.GetStartIndex()).//F12（看）
                    Take(bsgridPage.pageSize).
                    ToList();

                Vo.Bsgrid<LY.SelectListDingDan> bsgrid = new Vo.Bsgrid<LY.SelectListDingDan>();
                bsgrid.success = true;
                bsgrid.totalRows = totalRow;//总的行数
                bsgrid.curPage = bsgridPage.curPage;//请求当前页
                bsgrid.data = notices;//查出的数据
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("", JsonRequestBehavior.AllowGet);
            }
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

                        where tbSelectWanHtsList.ExamineNot == false && tbSelectWanHtsList.QuFenLeiXingID == 1 && tbSelectWanHtsList.CrushRedNot == false
                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                            Remember = tbSelectWanHtsList.Remember,//进仓编号
                            OrdernFromIDs = tbSelectOrderForm.OrderFormID,//订单ID

                            OrderNumber = tbSelectOrderForm.OrderNumber,//订单编号
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
                        where tbWanHtsList.QuFenLeiXingID == 1 && tbWanHtsList.ExamineNot == false && tbWanHtsList.WareHouseID == WareHouseID
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
                            join tbPackln in myModels.S_PackInfoIDList on tbShangPinDeill.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
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
                                PackInfos = tbPackln.PackContent,//包装含量

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
        /// 开始 新增 保存（进仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectJingCangDan(B_WareHouseList WareHouse, Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu, bool state)
        {
            string strMag = "fali";
          
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
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


                //判断记录编号是否存在
                int oldWareHouseRows = (from tbWareHouse in myModels.B_WareHouseList
                                        where tbWareHouse.Remember == WareHouse.Remember
                                        select tbWareHouse).Count();
                if (oldWareHouseRows == 0)
                {

                    WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                    WareHouse.Remember = Request.Form["Remember"];//记录编号
                    WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);//进货部门
                    WareHouse.OrderFormID = Convert.ToInt32(Request.Form["OrdernFromIDs"]);//订单ID
                    WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                    WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["StockPlaceID"]);//库存地点
                    WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人
                    WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间
                    WareHouse.RegisterName = Request.Form["RegisterName"];//登记人

                    if (WareHouse.OrderFormID > 0 && WareHouse.SpouseBRanchID > 0 && WareHouse.StockPlaceID > 0 && WareHouse.StaffID > 0
                        && WareHouse.RegisterName != null && WareHouse.RegisterTime != null)
                    {
                        myModels.B_WareHouseList.Add(WareHouse);
                        myModels.SaveChanges();


                        B_OrderFormList wafrtbool = (from tbbool in myModels.B_OrderFormList
                                                     where tbbool.OrderFormID == WareHouse.OrderFormID
                                                     select tbbool).Single();//查询原状态
                        wafrtbool.ShiFouShouHuo = state;//改变是否为收货状态
                        myModels.Entry(wafrtbool).State = EntityState.Modified;

                        myModels.SaveChanges();


                        B_WareHouseList wafrtboolWa = (from tbbool in myModels.B_WareHouseList
                                                       where tbbool.WareHouseID == WareHouse.WareHouseID
                                                       select tbbool).Single();//查询原状态
                        wafrtboolWa.HoveNot = state;//有无订单
                        myModels.Entry(wafrtboolWa).State = EntityState.Modified; //EntityState (生成 using)  这句代码意思把它进行修改

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

                                    WareHouseDetiail.YuanMumberOfPack = Convert.ToDecimal(intsN[H]);//原入库细数
                                    WareHouseDetiail.YuanSubdivision = Convert.ToDecimal(intsM[E]);//原入库件数


                                    myModels.B_WareHouseDetiailList.Add(WareHouseDetiail);
                                    myModels.SaveChanges();//保存
                                    E++;
                                    H++;
                                    strMag = "succsse";
                                }
                            }
                        }

                    }
                    else
                    {
                        return Json(strMag, JsonRequestBehavior.AllowGet);
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
        /// 开始 修改 保存（进仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]

        public ActionResult UptaleJingCangDan(Array JieShouMingXiID, Array JieShouID, Array JieShouJianShu, Array JieShouRuKuXiShu, bool state)
        {
            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();
            //声明字符串接收“数组的信息”
            //一（订单明细ID）
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');//剪切出来
            //二（入库件数）
            string M = ((string[])JieShouJianShu)[0];
            string[] intsM = M.Split(',');
            //三（入库细数）
            string N = ((string[])JieShouRuKuXiShu)[0];
            string[] intsN = N.Split(',');
            //四(进仓明细ID)
            string V = ((string[])JieShouMingXiID)[0];
            string[] intsV = V.Split(',');//剪切出来

            try
            {
                B_WareHouseList WareHouse = new B_WareHouseList();
                WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                WareHouse.WareHouseID = Convert.ToInt32(Request.Form["WareHouseID"]);//进仓单ID
                WareHouse.Remember = Request.Form["Remember"];//记录编号
                WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);//进货部门
                WareHouse.OrderFormID = Convert.ToInt32(Request.Form["OrdernFromIDs"]);//订单ID
                WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["StockPlaceID"]);//库存地点
                WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人
                WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间
                WareHouse.RegisterName = Request.Form["RegisterName"];//登记人

                myModels.Entry(WareHouse).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();


                if (WareHouse.WareHouseID > 0)
                {
                    var ling = (from tbWarHreDiell in myModels.B_WareHouseDetiailList
                                where tbWarHreDiell.WareHouseID == WareHouse.WareHouseID
                                select tbWarHreDiell).Count();
                    if (ling > 0)
                    {
                        if (V.Length > 0)
                        {
                            for (int H = 0; H < intsN.Length;)
                            {
                                for (int E = 0; E < intsM.Length;)
                                {
                                    for (int d = 0; d < intsZ.Length; d++)
                                    {
                                        for (int m = 0; m < intsV.Length; m++)
                                        {
                                            B_WareHouseDetiailList WareHouseDetial = new B_WareHouseDetiailList();

                                            WareHouseDetial.WareHouseDetiailID = Convert.ToInt32(intsV[m]);//进仓单明细ID
                                            WareHouseDetial.WareHouseID = Convert.ToInt32(Request.Form["WareHouseID"]);//进仓单ID
                                            WareHouseDetial.OrderFormDetailID = Convert.ToInt32(intsZ[d]);//订单明细ID
                                            WareHouseDetial.Subdivision = Convert.ToDecimal(intsN[H]);//入库细数
                                            WareHouseDetial.MumberOfPackages = Convert.ToDecimal(intsM[E]);//入库件数

                                            WareHouseDetial.YuanMumberOfPack = Convert.ToDecimal(intsN[H]);//入库细数
                                            WareHouseDetial.YuanSubdivision = Convert.ToDecimal(intsM[E]);//入库件数

                                            myModels.Entry(WareHouseDetial).State = System.Data.Entity.EntityState.Modified;
                                            myModels.SaveChanges();//保存


                                            strMag = "succsse";
                                            H++;
                                            E++;
                                            d++;
                                        }
                                    }
                                }
                            }
                        }
                        B_WareHouseList wafrtbool = (from tbbool in myModels.B_WareHouseList
                                                     where tbbool.WareHouseID == WareHouse.WareHouseID
                                                     select tbbool).Single();//查询原状态
                        wafrtbool.HoveNot = state;//改变是否为冲红单状态
                        myModels.Entry(wafrtbool).State = EntityState.Modified; //EntityState (生成 using)  这句代码意思把它进行修改

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
                    else
                    {

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
        /// 删除进仓单
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int WarHouserid,bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMsg = "fail";
            try
            {
                var linq = (from tbWarHouser in myModels.B_WareHouseList
                            where tbWarHouser.WareHouseID == WarHouserid
                            select new LY.WareHouseDeitaLL
                            {
                                OrdernFromIDs = tbWarHouser.OrderFormID
                            }).ToList();
                Session["OrdernFromIDsgh"] = linq[0].OrdernFromIDs;//订单ID
                int OrdernFromIDid = Convert.ToInt32(Session["OrdernFromIDsgh"]);

                B_WareHouseList WarHouser = (from tbWarHouser in myModels.B_WareHouseList
                                             where tbWarHouser.WareHouseID == WarHouserid
                                             select tbWarHouser).Single();
                myModels.B_WareHouseList.Remove(WarHouser);

                int waDetialid = (int)WarHouser.WareHouseID;

                //查询对应对应明细（总数量）
                var WarHouserDetial = (from tbWarHouserDetial in myModels.B_WareHouseDetiailList
                                       where tbWarHouserDetial.WareHouseID == waDetialid
                                       select tbWarHouserDetial).ToList();
                int thyCount = WarHouserDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        myModels.B_WareHouseDetiailList.Remove(WarHouserDetial[i]);
                        myModels.SaveChanges();
                        strMsg = "success";


                        B_OrderFormList wafrtbool = (from tbbool in myModels.B_OrderFormList
                                                     where tbbool.OrderFormID == OrdernFromIDid
                                                     select tbbool).Single();//查询原状态
                        wafrtbool.ShiFouShouHuo = state;//改变是否为收货状态
                        myModels.Entry(wafrtbool).State = EntityState.Modified;

                        if (myModels.SaveChanges() > 0)//保存
                        {
                            returnJson.State = false;
                            returnJson.Text = "修改成功";
                        }
                        else
                        {
                            returnJson.State = true;
                            returnJson.Text = "修改失败";
                        }

                    }
                }
              

                

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMsg , returnJson } , JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 点击填写审核信息（一）
        /// </summary>
        /// <param name="WareHouse"></param>
        /// <param name="WarHouserid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult ShenHeCiDan(B_WareHouseList WareHouse, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";
            try
            {
                WareHouse.QuFenLeiXingID = Convert.ToInt32(Request.Form["QuFenLeiXingID"]);//进仓类型ID
                WareHouse.WareHouseID = Convert.ToInt32(Request.Form["WareHouseID"]);//进仓单ID
                WareHouse.Remember = Request.Form["Remember"];//记录编号
                WareHouse.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);//进货部门
                WareHouse.OrderFormID = Convert.ToInt32(Request.Form["OrdernFromIDs"]);//订单ID
                WareHouse.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);//合同ID
                WareHouse.StockPlaceID = Convert.ToInt32(Request.Form["StockPlaceID"]);//库存地点
                WareHouse.StaffID = Convert.ToInt32(Request.Form["StaffID"]);//收货人
                WareHouse.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);//登记时间
                WareHouse.RegisterName = Request.Form["RegisterName"];//登记人
                WareHouse.ExamineName = Request.Form["ExamineName"];//审核人
                WareHouse.ExamineTime = Convert.ToDateTime(Request.Form["ExamineTime"]);//审核时间

                myModels.Entry(WareHouse).State = System.Data.Entity.EntityState.Modified;
                myModels.SaveChanges();

                if (WareHouse.WareHouseID > 0)
                {
                    B_WareHouseList wafrtbool = (from tbbool in myModels.B_WareHouseList
                                                 where tbbool.WareHouseID == WareHouse.WareHouseID
                                                 select tbbool).Single();//查询原状态
                    wafrtbool.HoveNot = state;//改变是否为有订单
                    myModels.Entry(wafrtbool).State = EntityState.Modified; //EntityState (生成 using)  这句代码意思把它进行修改

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

        /// <summary>
        /// 审核状态（二）
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
                wafrtbool.ExamineNot = state;//改变状态
                myModels.Entry(wafrtbool).State = EntityState.Modified; //EntityState (生成 using)  这句代码意思把它进行修改


                B_WareHouseList wafrtboole = (from tbbool in myModels.B_WareHouseList
                                              where tbbool.WareHouseID == WarHouserid
                                              select tbbool).Single();//查询原状态
                wafrtboole.Status = state;//改变状态
                myModels.Entry(wafrtboole).State = EntityState.Modified;

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
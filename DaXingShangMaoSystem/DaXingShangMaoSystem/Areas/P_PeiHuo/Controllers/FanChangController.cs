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
    public class FanChangController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: P_PeiHuo/FanChang
        public ActionResult FanChang()
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
            var listy = (from tbem in MyModels.B_RetureFactoryList
                         orderby tbem.Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_RetureFactoryList mymodell = listy[intcoun - 1];
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
        /// 下拉（退货原因）
        /// </summary>
        /// <returns></returns>
        public ActionResult TuiHuoYuanYin()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in MyModels.S_ReturnofList                                              
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.ReturnofID,
                                                     text = tb.Returnof
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 下拉（退货原因）
        /// </summary>
        /// <returns></returns>
        public ActionResult TuiHuoBuMen()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in MyModels.S_SpouseBRanchList
                                                 where tb.SpouseBRanchID==3 ||tb.SpouseBRanchID==4
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
        /// 查询合同单
        /// </summary>
        /// <returns></returns>
        public ActionResult SeleHeTong(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbPeiHuo in MyModels.B_ConverList
                        join tbJinCang in MyModels.B_WareHouseList on tbPeiHuo.WareHouseID equals tbJinCang.WareHouseID
                        join tbHeTong in MyModels.B_OrderFormPactList on tbJinCang.OrderFormPactID equals tbHeTong.OrderFormPactID
                        join tbGongHuo in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuo.SupplierID
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbHeTong.AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID
                        where tbPeiHuo.ExamineNot == true
                        select new LY.PeiHuoDan
                        {
                            ConverID=tbPeiHuo.ConverID,
                            ContractNumber = tbHeTong.ContractNumber,
                            SupplierCHName=tbGongHuo.SupplierCHName,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion
                        }).ToList();


            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
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
        /// 绑定合同单
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingHeTong(int converID)
        {
            var linq = (from tbPeiHuo in MyModels.B_ConverList
                        join tbJinCang in MyModels.B_WareHouseList on tbPeiHuo.WareHouseID equals tbJinCang.WareHouseID
                   
                        join tbHeTong in MyModels.B_OrderFormPactList on tbJinCang.OrderFormPactID equals tbHeTong.OrderFormPactID
                        join tbGongHuo in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuo.SupplierID
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbHeTong.AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID
                        where tbPeiHuo.ConverID == converID
                        select new LY.PeiHuoDan
                        {
                            ConverID=tbPeiHuo.ConverID,
                            ContractNumber=tbHeTong.ContractNumber,
                            SupplierCHName=tbGongHuo.SupplierCHName,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion,

                         

                        }).ToList();


            return Json(linq, JsonRequestBehavior.AllowGet);

        }

       

        /// <summary>
        /// 绑定退货部门
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDTuiHuoBuMen(int spouseBRanchID)
        {
            var linq = (from tb in MyModels.S_SpouseBRanchList
                        where tb.SpouseBRanchID == spouseBRanchID
                        select new LY.PeiHuoDan
                        {
                            SpouseBRanchName = tb.SpouseBRanchName
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 绑定库存部门
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDKuCun(int stockPlaceID)
        {
            var linq = (from tb in MyModels.S_StockPlaceList
                        where tb.StockPlaceID == stockPlaceID
                        select new LY.PeiHuoDan
                        {
                            StockPlaceName = tb.StockPlaceName
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 查询商品批量设置
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPiGoodel(Vo.BsgridPage bsgridPage, int converID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_ConverList
                        join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                        where tbWanHofDeaill.ConverID == converID//根据进仓明细中的“进仓ID”
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
        /// 绑定商品到（主界面）左
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
                    var wareHouID = Convert.ToInt32(intsK[P]);
                    var linq = (from tbSelectWanHtsList in MyModels.B_ConverList
                                join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                                join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                                where tbWanHofDeaill.GoodsID == goodsIDs && tbWanHofDeaill.ConverID == wareHouID//根据进仓明细中的“配货ID”
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
        public ActionResult PiPeiGoodelright(Vo.BsgridPage bsgridPage, int converID, int goodsIDs)
        {
          var linq = (from tbSelectWanHtsList in MyModels.B_ConverList
                                   join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                                   join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                                   join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                   join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                   join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                                   where tbWanHofDeaill.GoodsID == goodsIDs && tbWanHofDeaill.ConverID == converID//根据进仓明细中的“配货ID”
                                   select new LY.PeiHuoDan
                                   {
                                       ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                                       ConverID = tbSelectWanHtsList.ConverID,//配货ID
                                       Number = tbWanHofDeaill.Number,//批次
                                       MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//进仓件数
                                     

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


        //查询原来件数
        public ActionResult YuanJianShu(int wanHofDeailid)
        {
            var linq = (from tbWanHofDeaill in MyModels.B_ConverDeTailList //配货明细

                        where tbWanHofDeaill.ConverDeTaiID == wanHofDeailid//根据配货明细中的“配货ID”
                        select new LY.PeiHuoDan
                        {
                            ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//配货件数
                            Number = tbWanHofDeaill.Number,//批次号

                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 开始 新增 保存（返厂单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectPeiZaiDan(B_RetureFactoryList OK, Array JiShangPiID, Array JiRuJianShu)
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


                //判断记录编号是否存在
                int oldWareHouseRows = (from tb in MyModels.B_RetureFactoryList
                                        where tb.Remember == OK.Remember
                                        select tb).Count();

                if (oldWareHouseRows == 0)
                {
                    B_RetureFactoryList MyB_ConverList = new B_RetureFactoryList();


                    MyB_ConverList.ConverID = OK.ConverID;
                    MyB_ConverList.Remember = OK.Remember;
                    MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                    MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                    MyB_ConverList.ReturnofID = OK.ReturnofID;
                    MyB_ConverList.Settlement = OK.Settlement;
                    MyB_ConverList.Appendix = OK.Appendix;
                    MyB_ConverList.Evidences = OK.Evidences;
                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;

                    MyB_ConverList.MumberOfPackages = OK.MumberOfPackages;
                    MyB_ConverList.Number = OK.Number;

                    MyModels.B_RetureFactoryList.Add(MyB_ConverList);
                    if (MyModels.SaveChanges() > 0)
                    {
                        strMag = "succsee";

                        B_RetureFactoryDeTailList ConverDeTailList = new B_RetureFactoryDeTailList();

                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length; d++)
                            {

                                ConverDeTailList.RetureFactoryID = MyB_ConverList.RetureFactoryID;//进货单ID
                                ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID

                                ConverDeTailList.ReMumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数

                                MyModels.B_RetureFactoryDeTailList.Add(ConverDeTailList);
                                MyModels.SaveChanges();//保存

                                E++;
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




        //查询返厂单
        public ActionResult SelectRetlre(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbFanChan in MyModels.B_RetureFactoryList
                        join tbConver in MyModels.B_ConverList on tbFanChan.ConverID equals tbConver.ConverID
                        join tbWareHou in MyModels.B_WareHouseList on tbConver.WareHouseID equals tbWareHou.WareHouseID
                        join tbOrderFormPactI in MyModels.B_OrderFormPactList on tbWareHou.OrderFormPactID equals tbOrderFormPactI.OrderFormPactID
                        join tbSupptoc in MyModels.B_SupplierList on tbOrderFormPactI.SupplierID equals tbSupptoc.SupplierID
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbOrderFormPactI.AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID
                        join tbTuiHuo in MyModels.S_SpouseBRanchList on tbFanChan.SpouseBRanchID equals tbTuiHuo.SpouseBRanchID
                        join tbKuCun in MyModels.S_StockPlaceList on tbFanChan.StockPlaceID equals tbKuCun.StockPlaceID
                        join tbTuiYuanYin in MyModels.S_ReturnofList on tbFanChan.ReturnofID equals tbTuiYuanYin.ReturnofID
                        where tbFanChan.ExamineNot==false
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryID = tbFanChan.RetureFactoryID,
                            ConverID=tbConver.ConverID,
                            Remember = tbFanChan.Remember,
                            SpouseBRanchName = tbTuiHuo.SpouseBRanchName,
                            StockPlaceName = tbKuCun.StockPlaceName,
                            Returnof = tbTuiYuanYin.Returnof,

                            SupplierCHName = tbSupptoc.SupplierCHName,
                            ContractNumber = tbOrderFormPactI.ContractNumber,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion,
                            RegisterName = tbFanChan.RegisterName,
                            registerTime = tbFanChan.RegisterTime.ToString(),
                            Settlement = tbFanChan.Settlement,
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


        //绑定返厂单(一)
        public ActionResult BangDingRetlre(int retureFactoryID)
        {
            var linq = (from tbFanChan in MyModels.B_RetureFactoryList
                        join tbConver in MyModels.B_ConverList on tbFanChan.ConverID equals tbConver.ConverID
                        join tbWareHou in MyModels.B_WareHouseList on tbConver.WareHouseID equals tbWareHou.WareHouseID
                        join tbOrderFormPactI in MyModels.B_OrderFormPactList on tbWareHou.OrderFormPactID equals tbOrderFormPactI.OrderFormPactID
                        join tbSupptoc in MyModels.B_SupplierList on tbOrderFormPactI.SupplierID equals tbSupptoc.SupplierID
                        join tbHeSuan in MyModels.S_AdjustAccountsFashionList on tbOrderFormPactI.AdjustAccountsFashionID equals tbHeSuan.AdjustAccountsFashionID
                        join tbTuiHuo in MyModels.S_SpouseBRanchList on tbFanChan.SpouseBRanchID equals tbTuiHuo.SpouseBRanchID
                        join tbKuCun in MyModels.S_StockPlaceList on tbFanChan.StockPlaceID equals tbKuCun.StockPlaceID
                        join tbTuiYuanYin in MyModels.S_ReturnofList on tbFanChan.ReturnofID equals tbTuiYuanYin.ReturnofID
                        where tbFanChan.RetureFactoryID== retureFactoryID
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryID = tbFanChan.RetureFactoryID,
                            ConverID = tbConver.ConverID,
                            Remember = tbFanChan.Remember,
                            SpouseBRanchName = tbTuiHuo.SpouseBRanchName,
                            StockPlaceName = tbKuCun.StockPlaceName,
                            SpouseBRanchID = tbTuiHuo.SpouseBRanchID,
                            StockPlaceID = tbKuCun.StockPlaceID,
                            ReturnofID = tbTuiYuanYin.ReturnofID,
                            Appendix = tbFanChan.Appendix,
                            SupplierCHName = tbSupptoc.SupplierCHName,
                            ContractNumber = tbOrderFormPactI.ContractNumber,
                            AdjustAccountsFashion = tbHeSuan.AdjustAccountsFashion,
                            RegisterName = tbFanChan.RegisterName,
                            registerTime = tbFanChan.RegisterTime.ToString(),
                            Settlement = tbFanChan.Settlement,

                            Number = tbFanChan.Number,
                            MumberOfPackages = tbFanChan.MumberOfPackages

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 绑定商品到（主界面）左(二)附属商品
        /// </summary>
        /// <returns></returns>
        public ActionResult BangFanChangGoodel(Vo.BsgridPage bsgridPage,int retureFactoryID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_RetureFactoryList//返厂ID
                        join tbWanHofDeaill in MyModels.B_RetureFactoryDeTailList on tbSelectWanHtsList.RetureFactoryID equals tbWanHofDeaill.RetureFactoryID//返厂明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWanHofDeaill.RetureFactoryID == retureFactoryID//根据进仓明细中的“返厂ID”
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryDeTailID = tbWanHofDeaill.RetureFactoryDeTailID,//返厂明细ID
                            RetureFactoryID = tbSelectWanHtsList.RetureFactoryID,//返厂ID

                            ReMumberOfPackages = tbWanHofDeaill.ReMumberOfPackages,//返厂件数
                            MumberOfPackages = tbSelectWanHtsList.MumberOfPackages,//配货件数
                          
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
        /// 绑定商品到（批量选商品）右(三)附属商品
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingGoodelright(Vo.BsgridPage bsgridPage, int retureFactoryDeTailID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_RetureFactoryList//返厂ID
                        join tbWanHofDeaill in MyModels.B_RetureFactoryDeTailList on tbSelectWanHtsList.RetureFactoryID equals tbWanHofDeaill.RetureFactoryID//返厂明细
                      
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWanHofDeaill.RetureFactoryDeTailID == retureFactoryDeTailID//根据进仓明细中的“返厂ID”
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryDeTailID = tbWanHofDeaill.RetureFactoryDeTailID,//返厂明细ID
                            RetureFactoryID = tbSelectWanHtsList.RetureFactoryID,//返厂ID

                            ReMumberOfPackages = tbWanHofDeaill.ReMumberOfPackages,//返厂件数
                            MumberOfPackages = tbSelectWanHtsList.MumberOfPackages,//配货件数
                            Number = tbSelectWanHtsList.Number,//批号

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


        //查询原来件数
        public ActionResult FanYuanJianShu(int wanHofDeailid)
        {
            var linq = (from tbWanHofDeaill in MyModels.B_RetureFactoryList //返厂

                        where tbWanHofDeaill.RetureFactoryID == wanHofDeailid//根据配货明细中的“返厂ID”
                        select new LY.PeiHuoDan
                        {
                            RetureFactoryID = tbWanHofDeaill.RetureFactoryID,//返厂明细ID
                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//返厂件数
                            Number = tbWanHofDeaill.Number,//批次号

                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 开始 修改 保存（返厂单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptuarPeiZaiDan(B_RetureFactoryList OK, Array JiShangPiID, Array JiRuJianShu, Array FanChangJieID)
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
                //二
                string X = ((string[])FanChangJieID)[0];
                string[] intsFanChangMingXiID = X.Split(',');


               
                    B_RetureFactoryList MyB_ConverList = new B_RetureFactoryList();

                    MyB_ConverList.RetureFactoryID = OK.RetureFactoryID;
                    MyB_ConverList.ConverID = OK.ConverID;
                    MyB_ConverList.Remember = OK.Remember;
                    MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                    MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                    MyB_ConverList.ReturnofID = OK.ReturnofID;
                    MyB_ConverList.Settlement = OK.Settlement;
                    MyB_ConverList.Appendix = OK.Appendix;
                    MyB_ConverList.Evidences = OK.Evidences;
                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;

                    MyB_ConverList.MumberOfPackages = OK.MumberOfPackages;
                    MyB_ConverList.Number = OK.Number;

                    MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                    if (MyModels.SaveChanges() > 0)
                    {
                        strMag = "succsee";

                        B_RetureFactoryDeTailList ConverDeTailList = new B_RetureFactoryDeTailList();

                        for (int E = 0; E < intsRuJian.Length;)
                        {
                            for (int d = 0; d < intsid.Length; d++)
                            {
                                for (int Mf = 0; Mf < intsFanChangMingXiID.Length; Mf++)
                                {
                                    ConverDeTailList.RetureFactoryID = MyB_ConverList.RetureFactoryID;//返厂单ID
                                    ConverDeTailList.RetureFactoryDeTailID = Convert.ToInt32(intsFanChangMingXiID[Mf]); ;//返厂明细ID
                                    ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID

                                    ConverDeTailList.ReMumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数

                                    MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;

                                    MyModels.SaveChanges();//保存
                                    d++;
                                    E++;
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
        /// 开始 审核 保存（返厂单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenHePeiZaiDan(B_RetureFactoryList OK,bool state)
        {
        
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMag = "fali";

            try
            {

                B_RetureFactoryList MyB_ConverList = new B_RetureFactoryList();

                MyB_ConverList.RetureFactoryID = OK.RetureFactoryID;
                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.Remember = OK.Remember;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                MyB_ConverList.ReturnofID = OK.ReturnofID;
                MyB_ConverList.Settlement = OK.Settlement;
                MyB_ConverList.Appendix = OK.Appendix;
                MyB_ConverList.Evidences = OK.Evidences;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExamineTime = OK.ExamineTime;

                MyB_ConverList.MumberOfPackages = OK.MumberOfPackages;
                MyB_ConverList.Number = OK.Number;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";
                    B_RetureFactoryList wafrtbool = (from tbbool in MyModels.B_RetureFactoryList
                                                     where tbbool.RetureFactoryID == MyB_ConverList.RetureFactoryID
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
        public ActionResult DeleteWareHert(int retureFactoryID)
        {
            string strMsg = "fail";
            try
            {

                B_RetureFactoryList conver = (from tbWarHouser in MyModels.B_RetureFactoryList
                                       where tbWarHouser.RetureFactoryID == retureFactoryID
                                              select tbWarHouser).Single();
                MyModels.B_RetureFactoryList.Remove(conver);

                int waDetialid = (int)conver.RetureFactoryID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_RetureFactoryDeTailList
                                    where tbWarHouserDetial.RetureFactoryID == waDetialid
                                    select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_RetureFactoryDeTailList.Remove(converDetial[i]);
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
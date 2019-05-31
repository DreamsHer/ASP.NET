using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi.Controllers
{
    public class FuZhuGongNengController : Controller
    {
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: GoodsZhangGuanLi/FuZhuGongNeng

        //第一部分设置库存限量
        public ActionResult SheDingKuCunXianLiang()
        {
            return View();
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
          
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_SpouseBRanchList
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.SpouseBRanchID,
                                                     text = tb.SpouseBRanchName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// （部门名称）
        /// </summary>
        /// <returns></returns>
        public ActionResult CaiGouXiaLaName(int spouseBRanchID)
        {
            var linq = (from tb in myModels.S_SpouseBRanchList
                        where tb.SpouseBRanchID== spouseBRanchID
                        select new LY.WareHouseDeitaLL
                        {

                            SpouseBRanchName = tb.SpouseBRanchName
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 绑定商标
        /// </summary>
        /// <returns></returns>
        public ActionResult BangGoodBiao()
        {

            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.B_GoodsChopList
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.GoodsChopID,
                                                     text = tb.GoodsChopName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// （商标名称）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangGoodBiaoName(int goodsChopID)
        {
            var linq = (from tb in myModels.B_GoodsChopList
                        where tb.GoodsChopID == goodsChopID
                        select new LY.WareHouseDeitaLL
                        {

                            GoodsChopName = tb.GoodsChopName
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 界面table
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="goodsChdopID"></param>
        /// <returns></returns>
        public ActionResult BangDingShangPfdin(Vo.BsgridPage bsgridPage)
        {
            try
            {
                var linq = (from tbShangPin in myModels.B_GoodsList//商品
                            join tbGoodsBiao in myModels.B_GoodsChopList on tbShangPin.GoodsChopID equals tbGoodsBiao.GoodsChopID
                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            select new LY.SelectListDingDan
                            {

                                GoodsIDs = tbShangPin.GoodsID,
                                GoodsCodes = tbShangPin.GoodsCode,//商品代码
                                GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                GoodsNames = tbShangPin.GoodsName,//商品名称
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                ArtNos = tbShangPin.ArtNo,//商品货号
                                SpecificationsModels = tbShangPin.SpecificationsModel,//规格


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
        /// 选择绑定 （商品）到页面
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingShangPin(Vo.BsgridPage bsgridPage, int goodsChdopID)
        {
            try
            {
                var linq = (from tbShangPin in myModels.B_GoodsList//商品
                            join tbGoodsBiao in myModels.B_GoodsChopList on tbShangPin.GoodsChopID equals tbGoodsBiao.GoodsChopID
                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            where tbShangPin.GoodsChopID == goodsChdopID
                            select new LY.SelectListDingDan
                            {
                              
                                GoodsIDs = tbShangPin.GoodsID,
                                GoodsCodes = tbShangPin.GoodsCode,//商品代码
                                GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                GoodsNames = tbShangPin.GoodsName,//商品名称
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                ArtNos = tbShangPin.ArtNo,//商品货号
                                SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                              
                              
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
        /// 开始 新增
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectJingCangDan(B_GoodsKuCunList GoodsKuCunL, Array JieId, Array JieZuiDi, Array JieZuiGao, Array JieAnQuanTi)
        {
            string strMag = "fali";

            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                //一
                string Z = ((string[])JieId)[0];
                string[] intsId = Z.Split(',');
                //二
                string M = ((string[])JieZuiDi)[0];
                string[] intsD = M.Split(',');
                //三
                string N = ((string[])JieZuiGao)[0];
                string[] intsG = N.Split(',');

                //四
                string T= ((string[])JieAnQuanTi)[0];
                string[] intsT= T.Split(',');

                for (int H = 0; H < intsId.Length;)
                {
                    for (int D = 0; D < intsD.Length;)
                    {
                        for (int G = 0; G < intsG.Length;)
                        {
                            for (int Ti = 0; Ti < intsT.Length; Ti++)
                            {
                                GoodsKuCunL.GoodsIDs = Convert.ToInt32(intsId[H]); ;//
                                GoodsKuCunL.MumberOfPackages = Convert.ToInt32(intsD[D]); ;//
                                GoodsKuCunL.Subdivision = Convert.ToDecimal(intsG[G]);//
                                GoodsKuCunL.AnQuanTianShu = Convert.ToDecimal(intsT[Ti]);//
                                myModels.B_GoodsKuCunList.Add(GoodsKuCunL);
                                myModels.SaveChanges();//保存
                                G++;
                                D++;
                                H++;
                                strMag = "succsee";
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




        //第二删除零库存商品

        public ActionResult DeleteGoodsLing()
        {
            return View();
        }

        /// <summary>
        /// 选择绑定 （商品）到页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBangDingShangPin(Vo.BsgridPage bsgridPage, int spousBRanchID)
        {
            try
            {
             
                    var linq = (from tbSelectWanHtsList in myModels.B_WareHouseList//进仓单ID
                                join tbWanHofDeaill in myModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                                join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品

                                join tbShangPinDeill in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbShangPinDeill.GoodsID//商品明细
                                join tbBaoZhuangXin in myModels.S_PackInfoIDList on tbShangPinDeill.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                                join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位

                                where tbSelectWanHtsList.SpouseBRanchID == spousBRanchID
                                select new LY.WareHouseDeitaLL
                                {

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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }




        //第三部分
        //录入三级帐初始化数据
        public ActionResult LuRuZhangShi()
        {
            return View();
        }



        /// <summary>
        /// 选择绑定 订单（商品）到页面（2）
        /// </summary>
        /// <returns></returns>
        public ActionResult BfdangDingShangPin(Vo.BsgridPage bsgridPage, string goodsCode)
        {
            try
            {
                var linq = (from tbOrderFormDeill in myModels.B_OrderFormDetailList//订单明细
                            join tbOrderForm in myModels.B_OrderFormList on tbOrderFormDeill.OrderFormID equals tbOrderForm.OrderFormID
                            join tbOrderFormPactID in myModels.B_OrderFormPactList on tbOrderForm.OrderFormPactID equals tbOrderFormPactID.OrderFormPactID
                            join tbGongHuo in myModels.B_SupplierList on tbOrderFormPactID.SupplierID equals tbGongHuo.SupplierID

                            join tbShangPin in myModels.B_GoodsList on tbOrderFormDeill.GoodsID equals tbShangPin.GoodsID//商品
                            join tbShangPinDeill in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbShangPinDeill.GoodsID//商品明细

                            join tbBaoZhuangXin in myModels.S_PackInfoIDList on tbShangPinDeill.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息

                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            where tbShangPin.GoodsCode == goodsCode
                            select new LY.SelectListDingDan
                            {

                                ContractNumber = tbOrderFormPactID.ContractNumber,
                                ValidTip = tbOrderFormPactID.ValidTip.ToString(),
                                ValidBegin = tbOrderFormPactID.ValidBegin.ToString(),
                                SupplierNumber = tbGongHuo.SupplierNumber,
                                SupplierCHName = tbGongHuo.SupplierCHName,

                                GoodsIDs = tbShangPinDeill.GoodsID,
                                GoodsCodes = tbShangPin.GoodsCode,//商品代码
                                GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                GoodsNames = tbShangPin.GoodsName,//商品名称
                                ArtNos = tbShangPin.ArtNo,//商品货号
                                SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                TaxBids = tbShangPinDeill.TaxBid,//含税进价
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
        /// 开始 新增(确认)
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectSanZhang(B_ThreeSingleList GoodsKuCunL)
        {
            string strMag = "fali";

          
            try
            {
                B_ThreeSingleList myB_ThreeSingleList = new B_ThreeSingleList();
               

                myB_ThreeSingleList.PurchaseSectionID = GoodsKuCunL.PurchaseSectionID;
                myB_ThreeSingleList.GoodsID = GoodsKuCunL.GoodsID;

                myModels.B_ThreeSingleList.Add(myB_ThreeSingleList);
                myModels.SaveChanges();//保存

                strMag = "succsee";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }






    }
}
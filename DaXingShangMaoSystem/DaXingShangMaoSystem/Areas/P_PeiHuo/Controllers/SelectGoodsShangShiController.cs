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
    public class SelectGoodsShangShiController : Controller
    {
        // GET: P_PeiHuo/SelectGoodsShangShi
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult GoodsShangShi()
        {
            return View();
        }


        /// <summary>
        /// 查询上市单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsedrList(Vo.BsgridPage bsgridPage, int quFenLeiXingID, string goodsCode)
        {
            //进仓、员工、类型、合同、供货商、结算方式  
            var Linq = from tbGoodsListedList in myModels.B_GoodsListedList
                       join tbFaHuoBuMen in myModels.S_StockPlaceList on tbGoodsListedList.SpouseBRanchID equals tbFaHuoBuMen.StockPlaceID
                       join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbGoodsListedList.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID

                       join tbConver in myModels.B_ConverList on tbGoodsListedList.ConverID equals tbConver.ConverID
                       join tbWanHour in myModels.B_WareHouseList on tbConver.WareHouseID equals tbWanHour.WareHouseID
                       join tbOrerHovg in myModels.B_OrderFormPactList on tbWanHour.OrderFormPactID equals tbOrerHovg.OrderFormPactID
                       join tbGongHuo in myModels.B_SupplierList on tbOrerHovg.SupplierID equals tbGongHuo.SupplierID

                       join tbHuoDong in myModels.S_HuoDongZhuanQu on tbGoodsListedList.GoodsDingYiQuID equals tbHuoDong.GoodsDingYiQuID
                       where tbGoodsListedList.ExamineNot == true && tbGoodsListedList.AbateNot == false
                       select new LY.PeiHuoDan
                       {
                           SupplierCHName = tbGongHuo.SupplierName,//供货商名称
                           WareHouseID = tbGoodsListedList.GoodsListedID,//id
                           converIDs = tbGoodsListedList.ConverID,//id
                           P_Remember = tbGoodsListedList.ListedNumber,//编号
                           SpouseBRanchID = tbFaHuoBuMen.StockPlaceID,
                           SpouseBRanchName = tbFaHuoBuMen.StockPlaceName,
                           StockPlaceName = tbGoodsListedList.ConsigneeShop,
                           Remarks = tbGoodsListedList.ConsigneeShopDiZhi,
                           payName = tbGoodsListedList.payName,
                           furlName = tbGoodsListedList.furlName,
                           RegisterName = tbGoodsListedList.RegisterName,
                           registerTime = tbGoodsListedList.RegisterTime.ToString(),

                           ExamineName = tbGoodsListedList.ExamineName,
                           examineTime = tbGoodsListedList.ExamineTime.ToString(),

                           QiDongName = tbHuoDong.GoodsDingYiQuMC,
                           GoodsDingYiQuID = tbHuoDong.GoodsDingYiQuID,
                           ExamineNot=tbGoodsListedList.StoopSellNot
                        };

            //区分ID不为空（因为是下拉框）
            if (quFenLeiXingID > 0)
            {
                Linq = Linq.Where(p => p.GoodsDingYiQuID == quFenLeiXingID);
            }

            if (!string.IsNullOrEmpty(goodsCode))
            {
                goodsCode = goodsCode.Trim();
                Linq = Linq.Where(p => p.P_Remember.Contains(goodsCode));
            }

            int totalRow = Linq.Count();
            List<LY.PeiHuoDan> notices = Linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
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
        /// 绑定商品（2)
        /// </summary>
        public ActionResult BangDingPiGoodelsedrlis(Vo.BsgridPage bsgridPage, int goodsListedID)
        {

            var linq = (from tbShangShi in myModels.B_GoodsListedList
                        join tbGoodsListedDetailL in myModels.B_GoodsListedDetailList on tbShangShi.GoodsListedID equals tbGoodsListedDetailL.GoodsListedID//上市明细
                        join tbWanHofDeaill in myModels.B_ConverDeTailList on tbGoodsListedDetailL.ConverDateilID equals tbWanHofDeaill.ConverDeTaiID//配货明细
                        join tbShangPin in myModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        where tbGoodsListedDetailL.GoodsListedID == goodsListedID
                        select new LY.PeiHuoDan
                        {
                            YuanMumberOfPack = tbGoodsListedDetailL.SongHuoJianShu,//送货件数
                       
                            GoodsCodes = tbShangPin.GoodsCode,//商品代码
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            ArtNos = tbShangPin.ArtNo,//商品货号

                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            UnotMumberOfPa = tbGoodsListedDetailL.ShouChuLiang,//售出量
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.ConverDeTaiID).//noboer表达式
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
        /// 浏览器打印
        /// </summary>
        /// <returns></returns>
        public ActionResult WidowesDaYin()
        {
            var Linq = (from tbGoodsListedList in myModels.B_GoodsListedList
                        join tbFaHuoBuMen in myModels.S_SpouseBRanchList on tbGoodsListedList.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbHuoDongZhuan in myModels.S_HuoDongZhuanQu on tbGoodsListedList.GoodsDingYiQuID equals tbHuoDongZhuan.GoodsDingYiQuID
                        join tbConver in myModels.B_ConverList on tbGoodsListedList.ConverID equals tbConver.ConverID
                        join tbWanHour in myModels.B_WareHouseList on tbConver.WareHouseID equals tbWanHour.WareHouseID
                        join tbOrerHovg in myModels.B_OrderFormPactList on tbWanHour.OrderFormPactID equals tbOrerHovg.OrderFormPactID
                        join tbGongHuo in myModels.B_SupplierList on tbOrerHovg.SupplierID equals tbGongHuo.SupplierID

                        join tbHuoDong in myModels.S_HuoDongZhuanQu on tbGoodsListedList.GoodsDingYiQuID equals tbHuoDong.GoodsDingYiQuID
                        where tbGoodsListedList.ExamineNot == true
                        //orderby tbGoodsListedList.ListedNumber descending
                        select new LY.PeiHuoDan
                        {
                            P_Remember = tbGoodsListedList.ListedNumber,//编号
                            SpouseBRanchID = tbFaHuoBuMen.SpouseBRanchID,
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,
                            StockPlaceName = tbGoodsListedList.ConsigneeShop,
                            Remarks = tbGongHuo.SupplierCHName,
                            payName = tbGoodsListedList.payName,
                            furlName = tbGoodsListedList.furlName,
                            RegisterName = tbGoodsListedList.RegisterName,
                            registerTime = tbGoodsListedList.RegisterTime.ToString(),

                            ExamineName = tbGoodsListedList.ExamineName,
                            examineTime = tbGoodsListedList.ExamineTime.ToString(),

                            QiDongName = tbHuoDong.GoodsDingYiQuMC,
                            GoodsDingYiQuID = tbHuoDong.GoodsDingYiQuID,
                            ExamineNotSt = tbGoodsListedList.StoopSellNot.ToString()
                        }).ToList();
            List<PeiHuoDan> listWareHouseDeitaLL = new List<PeiHuoDan>();

            for (int i = 0; i < Linq.Count; i++)
            {
                PeiHuoDan myWareHouseDeitaLL = new PeiHuoDan();
                if (Convert.ToBoolean(Linq[i].ExamineNotSt) == true)
                {
                    myWareHouseDeitaLL.ExamineNotSt = "已停售";
                }
                else
                {
                    myWareHouseDeitaLL.ExamineNotSt = "销售中";
                }

                myWareHouseDeitaLL.P_Remember = Linq[i].P_Remember;
                myWareHouseDeitaLL.SpouseBRanchName = Linq[i].SpouseBRanchName;
                myWareHouseDeitaLL.StockPlaceName = Linq[i].StockPlaceName;
                myWareHouseDeitaLL.Remarks = Linq[i].Remarks;

                myWareHouseDeitaLL.payName = Linq[i].payName;
                myWareHouseDeitaLL.furlName = Linq[i].furlName;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                myWareHouseDeitaLL.QiDongName = Linq[i].QiDongName;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);
        }




        /// <summary>
        /// 状态（）
        /// </summary>
        public ActionResult UpdateExamineNot(int goodsListedID, bool state)
        {
            //添加（这个类（bit 类型类））
            ReturnJsonVo returnJson = new ReturnJsonVo();

            B_GoodsListedList wafrtbool = (from tbbool in myModels.B_GoodsListedList
                                             where tbbool.GoodsListedID == goodsListedID
                                           select tbbool).Single();//查询原状态
            wafrtbool.StoopSellNot = state;//改变状态
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

            return Json(returnJson, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 状态（）
        /// </summary>
        public ActionResult UpdateExamineNotStoop(int goodsListedID, bool state)
        {
            //添加（这个类（bit 类型类））
            ReturnJsonVo returnJson = new ReturnJsonVo();

            B_GoodsListedList wafrtbool = (from tbbool in myModels.B_GoodsListedList
                                           where tbbool.GoodsListedID == goodsListedID
                                           select tbbool).Single();//查询原状态
            wafrtbool.StoopSellNot = state;//改变状态
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

            return Json(returnJson, JsonRequestBehavior.AllowGet);
        }

    }
}
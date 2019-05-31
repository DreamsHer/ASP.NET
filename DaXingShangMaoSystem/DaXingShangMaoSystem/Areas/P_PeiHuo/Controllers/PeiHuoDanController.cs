using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
//using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.P_PeiHuo.Controllers
{
    public class PeiHuoDanController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: P_PeiHuo/PeiHuoDan
        public ActionResult PeiHuoChuLi()
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
            var listy = (from tbem in MyModels.B_ConverList
                         orderby tbem.P_Remember
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_ConverList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.P_Remember.Substring(1, 8));
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
        /// 查询进仓单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectWareHou(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbWareHoulist in MyModels.B_WareHouseList
                        join tbHeTong in MyModels.B_OrderFormPactList on tbWareHoulist.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbGongHuo in MyModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuo.SupplierID//供货商
                        join tbStoucp in MyModels.S_StockPlaceList on tbWareHoulist.StockPlaceID equals tbStoucp.StockPlaceID//库存地点
                        join tbQuBie in MyModels.S_QuFenLeiXingList on tbWareHoulist.QuFenLeiXingID equals tbQuBie.QuFenLeiXingID//区别类型
                        where tbQuBie.QuFenLeiXingID==1 && tbWareHoulist.CrushRedNot == false && tbWareHoulist.ExamineNot == true && tbWareHoulist.ShiFouKongNot == false ||
                        tbQuBie.QuFenLeiXingID == 2 && tbWareHoulist.CrushRedNot == false && tbWareHoulist.ExamineNot == true && tbWareHoulist.ShiFouKongNot == false

                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbWareHoulist.WareHouseID,
                            Remember = tbWareHoulist.Remember,
                            StockPlaceName = tbStoucp.StockPlaceName,
                            ContractNumber = tbHeTong.ContractNumber,
                            SupplierCHName = tbGongHuo.SupplierCHName,

                            RegisterName = tbWareHoulist.RegisterName,
                            RegisterTime = tbWareHoulist.RegisterTime.ToString(),
                            ExamineNot = tbWareHoulist.ExamineNot,
                            CrushRedNot = tbWareHoulist.CrushRedNot,
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
        /// 绑定进仓单
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingWareHou(int wareHouseID)
        {
            var linq = (from tbWareHoulist in MyModels.B_WareHouseList
                        join tbStoucp in MyModels.S_SpouseBRanchList on tbWareHoulist.SpouseBRanchID equals tbStoucp.SpouseBRanchID//发货部门
                        where tbWareHoulist.WareHouseID == wareHouseID

                        select new LY.WareHouseDeitaLL
                        {
                            WareHouseID = tbWareHoulist.WareHouseID,
                            SpouseBRanchID = tbStoucp.SpouseBRanchID,
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询商品批量设置
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPiGoodel(Vo.BsgridPage bsgridPage,int wareHouseGoodelID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_WareHouseList
                        join tbWanHofDeaill in MyModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                      

                        where tbWanHofDeaill.WareHouseID == wareHouseGoodelID && tbWanHofDeaill.YiKongNot==false//根据进仓明细中的“进仓ID”
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
                        
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

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
        /// 绑定商品到（主界面）
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, Array JieShouID,Array JieWarhou)
        {
            List<WareHouseDeitaLL> list = new List<WareHouseDeitaLL>();
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
                    var wareHouID= Convert.ToInt32(intsK[P]);
                    var linq = (from tbSelectWanHtsList in MyModels.B_WareHouseList//进仓单
                                join tbKuCun in MyModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCun.StockPlaceID//库存地点
                                join tbWanHofDeaill in MyModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                                join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品
                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                

                                where tbWanHofDeaill.OrderFormDetailID == goodsIDs && tbWanHofDeaill.WareHouseID== wareHouID//根据进仓明细中的“进仓ID”
                                select new LY.WareHouseDeitaLL
                                {
                                    WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                                    WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID
                                    OrderFormDetailID = tbWanHofDeaill.OrderFormDetailID,//订单明细ID
                                    YuanMumberOfPack = tbWanHofDeaill.MumberOfPackages,//包装含量（件数）
                                    MumberOfPackagesDing = tbWanHofDeaill.Subdivision,//细数

                                    GoodsIDs = tbShangPin.GoodsID,//商品ID
                                    GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                                    GoodsNames = tbShangPin.GoodsName,//商品名称
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
        /// 绑定商品到（批量选商品）左
        /// </summary>
        /// <returns></returns>
        public ActionResult PiPeiGoodel(Vo.BsgridPage bsgridPage, Array JieShouID, Array JieWarhou)
        {
            List<WareHouseDeitaLL> list = new List<WareHouseDeitaLL>();
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
                    var linq = (from tbSelectWanHtsList in MyModels.B_WareHouseList//进仓单
                                join tbKuCun in MyModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCun.StockPlaceID//库存地点
                                join tbWanHofDeaill in MyModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                                join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品
                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                                where tbWanHofDeaill.OrderFormDetailID == goodsIDs && tbWanHofDeaill.WareHouseID == wareHouID//根据进仓明细中的“进仓ID”
                                select new LY.WareHouseDeitaLL
                                {
                                    WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                                    WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID
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
        /// 随机（自动生成批号）
        /// </summary>
        /// <returns></returns>
        public ActionResult ZiDongPiHao(Array JieShouID, Array JieWarhou)
        {
            
            List<WareHouseDeitaLL> list = new List<WareHouseDeitaLL>();
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
                    var linq = (from tbSelectWanHtsList in MyModels.B_WareHouseList//进仓单
                                join tbKuCun in MyModels.S_StockPlaceList on tbSelectWanHtsList.StockPlaceID equals tbKuCun.StockPlaceID//库存地点
                                join tbWanHofDeaill in MyModels.B_WareHouseDetiailList on tbSelectWanHtsList.WareHouseID equals tbWanHofDeaill.WareHouseID//进仓明细
                                join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.OrderFormDetailID equals tbShangPin.GoodsID//商品
                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表

                                where tbWanHofDeaill.OrderFormDetailID == goodsIDs && tbWanHofDeaill.WareHouseID == wareHouID//根据进仓明细中的“进仓ID”
                                select new LY.WareHouseDeitaLL
                                {
                                    WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                                    WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID
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
            int ZongShu = totalRow+1;
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
        /// 开始 新增 保存（进仓单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectPeiZaiDan(B_ConverList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array PiHaoShuZu,bool state,
            Array WactHourDeiIdShuZ, Array WantHoursIddShuZ, Array OrdersfIdShuZ, Array ShenMusngbiShuZ, Array ShenSumightShuZ,
            Array JiYuanJianShu,Array JiYuanXiShu,Array JiYuanJinCangShu)
        {
           
            ReturnJsonVo returnJson = new ReturnJsonVo();
            ReturnJsonVo returnJsonGoodsNot = new ReturnJsonVo();

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
                string C = ((string[])PiHaoShuZu)[0];
                string[] intsNPiHao = C.Split(',');
                //五
                string YuJ = ((string[])JiYuanJianShu)[0];
                string[] intsidYuanJian = YuJ.Split(',');
                //六
                string YuX = ((string[])JiYuanXiShu)[0];
                string[] intsidYuanXi = YuX.Split(',');
                //七
                string wahourdetail = ((string[])JiYuanJinCangShu)[0];
                string[] intsidYuanWanHouDeta = wahourdetail.Split(',');



                //进 一
                string wahouDeiId = ((string[])WactHourDeiIdShuZ)[0];
                string[] wahouDeiIdintsid = wahouDeiId.Split(',');
                //二
                string WaHouId = ((string[])WantHoursIddShuZ)[0];
                string[] WaHouIdInt = WaHouId.Split(',');

                //三
                string OrderfId = ((string[])OrdersfIdShuZ)[0];
                string[] OrderfIdInt = OrderfId.Split(',');

                //四
                string ShenMusngb = ((string[])ShenMusngbiShuZ)[0];
                string[] ShenMusngbInt = ShenMusngb.Split(',');

                //五
                string Sumighb = ((string[])ShenSumightShuZ)[0];
                string[] SumighbInt = Sumighb.Split(',');


                //判断记录编号是否存在
                int oldWareHouseRows = (from tb in MyModels.B_ConverList
                                        where tb.P_Remember == OK.P_Remember
                                        select tb).Count();

                if (oldWareHouseRows==0) {
                    B_ConverList MyB_ConverList = new B_ConverList();

                    MyB_ConverList.WareHouseID = OK.WareHouseID;
                    MyB_ConverList.P_Remember = OK.P_Remember;
                    MyB_ConverList.payName = OK.payName;
                    MyB_ConverList.furlName = OK.furlName;
                    MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                    MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                    MyB_ConverList.Remarks = OK.Remarks;
                    MyB_ConverList.RegisterName = OK.RegisterName;
                    MyB_ConverList.RegisterTime = OK.RegisterTime;


                    MyModels.B_ConverList.Add(MyB_ConverList);
                    if ( MyModels.SaveChanges()>0)
                    {
                        strMag = "succsee";

                        B_WareHouseList wafrtbool = (from tbbool in MyModels.B_WareHouseList
                                                     where tbbool.WareHouseID == MyB_ConverList.WareHouseID
                                                     select tbbool).Single();//查询原状态
                        wafrtbool.PeiHuoNot = state;//改变是否为配货状态
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


                        B_ConverDeTailList ConverDeTailList = new B_ConverDeTailList();
                        for (int Wahoudetail = 0; Wahoudetail < intsidYuanWanHouDeta.Length;)
                        {
                            for (int YuanJiang = 0; YuanJiang < intsidYuanJian.Length; YuanJiang++)
                            {
                                for (int YuanXjk = 0; YuanXjk < intsidYuanXi.Length; YuanXjk++)
                                {
                                    for (int H = 0; H < intsNXiShu.Length;)
                                    {
                                        for (int E = 0; E < intsRuJian.Length;)
                                        {
                                            for (int d = 0; d < intsid.Length;)
                                            {
                                                for (int Pi = 0; Pi < intsNPiHao.Length; Pi++)
                                                {
                                                    ConverDeTailList.ConverID = MyB_ConverList.ConverID;//进货单ID
                                                    ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID

                                                    ConverDeTailList.WareHouseDetiailID = Convert.ToInt32(intsidYuanWanHouDeta[Wahoudetail]); ;//进仓明细Id

                                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                                                    ConverDeTailList.Number = Convert.ToDecimal(intsNPiHao[Pi]);//批号

                                                    ConverDeTailList.YuanMumberOfPack = Convert.ToDecimal(intsidYuanJian[YuanJiang]);//原件
                                                    ConverDeTailList.YuanSubdivision = Convert.ToDecimal(intsidYuanXi[YuanXjk]);//原细


                                                    MyModels.B_ConverDeTailList.Add(ConverDeTailList);
                                                    MyModels.SaveChanges();//保存
                                                    d++;
                                                    E++;
                                                    H++;
                                                    YuanJiang++;
                                                    YuanXjk++;
                                                    Wahoudetail++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }



                        for (int wahouDeiIdid = 0; wahouDeiIdid < wahouDeiIdintsid.Length;)
                        {
                            for (int WaHouIdInd = 0; WaHouIdInd < WaHouIdInt.Length;)
                            {
                                for (int OrderfIdI = 0; OrderfIdI < OrderfIdInt.Length;)
                                {
                                    for (int ShenMusn = 0; ShenMusn < ShenMusngbInt.Length;)
                                    {
                                        for (int Sumighbd = 0; Sumighbd < SumighbInt.Length; Sumighbd++)
                                        {
                                            B_WareHouseDetiailList GaiBian = new B_WareHouseDetiailList();
                                            GaiBian.WareHouseDetiailID = Convert.ToInt32(wahouDeiIdintsid[wahouDeiIdid]);//进明细Id
                                            GaiBian.WareHouseID = Convert.ToInt32(WaHouIdInt[WaHouIdInd]); ;//进ID
                                            GaiBian.OrderFormDetailID = Convert.ToInt32(OrderfIdInt[OrderfIdI]);//
                                            GaiBian.MumberOfPackages = Convert.ToDecimal(ShenMusngbInt[ShenMusn]);//件数

                                            GaiBian.Subdivision = Convert.ToDecimal(SumighbInt[Sumighbd]);//细数
                                            MyModels.Entry(GaiBian).State = System.Data.Entity.EntityState.Modified;
                                            MyModels.SaveChanges();//保存

                                            Session["WareHouseIDgfh"] = GaiBian.WareHouseID; // 
                                            if (GaiBian.MumberOfPackages == 0)
                                            {
                                                B_WareHouseDetiailList wafrtboolDeti = (from tbbool in MyModels.B_WareHouseDetiailList
                                                                                        where tbbool.WareHouseDetiailID == GaiBian.WareHouseDetiailID
                                                                                        select tbbool).Single();//查询原状态
                                                wafrtboolDeti.YiKongNot = state;//改变是否商品为空
                                                MyModels.Entry(wafrtboolDeti).State = EntityState.Modified;
                                                if (MyModels.SaveChanges() > 0)//保存
                                                {
                                                    returnJsonGoodsNot.State = true;
                                                    returnJsonGoodsNot.Text = "修改成功";
                                                    
                                                }
                                                else
                                                {
                                                    returnJsonGoodsNot.State = false;
                                                    returnJsonGoodsNot.Text = "修改失败";
                                                }
                                            }
                                            ShenMusn++;
                                            OrderfIdI++;
                                            WaHouIdInd++;
                                            wahouDeiIdid++;

                                        }
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
            return Json(new { strMag, returnJson, returnJsonGoodsNot },JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 改变进仓明细
        /// </summary>
        /// <param name="state"></param>
        /// <param name="WanHourId"></param>
        /// <returns></returns>
        public ActionResult GAiZhaungTaiMingXiNot(bool state, int WanHourId)
        {
            ReturnJsonVo returnJsonGoodsNot = new ReturnJsonVo();

            return Json(returnJsonGoodsNot, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 改变进仓状态
        /// </summary>
        /// <returns></returns>
        public ActionResult GAiZhaungTaiNot(bool state)
        {
            ReturnJsonVo returnJsonJin = new ReturnJsonVo();

            if (Session["WareHouseIDgfh"] != null)
            {
                string WanHourId = Session["WareHouseIDgfh"].ToString();
                if (WanHourId != null)
                {

                    int fdf = Convert.ToInt32(WanHourId);

                    var linq = (from tbWanHourDetiai in MyModels.B_WareHouseDetiailList
                                where tbWanHourDetiai.WareHouseID == fdf
                                select new LY.WareHouseDeitaLL
                                {
                                    ExamineNot = tbWanHourDetiai.YiKongNot
                                }).ToList();

                    int zong = linq.Count();
                    int JiLei = 0;

                    for (int i = 0; i < zong; i++)
                    {
                        if (linq[0].ExamineNot == true)
                        {
                            JiLei = JiLei + 1;
                        }
                    }
                    if (JiLei == zong)
                    {
                        B_WareHouseList wafrtbool = (from tbbool in MyModels.B_WareHouseList
                                                     where tbbool.WareHouseID == fdf
                                                     select tbbool).Single();
                        wafrtbool.ShiFouKongNot = state;
                        MyModels.Entry(wafrtbool).State = EntityState.Modified;
                        if (MyModels.SaveChanges() > 0)//保存
                        {
                            returnJsonJin.State = true;
                            returnJsonJin.Text = "修改成功";
                        }
                        else
                        {
                            returnJsonJin.State = false;
                            returnJsonJin.Text = "修改失败";
                        }
                    }
                    else
                    {
                        returnJsonJin.State = false;
                        returnJsonJin.Text = "修改失败";
                    }
                    return Json(returnJsonJin, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(returnJsonJin, JsonRequestBehavior.AllowGet);
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
                        where tbConverlist.ExamineNot==false
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
        /// 绑定查询配货单(一)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPeiZaiDan(int converID)
        {
            var linq = (from tbConverlist in MyModels.B_ConverList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID
                        where tbConverlist.ConverID== converID
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            WareHouseID = tbConverlist.WareHouseID,//进仓id
                            P_Remember = tbConverlist.P_Remember,//编号
                            payName = tbConverlist.payName,//付货人
                            furlName = tbConverlist.furlName,//收货人
                            SpouseBRanchID = tbFaHuoBuMen.SpouseBRanchID,//发货
                            StockPlaceID = tbShouHuoBuMen.StockPlaceID,//收货
                            RegisterName=tbConverlist.RegisterName,//制单人
                            registerTime=tbConverlist.RegisterTime.ToString(),//制单时间
                            Remarks = tbConverlist.Remarks//备注
                        }).ToList();
           
            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 点击查询 绑定配货单(二)商品
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectShangPinZhuf(Vo.BsgridPage bsgridPage, int converID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_ConverList
                    
                        join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                        join tbShangPin in MyModels.B_GoodsList on tbWanHofDeaill.GoodsID equals tbShangPin.GoodsID//商品
                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbShangPin.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                      

                        where tbWanHofDeaill.ConverID == converID//根据配货明细中的“配货ID”
                        select new LY.PeiHuoDan
                        {
                            ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                            ConverID = tbSelectWanHtsList.ConverID,//配货ID

                            WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID

                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//配货件数
                            Subdivision = tbWanHofDeaill.Subdivision,//配货细数
                            YuanMumberOfPack = tbWanHofDeaill.YuanMumberOfPack,//原（件数）
                            MumberOfPackagesDing = tbWanHofDeaill.YuanSubdivision,//原（细数）
                            Number = tbWanHofDeaill.Number,//批次号

                            GoodsIDs = tbShangPin.GoodsID,//商品ID
                            GoodsCodes = tbShangPin.GoodsCode,//商品代码
                            GoodsTiaoMas = tbShangPin.GoodsTiaoMa,//商品条码
                            GoodsNames = tbShangPin.GoodsName,//商品名称
                            ArtNos = tbShangPin.ArtNo,//商品货号

                            SpecificationsModels = tbShangPin.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
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


        public ActionResult SelectJiSuanWancHourDetil(int wareHouseDetiailID)
        {
            var linq = (from tbWanHourDetai in MyModels.B_WareHouseDetiailList//进仓明细
                        where tbWanHourDetai.WareHouseDetiailID == wareHouseDetiailID//根据明细中的“商品ID”
                        select new LY.PeiHuoDan
                        {
                            MumberOfPackages = tbWanHourDetai.MumberOfPackages,//进仓件数
                            Subdivision = tbWanHourDetai.Subdivision,//进仓细数

                            WareHouseID = tbWanHourDetai.WareHouseID,
                            GoodsIDs = tbWanHourDetai.OrderFormDetailID,

                            YuanMumberOfPack = tbWanHourDetai.YuanMumberOfPack,//原件（订单的）
                            UnotXiShu = tbWanHourDetai.YuanSubdivision,//原细（订单的）


                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 修改 保存（配货单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UptrnPeiZaiDan(B_ConverList OK, Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu, Array JieMingXiIDShu,
             Array PeiJiYuanJianShu, Array PeiJiYuanXiShu, Array PiHaoShuZu, Array WactHourDeiIdShuZ, Array WantHoursIddShuZ,
             Array OrdersfIdShuZ,Array ShenMusngbiShuZ,Array ShenSumightShuZ,Array JiYuanJianShu,Array JiYuanXiShu)
        {
           
            ReturnJsonVo returnJson = new ReturnJsonVo();

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
                string MingId = ((string[])JieMingXiIDShu)[0];
                string[] intsMingxiId = MingId.Split(',');


                //五
                string PeiYuJi = ((string[])PeiJiYuanJianShu)[0];
                string[] intsPeiYuJi = PeiYuJi.Split(',');
                //六
                string PeiYXi = ((string[])PeiJiYuanXiShu)[0];
                string[] intsNPeiYXi = PeiYXi.Split(',');
                //七
                string PiHao= ((string[])PiHaoShuZu)[0];
                string[] intsNPiHaos = PiHao.Split(',');



                //一进仓
                string WanrHourDetaiId= ((string[])WactHourDeiIdShuZ)[0];
                string[] intWanrHourDetaiId = WanrHourDetaiId.Split(',');
                //二
                string WantHoursId = ((string[])WantHoursIddShuZ)[0];
                string[] intWantHoursId = WantHoursId.Split(',');
                //三
                string OrdersfId = ((string[])OrdersfIdShuZ)[0];
                string[] intsOrdersfId = OrdersfId.Split(',');
                //四
                string ShenMusngJ = ((string[])ShenMusngbiShuZ)[0];
                string[] intShenMusngJ = ShenMusngJ.Split(',');

                //五
                string ShenSumigXi = ((string[])ShenSumightShuZ)[0];
                string[] intShenSumigXi = ShenSumigXi.Split(',');
                //六
                string YuanJianDing = ((string[])JiYuanJianShu)[0];
                string[] intYuanJianDing = YuanJianDing.Split(',');
                //七
                string YuanXiDing = ((string[])JiYuanXiShu)[0];
                string[] intYuanXiDing = YuanXiDing.Split(',');


                B_ConverList MyB_ConverList = new B_ConverList();
                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.WareHouseID = OK.WareHouseID;
                MyB_ConverList.P_Remember = OK.P_Remember;
                MyB_ConverList.payName = OK.payName;
                MyB_ConverList.furlName = OK.furlName;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                MyB_ConverList.Remarks = OK.Remarks;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                if (MyB_ConverList.ConverID > 0)
                {
                    //配货
                    for (int PeiMingXi = 0; PeiMingXi < intsMingxiId.Length; PeiMingXi++)
                    {
                        for (int Goodsa = 0; Goodsa < intsid.Length; Goodsa++)
                        {
                            for (int JinCangMingXi = 0; JinCangMingXi < intWanrHourDetaiId.Length; )
                            {
                                for (int PeiJian = 0; PeiJian < intsRuJian.Length; )
                                {
                                    for (int PeiXi = 0; PeiXi < intsNXiShu.Length; )
                                    {
                                        for (int PeiNPiHao = 0; PeiNPiHao < intsNPiHaos.Length; )
                                        {
                                            for (int YuanJian = 0; YuanJian < intsPeiYuJi.Length;)
                                            {
                                                for (int YuanXi = 0; YuanXi < intsNPeiYXi.Length; YuanXi++)
                                                {
                                                    B_ConverDeTailList ConverDeTailList = new B_ConverDeTailList();

                                                    ConverDeTailList.ConverDeTaiID = Convert.ToInt32(intsMingxiId[PeiMingXi]);//配货明细ID
                                                    ConverDeTailList.ConverID = MyB_ConverList.ConverID;//配货单ID
                                                    ConverDeTailList.GoodsID = Convert.ToInt32(intsid[Goodsa]); ;//商品ID
                                                    ConverDeTailList.WareHouseDetiailID = Convert.ToInt32(intWanrHourDetaiId[JinCangMingXi]); ;//进仓明细Id

                                                    ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[PeiJian]);//入库件数
                                                    ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[PeiXi]);//入库细数

                                                    ConverDeTailList.Number = Convert.ToDecimal(intsNPiHaos[PeiNPiHao]);//批号

                                                    ConverDeTailList.YuanMumberOfPack = Convert.ToDecimal(intsPeiYuJi[YuanJian]);//原件
                                                    ConverDeTailList.YuanSubdivision = Convert.ToDecimal(intsNPeiYXi[YuanXi]);//原细

                                                    MyModels.Entry(ConverDeTailList).State = System.Data.Entity.EntityState.Modified;
                                                    MyModels.SaveChanges();//保存

                                                    YuanJian++;
                                                    PeiNPiHao++;
                                                    PeiXi++;
                                                    PeiJian++;
                                                    JinCangMingXi++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //进仓
                    for (int WanHouDeTail = 0; WanHouDeTail < intWanrHourDetaiId.Length;)
                    {
                        for (int Order = 0; Order < intsOrdersfId.Length;)
                        {
                            for (int ShenMuYJ = 0; ShenMuYJ < intShenMusngJ.Length;)
                            {
                                for (int ShenSuxi = 0; ShenSuxi < intShenSumigXi.Length;)
                                {
                                    for (int YuanJianD = 0; YuanJianD < intYuanJianDing.Length;)
                                    {
                                        for (int YuanXiD = 0; YuanXiD < intYuanXiDing.Length; YuanXiD++)
                                        {
                                            B_WareHouseDetiailList WareHouseDetili = new B_WareHouseDetiailList();

                                            WareHouseDetili.WareHouseDetiailID = Convert.ToInt32(intWanrHourDetaiId[WanHouDeTail]);//进仓明细
                                            WareHouseDetili.WareHouseID = MyB_ConverList.WareHouseID;
                                            WareHouseDetili.OrderFormDetailID = Convert.ToInt32(intsOrdersfId[Order]);//

                                            WareHouseDetili.MumberOfPackages = Convert.ToDecimal(intShenMusngJ[ShenMuYJ]);//入库件数
                                            WareHouseDetili.Subdivision = Convert.ToDecimal(intShenSumigXi[ShenSuxi]);//入库细数

                                            WareHouseDetili.YuanMumberOfPack = Convert.ToDecimal(intYuanJianDing[YuanJianD]);//原件数（订单）
                                            WareHouseDetili.YuanSubdivision = Convert.ToDecimal(intYuanXiDing[YuanXiD]);//原细数（订单）

                                            MyModels.Entry(WareHouseDetili).State = System.Data.Entity.EntityState.Modified;
                                            MyModels.SaveChanges();//保存

                                            YuanJianD++;
                                            ShenSuxi++;
                                            ShenMuYJ++;
                                            Order++;
                                            WanHouDeTail++;

                                            strMag = "succsee";
                                        }
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
        /// 删除（配货单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int converID,bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            string strMsg = "fail";
            try
            {
                var linq = (from tbConverList in MyModels.B_ConverList
                            where tbConverList.ConverID == converID
                            select new LY.WareHouseDeitaLL
                            {
                                WareHouseID = tbConverList.WareHouseID
                            }).ToList();
                Session["WareHouseIDsgh"] = linq[0].WareHouseID;//订单ID
                int OrdernFromIDid = Convert.ToInt32(Session["WareHouseIDsgh"]);


                B_ConverList conver = (from tbWarHouser in MyModels.B_ConverList
                                             where tbWarHouser.ConverID == converID
                                          select tbWarHouser).Single();
                MyModels.B_ConverList.Remove(conver);

                int waDetialid = (int)conver.ConverID;

                //查询对应对应明细（总数量）
                var converDetial = (from tbWarHouserDetial in MyModels.B_ConverDeTailList
                                       where tbWarHouserDetial.ConverID == waDetialid
                                       select tbWarHouserDetial).ToList();
                int thyCount = converDetial.Count();

                if (thyCount > 0)
                {
                    for (int i = 0; i < thyCount; i++)
                    {
                        MyModels.B_ConverDeTailList.Remove(converDetial[i]);
                        MyModels.SaveChanges();
                        strMsg = "success";


                        B_WareHouseList wafrtbool = (from tbbool in MyModels.B_WareHouseList
                                                     where tbbool.WareHouseID == OrdernFromIDid
                                                     select tbbool).Single();//查询原状态
                        wafrtbool.PeiHuoNot = state;//改变是否为配货状态
                        MyModels.Entry(wafrtbool).State = EntityState.Modified;

                        if (MyModels.SaveChanges() > 0)//保存
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
            return Json(new { strMsg, returnJson }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 删除时查询配货单明细信息一
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsPeiHuo(int converDetaiId)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_ConverList
                        join tbWanHofDeaill in MyModels.B_ConverDeTailList on tbSelectWanHtsList.ConverID equals tbWanHofDeaill.ConverID//配货明细
                        where tbWanHofDeaill.ConverDeTaiID == converDetaiId//根据配货明细中的“配货ID”
                        select new LY.PeiHuoDan
                        {
                            ConverDeTaiID = tbWanHofDeaill.ConverDeTaiID,//配货明细ID
                            ConverID = tbSelectWanHtsList.ConverID,//配货ID
                            WareHouseDetiailID = tbWanHofDeaill.WareHouseDetiailID,//进仓明细ID
                            MumberOfPackages = tbWanHofDeaill.MumberOfPackages,//配货件数
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 删除时查询进仓明细信息二
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsWanrtHour(int wareHouseDetiailID)
        {
            var linq = (from tbSelectWanHtsList in MyModels.B_WareHouseDetiailList//进仓明细
                        where tbSelectWanHtsList.WareHouseDetiailID == wareHouseDetiailID
                        select new LY.PeiHuoDan
                        {
                            WareHouseDetiailID = tbSelectWanHtsList.WareHouseDetiailID,//进仓明细ID
                            WareHouseID = tbSelectWanHtsList.WareHouseID,//进仓ID
                            GoodsIDs = tbSelectWanHtsList.OrderFormDetailID,//
                            MumberOfPackages = tbSelectWanHtsList.MumberOfPackages,//进仓件数
                            YuanMumberOfPack = tbSelectWanHtsList.YuanMumberOfPack,//原件数（订单）
                            MumberOfPackagesDing = tbSelectWanHtsList.YuanSubdivision,//原细数（订单）
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除保存进   Array JiYuanJianShu, Array JiYuanXiShu
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteBaoCunWanHourDetai(Array WactHourDeiIdShuZ, Array WantHoursIddShuZ, Array OrdersfIdShuZ, Array ShenMusngbiShuZ,
            Array ShenSumightShuZ)

        {
            string strMag = "fali";
            //一进仓
            string WanrHourDetaiId = ((string[])WactHourDeiIdShuZ)[0];
            string[] intWanrHourDetaiId = WanrHourDetaiId.Split(',');
            //二
            string WantHoursId = ((string[])WantHoursIddShuZ)[0];
            string[] intWantHoursId = WantHoursId.Split(',');
            //三
            string OrdersfId = ((string[])OrdersfIdShuZ)[0];
            string[] intsOrdersfId = OrdersfId.Split(',');
            //四
            string ShenMusngJ = ((string[])ShenMusngbiShuZ)[0];
            string[] intShenMusngJ = ShenMusngJ.Split(',');

            //五
            string ShenSumigXi = ((string[])ShenSumightShuZ)[0];
            string[] intShenSumigXi = ShenSumigXi.Split(',');
            //六
            //string YuanJianDing = ((string[])JiYuanJianShu)[0];
            //string[] intYuanJianDing = YuanJianDing.Split(',');
            ////七
            //string YuanXiDing = ((string[])JiYuanXiShu)[0];
            //string[] intYuanXiDing = YuanXiDing.Split(',');


            //进仓
            for (int WanHouDeTail = 0; WanHouDeTail < intWanrHourDetaiId.Length;)
            {
                for (int Order = 0; Order < intsOrdersfId.Length;)
                {
                    for (int ShenMuYJ = 0; ShenMuYJ < intShenMusngJ.Length;)
                    {
                        for (int ShenSuxi = 0; ShenSuxi < intShenSumigXi.Length; ShenSuxi++)
                        {

                            B_WareHouseDetiailList WareHouseDetili = new B_WareHouseDetiailList();

                            WareHouseDetili.WareHouseDetiailID = Convert.ToInt32(intWanrHourDetaiId[WanHouDeTail]);//进仓明细
                            WareHouseDetili.WareHouseID = Convert.ToInt32(intWantHoursId[WanHouDeTail]);//进仓Id
                            WareHouseDetili.OrderFormDetailID = Convert.ToInt32(intsOrdersfId[Order]);//

                            WareHouseDetili.MumberOfPackages = Convert.ToDecimal(intShenMusngJ[ShenMuYJ]);//入库件数
                            WareHouseDetili.Subdivision = Convert.ToDecimal(intShenSumigXi[ShenSuxi]);//入库细数

                            //WareHouseDetili.YuanMumberOfPack = Convert.ToDecimal(intYuanJianDing[YuanJianD]);//原件数（订单）
                            //WareHouseDetili.YuanSubdivision = Convert.ToDecimal(intYuanXiDing[YuanXiD]);//原细数（订单）

                            MyModels.Entry(WareHouseDetili).State = System.Data.Entity.EntityState.Modified;
                            MyModels.SaveChanges();//保存

                            ShenMuYJ++;
                            Order++;
                            WanHouDeTail++;

                            strMag = "succsee";

                            //for (int YuanJianD = 0; YuanJianD < intYuanJianDing.Length;)
                            //{
                            //    for (int YuanXiD = 0; YuanXiD < intYuanXiDing.Length; YuanXiD++)
                            //    {

                            //    }
                            //}
                        }
                    }
                }
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 审核 保存（配货单）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult ShenehePeiZaiDan(B_ConverList OK,bool state)
        {
            //添加（这个类（bit 类型类））
            ReturnJsonVo returnJson = new ReturnJsonVo();

            string strMag = "fali";

            try
            {
                B_ConverList MyB_ConverList = new B_ConverList();

                MyB_ConverList.ConverID = OK.ConverID;
                MyB_ConverList.WareHouseID = OK.WareHouseID;
                MyB_ConverList.P_Remember = OK.P_Remember;
                MyB_ConverList.payName = OK.payName;
                MyB_ConverList.furlName = OK.furlName;
                MyB_ConverList.SpouseBRanchID = OK.SpouseBRanchID;
                MyB_ConverList.StockPlaceID = OK.StockPlaceID;
                MyB_ConverList.Remarks = OK.Remarks;
                MyB_ConverList.RegisterName = OK.RegisterName;
                MyB_ConverList.RegisterTime = OK.RegisterTime;

                MyB_ConverList.ExamineName = OK.ExamineName;
                MyB_ConverList.ExamineTime = OK.ExamineTime;

                MyModels.Entry(MyB_ConverList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                if (MyB_ConverList.ConverID > 0)
                {
                    strMag = "succsee";

                    B_ConverList wafrtbool = (from tbbool in MyModels.B_ConverList
                                              where tbbool.ConverID == MyB_ConverList.ConverID
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

                //return Json(strMag, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }

    }
}
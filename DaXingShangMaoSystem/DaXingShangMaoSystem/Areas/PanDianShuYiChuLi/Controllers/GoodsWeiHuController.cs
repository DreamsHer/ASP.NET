using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.PanDianShuYiChuLi.Controllers
{
    public class GoodsWeiHuController : Controller
    {
        // GET: PanDianShuYiChuLi/GoodsWeiHu
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        //主界面（视图）
        public ActionResult GoodsPanlWeiHu()
        {
            return View();
        }

        //选择盘点报告（视图）
        public ActionResult XuanPanlShiTu()
        {
            return View();
        }

        /// <summary>
        /// 下拉（盘点部门）
        /// </summary>
        /// <returns></returns>
        public ActionResult PanlBuMen()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in MyModels.B_PanlList
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.DrugTypeID,
                                                     text = tb.DrugType
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);

        }

        //一
        // <summary>
        ///查询盘点计划(记录列表)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckgh(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        //where tbGoodBiao.GoodsNot == false
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
                            Remember = tbGoodBiao.Remter,
                            PablData = tbGoodBiao.PablData.ToString(),
                            CommodityType = tbGoodBiao.CommodityType,
                            DrugType = tbGoodBiao.DrugType,

                            StockPlaceName = tbStock.StockPlaceName,
                            ExamineName = tbGoodBiao.ExamineName,
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
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


        // <summary>
        ///条件查询盘点计划(记录列表)
        /// </summary>
        /// <returns></returns>
        public ActionResult TiaoSelectCheckgh(Vo.BsgridPage bsgridPage,int StockPlaceID,string Remter)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.Remter== Remter && tbGoodBiao.GoodsNot == false || tbGoodBiao.StockPlaceID== StockPlaceID && tbGoodBiao.GoodsNot==false
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
                            Remember = tbGoodBiao.Remter,
                            PablData = tbGoodBiao.PablData.ToString(),
                            CommodityType = tbGoodBiao.CommodityType,
                            DrugType = tbGoodBiao.DrugType,

                            StockPlaceName = tbStock.StockPlaceName,
                            ExamineName = tbGoodBiao.ExamineName,
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
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


        // <summary>
        ///选中(记录列表)Id
        /// </summary>
        /// <returns></returns>
        public ActionResult ChuanlectCheckgh(int checkRermeberId)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.CheckRermeberID == checkRermeberId
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
                            Remember = tbGoodBiao.Remter,
                            PablData = tbGoodBiao.PablData.ToString(),
                            CommodityType = tbGoodBiao.CommodityType,
                            DrugType = tbGoodBiao.DrugType,

                            StockPlaceName = tbStock.StockPlaceName,
                            StockPlaceID = tbStock.StockPlaceID,
                            ExamineName = tbGoodBiao.ExamineName,
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),
                            OrderFormPactID = tbGoodBiao.OrderFormPactID,
                            GoodsClassifyID = tbGoodBiao.GoodsClassifyID,
                            GoodsChopID = tbGoodBiao.GoodsChopID,

                        }).ToList();
            Session["CheckRermeberID"] = linq[0].CheckRermeberID; // 传递
            Session["OrderFormPactID"] = linq[0].OrderFormPactID; // 传递
            Session["GoodsClassifyID"] = linq[0].GoodsClassifyID;
            Session["GoodsChopID"] = linq[0].GoodsChopID;
            Session["StockPlaceID"] = linq[0].StockPlaceID;

            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        //二
        // <summary>
        ///查询(绑定)盘点计划
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsff()
        {
            string CheckRermeberId = Session["CheckRermeberID"].ToString();//计划盘点id   
            int intCheckRermeId = Convert.ToInt32(CheckRermeberId);

            var linq = (from tbPanlList in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbPanlList.StockPlaceID equals tbStock.StockPlaceID//库存
                        where tbPanlList.CheckRermeberID== intCheckRermeId

                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbPanlList.CheckRermeberID,
                            Remember = tbPanlList.Remter,
                            DrugType = tbPanlList.DrugType,
                            StockPlaceName = tbStock.StockPlaceName,
                        }).ToList();

            return Json(linq, JsonRequestBehavior.AllowGet);

        }


        //二（第一种情况有商品）
        // <summary>
        ///查询(绑定)盘点计划(商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsffGoodsjHave(Vo.BsgridPage bsgridPage)
        {
            string CheckRermeberId = Session["CheckRermeberID"].ToString();//计划盘点id   
            int intCheckRermeId = Convert.ToInt32(CheckRermeberId);

            var linq = (from tbChckenDetiList in MyModels.B_CheckRemerbenDetillList
                        join tbGoods in MyModels.B_GoodsList on tbChckenDetiList.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                        join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                        join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                        where tbChckenDetiList.CheckRermeberID == intCheckRermeId
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRemerbenDetillD=tbChckenDetiList.CheckRemerbenDetillD,
                            GoodsIDs = tbGoods.GoodsID,
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsNames = tbGoods.GoodsName,//商品名称

                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                            SpecificationsModels = tbGoods.SpecificationsModel,//规格
                            ArtNos = tbGoods.ArtNo,//商品货号
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                            JiaZaiFou=tbChckenDetiList.JiaZaiFou,//加载否
                            MumberOfPackages = tbChckenDetiList.MumberOfPackages,//件数
                            Subdivision = tbChckenDetiList.Subdivision,//细数

                        }).ToList();
            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();
            Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }








        //二(第二种情况没商品)
        // <summary>
        ///查询(绑定)盘点计划(商品)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheckewsffGoodsj(Vo.BsgridPage bsgridPage,Array ChuangDiGoodsIDShuZu)
        {
            //一
            string Z = ((string[])ChuangDiGoodsIDShuZu)[0];
            string[] intsZ = Z.Split(',');
            List<WareHouseDeitaLL> list = new List<WareHouseDeitaLL>();

            
                for (int i = 0; i < intsZ.Length;)
                {
                    var goodsIDs = Convert.ToString(intsZ[i]);
                    var linq = (from tbGoods in MyModels.B_GoodsList//商品
                                join tbGoodChoClass in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodChoClass.GoodsClassifyID//商品分类
                                join tbShangBiao in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标

                                join tbJiLiangDanWei in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                                join tbGoodDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                                join tbBaoZhuangXin in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbBaoZhuangXin.GoodsDetailID//包装信息
                                where tbGoods.GoodsCode == goodsIDs
                                select new LY.WareHouseDeitaLL
                                {
                                    GoodsIDs = tbGoods.GoodsID,
                                    GoodsCodes = tbGoods.GoodsCode,//代码
                                    GoodsNames = tbGoods.GoodsName,//商品名称

                                    GoodsTiaoMas = tbGoods.GoodsTiaoMa,//商品条码
                                    SpecificationsModels = tbGoods.SpecificationsModel,//规格
                                    ArtNos = tbGoods.ArtNo,//商品货号
                                    TaxBids = tbGoodDetail.TaxBid,//含税进价
                                    PackInfos = tbBaoZhuangXin.PackContent,//包装含量
                                    RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价
                                    EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位

                                }).ToList();
                    list.AddRange(linq);
                    i++;
                }
                int totalRow = list.Count();
                List<LY.WareHouseDeitaLL> notices = list.OrderByDescending(p => p.GoodsChopID).//noboer表达式
                     Skip(bsgridPage.GetStartIndex()).
                    Take(bsgridPage.pageSize).
                    ToList();
                Vo.Bsgrid<LY.WareHouseDeitaLL> bsgrid = new Vo.Bsgrid<LY.WareHouseDeitaLL>();
                bsgrid.success = true;
                bsgrid.totalRows = totalRow;
                bsgrid.curPage = bsgridPage.curPage;//请求当前页
                bsgrid.data = notices;//查出的数据
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 开始 新增 保存（盘点商品）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectCher(B_CheckRemerbenDetillList Ok,Array JiShangPiID, Array JiRuJianShu, Array JiRuXiShu,bool state)
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

                B_CheckRemerbenDetillList ConverDeTailList = new B_CheckRemerbenDetillList();

                for (int H = 0; H < intsNXiShu.Length;)
                {
                    for (int E = 0; E < intsRuJian.Length;)
                    {
                        for (int d = 0; d < intsid.Length; d++)
                        {

                            ConverDeTailList.CheckRermeberID = Ok.CheckRermeberID;//
                            ConverDeTailList.GoodsID = Convert.ToInt32(intsid[d]); ;//商品ID
                            ConverDeTailList.Subdivision = Convert.ToDecimal(intsNXiShu[H]);//入库细数
                            ConverDeTailList.MumberOfPackages = Convert.ToDecimal(intsRuJian[E]);//入库件数
                            MyModels.B_CheckRemerbenDetillList.Add(ConverDeTailList);
                            MyModels.SaveChanges();//保存
                            H++;
                            E++;
                            strMag = "succsee";
                        }
                    }
                }

                B_CheckRemerbenList wafrtbool = (from tbbool in MyModels.B_CheckRemerbenList
                                        where tbbool.CheckRermeberID == Ok.CheckRermeberID
                                        select tbbool).Single();//查询原状态
                wafrtbool.GoodsNot = state;
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(new { strMag, returnJson }, JsonRequestBehavior.AllowGet);
        }










    }
}
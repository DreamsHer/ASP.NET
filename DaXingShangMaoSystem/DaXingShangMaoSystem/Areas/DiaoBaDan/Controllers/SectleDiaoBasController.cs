using DaXingShangMaoSystem.LY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.DiaoBaDan.Controllers
{
    public class SectleDiaoBasController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: DiaoBaDan/SectleDiaoBas
        public ActionResult SelectDianBa()
        {
            return View();
        }


        /// <summary>
        /// 查询调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectConverList(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbConverlist in MyModels.B_AllocateList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchIDtwo equals tbShouHuoBuMen.SpouseBRanchID
                        where tbConverlist.ExamineNot==true
                        select new LY.PeiHuoDan
                        {
                            AllocateID = tbConverlist.AllocateID,//id
                            Remember = tbConverlist.Remember,//编号
                            payName = tbConverlist.payNamel,//发货人
                            furlName = tbConverlist.furlNamel,//收货人
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,//发货部门
                            SpouseBRanchNsame = tbShouHuoBuMen.SpouseBRanchName,//收货部门
                            Remarks = tbConverlist.Remarks,//备注

                            RegisterName = tbConverlist.RegisterName,//制单人
                            registerTime = tbConverlist.RegisterTime.ToString(),//制单时间
                            ExamineName = tbConverlist.ExamineName,//审核人
                            examineTime = tbConverlist.ExecuteTime.ToString(),//审核时间
                            ExamineNot = tbConverlist.ExamineNot//审核否

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
        /// 查询对应商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunShangPin(Vo.BsgridPage bsgridPage, int allocateID)
        {
            var Linq = (from tbWareHouseDetiai in MyModels.B_AllocateDetailList//配货明细
                        join tbSelectGoods in MyModels.B_GoodsList on tbWareHouseDetiai.GoodsID equals tbSelectGoods.GoodsID//商品
                        join tbJiSuanDan in MyModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWareHouseDetiai.AllocateID == allocateID
                        select new LY.PeiHuoDan
                        {
                            AllocateID = allocateID,
                            GoodsIDs = tbSelectGoods.GoodsID,//商品ID
                            GoodsCodes = tbSelectGoods.GoodsCode,//商品代码
                            GoodsTiaoMas = tbSelectGoods.GoodsTiaoMa,//商品条码
                            GoodsNames = tbSelectGoods.GoodsName,//商品名称
                            ArtNos = tbSelectGoods.ArtNo,//商品货号

                            SpecificationsModels = tbSelectGoods.SpecificationsModel,//规格
                            EstimateUnitNames = tbJiSuanDan.EstimateUnitName,//计量单位
                            PackContents = tbPackln.PackContent,//包装含量
                            TaxBids = tbGoodDetail.TaxBid,//含税进价
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//零售单价

                            MumberOfPackages = tbWareHouseDetiai.MumberOfPackages,//件数
                            Subdivision = tbWareHouseDetiai.Subdivision//入库小数


                        }).ToList();

            int totalRow = Linq.Count();
            List<LY.PeiHuoDan> notices = Linq.OrderByDescending(p => p.GoodsIDs).//noboer表达式
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
        /// 浏览器打印调拨单
        /// </summary>
        /// <returns></returns>
        public ActionResult DaYinConverList()
        {
            var Linq = (from tbConverlist in MyModels.B_AllocateList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchIDtwo equals tbShouHuoBuMen.SpouseBRanchID
                        where tbConverlist.ExamineNot==true
                        select new LY.PeiHuoDan
                        {
                            AllocateID = tbConverlist.AllocateID,//id
                            Remember = tbConverlist.Remember,//编号
                            payName = tbConverlist.payNamel,//发货人
                            furlName = tbConverlist.furlNamel,//收货人
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,//发货部门
                            SpouseBRanchNsame = tbShouHuoBuMen.SpouseBRanchName,//发货部门
                            Remarks = tbConverlist.Remarks,//备注

                            RegisterName = tbConverlist.RegisterName,//制单人
                            registerTime = tbConverlist.RegisterTime.ToString(),//制单时间
                            ExamineName = tbConverlist.ExamineName,//审核人
                            examineTime = tbConverlist.ExecuteTime.ToString(),//审核时间
                            ExamineNots = tbConverlist.ExamineNot.ToString()//审核否

                        }).ToList();

            List<PeiHuoDan> listWareHouseDeitaLL = new List<PeiHuoDan>();

            for (int i = 0; i < Linq.Count; i++)
            {
                PeiHuoDan myWareHouseDeitaLL = new PeiHuoDan();
                if (Convert.ToBoolean(Linq[i].ExamineNots) == true)
                {
                    myWareHouseDeitaLL.ExamineNots = "已审核";
                }
                else
                {
                    myWareHouseDeitaLL.ExamineNots = "未审核";
                }

                myWareHouseDeitaLL.Remember = Linq[i].Remember;
                myWareHouseDeitaLL.payName = Linq[i].payName;
                myWareHouseDeitaLL.furlName = Linq[i].furlName;
                myWareHouseDeitaLL.SpouseBRanchName = Linq[i].SpouseBRanchName;
                myWareHouseDeitaLL.SpouseBRanchNsame = Linq[i].SpouseBRanchNsame;
                myWareHouseDeitaLL.Remarks = Linq[i].Remarks;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;

                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);

        }

    }
}
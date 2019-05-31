using CrystalDecisions.CrystalReports.Engine;
using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.P_PeiHuo.Controllers
{
    public class SelectPeiHuoController : Controller
    {

        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: P_PeiHuo/SelectPeiHuo
        public ActionResult SelectPeiHuo()
        {
            return View();
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
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            P_Remember = tbConverlist.P_Remember,//编号
                            payName = tbConverlist.payName,//发货人
                            furlName = tbConverlist.furlName,//收货人
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,//发货部门
                            StockPlaceName = tbShouHuoBuMen.StockPlaceName,//发货部门
                            Remarks = tbConverlist.Remarks,//备注

                            RegisterName = tbConverlist.RegisterName,//制单人
                            registerTime = tbConverlist.RegisterTime.ToString(),//制单时间
                            ExamineName = tbConverlist.ExamineName,//审核人
                            examineTime = tbConverlist.ExamineTime.ToString(),//审核时间
                            ExamineNot=tbConverlist.ExamineNot//审核否

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
        public ActionResult ChaXunShangPin(Vo.BsgridPage bsgridPage, int converID)
        {
            var Linq = (from tbWareHouseDetiai in MyModels.B_ConverDeTailList//配货明细
                        join tbSelectGoods in MyModels.B_GoodsList on tbWareHouseDetiai.GoodsID equals tbSelectGoods.GoodsID//商品
                        join tbJiSuanDan in MyModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in MyModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in MyModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWareHouseDetiai.ConverID == converID
                        select new LY.PeiHuoDan
                        {
                            ConverID = converID,
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
        /// 浏览器打印配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult DaYinConverList()
        {
            var Linq = (from tbConverlist in MyModels.B_ConverList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID
                        select new LY.PeiHuoDan
                        {
                            ConverID = tbConverlist.ConverID,//id
                            P_Remember = tbConverlist.P_Remember,//编号
                            payName = tbConverlist.payName,//发货人
                            furlName = tbConverlist.furlName,//收货人
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,//发货部门
                            StockPlaceName = tbShouHuoBuMen.StockPlaceName,//发货部门
                            Remarks = tbConverlist.Remarks,//备注

                            RegisterName = tbConverlist.RegisterName,//制单人
                            registerTime = tbConverlist.RegisterTime.ToString(),//制单时间
                            ExamineName = tbConverlist.ExamineName,//审核人
                            examineTime = tbConverlist.ExamineTime.ToString(),//审核时间
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

                myWareHouseDeitaLL.P_Remember = Linq[i].P_Remember;
                myWareHouseDeitaLL.payName = Linq[i].payName;
                myWareHouseDeitaLL.furlName = Linq[i].furlName;
                myWareHouseDeitaLL.SpouseBRanchName = Linq[i].SpouseBRanchName;
                myWareHouseDeitaLL.StockPlaceName = Linq[i].StockPlaceName;
                myWareHouseDeitaLL.Remarks = Linq[i].Remarks;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;

                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);

        }


        /// <summary>
        /// 水晶打印配货单
        /// </summary>
        /// <returns></returns>
        public ActionResult ShuiJingConverList()
        {
            var Linq = (from tbConverlist in MyModels.B_ConverList
                        join tbFaHuoBuMen in MyModels.S_SpouseBRanchList on tbConverlist.SpouseBRanchID equals tbFaHuoBuMen.SpouseBRanchID
                        join tbShouHuoBuMen in MyModels.S_StockPlaceList on tbConverlist.StockPlaceID equals tbShouHuoBuMen.StockPlaceID
                        orderby tbConverlist.P_Remember descending
                        select new LY.CeShi
                        {
                            ConverID = tbConverlist.ConverID,//id
                            P_Remember = tbConverlist.P_Remember,//编号
                            payName = tbConverlist.payName,//发货人
                            furlName = tbConverlist.furlName,//收货人
                            SpouseBRanchName = tbFaHuoBuMen.SpouseBRanchName,//发货部门
                            StockPlaceName = tbShouHuoBuMen.StockPlaceName,//发货部门
                            Remarks = tbConverlist.Remarks,//备注

                            RegisterName = tbConverlist.RegisterName,//制单人
                            registerTime = tbConverlist.RegisterTime.ToString(),//制单时间
                            ExamineName = tbConverlist.ExamineName,//审核人
                            examineTime = tbConverlist.ExamineTime.ToString(),//审核时间
                            ExamineNot = tbConverlist.ExamineNot.ToString()//审核否

                        }).ToList();

            List<CeShi> listWareHouseDeitaLL = new List<CeShi>();

            for (int i = 0; i < Linq.Count; i++)
            {
                CeShi myWareHouseDeitaLL = new CeShi();
                if (Convert.ToBoolean(Linq[i].ExamineNot) == true)
                {
                    myWareHouseDeitaLL.ExamineNot = "已审核";
                }
                else
                {
                    myWareHouseDeitaLL.ExamineNot = "未审核";
                }

                myWareHouseDeitaLL.P_Remember = Linq[i].P_Remember;
                myWareHouseDeitaLL.payName = Linq[i].payName;
                myWareHouseDeitaLL.furlName = Linq[i].furlName;
                myWareHouseDeitaLL.SpouseBRanchName = Linq[i].SpouseBRanchName;
                myWareHouseDeitaLL.StockPlaceName = Linq[i].StockPlaceName;
                myWareHouseDeitaLL.Remarks = Linq[i].Remarks;
                myWareHouseDeitaLL.registerTime = Linq[i].registerTime;
                myWareHouseDeitaLL.RegisterName = Linq[i].RegisterName;

                myWareHouseDeitaLL.ExamineName = Linq[i].ExamineName;
                myWareHouseDeitaLL.examineTime = Linq[i].examineTime;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            //查询数据
            List<CeShi> listExaminee = listWareHouseDeitaLL;

            //实例table
            DataTable dt = new DataTable();

            //给table添加列 
            dt.Columns.Add("P_Remember");
            dt.Columns.Add("payName");
            dt.Columns.Add("furlName");
            dt.Columns.Add("SpouseBRanchName");
            dt.Columns.Add("StockPlaceName");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("registerTime");
            dt.Columns.Add("RegisterName");
            dt.Columns.Add("ExamineName");
            dt.Columns.Add("examineTime"); 
            dt.Columns.Add("ExamineNot"); 

            foreach (CeShi item in listExaminee)
            {
                DataRow dr = dt.NewRow();
                dr["P_Remember"] = item.P_Remember;
                dr["payName"] = item.payName;
                dr["furlName"] = item.furlName;
                dr["SpouseBRanchName"] = item.SpouseBRanchName;
                dr["StockPlaceName"] = item.StockPlaceName;
                dr["Remarks"] = item.Remarks;
                dr["registerTime"] = item.registerTime;
                dr["RegisterName"] = item.RegisterName;
                dr["ExamineName"] = item.ExamineName;
                dr["examineTime"] = item.examineTime;
                dr["ExamineNot"] = item.ExamineNot;

                dt.Rows.Add(dr);
            }


            //1、实例数据集  
            CRP.DataSWareHou dtAchievement = new CRP.DataSWareHou();

            //2、合并：
            dtAchievement.Tables["DataPeiHuoDan"].Merge(dt);

            ReportDocument rd = new ReportDocument();

            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CRP\\CrystalRPeiHuo.rpt";

            rd.Load(strRptPath);
            rd.SetDataSource(dtAchievement); //设置报表数据源

            //最后（IO 流形式）
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(stream, "application/pdf");

        }

    }
}
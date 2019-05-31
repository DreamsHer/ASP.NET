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

namespace DaXingShangMaoSystem.Areas.ShangPinJinCang.Controllers
{
    public class SelectWaretFController : Controller
    {
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: ShangPinJinCang/SelectWaretF
        public ActionResult SelectWarent()
        {
            return View();
        }


        /// <summary>
        /// 进仓单类型
        /// </summary>
        /// <returns></returns>
        public ActionResult QuarctJin()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in myModels.S_QuFenLeiXingList
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.QuFenLeiXingID,
                                                     text = tb.QuFenLeiXingMC
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询进仓单（一）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunWareFon(Vo.BsgridPage bsgridPage, int quFenLeiXingID)
        {
            //进仓、员工、类型、合同、供货商、结算方式
            var Linq = from tbWaerHouse in myModels.B_WareHouseList
                       join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                       join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                       join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                       join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                       join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算

                       select new LY.WareHouseDeitaLL
                       {
                           WareHouseID = tbWaerHouse.WareHouseID,//进仓ID
                           Remember = tbWaerHouse.Remember,//进仓编号
                           OrderFormPactID = tbHeTong.OrderFormPactID,//合同ID
                           ContractNumber = tbHeTong.ContractNumber,//合同编号
                           MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                           SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                           StaffID = tbStall.StaffID,//员工ID
                           StaffCode = tbStall.StaffCode,//员工编号
                           StaffName = tbStall.StaffName,//姓名
                           RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                           ExamineNot = tbWaerHouse.ExamineNot,//审核状态
                           Status = tbWaerHouse.Status,//生效状态
                           CrushRedNot = tbWaerHouse.CrushRedNot,//冲红状态
                           QuFenLeiXingID = tbQuFen.QuFenLeiXingID,//区分ID
                           QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
                       };

            //区分ID不为空（因为是下拉框）
            if (quFenLeiXingID > 0)
            {
                Linq = Linq.Where(p => p.QuFenLeiXingID == quFenLeiXingID);
            }

            int totalRow = Linq.Count();
            List<LY.WareHouseDeitaLL> notices = Linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
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
        /// 查询对应商品（二）
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunShangPin(Vo.BsgridPage bsgridPage, int wareHouseID)
        {
            var Linq = (from tbWareHouseDetiai in myModels.B_WareHouseDetiailList//进仓明细
                        join tbSelectGoods in myModels.B_GoodsList on tbWareHouseDetiai.OrderFormDetailID equals tbSelectGoods.GoodsID//商品
                        join tbJiSuanDan in myModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                        where tbWareHouseDetiai.WareHouseID == wareHouseID
                        select new LY.WareHouseDeitaLL
                        {
                            OrderFormDetailID = tbWareHouseDetiai.OrderFormDetailID,
                            WareHouseDetiailID = tbWareHouseDetiai.WareHouseDetiailID,
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
            List<LY.WareHouseDeitaLL> notices = Linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
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
        /// 浏览器打印
        /// </summary>
        /// <returns></returns>
        public ActionResult WidowesDaYin()
        {
            var Linq = (from tbWaerHouse in myModels.B_WareHouseList
                        join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                        join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                        join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                        join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                        orderby tbWaerHouse.Remember descending
                        select new LY.GongHuoShang
                        {
                            Remember = tbWaerHouse.Remember,//进仓编号
                            ContractNumber = tbHeTong.ContractNumber,//合同编号
                            MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                            SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                            StaffCode = tbStall.StaffCode,//员工编号
                            StaffName = tbStall.StaffName,//姓名
                            RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                            ExamineNot = tbWaerHouse.ExamineNot.ToString(),//审核状态
                            Status = tbWaerHouse.Status.ToString(),//生效状态
                            CrushRedNot = tbWaerHouse.CrushRedNot.ToString(),//冲红状态
                            QuFenLeiXingID = tbQuFen.QuFenLeiXingID,//区分ID
                            QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
                        }).ToList();
            List<GongHuoShang> listWareHouseDeitaLL = new List<GongHuoShang>();

            for (int i = 0; i < Linq.Count; i++)
            {
                GongHuoShang myWareHouseDeitaLL = new GongHuoShang();
                if (Convert.ToBoolean(Linq[i].ExamineNot) == true)
                {
                    myWareHouseDeitaLL.ExamineNot = "已审核";
                }
                else
                {
                    myWareHouseDeitaLL.ExamineNot = "未审核";
                }

                if (Convert.ToBoolean(Linq[i].Status) == true)
                {
                    myWareHouseDeitaLL.Status = "已生效";
                }
                else
                {
                    myWareHouseDeitaLL.Status = "未生效";
                }

                if (Convert.ToBoolean(Linq[i].CrushRedNot) == true)
                {
                    myWareHouseDeitaLL.CrushRedNot = "冲红单";
                }
                else
                {
                    myWareHouseDeitaLL.CrushRedNot = "正常有效使用单";
                }

                myWareHouseDeitaLL.Remember = Linq[i].Remember;
                myWareHouseDeitaLL.ContractNumber = Linq[i].ContractNumber;
                myWareHouseDeitaLL.MethodOfSettlingAccounts = Linq[i].MethodOfSettlingAccounts;
                myWareHouseDeitaLL.SupplierCHName = Linq[i].SupplierCHName;
                myWareHouseDeitaLL.StaffCode = Linq[i].StaffCode;
                myWareHouseDeitaLL.StaffName = Linq[i].StaffName;
                myWareHouseDeitaLL.RegisterTime = Linq[i].RegisterTime;
                myWareHouseDeitaLL.QuFenLeiXingID = Linq[i].QuFenLeiXingID;
                myWareHouseDeitaLL.QuFenLeiXingMC = Linq[i].QuFenLeiXingMC;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }

            return PartialView(listWareHouseDeitaLL);
        }


        /// <summary>
        /// 水晶报表
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        /// 
        public ActionResult ShuiJIngBaoBiao(int quFenLeiXingID)
        {
            //进仓、员工、类型、合同、供货商、结算方式
            var Linq = (from tbWaerHouse in myModels.B_WareHouseList
                        join tbStall in myModels.B_StaffList on tbWaerHouse.StaffID equals tbStall.StaffID//员工
                        join tbQuFen in myModels.S_QuFenLeiXingList on tbWaerHouse.QuFenLeiXingID equals tbQuFen.QuFenLeiXingID//区分类型

                        join tbHeTong in myModels.B_OrderFormPactList on tbWaerHouse.OrderFormPactID equals tbHeTong.OrderFormPactID//合同
                        join tbSupplier in myModels.B_SupplierList on tbHeTong.SupplierID equals tbSupplier.SupplierID//供货商
                        join tbJieSuan in myModels.S_MethodOfSettlingAccountsList on tbHeTong.MethodOfSettlingAccountsID equals tbJieSuan.MethodOfSettlingAccountsID//结算
                        orderby tbWaerHouse.Remember descending
                        select new LY.GongHuoShang
                        {
                          
                            Remember = tbWaerHouse.Remember,//进仓编号

                            ContractNumber = tbHeTong.ContractNumber,//合同编号
                            MethodOfSettlingAccounts = tbJieSuan.MethodOfSettlingAccounts,//结算方式
                            SupplierCHName = tbSupplier.SupplierCHName,//供货商名称
                            StaffCode = tbStall.StaffCode,//员工编号
                            StaffName = tbStall.StaffName,//姓名
                            RegisterTime = tbWaerHouse.RegisterTime.ToString(),//登记时间
                            ExamineNot = tbWaerHouse.ExamineNot.ToString(),//审核状态
                            Status = tbWaerHouse.Status.ToString(),//生效状态
                            CrushRedNot = tbWaerHouse.CrushRedNot.ToString(),//冲红状态
                            QuFenLeiXingID = tbQuFen.QuFenLeiXingID,//区分ID
                            QuFenLeiXingMC = tbQuFen.QuFenLeiXingMC//区分
                        }).ToList();
          
            List<GongHuoShang> listWareHouseDeitaLL = new List<GongHuoShang>();

            for (int i = 0; i < Linq.Count; i++)
            {
                GongHuoShang myWareHouseDeitaLL = new GongHuoShang();
                if (Convert.ToBoolean(Linq[i].ExamineNot) == true)
                {
                    myWareHouseDeitaLL.ExamineNot = "已审核";
                }
                else
                {
                    myWareHouseDeitaLL.ExamineNot = "未审核";
                }

                if (Convert.ToBoolean(Linq[i].Status) == true)
                {
                    myWareHouseDeitaLL.Status = "已生效";
                }
                else
                {
                    myWareHouseDeitaLL.Status = "未生效";
                }

                if (Convert.ToBoolean(Linq[i].CrushRedNot) == true)
                {
                    myWareHouseDeitaLL.CrushRedNot = "冲红单";
                }
                else
                {
                    myWareHouseDeitaLL.CrushRedNot = "正常有效使用单";
                }

                myWareHouseDeitaLL.Remember = Linq[i].Remember;
                myWareHouseDeitaLL.ContractNumber = Linq[i].ContractNumber;
                myWareHouseDeitaLL.MethodOfSettlingAccounts = Linq[i].MethodOfSettlingAccounts;
                myWareHouseDeitaLL.SupplierCHName = Linq[i].SupplierCHName;
                myWareHouseDeitaLL.StaffCode = Linq[i].StaffCode;
                myWareHouseDeitaLL.StaffName = Linq[i].StaffName;
                myWareHouseDeitaLL.RegisterTime = Linq[i].RegisterTime;
                myWareHouseDeitaLL.QuFenLeiXingID = Linq[i].QuFenLeiXingID;
                myWareHouseDeitaLL.QuFenLeiXingMC = Linq[i].QuFenLeiXingMC;
                listWareHouseDeitaLL.Add(myWareHouseDeitaLL);
            }


            //查询数据
            List<GongHuoShang> listExaminee = listWareHouseDeitaLL;

            //实例table
            DataTable dt = new DataTable();

            //给table添加列 
            dt.Columns.Add("Remember");
            dt.Columns.Add("ContractNumber");
            dt.Columns.Add("MethodOfSettlingAccounts");
            dt.Columns.Add("SupplierCHName");
            dt.Columns.Add("StaffCode");
            dt.Columns.Add("StaffName");
            dt.Columns.Add("RegisterTime");
            dt.Columns.Add("ExamineNot");//如果出错bit 类型，去数据集改类型
            dt.Columns.Add("Status");
            dt.Columns.Add("CrushRedNot");
            dt.Columns.Add("QuFenLeiXingMC");


            foreach (GongHuoShang item in listExaminee)
            {
                DataRow dr = dt.NewRow();
                dr["Remember"] = item.Remember;
                dr["ContractNumber"] = item.ContractNumber;
                dr["MethodOfSettlingAccounts"] = item.MethodOfSettlingAccounts;
                dr["SupplierCHName"] = item.SupplierCHName;
                dr["StaffCode"] = item.StaffCode;
                dr["StaffName"] = item.StaffName;
                dr["RegisterTime"] = item.RegisterTime;
                dr["ExamineNot"] = item.ExamineNot;
                dr["Status"] = item.Status;
                dr["CrushRedNot"] = item.CrushRedNot;
                dr["QuFenLeiXingMC"] = item.QuFenLeiXingMC;
                dt.Rows.Add(dr);
            }


            //1、实例数据集  
            CRP.DataSWareHou dtAchievement = new CRP.DataSWareHou();

            //2、合并：
            dtAchievement.Tables["DataWareHouer"].Merge(dt);

            ReportDocument rd = new ReportDocument();

            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Areas\\CRP\\CrystalReWareHour.rpt";

            rd.Load(strRptPath);
            rd.SetDataSource(dtAchievement); //设置报表数据源

            //最后（IO 流形式）
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return File(stream, "application/pdf");
        }



        public ActionResult ChaXunShangPinShu(int wareHouseID)
        {
            var Linq = (from tbWanHouct in myModels.B_WareHouseList
                        join tbWareHouseDetiai in myModels.B_WareHouseDetiailList on tbWanHouct.WareHouseID equals tbWareHouseDetiai.WareHouseID//
                        join tbStocRtcf in myModels.S_StockPlaceList on tbWanHouct.StockPlaceID equals tbStocRtcf.StockPlaceID

                        join tbSelectGoods in myModels.B_GoodsList on tbWareHouseDetiai.OrderFormDetailID equals tbSelectGoods.GoodsID//
                        join tbJiSuanDan in myModels.S_EstimateUnitList on tbSelectGoods.EstimateUnitID equals tbJiSuanDan.EstimateUnitID//
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbSelectGoods.GoodsID equals tbGoodDetail.GoodsID//

                        where tbWareHouseDetiai.WareHouseID == wareHouseID
                        select new LY.WareHouseDeitaLL
                        {
                            StockPlaceName=tbStocRtcf.StockPlaceName,
                            RetailMonovalents = tbGoodDetail.RetailMonovalent,//
                            MumberOfPackages = tbWareHouseDetiai.MumberOfPackages,//

                        }).ToList();
            var hfdf = 0;
            var JinE = 0;
            for (int i = 0; i < Linq.Count; i++)
            {
                var ShuLiang = Linq[i].MumberOfPackages;
                var JiaGe = Linq[i].RetailMonovalents;

                hfdf = hfdf + Convert.ToInt32(ShuLiang);
                JinE = JinE + Convert.ToInt32(JiaGe);
            }
            var StockPlaceName="";
             StockPlaceName= Linq[0].StockPlaceName;

            return Json(new { hfdf, JinE , StockPlaceName }, JsonRequestBehavior.AllowGet);

        }

    }
}
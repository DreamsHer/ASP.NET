using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.GoodsZhangGuanLi.Controllers
{
    public class TianQiController : Controller
    {
        Models.DaXingShangMaoXiTongEntities myModels = new DaXingShangMaoXiTongEntities();
        // GET: GoodsZhangGuanLi/TianQi
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 开始 新增天气
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult InsectJingCangDan(B_WeatherList WareHouse)
        {
            string strMag = "fali";
            try
            {
                //判断记录编号是否存在
                int oldWareHouseRows = (from tbWareHouse in myModels.B_WeatherList
                                        where tbWareHouse.Data == WareHouse.Data
                                        select tbWareHouse).Count();
                B_WeatherList myB_WeatherList = new B_WeatherList();
                if (oldWareHouseRows == 0)
                {

                    myB_WeatherList.Data = WareHouse.Data;
                    myB_WeatherList.WeatherCondition = WareHouse.WeatherCondition;
                    myModels.B_WeatherList.Add(myB_WeatherList);
                    myModels.SaveChanges();//保存
                    strMag = "succsee";
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }





        //第二（查询租赁销售)
        public ActionResult ZuLinXiaoShou()
        {
            return View();
        }


        /// <summary>
        /// 查询商品代码
        /// </summary>
        /// <returns></returns>
        public ActionResult BangFanChangGoodel(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbShangPin in myModels.B_GoodsList//商品

                        join tbGoodDetail in myModels.B_GoodsDetailList on tbShangPin.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        join tbPackln in myModels.S_PackInfoIDList on tbGoodDetail.GoodsDetailID equals tbPackln.GoodsDetailID//包装信息表
                       
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbShangPin.GoodsID,
                            GoodsCode =tbShangPin.GoodsCode,
                            GoodsName = tbShangPin.GoodsName,

                        }).ToList();
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.GoodsCode).//noboer表达式
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
        /// 绑定商品代码
        /// </summary>
        /// <returns></returns>
        public ActionResult BanggGoodel(int GoodsIDs)
        {
            var linq = (from tbShangPin in myModels.B_GoodsList//商品
                        where tbShangPin.GoodsID== GoodsIDs
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbShangPin.GoodsID,
                            GoodsCode = tbShangPin.GoodsCode,
                            GoodsName = tbShangPin.GoodsName,

                        }).ToList();
           
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询供货商
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSuppoc(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbShangPin in myModels.B_SupplierList//供货商
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbShangPin.SupplierID,
                            GoodsCode = tbShangPin.SupplierNumber,
                            GoodsName = tbShangPin.SupplierName,

                        }).ToList();
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.GoodsCode).//noboer表达式
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
        /// 绑定供货商
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingSuppoc(int SupplierID)
        {
            var linq = (from tbShangPin in myModels.B_SupplierList//供货商
                        where tbShangPin.SupplierID== SupplierID
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbShangPin.SupplierID,
                            GoodsCode = tbShangPin.SupplierNumber,
                            GoodsName = tbShangPin.SupplierName,

                        }).ToList();
           
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// 查询合同
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectHeTong(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbShangPin in myModels.B_OrderFormPactList//合同
                        where tbShangPin.AgreementTypeID == 4
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbShangPin.OrderFormPactID,
                            GoodsCode = tbShangPin.ContractNumber,

                        }).ToList();
            int totalRow = linq.Count();
            List<LY.PeiHuoDan> notices = linq.OrderByDescending(p => p.GoodsCode).//noboer表达式
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

        /// 绑定合同
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingtHeTong(int OrderFormPactID)
        {
            var linq = (from tbShangPin in myModels.B_OrderFormPactList//合同
                        where tbShangPin.OrderFormPactID == OrderFormPactID
                        select new LY.PeiHuoDan
                        {
                            GoodsIDs = tbShangPin.OrderFormPactID,
                            GoodsCode = tbShangPin.ContractNumber,

                        }).ToList();
           
            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 点击“”查询 进仓单
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public ActionResult ChaXunJinCang(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbSelctWanrHon in myModels.B_WareHouseList//进仓单
                        join tbJinHuoBumen in myModels.S_SpouseBRanchList on tbSelctWanrHon.SpouseBRanchID equals tbJinHuoBumen.SpouseBRanchID//进货部门
                        join tbStall in myModels.B_StaffList on tbSelctWanrHon.StaffID equals tbStall.StaffID//员工
                        join tbHeTong in myModels.B_OrderFormPactList on tbSelctWanrHon.OrderFormPactID equals tbHeTong.OrderFormPactID//合同单
                        join tbGongHuoShang in myModels.B_SupplierList on tbHeTong.SupplierID equals tbGongHuoShang.SupplierID//供货商
                        where tbSelctWanrHon.QuFenLeiXingID == 4 && tbSelctWanrHon.ExamineNot == true && tbSelctWanrHon.CrushRedNot == false
                        select new LY.SelectOrderPact
                        {
                            WareHouseID = tbSelctWanrHon.WareHouseID,//进仓单
                            Remember = tbSelctWanrHon.Remember,//记录编号

                            SpouseBRanchName = tbJinHuoBumen.SpouseBRanchName,//进货部门
                          

                            Appendix = tbSelctWanrHon.Appendix,//附件
                            Evidences = tbSelctWanrHon.Evidences,//原始单号
                            StaffCode = tbStall.StaffCode,//收货人编号
                            StaffName = tbStall.StaffName,//收货人姓名
                            RegisterTime = tbSelctWanrHon.RegisterTime.ToString(),//登记时间

                            OrderFormPactIDs = tbSelctWanrHon.OrderFormPactID,//合同ID
                            ContractNumber = tbHeTong.ContractNumber,//合同号
                        
                            SupplierCHName = tbGongHuoShang.SupplierCHName,//供货商单位
                            ExamineNot = tbSelctWanrHon.ExamineNot,//审核否
                            Status = tbSelctWanrHon.Status//生效否

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.SelectOrderPact> notices = linq.OrderByDescending(p => p.WareHouseID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<LY.SelectOrderPact> bsgrid = new Vo.Bsgrid<LY.SelectOrderPact>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }






    }
}
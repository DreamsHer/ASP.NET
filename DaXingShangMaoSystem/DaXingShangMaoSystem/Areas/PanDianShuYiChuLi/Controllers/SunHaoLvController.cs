using DaXingShangMaoSystem.LY;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.PanDianShuYiChuLi.Controllers
{
    public class SunHaoLvController : Controller
    {
        // GET: PanDianShuYiChuLi/SunHaoLv
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult SunHaoLv()
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
        /// 查询盘点部门
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectGrade(int academeId)
        {                          
            List<SelectVo> listGrade = (from tbGrade in myModels.B_DepatmenList
                                           where tbGrade.SpouseBRanchID == academeId
                                           select new SelectVo
                                           {
                                               id = tbGrade.departmentID,
                                               text = tbGrade.departmentName
                                           }).ToList();
            listGrade = Common.Tools.SetSelectJson(listGrade);
            return Json(listGrade, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 查询商品 信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectStudentA(BsgridPage bsgridPage) //查询信息
        {
            var linq = from tbSpaouc in myModels.S_SpouseBRanchList
                           join tbDetrem in myModels.B_DepatmenList on tbSpaouc.SpouseBRanchID equals tbDetrem.SpouseBRanchID
                           //orderby tbDetrem.departmentID
                           select new LY.WareHouseDeitaLL
                           {
                               departmentID = tbDetrem.departmentID,
                               DfrugTypeID = tbDetrem.DrugTypeID,
                               SpouseBRancfdhID = tbSpaouc.SpouseBRanchID,
                               departmentCodes = tbDetrem.departmentCodes,
                               departmentName = tbDetrem.departmentName,
                               HaoSunLv = tbDetrem.HaoSunLv,
                           };
            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.DfrugTypeID).
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
        /// 查询商品 信息(条件)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectStudentAll(BsgridPage bsgridPage,int academeId, int gradeId) //查询信息
        {
            var linqItem = from tbSpaouc in myModels.S_SpouseBRanchList
                           join tbDetrem in myModels.B_DepatmenList on tbSpaouc.SpouseBRanchID equals tbDetrem.SpouseBRanchID
                           orderby tbDetrem.departmentID
                           select new LY.WareHouseDeitaLL
                           {
                               departmentID = tbDetrem.departmentID,
                               DfrugTypeID = tbDetrem.DrugTypeID,
                               SpouseBRancfdhID = tbSpaouc.SpouseBRanchID,
                               departmentCodes = tbDetrem.departmentCodes,
                               departmentName = tbDetrem.departmentName,
                               HaoSunLv = tbDetrem.HaoSunLv,
                           };
            ////采购Id
            if (academeId > 0)
            {
                linqItem = linqItem.Where(s => s.SpouseBRancfdhID == academeId);
            }

            int intTotalRow = linqItem.Count();
            List<WareHouseDeitaLL> listStudent = linqItem
                .Skip(bsgridPage.GetStartIndex())
                .Take(bsgridPage.pageSize).ToList();

            Bsgrid<WareHouseDeitaLL> bsgrid = new Bsgrid<WareHouseDeitaLL>
            {
                success = true,           //分页
                totalRows = intTotalRow,
                curPage = bsgridPage.curPage,
                data = listStudent
            };

            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// 开始保存（损耗率）
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult UnpticoIncrea(Array JieId, Array JiePanlId,
            Array JieSpatckId, Array JieDaiMa, Array JieMingCheng, Array JieHaoSunLv)
        {

            string strMag = "fali";

            try
            {
                //一
                string Z = ((string[])JieId)[0];
                string[] intsid = Z.Split(',');
                //二
                string M = ((string[])JiePanlId)[0];
                string[] intsPanlId = M.Split(',');
                //三
                string N = ((string[])JieSpatckId)[0];
                string[] intSpatckId = N.Split(',');
                //四
                string C = ((string[])JieDaiMa)[0];
                string[] intsDaiMa = C.Split(',');

                //五
                string Pic = ((string[])JieMingCheng)[0];
                string[] intsName = Pic.Split(',');

                //六
                string kj = ((string[])JieHaoSunLv)[0];
                string[] intsSunYinLv = kj.Split(',');


               

                for (int A = 0; A < intsid.Length;)
                {
                    for (int B = 0; B < intsPanlId.Length;)
                    {
                        for (int D = 0; D < intSpatckId.Length;)
                        {
                            for (int F = 0; F < intsDaiMa.Length;)
                            {
                                for (int H = 0; H < intsName.Length;)
                                {
                                    for (int G = 0; G < intsSunYinLv.Length; G++)
                                    {
                                        B_DepatmenList OK = new B_DepatmenList();

                                        OK.departmentID = Convert.ToInt32(intsid[A]);//id
                                        OK.DrugTypeID = Convert.ToInt32(intsPanlId[B]);//盘点部门
                                        OK.SpouseBRanchID = Convert.ToInt32(intSpatckId[D]);//采购
                                        OK.departmentCodes = intsDaiMa[F];//代码
                                        OK.departmentName = intsName[H];//名称
                                        OK.HaoSunLv = Convert.ToDecimal(intsSunYinLv[G]);//耗损率

                                        myModels.Entry(OK).State = System.Data.Entity.EntityState.Modified;
                                        myModels.SaveChanges();//保存
                                        H++;
                                        F++;
                                        D++;
                                        B++;
                                        A++;
                                        strMag = "succsee";
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






        //第二部分


        /// <summary>
        /// 盘点汇总
        /// </summary>
        /// <returns></returns>
        public ActionResult PanlHuiZong()
        {
            return View();
        }

        /// <summary>
        /// 查询盘点部门代码
        /// </summary>
        //public ActionResult SelectDernrt()
        //{
        //    List<SelectVo> listGrade = (from tbGrade in myModels.B_DepatmenList
        //                                select new SelectVo
        //                                {
        //                                    id = tbGrade.departmentID,
        //                                    text = tbGrade.departmentCodes
        //                                }).ToList();
        //    listGrade = Common.Tools.SetSelectJson(listGrade);
        //    return Json(listGrade, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult SelectDernrt()
        {
            List<SelectVo> listGrade = (from tbChremtn in myModels.B_CheckRemerbenList
                                       join tbGrade in myModels.B_DepatmenList on tbChremtn.DrugTypeID equals tbGrade.DrugTypeID
                                        select new SelectVo
                                        {
                                            id = tbGrade.departmentID,
                                            text = tbGrade.departmentCodes
                                        }).ToList();
            listGrade = Common.Tools.SetSelectJson(listGrade);
            return Json(listGrade, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询盘点部门名称
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectDernrtName(int deparntCodes)
        {
            var linq = (from tbDerred in myModels.B_DepatmenList
                        where tbDerred.departmentID == deparntCodes
                        select new LY.WareHouseDeitaLL
                        {
                            departmentID = tbDerred.departmentID,
                            DrugTypefID = tbDerred.DrugTypeID,
                            departmentName = tbDerred.departmentName,
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 查询盘点部门
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectPanl(int JieId)
        {
            var linq = (from tbDerred in myModels.B_PanlList
                        where tbDerred.DrugTypeID == JieId
                        select new LY.WareHouseDeitaLL
                        {
                            DrugTypeID = tbDerred.DrugTypeID,
                            DrugType = tbDerred.DrugType,
                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 查询盘点商品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectHoaSun(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbCheckRemerben in myModels.B_CheckRemerbenList//计划盘点表
                        join tbPanlBuMen in myModels.B_PanlList on tbCheckRemerben.DrugTypeID equals tbPanlBuMen.DrugTypeID//盘点部门表  
                        join tbHaoSunLv in myModels.B_DepatmenList on tbPanlBuMen.DrugTypeID equals tbHaoSunLv.DrugTypeID//耗损率 (主要表)
                        join tbSpaRkld in myModels.S_SpouseBRanchList on tbHaoSunLv.SpouseBRanchID equals tbSpaRkld.SpouseBRanchID//采购表

                        join tbtbCheckReDetill in myModels.B_CheckRemerbenDetillList on tbCheckRemerben.CheckRermeberID equals tbtbCheckReDetill.CheckRermeberID//计划盘点明细表
                        join tbGoods in myModels.B_GoodsList on tbtbCheckReDetill.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细

                        select new LY.WareHouseDeitaLL
                        {
                            departmentID = tbHaoSunLv.departmentID,//耗损表ID
                            departmentCodes = tbHaoSunLv.departmentCodes,//耗损部门代码
                            departmentName = tbHaoSunLv.departmentName,//耗损部门名称
                            SpouseBRanchID = tbSpaRkld.SpouseBRanchID,//采购部门
                            DrugTypefID = tbHaoSunLv.DrugTypeID,//盘点部门ID

                            DrugType = tbCheckRemerben.DrugType,
                            GoodsNames = tbGoods.GoodsName,

                            HaoSunLv = tbHaoSunLv.HaoSunLv,//耗损率
                            //FactPrice = tbGoodDetail.FactPrice,//商品单价（销售价）
                            //试一下
                            //HeLiHaoSunLv = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice,//合理耗损
                            //ChaE = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice - tbHaoSunLv.HaoSunLv,//差额
                            //ShiJiHaoSunLv= tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice - tbHaoSunLv.HaoSunLv / tbGoodDetail.FactPrice,

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).
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
        /// 查询盘点商品类型(条 件)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectHoaSunTiao(Vo.BsgridPage bsgridPage,int drugType)
        {
            var linq = (from tbCheckRemerben in myModels.B_CheckRemerbenList//计划盘点表
                        join tbHaoSunLv in myModels.B_DepatmenList on tbCheckRemerben.DrugTypeID equals tbHaoSunLv.DrugTypeID//耗损率 (主要表)
                        join tbSpaRkld in myModels.S_SpouseBRanchList on tbHaoSunLv.SpouseBRanchID equals tbSpaRkld.SpouseBRanchID//采购表

                        join tbtbCheckReDetill in myModels.B_CheckRemerbenDetillList on tbCheckRemerben.CheckRermeberID equals tbtbCheckReDetill.CheckRermeberID//计划盘点明细表
                        join tbGoods in myModels.B_GoodsList on tbtbCheckReDetill.GoodsID equals tbGoods.GoodsID//商品
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        where tbCheckRemerben.DrugTypeID== drugType
                        select new LY.WareHouseDeitaLL
                        {
                            departmentID = tbHaoSunLv.departmentID,//耗损表ID
                            departmentCodes = tbHaoSunLv.departmentCodes,//耗损部门代码
                            departmentName = tbHaoSunLv.departmentName,//耗损部门名称
                            SpouseBRanchID = tbSpaRkld.SpouseBRanchID,//采购部门
                            DrugTypefID = tbHaoSunLv.DrugTypeID,//盘点部门ID

                            DrugType = tbCheckRemerben.DrugType,
                            GoodsNames = tbGoods.GoodsName,

                            HaoSunLv = tbHaoSunLv.HaoSunLv,//耗损率
                            //FactPrice = tbGoodDetail.FactPrice,//商品单价（销售价）
                            //试一下
                            //HeLiHaoSunLv = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice,//合理耗损
                            //ChaE = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice - tbHaoSunLv.HaoSunLv,//差额
                            //ShiJiHaoSunLv = tbHaoSunLv.HaoSunLv * tbGoodDetail.FactPrice - tbHaoSunLv.HaoSunLv / tbGoodDetail.FactPrice,

                        }).ToList();


            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).
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



        //第三部分
        /// <summary>
        /// 商品盘点属性设置（视图）
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsPanlSex()
        {
            return View();
        }


        /// <summary>
        /// 查询计划盘点有的部门
        /// </summary>
        /// <param name="academeId"></param>
        /// <returns></returns>
        public ActionResult SelectDernrtBuMen()
        {
            List<SelectVo> listGrade = (from tbPanl in myModels.B_CheckRemerbenList
                                          join tbGraded in myModels.B_PanlList on tbPanl.DrugTypeID equals tbGraded.DrugTypeID
                                          join tbGrade in myModels.B_DepatmenList on tbGraded.DrugTypeID equals tbGrade.DrugTypeID
                                           select new SelectVo
                                           {
                                               id = tbGrade.departmentID,
                                               text = tbGrade.departmentCodes
                                           }).ToList();
            listGrade = Common.Tools.SetSelectJson(listGrade);
            return Json(listGrade, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 查询盘点 商品(部门类型)
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectHoaSunChan(Vo.BsgridPage bsgridPage, int drugTypeID)
        {
            var linq = (from tbCheckRemerben in myModels.B_CheckRemerbenList//计划盘点表
                        join tbPanlBuMen in myModels.B_PanlList on tbCheckRemerben.DrugTypeID equals tbPanlBuMen.DrugTypeID//盘点部门表  
                        join tbtbCheckReDetill in myModels.B_CheckRemerbenDetillList on tbCheckRemerben.CheckRermeberID equals tbtbCheckReDetill.CheckRermeberID//计划盘点明细表

                        join tbGoods in myModels.B_GoodsList on tbtbCheckReDetill.GoodsID equals tbGoods.GoodsID//商品
                        join tbFenLei in myModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbFenLei.GoodsClassifyID//商品分类
                        join tbShangBiao in myModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                        join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                        join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                        where tbCheckRemerben.DrugTypeID == drugTypeID
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbCheckRemerben.CheckRermeberID,
                            DrugTypefID = tbCheckRemerben.DrugTypeID,
                            DrugType = tbCheckRemerben.DrugType,
                            GoodsIDs = tbtbCheckReDetill.GoodsID,
                            GoodsNames = tbGoods.GoodsName,//商品名称
                            GoodsCodes = tbGoods.GoodsCode,//代码
                            GoodsTiaoMas = tbGoods.GoodsTiaoMa,//
                            EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                            ArtNos = tbGoods.ArtNo,//商品货号
                            GoodsClassifyName = tbFenLei.GoodsClassifyName,//商品分类
                            GoodsChopName = tbShangBiao.GoodsChopName//商标
                        }).ToList();

            int totalRow = linq.Count();
            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).
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
        ///绑定商品()
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingPiGoodel(Vo.BsgridPage bsgridPage, Array jiLuChaekID)
        {
            List<WareHouseDeitaLL> list = new List<WareHouseDeitaLL>();
            string Z = ((string[])jiLuChaekID)[0];
            string[] intsZ = Z.Split(',');

            for (int P = 0; P < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[P]);
                var linq = (from tbGoods in myModels.B_GoodsList //商品
                            join tbFenLei in myModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbFenLei.GoodsClassifyID//商品分类
                            join tbShangBiao in myModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbShangBiao.GoodsChopID//商标
                            join tbJiLiangDanWei in myModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbJiLiangDanWei.EstimateUnitID//计量单位
                            join tbGoodDetail in myModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodDetail.GoodsID//商品明细
                            where tbGoods.GoodsID == goodsIDs
                            select new LY.WareHouseDeitaLL
                            {

                                GoodsIDs = tbGoods.GoodsID,
                                GoodsNames = tbGoods.GoodsName,//商品名称
                                GoodsCodes = tbGoods.GoodsCode,//代码
                                GoodsTiaoMas = tbGoods.GoodsTiaoMa,//
                                EstimateUnitNames = tbJiLiangDanWei.EstimateUnitName,//计量单位
                                ArtNos = tbGoods.ArtNo,//商品货号
                                GoodsClassifyName = tbFenLei.GoodsClassifyName,//商品分类
                                GoodsChopName = tbShangBiao.GoodsChopName//商标
                            }).ToList();
                list.AddRange(linq);
                P++;
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


    }
}
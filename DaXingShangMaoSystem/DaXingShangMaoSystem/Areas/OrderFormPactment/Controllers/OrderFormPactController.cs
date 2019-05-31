 using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.OrderFormPactment.Controllers
{
    public class OrderFormPactController : Controller
    {
        // GET: OrderFormPactment/OrderFormPact
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        /// <summary>
        /// 经销合同处理
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 代销合同处理
        /// </summary>
        /// <returns></returns>
        public ActionResult DaiXiaoHeTong()
        {
            return View();
        }
        /// <summary>
        /// 联营合同处理
        /// </summary>
        /// <returns></returns>
        public ActionResult LianYingHeTong()
        {
            return View();
        }
        /// <summary>
        /// 租赁合同处理,合同提前结算日设定
        /// </summary>
        /// <returns></returns>
        public ActionResult ZuLinHeTongChuLI()
        {
            return View();
        }



        /// <summary>
        /// 经销合同变更处理
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongChuLI()
        {
            return View();
        }
        /// <summary>
        /// 代销合同变更处理
        /// </summary>
        /// <returns></returns>
        public ActionResult DaiXiaoXiaoHeTongChuLI()
        {
            return View();
        }
        /// <summary>
        /// 联营合同变更处理
        /// </summary>
        /// <returns></returns>
        public ActionResult LianYingHeTongChuLI()
        {
            return View();
        }
        /// <summary>
        /// 租赁合同变更处理
        /// </summary>
        /// <returns></returns>
        public ActionResult ZuLinHeTongBianGengChuLI()
        {
            return View();
        }



        /// <summary>
        /// 经销续签处理
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoXuQianHeTongChuLI()
        {
            return View();
        }
        /// <summary>
        /// 代销续签处理
        /// </summary>
        /// <returns></returns>
        public ActionResult DaiXiaoXiaoXuQianHeTongChuLI()
        {
            return View();
        }
        /// <summary>
        /// 联营续签处理
        /// </summary>
        /// <returns></returns>
        public ActionResult LianYingXuQianHeTongChuLI()
        {
            return View();
        }
        /// <summary>
        /// 租赁续签处理
        /// </summary>
        /// <returns></returns>
        public ActionResult ZuLinXuQianHeTongChuLI()
        {
            return View();
        }



        /// <summary>
        /// 合同清退
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongChuTuiChuLI()
        {
            return View();
        }
      
        /// <summary>
        /// 合同清退代销
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongChuTuiChuLI1()
        {
            return View();
        }
        /// <summary>
        /// 合同清退联营
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongChuTuiChuLI2()
        {
            return View();
        }
        /// <summary>
        /// 合同清退租赁
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongChuTuiChuLI3()
        {
            return View();
        }



        /// <summary>
        /// 合同终止
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongZhongZhiChuTuiChuLI()
        {
            return View();
        }
        /// <summary>
        /// 合同终止
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongZhongZhiChuTuiChuLI1()
        {
            return View();
        }
        /// <summary>
        /// 合同终止
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongZhongZhiChuTuiChuLI2()
        {
            return View();
        }
        /// <summary>
        /// 合同终止
        /// </summary>
        /// <returns></returns>
        public ActionResult JingXiaoHeTongZhongZhiChuTuiChuLI3()
        {
            return View();
        }


        /// <summary>
        /// 合同结算扣款项定义
        /// </summary>
        public ActionResult HeTongJieSuanKouKuan()
        {
            return View();
        }


        /// <summary>
        /// 定义固定扣款项目对照表
        /// </summary>
        /// <returns></returns>
        public ActionResult DingYiHeTongKouKuanXianMu()
        {
            return View();
        }

        /// <summary>
        /// 固定扣款项目对照表
        /// </summary>
        /// <returns></returns>
        public ActionResult GuDingKouKuanXiangMuDuiZhaoBiao()
        {
            return View();
        }
        /// <summary>
        /// 定义进场费用项目 
        /// </summary>
        /// <returns></returns>
        public ActionResult DingYiJinChangFeiYongXianMu()
        {
            return View();
        }


        /// <summary>
        /// 合同进场处理
        /// </summary>
        /// <returns></returns>
        public ActionResult HongTongJinChangChuLi()
        {
            return View();
        }
        /// <summary>
        /// 合同退场处理
        /// </summary>
        /// <returns></returns>
        public ActionResult HongTongTuiChangChuLi()
        {
            return View();
        }




        /// <summary>
        /// 绑定供应商代码
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCode()
        {
            string Code = "";
            var listEmp = (from tbEmp in MyModels.B_OrderFormPactList
                           orderby tbEmp.ContractNumber
                           select tbEmp).ToList();
            if (listEmp.Count > 0)
            {
                int intCount = listEmp.Count;
                B_OrderFormPactList modelEmp = listEmp[intCount - 1];
                int intEmp = Convert.ToInt32(modelEmp.ContractNumber.Substring(1, 5));
                intEmp++;
                Code = intEmp.ToString();
                for (int i = 0; i < 5; i++)
                {
                    Code = Code.Length < 5 ? "0" + Code : Code;
                }
                Code = "" + Code;
            }

            else
            {
                Code = "12100";
            }
            return Json(Code, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 绑定合同代码
        /// </summary>
        /// <returns></returns>
        public ActionResult HeTongDaiXiao()
        {
            string strCurrentCode = "";            
            var listDep = (from tbDmp in MyModels.B_OrderFormPactList
                           orderby tbDmp.ContractNumber
                           select tbDmp).ToList();
            if (listDep.Count > 0)//判断listDep中是否有数据
            {   
                int count = listDep.Count;               
                B_OrderFormPactList modelDep = listDep[count - 1];                             
                int intCode = Convert.ToInt32(modelDep.ContractNumber.Substring(1, 4));//Substring(1,4)截取字符串,1代表字符串起始位置，4表示截取长                
                intCode++;               
                strCurrentCode = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {   
                    strCurrentCode = strCurrentCode.Length < 4 ? "0" + strCurrentCode : strCurrentCode;//三目运算符
                }
                strCurrentCode = "1" + strCurrentCode;
            }
            else
            {
                strCurrentCode = "15101";
            }
            return Json(strCurrentCode, JsonRequestBehavior.AllowGet);//返回Json格式的数据
        }
        /// <summary>
        /// 绑定合同代码
        /// </summary>
        /// <returns></returns>
        public ActionResult HeTongJingXiao()
        {
            string strCurrentCode = "";
            var listDep = (from tbDmp in MyModels.B_OrderFormPactList
                           orderby tbDmp.ContractNumber
                           select tbDmp).ToList();
            if (listDep.Count > 0)//判断listDep中是否有数据
            {
                int count = listDep.Count;
                B_OrderFormPactList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.ContractNumber.Substring(1, 4));//Substring(1,4)截取字符串,1代表字符串起始位置，4表示截取长                
                intCode++;
                strCurrentCode = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {
                    strCurrentCode = strCurrentCode.Length < 4 ? "0" + strCurrentCode : strCurrentCode;//三目运算符
                }
                strCurrentCode = "1" + strCurrentCode;
            }
            else
            {
                strCurrentCode = "12101";
            }
            return Json(strCurrentCode, JsonRequestBehavior.AllowGet);//返回Json格式的数据
        }
        /// <summary>
        /// 绑定合同代码
        /// </summary>
        /// <returns></returns>
        public ActionResult HeTongLianYing()
        {
            string strCurrentCode = "";
            var listDep = (from tbDmp in MyModels.B_OrderFormPactList
                           orderby tbDmp.ContractNumber
                           select tbDmp).ToList();
            if (listDep.Count > 0)//判断listDep中是否有数据
            {
                int count = listDep.Count;
                B_OrderFormPactList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.ContractNumber.Substring(1, 4));//Substring(1,4)截取字符串,1代表字符串起始位置，4表示截取长                
                intCode++;
                strCurrentCode = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {
                    strCurrentCode = strCurrentCode.Length < 4 ? "0" + strCurrentCode : strCurrentCode;//三目运算符
                }
                strCurrentCode = "1" + strCurrentCode;
            }
            else
            {
                strCurrentCode = "12101";
            }
            return Json(strCurrentCode, JsonRequestBehavior.AllowGet);//返回Json格式的数据
        }
        /// <summary>
        /// 绑定合同代码
        /// </summary>
        /// <returns></returns>
        public ActionResult HeTongZuLin()
        {
            string strCurrentCode = "";
            var listDep = (from tbDmp in MyModels.B_OrderFormPactList
                           orderby tbDmp.ContractNumber
                           select tbDmp).ToList();
            if (listDep.Count > 0)//判断listDep中是否有数据
            {
                int count = listDep.Count;
                B_OrderFormPactList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.ContractNumber.Substring(1, 4));//Substring(1,4)截取字符串,1代表字符串起始位置，4表示截取长                
                intCode++;
                strCurrentCode = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {
                    strCurrentCode = strCurrentCode.Length < 4 ? "0" + strCurrentCode : strCurrentCode;//三目运算符
                }
                strCurrentCode = "1" + strCurrentCode;
            }
            else
            {
                strCurrentCode = "12101";
            }
            return Json(strCurrentCode, JsonRequestBehavior.AllowGet);//返回Json格式的数据
        }
        /// <summary>
        /// 品牌table绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult selectGoodsBrandList(Vo.BsgridPage bsgridPage)
        {
            var linq = from tbGoodsBrandList in MyModels.S_GoodsBrandList
                       select new LY.SelectOrderPact
                       {
                           GoodsBrandID = tbGoodsBrandList.GoodsBrandID,
                           GoodsBrandName = tbGoodsBrandList.GoodsBrandName,
                           GoodsBrandZhuPinPai = tbGoodsBrandList.GoodsBrandZhuPinPai
                       };
            int totalRow = linq.Count();

            List<LY.SelectOrderPact> notices = linq.OrderByDescending(p => p.GoodsBrandID).//noboer表达式
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
        /// <summary>
        /// 删除品牌信息
        /// </summary>
        /// <param name="GoodsBrandId"></param>
        /// <returns></returns>
        public ActionResult DelateEmployee(int GoodsBrandId)
        {
            if (GoodsBrandId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_GoodsBrandList.Where(m => m.GoodsBrandID == GoodsBrandId).Single();
                    MyModels.S_GoodsBrandList.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);//
                }
            }
            else
            {
                return Json("fail");
            }
        }
        /// <summary>
        /// 品牌table绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult selectGoodsBrandList1()
        {
            var listGoodsBrandList = from tbGoodsBrandList in MyModels.S_TrademarkRankList
                                     select new
                                     {
                                         tbGoodsBrandList.TrademarkRankID,
                                         tbGoodsBrandList.TrademarkRankName,
                                         
                                     };
            return Json(listGoodsBrandList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询商品分类
        /// </summary>
        /// <param name="GoodsBrandName"></param>
        /// <param name="GoodsBrandZhuPinPai"></param>
        /// <returns></returns>
        public ActionResult selectSupplierGroup(string GoodsBrandName, string GoodsBrandZhuPinPai)
        {
            string mag = "falied";
            try
            {
                Models.S_GoodsBrandList myModel = new S_GoodsBrandList();
                myModel.GoodsBrandName = GoodsBrandName.Trim();
                myModel.GoodsBrandZhuPinPai = GoodsBrandZhuPinPai.Trim();
                MyModels.S_GoodsBrandList.Add(myModel);
                MyModels.SaveChanges();
                mag = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(mag, JsonRequestBehavior.AllowGet);
        }
      
      
        /// <summary>
        /// 分类table绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult selectGoodsGenre(Vo.BsgridPage bsgridPage)
        {
            var linq = from tbGoodsGenret in MyModels.S_GoodsGenreIDList
                       select new LY.SelectOrderPact
                       {
                           GoodsGenreID = tbGoodsGenret.GoodsGenreID,
                           GoodsGenreName = tbGoodsGenret.GoodsGenreName

                       };
            int totalRow = linq.Count();

            List<LY.SelectOrderPact> notices = linq.OrderByDescending(p => p.GoodsGenreID).//noboer表达式
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
        /// <summary>
        /// 经销合同查询员工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult selectOrderFormPactList(BsgridPage bsgridPage)
        {
            var listStaff = from tbStaff in MyModels.B_OrderFormPactList
                            orderby tbStaff.OrderFormPactID
                            select new Vo.OrderFormPactList
                            {
                                OrderFormPactID = tbStaff.OrderFormPactID,
                                PurchasingAgent = tbStaff.PurchasingAgent
                            };
            int intTotalRow = listStaff.Count();//总行数
            List<OrderFormPactList> listNotices = listStaff.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同获取采购员
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangBiaoMingCheng(int OrderFormPactId)
        {
            if (OrderFormPactId > 0)
            {
                var GoodsChop = (from tbEmp in MyModels.B_OrderFormPactList
                                 where tbEmp.OrderFormPactID == OrderFormPactId
                                 select new Vo.OrderFormPactList
                                 {
                                     OrderFormPactID = tbEmp.OrderFormPactID,
                                     PurchasingAgent = tbEmp.PurchasingAgent
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 经销合同查询员工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult selectOrderFormPactList1(BsgridPage bsgridPage)
        {
            var listStaff = from tbStaff in MyModels.B_SupplierList
                            join tbAgreementType in MyModels.S_AgreementTypeList on tbStaff.AgreementTypeID equals tbAgreementType.AgreementTypeID
                            orderby tbStaff.SupplierID
                            select new Vo.OrderFormPactList
                            {
                                SupplierID = tbStaff.SupplierID,
                                AgreementTypeID = tbStaff.AgreementTypeID,
                                AgreementTypeName = tbAgreementType.AgreementTypeName
                            };
            int intTotalRow = listStaff.Count();//总行数
            List<OrderFormPactList> listNotices = listStaff.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同获取采购员
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangBiaoMingCheng1(int SupplierId)
        {
            if (SupplierId > 0)
            {
                var GoodsChop = (from tbEmp in MyModels.B_SupplierList
                                 join tbAgreementType in MyModels.S_AgreementTypeList on tbEmp.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                 where tbEmp.SupplierID == SupplierId
                                 select new Vo.OrderFormPactList
                                 {
                                     SupplierID = tbEmp.SupplierID,
                                     AgreementTypeName = tbAgreementType.AgreementTypeName
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 经销合同查询员工信息
        /// </summary>
        /// <returns></returns>
        public ActionResult selectOrderFormPactList2(BsgridPage bsgridPage)
        {
            var listStaff = from tbStaff in MyModels.S_SpouseBRanchList
                            orderby tbStaff.SpouseBRanchID
                            select new Vo.OrderFormPactList
                            {
                                SpouseBRanchID = tbStaff.SpouseBRanchID,
                                SpouseBRanchName = tbStaff.SpouseBRanchName
                            };
            int intTotalRow = listStaff.Count();//总行数
            List<OrderFormPactList> listNotices = listStaff.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同获取采购员
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangBiaoMingCheng2(int SpouseBRanchId)
        {
            if (SpouseBRanchId > 0)
            {
                var GoodsChop = (from tbEmp in MyModels.S_SpouseBRanchList
                                 where tbEmp.SpouseBRanchID == SpouseBRanchId
                                 select new Vo.OrderFormPactList
                                 {
                                     SpouseBRanchID = tbEmp.SpouseBRanchID,
                                     SpouseBRanchName = tbEmp.SpouseBRanchName
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 经销合同查询经理信息
        /// </summary>
        /// <returns></returns>
        public ActionResult selectOrderFormPactList3(BsgridPage bsgridPage)
        {
            var listStaff = from tbStaff in MyModels.S_AgreementManagerList
                            orderby tbStaff.AgreementManagerID
                            select new Vo.OrderFormPactList
                            {
                                AgreementManagerID = tbStaff.AgreementManagerID,
                                AgreementManagerGaoJi = tbStaff.AgreementManagerGaoJi,
                                AgreementManagerName = tbStaff.AgreementManagerName,
                                AgreementManagerZong = tbStaff.AgreementManagerZong
                            };
            int intTotalRow = listStaff.Count();//总行数
            List<OrderFormPactList> listNotices = listStaff.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同获取采购员
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangBiaoMingCheng3(int AgreementManagerId)
        {
            if (AgreementManagerId > 0)
            {
                var GoodsChop = (from tbEmp in MyModels.S_AgreementManagerList
                                 where tbEmp.AgreementManagerID == AgreementManagerId
                                 select new Vo.OrderFormPactList
                                 {
                                     AgreementManagerID = tbEmp.AgreementManagerID,
                                     AgreementManagerGaoJi = tbEmp.AgreementManagerGaoJi,
                                     AgreementManagerName = tbEmp.AgreementManagerName,
                                     AgreementManagerZong = tbEmp.AgreementManagerZong
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 查询经销合同
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectXiuGaiShenHeFouQuanBu(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                    
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,
                                         RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                         RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                         AgreementRen = tbOrderFormPact.AgreementRen



                                     }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 查询经销合同
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectXiuGaiShenHeFou(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                     where tbOrderFormPact.AgreementTypeID == 1
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,
                                         RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                         RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                         AgreementRen=tbOrderFormPact.AgreementRen



                                     }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改状态 
        /// </summary>
        /// <param name="titleId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ActionResult UpdateTitleState(int titleId, bool state)
        {
            ReturnJsonVo returnJson = new ReturnJsonVo();
            try
            {
                B_OrderFormPactList title = (from tbTitle in MyModels.B_OrderFormPactList
                                             where tbTitle.OrderFormPactID == titleId
                                  select tbTitle).Single();
                title.UseOf = state;
                MyModels.Entry(title).State = EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
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
                returnJson.State = false;
                returnJson.Text = "数据异常";
            }
            return Json(returnJson, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 代销
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectXiuGaiShenHeFou1(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                     where tbOrderFormPact.AgreementTypeID == 2
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,

                                         RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                         RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                         AgreementRen = tbOrderFormPact.AgreementRen


                                     }
                                  );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 联营
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectXiuGaiShenHeFou3(BsgridPage bsgridPage)
        {

            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                   
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                     where tbOrderFormPact.AgreementTypeID == 3
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,
                                         RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                         RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                         AgreementRen = tbOrderFormPact.AgreementRen


                                    }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

           
        }
        /// <summary>
        /// 经销绑定text
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult selectID1(int id)
        {
            if (id > 0)
            {
                var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                         join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                         join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                         join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                         ////join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                         join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                         join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                         join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                         join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID



                                         where tbOrderFormPact.OrderFormPactID == id

                                         select new OrderFormPactList
                                         {

                                             OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                             SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                             HostContractNumber = tbOrderFormPact.HostContractNumber,
                                             ContractNumber = tbOrderFormPact.ContractNumber,
                                             SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                             SupplierID = tbOrderFormPact.SupplierID,
                                             AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                             AgreementTypeName = tbAgreementType.AgreementTypeName,
                                             ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                             ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                             ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                             ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                             MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                             MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                             ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                             ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                             AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                             AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                             AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                             AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                             AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                             AgreementState = tbAgreementStateList.AgreementState,
                                             PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                             ConclusionSite = tbOrderFormPact.ConclusionSite,
                                             ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                             TryPaymentData = tbOrderFormPact.TryPaymentData,
                                             TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                             GoodsID = tbOrderFormPact.GoodsID,
                                             Registrant = tbOrderFormPact.Registrant,
                                             Auditor = tbOrderFormPact.Auditor,
                                             Executor = tbOrderFormPact.Executor,
                                             Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                             RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                             RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                             UseOf = tbOrderFormPact.UseOf,

                                             RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                             RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                             AgreementRen = tbOrderFormPact.AgreementRen

                                         }).Single();
                return Json(listOrderFormPact, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 经销绑定text
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult selectID2(int id)
        {
            if (id > 0)
            {
                var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                         join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                         join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                         join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                         ////join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                         join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                         join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                         join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                         join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID



                                         where tbOrderFormPact.OrderFormPactID == id

                                         select new OrderFormPactList
                                         {

                                             OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                             SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                             HostContractNumber = tbOrderFormPact.HostContractNumber,
                                             ContractNumber = tbOrderFormPact.ContractNumber,
                                             SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                             SupplierID = tbOrderFormPact.SupplierID,
                                             AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                             AgreementTypeName = tbAgreementType.AgreementTypeName,
                                             ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                             ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                             ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                             ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                             MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                             MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                             ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                             ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                             AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                             AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                             AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                             AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                             AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                             AgreementState = tbAgreementStateList.AgreementState,
                                             PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                             ConclusionSite = tbOrderFormPact.ConclusionSite,
                                             ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                             TryPaymentData = tbOrderFormPact.TryPaymentData,
                                             TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                             GoodsID = tbOrderFormPact.GoodsID,
                                             Registrant = tbOrderFormPact.Registrant,
                                             Auditor = tbOrderFormPact.Auditor,
                                             Executor = tbOrderFormPact.Executor,
                                             Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                             RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                             RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                             UseOf = tbOrderFormPact.UseOf,

                                             RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                             RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                             AgreementRen = tbOrderFormPact.AgreementRen

                                         }).Single();
                return Json(listOrderFormPact, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 租赁
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectXiuGaiShenHeFou4(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                     where tbOrderFormPact.AgreementTypeID == 4
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,

                                           RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                         RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                         AgreementRen = tbOrderFormPact.AgreementRen


                                     }
                                    );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销绑定text
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult selectID(int id)
        {
            if (id > 0)
            {
                var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                         join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                         join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                         join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                         //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                         join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                         join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                         join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                         join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                         


                                         where tbOrderFormPact.OrderFormPactID == id

                                         select new OrderFormPactList
                                         {

                                             OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                             SpouseBRanchID=tbOrderFormPact.SpouseBRanchID,
                                             HostContractNumber = tbOrderFormPact.HostContractNumber,
                                             ContractNumber = tbOrderFormPact.ContractNumber,
                                             SpouseBRanchName =tbSpouseBRanchList.SpouseBRanchName,
                                             SupplierID=tbOrderFormPact.SupplierID,
                                             AgreementTypeID=tbOrderFormPact.AgreementTypeID,
                                             AgreementTypeName=tbAgreementType.AgreementTypeName,
                                             ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                             ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                             ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                             ReleaseTimetuichangchangtime=tbOrderFormPact.RetreatMarketData.ToString(),
                                             MethodOfSettlingAccountsID=tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                             MethodOfSettlingAccounts=tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                             ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                             ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                             AgreementManagerID=tbOrderFormPact.AgreementManagerID,
                                             AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                             AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                             AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                             AgreementStatusID=tbOrderFormPact.AgreementStatusID,
                                             AgreementState = tbAgreementStateList.AgreementState,
                                             PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                             ConclusionSite = tbOrderFormPact.ConclusionSite,
                                             ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                             TryPaymentData=tbOrderFormPact.TryPaymentData,
                                             TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                             GoodsID = tbOrderFormPact.GoodsID,
                                             Registrant = tbOrderFormPact.Registrant,
                                             Auditor = tbOrderFormPact.Auditor,
                                             Executor = tbOrderFormPact.Executor,
                                             Registranttimea=tbOrderFormPact.Registranttime.ToString(),
                                             RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                             RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                             UseOf = tbOrderFormPact.UseOf,
                                             RegistrantPlanFiringData = tbOrderFormPact.PlanFiringData.ToString(),
                                             RegistrantFiringdata = tbOrderFormPact.Firingdata.ToString(),
                                             AgreementRen = tbOrderFormPact.AgreementRen

                                         }).Single();
                return Json(listOrderFormPact, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 代销合同商标新增
        /// </summary>
        /// <param name="sysNoticeType"></param>
        /// <returns></returns>
        public ActionResult InsertNoticeType(S_TrademarkRankList sysNoticeType)
        {
            string strMsg = "falied";
            if (!string.IsNullOrEmpty(sysNoticeType.TrademarkRankName))
            {
                int oldCount = (from tbNoticeType in MyModels.S_TrademarkRankList
                                where tbNoticeType.TrademarkRankName == sysNoticeType.TrademarkRankName.Trim()
                                select tbNoticeType).Count();
                if (oldCount == 0)
                {
                    try
                    {
                        MyModels.S_TrademarkRankList.Add(sysNoticeType);
                        if (MyModels.SaveChanges() > 0)
                        {
                            strMsg = "success";
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
                else
                {
                    strMsg = "exist";//已经存在
                }
            }
            else
            {
                strMsg = "nofull";//数据不完整
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 代销合同商标删除
        /// </summary>
        /// <param name="noticeTypeId"></param>
        /// <returns></returns>
        public ActionResult DeleteNoticeType(int noticeTypeId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                S_TrademarkRankList dbNoticeType = (from tbNoticeTypeDetails in MyModels.S_TrademarkRankList
                                                         where tbNoticeTypeDetails.TrademarkRankID == noticeTypeId
                                                         select tbNoticeTypeDetails).Single();
                MyModels.S_TrademarkRankList.Remove(dbNoticeType);//删除
                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception)
            {

            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
       /// <summary>
       /// 代销合同删除
       /// </summary>
       /// <param name="GoodsBrandId"></param>
       /// <returns></returns>
        public ActionResult DelateEmployee1(int GoodsBrandId)
        {
            if (GoodsBrandId > 0)
            {
                try
                {

                    //var deleteEmp = (from tbEmp in mymodal.Employee
                    //                 where tbEmp.EmployeeID == EmpID
                    //                 select tbEmp).Single();
                    ////1.第一种删除写法
                    ////mymodal.Entry(deleteEmp).State = System.Data.Entity.EntityState.Deleted;
                    ////2.第二种删除写法
                    //mymodal.Employee.Remove(deleteEmp);
                    //mymodal.SaveChanges();
                    //3.Lamda表达式 用lamda表达式书写上面的两种方法
                    var deleteEmp = MyModels.S_TrademarkRankList.Where(m => m.TrademarkRankID == GoodsBrandId).Single();
                    //3.1第一种的Lamda表达式写法
                    //mymodal.Entry(deleteEmp).State = System.Data.Entity.EntityState.Deleted;
                    //3.2第二种的Lamda表达式写法
                    MyModels.S_TrademarkRankList.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);//
                }
            }
            else
            {
                return Json("fail");//
            }
        }
        /// <summary>
        /// 定义合同扣款项目
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult selectAgreementWithhold(BsgridPage bsgridPage)
        {
            var listAgreementWithhold = (from tbAgreementWithhold in MyModels.B_AgreementWithholdList
                                       orderby tbAgreementWithhold.AgreementWithholdID
                                         select new AgreementWithholdVo
                                         {
                                             AgreementWithholdID = tbAgreementWithhold.AgreementWithholdID,
                                             AgreementWithholdName = tbAgreementWithhold.AgreementWithholdName,
                                             MethodOfSettling = tbAgreementWithhold.MethodOfSettling,
                                             UseNo = tbAgreementWithhold.UseNo,
                                            
                                             SettlementDebi = tbAgreementWithhold.SettlementDebiList,

                                             
                                         });
            int intTotalRow = listAgreementWithhold.Count();//总行数
            List<AgreementWithholdVo> listNotices = listAgreementWithhold.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<AgreementWithholdVo> bsgrid = new Bsgrid<AgreementWithholdVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectSettlementDebi()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_SettlementDebiList
                                   select new
                                   {
                                       id = tbSupplierRank.SettlementDebiID,
                                       text = tbSupplierRank.SettlementDebi
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectFangXingXinXi(int SettlementDebiID)
        {
            try
            {
                var lisSupplierRank = (from tbSupplierRank in MyModels.S_SettlementDebiList
                                       where tbSupplierRank.SettlementDebiID == SettlementDebiID
                                       select new
                                       {
                                           SettlementDebi = tbSupplierRank.SettlementDebi.Trim()
                                       }).Single();
                return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 新增定义合同扣款项目 
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertRoom()
        {
            string Tip = "";
           
            string AgreementWithholdName = Request.Form["AgreementWithholdName"];
            string MethodOfSettling = Request.Form["MethodOfSettling"];
            string UseNo = Request.Form["UseNo"];
            string SettlementDebiID = Request.Form["SettlementDebiID"];
            string SettlementDebiList = Request.Form["SettlementDebiList"];


            if  (AgreementWithholdName != "" && MethodOfSettling != "" && UseNo != "" && SettlementDebiID != "" && SettlementDebiList != "")
            {
                try
                {
                    var dbRoomDetail = (from tbRoomDetail in MyModels.B_AgreementWithholdList
                                      
                                        select tbRoomDetail).ToList();
                    if (dbRoomDetail.Count() != 0)
                    {
                        B_AgreementWithholdList RoomDetail = new B_AgreementWithholdList();
                       
                       
                        RoomDetail.AgreementWithholdName = AgreementWithholdName.Trim();
                        RoomDetail.MethodOfSettling = MethodOfSettling.Trim();
                        RoomDetail.SettlementDebiID = Convert.ToInt32(SettlementDebiID);
                        RoomDetail.UseNo = UseNo.Trim();
                        RoomDetail.SettlementDebiList = SettlementDebiList.Trim();




                        MyModels.B_AgreementWithholdList.Add(RoomDetail);
                        MyModels.SaveChanges();
                        Tip = "success";
                    }
                    else
                    {
                        Tip = "已经有此房间了";
                    }
                }
                catch (Exception)
                {

                    Tip = "出错啦";
                }
            }
            else
            {
                Tip = "请填写完整数据！！！！！";
            }

            return Json(Tip, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteGoods(int goodsId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_AgreementWithholdList dbGoodsDetail = (from tbGoodsDetail in MyModels.B_AgreementWithholdList
                                             where tbGoodsDetail.AgreementWithholdID == goodsId
                                                         select tbGoodsDetail).Single();

                MyModels.B_AgreementWithholdList.Remove(dbGoodsDetail);

                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception )
            {
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 代销合同处理
        /// </summary>
        /// <returns></returns>   
        public ActionResult SelectXiuGaiShenHeFoua(BsgridPage bsgridPage)
        {

            var linqItem = (from tbOrderFormPactList in MyModels.B_OrderFormPactList
                           join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPactList.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                           join tbAgreementTypeList in MyModels.S_AgreementTypeList on tbOrderFormPactList.AgreementTypeID equals tbAgreementTypeList.AgreementTypeID
                           join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPactList.SupplierID equals tbSupplierList.SupplierID
                           orderby tbOrderFormPactList.OrderFormPactID                        
                           select new OrderFormPactList
                           {
                               OrderFormPactID = tbOrderFormPactList.OrderFormPactID,
                               SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                               ContractNumber = tbOrderFormPactList.ContractNumber,
                               SpouseBRanchID = tbOrderFormPactList.SpouseBRanchID,
                               ValidBegin = tbOrderFormPactList.ValidBegin,
                               ValidTip = tbOrderFormPactList.ValidTip,
                               ReleaseTimeStr = tbOrderFormPactList.ValidBegin.ToString(),
                               ReleaseTimeStrr = tbOrderFormPactList.ValidTip.ToString(),
                               SupplierID = tbOrderFormPactList.SupplierID,
                               AgreementTypeID = tbOrderFormPactList.AgreementTypeID,
                               AgreementTypeName = tbAgreementTypeList.AgreementTypeName,
                               SupplierNumber = tbSupplierList.SupplierNumber,
                               UseOf = tbOrderFormPactList.UseOf,
                               HostContractNumber=tbOrderFormPactList.HostContractNumber


                           });
          


            int intTotalRow = linqItem.Count();//总行数
            List<OrderFormPactList> listNotices = linqItem.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectMethodOfSettlingAccounts()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_MethodOfSettlingAccountsList
                                   select new
                                   {
                                       id = tbSupplierRank.MethodOfSettlingAccountsID,
                                       text = tbSupplierRank.MethodOfSettlingAccounts
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同终止处理
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectJingXiaoHeTongZhongZhi(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     //join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID

                                     join tbGoodsGenre in MyModels.S_GoodsGenreIDList on tbOrderFormPact.GoodsGenreID equals tbGoodsGenre.GoodsGenreID
                                     join tbGoodsChop in MyModels.B_GoodsChopList on tbOrderFormPact.GoodsChopID equals tbGoodsChop.GoodsChopID
                                     where tbOrderFormPact.AgreementTypeID==1
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         HostContractNumber = tbOrderFormPact.HostContractNumber,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,

                                         GoodsChopID = tbOrderFormPact.GoodsChopID,
                                         GoodsChopName = tbGoodsChop.GoodsChopName,
                                         GoodsGenreID = tbOrderFormPact.GoodsGenreID,
                                         GoodsGenreName = tbGoodsGenre.GoodsGenreName

                                     }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同终止处理
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectJingXiaoHeTongZhongZhi1(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         //join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID

                                     join tbGoodsGenre in MyModels.S_GoodsGenreIDList on tbOrderFormPact.GoodsGenreID equals tbGoodsGenre.GoodsGenreID
                                     join tbGoodsChop in MyModels.B_GoodsChopList on tbOrderFormPact.GoodsChopID equals tbGoodsChop.GoodsChopID
                                     where tbOrderFormPact.AgreementTypeID == 2
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         HostContractNumber = tbOrderFormPact.HostContractNumber,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,

                                         GoodsChopID = tbOrderFormPact.GoodsChopID,
                                         GoodsChopName = tbGoodsChop.GoodsChopName,
                                         GoodsGenreID = tbOrderFormPact.GoodsGenreID,
                                         GoodsGenreName = tbGoodsGenre.GoodsGenreName

                                     }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同终止处理
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectJingXiaoHeTongZhongZhi2(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         //join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID

                                     join tbGoodsGenre in MyModels.S_GoodsGenreIDList on tbOrderFormPact.GoodsGenreID equals tbGoodsGenre.GoodsGenreID
                                     join tbGoodsChop in MyModels.B_GoodsChopList on tbOrderFormPact.GoodsChopID equals tbGoodsChop.GoodsChopID
                                     where tbOrderFormPact.AgreementTypeID == 3
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         HostContractNumber = tbOrderFormPact.HostContractNumber,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,

                                         GoodsChopID = tbOrderFormPact.GoodsChopID,
                                         GoodsChopName = tbGoodsChop.GoodsChopName,
                                         GoodsGenreID = tbOrderFormPact.GoodsGenreID,
                                         GoodsGenreName = tbGoodsGenre.GoodsGenreName

                                     }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 经销合同终止处理
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectJingXiaoHeTongZhongZhi3(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         //join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID

                                     join tbGoodsGenre in MyModels.S_GoodsGenreIDList on tbOrderFormPact.GoodsGenreID equals tbGoodsGenre.GoodsGenreID
                                     join tbGoodsChop in MyModels.B_GoodsChopList on tbOrderFormPact.GoodsChopID equals tbGoodsChop.GoodsChopID
                                     where tbOrderFormPact.AgreementTypeID == 4
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         HostContractNumber = tbOrderFormPact.HostContractNumber,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf,

                                         GoodsChopID = tbOrderFormPact.GoodsChopID,
                                         GoodsChopName = tbGoodsChop.GoodsChopName,
                                         GoodsGenreID = tbOrderFormPact.GoodsGenreID,
                                         GoodsGenreName = tbGoodsGenre.GoodsGenreName

                                     }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 生产厂家
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectManufacturer(BsgridPage bsgridPage)
        {
            var listManufacturer = from tbManufacturer in MyModels.B_ManufacturerList
                                   orderby tbManufacturer.ManufacturerID
                                   select new Vo.Goods
                                   {
                                       ManufacturerID = tbManufacturer.ManufacturerID,
                                       ManufacturerName = tbManufacturer.ManufacturerName,
                                       ManufacturerCode = tbManufacturer.ManufacturerCode,
                                       ManufacturerPC = tbManufacturer.ManufacturerPC,
                                       ManufacturerAddress = tbManufacturer.ManufacturerAddress,
                                   };
            int intTotalRow = listManufacturer.Count();//总行数
            List<Goods> listNotices = listManufacturer.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取生产厂家名称
        /// </summary>
        /// <param name="ChangJiaID"></param>
        /// <returns></returns>
        public ActionResult HuoQuChangJiaMingCheng(int ChangJiaID)
        {
            if (ChangJiaID > 0)
            {
                var ChangJia = (from tbManufacturer in MyModels.B_ManufacturerList
                                where tbManufacturer.ManufacturerID == ChangJiaID
                                select new Vo.Goods
                                {
                                    ManufacturerID = tbManufacturer.ManufacturerID,
                                    ManufacturerName = tbManufacturer.ManufacturerName,
                                    ManufacturerCode = tbManufacturer.ManufacturerCode,
                                    ManufacturerPC = tbManufacturer.ManufacturerPC,
                                    ManufacturerAddress = tbManufacturer.ManufacturerAddress,
                                }).Single();
                return Json(ChangJia, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 合同提前结算日设定
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectXiuGaiShenHeFou2(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                     
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf



                                     }
                                   );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// //下拉框数据绑定结算方式 经销合同|终止处理
        /// </summary>
        /// <returns></returns>
        public ActionResult selectMethodOfSettlingAccount()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_MethodOfSettlingAccountsList
                                   select new
                                   {
                                       id = tbSupplierRank.MethodOfSettlingAccountsID,
                                       text = tbSupplierRank.MethodOfSettlingAccounts
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 结算日合同提前结算日设定
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectHeTongJieSuanRi(BsgridPage bsgridPage, string contractNumber)
        {

            var linqItem = from tbOrderFormPactList in MyModels.B_OrderFormPactList
                            join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPactList.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                            join tbAgreementTypeList in MyModels.S_AgreementTypeList on tbOrderFormPactList.AgreementTypeID equals tbAgreementTypeList.AgreementTypeID
                            join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPactList.SupplierID equals tbSupplierList.SupplierID
                            orderby tbOrderFormPactList.OrderFormPactID descending
                            select new OrderFormPactList
                            {
                                OrderFormPactID = tbOrderFormPactList.OrderFormPactID,
                                SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                ContractNumber = tbOrderFormPactList.ContractNumber,
                                SpouseBRanchID = tbOrderFormPactList.SpouseBRanchID,
                                ValidBegin = tbOrderFormPactList.ValidBegin,
                                ValidTip = tbOrderFormPactList.ValidTip,
                                ReleaseTimeStr = tbOrderFormPactList.ValidBegin.ToString(),
                                ReleaseTimeStrr = tbOrderFormPactList.ValidTip.ToString(),
                                SupplierID = tbOrderFormPactList.SupplierID,
                                AgreementTypeID = tbOrderFormPactList.AgreementTypeID,
                                AgreementTypeName = tbAgreementTypeList.AgreementTypeName,
                                SupplierNumber = tbSupplierList.SupplierNumber,
                                UseOf = tbOrderFormPactList.UseOf,
                                HostContractNumber = tbOrderFormPactList.HostContractNumber


                            };
            if (!string.IsNullOrEmpty(contractNumber))
            {
                linqItem = linqItem.Where(s => s.ContractNumber.Contains(contractNumber.Trim()));
            }
           


            int intTotalRow = linqItem.Count();//总行数
            List<OrderFormPactList> listNotices = linqItem.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 定义固定扣款项目对照表
        /// </summary>
        /// <returns></returns>
        public ActionResult selectWithholdTypeList()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_WithholdTypeList
                                   select new
                                   {
                                       id = tbSupplierRank.WithholdTypeID,
                                       text = tbSupplierRank.WithholdType
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 定义固定扣款项目对照表
        /// </summary>
        /// <returns></returns>
        public ActionResult selectWithholdTypeList2()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_WithholdTypeList
                                   select new
                                   {
                                       id = tbSupplierRank.WithholdTypeID,
                                       text = tbSupplierRank.WithholdType
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }
        //合同退场处理
        public ActionResult SelectHeTongChuLi(BsgridPage bsgridPage, string contractNumber)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                    
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf



                                     }
                                  );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
         //合同退场处理
        public ActionResult SelectHeTongChuLi1(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                     join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                     join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                     join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                     //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                     join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                     join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                     join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                    
                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                         SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                         ContractNumber = tbOrderFormPact.ContractNumber,
                                         SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                         ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                         MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                         MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                         ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                         ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                         AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                         AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                         AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                         AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                         ConclusionSite = tbOrderFormPact.ConclusionSite,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                         TryPaymentData = tbOrderFormPact.TryPaymentData,
                                         TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                         Registrant = tbOrderFormPact.Registrant,
                                         Auditor = tbOrderFormPact.Auditor,
                                         Executor = tbOrderFormPact.Executor,
                                         Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                         RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                         RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                         UseOf = tbOrderFormPact.UseOf



                                     }
                                  );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<OrderFormPactList> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPactList> bsgrid = new Bsgrid<OrderFormPactList>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectHeTongChuLiID(int ID)
        {
            if (ID > 0)
            {
                var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                         join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                         join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                         join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                         //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                         join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                         join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                         join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                         join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                         where tbOrderFormPact.OrderFormPactID == ID
                                         orderby tbOrderFormPact.OrderFormPactID
                                         select new OrderFormPactList
                                         {
                                             OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                             SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                             ContractNumber = tbOrderFormPact.ContractNumber,
                                             SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                             SupplierID = tbOrderFormPact.SupplierID,
                                             AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                             AgreementTypeName = tbAgreementType.AgreementTypeName,
                                             ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                             ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                             ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                             ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                             MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                             MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                             ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                             ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                             AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                             AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                             AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                             AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                             AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                             AgreementState = tbAgreementStateList.AgreementState,
                                             PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                             ConclusionSite = tbOrderFormPact.ConclusionSite,
                                             ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                             TryPaymentData = tbOrderFormPact.TryPaymentData,
                                             TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                             Registrant = tbOrderFormPact.Registrant,
                                             Auditor = tbOrderFormPact.Auditor,
                                             Executor = tbOrderFormPact.Executor,
                                             Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                             RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                             RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                             UseOf = tbOrderFormPact.UseOf



                                         }).Single();
                return Json(listOrderFormPact, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        /// <summary>
        /// 合同终止绑定text
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult selectHETONGID(int id)
        {
            if (id > 0)
            {
                var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         join tbStaffList in MyModels.B_StaffList on tbOrderFormPact.StaffID equals tbStaffList.StaffID
                                         join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID
                                         join tbAgreementManagerList in MyModels.S_AgreementManagerList on tbOrderFormPact.AgreementManagerID equals tbAgreementManagerList.AgreementManagerID
                                         join tbSpouseBRanchList in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanchList.SpouseBRanchID
                                         //join tbSupplierList in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplierList.SupplierID
                                         join tbMethodOfSettlingAccountsList in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID
                                         join tbGoodsList in MyModels.B_GoodsList on tbOrderFormPact.GoodsID equals tbGoodsList.GoodsID
                                         join tbAdjustAccountsFashionList in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashionList.AdjustAccountsFashionID
                                         join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                         join tbGoodsGenre in MyModels.S_GoodsGenreIDList on tbOrderFormPact.GoodsGenreID equals tbGoodsGenre.GoodsGenreID
                                         join tbGoodsChop in MyModels.B_GoodsChopList on tbOrderFormPact.GoodsChopID equals tbGoodsChop.GoodsChopID


                                         where tbOrderFormPact.OrderFormPactID == id

                                         select new OrderFormPactList
                                         {

                                             OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                             SpouseBRanchID = tbOrderFormPact.SpouseBRanchID,
                                             HostContractNumber = tbOrderFormPact.HostContractNumber,
                                             ContractNumber = tbOrderFormPact.ContractNumber,
                                             SpouseBRanchName = tbSpouseBRanchList.SpouseBRanchName,
                                             SupplierID = tbOrderFormPact.SupplierID,
                                             AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                             AgreementTypeName = tbAgreementType.AgreementTypeName,
                                             ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                             ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                             ReleaseTimejinchangtime = tbOrderFormPact.AdvanceMarketData.ToString(),
                                             ReleaseTimetuichangchangtime = tbOrderFormPact.RetreatMarketData.ToString(),
                                             MethodOfSettlingAccountsID = tbMethodOfSettlingAccountsList.MethodOfSettlingAccountsID,
                                             MethodOfSettlingAccounts = tbMethodOfSettlingAccountsList.MethodOfSettlingAccounts,
                                             ReleaseTimepiaoqitime = tbOrderFormPact.WritData.ToString(),
                                             ReleaseTimezhangqitime = tbOrderFormPact.AccountData.ToString(),
                                             AgreementManagerID = tbOrderFormPact.AgreementManagerID,
                                             AgreementManagerName = tbAgreementManagerList.AgreementManagerName,
                                             AgreementManagerZong = tbAgreementManagerList.AgreementManagerZong,
                                             AgreementManagerGaoJi = tbAgreementManagerList.AgreementManagerGaoJi,
                                             AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                             AgreementState = tbAgreementStateList.AgreementState,
                                             PurchasingAgent = tbOrderFormPact.PurchasingAgent,
                                             ConclusionSite = tbOrderFormPact.ConclusionSite,
                                             ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),
                                             TryPaymentData = tbOrderFormPact.TryPaymentData,
                                             TryPaymentShiXiaoData = tbOrderFormPact.TryPaymentShiXiaoData,
                                             GoodsID = tbOrderFormPact.GoodsID,
                                             Registrant = tbOrderFormPact.Registrant,
                                             Auditor = tbOrderFormPact.Auditor,
                                             Executor = tbOrderFormPact.Executor,
                                             Registranttimea = tbOrderFormPact.Registranttime.ToString(),
                                             RegistrantAuditortime = tbOrderFormPact.Auditortime.ToString(),
                                             RegistrantExecutortime = tbOrderFormPact.Executortime.ToString(),
                                             UseOf = tbOrderFormPact.UseOf,
                                             GoodsChopID = tbOrderFormPact.GoodsChopID,
                                             GoodsChopName = tbGoodsChop.GoodsChopName,
                                             GoodsGenreID = tbOrderFormPact.GoodsGenreID,
                                             GoodsGenreName = tbGoodsGenre.GoodsGenreName

                                         }).Single();
                return Json(listOrderFormPact, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        public ActionResult Insertaaa(B_OrderFormPactList OrderFormPactList)
        {
                
            
                      
            OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
            OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
            OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
            OrderFormPactList.SupplierID = 2;
            OrderFormPactList.AgreementTypeID = 1;

            OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
            OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
            OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
            OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

            OrderFormPactList.MethodOfSettlingAccountsID =1;

            OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
            OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

            OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
            OrderFormPactList.AgreementStatusID = 2;
            OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
            OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
            OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
            OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
            OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
            OrderFormPactList.GoodsID = 1;
            OrderFormPactList.Registrant = Request.Form["Registrant"];
            OrderFormPactList.Auditor = Request.Form["Auditor"];
            OrderFormPactList.Executor = Request.Form["Executor"];
            OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
            OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
            OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
            OrderFormPactList.UseOf = false;
            OrderFormPactList.GoodsGenreID = 3;
            OrderFormPactList.GoodsChopID = 1003;
            OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
            OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
            OrderFormPactList.StaffID = 1;
            OrderFormPactList.AgreementID = 1;
            OrderFormPactList.AdjustAccountsFashionID = 1;

           


            if (OrderFormPactList.OrderFormPactID > 0 && OrderFormPactList.SpouseBRanchID > 0 && OrderFormPactList.ContractNumber != null && OrderFormPactList.SupplierID > 0
                 && OrderFormPactList.AgreementTypeID > 0 && OrderFormPactList.MethodOfSettlingAccountsID != null && OrderFormPactList.AgreementManagerID != null
                 && OrderFormPactList.AgreementStatusID > 0 && OrderFormPactList.PurchasingAgent != null && OrderFormPactList.ConclusionSite != null

                && OrderFormPactList.TryPaymentData > 0 && OrderFormPactList.TryPaymentShiXiaoData > 0 && OrderFormPactList.GoodsID > 0
                && OrderFormPactList.Registrant != null && OrderFormPactList.Auditor != null
                && OrderFormPactList.Executor != null && OrderFormPactList.UseOf != null
                && OrderFormPactList.GoodsGenreID > 0 && OrderFormPactList.GoodsChopID > 0
                && OrderFormPactList.StaffID > 0 && OrderFormPactList.AgreementID > 0 && OrderFormPactList.AdjustAccountsFashionID > 0
               

                )
                {
                    MyModels.B_OrderFormPactList.Add(OrderFormPactList);
                    MyModels.SaveChanges();//changes的三种状态：新增、修改、删除                                         
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }

        public ActionResult UpdateJingXiao(B_OrderFormPactList OrderFormPactList)
        {
            string MSG = "fail";
            try
            {
                OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
                OrderFormPactList.SupplierID = 3;
                OrderFormPactList.AgreementTypeID = 1;

                OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
                OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
                OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
                OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

                OrderFormPactList.MethodOfSettlingAccountsID = 1;

                OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
                OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

                OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
                OrderFormPactList.AgreementStatusID = 2;
                OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
                OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
                OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
                OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
                OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
                OrderFormPactList.GoodsID = 1;
                OrderFormPactList.Registrant = Request.Form["Registrant"];
                OrderFormPactList.Auditor = Request.Form["Auditor"];
                OrderFormPactList.Executor = Request.Form["Executor"];
                OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
                OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
                OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
                OrderFormPactList.UseOf = false;
                OrderFormPactList.GoodsGenreID = 3;
                OrderFormPactList.GoodsChopID = 1003;
                OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
                OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
                OrderFormPactList.StaffID = 1;
                OrderFormPactList.AgreementID = 1;
                OrderFormPactList.AdjustAccountsFashionID = 1;



                MyModels.Entry(OrderFormPactList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                MSG = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改经销
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult UpdateJingXiao1(B_OrderFormPactList OrderFormPactList)
        {
            string MSG = "fail";
            try
            {
                OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
                OrderFormPactList.SupplierID = 3;
                OrderFormPactList.AgreementTypeID = 1;

                OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
                OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
                OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
                OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

                OrderFormPactList.MethodOfSettlingAccountsID = 1;

                OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
                OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

                OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
                OrderFormPactList.AgreementStatusID = 2;
                OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
                OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
                OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
                OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
                OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
                OrderFormPactList.GoodsID = 1;
                OrderFormPactList.Registrant = Request.Form["Registrant"];
                OrderFormPactList.Auditor = Request.Form["Auditor"];
                OrderFormPactList.Executor = Request.Form["Executor"];
                OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
                OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
                OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
                OrderFormPactList.UseOf = false;
                OrderFormPactList.GoodsGenreID = 3;
                OrderFormPactList.GoodsChopID = 1003;
                OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
                OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
                OrderFormPactList.StaffID = 1;
                OrderFormPactList.AgreementID = 1;
                OrderFormPactList.AdjustAccountsFashionID = 1;

                OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
                OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
                OrderFormPactList.AgreementRen = Request.Form["AgreementRen"];


                MyModels.Entry(OrderFormPactList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                MSG = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改代销
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult UpdateJingXiao2(B_OrderFormPactList OrderFormPactList)
        {
            string MSG = "fail";
            try
            {
                OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
                OrderFormPactList.SupplierID = 3;
                OrderFormPactList.AgreementTypeID = 2;

                OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
                OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
                OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
                OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

                OrderFormPactList.MethodOfSettlingAccountsID = 1;

                OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
                OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

                OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
                OrderFormPactList.AgreementStatusID = 2;
                OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
                OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
                OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
                OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
                OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
                OrderFormPactList.GoodsID = 1;
                OrderFormPactList.Registrant = Request.Form["Registrant"];
                OrderFormPactList.Auditor = Request.Form["Auditor"];
                OrderFormPactList.Executor = Request.Form["Executor"];
                OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
                OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
                OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
                OrderFormPactList.UseOf = false;
                OrderFormPactList.GoodsGenreID = 3;
                OrderFormPactList.GoodsChopID = 1003;
                OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
                OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
                OrderFormPactList.StaffID = 1;
                OrderFormPactList.AgreementID = 1;
                OrderFormPactList.AdjustAccountsFashionID = 1;

                OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
                OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
                OrderFormPactList.AgreementRen = Request.Form["AgreementRen"];


                MyModels.Entry(OrderFormPactList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                MSG = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改联营
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult UpdateJingXiao3(B_OrderFormPactList OrderFormPactList)
        {
            string MSG = "fail";
            try
            {
                OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
                OrderFormPactList.SupplierID = 3;
                OrderFormPactList.AgreementTypeID = 3;

                OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
                OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
                OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
                OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

                OrderFormPactList.MethodOfSettlingAccountsID = 1;

                OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
                OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

                OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
                OrderFormPactList.AgreementStatusID = 2;
                OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
                OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
                OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
                OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
                OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
                OrderFormPactList.GoodsID = 1;
                OrderFormPactList.Registrant = Request.Form["Registrant"];
                OrderFormPactList.Auditor = Request.Form["Auditor"];
                OrderFormPactList.Executor = Request.Form["Executor"];
                OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
                OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
                OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
                OrderFormPactList.UseOf = false;
                OrderFormPactList.GoodsGenreID = 3;
                OrderFormPactList.GoodsChopID = 1003;
                OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
                OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
                OrderFormPactList.StaffID = 1;
                OrderFormPactList.AgreementID = 1;
                OrderFormPactList.AdjustAccountsFashionID = 1;

                OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
                OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
                OrderFormPactList.AgreementRen = Request.Form["AgreementRen"];


                MyModels.Entry(OrderFormPactList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                MSG = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改租赁
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult UpdateJingXiao4(B_OrderFormPactList OrderFormPactList)
        {
            string MSG = "fail";
            try
            {
                OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
                OrderFormPactList.SupplierID = 3;
                OrderFormPactList.AgreementTypeID = 4;

                OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
                OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
                OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
                OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

                OrderFormPactList.MethodOfSettlingAccountsID = 1;

                OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
                OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

                OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
                OrderFormPactList.AgreementStatusID = 2;
                OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
                OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
                OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
                OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
                OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
                OrderFormPactList.GoodsID = 1;
                OrderFormPactList.Registrant = Request.Form["Registrant"];
                OrderFormPactList.Auditor = Request.Form["Auditor"];
                OrderFormPactList.Executor = Request.Form["Executor"];
                OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
                OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
                OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
                OrderFormPactList.UseOf = false;
                OrderFormPactList.GoodsGenreID = 3;
                OrderFormPactList.GoodsChopID = 1003;
                OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
                OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
                OrderFormPactList.StaffID = 1;
                OrderFormPactList.AgreementID = 1;
                OrderFormPactList.AdjustAccountsFashionID = 1;

                OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
                OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
                OrderFormPactList.AgreementRen = Request.Form["AgreementRen"];


                MyModels.Entry(OrderFormPactList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                MSG = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateDaiXiao(B_OrderFormPactList OrderFormPactList)
        {
            string MSG = "fail";
            try
            {
                OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
                OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
                OrderFormPactList.SupplierID = 3;
                OrderFormPactList.AgreementTypeID = 2;

                OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
                OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
                OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
                OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

                OrderFormPactList.MethodOfSettlingAccountsID = 1;

                OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
                OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

                OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
                OrderFormPactList.AgreementStatusID = 2;
                OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
                OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
                OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
                OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
                OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
                OrderFormPactList.GoodsID = 1;
                OrderFormPactList.Registrant = Request.Form["Registrant"];
                OrderFormPactList.Auditor = Request.Form["Auditor"];
                OrderFormPactList.Executor = Request.Form["Executor"];
                OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
                OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
                OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
                OrderFormPactList.UseOf = false;
                OrderFormPactList.GoodsGenreID = 3;
                OrderFormPactList.GoodsChopID = 1003;
                OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
                OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
                OrderFormPactList.StaffID = 1;
                OrderFormPactList.AgreementID = 1;
                OrderFormPactList.AdjustAccountsFashionID = 1;



                MyModels.Entry(OrderFormPactList).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                MSG = "success";
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }
            return Json(MSG, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除合同
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public ActionResult DeleteHeTong(int goodsId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_OrderFormPactList dbGoodsDetail = (from tbGoodsDetail in MyModels.B_OrderFormPactList
                                             where tbGoodsDetail.OrderFormPactID == goodsId
                                             select tbGoodsDetail).Single();

                MyModels.B_OrderFormPactList.Remove(dbGoodsDetail);

                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception )
            {
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
         
        public ActionResult Insertaaas(B_OrderFormPactList OrderFormPactList)
        {



            OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
            OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
            OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
            OrderFormPactList.SupplierID = Convert.ToInt32(Request.Form["SupplierID"]);
            OrderFormPactList.AgreementTypeID = 2;

            OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
            OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
            OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
            OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

            OrderFormPactList.MethodOfSettlingAccountsID = 2;
            OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
            OrderFormPactList.AgreementStatusID = 2;
            OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
            OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
            OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
            OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);            
            OrderFormPactList.GoodsID = 1;
            OrderFormPactList.Registrant = Request.Form["Registrant"];
            OrderFormPactList.Auditor = Request.Form["Auditor"];
            OrderFormPactList.Executor = Request.Form["Executor"];
            OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
            OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
            OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
            OrderFormPactList.UseOf = false;
            OrderFormPactList.GoodsGenreID = 3;
            OrderFormPactList.GoodsChopID = 1003;
            OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
            OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
            OrderFormPactList.StaffID = 1;
            OrderFormPactList.AgreementID = 1;
            OrderFormPactList.AdjustAccountsFashionID = 1;

            if (OrderFormPactList.OrderFormPactID > 0 && OrderFormPactList.SpouseBRanchID > 0 && OrderFormPactList.ContractNumber != null && OrderFormPactList.SupplierID > 0
                 && OrderFormPactList.AgreementTypeID > 0 && OrderFormPactList.MethodOfSettlingAccountsID != null && OrderFormPactList.AgreementManagerID != null
                 && OrderFormPactList.AgreementStatusID > 0 && OrderFormPactList.PurchasingAgent != null && OrderFormPactList.ConclusionSite != null

                && OrderFormPactList.TryPaymentData > 0 && OrderFormPactList.GoodsID > 0
                && OrderFormPactList.Registrant != null && OrderFormPactList.Auditor != null
                && OrderFormPactList.Executor != null && OrderFormPactList.UseOf != null
                && OrderFormPactList.GoodsGenreID > 0 && OrderFormPactList.GoodsChopID > 0
                && OrderFormPactList.StaffID > 0 && OrderFormPactList.AgreementID > 0 && OrderFormPactList.AdjustAccountsFashionID > 0

                )
            {
                MyModels.B_OrderFormPactList.Add(OrderFormPactList);
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 新增经销
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult InsertJingXiao(B_OrderFormPactList OrderFormPactList)
        {




            OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
            OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
            OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
            OrderFormPactList.SupplierID = 4;
            OrderFormPactList.AgreementTypeID = 1;

            OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
            OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
            OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
            OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

            OrderFormPactList.MethodOfSettlingAccountsID = 1;

            OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
            OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

            OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
            OrderFormPactList.AgreementStatusID = 2;
            OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
            OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
            OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
            OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
            OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
            OrderFormPactList.GoodsID = 1;
            OrderFormPactList.Registrant = Request.Form["Registrant"];
            OrderFormPactList.Auditor = Request.Form["Auditor"];
            OrderFormPactList.Executor = Request.Form["Executor"];
            OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
            OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
            OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
            OrderFormPactList.UseOf = false;
            OrderFormPactList.GoodsGenreID = 3;
            OrderFormPactList.GoodsChopID = 1003;
            OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
            OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
            OrderFormPactList.StaffID = 1;
            OrderFormPactList.AgreementID = 1;
            OrderFormPactList.AdjustAccountsFashionID = 1;

            OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
            OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
            OrderFormPactList.AgreementRen = Request.Form["AgreementRen"];



            if (OrderFormPactList.OrderFormPactID > 0 && OrderFormPactList.SpouseBRanchID > 0 && OrderFormPactList.ContractNumber != null && OrderFormPactList.SupplierID > 0
                 && OrderFormPactList.AgreementTypeID > 0 && OrderFormPactList.MethodOfSettlingAccountsID != null && OrderFormPactList.AgreementManagerID != null
                 && OrderFormPactList.AgreementStatusID > 0 && OrderFormPactList.PurchasingAgent != null && OrderFormPactList.ConclusionSite != null

                && OrderFormPactList.TryPaymentData > 0 && OrderFormPactList.TryPaymentShiXiaoData > 0 && OrderFormPactList.GoodsID > 0
                && OrderFormPactList.Registrant != null && OrderFormPactList.Auditor != null
                && OrderFormPactList.Executor != null && OrderFormPactList.UseOf != null
                && OrderFormPactList.GoodsGenreID > 0 && OrderFormPactList.GoodsChopID > 0
                && OrderFormPactList.StaffID > 0 && OrderFormPactList.AgreementID > 0 && OrderFormPactList.AdjustAccountsFashionID > 0
                && OrderFormPactList.AgreementRen != null

                )
            {
                MyModels.B_OrderFormPactList.Add(OrderFormPactList);
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除                                         
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 新增代销
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult InsertDaiXiao(B_OrderFormPactList OrderFormPactList)
        {




            OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
            OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
            OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
            OrderFormPactList.SupplierID = 4;
            OrderFormPactList.AgreementTypeID = 2;

            OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
            OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
            OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
            OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

            OrderFormPactList.MethodOfSettlingAccountsID = 1;

            OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
            OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);
            OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
            OrderFormPactList.AgreementStatusID = 2;
            OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
            OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
            OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
            OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
            OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
            OrderFormPactList.GoodsID = 1;
            OrderFormPactList.Registrant = Request.Form["Registrant"];
            OrderFormPactList.Auditor = Request.Form["Auditor"];
            OrderFormPactList.Executor = Request.Form["Executor"];
            OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
            OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
            OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
            OrderFormPactList.UseOf = false;
            OrderFormPactList.GoodsGenreID = 3;
            OrderFormPactList.GoodsChopID = 1003;
            OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
            OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
            OrderFormPactList.StaffID = 1;
            OrderFormPactList.AgreementID = 1;
            OrderFormPactList.AdjustAccountsFashionID = 1;

            OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
            OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
            OrderFormPactList.AgreementRen = Request.Form["AgreementRen"];



            if (OrderFormPactList.OrderFormPactID > 0 && OrderFormPactList.SpouseBRanchID > 0 && OrderFormPactList.ContractNumber != null && OrderFormPactList.SupplierID > 0
                 && OrderFormPactList.AgreementTypeID > 0 && OrderFormPactList.MethodOfSettlingAccountsID != null && OrderFormPactList.AgreementManagerID != null
                 && OrderFormPactList.AgreementStatusID > 0 && OrderFormPactList.PurchasingAgent != null && OrderFormPactList.ConclusionSite != null

                && OrderFormPactList.TryPaymentData > 0 && OrderFormPactList.TryPaymentShiXiaoData > 0 && OrderFormPactList.GoodsID > 0
                && OrderFormPactList.Registrant != null && OrderFormPactList.Auditor != null
                && OrderFormPactList.Executor != null && OrderFormPactList.UseOf != null
                && OrderFormPactList.GoodsGenreID > 0 && OrderFormPactList.GoodsChopID > 0
                && OrderFormPactList.StaffID > 0 && OrderFormPactList.AgreementID > 0 && OrderFormPactList.AdjustAccountsFashionID > 0
                && OrderFormPactList.AgreementRen != null

                )
            {
                MyModels.B_OrderFormPactList.Add(OrderFormPactList);
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除                                         
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 新增联营
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult InsertLianYing(B_OrderFormPactList OrderFormPactList)
        {




            OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
            OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
            OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
            OrderFormPactList.SupplierID = 4;
            OrderFormPactList.AgreementTypeID = 3;

            OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
            OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
            OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
            OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

            OrderFormPactList.MethodOfSettlingAccountsID = 1;

            OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
            OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

            OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
            OrderFormPactList.AgreementStatusID = 2;
            OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
            OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
            OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
            OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
            OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
            OrderFormPactList.GoodsID = 1;
            OrderFormPactList.Registrant = Request.Form["Registrant"];
            OrderFormPactList.Auditor = Request.Form["Auditor"];
            OrderFormPactList.Executor = Request.Form["Executor"];
            OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
            OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
            OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
            OrderFormPactList.UseOf = false;
            OrderFormPactList.GoodsGenreID = 3;
            OrderFormPactList.GoodsChopID = 1003;
            OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
            OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
            OrderFormPactList.StaffID = 1;
            OrderFormPactList.AgreementID = 1;
            OrderFormPactList.AdjustAccountsFashionID = 1;

            OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
            OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
            OrderFormPactList.AgreementRen= Request.Form["AgreementRen"];



            if (OrderFormPactList.OrderFormPactID > 0 && OrderFormPactList.SpouseBRanchID > 0 && OrderFormPactList.ContractNumber != null && OrderFormPactList.SupplierID > 0
                 && OrderFormPactList.AgreementTypeID > 0 && OrderFormPactList.MethodOfSettlingAccountsID != null && OrderFormPactList.AgreementManagerID != null
                 && OrderFormPactList.AgreementStatusID > 0 && OrderFormPactList.PurchasingAgent != null && OrderFormPactList.ConclusionSite != null

                && OrderFormPactList.TryPaymentData > 0 && OrderFormPactList.TryPaymentShiXiaoData > 0 && OrderFormPactList.GoodsID > 0
                && OrderFormPactList.Registrant != null && OrderFormPactList.Auditor != null
                && OrderFormPactList.Executor != null && OrderFormPactList.UseOf != null
                && OrderFormPactList.GoodsGenreID > 0 && OrderFormPactList.GoodsChopID > 0
                && OrderFormPactList.StaffID > 0 && OrderFormPactList.AgreementID > 0 && OrderFormPactList.AdjustAccountsFashionID > 0
                && OrderFormPactList.AgreementRen != null

                )
            {
                MyModels.B_OrderFormPactList.Add(OrderFormPactList);
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除                                         
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 租赁新增
        /// </summary>
        /// <param name="OrderFormPactList"></param>
        /// <returns></returns>
        public ActionResult InsertZuLin(B_OrderFormPactList OrderFormPactList)
        {




            OrderFormPactList.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
            OrderFormPactList.SpouseBRanchID = Convert.ToInt32(Request.Form["SpouseBRanchID"]);
            OrderFormPactList.ContractNumber = Request.Form["ContractNumber"];
            OrderFormPactList.SupplierID = 1;
            OrderFormPactList.AgreementTypeID = 4;

            OrderFormPactList.ValidTip = Convert.ToDateTime(Request.Form["ValidTip"]);
            OrderFormPactList.ValidBegin = Convert.ToDateTime(Request.Form["ValidBegin"]);
            OrderFormPactList.AdvanceMarketData = Convert.ToDateTime(Request.Form["AdvanceMarketData"]);
            OrderFormPactList.RetreatMarketData = Convert.ToDateTime(Request.Form["RetreatMarketData"]);

            OrderFormPactList.MethodOfSettlingAccountsID = 3;

            OrderFormPactList.WritData = Convert.ToDateTime(Request.Form["WritData"]);
            OrderFormPactList.AccountData = Convert.ToDateTime(Request.Form["AccountData"]);

            OrderFormPactList.AgreementManagerID = Convert.ToInt32(Request.Form["AgreementManagerID"]);
            OrderFormPactList.AgreementStatusID = 2;
            OrderFormPactList.PurchasingAgent = Request.Form["PurchasingAgent"];
            OrderFormPactList.ConclusionSite = Request.Form["ConclusionSite"];
            OrderFormPactList.ConclusionTime = Convert.ToDateTime(Request.Form["ConclusionTime"]);
            OrderFormPactList.TryPaymentData = Convert.ToInt32(Request.Form["TryPaymentData"]);
            OrderFormPactList.TryPaymentShiXiaoData = Convert.ToInt32(Request.Form["TryPaymentShiXiaoData"]);
            OrderFormPactList.GoodsID = 1;
            OrderFormPactList.Registrant = Request.Form["Registrant"];
            OrderFormPactList.Auditor = Request.Form["Auditor"];
            OrderFormPactList.Executor = Request.Form["Executor"];
            OrderFormPactList.Registranttime = Convert.ToDateTime(Request.Form["Registranttime"]);
            OrderFormPactList.Auditortime = Convert.ToDateTime(Request.Form["Auditortime"]);
            OrderFormPactList.Executortime = Convert.ToDateTime(Request.Form["Executortime"]);
            OrderFormPactList.UseOf = false;
          OrderFormPactList.GoodsGenreID = 3;
            OrderFormPactList.GoodsChopID = 1003;
            OrderFormPactList.HostContractNumber = Request.Form["HostContractNumber"];
            OrderFormPactList.HandworkContractNumber = Request.Form["HandworkContractNumber"];
            OrderFormPactList.StaffID = 1;
            OrderFormPactList.AgreementID = 1;
            OrderFormPactList.AdjustAccountsFashionID = 1;

            OrderFormPactList.PlanFiringData = Convert.ToDateTime(Request.Form["PlanFiringData"]);
            OrderFormPactList.Firingdata = Convert.ToDateTime(Request.Form["Firingdata"]);
            OrderFormPactList.AgreementRen = Request.Form["AgreementRen"];



            if (OrderFormPactList.OrderFormPactID > 0 && OrderFormPactList.SpouseBRanchID > 0 && OrderFormPactList.ContractNumber != null && OrderFormPactList.SupplierID > 0
                 && OrderFormPactList.AgreementTypeID > 0 && OrderFormPactList.MethodOfSettlingAccountsID != null && OrderFormPactList.AgreementManagerID != null
                 && OrderFormPactList.AgreementStatusID > 0 && OrderFormPactList.PurchasingAgent != null && OrderFormPactList.ConclusionSite != null

                && OrderFormPactList.TryPaymentData > 0 && OrderFormPactList.TryPaymentShiXiaoData > 0 && OrderFormPactList.GoodsID > 0
                && OrderFormPactList.Registrant != null && OrderFormPactList.Auditor != null
                && OrderFormPactList.Executor != null && OrderFormPactList.UseOf != null
                && OrderFormPactList.GoodsGenreID > 0 && OrderFormPactList.GoodsChopID > 0
                && OrderFormPactList.StaffID > 0 && OrderFormPactList.AgreementID > 0 && OrderFormPactList.AdjustAccountsFashionID > 0
                && OrderFormPactList.AgreementRen != null

                )
            {
                MyModels.B_OrderFormPactList.Add(OrderFormPactList);
                MyModels.SaveChanges();//changes的三种状态：新增、修改、删除                                         
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
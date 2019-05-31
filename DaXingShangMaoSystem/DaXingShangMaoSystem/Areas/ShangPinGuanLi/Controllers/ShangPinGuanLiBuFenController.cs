using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.ShangPinGuanLi.Controllers
{
    public class ShangPinGuanLiBuFenController : Controller
    {
        // GET: ShangPinGuanLi/ShangPinGuanLiBuFen
        //自营商品信息管理
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        #region 自营商品定义信息       
        public ActionResult ZiYinShangPinXinXi()
        {
            return View();
        }
      
        /// <summary>
        /// 类型条码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBarCodTypeID()
        {
            List<SelectVo> listBarCodType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listBarCodType.Add(selectVo);

            List<SelectVo> listBarCodTypeID = (from tbBarCodType in MyModels.S_BarCodTypeList
                                               select new SelectVo
                                               {
                                                   id = tbBarCodType.BarCodTypeID,
                                                   text = tbBarCodType.BarCodTypeName.Trim()
                                               }).ToList();

            listBarCodType.AddRange(listBarCodTypeID);

            return Json(listBarCodType, JsonRequestBehavior.AllowGet);          
        }      

        /// <summary>
        /// 商品标记
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsStampID()
        {
            List<SelectVo> listGoodsStamp = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsStamp.Add(selectVo);

            List<SelectVo> listGoodsStampID = (from tbGoodsStamp in MyModels.S_GoodsStampList
                                               select new SelectVo
                                               {
                                                   id = tbGoodsStamp.GoodsStampID,
                                                   text = tbGoodsStamp.GoodsStampName.Trim()
                                               }).ToList();

            listGoodsStamp.AddRange(listGoodsStampID);

            return Json(listGoodsStamp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCommodityTypeID()
        {
            List<SelectVo> listCommodityTypeID = (from tbCommodityType in MyModels.S_CommodityTypeList
                                                  where tbCommodityType.CommodityTypeID == 1
                                                  select new SelectVo
                                                  {
                                                      id = tbCommodityType.CommodityTypeID,
                                                      text = tbCommodityType.CommodityType.Trim()
                                                  }).ToList();

            return Json(listCommodityTypeID, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品颜色
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsColoursID()
        {
            List<SelectVo> listGoodsStamp = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsStamp.Add(selectVo);

            List<SelectVo> listGoodsStampID = (from tbGoodsStamp in MyModels.S_GoodsColoursList
                                               select new SelectVo
                                               {
                                                   id = tbGoodsStamp.GoodsColoursID,
                                                   text = tbGoodsStamp.GoodsColours.Trim()
                                               }).ToList();

            listGoodsStamp.AddRange(listGoodsStampID);

            return Json(listGoodsStamp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 计量单位
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectEstimateUnitName()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.S_EstimateUnitList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.EstimateUnitID,
                                                     text = tbEstimateUnit.EstimateUnitName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 使用对象
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectApplyTargetID()
        {
            List<SelectVo> listApplyTarget = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listApplyTarget.Add(selectVo);

            List<SelectVo> listApplyTargetID = (from tbApplyTarget in MyModels.S_ApplyTargetList
                                                select new SelectVo
                                                {
                                                    id = tbApplyTarget.ApplyTargetID,
                                                    text = tbApplyTarget.ApplyTarget.Trim()
                                                }).ToList();

            listApplyTarget.AddRange(listApplyTargetID);

            return Json(listApplyTarget, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 来源地点
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSourceID()
        {
            List<SelectVo> listSource = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listSource.Add(selectVo);

            List<SelectVo> listSourceID = (from tbSource in MyModels.B_SourceList
                                           select new SelectVo
                                           {
                                               id = tbSource.SourceID,
                                               text = tbSource.SourceName.Trim()
                                           }).ToList();

            listSource.AddRange(listSourceID);

            return Json(listSource, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品经营季节
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsSeasonID()
        {
            List<SelectVo> listGoodsSeason = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsSeason.Add(selectVo);

            List<SelectVo> listGoodsSeasonID = (from tbGoodsSeason in MyModels.S_GoodsSeasonList
                                                select new SelectVo
                                                {
                                                    id = tbGoodsSeason.GoodsSeasonID,
                                                    text = tbGoodsSeason.GoodsSeasonName.Trim()
                                                }).ToList();

            listGoodsSeason.AddRange(listGoodsSeasonID);

            return Json(listGoodsSeason, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsStatusID()
        {
            List<SelectVo> listGoodsStatus = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsStatus.Add(selectVo);

            List<SelectVo> listGoodsStatusID = (from tbGoodsStatus in MyModels.S_GoodsStatusIList
                                                where tbGoodsStatus.GoodsStatusID ==2 || tbGoodsStatus.GoodsStatusID ==3
                                                select new SelectVo
                                                {
                                                    id = tbGoodsStatus.GoodsStatusID,
                                                    text = tbGoodsStatus.GoodsStatusName.Trim()
                                                }).ToList();

            listGoodsStatus.AddRange(listGoodsStatusID);

            return Json(listGoodsStatus, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 负库存销售状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBurdenStockSellStatusID()
        {
            List<SelectVo> listBurdenStockSellStatus = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listBurdenStockSellStatus.Add(selectVo);

            List<SelectVo> listBurdenStockSellStatusID = (from tbBurdenStockSellStatus in MyModels.S_BurdenStockSellStatusList
                                                          select new SelectVo
                                                          {
                                                              id = tbBurdenStockSellStatus.BurdenStockSellStatusID,
                                                              text = tbBurdenStockSellStatus.BurdenStockSellStatusName.Trim()
                                                          }).ToList();

            listBurdenStockSellStatus.AddRange(listBurdenStockSellStatusID);

            return Json(listBurdenStockSellStatus, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自动生成商品代码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsCode()
        {
            string strGoodsCode = "";
            var listDep = (from tbDmp in MyModels.B_GoodsList orderby tbDmp.GoodsCode select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_GoodsList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.GoodsCode.Substring(2, 7));
                intCode++;
                strGoodsCode = intCode.ToString();
                for (int i = 0; i < 6; i++)
                {
                    strGoodsCode = strGoodsCode.Length < 6 ? "0" + strGoodsCode : strGoodsCode;
                }
            }
            else
            {
                strGoodsCode = "000001";
            }
            return Json(strGoodsCode, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自动生成商品条码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsTiaoMa()
        {
            string strGoodsTiaoMa = "";
            var listDep = (from tbDmp in MyModels.B_GoodsList orderby tbDmp.GoodsCode select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_GoodsList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.GoodsCode.Substring(2, 5));
                intCode++;
                strGoodsTiaoMa = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {
                    strGoodsTiaoMa = strGoodsTiaoMa.Length < 4 ? "0" + strGoodsTiaoMa : strGoodsTiaoMa;
                }
                strGoodsTiaoMa = "123456789" + strGoodsTiaoMa;
            }
            else
            {
                strGoodsTiaoMa = "1234567890001";
            }
            return Json(strGoodsTiaoMa, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 自动生成PLUCod码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectPLUCod()
        {
            string strPLUCod = "";
            var listDep = (from tbDmp in MyModels.B_GoodsList orderby tbDmp.PLUCod select tbDmp).ToList();
            if (listDep.Count > 0)
            {
                int count = listDep.Count;
                B_GoodsList modelDep = listDep[count - 1];
                int intCode = Convert.ToInt32(modelDep.PLUCod.Substring(1, 4));
                intCode++;
                strPLUCod = intCode.ToString();
                for (int i = 0; i < 4; i++)
                {
                    strPLUCod = strPLUCod.Length < 4 ? "0" + strPLUCod : strPLUCod;
                }
                strPLUCod = "E" + strPLUCod;
            }
            else
            {
                strPLUCod = "E0001";
            }
            return Json(strPLUCod, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 选择合同
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectOrderFormPactID(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                    join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                                    orderby tbOrderFormPact.OrderFormPactID
                                    where tbAgreementType.AgreementTypeID ==1 || tbAgreementType.AgreementTypeID == 2
                                     select new Vo.OrderFormPact
                                    {
                                        OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                        AgreementTypeName = tbAgreementType.AgreementTypeName.Trim(),
                                        ContractNumber = tbOrderFormPact.ContractNumber,                                                                                                                    
                                    }).ToList();           
            int totalRow = listOrderFormPact.Count();

            List<OrderFormPact> notices = listOrderFormPact.OrderByDescending(p => p.OrderFormPactID).//noboer表达式
                 Skip(bsgridPage.GetStartIndex()).//F12（看）
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<OrderFormPact> bsgrid = new Vo.Bsgrid<OrderFormPact>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 获取合同名称
        /// </summary>
        /// <param name="HeTongID"></param>
        /// <returns></returns>
        public ActionResult HuoQuHeTongMingCheng(int HeTongID)
        {
            if (HeTongID > 0)
            {
                var HeTong = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                              join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID
                              where tbOrderFormPact.OrderFormPactID == HeTongID
                              select new Vo.OrderFormPact
                              {
                                  OrderFormPactID = tbOrderFormPact.OrderFormPactID,                                
                                  AgreementTypeName = tbAgreementType.AgreementTypeName.Trim(),
                                  ContractNumber = tbOrderFormPact.ContractNumber,  
                              }).Single();
                return Json(HeTong, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        ///  选择供货商
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectSupplier(BsgridPage bsgridPage)
        {
            var listSupplier = from tbSupplier in MyModels.B_SupplierList
                               orderby tbSupplier.SupplierID
                               select new Vo.Goods
                               {
                                   SupplierID = tbSupplier.SupplierID,
                                   SupplierName = tbSupplier.SupplierName,
                                   SupplierNumber = tbSupplier.SupplierNumber,                          
                               };
            int intTotalRow = listSupplier.Count();//总行数
            List<Goods> listNotices = listSupplier.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取供货商
        /// </summary>
        /// <param name="GongHuoID"></param>
        /// <returns></returns>
        public ActionResult HuoQuGongHuoShangMingCheng(int GongHuoID)
        {
            if (GongHuoID > 0)
            {
                var GongHuo = (from tbSupplier in MyModels.B_SupplierList
                               where tbSupplier.SupplierID == GongHuoID
                               select new Vo.Goods
                               {
                                   SupplierID = tbSupplier.SupplierID,
                                   SupplierName = tbSupplier.SupplierName,
                                   SupplierNumber = tbSupplier.SupplierNumber,                         
                               }).Single();
                return Json(GongHuo, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
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
                                    ManufacturerCode = tbManufacturer.ManufacturerCode
                                }).Single();
                return Json(ChangJia, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 商标品牌的选择
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectGoodsChopID(BsgridPage bsgridPage)
        {
            var items = from tbGoodsChop in MyModels.B_GoodsChopList
                        orderby tbGoodsChop.GoodsChopID
                        select new Vo.GoodsChop
                        {
                            GoodsChopID = tbGoodsChop.GoodsChopID,
                            GoodsChopName = tbGoodsChop.GoodsChopName,
                            GoodsChopCode = tbGoodsChop.GoodsChopCode,
                        };
            int intTotalRow = items.Count();//总行数
            List<GoodsChop> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<GoodsChop> bsgrid = new Bsgrid<GoodsChop>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取商标品牌名称
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangBiaoMingCheng(int GPID)
        {
            if (GPID > 0)
            {
                var GoodsChop = (from tbGoodsChop in MyModels.B_GoodsChopList
                                 where tbGoodsChop.GoodsChopID == GPID
                                 select new Vo.GoodsChop
                                 {
                                     GoodsChopID = tbGoodsChop.GoodsChopID,
                                     GoodsChopName = tbGoodsChop.GoodsChopName,
                                     GoodsChopCode = tbGoodsChop.GoodsChopCode,
                                 }).Single();
                return Json(GoodsChop, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 商品分类选择
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectGoodsClassifyID(BsgridPage bsgridPage)
        {
            var items = from tbGoodsClassify in MyModels.B_GoodsClassifyList
                        
                        orderby tbGoodsClassify.GoodsClassifyID
                        where tbGoodsClassify.F_GoodsClassifyID == 0
                        select new Vo.Goods
                        {
                            GoodsClassifyID = tbGoodsClassify.GoodsClassifyID,
                            Code = tbGoodsClassify.Code,
                            GoodsClassifyName = tbGoodsClassify.GoodsClassifyName,                                                      
                        };
            int intTotalRow = items.Count();//总行数
            List<Goods> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取商品分类
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuGoodsClassify(int GPID)
        {
            if (GPID > 0)
            {
                var ListGoodsClassifyName = (from tbEmp in MyModels.B_GoodsClassifyList
                                 where tbEmp.GoodsClassifyID == GPID
                                 select new Vo.Goods
                                 {
                                     GoodsClassifyID = tbEmp.GoodsClassifyID,
                                     GoodsClassifyName = tbEmp.GoodsClassifyName,
                                     Code = tbEmp.Code,                                     
                                 }).Single();
                return Json(ListGoodsClassifyName, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 选择采购进价
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult MingXiCaiGouJinJia(BsgridPage bsgridPage)
        {
            var listOrderFormPact = from tbGoods in MyModels.B_PurchaseList
                                    orderby tbGoods.PurchaseID
                                    select new Vo.Goods
                                    {
                                        PurchaseID = tbGoods.PurchaseID,
                                        PurchasingAgent = tbGoods.PurchasingAgent,
                                        LastBid = tbGoods.LastBid,
                                    };
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<Goods> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取采购进价
        /// </summary>
        /// <param name="shangpinID"></param>
        /// <returns></returns>
        public ActionResult HuoQuMingXiCaiGouJinJia(int caigouID)
        {
            if (caigouID > 0)
            {
                var HeTong = (from tbGoods in MyModels.B_PurchaseList
                              where tbGoods.PurchaseID == caigouID
                              select new Vo.Goods
                              {
                                  PurchaseID = tbGoods.PurchaseID,
                                  PurchasingAgent = tbGoods.PurchasingAgent,
                                  LastBid = tbGoods.LastBid,
                              }).Single();
                return Json(HeTong, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        //修改查询下拉框
        /// <summary>
        /// 类型条码
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBarCodTypeID1()
        {
            List<SelectVo> listBarCodType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listBarCodType.Add(selectVo);

            List<SelectVo> listBarCodTypeID = (from tbBarCodType in MyModels.S_BarCodTypeList
                                               select new SelectVo
                                               {
                                                   id = tbBarCodType.BarCodTypeID,
                                                   text = tbBarCodType.BarCodTypeName.Trim()
                                               }).ToList();

            listBarCodType.AddRange(listBarCodTypeID);

            return Json(listBarCodType, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCommodityTypeID1()
        {

            List<SelectVo> listCommodityTypeID = (from tbCommodityType in MyModels.S_CommodityTypeList
                                                  where tbCommodityType.CommodityTypeID == 1
                                                  select new SelectVo
                                                  {
                                                      id = tbCommodityType.CommodityTypeID,
                                                      text = tbCommodityType.CommodityType.Trim()
                                                  }).ToList();

            return Json(listCommodityTypeID, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品标记
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsStampID1()
        {
            List<SelectVo> listGoodsStamp = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsStamp.Add(selectVo);

            List<SelectVo> listGoodsStampID = (from tbGoodsStamp in MyModels.S_GoodsStampList
                                               select new SelectVo
                                               {
                                                   id = tbGoodsStamp.GoodsStampID,
                                                   text = tbGoodsStamp.GoodsStampName.Trim()
                                               }).ToList();

            listGoodsStamp.AddRange(listGoodsStampID);

            return Json(listGoodsStamp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品颜色
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsColoursID1()
        {
            List<SelectVo> listGoodsStamp = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsStamp.Add(selectVo);

            List<SelectVo> listGoodsStampID = (from tbGoodsStamp in MyModels.S_GoodsColoursList
                                               select new SelectVo
                                               {
                                                   id = tbGoodsStamp.GoodsColoursID,
                                                   text = tbGoodsStamp.GoodsColours.Trim()
                                               }).ToList();

            listGoodsStamp.AddRange(listGoodsStampID);

            return Json(listGoodsStamp, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询计量单位
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectEstimateUnitName1()
        {
            List<SelectVo> listEstimateUnit = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listEstimateUnit.Add(selectVo);

            List<SelectVo> listEstimateUnitID = (from tbEstimateUnit in MyModels.S_EstimateUnitList
                                                 select new SelectVo
                                                 {
                                                     id = tbEstimateUnit.EstimateUnitID,
                                                     text = tbEstimateUnit.EstimateUnitName.Trim()
                                                 }).ToList();

            listEstimateUnit.AddRange(listEstimateUnitID);

            return Json(listEstimateUnit, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 使用对象
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectApplyTargetID1()
        {
            List<SelectVo> listApplyTarget = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listApplyTarget.Add(selectVo);

            List<SelectVo> listApplyTargetID = (from tbApplyTarget in MyModels.S_ApplyTargetList
                                                select new SelectVo
                                                {
                                                    id = tbApplyTarget.ApplyTargetID,
                                                    text = tbApplyTarget.ApplyTarget.Trim()
                                                }).ToList();

            listApplyTarget.AddRange(listApplyTargetID);

            return Json(listApplyTarget, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 来源地点
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectSourceID1()
        {
            List<SelectVo> listSource = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listSource.Add(selectVo);

            List<SelectVo> listSourceID = (from tbSource in MyModels.B_SourceList
                                           select new SelectVo
                                           {
                                               id = tbSource.SourceID,
                                               text = tbSource.SourceName.Trim()
                                           }).ToList();

            listSource.AddRange(listSourceID);

            return Json(listSource, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品经营季节
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsSeasonID1()
        {
            List<SelectVo> listGoodsSeason = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsSeason.Add(selectVo);

            List<SelectVo> listGoodsSeasonID = (from tbGoodsSeason in MyModels.S_GoodsSeasonList
                                                select new SelectVo
                                                {
                                                    id = tbGoodsSeason.GoodsSeasonID,
                                                    text = tbGoodsSeason.GoodsSeasonName.Trim()
                                                }).ToList();

            listGoodsSeason.AddRange(listGoodsSeasonID);

            return Json(listGoodsSeason, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsStatusID1()
        {
            List<SelectVo> listGoodsStatus = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsStatus.Add(selectVo);

            List<SelectVo> listGoodsStatusID = (from tbGoodsStatus in MyModels.S_GoodsStatusIList
                                                select new SelectVo
                                                {
                                                    id = tbGoodsStatus.GoodsStatusID,
                                                    text = tbGoodsStatus.GoodsStatusName.Trim()
                                                }).ToList();

            listGoodsStatus.AddRange(listGoodsStatusID);

            return Json(listGoodsStatus, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 负库存销售状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectBurdenStockSellStatusID1()
        {
            List<SelectVo> listBurdenStockSellStatus = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listBurdenStockSellStatus.Add(selectVo);

            List<SelectVo> listBurdenStockSellStatusID = (from tbBurdenStockSellStatus in MyModels.S_BurdenStockSellStatusList
                                                          select new SelectVo
                                                          {
                                                              id = tbBurdenStockSellStatus.BurdenStockSellStatusID,
                                                              text = tbBurdenStockSellStatus.BurdenStockSellStatusName.Trim()
                                                          }).ToList();

            listBurdenStockSellStatus.AddRange(listBurdenStockSellStatusID);

            return Json(listBurdenStockSellStatus, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询全部商品
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ChaXunQuanBuShangPinXinXi(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                             join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                             join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID 
                             join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                             where tbGoods.CommodityTypeID == 1
                             orderby tbGoodsDetail.GoodsDetailID
                             select new Vo.Goods
                             {
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsName = tbGoods.GoodsName,
                                 SpellCod = tbGoods.SpellCod,
                                 GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 SupplierID = tbSupplier.SupplierID,
                                 SupplierName = tbSupplier.SupplierName,
                                 ProducingArea = tbGoods.ProducingArea,
                                 SpecificationsModel = tbGoods.SpecificationsModel,
                                 EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                 EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                 Registrant = tbGoods.Registrant,
                                 ReleaseTimeStr = tbGoods.RegisterTime.ToString(),
                                 Auditor = tbGoods.Auditor,
                                 ReleaseTimeStrr = tbGoods.Checktime.ToString(), 
                                 AuditingBit = tbGoods.AuditingBit,
                             });

            int intTotalRow = listGoods.Count();//总行数
            List<Goods> notices = listGoods.OrderByDescending(p => p.GoodsDetailID).//noboer表达式
               Skip(bsgridPage.GetStartIndex()).//F12（看）
               Take(bsgridPage.pageSize).
               ToList();
            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = notices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询修改数据
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectXiuGaiShenHeFou(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                             join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                             join tbGoodsBarCod in MyModels.S_BarCodTypeList on tbGoods.BarCodTypeID equals tbGoodsBarCod.BarCodTypeID
                             join tbGoodsStamp in MyModels.S_GoodsStampList on tbGoods.GoodsStampID equals tbGoodsStamp.GoodsStampID
                             join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbManufacturer in MyModels.B_ManufacturerList on tbGoods.ManufacturerID equals tbManufacturer.ManufacturerID
                             join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID
                             join tbGoodsClassify in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodsClassify.GoodsClassifyID
                             join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                             join tbApplyTarget in MyModels.S_ApplyTargetList on tbGoods.ApplyTargetID equals tbApplyTarget.ApplyTargetID
                             join tbGoodsSeason in MyModels.S_GoodsSeasonList on tbGoods.GoodsSeasonID equals tbGoodsSeason.GoodsSeasonID
                             join tbSource in MyModels.B_SourceList on tbGoods.SourceID equals tbSource.SourceID
                             join tbGoodsStatus in MyModels.S_GoodsStatusIList on tbGoods.GoodsStatusID equals tbGoodsStatus.GoodsStatusID
                             join tbBurdenStockSellStatus in MyModels.S_BurdenStockSellStatusList on tbGoods.BurdenStockSellStatusID equals tbBurdenStockSellStatus.BurdenStockSellStatusID
                             join tbCommodityType in MyModels.S_CommodityTypeList on tbGoods.CommodityTypeID equals tbCommodityType.CommodityTypeID
                             join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                             join tbGoodsColours in MyModels.S_GoodsColoursList on tbGoods.GoodsColoursID equals tbGoodsColours.GoodsColoursID
                             join tbDetail in MyModels.S_PackInfoIDList on tbGoodsDetail.GoodsDetailID equals tbDetail.GoodsDetailID
                             join tbPackInfo in MyModels.S_PackInfoIDList on tbDetail.PackInfoID equals tbPackInfo.PackInfoID

                             where tbGoods.CommodityTypeID==1 && tbGoods.AuditingBit ==false
                             orderby tbGoodsDetail.GoodsDetailID
                             select new Vo.Goods
                             { 
                                 //商品表
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsCode = tbGoods.GoodsCode,
                                 PLUCod = tbGoods.PLUCod,
                                 GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                 BarCodTypeID = tbGoodsBarCod.BarCodTypeID,
                                 BarCodTypeName = tbGoodsBarCod.BarCodTypeName,
                                 GoodsName = tbGoods.GoodsName,
                                 SpellCod = tbGoods.SpellCod,
                                 GoodsStampID = tbGoodsStamp.GoodsStampID,
                                 GoodsStampName = tbGoodsStamp.GoodsStampName,
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 ContractNumber = tbOrderFormPact.ContractNumber,
                                 SupplierID = tbSupplier.SupplierID,
                                 SupplierName = tbSupplier.SupplierName,
                                 ManufacturerID = tbManufacturer.ManufacturerID,
                                 ManufacturerName = tbManufacturer.ManufacturerName,
                                 GoodsChopID = tbGoodsChop.GoodsChopID,
                                 GoodsChopName = tbGoodsChop.GoodsChopName,
                                 ProducingArea = tbGoods.ProducingArea,
                                 GoodsClassifyID = tbGoodsClassify.GoodsClassifyID,
                                 GoodsClassifyName = tbGoodsClassify.GoodsClassifyName,
                                 ArtNo = tbGoods.ArtNo,
                                 QualityGuaranteePeriod = tbGoods.QualityGuaranteePeriod,
                                 StatisticsScale = tbGoods.StatisticsScale,
                                 SpecificationsModel = tbGoods.SpecificationsModel,
                                 QualityContent = tbGoods.QualityContent,
                                 EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                 EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                 ApplyTargetID = tbApplyTarget.ApplyTargetID,
                                 ApplyTarget = tbApplyTarget.ApplyTarget,
                                 GoodsSeasonID = tbGoodsSeason.GoodsSeasonID,
                                 GoodsSeasonName = tbGoodsSeason.GoodsSeasonName,
                                 SourceID = tbSource.SourceID,
                                 SourceName = tbSource.SourceName,
                                 GoodsColoursID = tbGoodsColours.GoodsColoursID,
                                 GoodsColours = tbGoodsColours.GoodsColours,
                                 AdvanceCess = tbGoods.AdvanceCess,
                                 SellCess = tbGoods.SellCess,
                                 Remarks = tbGoods.Remarks,
                                 AllowGoodsXiaoShu = tbGoods.AllowGoodsXiaoShu,
                                 Registrant = tbGoods.Registrant,
                                 ReleaseTimeStr = tbGoods.RegisterTime.ToString(),
                                 Auditor = tbGoods.Auditor,
                                 ReleaseTimeStrr = tbGoods.Checktime.ToString(),
                                 GoodsStatusID = tbGoodsStatus.GoodsStatusID,
                                 GoodsStatusName = tbGoodsStatus.GoodsStatusName,
                                 BurdenStockSellStatusID = tbBurdenStockSellStatus.BurdenStockSellStatusID,
                                 BurdenStockSellStatusName = tbBurdenStockSellStatus.BurdenStockSellStatusName,
                                 AuditingBit = tbGoods.AuditingBit,
                                 CommodityTypeID=tbCommodityType.CommodityTypeID,
                                 CommodityType = tbCommodityType.CommodityType,
                                 GoodsPicture = tbGoods.GoodsPicture,
                                 //商品明细表
                                 GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 PurchaseID = tbPurchase.PurchaseID,
                                 PurchasingAgent = tbPurchase.PurchasingAgent,
                                 GoodsDetailName = tbGoodsDetail.GoodsDetailName,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 GoodsFixAPrice = tbGoodsDetail.GoodsFixAPrice,
                                 ReferencePrice = tbGoodsDetail.ReferencePrice,
                                 MemberPrice = tbGoodsDetail.MemberPrice,
                                 RateSale = tbGoodsDetail.RateSale,
                                 TaxBid = tbGoodsDetail.TaxBid,
                                 //包装表
                                 PackInfoID = tbPackInfo.PackInfoID,
                                 PackContent = tbPackInfo.PackContent,
                                 PackWeight = tbPackInfo.PackWeight,
                                 DefaultPack = tbPackInfo.DefaultPack,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<Goods> notices = listGoods.OrderByDescending(p => p.GoodsDetailID).//noboer表达式
               Skip(bsgridPage.GetStartIndex()).//F12（看）
               Take(bsgridPage.pageSize).
               ToList();
            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = notices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }        

        /// <summary>
        /// 绑定修改数据
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShuJuXiuGaiShenHe(int GID)
        {
            if (GID > 0 )                       
            {
                var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                 join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                 join tbGoodsBarCod in MyModels.S_BarCodTypeList on tbGoods.BarCodTypeID equals tbGoodsBarCod.BarCodTypeID
                                 join tbGoodsStamp in MyModels.S_GoodsStampList on tbGoods.GoodsStampID equals tbGoodsStamp.GoodsStampID
                                 join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                                 join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                                 join tbManufacturer in MyModels.B_ManufacturerList on tbGoods.ManufacturerID equals tbManufacturer.ManufacturerID
                                 join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID
                                 join tbGoodsClassify in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodsClassify.GoodsClassifyID
                                 join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                                 join tbApplyTarget in MyModels.S_ApplyTargetList on tbGoods.ApplyTargetID equals tbApplyTarget.ApplyTargetID
                                 join tbGoodsSeason in MyModels.S_GoodsSeasonList on tbGoods.GoodsSeasonID equals tbGoodsSeason.GoodsSeasonID
                                 join tbSource in MyModels.B_SourceList on tbGoods.SourceID equals tbSource.SourceID
                                 join tbGoodsStatus in MyModels.S_GoodsStatusIList on tbGoods.GoodsStatusID equals tbGoodsStatus.GoodsStatusID
                                 join tbBurdenStockSellStatus in MyModels.S_BurdenStockSellStatusList on tbGoods.BurdenStockSellStatusID equals tbBurdenStockSellStatus.BurdenStockSellStatusID
                                 join tbCommodityType in MyModels.S_CommodityTypeList on tbGoods.CommodityTypeID equals tbCommodityType.CommodityTypeID
                                 join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                                 join tbGoodsColours in MyModels.S_GoodsColoursList on tbGoods.GoodsColoursID equals tbGoodsColours.GoodsColoursID
                                 join tbDetail in MyModels.S_PackInfoIDList on tbGoodsDetail.GoodsDetailID equals tbDetail.GoodsDetailID
                                 join tbPackInfo in MyModels.S_PackInfoIDList on tbDetail.PackInfoID equals tbPackInfo.PackInfoID

                                 where tbGoodsDetail.GoodsDetailID == GID
                                 where tbGoods.AuditingBit == false
                                 select new Vo.Goods
                                 {
                                     //商品表
                                     GoodsID = tbGoods.GoodsID,
                                     GoodsCode = tbGoods.GoodsCode,
                                     PLUCod = tbGoods.PLUCod,
                                     GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                     BarCodTypeID = tbGoodsBarCod.BarCodTypeID,
                                     BarCodTypeName = tbGoodsBarCod.BarCodTypeName,
                                     GoodsName = tbGoods.GoodsName,
                                     SpellCod = tbGoods.SpellCod,
                                     GoodsStampID = tbGoodsStamp.GoodsStampID,
                                     GoodsStampName = tbGoodsStamp.GoodsStampName,
                                     OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                     ContractNumber = tbOrderFormPact.ContractNumber,
                                     SupplierID = tbSupplier.SupplierID,
                                     SupplierName = tbSupplier.SupplierName,
                                     ManufacturerID = tbManufacturer.ManufacturerID,
                                     ManufacturerName = tbManufacturer.ManufacturerName,
                                     GoodsChopID = tbGoodsChop.GoodsChopID,
                                     GoodsChopName = tbGoodsChop.GoodsChopName,
                                     ProducingArea = tbGoods.ProducingArea,
                                     GoodsClassifyID = tbGoodsClassify.GoodsClassifyID,
                                     GoodsClassifyName = tbGoodsClassify.GoodsClassifyName,
                                     ArtNo = tbGoods.ArtNo,
                                     QualityGuaranteePeriod = tbGoods.QualityGuaranteePeriod,
                                     StatisticsScale = tbGoods.StatisticsScale,
                                     SpecificationsModel = tbGoods.SpecificationsModel,
                                     QualityContent = tbGoods.QualityContent,
                                     EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                     EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                     ApplyTargetID = tbApplyTarget.ApplyTargetID,
                                     ApplyTarget = tbApplyTarget.ApplyTarget,
                                     GoodsSeasonID = tbGoodsSeason.GoodsSeasonID,
                                     GoodsSeasonName = tbGoodsSeason.GoodsSeasonName,
                                     SourceID = tbSource.SourceID,
                                     SourceName = tbSource.SourceName,
                                     GoodsColoursID = tbGoodsColours.GoodsColoursID,
                                     GoodsColours = tbGoodsColours.GoodsColours,
                                     AdvanceCess = tbGoods.AdvanceCess,
                                     SellCess = tbGoods.SellCess,
                                     Remarks = tbGoods.Remarks,
                                     AllowGoodsXiaoShu = tbGoods.AllowGoodsXiaoShu,
                                     Registrant = tbGoods.Registrant,
                                     ReleaseTimeStr = tbGoods.RegisterTime.ToString(),
                                     Auditor = tbGoods.Auditor,
                                     ReleaseTimeStrr = tbGoods.Checktime.ToString(),
                                     GoodsStatusID = tbGoodsStatus.GoodsStatusID,
                                     GoodsStatusName = tbGoodsStatus.GoodsStatusName,
                                     BurdenStockSellStatusID = tbBurdenStockSellStatus.BurdenStockSellStatusID,
                                     BurdenStockSellStatusName = tbBurdenStockSellStatus.BurdenStockSellStatusName,
                                     AuditingBit = tbGoods.AuditingBit,
                                     CommodityTypeID = tbCommodityType.CommodityTypeID,
                                     CommodityType = tbCommodityType.CommodityType,
                                     GoodsPicture = tbGoods.GoodsPicture,
                                     //商品明细表
                                     GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                     PurchaseID = tbPurchase.PurchaseID,
                                     PurchasingAgent = tbPurchase.PurchasingAgent,
                                     GoodsDetailName = tbGoodsDetail.GoodsDetailName,
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                     GoodsFixAPrice = tbGoodsDetail.GoodsFixAPrice,
                                     ReferencePrice = tbGoodsDetail.ReferencePrice,
                                     MemberPrice = tbGoodsDetail.MemberPrice,
                                     RateSale = tbGoodsDetail.RateSale,
                                     TaxBid = tbGoodsDetail.TaxBid,
                                     //包装表
                                     PackInfoID = tbPackInfo.PackInfoID,
                                     PackContent = tbPackInfo.PackContent,
                                     PackWeight = tbPackInfo.PackWeight,
                                     DefaultPack = tbPackInfo.DefaultPack,
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }            
        }

        /// <summary>
        /// 查询商品照片
        /// </summary>
        /// <param name="Goodsid"></param>
        /// <returns></returns>
        public ActionResult GetGoodsImage(int GID)
        {
            try
            {
                var GoodsImg = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                where tbGoodsDetail.GoodsDetailID == GID
                                select new
                                {
                                    tbGoods.GoodsPicture,
                                }).Single();
                byte[] imageData = GoodsImg.GoodsPicture;
                return File(imageData, @"image/jpg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
       
        /// <summary>
        /// 新增修改保存
        /// </summary>
        /// <param name="pwGoods"></param>
        /// <param name="fileImage"></param>
        /// <returns></returns>
        public ActionResult btnInsertUpdate(B_GoodsList pwGoods, HttpPostedFileBase fileImage , B_GoodsDetailList pwGoodsDetail , S_PackInfoIDList pwpackInfo)
        {
            string strMsg = "fali";
            try
            {
                
                //判断数据是否已经存在
                int oldGoodsRows = (from tbGoods in MyModels.B_GoodsList
                                    where tbGoods.GoodsCode == pwGoods.GoodsCode
                                    select tbGoods).Count();

                if (oldGoodsRows == 0)
                {
                    //商品表
                    B_GoodsList Dep = new B_GoodsList();

                    //判断是否上传图片
                    if (fileImage != null && fileImage.ContentLength > 0)
                    {
                        //读取上传的图片 初始化为图片的长度 将图片转为流 将流读取为byte[]，参数：byte[]，读取开始位置，读取结束位置
                        byte[] imgFiles = new byte[fileImage.ContentLength];
                        fileImage.InputStream.Read(imgFiles, 0, fileImage.ContentLength);
                        Dep.GoodsPicture = imgFiles;
                    }

                    Dep.GoodsCode = Request.Form["GoodsCode"];
                    Dep.PLUCod = Request.Form["PLUCod"];
                    Dep.GoodsTiaoMa = Request.Form["GoodsTiaoMa"];
                    Dep.BarCodTypeID = Convert.ToInt32(Request.Form["BarCodTypeID"]);
                    Dep.GoodsName = Request.Form["GoodsName"];
                    Dep.SpellCod = Request.Form["SpellCod"];
                    Dep.GoodsStampID = Convert.ToInt32(Request.Form["GoodsStampID"]);
                    Dep.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                    Dep.SupplierID = Convert.ToInt32(Request.Form["SupplierID"]);
                    Dep.ManufacturerID = Convert.ToInt32(Request.Form["ManufacturerID"]);
                    Dep.GoodsChopID = Convert.ToInt32(Request.Form["GoodsChopID"]);
                    Dep.ProducingArea = Request.Form["ProducingArea"];
                    Dep.GoodsClassifyID = Convert.ToInt32(Request.Form["GoodsClassifyID"]);
                    Dep.ArtNo = Request.Form["ArtNo"];
                    Dep.QualityGuaranteePeriod = Request.Form["QualityGuaranteePeriod"];
                    Dep.StatisticsScale = Convert.ToDecimal(Request.Form["StatisticsScale"]);
                    Dep.SpecificationsModel = Request.Form["SpecificationsModel"];
                    Dep.QualityContent = Request.Form["QualityContent"];
                    Dep.EstimateUnitID = Convert.ToInt32(Request.Form["EstimateUnitID"]);
                    Dep.ApplyTargetID = Convert.ToInt32(Request.Form["ApplyTargetID"]);
                    Dep.GoodsSeasonID = Convert.ToInt32(Request.Form["GoodsSeasonID"]);
                    Dep.SourceID = Convert.ToInt32(Request.Form["SourceID"]);
                    Dep.GoodsColoursID = Convert.ToInt32(Request.Form["GoodsColoursID"]);
                    Dep.AdvanceCess = Convert.ToDecimal(Request.Form["AdvanceCess"]);
                    Dep.SellCess = Convert.ToDecimal(Request.Form["SellCess"]);
                    Dep.Remarks = Request.Form["Remarks"];
                    Dep.AllowGoodsXiaoShu = pwGoods.AllowGoodsXiaoShu;
                    Dep.AuditingBit = pwGoods.AuditingBit;
                    Dep.Registrant = Request.Form["Registrant"];
                    Dep.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                    Dep.Auditor = Request.Form["Auditor"];
                    Dep.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                    Dep.GoodsStatusID = Convert.ToInt32(Request.Form["GoodsStatusID"]);
                    Dep.BurdenStockSellStatusID = Convert.ToInt32(Request.Form["BurdenStockSellStatusID"]);
                    Dep.CommodityTypeID = Convert.ToInt32(Request.Form["CommodityTypeID"]);
                    if (Dep.GoodsName != null && Dep.OrderFormPactID != null && Dep.OrderFormPactID != null && Dep.SupplierID != null
                        && Dep.ManufacturerID != null && Dep.GoodsChopID != null && Dep.GoodsClassifyID != null && Dep.ProducingArea != null
                        && Dep.StatisticsScale != null && Dep.AdvanceCess != null && Dep.SellCess != null && Dep.BarCodTypeID != null && Dep.GoodsStampID != null
                        && Dep.EstimateUnitID != null && Dep.ApplyTargetID != null && Dep.GoodsSeasonID != null && Dep.SourceID != null
                        && Dep.GoodsStatusID != null && Dep.BurdenStockSellStatusID != null && Dep.RegisterTime != null && Dep.Checktime != null && Dep.GoodsPicture != null)
                    {
                        MyModels.B_GoodsList.Add(Dep);
                        MyModels.SaveChanges();

                        //商品明细
                        B_GoodsDetailList KK = new B_GoodsDetailList();
                        KK.GoodsID = Dep.GoodsID;
                        KK.PurchaseID = Convert.ToInt32(Request.Form["PurchaseID"]);
                        KK.GoodsDetailName = Request.Form["GoodsDetailName"];
                        KK.RetailMonovalent = Convert.ToDecimal(Request.Form["RetailMonovalent"]);
                        KK.GoodsFixAPrice = Convert.ToDecimal(Request.Form["GoodsFixAPrice"]);
                        KK.ReferencePrice = Convert.ToDecimal(Request.Form["ReferencePrice"]); 
                        KK.MemberPrice = Convert.ToDecimal(Request.Form["MemberPrice"]);
                        KK.RateSale = Convert.ToDecimal(Request.Form["RateSale"]);
                        KK.TaxBid = Convert.ToDecimal(Request.Form["TaxBid"]);
                        if (KK.GoodsID != null && KK.PurchaseID !=null && KK.GoodsDetailName != null && KK.RetailMonovalent != null && KK.GoodsFixAPrice != null
                            && KK.ReferencePrice != null && KK.MemberPrice != null && KK.RateSale != null && KK.TaxBid != null) 
                        {
                            MyModels.B_GoodsDetailList.Add(KK);
                            MyModels.SaveChanges();

                            //包装表
                            S_PackInfoIDList QQ = new S_PackInfoIDList();
                            QQ.GoodsDetailID = KK.GoodsDetailID;
                            QQ.PackContent = Request.Form["PackContent"];
                            QQ.PackWeight = Request.Form["PackContent"];
                            QQ.DefaultPack = pwpackInfo.DefaultPack;

                            if (QQ.GoodsDetailID != null && QQ.PackContent != null && QQ.PackWeight != null)
                            {
                                MyModels.S_PackInfoIDList.Add(QQ);
                                MyModels.SaveChanges();
                            }

                        }

                    }

                    strMsg = "succsee";
                }               
                
                if (pwGoods.GoodsID > 0)
                {
                    //商品表
                    B_GoodsList Dep = new B_GoodsList();

                    if (fileImage != null && fileImage.ContentLength > 0)
                    {
                        //读取上传的图片 初始化为图片的长度 将图片转为流 将流读取为byte[]，参数：byte[]，读取开始位置，读取结束位置
                        byte[] imgFiles = new byte[fileImage.ContentLength];
                        fileImage.InputStream.Read(imgFiles, 0, fileImage.ContentLength);
                        Dep.GoodsPicture = imgFiles;
                    }

                    Dep.GoodsID = Convert.ToInt32(Request.Form["GoodsID"]);
                    Dep.GoodsCode = Request.Form["GoodsCode"];
                    Dep.PLUCod = Request.Form["PLUCod"];
                    Dep.GoodsTiaoMa = Request.Form["GoodsTiaoMa"];
                    Dep.BarCodTypeID = Convert.ToInt32(Request.Form["BarCodTypeID"]);
                    Dep.GoodsName = Request.Form["GoodsName"];
                    Dep.SpellCod = Request.Form["SpellCod"];
                    Dep.GoodsStampID = Convert.ToInt32(Request.Form["GoodsStampID"]);
                    Dep.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);
                    Dep.SupplierID = Convert.ToInt32(Request.Form["SupplierID"]);
                    Dep.ManufacturerID = Convert.ToInt32(Request.Form["ManufacturerID"]);
                    Dep.GoodsChopID = Convert.ToInt32(Request.Form["GoodsChopID"]);
                    Dep.ProducingArea = Request.Form["ProducingArea"];
                    Dep.GoodsClassifyID = Convert.ToInt32(Request.Form["GoodsClassifyID"]);
                    Dep.ArtNo = Request.Form["ArtNo"];
                    Dep.QualityGuaranteePeriod = Request.Form["QualityGuaranteePeriod"];
                    Dep.StatisticsScale = Convert.ToDecimal(Request.Form["StatisticsScale"]);
                    Dep.SpecificationsModel = Request.Form["SpecificationsModel"];
                    Dep.QualityContent = Request.Form["QualityContent"]; 
                    Dep.EstimateUnitID = Convert.ToInt32(Request.Form["EstimateUnitID"]);
                    Dep.ApplyTargetID = Convert.ToInt32(Request.Form["ApplyTargetID"]);
                    Dep.GoodsSeasonID = Convert.ToInt32(Request.Form["GoodsSeasonID"]);
                    Dep.SourceID = Convert.ToInt32(Request.Form["SourceID"]);
                    Dep.GoodsColoursID = Convert.ToInt32(Request.Form["GoodsColoursID"]);
                    Dep.AdvanceCess = Convert.ToDecimal(Request.Form["AdvanceCess"]);
                    Dep.SellCess = Convert.ToDecimal(Request.Form["SellCess"]);
                    Dep.Remarks = Request.Form["Remarks"];
                    Dep.AllowGoodsXiaoShu = pwGoods.AllowGoodsXiaoShu;
                    Dep.AuditingBit = pwGoods.AuditingBit;
                    Dep.Registrant = Request.Form["Registrant"];
                    //Dep.RegisterTime = Convert.ToDateTime(Request.Form["RegisterTime"]);
                    Dep.Auditor = Request.Form["Auditor"];
                    //Dep.Checktime = Convert.ToDateTime(Request.Form["Checktime"]);
                    Dep.GoodsStatusID = Convert.ToInt32(Request.Form["GoodsStatusID"]);
                    Dep.BurdenStockSellStatusID = Convert.ToInt32(Request.Form["BurdenStockSellStatusID"]);
                    Dep.CommodityTypeID = Convert.ToInt32(Request.Form["CommodityTypeID"]);

                    MyModels.Entry(Dep).State = System.Data.Entity.EntityState.Modified;
                    MyModels.SaveChanges();

                    //商品明细
                    B_GoodsDetailList KK = new B_GoodsDetailList();
                    KK.GoodsID = Dep.GoodsID;
                    KK.GoodsDetailID = pwGoodsDetail.GoodsDetailID;
                    KK.PurchaseID = Convert.ToInt32(Request.Form["PurchaseID"]);
                    KK.GoodsDetailName = Request.Form["GoodsDetailName"];
                    KK.RetailMonovalent = Convert.ToDecimal(Request.Form["RetailMonovalent"]);
                    KK.GoodsFixAPrice = Convert.ToDecimal(Request.Form["GoodsFixAPrice"]);
                    KK.ReferencePrice = Convert.ToDecimal(Request.Form["ReferencePrice"]);
                    KK.MemberPrice = Convert.ToDecimal(Request.Form["MemberPrice"]);
                    KK.RateSale = Convert.ToDecimal(Request.Form["RateSale"]); 
                    KK.TaxBid = Convert.ToDecimal(Request.Form["TaxBid"]);

                    MyModels.Entry(KK).State = System.Data.Entity.EntityState.Modified;
                    MyModels.SaveChanges();

                    //包装含量
                    S_PackInfoIDList QQ = new S_PackInfoIDList();
                    QQ.GoodsDetailID = KK.GoodsDetailID;
                    QQ.PackInfoID = Convert.ToInt32(Request.Form["PackInfoID"]);
                    QQ.DefaultPack = pwpackInfo.DefaultPack;
                    QQ.PackContent = Request.Form["PackContent"];
                    QQ.PackWeight = Request.Form["PackContent"];

                    MyModels.Entry(QQ).State = System.Data.Entity.EntityState.Modified;
                    MyModels.SaveChanges();

                    strMsg = "succsee";
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                strMsg = "exsit";
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public ActionResult DeleteGoods(int goodsId)
        {  
            //定义返回
            string strMsg = "fail";
            try
            {
                var dbGoods = MyModels.B_GoodsList.Where(p => p.GoodsID == goodsId).Single();
                MyModels.B_GoodsList.Remove(dbGoods);

                int GoodsID = (int)dbGoods.GoodsID;
                //文件跟踪
                var dbGoodsDetail = MyModels.B_GoodsDetailList.Where(p => p.GoodsID == GoodsID).Single();
                MyModels.B_GoodsDetailList.Remove(dbGoodsDetail);

                int GoodsDetailID = (int)dbGoodsDetail.GoodsDetailID;
                //文件跟踪
                var dbPackInfoID = MyModels.S_PackInfoIDList.Where(p => p.GoodsDetailID == GoodsDetailID).Single();
                MyModels.S_PackInfoIDList.Remove(dbPackInfoID);

                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }               

        /// <summary>
        /// 查询证书资信书
        /// </summary>    
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectRegisteredDiploma(BsgridPage bsgridPage)
        {         
            try
            {
                var linqItem = from tbGoodsChop in MyModels.B_GoodsChopList
                               orderby tbGoodsChop.GoodsChopID
                               select new Vo.GoodsChop
                               {
                                   GoodsChopID = tbGoodsChop.GoodsChopID,
                                   RegisteredDiploma = tbGoodsChop.RegisteredDiploma,
                                   GoodsChopCode = tbGoodsChop.GoodsChopCode,
                                   ReleaseTimeStr = tbGoodsChop.DiplomaValid.ToString(),
                               };
                int totalRow = linqItem.Count();
                List<GoodsChop> UserTypeVos = linqItem.OrderByDescending(p => p.GoodsChopID).
                    Skip(bsgridPage.GetStartIndex()).
                    Take(bsgridPage.pageSize).
                    ToList();

                Bsgrid<GoodsChop> bsgrid = new Bsgrid<GoodsChop>();
                bsgrid.success = true;
                bsgrid.totalRows = totalRow;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.data = UserTypeVos;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("shibai", JsonRequestBehavior.AllowGet);
            }

        }

        #endregion        

        #region 自营商品状态变更      
        public ActionResult ShangPinZhuangTaiBianGeng()
        {
            return View();
        }
        /// <summary>
        /// 查询商品状态 条件
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsStatusID2()
        {
            List<SelectVo> listStatusID = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listStatusID.Add(selectVo);

            List<SelectVo> listStatusIDD = (from tbStatusID in MyModels.S_GoodsStatusIList
                                            where tbStatusID.GoodsStatusID != 1 && tbStatusID.GoodsStatusID != 2
                                            select new SelectVo
                                            {
                                                id = tbStatusID.GoodsStatusID,
                                                text = tbStatusID.GoodsStatusName
                                            }).ToList();

            listStatusID.AddRange(listStatusIDD);

            return Json(listStatusID, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 商品经营季节 条件
        /// </summary>
        /// <returns></returns>
        public ActionResult GSelectGoodsSeasonID()
        {
            List<SelectVo> listGoodsSeason = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsSeason.Add(selectVo);

            List<SelectVo> listGoodsSeasonID = (from tbGoodsSeason in MyModels.S_GoodsSeasonList
                                                select new SelectVo
                                                {
                                                    id = tbGoodsSeason.GoodsSeasonID,
                                                    text = tbGoodsSeason.GoodsSeasonName.Trim()
                                                }).ToList();

            listGoodsSeason.AddRange(listGoodsSeasonID);

            return Json(listGoodsSeason, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 条件查询商品
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="goodsCode"></param>
        /// <param name="goodsStatusID"></param>
        /// <returns></returns>
        public ActionResult SelectHuoQuGengGaiShuJu(BsgridPage bsgridPage, string goodsCode, int goodsStatusID, int goodsSeasonID)
        {
            var items = (from tbGoods in MyModels.B_GoodsList
                         join tbGoodsStatus in MyModels.S_GoodsStatusIList on tbGoods.GoodsStatusID equals tbGoodsStatus.GoodsStatusID /*商品状态*/
                         join tbGoodsSeason in MyModels.S_GoodsSeasonList on tbGoods.GoodsSeasonID equals tbGoodsSeason.GoodsSeasonID /*经营季节*/
                         join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID /*商标品牌*/
                         join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID /*计量单位*/
                         join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID /*零售单价*/
                         where tbGoods.CommodityTypeID == 1
                         orderby tbGoods.GoodsID
                         select new Vo.Goods
                         {
                             GoodsID = tbGoods.GoodsID,
                             GoodsCode = tbGoods.GoodsCode,/*商品代码*/
                             GoodsName = tbGoods.GoodsName,/*商品名称*/
                             GoodsStatusID = tbGoodsStatus.GoodsStatusID,
                             GoodsStatusName = tbGoodsStatus.GoodsStatusName,/*商品状态*/
                             GoodsSeasonID = tbGoodsSeason.GoodsSeasonID,
                             GoodsSeasonName = tbGoodsSeason.GoodsSeasonName,/*经营季节*/
                             GoodsChopID = tbGoodsChop.GoodsChopID,
                             GoodsChopName = tbGoodsChop.GoodsChopName,/*商标品牌*/
                             EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                             EstimateUnitName = tbEstimateUnit.EstimateUnitName,/*计量单位*/
                             SpecificationsModel = tbGoods.SpecificationsModel,/*规格型号*/
                             RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/
                             GoodsTiaoMa = tbGoods.GoodsTiaoMa,/*商品条码*/
                         });
            //如果查询条件不为空
            if (!string.IsNullOrEmpty(goodsCode))
            {
                goodsCode = goodsCode.Trim();
                items = items.Where(p => p.GoodsCode.Contains(goodsCode));
            }
            if (goodsStatusID > 0)
            {
                items = items.Where(p => p.GoodsStatusID == goodsStatusID);
            }
            if (goodsSeasonID > 0)
            {
                items = items.Where(p => p.GoodsSeasonID == goodsSeasonID);
            }         
            int intTotalRow = items.Count();//总行数
            List<Goods> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改变更商品状态
        /// </summary>
        /// <param name="GBGID"></param>
        /// <returns></returns>
        public ActionResult GoodsXiuGaiBianGeng(int GoodsID)
        {
            //选中表格一条或多条数据进行状态变化并更新
            string strMsg = "";
            try
            {
                var ListGoods = (from tbGoods in MyModels.B_GoodsList
                             join tbGoodsStatus in MyModels.S_GoodsStatusIList on tbGoods.GoodsStatusID equals tbGoodsStatus.GoodsStatusID /*商品状态*/
                             where tbGoods.GoodsID == GoodsID
                             where tbGoodsStatus.GoodsStatusID != 1 && tbGoodsStatus.GoodsStatusID != 2 && tbGoodsStatus.GoodsStatusID != 6
                             select new
                             {
                                 GoodsID = tbGoods.GoodsID,                               
                                 GoodsStatusID = tbGoodsStatus.GoodsStatusID,
                                 GoodsStatusName = tbGoodsStatus.GoodsStatusName,/*商品状态*/                               
                             });
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);               
            }

            return Json(strMsg, JsonRequestBehavior.AllowGet);

           
        }

        /// <summary>
        /// 查询商品状态  变更状态类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectGoodsStatusID3()
        {
            List<SelectVo> listGoodsStatusID2 = (from tbGoodsStatusID2 in MyModels.S_GoodsStatusIList
                                                 where tbGoodsStatusID2.GoodsStatusID != 1 && tbGoodsStatusID2.GoodsStatusID != 2 && tbGoodsStatusID2.GoodsStatusID != 6
                                                 select new SelectVo
                                                 {
                                                     id = tbGoodsStatusID2.GoodsStatusID,
                                                     text = tbGoodsStatusID2.GoodsStatusName.Trim()
                                                 }).ToList();
            return Json(listGoodsStatusID2, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 自营商品清退        
        public ActionResult ZiYinShangPinQingTui()
        {
            return View();
        }
        /// <summary>
        /// 商品状态 查询条件
        /// </summary>
        /// <returns></returns>
        public ActionResult QSelectGoodsStatusID()
        {
            List<SelectVo> listGoodsStatusID1 = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsStatusID1.Add(selectVo);

            List<SelectVo> listGoodsStatusID = (from tbGoodsStatusID1 in MyModels.S_GoodsStatusIList
                                                where tbGoodsStatusID1.GoodsStatusID != 1 && tbGoodsStatusID1.GoodsStatusID != 2 && tbGoodsStatusID1.GoodsStatusID != 6
                                                select new SelectVo
                                                {
                                                    id = tbGoodsStatusID1.GoodsStatusID,
                                                    text = tbGoodsStatusID1.GoodsStatusName
                                                }).ToList();

            listGoodsStatusID1.AddRange(listGoodsStatusID);

            return Json(listGoodsStatusID1, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 商标品牌 查询条件
        /// </summary>
        /// <returns></returns>
        public ActionResult QSelectGoodsChopID()
        {
            List<SelectVo> listGoodsChop = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listGoodsChop.Add(selectVo);

            List<SelectVo> listGoodsChopID = (from tbGoodsChop in MyModels.B_GoodsChopList
                                              select new SelectVo
                                              {
                                                  id = tbGoodsChop.GoodsChopID,
                                                  text = tbGoodsChop.GoodsChopName.Trim()
                                              }).ToList();

            listGoodsChop.AddRange(listGoodsChopID);

            return Json(listGoodsChop, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 多条件查询表格数据
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="goodsCode"></param>
        /// <param name="goodsStatusID"></param>
        /// <param name="goodsChopID"></param>
        /// <returns></returns>
        public ActionResult SelectShangPinQingTui(BsgridPage bsgridPage, string goodsCode, int goodsStatusID, int goodsChopID)
        {
            var items = (from tbGoods in MyModels.B_GoodsList
                         join tbGoodsStatus in MyModels.S_GoodsStatusIList on tbGoods.GoodsStatusID equals tbGoodsStatus.GoodsStatusID /*商品状态*/
                         join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID /*商标品牌*/
                         join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID /*计量单位*/
                         join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID /*零售单价*/
                         where tbGoods.CommodityTypeID ==1
                         orderby tbGoods.GoodsID
                         select new Vo.Goods
                         {
                             GoodsID = tbGoods.GoodsID,
                             GoodsCode = tbGoods.GoodsCode,/*商品代码*/
                             GoodsChopID = tbGoodsChop.GoodsChopID,
                             GoodsStatusID = tbGoodsStatus.GoodsStatusID,
                             GoodsName = tbGoods.GoodsName,/*商品名称*/
                             GoodsStatusName = tbGoodsStatus.GoodsStatusName,/*商品状态*/
                             GoodsChopName = tbGoodsChop.GoodsChopName,/*商标品牌*/
                             EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                             EstimateUnitName = tbEstimateUnit.EstimateUnitName,/*计量单位*/
                             SpecificationsModel = tbGoods.SpecificationsModel,/*规格型号*/
                             RetailMonovalent = tbGoodsDetail.RetailMonovalent,/*零售单价*/
                             GoodsTiaoMa = tbGoods.GoodsTiaoMa,/*商品条码*/
                         });
            //如果查询条件不为空
            if (!string.IsNullOrEmpty(goodsCode))
            {
                goodsCode = goodsCode.Trim();
                items = items.Where(p => p.GoodsCode.Contains(goodsCode));
            }
            if (goodsChopID > 0)
            {
                items = items.Where(p => p.GoodsChopID == goodsChopID);
            }
            if (goodsStatusID > 0)
            {
                items = items.Where(p => p.GoodsStatusID == goodsStatusID);
            }

            int intTotalRow = items.Count();//总行数
            List<Goods> listNotices = items.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ZiYinShangPinQingTuiGaiBian()
        {

            return View();
        }



        #endregion

        #region 设置新商品销售柜台              
        public ActionResult XinShangPinXiaoShouGuiTai()
        {
            return View();
        }
        /// <summary>
        ///柜台位置
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectMarketID()
        {
            List<SelectVo> listMarketName = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listMarketName.Add(selectVo);

            List<SelectVo> listMarketID = (from tbMarketName in MyModels.S_MarketNameList
                                               select new SelectVo
                                               {
                                                   id = tbMarketName.MarketID,
                                                   text = tbMarketName.MarketName.Trim(),
                                               }).ToList();

            listMarketName.AddRange(listMarketID);

            return Json(listMarketName, JsonRequestBehavior.AllowGet);
           
        }

        /// <summary>
        /// 选择合同
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectShangPin(BsgridPage bsgridPage)
        {
            var listGoods = from tbGoodsDetail in MyModels.B_GoodsDetailList                          
                            join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID                          
                            join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                            join tbCommodityType in MyModels.S_CommodityTypeList on tbGoods.CommodityTypeID equals tbCommodityType.CommodityTypeID
                            where tbGoods.CommodityTypeID == 1
                            orderby tbGoodsDetail.GoodsDetailID
                            select new Vo.Goods
                            {
                                GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                GoodsCode = tbGoods.GoodsCode,
                                GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                GoodsName = tbGoods.GoodsName,
                                SpecificationsModel = tbGoods.SpecificationsModel,
                                EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                ArtNo = tbGoods.ArtNo,
                                RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                            };
            int intTotalRow = listGoods.Count();//总行数
            List<Goods> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取商品
        /// </summary>
        /// <param name="ShangPinID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPin(int ShangPinID)
        {
            if (ShangPinID > 0)
            {
                var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                 join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                 join tbEstimateUnit in MyModels.S_EstimateUnitList on tbGoods.EstimateUnitID equals tbEstimateUnit.EstimateUnitID
                                 join tbCommodityType in MyModels.S_CommodityTypeList on tbGoods.CommodityTypeID equals tbCommodityType.CommodityTypeID
                                 where tbGoodsDetail.GoodsDetailID == ShangPinID
                                 where tbGoods.CommodityTypeID == 1
                                 select new Vo.Goods
                                 {
                                     GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                     GoodsCode = tbGoods.GoodsCode,
                                     GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                     GoodsName = tbGoods.GoodsName,
                                     SpecificationsModel = tbGoods.SpecificationsModel,
                                     EstimateUnitID = tbEstimateUnit.EstimateUnitID,
                                     EstimateUnitName = tbEstimateUnit.EstimateUnitName,
                                     ArtNo = tbGoods.ArtNo,
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 新增新商品销售柜台
        /// </summary>
        /// <returns></returns>       
        public ActionResult XinZengXinShangPinGuiTai(B_NewGoodsManageList pwNewGoodsManage)
        {
            string strMsg = "fali";
            try
            {
                //判断数据是否已经存在
                int oldNewGoodsManageRows = (from tbNewGoodsManage in MyModels.B_NewGoodsManageList
                                             where tbNewGoodsManage.NewGoodsManageMC == pwNewGoodsManage.NewGoodsManageMC
                                             select tbNewGoodsManage).Count();
                if (oldNewGoodsManageRows == 0)
                {
                    B_NewGoodsManageList KK = new B_NewGoodsManageList();
                    KK.NewGoodsManageMC = Request.Form["NewGoodsManageMC"];
                    KK.MarketID = Convert.ToInt32(Request.Form["MarketID"]);
                    KK.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                    KK.You = Convert.ToBoolean(Request.Form["You"]);
                    KK.Wu = Convert.ToBoolean(Request.Form["Wu"]);
                    KK.XiaoShou = Convert.ToBoolean(Request.Form["XiaoShou"]);
                    KK.XiaoShouBuXiuGai = Convert.ToBoolean(Request.Form["XiaoShouBuXiuGai"]);
                    KK.BuXiaoShou = Convert.ToBoolean(Request.Form["BuXiaoShou"]);
                    KK.BuXiaoShouBuXiuGai = Convert.ToBoolean(Request.Form["BuXiaoShouBuXiuGai"]);
                   
                    if (KK.NewGoodsManageMC != null && KK.MarketID != null && KK.GoodsDetailID != null)
                    {
                        MyModels.B_NewGoodsManageList.Add(KK);
                        MyModels.SaveChanges();                       
                    }
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    strMsg = "fali";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
       
        #endregion

        #region 以销定进/负库存销售商品定义      
        public ActionResult FuKuCunXiaoShouShangPin()
        {
            return View();
        }
        /// <summary>
        /// 查询数据在表格中
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectFuKuCunXiaoSou(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoods in MyModels.B_GoodsList
                             join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbGoodsChop in MyModels.B_GoodsChopList on tbGoods.GoodsChopID equals tbGoodsChop.GoodsChopID
                             join tbBurdenStockSellStatus in MyModels.S_BurdenStockSellStatusList on tbGoods.BurdenStockSellStatusID equals tbBurdenStockSellStatus.BurdenStockSellStatusID
                             where tbGoods.CommodityTypeID == 1
                             //where tbBurdenStockSellStatus.BurdenStockSellStatusID == 6
                             orderby tbGoods.GoodsID
                             select new Vo.Goods                          
                             {
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsTiaoMa = tbGoods.GoodsTiaoMa,
                                 SupplierID = tbSupplier.SupplierID,
                                 SupplierName = tbSupplier.SupplierName,
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 ContractNumber = tbOrderFormPact.ContractNumber,
                                 GoodsChopID = tbGoodsChop.GoodsChopID,
                                 GoodsChopName = tbGoodsChop.GoodsChopName,
                                 BurdenStockSellStatusID = tbBurdenStockSellStatus.BurdenStockSellStatusID,
                                 BurdenStockSellStatusName = tbBurdenStockSellStatus.BurdenStockSellStatusName,
                                 BurdenStockDataNumber = tbBurdenStockSellStatus.BurdenStockDataNumber,
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<Goods> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 自营商品经营合同定义       
        public ActionResult ShangPinJingYinHeTong()
        {
            return View();
        }
        /// <summary>
        /// 商品信息选择
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ShangPinXinXiXuanZe(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoods in MyModels.B_GoodsList
                             join tbGoodsClassify in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodsClassify.GoodsClassifyID
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID                                                       
                             orderby tbGoods.GoodsID
                             where tbGoods.CommodityTypeID == 1
                             select new Vo.Goods
                             {
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsName = tbGoods.GoodsName,
                                 GoodsClassifyID = tbGoodsClassify.GoodsClassifyID,
                                 GoodsClassifyName = tbGoodsClassify.GoodsClassifyName,
                                 AdvanceCess = tbGoods.AdvanceCess,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,                                 
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<Vo.Goods> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Vo.Goods> bsgrid = new Bsgrid<Vo.Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="GPID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPinXinXi(int GGPID)
        {
            if (GGPID > 0)
            {
                var listGoods = (from tbGoods in MyModels.B_GoodsList
                                 join tbGoodsClassify in MyModels.B_GoodsClassifyList on tbGoods.GoodsClassifyID equals tbGoodsClassify.GoodsClassifyID
                                 join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                                 where tbGoods.GoodsID == GGPID
                                 select new Vo.Goods
                                 {
                                     GoodsID = tbGoods.GoodsID,
                                     GoodsName = tbGoods.GoodsName,
                                     GoodsClassifyID = tbGoodsClassify.GoodsClassifyID,
                                     GoodsClassifyName = tbGoodsClassify.GoodsClassifyName,
                                     AdvanceCess = tbGoods.AdvanceCess,
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 查询合同基本信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult HeTongXinXiXuanZe(BsgridPage bsgridPage)
        {
            var listGoods = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                             join tbAgreement in MyModels.B_AgreementList on tbOrderFormPact.AgreementID equals tbAgreement.AgreementID
                             join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID
                             join tbAdjustAccountsFashion in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashion.AdjustAccountsFashionID
                             join tbSpouseBRanch in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanch.SpouseBRanchID
                             orderby tbOrderFormPact.OrderFormPactID
                             select new Vo.OrderFormPact
                             {
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 AgreementID = tbAgreement.AgreementID, /*相关合同信息ID*/
                                 AgreementCode = tbAgreement.AgreementCode,/*合同代码*/
                                 SupplierID = tbSupplier.SupplierID,/*供货商*/
                                 SupplierName = tbSupplier.SupplierName,
                                 AdjustAccountsFashionID = tbAdjustAccountsFashion.AdjustAccountsFashionID,/*核算方式*/
                                 AdjustAccountsFashion = tbAdjustAccountsFashion.AdjustAccountsFashion,
                                 ReleaseTimeStr = tbOrderFormPact.ValidBegin.ToString(),/*有效期始*/
                                 ReleaseTimeStrr = tbOrderFormPact.ValidTip.ToString(), /*有效期末*/
                                 SpouseBRanchID = tbSpouseBRanch.SpouseBRanchID, /*采购部门*/
                                 SpouseBRanchName = tbSpouseBRanch.SpouseBRanchName,
                                 ContractNumber = tbOrderFormPact.ContractNumber,
                             }).ToList();

            int totalRow = listGoods.Count();
            List<Vo.OrderFormPact> notices = listGoods.OrderByDescending(p => p.OrderFormPactID).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).
                Take(bsgridPage.pageSize).
                ToList();

            Vo.Bsgrid<Vo.OrderFormPact> bsgrid = new Vo.Bsgrid<Vo.OrderFormPact>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = notices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取合同基本信息
        /// </summary>
        /// <param name="HTID"></param>
        /// <returns></returns>
        public ActionResult HuoQuHeTongXinXi(BsgridPage bsgridPage, Array JieShouID)
        {
            List<OrderFormPact> list = new List<OrderFormPact>();
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');

            for (int i = 0; i < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[i]);
                var listGoods = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                 join tbAgreement in MyModels.B_AgreementList on tbOrderFormPact.AgreementID equals tbAgreement.AgreementID
                                 join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID
                                 join tbAdjustAccountsFashion in MyModels.S_AdjustAccountsFashionList on tbOrderFormPact.AdjustAccountsFashionID equals tbAdjustAccountsFashion.AdjustAccountsFashionID
                                 join tbSpouseBRanch in MyModels.S_SpouseBRanchList on tbOrderFormPact.SpouseBRanchID equals tbSpouseBRanch.SpouseBRanchID
                                 orderby tbOrderFormPact.OrderFormPactID
                                 where tbOrderFormPact.OrderFormPactID == goodsIDs
                                 select new Vo.OrderFormPact
                                 {
                                     OrderFormPactIDs = tbOrderFormPact.OrderFormPactID,
                                     AgreementID = tbAgreement.AgreementID, /*相关合同信息ID*/
                                     AgreementCode = tbAgreement.AgreementCode,/*合同代码*/
                                     SupplierID = tbSupplier.SupplierID,/*供货商*/
                                     SupplierName = tbSupplier.SupplierName,
                                     AdjustAccountsFashionID = tbAdjustAccountsFashion.AdjustAccountsFashionID,/*核算方式*/
                                     AdjustAccountsFashion = tbAdjustAccountsFashion.AdjustAccountsFashion,
                                     ReleaseTimeStr = tbOrderFormPact.ValidBegin.ToString(),/*有效期始*/
                                     ReleaseTimeStrr = tbOrderFormPact.ValidTip.ToString(), /*有效期末*/
                                     SpouseBRanchID = tbSpouseBRanch.SpouseBRanchID, /*采购部门*/
                                     SpouseBRanchName = tbSpouseBRanch.SpouseBRanchName,
                                     ContractNumber = tbOrderFormPact.ContractNumber,
                                 }).ToList();
                list.AddRange(listGoods);
                i++;
            }

            int totalRow = list.Count();
            List<Vo.OrderFormPact> listNotices = list.OrderByDescending(p => p.OrderFormPactIDs).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Vo.Bsgrid<Vo.OrderFormPact> bsgrid = new Vo.Bsgrid<Vo.OrderFormPact>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = listNotices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除删除合同
        /// </summary>
        /// <param name="GoodsMoneyRuleId"></param>
        /// <returns></returns>
        public ActionResult DeleteOrderFormPact(int OrderFormPactId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_OrderFormPactList dbOrderFormPact = (from tbGoodsMoneyRule in MyModels.B_OrderFormPactList
                                                       where tbGoodsMoneyRule.OrderFormPactID == OrderFormPactId
                                                       select tbGoodsMoneyRule).Single();
                MyModels.B_OrderFormPactList.Remove(dbOrderFormPact);
                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
       
        #endregion

        #region 自营合同经营商品定义     
        public ActionResult HeTongJingYinShangPin()
        {
            return View();
        }
        /// <summary>
        /// 查询合同信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectHeTongXinXiXuanZe(BsgridPage bsgridPage)
        {
            var listGoods = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                             join tbMethod in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethod.MethodOfSettlingAccountsID
                             join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID
                             orderby tbOrderFormPact.OrderFormPactID
                             select new Vo.OrderFormPact
                             {
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 ContractNumber = tbOrderFormPact.ContractNumber,/*主合同号*/
                                 MethodOfSettlingAccountsID = tbMethod.MethodOfSettlingAccountsID,/*结算方式*/
                                 MethodOfSettlingAccounts = tbMethod.MethodOfSettlingAccounts,
                                 SupplierID = tbSupplier.SupplierID,/*供货商*/
                                 SupplierName = tbSupplier.SupplierName,
                                 ReleaseTimeStr = tbOrderFormPact.ValidBegin.ToString(),/*有效期始*/
                                 ReleaseTimeStrr = tbOrderFormPact.ValidTip.ToString(), /*有效期末*/
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<OrderFormPact> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<OrderFormPact> bsgrid = new Bsgrid<OrderFormPact>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取合同信息
        /// </summary>
        /// <param name="HTID"></param>
        /// <returns></returns>
        public ActionResult SelectHuoQuHeTongXinXi(int HTID)
        {
            if (HTID > 0)
            {
                var listGoods = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                 join tbMethod in MyModels.S_MethodOfSettlingAccountsList on tbOrderFormPact.MethodOfSettlingAccountsID equals tbMethod.MethodOfSettlingAccountsID
                                 join tbSupplier in MyModels.B_SupplierList on tbOrderFormPact.SupplierID equals tbSupplier.SupplierID
                                 where tbOrderFormPact.OrderFormPactID == HTID
                                 select new Vo.OrderFormPact
                                 {
                                     OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                     ContractNumber = tbOrderFormPact.ContractNumber,/*主合同号*/
                                     MethodOfSettlingAccountsID = tbMethod.MethodOfSettlingAccountsID,/*结算方式*/
                                     MethodOfSettlingAccounts = tbMethod.MethodOfSettlingAccounts,
                                     SupplierID = tbSupplier.SupplierID,/*供货商*/
                                     SupplierName = tbSupplier.SupplierName,
                                     ReleaseTimeStr = tbOrderFormPact.ValidBegin.ToString(),/*有效期始*/
                                     ReleaseTimeStrr = tbOrderFormPact.ValidTip.ToString(), /*有效期末*/
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }

        /// <summary>
        /// 查询商品信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult ShangPinXinXiDeXuanZe(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                             join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                             join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                             where tbGoods.CommodityTypeID == 1
                             orderby tbGoodsDetail.GoodsDetailID
                             select new Vo.Goods
                             {
                                 GoodsDetailID = tbGoodsDetail.GoodsDetailID,
                                 GoodsID = tbGoods.GoodsID,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsName = tbGoods.GoodsName,
                                 AdvanceCess = tbGoods.AdvanceCess,
                                 PurchaseID = tbPurchase.PurchaseID,
                                 AdvanceBid = tbPurchase.AdvanceBid,
                                 NoAdvanceBid = tbPurchase.NoAdvanceBid,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 PurchasePriceMarkup = tbPurchase.PurchasePriceMarkup,
                             }).ToList();
            int intTotalRow = listGoods.Count();//总行数
            
            List<Goods> listNotices = listGoods.OrderByDescending(p => p.GoodsDetailID).//noboer表达式
               Skip(bsgridPage.GetStartIndex()).
               Take(bsgridPage.pageSize).
               ToList();

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="JieShouID"></param>
        /// <returns></returns>
        public ActionResult HuoQuShangPinXinXiDeXuanZe(BsgridPage bsgridPage, Array JieShouID)
        {
            List<Goods> list = new List<Goods>();
            string Z = ((string[])JieShouID)[0];
            string[] intsZ = Z.Split(',');

            for (int i = 0; i < intsZ.Length;)
            {
                var goodsIDs = Convert.ToInt32(intsZ[i]);
                var listGoods = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                 join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                 join tbPurchase in MyModels.B_PurchaseList on tbGoodsDetail.PurchaseID equals tbPurchase.PurchaseID
                                 //where tbGoods.CommodityTypeID == 1
                                 where tbGoodsDetail.GoodsDetailID == goodsIDs
                                 orderby tbGoodsDetail.GoodsDetailID
                                 select new Vo.Goods
                                 {
                                     GoodsDetailIDs = tbGoodsDetail.GoodsDetailID,
                                     GoodsID = tbGoods.GoodsID,
                                     GoodsCode = tbGoods.GoodsCode,
                                     GoodsName = tbGoods.GoodsName,
                                     AdvanceCess = tbGoods.AdvanceCess,
                                     PurchaseID = tbPurchase.PurchaseID,
                                     AdvanceBid = tbPurchase.AdvanceBid,
                                     NoAdvanceBid = tbPurchase.NoAdvanceBid,
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                     PurchasePriceMarkup = tbPurchase.PurchasePriceMarkup,
                                 }).ToList();
                list.AddRange(listGoods);
                i++;
            }

            int totalRow = list.Count();
            List<Vo.Goods> listNotices = list.OrderByDescending(p => p.GoodsDetailIDs).//noboer表达式
                Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();

            Vo.Bsgrid<Vo.Goods> bsgrid = new Vo.Bsgrid<Vo.Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = totalRow;//总的行数
            bsgrid.curPage = bsgridPage.curPage;//请求当前页
            bsgrid.data = listNotices;//查出的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);            
        }

        /// <summary>
        /// 删除合同信息
        /// </summary>
        /// <param name="GoodsMoneyRuleId"></param>
        /// <returns></returns>
        public ActionResult DeleteGoodsDetail(int GoodsDetailId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_GoodsDetailList dbGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                                   join tbGoods in MyModels.B_GoodsList on tbGoodsDetail.GoodsID equals tbGoods.GoodsID
                                                   where tbGoodsDetail.GoodsDetailID == GoodsDetailId  
                                                   where tbGoods.AuditingBit == false                                                 
                                                   select tbGoodsDetail).Single();
                MyModels.B_GoodsDetailList.Remove(dbGoodsDetail);
                if (MyModels.SaveChanges() > 0)
                {
                    strMsg = "success";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}

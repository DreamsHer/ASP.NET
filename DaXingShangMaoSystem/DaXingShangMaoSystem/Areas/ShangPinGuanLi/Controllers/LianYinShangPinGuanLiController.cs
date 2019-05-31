using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.ShangPinGuanLi.Controllers
{
    public class LianYinShangPinGuanLiController : Controller
    {
        // GET: ShangPinGuanLi/LianYinShangPinGuanLi  
        // 联营商品管理
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        #region 联营商品定义信息       
        public ActionResult LianYinShangPinXinXi()
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
                                                  where tbCommodityType.CommodityTypeID == 2
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
                                                where tbGoodsStatus.GoodsStatusID == 2 || tbGoodsStatus.GoodsStatusID == 3
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
        /// 专柜商品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectZhuanGuiCommodityTypeID()
        {
            List<SelectVo> listCommodityType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listCommodityType.Add(selectVo);

            List<SelectVo> listCommodityTypeID = (from tbCommodityType in MyModels.S_ZhuanGuiCommodityTypeLiat

                                                  select new SelectVo
                                                  {
                                                      id = tbCommodityType.ZhuanGuiCommodityTypeID,
                                                      text = tbCommodityType.ZhuanGuiCommodityType.Trim()
                                                  }).ToList();

            listCommodityType.AddRange(listCommodityTypeID);

            return Json(listCommodityType, JsonRequestBehavior.AllowGet);
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
                                     where tbAgreementType.AgreementTypeID == 3
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
                                                  where tbCommodityType.CommodityTypeID == 2
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
        /// 专柜商品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectZhuanGuiCommodityType1()
        {
            List<SelectVo> listCommodityType = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择----"
            };
            listCommodityType.Add(selectVo);

            List<SelectVo> listCommodityTypeID = (from tbCommodityType in MyModels.S_ZhuanGuiCommodityTypeLiat

                                                  select new SelectVo
                                                  {
                                                      id = tbCommodityType.ZhuanGuiCommodityTypeID,
                                                      text = tbCommodityType.ZhuanGuiCommodityType.Trim()
                                                  }).ToList();

            listCommodityType.AddRange(listCommodityTypeID);

            return Json(listCommodityType, JsonRequestBehavior.AllowGet);
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
                             where tbGoods.CommodityTypeID == 2
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
                             join tbZhuanGuiCommodityType in MyModels.S_ZhuanGuiCommodityTypeLiat on tbGoods.ZhuanGuiCommodityTypeID equals tbZhuanGuiCommodityType.ZhuanGuiCommodityTypeID

                             where tbGoods.CommodityTypeID == 2 && tbGoods.AuditingBit == false
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
                                 CommodityTypeID = tbCommodityType.CommodityTypeID,
                                 CommodityType = tbCommodityType.CommodityType,
                                 GoodsPicture = tbGoods.GoodsPicture,
                                 ZhuanGuiCommodityTypeID = tbZhuanGuiCommodityType.ZhuanGuiCommodityTypeID,
                                 ZhuanGuiCommodityType = tbZhuanGuiCommodityType.ZhuanGuiCommodityType,
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
            if (GID > 0)
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
                                 join tbZhuanGuiCommodityType in MyModels.S_ZhuanGuiCommodityTypeLiat on tbGoods.ZhuanGuiCommodityTypeID equals tbZhuanGuiCommodityType.ZhuanGuiCommodityTypeID

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
                                     ZhuanGuiCommodityTypeID = tbZhuanGuiCommodityType.ZhuanGuiCommodityTypeID,
                                     ZhuanGuiCommodityType = tbZhuanGuiCommodityType.ZhuanGuiCommodityType,
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
        public ActionResult btnInsertUpdate(B_GoodsList pwGoods, HttpPostedFileBase fileImage, B_GoodsDetailList pwGoodsDetail, S_PackInfoIDList pwpackInfo)
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
                    Dep.ZhuanGuiCommodityTypeID = Convert.ToInt32(Request.Form["ZhuanGuiCommodityTypeID"]);
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
                        if (KK.GoodsID != null && KK.PurchaseID != null && KK.GoodsDetailName != null && KK.RetailMonovalent != null && KK.GoodsFixAPrice != null
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
                    Dep.ZhuanGuiCommodityTypeID = Convert.ToInt32(Request.Form["ZhuanGuiCommodityTypeID"]);

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
                
                var dbGoodsDetail = MyModels.B_GoodsDetailList.Where(p => p.GoodsID == GoodsID).Single();
                MyModels.B_GoodsDetailList.Remove(dbGoodsDetail);

                int GoodsDetailID = (int)dbGoodsDetail.GoodsDetailID;
                
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

        #endregion          

        #region 联营商品状态变更     
        public ActionResult LianYinShangPinZhuangTaiBianGeng()
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
                         join tbCommodityType in MyModels.S_CommodityTypeList on tbGoods.CommodityTypeID equals tbCommodityType.CommodityTypeID
                         where tbGoods.CommodityTypeID == 2
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
        public ActionResult GoodsXiuGaiBianGeng(int GoodsID, int goodsStatusID)
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
                if (goodsStatusID > 0)
                {
                    ListGoods = ListGoods.Where(p => p.GoodsStatusID == goodsStatusID);
                }
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

        #region 联营商品清退        
        public ActionResult LianYinShangPinQingTui()
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
        /// 多条件查询
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
                         where tbGoods.CommodityTypeID == 2
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

        #endregion
              
        #region 修改专柜商品合同信息
        public ActionResult LianYinZhuanKuiShangPinHeTong()
        {
            return View();
        }
        /// <summary>
        /// 查询商品信息至表格中
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="goodsID"></param>
        /// <returns></returns>
        public ActionResult SelectHeTongXinXi(BsgridPage bsgridPage, int goodsID)
        {
            var listGoods = (from tbGoods in MyModels.B_GoodsList
                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                             orderby tbOrderFormPact.OrderFormPactID
                             where tbGoods.GoodsID== goodsID
                             where tbGoods.CommodityTypeID == 3
                             select new Vo.Goods
                             {
                                 GoodsID = tbGoods.GoodsID,
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 HostContractNumber = tbOrderFormPact.HostContractNumber,/*主合同号*/
                                 SupplierID = tbSupplier.SupplierID,/*供货商*/
                                 SupplierName = tbSupplier.SupplierName,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsName = tbGoods.GoodsName,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 SupplierNumber = tbSupplier.SupplierNumber,
                                 RateSale = tbGoodsDetail.RateSale,
                                 NewRateSale = tbGoodsDetail.NewRateSale,
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

        /// <summary>
        /// 查询合同号
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult SelectHeTongXinXiXuanZe(BsgridPage bsgridPage)
        {
            var listGoods = (from tbGoods in MyModels.B_GoodsList
                             join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                             join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                             join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                             orderby tbOrderFormPact.OrderFormPactID
                             where tbGoods.CommodityTypeID == 3
                             select new Vo.Goods
                             {
                                 GoodsID = tbGoods.GoodsID,
                                 OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                 HostContractNumber = tbOrderFormPact.HostContractNumber,/*主合同号*/                                
                                 SupplierID = tbSupplier.SupplierID,/*供货商*/
                                 SupplierName = tbSupplier.SupplierName,
                                 GoodsCode = tbGoods.GoodsCode,
                                 GoodsName = tbGoods.GoodsName,
                                 RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                 SupplierNumber = tbSupplier.SupplierNumber,
                                 RateSale = tbGoodsDetail.RateSale,
                                 NewRateSale = tbGoodsDetail.NewRateSale,
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
       
        /// <summary>
        /// 获取数据填入单元格
        /// </summary>
        /// <param name="HTID"></param>
        /// <returns></returns>
        public ActionResult SelectHuoQuHeTongXinXi(int HTID)
        {
            if (HTID > 0)
            {
                var listGoods = (from tbGoods in MyModels.B_GoodsList
                                 join tbOrderFormPact in MyModels.B_OrderFormPactList on tbGoods.OrderFormPactID equals tbOrderFormPact.OrderFormPactID
                                 join tbSupplier in MyModels.B_SupplierList on tbGoods.SupplierID equals tbSupplier.SupplierID
                                 join tbGoodsDetail in MyModels.B_GoodsDetailList on tbGoods.GoodsID equals tbGoodsDetail.GoodsID
                                 where tbGoods.GoodsID == HTID
                                 where tbGoods.CommodityTypeID == 3
                                 select new Vo.Goods
                                 {
                                     GoodsID = tbGoods.GoodsID,
                                     OrderFormPactID = tbOrderFormPact.OrderFormPactID,
                                     HostContractNumber = tbOrderFormPact.HostContractNumber,/*主合同号*/
                                     SupplierID = tbSupplier.SupplierID,/*供货商*/
                                     SupplierName = tbSupplier.SupplierName,
                                     GoodsCode = tbGoods.GoodsCode,
                                     GoodsName = tbGoods.GoodsName,
                                     RetailMonovalent = tbGoodsDetail.RetailMonovalent,
                                     SupplierNumber = tbSupplier.SupplierNumber,
                                     RateSale = tbGoodsDetail.RateSale,
                                     NewRateSale = tbGoodsDetail.NewRateSale,
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
     
        /// <summary>
        /// 修改商品合同信息保存
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateHeTongXinXi()
        {
            string styMy = "fail";            
            try
            {
                B_GoodsList KK = new B_GoodsList();
                var TT =KK.GoodsID = Convert.ToInt32(Request.Form["GoodsID"]);
                var listGoodsID = (from tbGoods in MyModels.B_GoodsList
                                   where tbGoods.GoodsID == TT
                                   select new 
                                   {
                                       tbGoods.GoodsID,
                                       tbGoods.GoodsCode,
                                       tbGoods.GoodsName,
                                       //tbGoods.GoodsAbbreviation,
                                       tbGoods.GoodsStampID, 
                                       tbGoods.AdvanceCess,
                                       tbGoods.AllowGoodsXiaoShu,
                                       tbGoods.ApplyTargetID,
                                       tbGoods.ArtNo,
                                       tbGoods.AuditingBit,
                                       tbGoods.Auditor,
                                       tbGoods.BarCodTypeID,
                                       tbGoods.BurdenStockSellStatusID,
                                       tbGoods.Checktime,
                                       tbGoods.CommodityTypeID,
                                       tbGoods.CountersellID,
                                       tbGoods.EstimateUnitID,          
                                       tbGoods.GoodsChopID,
                                       tbGoods.GoodsClassifyID,
                                       //tbGoods.GoodsColours,
                                       tbGoods.GoodsPurchasemoneyStatusID,
                                       tbGoods.GoodsPurchasemoneyTypeID,
                                       tbGoods.GoodsSeasonID,                                      
                                       tbGoods.GoodsStatusID,
                                       tbGoods.GoodsTiaoMa,
                                       tbGoods.ManufacturerID,
                                       tbGoods.OrderFormPactID,
                                       tbGoods.PLUCod,
                                       tbGoods.ProducingArea,
                                       tbGoods.PurchaseSectionID,
                                       tbGoods.QualityContent,
                                       //tbGoods.QualityGrade,
                                       tbGoods.QualityGuaranteePeriod,
                                       tbGoods.RegisterTime,
                                       tbGoods.Registrant,
                                       tbGoods.Remarks,
                                       tbGoods.SellCess,
                                       tbGoods.SourceID,
                                       tbGoods.SpecificationsModel,
                                       tbGoods.SpellCod,
                                       tbGoods.StaffID,
                                       tbGoods.StatisticsScale,
                                       tbGoods.SupplierID,
                                       tbGoods.ZhuanGuiCommodityTypeID,                                   
                                   }).Single();
                var ww = listGoodsID.GoodsCode;
                var ee = listGoodsID.GoodsName;
                //var ss = listGoodsID.GoodsAbbreviation;
                var dd = listGoodsID.GoodsStampID;
                var q1 = listGoodsID.AdvanceCess;
                var q2 = listGoodsID.AllowGoodsXiaoShu;
                var q3 = listGoodsID.ApplyTargetID;
                var q4 = listGoodsID.ArtNo;
                var q5 = listGoodsID.AuditingBit;
                var q6 = listGoodsID.Auditor;
                var q7 = listGoodsID.BarCodTypeID;
                var q8 = listGoodsID.BurdenStockSellStatusID;
                var w1 = listGoodsID.Checktime;
                var w2 = listGoodsID.CommodityTypeID;
                var w3 = listGoodsID.CountersellID;
                var w4 = listGoodsID.EstimateUnitID;
                var w7 = listGoodsID.GoodsClassifyID;
                //var w8 = listGoodsID.GoodsColours;
                var w9 = listGoodsID.GoodsPurchasemoneyStatusID;
                var e1 = listGoodsID.GoodsPurchasemoneyTypeID;
                var e2 = listGoodsID.GoodsSeasonID;
                var e3 = listGoodsID.GoodsStatusID;
                var e4 = listGoodsID.GoodsTiaoMa;
                var e5 = listGoodsID.ManufacturerID;
                var e6 = listGoodsID.OrderFormPactID;
                var e7 = listGoodsID.PLUCod;
                var e8 = listGoodsID.ProducingArea;
                var e9 = listGoodsID.PurchaseSectionID;
                var r1 = listGoodsID.QualityContent;
                //var r2 = listGoodsID.QualityGrade;
                var r3 = listGoodsID.QualityGuaranteePeriod;
                var r4 = listGoodsID.Registrant;
                var r5 = listGoodsID.Remarks;
                var r6 = listGoodsID.SellCess;
                var r7 = listGoodsID.SpecificationsModel;
                var r8 = listGoodsID.SpellCod;
                var r9 = listGoodsID.StaffID;
                var r10 = listGoodsID.StatisticsScale;
                var r11= listGoodsID.SupplierID;
                var r12 = listGoodsID.ZhuanGuiCommodityTypeID;
                var r13 = listGoodsID.SourceID;
                var r14 = listGoodsID.GoodsChopID;
                var r15 = listGoodsID.RegisterTime;

                KK.GoodsID = Convert.ToInt32(Request.Form["GoodsID"]);
                KK.GoodsCode = ww;
                KK.GoodsName = ee;
                //KK.GoodsAbbreviation = ss;
                KK.GoodsStampID = dd;
                KK.GoodsCode = ww;
                KK.GoodsName = ee;
                //KK.GoodsAbbreviation = ss;
                KK.GoodsStampID = dd;
                KK.AdvanceCess = q1;
                KK.AllowGoodsXiaoShu = q2;
                KK.ApplyTargetID = q3;
                KK.ArtNo = q4;
                KK.AuditingBit = q5;
                KK.Auditor = q6;
                KK.BarCodTypeID = q7;
                KK.BurdenStockSellStatusID = q8;
                KK.Checktime = w1;
                KK.CommodityTypeID = w2;
                KK.CountersellID = w3;
                KK.EstimateUnitID = w4;
                KK.GoodsClassifyID = w7;
                //KK.GoodsColours = w8;
                KK.GoodsPurchasemoneyStatusID = w9;
                KK.GoodsPurchasemoneyTypeID = e1;
                KK.GoodsSeasonID = e2;
                KK.GoodsStatusID = e3;
                KK.GoodsTiaoMa = e4;
                KK.ManufacturerID = e5;
                KK.OrderFormPactID = e6;
                KK.PLUCod = e7;
                KK.ProducingArea = e8;
                KK.PurchaseSectionID = e9;
                KK.QualityContent = r1;
                //KK.QualityGrade = r2;
                KK.QualityGuaranteePeriod = r3;
                KK.Registrant = r4;
                KK.Remarks = r5;
                KK.SellCess = r6;
                KK.SpecificationsModel = r7;
                KK.SpellCod = r8;
                KK.StaffID = r9;
                KK.StatisticsScale = r10;
                KK.SupplierID = r11;
                KK.ZhuanGuiCommodityTypeID = r12;
                KK.SourceID = r13;
                KK.GoodsChopID = r14;
                KK.RegisterTime = r15;

                B_GoodsDetailList YY = new B_GoodsDetailList();
                var rr = listGoodsID.GoodsID;
                var listGoodsDetail = (from tbGoodsDetail in MyModels.B_GoodsDetailList
                                       where tbGoodsDetail.GoodsID == rr
                                       select new
                                   {
                                           tbGoodsDetail.GoodsID,
                                           tbGoodsDetail.GoodsDetailID,
                                           tbGoodsDetail.RetailMonovalent,
                                           tbGoodsDetail.RateSale,                                       
                                   }).Single();

                var tt = listGoodsDetail.RetailMonovalent;
                var uu = listGoodsDetail.RateSale;                 

                YY.GoodsDetailID = Convert.ToInt32(Request.Form["GoodsDetailID"]);
                YY.RetailMonovalent = tt;
                YY.RateSale = uu;

                //YY.NewRateSale =Convert.ToDecimal(Request.Form["NewRateSale"]);
                //MyModels.Entry(YY).State = System.Data.Entity.EntityState.Modified;
                //MyModels.SaveChanges();

                B_OrderFormPactList JJ = new B_OrderFormPactList();
                var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList
                                         where tbOrderFormPact.GoodsID == rr
                                         select new
                                         {
                                             tbOrderFormPact.OrderFormPactID,
                                             tbOrderFormPact.AgreementTypeID,
                                             tbOrderFormPact.AccountData,
                                             tbOrderFormPact.AdjustAccountsFashionID,
                                             tbOrderFormPact.AdvancedSettleAccounts,
                                             tbOrderFormPact.AdvanceMarketData,
                                             tbOrderFormPact.AgreementID,
                                             tbOrderFormPact.AgreementManagerID,
                                             tbOrderFormPact.AgreementStatusID,
                                             tbOrderFormPact.ConclusionSite,
                                             tbOrderFormPact.ConclusionTime,
                                             tbOrderFormPact.ContractNumber,
                                             tbOrderFormPact.Firingdata,
                                             tbOrderFormPact.GoodsID,
                                             tbOrderFormPact.HandworkContractNumber,
                                             //tbOrderFormPact.HostContractNumber,                                            
                                             tbOrderFormPact.MethodOfSettlingAccountsID,
                                             tbOrderFormPact.PlanFiringData,
                                             tbOrderFormPact.PurchasingAgent,
                                             tbOrderFormPact.RetreatMarketData,
                                             tbOrderFormPact.SpouseBRanchID,
                                             tbOrderFormPact.StaffID,
                                             tbOrderFormPact.SupplierID,
                                             tbOrderFormPact.TryPaymentData,
                                             tbOrderFormPact.ValidBegin,
                                             tbOrderFormPact.ValidTip,
                                             tbOrderFormPact.WritData,
                                         }).Single();

                var p1 = listOrderFormPact.AgreementTypeID;
                var p2 = listOrderFormPact.AccountData;
                var p3 = listOrderFormPact.AdjustAccountsFashionID;
                var p4 = listOrderFormPact.AdvancedSettleAccounts;
                var p5 = listOrderFormPact.AdvanceMarketData;
                var p6 = listOrderFormPact.AgreementID;
                var p7 = listOrderFormPact.AgreementManagerID;
                var p8 = listOrderFormPact.AgreementStatusID;
                var p9 = listOrderFormPact.ConclusionSite;
                var p10 = listOrderFormPact.ConclusionTime;
                var p11 = listOrderFormPact.ContractNumber;
                var p12 = listOrderFormPact.Firingdata;
                var p13 = listOrderFormPact.GoodsID;
                var p14 = listOrderFormPact.HandworkContractNumber;
                //var p15 = listOrderFormPact.HostContractNumber;
                var p16 = listOrderFormPact.MethodOfSettlingAccountsID;
                var p17 = listOrderFormPact.PlanFiringData;
                var p18 = listOrderFormPact.PurchasingAgent;
                var p19 = listOrderFormPact.RetreatMarketData;
                var p20 = listOrderFormPact.SpouseBRanchID;
                var p21 = listOrderFormPact.StaffID;
                var p22 = listOrderFormPact.SupplierID;
                var p23 = listOrderFormPact.TryPaymentData;
                var p24 = listOrderFormPact.ValidBegin;
                var p25 = listOrderFormPact.ValidTip;
                var p26 = listOrderFormPact.WritData;

                JJ.AgreementTypeID = p1;
                JJ.AccountData = p2;
                JJ.AdjustAccountsFashionID = p3;
                JJ.AdvancedSettleAccounts = p4;
                JJ.AdvanceMarketData = p5;
                JJ.AgreementID = p6;
                JJ.AgreementManagerID = p7;
                JJ.AgreementStatusID = p8;
                JJ.ConclusionSite = p9;
                JJ.ConclusionTime = p10;
                JJ.ContractNumber = p11;
                JJ.Firingdata = p12;
                JJ.GoodsID = p13;
                JJ.HandworkContractNumber = p14;
                //JJ.HostContractNumber = p15;
                JJ.MethodOfSettlingAccountsID = p16;
                JJ.PlanFiringData = p17;
                JJ.PurchasingAgent = p18;
                JJ.RetreatMarketData = p19;
                JJ.SpouseBRanchID = p20;
                JJ.StaffID = p21;
                JJ.SupplierID = p22;
                JJ.TryPaymentData = p23;
                JJ.ValidBegin = p24;
                JJ.ValidTip = p25;
                JJ.WritData = p26;

                KK.OrderFormPactID = Convert.ToInt32(Request.Form["OrderFormPactID"]);

                MyModels.Entry(KK).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
                
                JJ.OrderFormPactID =Convert.ToInt32(KK.OrderFormPactID);
                JJ.HostContractNumber = Request.Form["HostContractNumber"];

                MyModels.Entry(JJ).State = System.Data.Entity.EntityState.Modified;
                MyModels.SaveChanges();
           
                styMy = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(styMy, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
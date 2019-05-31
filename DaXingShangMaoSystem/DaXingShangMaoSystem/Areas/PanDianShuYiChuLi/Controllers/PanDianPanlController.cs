using DaXingShangMaoSystem.Common;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.PanDianShuYiChuLi.Controllers
{
    public class PanDianPanlController : Controller
    {
        // GET: PanDianShuYiChuLi/PanDianPanl
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        
        /// <summary>
        /// 计划盘点（选择）一
        /// </summary>
        /// <returns></returns>
        public ActionResult Panl()
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
            var listy = (from tbem in MyModels.B_CheckRemerbenList
                         orderby tbem.Remter
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_CheckRemerbenList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.Remter.Substring(1, 8));
                inemp++;
                Std = inemp.ToString();
                for (int i = 0; i < 4; i++)
                {
                    Std = Std.Length < 4 ? "0" + Std : Std;
                }
            }
            else
            {
                Std = "0001";
            }
            return Json(Std, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 下拉（库存地点）
        /// </summary>
        /// <returns></returns>
        public ActionResult KuCunDiDian()
        {
            List<SelectXiaLa> listCaiGou = new List<SelectXiaLa>();
            SelectXiaLa selectXiaLa = new SelectXiaLa()
            {
                id = 0,
                text = "---请选择---"
            };
            listCaiGou.Add(selectXiaLa);
            //然后又使用  List<SelectXiaLa>来进行查询
            List<SelectXiaLa> listNoticeTypeS = (from tb in MyModels.S_StockPlaceList
                                                 select new SelectXiaLa
                                                 {
                                                     id = tb.StockPlaceID,
                                                     text = tb.StockPlaceName
                                                 }).ToList();
            listCaiGou.AddRange(listNoticeTypeS);
            //最后拼接起来返回 listNoticeTypeS
            return Json(listCaiGou, JsonRequestBehavior.AllowGet);

        }

        //绑树形（一）
        public string GetDrugTypeTreeData(List<Models.B_PanlList> list, int fid)
        {
            //把list结构转化为json字符串
            StringBuilder sbTree = new StringBuilder();
            List<Models.B_PanlList> listNode = list.Where(m => m.F_DrugTypeID == fid).ToList();
            if (listNode.Count > 0)
            {
                //有节点存在
                sbTree.Append("[");
                for (int i = 0; i < listNode.Count; i++)

                {
                    #region 获取子节点
                    int proid = listNode[i].DrugTypeID;
                    //判断当前节点是否有子节点
                    string sbChild = GetDrugTypeTreeData(list, proid);//调用自身的list
                   
                    #endregion

                    #region 获取json格式
                    if (sbChild.ToString() != "")
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].DrugTypeID + "\",\"text\":\"" + listNode[i].DrugType + "\",\"children\":");
                        sbTree.Append(sbChild);
                        sbTree.Append("},");

                    }
                    else
                    {
                        sbTree.Append("{\"id\":\"" + listNode[i].DrugTypeID + "\",\"text\":\"" + listNode[i].DrugType + "\"},");
                    }
                    #endregion
                }
                //没有子节点
                /*没有子节点*/
                sbTree.Remove(sbTree.Length - 1, 1);
                sbTree.Append("]");
            }
            return sbTree.ToString();

            //[{},{}]
        }

        //绑树形(二)
        public ActionResult GetAllDrugType()
        {
            //获取整张表的数据，转化成
            string strJsonDrugType = MyModels.B_PanlList.ToList().GetJSONTreeData("DrugTypeID", "DrugType", "F_DrugTypeID");//调用下面的方法，转换SJons 格式字符串
            //string strDrugType = GetDrugTypeTreeData(listDrugType, 0);
            return Content(strJsonDrugType);//返回
        }

        
        //拖动（移动）节点（）
        public ActionResult UpdateFather(int intTargetId, int intSourceId)
        {
            var modelDrugType = MyModels.B_PanlList.Find(intSourceId);
            modelDrugType.F_DrugTypeID = intTargetId;
            MyModels.SaveChanges();
            return new EmptyResult();
        }


        public ActionResult UpdateDrug(Models.B_PanlList modelDrug)
        {
            MyModels.Entry(modelDrug).State = System.Data.Entity.EntityState.Modified;
            MyModels.SaveChanges();
            return Content("OK");
        }


        /// <summary>
        /// 商品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsClass(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodClass in MyModels.B_GoodsClassifyList
                        where tbGoodClass.F_GoodsClassifyID==0
                        select new LY.WareHouseDeitaLL
                        {
                            GoodsClassifyID = tbGoodClass.GoodsClassifyID,
                            Code = tbGoodClass.Code,
                            GoodsClassifyName = tbGoodClass.GoodsClassifyName

                        }).ToList();


            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).//noboer表达式
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
        /// 绑定商品类型
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDGoodsClass(Vo.BsgridPage bsgridPage,int goodsClassifyID)
        {
            var linq = (from tbGoodClass in MyModels.B_GoodsClassifyList
                        where tbGoodClass.GoodsClassifyID== goodsClassifyID
                        select new LY.WareHouseDeitaLL
                        {
                            GoodsClassifyID = tbGoodClass.GoodsClassifyID,
                            Code = tbGoodClass.Code,
                            GoodsClassifyName = tbGoodClass.GoodsClassifyName

                        }).ToList();

            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.GoodsClassifyID).//noboer表达式
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
        /// 合同
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderFrom(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbOrderFrom in MyModels.B_OrderFormPactList
                        select new LY.WareHouseDeitaLL
                        {
                            OrderFormPactID = tbOrderFrom.OrderFormPactID,
                            ContractNumber = tbOrderFrom.ContractNumber,
                            ValidBegin = tbOrderFrom.ValidBegin.ToString(),
                            ValidTip = tbOrderFrom.ValidTip.ToString()

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.OrderFormPactID).//noboer表达式
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
        /// 绑定合同
        /// </summary>
        /// <returns></returns>
        public ActionResult BangOrderFrom(Vo.BsgridPage bsgridPage,int OrderFormPact)
        {
            var linq = (from tbOrderFrom in MyModels.B_OrderFormPactList
                        where tbOrderFrom.OrderFormPactID== OrderFormPact
                        select new LY.WareHouseDeitaLL
                        {
                            OrderFormPactID = tbOrderFrom.OrderFormPactID,
                            ContractNumber = tbOrderFrom.ContractNumber,
                            ValidBegin = tbOrderFrom.ValidBegin.ToString(),
                            ValidTip = tbOrderFrom.ValidTip.ToString()

                        }).ToList();
            int totalRow = linq.Count();

            List<LY.WareHouseDeitaLL> notices = linq.OrderByDescending(p => p.OrderFormPactID).//noboer表达式
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
        /// 商标
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodBiao(Vo.BsgridPage bsgridPage)
        {
            var linq = (from tbGoodBiao in MyModels.B_GoodsChopList
                        select new LY.WareHouseDeitaLL
                        {
                            GoodsChopID = tbGoodBiao.GoodsChopID,
                            GoodsChopCode = tbGoodBiao.GoodsChopCode,
                            GoodsChopName = tbGoodBiao.GoodsChopName

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


        /// <summary>
        /// 绑定商标
        /// </summary>
        /// <returns></returns>
        public ActionResult BangGoodBiao(Vo.BsgridPage bsgridPage,int goodsChopID)
        {
            var linq = (from tbGoodBiao in MyModels.B_GoodsChopList
                        where tbGoodBiao.GoodsChopID== goodsChopID
                        select new LY.WareHouseDeitaLL
                        {
                            GoodsChopID = tbGoodBiao.GoodsChopID,
                            GoodsChopCode = tbGoodBiao.GoodsChopCode,
                            GoodsChopName = tbGoodBiao.GoodsChopName

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
        /// 保存盘点
        /// </summary>
        [ValidateInput(false)]
        public ActionResult InserCheck(B_CheckRemerbenList OK)
        {

            string strMag = "fali";

            try
            {
                int oldWareHouseRows = (from tb in MyModels.B_CheckRemerbenList
                                        where tb.PablData == OK.PablData
                                        select tb).Count();
                if (oldWareHouseRows == 0)
                {
                    B_CheckRemerbenList MyB_CheckReme = new B_CheckRemerbenList();
                    MyB_CheckReme.Remter = OK.Remter;
                    MyB_CheckReme.PablData = OK.PablData;
                    MyB_CheckReme.CommodityType = OK.CommodityType;
                    MyB_CheckReme.DrugType = OK.DrugType;
                    MyB_CheckReme.StockPlaceID = OK.StockPlaceID;
                    MyB_CheckReme.OrderFormPactID = OK.OrderFormPactID;
                    MyB_CheckReme.GoodsClassifyID = OK.GoodsClassifyID;
                    MyB_CheckReme.GoodsChopID = OK.GoodsChopID;
                    MyB_CheckReme.ExamineName = OK.ExamineName;
                    MyB_CheckReme.ExamineTime = OK.ExamineTime;
                    MyB_CheckReme.DrugTypeID = OK.DrugTypeID;

                    MyModels.B_CheckRemerbenList.Add(MyB_CheckReme);

                    if (MyModels.SaveChanges() > 0)
                    {
                        strMag = "succsee";
                    }
                }
                else
                {
                    strMag = "exist";//已经存在
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }


        // <summary>
        ///查询盘点计划
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectCheck(Vo.BsgridPage bsgridPage)
        {


            string dateTimeNow = DateTime.Now.ToString("yyyy-MM-dd");//获取当前时间
            DateTime d = Convert.ToDateTime(dateTimeNow);
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.PablData== d
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
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
        ///绑定盘点计划
        /// </summary>
        /// <returns></returns>
        public ActionResult BangDingCheck(int checkRermeberID)
        {
            var linq = (from tbGoodBiao in MyModels.B_CheckRemerbenList
                        join tbStock in MyModels.S_StockPlaceList on tbGoodBiao.StockPlaceID equals tbStock.StockPlaceID
                        where tbGoodBiao.CheckRermeberID== checkRermeberID
                        select new LY.WareHouseDeitaLL
                        {
                            CheckRermeberID = tbGoodBiao.CheckRermeberID,
                            PablData = tbGoodBiao.PablData.ToString(),
                            CommodityType = tbGoodBiao.CommodityType,
                            DrugType = tbGoodBiao.DrugType,

                            OrderFormPactID = tbGoodBiao.OrderFormPactID,
                            drugTypeID =tbGoodBiao.DrugTypeID,
                            GoodsChopID = tbGoodBiao.GoodsChopID,
                            GoodsClassifyID = tbGoodBiao.GoodsClassifyID,

                            StockPlaceID = tbStock.StockPlaceID,
                            ExamineName = tbGoodBiao.ExamineName,
                            ExamineTime = tbGoodBiao.ExamineTime.ToString(),

                        }).ToList();
            return Json(linq, JsonRequestBehavior.AllowGet);
        }


        // <summary>
        /// 修改保存盘点
        /// </summary>
        [ValidateInput(false)]
        public ActionResult UpticoCheck(B_CheckRemerbenList OK)
        {

            string strMag = "fali";

            try
            {
                B_CheckRemerbenList MyB_CheckReme = new B_CheckRemerbenList();
                MyB_CheckReme.CheckRermeberID = OK.CheckRermeberID;
                MyB_CheckReme.Remter = OK.Remter;
                MyB_CheckReme.PablData = OK.PablData;
                MyB_CheckReme.CommodityType = OK.CommodityType;
                MyB_CheckReme.DrugType = OK.DrugType;
                MyB_CheckReme.StockPlaceID = OK.StockPlaceID;
                MyB_CheckReme.OrderFormPactID = OK.OrderFormPactID;
                MyB_CheckReme.GoodsClassifyID = OK.GoodsClassifyID;
                MyB_CheckReme.GoodsChopID = OK.GoodsChopID;
                MyB_CheckReme.ExamineName = OK.ExamineName;
                MyB_CheckReme.ExamineTime = OK.ExamineTime;
                MyB_CheckReme.DrugTypeID = OK.DrugTypeID;

                MyModels.Entry(MyB_CheckReme).State = System.Data.Entity.EntityState.Modified;
                if (MyModels.SaveChanges() > 0)
                {
                    strMag = "succsee";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 删除（返仓单）
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteWareHert(int checkRermeberID)
        {
            string strMsg = "fail";
            try
            {

                B_CheckRemerbenList conver = (from tbWarHouser in MyModels.B_CheckRemerbenList
                                           where tbWarHouser.CheckRermeberID == checkRermeberID
                                           select tbWarHouser).Single();
                MyModels.B_CheckRemerbenList.Remove(conver);
                MyModels.SaveChanges();
                strMsg = "success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

    }
}
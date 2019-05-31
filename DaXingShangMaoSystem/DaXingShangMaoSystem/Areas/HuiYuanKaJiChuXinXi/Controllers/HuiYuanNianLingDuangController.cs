using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaJiChuXinXi.Controllers
{
    public class HuiYuanNianLingDuangController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: HuiYuanKaJiChuXinXi/HuiYuanNianLingDuang
        public ActionResult DiYiHuiYuanNianLin()
        {
            return View();
        }
        public ActionResult SelectAge(BsgridPage bsgridPage)//查询会员年龄信息
        {
            var linqItme = from tbMember in MyModels.S_MembershipAgeList
                           orderby tbMember.MemberID
                           select tbMember;

            int intTotalRow = linqItme.Count();

            List<Models.S_MembershipAgeList> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MembershipAgeList> bsgrid = new Vo.Bsgrid<Models.S_MembershipAgeList>();//实例化 Bsgrid的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult InsertAGe(string MemberName, string OriginationAge, string EndAge)//新增年龄信息
        {
            string strAge = "falied";
            try
            {
                //实例化对象
                Models.S_MembershipAgeList myAge = new Models.S_MembershipAgeList();
                //给表对象属性赋值
                myAge.MemberName = MemberName.Trim();
                myAge.OriginationAge = OriginationAge.Trim();
                myAge.EndAge = EndAge.Trim();
                //把表对象实体模型中
                MyModels.S_MembershipAgeList.Add(myAge);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strAge = "ok";
            }
            catch (Exception e)
            { 
                Console.WriteLine(e);
            }
            return Json(strAge, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMemberAge(int MemberAgeId)//删除年龄信息
        {
            string strMsg = "fail";//定义返回

            //查询数据
            Models.S_MembershipAgeList dbMemberAge = (from tbMemberAge in MyModels.S_MembershipAgeList
                                               where tbMemberAge.MemberID == MemberAgeId
                                               select tbMemberAge).Single();
            MyModels.S_MembershipAgeList.Remove(dbMemberAge);//删除
            if (MyModels.SaveChanges() > 0)
            {
                strMsg = "success";
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

    }
    }

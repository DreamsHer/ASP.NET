using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Areas.UserLog.Controllers
{
    public class UserlogController : Controller
    {
        // GET: UserLog/Userlog
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult UserLogIndex()
        {
            return View();
        }
        public ActionResult UserLogIndex1()
        {
            return View();
        }
        public ActionResult UserLogIndex2()
        {
            return View();
        }
        public ActionResult UserLogIndex3()
        {
            return View();
        }
       public ActionResult UserLogIndex4()
        {
            return View();
        }
        public ActionResult UserLogIndex5()
        {
            return View();
        }
        /// <summary>
        /// 地址信息
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogIndex6()
        {
            return View();
        }
        public ActionResult UserLogIndex7()
        {
            return View();
        }
        //public ActionResult InsertUname()
        //{
        //    B_Unamelist KK = new B_Unamelist();
        //    KK.uname = Request.Form["uname"];

        //    KK.phone = Request.Form["phone"];

        //    KK.pwd = Common.AESEncryptHelper.Encrypt(Request.Form["pwd"]);

        //    KK.pwd = Request.Form["pwd"];


        //    if (KK.uname != null && KK.phone != null && KK.pwd != null)
        //    {
        //        MyModels.B_Unamelist.Add(KK);
        //        MyModels.SaveChanges();

        //        return Json("success", JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json("fail", JsonRequestBehavior.AllowGet);
        //    }

        //}
    }
}
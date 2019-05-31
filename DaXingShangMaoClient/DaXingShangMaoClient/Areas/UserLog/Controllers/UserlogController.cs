using DaXingShangMaoClient.Models;
using System;
using System.Web.Mvc;


namespace DaXingShangMaoClient.Areas.UserLog.Controllers
{
    public class UserlogController : Controller
    {
        // GET: UserLog/Userlog
        Models.DaXingShangMaoXiTongEntities myModels = new Models.DaXingShangMaoXiTongEntities();

        /// <summary>
        /// 注册界面
        /// </summary>
        /// <returns></returns>
        public ActionResult UserLogIndex()
        {
            return View();
        }


        //用户详细（个人或收获信息）
        public ActionResult UserLogIndex7()
        {
            return View();
        }

        public ActionResult UserLogDingDan()
        {
            return View();
        }
        public ActionResult InsertUname()
        {

            B_Unamelist KK = new B_Unamelist();
            KK.uname = Request.Form["uname"];

            KK.phone = Request.Form["phone"];

            KK.pwd = Common.AESEncryptHelper.Encrypt(Request.Form["pwd"]);

            KK.pwd = Request.Form["pwd"];

            KK.useof = Convert.ToBoolean(Request.Form["useof"]);
            if (KK.uname != null && KK.phone != null && KK.pwd != null && KK.useof != null)
            {
                myModels.B_Unamelist.Add(KK);
                myModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DuanXinYanZheng()
        {
            return View();
        }


        public ActionResult YanZhengShouJIHaoMa(string HaoMa)
        {
            string Tip = "";
            try
            {
                Common.FaSongYanZhengMa YanZhengMa = new Common.FaSongYanZhengMa();
                YanZhengMa.Page_Load(HaoMa);
                Tip = "success";

            }
            catch (Exception)
            {

                Tip = "false";
            }
            return Json(Tip, JsonRequestBehavior.AllowGet);
        }
        public ActionResult YanZhengShouJIHaoMa1(string HaoMa1)
        {
            string Tip = "";
            try
            {
                Common.FaSongYanZhengMa mobile_code = new Common.FaSongYanZhengMa();
                mobile_code.Page_Load(HaoMa1);
                Tip = "success";

            }
            catch (Exception)
            {

                Tip = "false";
            }
            return Json(Tip, JsonRequestBehavior.AllowGet);
        }


    }
}
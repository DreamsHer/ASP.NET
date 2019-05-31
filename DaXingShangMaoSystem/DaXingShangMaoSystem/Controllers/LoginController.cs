using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaXingShangMaoSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();

        /// <summary>
        /// 登录界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            string UserName = "";
            string Password = "";
            string StaffClass = "";
            bool isRember = false;

            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["user"];
            if (cookie != null)
            {
                if (cookie["UserName"] != null)
                {
                    UserName = System.Web.HttpContext.Current.Server.UrlDecode(cookie["UserName"]);
                }
                if (cookie["Password"] != null)
                {
                    Password = System.Web.HttpContext.Current.Server.UrlDecode(cookie["Password"]);
                }
                if (cookie["StaffClass"] != null)
                {
                    StaffClass = System.Web.HttpContext.Current.Server.UrlDecode(cookie["StaffClass"]);
                }
                isRember = true;
            }
            ViewBag.UserName = UserName;
            ViewBag.Password = Password;
            ViewBag.StaffClass = StaffClass;
            ViewBag.isRember = isRember;

            return View();
        }

        /// <summary>
        /// 主首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Mian()
        {
            try
            {
                string strStaffId = Session["StaffID"].ToString();
                string strUserTypeID = Session["StaffClassID"].ToString();
                string strStaffClass = Session["StaffClass"].ToString();
                string strServerTime = Session["ServerTime"].ToString();

                string strUserName = "";//用户名称

                int intStaffId = Convert.ToInt32(strStaffId);

                if (strStaffClass.Trim() == "1")
                {
                    var student = (from tbStudent in MyModels.S_StaffClassList
                                   where tbStudent.StaffID == intStaffId
                                   where tbStudent.StaffClassID==1
                                   select tbStudent).Single();

                    strUserName = student.StaffClass;
                }

                if (strStaffClass.Trim() == "2")
                {
                    var student = (from tbStudent in MyModels.S_StaffClassList
                                   where tbStudent.StaffID == intStaffId
                                   where tbStudent.StaffClassID == 2
                                   select tbStudent).Single();

                    strUserName = student.StaffClass;
                }
                if (strStaffClass.Trim() == "3")
                {
                    var student = (from tbStudent in MyModels.S_StaffClassList
                                   where tbStudent.StaffID == intStaffId
                                   where tbStudent.StaffClassID == 3
                                   select tbStudent).Single();

                    strUserName = student.StaffClass;
                }
                if (strStaffClass.Trim() == "4")
                {
                    var student = (from tbStudent in MyModels.S_StaffClassList
                                   where tbStudent.StaffID == intStaffId
                                   where tbStudent.StaffClassID == 4
                                   select tbStudent).Single();

                    strUserName = student.StaffClass;
                }
                if (strStaffClass.Trim() == "5")
                {
                    var student = (from tbStudent in MyModels.S_StaffClassList
                                   where tbStudent.StaffID == intStaffId
                                   where tbStudent.StaffClassID == 5
                                   select tbStudent).Single();

                    strUserName = student.StaffClass;
                }
                //else
                //{
                //    var student = (from tbStudent in MyModels.S_StaffClassList
                //                   where tbStudent.StaffID == intStaffId
                //                   select tbStudent).Single();

                //    strUserName = student.StaffClass;
                //}

                strServerTime = "登录时间：" + strServerTime;

                ViewBag.UserName = strUserName;
                ViewBag.serverTime = strServerTime;

                return View();
            }
            catch (Exception e)
            {
                Console.Write(e);
                //无法获取session 重定向到登录界面 重新登录
                return RedirectToAction("Login");
            }

            //return View();
        }
        
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="bStaffList"></param>
        /// <returns></returns>
        public ActionResult UserLogin()
        {
            string strMsg = "fail";//返回的字符串
            string strUserName = Request.Form["UserName"];//用户名
            string strpassword = Request.Form["Password"];//密码
            string strvalidCode = Request.Form["validCode"];//验证码
            string strStaffClass = Request.Form["StaffClass"];//登录身份
            string strrememberMe = Request.Form["rememberMe"];//记住我

            string strSessionvildeCode = "";
            if (Session["vildeCode"] != null)
            {
                strSessionvildeCode = Session["vildeCode"].ToString();
                if (strSessionvildeCode.Equals(strvalidCode, StringComparison.CurrentCultureIgnoreCase))
                {
                    try
                    {
                        //根据 UserName 查询用户
                        var dbStaff = (from tbStaff in MyModels.B_StaffList
                                      where tbStaff.UserName == strUserName.Trim()
                                      select new
                                      {
                                          tbStaff.StaffID,
                                          tbStaff.UserName,
                                          tbStaff.Password,
                                          tbStaff.UniformAuthenticationCode
                                      }).Single();

                         string strPass = Common.AESEncryptHelper.Encrypt(strpassword); //调用加密铭文

                        if (strPass == dbStaff.Password)
                        {
                            //根据帐号查询该用户的用户类型
                            var staffClass = (from tbStaff in MyModels.B_StaffList                                             
                                              join tbStaffClass in MyModels.S_StaffClassList on tbStaff.StaffClassID equals tbStaffClass.StaffClassID
                                              //where tbStaffClass.StaffClass == strStaffClass.Trim()
                                              select new
                                              {
                                                  tbStaffClass.StaffClassID,
                                                  tbStaffClass.StaffClass
                                              }).ToList();
                            if (staffClass.Count > 0)
                            {
                                //获取用户类型ID
                                int userTypeId = staffClass[0].StaffClassID;
                                //获取用户类型名称
                                string userTypeName = staffClass[0].StaffClass.Trim();

                                //设置cookie
                                if (strrememberMe == "true")
                                {
                                    HttpCookie cookie = new HttpCookie("user");
                                    cookie["UserName"] = strUserName;
                                    cookie["Password"] = strpassword;
                                    cookie["StaffClass"] = strStaffClass;
                                    cookie.Expires = DateTime.Now.AddDays(7);
                                    Response.Cookies.Add(cookie);
                                }
                                else
                                {
                                    HttpCookie cookie = new HttpCookie("user");
                                    cookie.Expires = DateTime.Now.AddDays(-1);
                                    Response.Cookies.Add(cookie);
                                }

                                string strTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                                // 设置session
                                Session["StaffID"] = dbStaff.StaffID; // 传递 UserID   
                                Session["StaffClassID"] = userTypeId;// 传递 UserTypeID  
                                Session["ServerTime"] = strTime;//登录时间
                                Session["StaffClass"] = strStaffClass;//用户的类型 

                                strMsg = "login";
                            }
                            else
                            {
                                strMsg = "staffClassErro";
                            }
                        }
                        else
                        {
                            strMsg = "passwordErro";
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    strMsg = "vlodeCodeErro";
                }
            }
            else
            {
                strMsg = "loginErro";
            }

            return Json(strMsg, JsonRequestBehavior.AllowGet);          
        }

        /// <summary>
        /// 获得登录时间（分钟）
        /// </summary>
        /// <returns></returns>
        public ActionResult GetLoginTime()
        {
            try
            {
                string loginTime = Session["ServerTime"].ToString();//获取Session中的时间
                DateTime dateTimeLogin = Convert.ToDateTime(loginTime);//转为datetime
                DateTime dateTimeNow = DateTime.Now;//获取当前时间
                TimeSpan time = dateTimeNow - dateTimeLogin;//求时间差
                double minute = time.TotalMinutes;
                int intMinute = Convert.ToInt32(minute);

                return Json(intMinute, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
               
                return Redirect("/Login/Login");
            }
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <returns></returns>
        public ActionResult ValideCode()
        {
            string strVildeCode = Common.validCodeUtils.GetRandomCode(4);//获取随机字符串
            Session["vildeCode"] = strVildeCode;//放入session
            byte[] vildeImage = Common.validCodeUtils.CreateImage(strVildeCode);//byte[]//根据验证码产生图片
            return File(vildeImage, @"image/jpeg");//把图片返回到视图            
        }

        /// <summary>
        /// 清空session
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
            Session.Clear();
            return Json(true, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 员工编号
        /// </summary>
        /// <returns></returns>
        public ActionResult getEmpCodef()
        {
            string Std = "";
            var listy = (from tbem in MyModels.B_StaffList
                         orderby tbem.StaffCode
                         select tbem).ToList();
            if (listy.Count > 0)
            {
                int intcoun = listy.Count;
                B_StaffList mymodell = listy[intcoun - 1];
                int inemp = Convert.ToInt32(mymodell.StaffCode.Substring(1, 8));
                inemp++;
                Std = inemp.ToString();
                for (int i = 0; i < 2; i++)
                {
                    Std = Std.Length < 2 ? "0" + Std : Std;
                }
            }
            else
            {
                Std = "01";
            }
            return Json(Std, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 员工类型
        /// </summary>
        /// <returns></returns>
        public ActionResult SelectStaffClassID()
        {
            List<SelectVo> listStaffClass = new List<SelectVo>();
            SelectVo selectVo = new SelectVo
            {
                id = 0,
                text = "---请选择类型----"
            };
            listStaffClass.Add(selectVo);

            List<SelectVo> listPriceTypeID = (from tbStaffClass in MyModels.S_StaffClassList
                                              select new SelectVo
                                              {
                                                  id = tbStaffClass.StaffClassID,
                                                  text = tbStaffClass.StaffClass.Trim()
                                              }).ToList();

            listStaffClass.AddRange(listPriceTypeID);

            return Json(listStaffClass, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <returns></returns>
        public ActionResult XinZengYongHuXinXi()
        {
            B_StaffList KK = new B_StaffList();
            KK.StaffName = Request.Form["StaffName"];
            KK.StaffCode = Request.Form["StaffCode"];
            KK.UserName = Request.Form["UserName"];

            KK.Password = Common.AESEncryptHelper.Encrypt(Request.Form["UniformAuthenticationCode"]);
           
            KK.UniformAuthenticationCode = Request.Form["UniformAuthenticationCode"];
            KK.StaffClassID = Convert.ToInt32(Request.Form["StaffClassID"]);

            if (KK.StaffName !=null && KK.StaffCode != null && KK.UserName != null && KK.UniformAuthenticationCode != null && KK.StaffClassID != null)
            {
                MyModels.B_StaffList.Add(KK);
                MyModels.SaveChanges();

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }


        }


        //模块权
        public ActionResult MoKuaiQuanXianSelect(int UserId)
        {
            var caozuoid = (from tdUser in MyModels.B_StaffList//员工
                            join tdStaffClass in MyModels.S_StaffClassList on tdUser.StaffClassID equals tdStaffClass.StaffClassID//员工类型
                            join tdStaffPower in MyModels.S_StaffPowerList on tdStaffClass.StaffClassID equals tdStaffPower.StaffClassID//权限
                            join tdModular in MyModels.S_ModuleList on tdStaffPower.ModuleID equals tdModular.ModuleID//模块
                            where tdUser.StaffID == UserId
                            select new
                            {
                                ID = tdModular.ModuleID,
                                Name = tdModular.ModuleName.Trim()
                            }).ToList();

            return Json(caozuoid, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 后台首页
        /// </summary>
        /// <returns></returns>       
        public ActionResult ShuoYe()
        {
            return View();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaJiChuXinXi.Controllers
{
    public class HuiYuanXinXiXianMuController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: HuiYuanKaJiChuXinXi/HuiYuanXinXiXianMu
        public ActionResult HuiYuanXinXiShuXin()
        {
            return View();
        }
        #region 查询
        public ActionResult SelectInfo(BsgridPage bsgridPage)//查询会员证件信息
        {
            var linqItme = from tbMember in MyModels.S_MemberCredentialsType
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberCredentialsType> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberCredentialsType> bsgrid = new Vo.Bsgrid<Models.S_MemberCredentialsType>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectProfession(BsgridPage bsgridPage)//查询会员职业信息
        {
            var linqItme = from tbMember in MyModels.S_MemberProfession
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberProfession> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberProfession> bsgrid = new Vo.Bsgrid<Models.S_MemberProfession>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectMemberFamilyIncome(BsgridPage bsgridPage)//查询会员家庭收入信息
        {
            var linqItme = from tbMember in MyModels.S_MemberMemberFamilyIncome
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberMemberFamilyIncome> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberMemberFamilyIncome> bsgrid = new Vo.Bsgrid<Models.S_MemberMemberFamilyIncome>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectMemberEd(BsgridPage bsgridPage)//查询会员学历信息
        {
            var linqItme = from tbMember in MyModels.S_MemberMemberEd
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberMemberEd> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberMemberEd> bsgrid = new Vo.Bsgrid<Models.S_MemberMemberEd>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectTimeToMall(BsgridPage bsgridPage)//查询会员至商场时间信息
        {
            var linqItme = from tbMember in MyModels.S_MemberTimeToMall
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberTimeToMall> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberTimeToMall> bsgrid = new Vo.Bsgrid<Models.S_MemberTimeToMall>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectVehicle(BsgridPage bsgridPage)//查询会员信息
        {
            var linqItme = from tbMember in MyModels.S_MemberVehicle
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberVehicle> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberVehicle> bsgrid = new Vo.Bsgrid<Models.S_MemberVehicle>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectFamilyMember(BsgridPage bsgridPage)//查询会员家庭成员信息
        {
            var linqItme = from tbMember in MyModels.S_MemberFamilyMember
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberFamilyMember> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberFamilyMember> bsgrid = new Vo.Bsgrid<Models.S_MemberFamilyMember>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectHobbiesInterests(BsgridPage bsgridPage)//查询会员兴趣爱好信息
        {
            var linqItme = from tbMember in MyModels.S_MemberHobbiesInterests
                           orderby tbMember.MemberID
                           select tbMember;
            //查询总行数
            int intTotalRow = linqItme.Count();

            List<Models.S_MemberHobbiesInterests> listnTypeTables = linqItme
                                                         .Skip(bsgridPage.GetStartIndex())
                                                         .Take(bsgridPage.pageSize)
                                                         .ToList();
            Bsgrid<Models.S_MemberHobbiesInterests> bsgrid = new Vo.Bsgrid<Models.S_MemberHobbiesInterests>();//实例化 Bsgrid 的返回实体类
            bsgrid.success = true;//返回状态
            bsgrid.totalRows = intTotalRow;//总行数
            bsgrid.curPage = bsgridPage.curPage;//当前页
            bsgrid.data = listnTypeTables;//当前页的数据
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新增
        public ActionResult InsertCredentialsType(string CredentialsType)//新增会员证件信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberCredentialsType myCredentialsType = new Models.S_MemberCredentialsType();
                //给表对象属性赋值
                myCredentialsType.CredentialsType = CredentialsType.Trim();
                //把表对象实体模型中
                MyModels.S_MemberCredentialsType.Add(myCredentialsType);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertProfessione(string Profession)//新增会员职业信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberProfession myProfession = new Models.S_MemberProfession();
                //给表对象属性赋值
                myProfession.Profession = Profession.Trim();
                //把表对象实体模型中
                MyModels.S_MemberProfession.Add(myProfession);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertMemberFamilyIncome(string MemberFamilyIncome)//新增会员家庭收入
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberMemberFamilyIncome myMemberFamilyIncome = new Models.S_MemberMemberFamilyIncome();
                //给表对象属性赋值
                myMemberFamilyIncome.MemberFamilyIncome = MemberFamilyIncome.Trim();
                //把表对象实体模型中
                MyModels.S_MemberMemberFamilyIncome.Add(myMemberFamilyIncome);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertMemberEd(string MemberEd)//新增会员学历信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberMemberEd myMemberEd = new Models.S_MemberMemberEd();
                //给表对象属性赋值
                myMemberEd.MemberEd = MemberEd.Trim();
                //把表对象实体模型中
                MyModels.S_MemberMemberEd.Add(myMemberEd);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertTimeToMall(string TimeToMall)//新增会员至商场时间信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberTimeToMall myMemberTimeToMall = new Models.S_MemberTimeToMall();
                //给表对象属性赋值
                myMemberTimeToMall.TimeToMall = TimeToMall.Trim();
                //把表对象实体模型中
                MyModels.S_MemberTimeToMall.Add(myMemberTimeToMall);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertVehicle(string Vehicle)//新增会员交通工具信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberVehicle myVehicle = new Models.S_MemberVehicle();
                //给表对象属性赋值
                myVehicle.Vehicle = Vehicle.Trim();
                //把表对象实体模型中
                MyModels.S_MemberVehicle.Add(myVehicle);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertFamilyMember(string FamilyMember)//新增会员家庭成员信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberFamilyMember myFamilyMember = new Models.S_MemberFamilyMember();
                //给表对象属性赋值
                myFamilyMember.FamilyMember = FamilyMember.Trim();
                //把表对象实体模型中
                MyModels.S_MemberFamilyMember.Add(myFamilyMember);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }

        public ActionResult InsertHobbiesInterests(string HobbiesInterests)//新增会员兴趣爱好信息
        {
            string strInfo = "falied";
            try
            {
                //实例化对象

                Models.S_MemberHobbiesInterests myHobbiesInterests = new Models.S_MemberHobbiesInterests();
                //给表对象属性赋值
                myHobbiesInterests.HobbiesInterests = HobbiesInterests.Trim();
                //把表对象实体模型中
                MyModels.S_MemberHobbiesInterests.Add(myHobbiesInterests);
                //将对象实体插入到数据库中
                MyModels.SaveChanges();
                strInfo = "ok";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return Json(strInfo, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除 
        public ActionResult DeleteMemberid(int MemberId)//删除证件信息
        {
              if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberCredentialsType.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberCredentialsType.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        public ActionResult DeleteProfession(int MemberId)//删除会员职业信息
        {
            if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberProfession.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberProfession.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        public ActionResult DeleteMemberMemberFamilyIncome(int MemberId)//删除会员家庭收入信息
        {
            if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberMemberFamilyIncome.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberMemberFamilyIncome.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        public ActionResult DeleteMemberEd(int MemberId)//删除会员学历信息
        {
            if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberMemberEd.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberMemberEd.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        public ActionResult DeleteTimeToMall(int MemberId)//删除至商场时间信息
        {
            if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberTimeToMall.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberTimeToMall.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        public ActionResult DeleteVehicle(int MemberId)//删除交通工具信息
        {
            if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberVehicle.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberVehicle.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        public ActionResult DeleteFamilyMember(int MemberId)//删除家庭成员信息
        {
            if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberFamilyMember.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberFamilyMember.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        public ActionResult DeleteHobbiesInterests(int MemberId)//删除兴趣爱好信息
        {
            if (MemberId > 0)
            {
                try
                {
                    var deleteEmp = MyModels.S_MemberHobbiesInterests.Where(m => m.MemberID == MemberId).Single();
                    MyModels.S_MemberHobbiesInterests.Remove(deleteEmp);
                    MyModels.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                catch (Exception)
                {
                    return Json("fail", JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json("fail");
            }
        }
        #endregion
    }
}
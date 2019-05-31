using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.HuiYuanKaXinXiGuanLi.Controllers
{
    public class HuiYuanDangAnLuRuController : Controller
    {
        // GET: HuiYuanKaXinXiGuanLi/HuiYuanDangAnLuRu
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        public ActionResult DangAn()
        {
            return View();
        }
        public ActionResult XuanZheKa()
        {
            return View();
        }

        #region 查询会员类型
        public ActionResult SelectKaLei()
        {
            try
            {
                var selectkazhong = (from tbKaLei in MyModels.S_MembershipCardTypeList
                                     orderby tbKaLei.MembershipCardTypeID
                                     select new
                                     {
                                         Name = tbKaLei.MembershipCardTypeMC
                                     });
                return Json(selectkazhong, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 下拉框
        public ActionResult SelectZhengjianleixing()//证件类型
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.CredentialsType
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectLocation()//所在区域
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.Location
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectProfession()//职业
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.Profession
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectTimeToMall()//至商场时间
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.TimeToMall
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectMemberEd()//教育程度
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.MemberEd
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectMemberFamilyIncome()//家庭收入
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.MemberFamilyIncome
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectVehicle()//交通工具
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.Vehicle
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SelectFamilyMember()//家庭成员
        {
            List<SelectVo> listAcademe = (from tbAcademe in MyModels.S_MemberInfoList
                                          select new SelectVo
                                          {
                                              id = tbAcademe.AreaID,
                                              text = tbAcademe.FamilyMember
                                          }).ToList();
            listAcademe = Common.Tools.SetSelectJson(listAcademe);//设置selectjson
            return Json(listAcademe, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 信息获取
        public ActionResult SelectHeellingInfo(BsgridPage bsgridPage)//选择信息
        {
            var listGoods = (from tbMemberInfo in MyModels.S_MemberInfoList
                             orderby tbMemberInfo.MemberID
                             select new MemberInfoVo
                             {
                                 MemberID = tbMemberInfo.MemberID,//会员ID
                                 XingMing  = tbMemberInfo.XingMing ,//姓名
                                 CredentialsType = tbMemberInfo.CredentialsType,//证件类型
                                 CredentialsNumber = tbMemberInfo.CredentialsNumber,//证件号码
                                 PostalAddress = tbMemberInfo.PostalAddress,//通讯地址
                                 Location = tbMemberInfo.Location,//所在区域
                                 Postalcode = tbMemberInfo.Postalcode,//邮政编码
                                 dianziyoujian = tbMemberInfo.dianziyoujian,//电子邮件
                                 Profession = tbMemberInfo.Profession,//职业
                                 OfficePhone = tbMemberInfo.OfficePhone,//办公电话
                                 PrivateTelephone  = tbMemberInfo.PrivateTelephone ,//私人电话
                                 CellphoneNumber  = tbMemberInfo.CellphoneNumber,//手机号码
                                 TimeToMall = tbMemberInfo.TimeToMall,//至商场时间
                                 MemberEd = tbMemberInfo.MemberEd,//教育程度
                                 MemberFamilyIncome = tbMemberInfo.MemberFamilyIncome,//家庭收入
                                 Vehicle  = tbMemberInfo.Vehicle,//交通工具
                                 FamilyMember  = tbMemberInfo.FamilyMember,//家庭成员
                                 CardNumberChange  = tbMemberInfo.CardNumberChange,//卡号变更
                                 CommentsOnAForm = tbMemberInfo.CommentsOnAForm,//备注
                             });
            int intTotalRow = listGoods.Count();//总行数
            List<MemberInfoVo> listNotices = listGoods.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<MemberInfoVo> bsgrid = new Bsgrid<MemberInfoVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectHuoQuKaXinXi(int HTID)//获取信息
        {
            if (HTID > 0)
            {
                var listGoods = (from tbMemberInfo in MyModels.S_MemberInfoList
                                 where tbMemberInfo.MemberID == HTID
                                 select new Vo.MemberInfoVo
                                 {
                                     MemberID = tbMemberInfo.MemberID,//会员ID
                                     XingMing = tbMemberInfo.XingMing,//姓名
                                     CredentialsType = tbMemberInfo.CredentialsType,//证件类型
                                     CredentialsNumber = tbMemberInfo.CredentialsNumber,//证件号码
                                     PostalAddress = tbMemberInfo.PostalAddress,//通讯地址
                                     Location = tbMemberInfo.Location,//所在区域
                                     Postalcode = tbMemberInfo.Postalcode,//邮政编码
                                     dianziyoujian = tbMemberInfo.dianziyoujian,//电子邮件
                                     Profession = tbMemberInfo.Postalcode,//职业
                                     OfficePhone = tbMemberInfo.OfficePhone,//办公电话
                                     PrivateTelephone = tbMemberInfo.PrivateTelephone,//私人电话
                                     CellphoneNumber = tbMemberInfo.CellphoneNumber,//手机号码
                                     TimeToMall = tbMemberInfo.TimeToMall,//至商场时间
                                     MemberEd = tbMemberInfo.MemberEd,//教育程度
                                     MemberFamilyIncome = tbMemberInfo.MemberFamilyIncome,//家庭收入
                                     Vehicle = tbMemberInfo.Vehicle,//交通工具
                                     FamilyMember = tbMemberInfo.FamilyMember,//家庭成员
                                     CardNumberChange = tbMemberInfo.CardNumberChange,//卡号变更
                                     CommentsOnAForm = tbMemberInfo.CommentsOnAForm,//备注
                                 }).Single();
                return Json(listGoods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }
        #endregion

        #region 新增信息
        public ActionResult Select()
        {
            var listSeltct = (from tbSelect in MyModels.S_MemberInfoList
                              select new Vo.MemberInfoVo
                              {
                                  XingMing = tbSelect.XingMing,//姓名
                                  CredentialsType = tbSelect.CredentialsType,//证件类型
                                  CredentialsNumber = tbSelect.CredentialsNumber,//证件号码
                                  PostalAddress = tbSelect.PostalAddress,//通讯地址
                                  Location = tbSelect.Location,//所在区域
                                  Postalcode = tbSelect.Postalcode,//邮政编码
                                  dianziyoujian = tbSelect.dianziyoujian,//电子邮件
                                  Profession = tbSelect.Profession,//职业
                                  OfficePhone = tbSelect.OfficePhone,//办公电话
                                  PrivateTelephone = tbSelect.PrivateTelephone,//私人电话
                                  CellphoneNumber = tbSelect.CellphoneNumber,//手机号码
                                  TimeToMall = tbSelect.TimeToMall,//至商场时间
                                  MemberEd = tbSelect.MemberEd,//教育程度
                                  MemberFamilyIncome = tbSelect.MemberFamilyIncome,//家庭收入
                                  Vehicle = tbSelect.Vehicle,//交通工具
                                  FamilyMember = tbSelect.FamilyMember,//家庭成员
                                  CardNumberChange = tbSelect.CardNumberChange,//卡号变更
                                  CommentsOnAForm = tbSelect.CommentsOnAForm,//备注
                              }).ToList();
            return Json(listSeltct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult btnInsert()//保存
        {
            Models.S_MemberInfoList Delst = new Models.S_MemberInfoList();
            Delst.XingMing = Request.Form["XingMing"];
            Delst.CredentialsType = Request.Form["CredentialsType"];
            Delst.CredentialsNumber = Request.Form["CredentialsNumber"];
            Delst.PostalAddress = Request.Form["PostalAddress"];
            Delst.Location = Request.Form["Location"];
            Delst.Postalcode = Request.Form["Postalcode"];
            Delst.dianziyoujian = Request.Form["dianziyoujian"];
            Delst.Profession = Request.Form["Profession"];
            Delst.OfficePhone = Request.Form["OfficePhone"];
            Delst.PrivateTelephone = Request.Form["PrivateTelephone"];
            Delst.CellphoneNumber = Request.Form["CellphoneNumber"];
            Delst.TimeToMall = Request.Form["TimeToMall"];
            Delst.MemberEd = Request.Form["MemberEd"];
            Delst.MemberFamilyIncome = Request.Form["MemberFamilyIncome"];
            Delst.Vehicle = Request.Form["Vehicle"];
            Delst.FamilyMember = Request.Form["FamilyMember"];
            Delst.CardNumberChange = Request.Form["CardNumberChange"];
            Delst.CommentsOnAForm = Request.Form["CommentsOnAForm"];
            if (Delst.XingMing != null && Delst.CredentialsType != null && Delst.CredentialsNumber != null && Delst.PostalAddress != null && Delst.Location != null && Delst.Postalcode != null && Delst.dianziyoujian != null && Delst.Profession != null && Delst.OfficePhone != null && Delst.PrivateTelephone != null && Delst.CellphoneNumber != null && Delst.TimeToMall != null && Delst.MemberEd != null && Delst.MemberFamilyIncome != null && Delst.Vehicle != null && Delst.FamilyMember != null && Delst.CardNumberChange != null && Delst.CommentsOnAForm != null)
            {
                MyModels.S_MemberInfoList.Add(Delst);
                MyModels.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("fail", JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult DeleteMember(int MemberId)//删除信息
        {
            string strMsg = "fail";//定义返回

            //查询数据
            Models.S_MemberInfoList dbMember = (from tbMember in MyModels.S_MemberInfoList
                                                  where tbMember.MemberID == MemberId
                                                   select tbMember).Single();
            MyModels.S_MemberInfoList.Remove(dbMember);//删除
            if (MyModels.SaveChanges() > 0)
            {
                strMsg = "success";
            }
            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }
    }
}
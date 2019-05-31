using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DaXingShangMaoSystem.Models;
using DaXingShangMaoSystem.Vo;

namespace DaXingShangMaoSystem.Areas.SupplierManagement.Controllers
{
    public class OperationControlController : Controller
    {
        Models.DaXingShangMaoXiTongEntities MyModels = new Models.DaXingShangMaoXiTongEntities();
        // GET: SupplierManagement/OperationControl
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
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
            var listEmp = (from tbEmp in MyModels.B_SupplierList
                           orderby tbEmp.SupplierNumber
                           select tbEmp).ToList();
            if (listEmp.Count > 0)
            {
                int intCount = listEmp.Count;
                B_SupplierList modelEmp = listEmp[intCount - 1];
                int intEmp = Convert.ToInt32(modelEmp.SupplierNumber.Substring(1, 6));
                intEmp++;
                Code = intEmp.ToString();
                for (int i = 0; i < 6; i++)
                {
                    Code = Code.Length < 6 ? "0" + Code : Code;
                }
                Code = "" + Code;
            }

            else
            {
                Code = "000000";
            }
            return Json(Code, JsonRequestBehavior.AllowGet);
        }
        //下拉框数据绑定
        public ActionResult selectSupplierRank()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_SupplierRankList
                                   select new
                                   {
                                       id = tbSupplierRank.SupplierRankID,
                                       text = tbSupplierRank.SupplierRankName
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectEnterpriseNature()
        {
            var lisEnterpriseNature = (from tbEnterpriseNature in MyModels.S_EnterpriseNatureList
                                       select new
                                       {
                                           id = tbEnterpriseNature.EnterpriseNatureID,
                                           text = tbEnterpriseNature.EnterpriseNatureName
                                       }).ToList();
            return Json(lisEnterpriseNature, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectEnterpriseType()
        {
            var lisEnterpriseType = (from tbEnterpriseType in MyModels.S_EnterpriseTypeList
                                     select new
                                     {
                                         id = tbEnterpriseType.EnterpriseTypeID,
                                         text = tbEnterpriseType.EnterpriseTypeName
                                     }).ToList();
            return Json(lisEnterpriseType, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectPayment()
        {
            var lisPayment = (from tbPayment in MyModels.S_PaymentList
                              select new
                              {
                                  id = tbPayment.PaymentID,
                                  text = tbPayment.PaymentMC
                              }).ToList();
            return Json(lisPayment, JsonRequestBehavior.AllowGet);
        }
        public ActionResult selectCompany()
        {
            var lisCompany = (from tbCompany in MyModels.S_CompanyList
                              select new
                              {
                                  id = tbCompany.CompanyID,
                                  text = tbCompany.CompanyMC
                              }).ToList();
            return Json(lisCompany, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 单位性质
        /// </summary>
        /// <returns></returns>
        public ActionResult selectUnitCharacterName()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_UnitCharacterList
                                   select new
                                   {
                                       id = tbSupplierRank.UnitCharacterID,
                                       text = tbSupplierRank.UnitCharacterName
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 合同类型
        /// </summary>
        /// <returns></returns>
        public ActionResult selectAdjustAccountsFashion()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_AgreementTypeList
                                   select new
                                   {
                                       id = tbSupplierRank.AgreementTypeID,
                                       text = tbSupplierRank.AgreementTypeName
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 状态类型
        /// </summary>
        /// <returns></returns>

        public ActionResult selectAgreementState()
        {
            var lisSupplierRank = (from tbSupplierRank in MyModels.S_AgreementStateList
                                   select new
                                   {
                                       id = tbSupplierRank.AgreementStateID,
                                       text = tbSupplierRank.AgreementState
                                   }).ToList();
            return Json(lisSupplierRank, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 关联属性
        /// </summary>
        /// <returns></returns>

        public ActionResult SelectAgreementList(BsgridPage bsgridPage)
        {
            try
            {
                var AgreementList = (from tbAgreement in MyModels.B_AgreementList
                                     join tbAdjustAccountsFashionID in MyModels.S_AdjustAccountsFashionList on tbAgreement.AdjustAccountsFashionID equals tbAdjustAccountsFashionID.AdjustAccountsFashionID
                                     join tbAgreementStateID in MyModels.S_AgreementStateList on tbAgreement.AgreementStateID equals tbAgreementStateID.AgreementStateID
                                     select new AgreementVo
                                     {
                                         AdjustAccountsFashionID = tbAgreement.AdjustAccountsFashionID,
                                         AdjustAccountsFashion = tbAdjustAccountsFashionID.AdjustAccountsFashion,
                                         StartTime = tbAgreement.AgreementBeginTime.ToString(),
                                         AgreementCode = tbAgreement.AgreementCode,
                                         AgreementID = tbAgreement.AgreementID,
                                         EndTiem = tbAgreement.AgreementFinishTime.ToString(),
                                         ReleaseTimeStr = tbAgreement.ConclusionTime.ToString(),
                                         AgreementState = tbAgreementStateID.AgreementState,
                                         GoodsID = tbAgreement.GoodsID
                                     }).OrderBy(m => m.AgreementCode).Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();
                int totalRow = AgreementList.Count();
                Bsgrid<AgreementVo> bsgrid = new Bsgrid<AgreementVo>();
                bsgrid.success = true;
                bsgrid.totalRows = totalRow;
                bsgrid.curPage = bsgridPage.curPage;
                bsgrid.data = AgreementList;
                return Json(bsgrid, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }


        /// <summary>
        /// 新增供应商
        /// </summary>
        /// <param name="SupplierList"></param>
        /// <param name="SupplierInformationList"></param>
        /// <param name="EnterpriseNatureList"></param>
        /// <returns></returns>


        public ActionResult SelectGoods(B_SupplierList SupplierListgf)
        {
            B_SupplierList SupplierList = new B_SupplierList();
            string strMag = "fali";
            ReturnJsonVo returnJson = new ReturnJsonVo();

            try
            {
                SupplierList.SupplierNumber = SupplierListgf.SupplierNumber;
                SupplierList.SupplierName = SupplierListgf.SupplierName;
                SupplierList.SupplierCHName = SupplierListgf.SupplierCHName;
                SupplierList.SupplierEnglishName = SupplierListgf.SupplierEnglishName;
                SupplierList.SupplierAbbreviation = SupplierListgf.SupplierAbbreviation;
                SupplierList.SupplierPTCode = SupplierListgf.SupplierPTCode;
                SupplierList.SupplierDependency = SupplierListgf.SupplierDependency;
                SupplierList.SupplierRegisteredAddress = SupplierListgf.SupplierRegisteredAddress;
                SupplierList.SupplierRegisteredEnglish = SupplierListgf.SupplierRegisteredEnglish;

                SupplierList.PaymentID = Convert.ToInt32(SupplierListgf.PaymentID);
                SupplierList.CompanyID = Convert.ToInt32(SupplierListgf.CompanyID);
                SupplierList.SupplierRankID = Convert.ToInt32(SupplierListgf.SupplierRankID);
                SupplierList.UnitCharacterID = Convert.ToInt32(SupplierListgf.UnitCharacterID);
                SupplierList.AgreementTypeID = Convert.ToInt32(SupplierListgf.AgreementTypeID);
                SupplierList.AgreementStateID = Convert.ToInt32(SupplierListgf.AgreementStateID);


                SupplierList.SupplierInformationName = SupplierListgf.SupplierInformationName;
                SupplierList.SupplierTelephone = SupplierListgf.SupplierTelephone;
                SupplierList.SupplierContacts = SupplierListgf.SupplierContacts;
                SupplierList.SupplierPost = SupplierListgf.SupplierPost;
                SupplierList.SupplierEmail = SupplierListgf.SupplierEmail;
                SupplierList.SupplierZipCode = SupplierListgf.SupplierZipCode;
                SupplierList.SupplierPortraiture = SupplierListgf.SupplierPortraiture;
                SupplierList.SupplierAddress = SupplierListgf.SupplierAddress;
                SupplierList.SupplierURL = SupplierListgf.SupplierURL;

                SupplierList.EnterpriseBank = SupplierListgf.EnterpriseBank;
                SupplierList.EnterpriseNumber = SupplierListgf.EnterpriseNumber;
                SupplierList.EnterpriseNatureID = Convert.ToInt32(SupplierListgf.EnterpriseNatureID);
                SupplierList.NationalTaxNumber = SupplierListgf.NationalTaxNumber;
                SupplierList.EnterpriseTypeID = Convert.ToInt32(SupplierListgf.EnterpriseTypeID);
                SupplierList.LocalTaxNumber = SupplierListgf.LocalTaxNumber;
                SupplierList.EnterprisePassword = SupplierListgf.EnterprisePassword;


                SupplierList.Registrant = SupplierListgf.Registrant;
                SupplierList.Registranttime = Convert.ToDateTime(SupplierListgf.Registranttime);

                if (SupplierList.SupplierNumber != "")
                {

                    MyModels.B_SupplierList.Add(SupplierList);
                    MyModels.SaveChanges();
                    strMag = "success";

                }

            }
            catch (Exception)
            {

                return Json(strMag, JsonRequestBehavior.AllowGet);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="SupplierListgf"></param>
        /// <returns></returns>
        public ActionResult UpdateJingXiao(B_SupplierList SupplierListgf)
        {
            string strMag = "fail";
            B_SupplierList SupplierList = new B_SupplierList();
            try
            {
                SupplierList.SupplierID = Convert.ToInt32(SupplierListgf.SupplierID);

                SupplierList.SupplierNumber = SupplierListgf.SupplierNumber;
                SupplierList.SupplierName = SupplierListgf.SupplierName;
                SupplierList.SupplierCHName = SupplierListgf.SupplierCHName;
                SupplierList.SupplierEnglishName = SupplierListgf.SupplierEnglishName;
                SupplierList.SupplierAbbreviation = SupplierListgf.SupplierAbbreviation;
                SupplierList.SupplierPTCode = SupplierListgf.SupplierPTCode;
                SupplierList.SupplierDependency = SupplierListgf.SupplierDependency;
                SupplierList.SupplierRegisteredAddress = SupplierListgf.SupplierRegisteredAddress;
                SupplierList.SupplierRegisteredEnglish = SupplierListgf.SupplierRegisteredEnglish;

                SupplierList.PaymentID = Convert.ToInt32(SupplierListgf.PaymentID);
                SupplierList.CompanyID = Convert.ToInt32(SupplierListgf.CompanyID);
                SupplierList.SupplierRankID = Convert.ToInt32(SupplierListgf.SupplierRankID);
                SupplierList.UnitCharacterID = Convert.ToInt32(SupplierListgf.UnitCharacterID);
                SupplierList.AgreementTypeID = Convert.ToInt32(SupplierListgf.AgreementTypeID);
                SupplierList.AgreementStateID = Convert.ToInt32(SupplierListgf.AgreementStateID);


                SupplierList.SupplierInformationName = SupplierListgf.SupplierInformationName;
                SupplierList.SupplierTelephone = SupplierListgf.SupplierTelephone;
                SupplierList.SupplierContacts = SupplierListgf.SupplierContacts;
                SupplierList.SupplierPost = SupplierListgf.SupplierPost;
                SupplierList.SupplierEmail = SupplierListgf.SupplierEmail;
                SupplierList.SupplierZipCode = SupplierListgf.SupplierZipCode;
                SupplierList.SupplierPortraiture = SupplierListgf.SupplierPortraiture;
                SupplierList.SupplierAddress = SupplierListgf.SupplierAddress;
                SupplierList.SupplierURL = SupplierListgf.SupplierURL;

                SupplierList.EnterpriseBank = SupplierListgf.EnterpriseBank;
                SupplierList.EnterpriseNumber = SupplierListgf.EnterpriseNumber;
                SupplierList.EnterpriseNatureID = Convert.ToInt32(SupplierListgf.EnterpriseNatureID);
                SupplierList.NationalTaxNumber = SupplierListgf.NationalTaxNumber;
                SupplierList.EnterpriseTypeID = Convert.ToInt32(SupplierListgf.EnterpriseTypeID);
                SupplierList.LocalTaxNumber = SupplierListgf.LocalTaxNumber;
                SupplierList.EnterprisePassword = SupplierListgf.EnterprisePassword;


                SupplierList.Registrant = SupplierListgf.Registrant;
                SupplierList.Registranttime = Convert.ToDateTime(SupplierListgf.Registranttime);
                //SupplierList.GoodsStatusID = Convert.ToInt32(SupplierListgf.GoodsStatusID);

                SupplierList.Executor = SupplierListgf.Executor;
                SupplierList.Executortime = Convert.ToDateTime(SupplierListgf.Executortime);

                //SupplierList.Auditor = SupplierListgf.Auditor;
                //SupplierList.Auditortime = Convert.ToDateTime(SupplierListgf.Auditortime);

                if (SupplierList.SupplierNumber != "" && SupplierList.SupplierID > 0)
                {

                    MyModels.Entry(SupplierList).State = System.Data.Entity.EntityState.Modified;
                    MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                    strMag = "success";

                }
            }
            catch (Exception)
            {
                return Json(strMag, JsonRequestBehavior.AllowGet);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 查询供货商
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <returns></returns>
        public ActionResult selectA(BsgridPage bsgridPage)
        {
            var listAgreementWithhold = (from tbSupplierList in MyModels.B_SupplierList
                                         join tbUnitCharacterList in MyModels.S_UnitCharacterList on tbSupplierList.UnitCharacterID equals tbUnitCharacterList.UnitCharacterID
                                         join tbSupplierRankList in MyModels.S_SupplierRankList on tbSupplierList.SupplierRankID equals tbSupplierRankList.SupplierRankID
                                         join tbAgreementTypeList in MyModels.S_AgreementTypeList on tbSupplierList.AgreementTypeID equals tbAgreementTypeList.AgreementTypeID
                                         join tbAgreementStateList in MyModels.S_AgreementStateList on tbSupplierList.AgreementStateID equals tbAgreementStateList.AgreementStateID
                                         join tbPaymentList in MyModels.S_PaymentList on tbSupplierList.PaymentID equals tbPaymentList.PaymentID
                                         join tbCompany in MyModels.S_CompanyList on tbSupplierList.CompanyID equals tbCompany.CompanyID
                                         join tbEnterpriseNature in MyModels.S_EnterpriseNatureList on tbSupplierList.EnterpriseNatureID equals tbEnterpriseNature.EnterpriseNatureID
                                         join tbEnterpriseTypeID in MyModels.S_EnterpriseTypeList on tbSupplierList.EnterpriseTypeID equals tbEnterpriseTypeID.EnterpriseTypeID

                                         orderby tbSupplierList.SupplierID
                                         select new SupplierListVo
                                         {
                                             SupplierID = tbSupplierList.SupplierID,
                                             SupplierName = tbSupplierList.SupplierName,
                                             SupplierNumber = tbSupplierList.SupplierNumber,
                                             SupplierCHName = tbSupplierList.SupplierCHName,
                                             SupplierAbbreviation = tbSupplierList.SupplierAbbreviation,
                                             SupplierEnglishName = tbSupplierList.SupplierEnglishName,
                                             SupplierPTCode = tbSupplierList.SupplierPTCode,
                                             SupplierDependency = tbSupplierList.SupplierDependency,
                                             SupplierRegisteredAddress = tbSupplierList.SupplierRegisteredAddress,
                                             UnitCharacterID = tbSupplierList.UnitCharacterID,
                                             UnitCharacterName = tbUnitCharacterList.UnitCharacterName,
                                             //SupplierReturnTypeID=tbSupplierList.SupplierReturnTypeID,
                                             PaymentID = tbSupplierList.PaymentID,
                                             PaymentMC = tbPaymentList.PaymentMC,
                                             CompanyID = tbSupplierList.CompanyID,
                                             CompanyMC = tbCompany.CompanyMC,
                                             SupplierRegisteredEnglish = tbSupplierList.SupplierRegisteredEnglish,
                                             SupplierRankID = tbSupplierList.SupplierRankID,
                                             SupplierRankName = tbSupplierRankList.SupplierRankName,
                                             AgreementStateID = tbSupplierList.AgreementStateID,
                                             AgreementState = tbAgreementStateList.AgreementState,
                                             AgreementTypeID = tbSupplierList.AgreementTypeID,
                                             AgreementTypeName = tbAgreementTypeList.AgreementTypeName,
                                             Registrant = tbSupplierList.Registrant,
                                             Auditor = tbSupplierList.Auditor,
                                             Executor = tbSupplierList.Executor,

                                             ReleaseTimeStr = tbSupplierList.Registranttime.ToString(),
                                             ReleaseTimeStrr = tbSupplierList.Auditortime.ToString(),
                                             ReleaseTimeStrrr = tbSupplierList.Executortime.ToString(),

                                             SupplierInformationName = tbSupplierList.SupplierInformationName,
                                             SupplierTelephone = tbSupplierList.SupplierTelephone,
                                             SupplierContacts = tbSupplierList.SupplierContacts,
                                             SupplierPost = tbSupplierList.SupplierPost,
                                             SupplierEmail = tbSupplierList.SupplierEmail,
                                             SupplierZipCode = tbSupplierList.SupplierZipCode,
                                             SupplierPortraiture = tbSupplierList.SupplierPortraiture,
                                             SupplierAddress = tbSupplierList.SupplierAddress,
                                             SupplierURL = tbSupplierList.SupplierURL,

                                             EnterpriseBank = tbSupplierList.EnterpriseBank,
                                             EnterpriseNumber = tbSupplierList.EnterpriseNumber,
                                             LocalTaxNumber = tbSupplierList.LocalTaxNumber,
                                             EnterprisePassword = tbSupplierList.EnterprisePassword,
                                             EnterpriseNatureID = tbEnterpriseNature.EnterpriseNatureID,
                                             NationalTaxNumber = tbSupplierList.EnterpriseNumber



                                         });
            int intTotalRow = listAgreementWithhold.Count();//总行数
            List<SupplierListVo> listNotices = listAgreementWithhold.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<SupplierListVo> bsgrid = new Bsgrid<SupplierListVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 选择审核供货商
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult selectID(int id)
        {
            if (id > 0)
            {
                var listSupplierList = (from tbSupplierList in MyModels.B_SupplierList
                                        join tbUnitCharacterList in MyModels.S_UnitCharacterList on tbSupplierList.UnitCharacterID equals tbUnitCharacterList.UnitCharacterID
                                        join tbSupplierRankList in MyModels.S_SupplierRankList on tbSupplierList.SupplierRankID equals tbSupplierRankList.SupplierRankID
                                        join tbAgreementTypeList in MyModels.S_AgreementTypeList on tbSupplierList.AgreementTypeID equals tbAgreementTypeList.AgreementTypeID
                                        join tbAgreementStateList in MyModels.S_AgreementStateList on tbSupplierList.AgreementStateID equals tbAgreementStateList.AgreementStateID
                                        join tbPaymentList in MyModels.S_PaymentList on tbSupplierList.PaymentID equals tbPaymentList.PaymentID
                                        join tbCompany in MyModels.S_CompanyList on tbSupplierList.CompanyID equals tbCompany.CompanyID
                                        join tbEnterpriseNature in MyModels.S_EnterpriseNatureList on tbSupplierList.EnterpriseNatureID equals tbEnterpriseNature.EnterpriseNatureID
                                        join tbEnterpriseTypeID in MyModels.S_EnterpriseTypeList on tbSupplierList.EnterpriseTypeID equals tbEnterpriseTypeID.EnterpriseTypeID
                                        where tbSupplierList.SupplierID == id

                                        select new SupplierListVo
                                        {
                                            SupplierID = tbSupplierList.SupplierID,
                                            SupplierName = tbSupplierList.SupplierName,
                                            SupplierNumber = tbSupplierList.SupplierNumber,
                                            SupplierCHName = tbSupplierList.SupplierCHName,
                                            SupplierAbbreviation = tbSupplierList.SupplierAbbreviation,
                                            SupplierEnglishName = tbSupplierList.SupplierEnglishName,
                                            SupplierPTCode = tbSupplierList.SupplierPTCode,
                                            SupplierDependency = tbSupplierList.SupplierDependency,
                                            SupplierRegisteredAddress = tbSupplierList.SupplierRegisteredAddress,
                                            UnitCharacterID = tbSupplierList.UnitCharacterID,
                                            UnitCharacterName = tbUnitCharacterList.UnitCharacterName,
                                            //SupplierReturnTypeID = tbSupplierList.SupplierReturnTypeID,
                                            PaymentID = tbSupplierList.PaymentID,
                                            PaymentMC = tbPaymentList.PaymentMC,
                                            CompanyID = tbSupplierList.CompanyID,
                                            CompanyMC = tbCompany.CompanyMC,
                                            SupplierRegisteredEnglish = tbSupplierList.SupplierRegisteredEnglish,
                                            SupplierRankID = tbSupplierList.SupplierRankID,
                                            SupplierRankName = tbSupplierRankList.SupplierRankName,
                                            AgreementStateID = tbSupplierList.AgreementStateID,
                                            AgreementState = tbAgreementStateList.AgreementState,
                                            AgreementTypeID = tbSupplierList.AgreementTypeID,
                                            AgreementTypeName = tbAgreementTypeList.AgreementTypeName,
                                            Registrant = tbSupplierList.Registrant,
                                            Auditor = tbSupplierList.Auditor,
                                            Executor = tbSupplierList.Executor,

                                            ReleaseTimeStr = tbSupplierList.Registranttime.ToString(),
                                            ReleaseTimeStrr = tbSupplierList.Auditortime.ToString(),
                                            ReleaseTimeStrrr = tbSupplierList.Executortime.ToString(),

                                            SupplierInformationName = tbSupplierList.SupplierInformationName,
                                            SupplierTelephone = tbSupplierList.SupplierTelephone,
                                            SupplierContacts = tbSupplierList.SupplierContacts,
                                            SupplierPost = tbSupplierList.SupplierPost,
                                            SupplierEmail = tbSupplierList.SupplierEmail,
                                            SupplierZipCode = tbSupplierList.SupplierZipCode,
                                            SupplierPortraiture = tbSupplierList.SupplierPortraiture,
                                            SupplierAddress = tbSupplierList.SupplierAddress,
                                            SupplierURL = tbSupplierList.SupplierURL,
                                            EnterpriseBank = tbSupplierList.EnterpriseBank,
                                            EnterpriseNumber = tbSupplierList.EnterpriseNumber,
                                            LocalTaxNumber = tbSupplierList.LocalTaxNumber,
                                            EnterprisePassword = tbSupplierList.EnterprisePassword,
                                            EnterpriseNatureID = tbEnterpriseNature.EnterpriseNatureID,
                                            NationalTaxNumber = tbSupplierList.EnterpriseNumber


                                        }).Single();
                return Json(listSupplierList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("失败");
            }
        }



        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="SupplierListgf"></param>
        /// <returns></returns>
        public ActionResult ShenheJingXiao(B_SupplierList SupplierListgf)
        {
            string strMag = "fail";
            B_SupplierList SupplierList = new B_SupplierList();
            try
            {
                SupplierList.SupplierID = Convert.ToInt32(SupplierListgf.SupplierID);

                SupplierList.SupplierNumber = SupplierListgf.SupplierNumber;
                SupplierList.SupplierName = SupplierListgf.SupplierName;
                SupplierList.SupplierCHName = SupplierListgf.SupplierCHName;
                SupplierList.SupplierEnglishName = SupplierListgf.SupplierEnglishName;
                SupplierList.SupplierAbbreviation = SupplierListgf.SupplierAbbreviation;
                SupplierList.SupplierPTCode = SupplierListgf.SupplierPTCode;
                SupplierList.SupplierDependency = SupplierListgf.SupplierDependency;
                SupplierList.SupplierRegisteredAddress = SupplierListgf.SupplierRegisteredAddress;
                SupplierList.SupplierRegisteredEnglish = SupplierListgf.SupplierRegisteredEnglish;

                SupplierList.PaymentID = Convert.ToInt32(SupplierListgf.PaymentID);
                SupplierList.CompanyID = Convert.ToInt32(SupplierListgf.CompanyID);
                SupplierList.SupplierRankID = Convert.ToInt32(SupplierListgf.SupplierRankID);
                SupplierList.UnitCharacterID = Convert.ToInt32(SupplierListgf.UnitCharacterID);
                SupplierList.AgreementTypeID = Convert.ToInt32(SupplierListgf.AgreementTypeID);
                SupplierList.AgreementStateID = Convert.ToInt32(SupplierListgf.AgreementStateID);


                SupplierList.SupplierInformationName = SupplierListgf.SupplierInformationName;
                SupplierList.SupplierTelephone = SupplierListgf.SupplierTelephone;
                SupplierList.SupplierContacts = SupplierListgf.SupplierContacts;
                SupplierList.SupplierPost = SupplierListgf.SupplierPost;
                SupplierList.SupplierEmail = SupplierListgf.SupplierEmail;
                SupplierList.SupplierZipCode = SupplierListgf.SupplierZipCode;
                SupplierList.SupplierPortraiture = SupplierListgf.SupplierPortraiture;
                SupplierList.SupplierAddress = SupplierListgf.SupplierAddress;
                SupplierList.SupplierURL = SupplierListgf.SupplierURL;

                SupplierList.EnterpriseBank = SupplierListgf.EnterpriseBank;
                SupplierList.EnterpriseNumber = SupplierListgf.EnterpriseNumber;
                SupplierList.EnterpriseNatureID = Convert.ToInt32(SupplierListgf.EnterpriseNatureID);
                SupplierList.NationalTaxNumber = SupplierListgf.NationalTaxNumber;
                SupplierList.EnterpriseTypeID = Convert.ToInt32(SupplierListgf.EnterpriseTypeID);
                SupplierList.LocalTaxNumber = SupplierListgf.LocalTaxNumber;
                SupplierList.EnterprisePassword = SupplierListgf.EnterprisePassword;


                SupplierList.Registrant = SupplierListgf.Registrant;
                SupplierList.Registranttime = Convert.ToDateTime(SupplierListgf.Registranttime);


                SupplierList.Executor = SupplierListgf.Executor;
                SupplierList.Executortime = Convert.ToDateTime(SupplierListgf.Executortime);

                SupplierList.Auditor = SupplierListgf.Auditor;
                SupplierList.Auditortime = Convert.ToDateTime(SupplierListgf.Auditortime);

                if (SupplierList.SupplierNumber != "" && SupplierList.SupplierID > 0)
                {

                    MyModels.Entry(SupplierList).State = System.Data.Entity.EntityState.Modified;
                    MyModels.SaveChanges();//changes的三种状态：新增、修改、删除 
                    strMag = "success";

                }
            }
            catch (Exception)
            {
                return Json(strMag, JsonRequestBehavior.AllowGet);
            }
            return Json(strMag, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 供货商单位维护
        /// </summary>
        /// <param name="bsgridPage"></param>
        /// <param name="contractNumber"></param>
        /// <returns></returns>
        public ActionResult SelectHeTongChuLi(BsgridPage bsgridPage, string contractNumber)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_OrderFormPactList

                                     join tbAgreementStateList in MyModels.S_AgreementStateList on tbOrderFormPact.AgreementStatusID equals tbAgreementStateList.AgreementStateID

                                     join tbAgreementType in MyModels.S_AgreementTypeList on tbOrderFormPact.AgreementTypeID equals tbAgreementType.AgreementTypeID



                                     orderby tbOrderFormPact.OrderFormPactID
                                     select new OrderFormPactList
                                     {
                                         OrderFormPactID = tbOrderFormPact.OrderFormPactID,

                                         SupplierID = tbOrderFormPact.SupplierID,
                                         AgreementTypeID = tbOrderFormPact.AgreementTypeID,
                                         AgreementTypeName = tbAgreementType.AgreementTypeName,
                                         ReleaseTimeStrr = tbOrderFormPact.ValidBegin.ToString(),
                                         ReleaseTimeStrrr = tbOrderFormPact.ValidTip.ToString(),
                                         GoodsID = tbOrderFormPact.GoodsID,
                                         AgreementStatusID = tbOrderFormPact.AgreementStatusID,
                                         AgreementState = tbAgreementStateList.AgreementState,
                                         ReleaseTimeStr = tbOrderFormPact.ConclusionTime.ToString(),



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
        public ActionResult SelectNoticeTypeDetailByNoticeTypeId(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_GoodsList
                                     join tbAgreementType in MyModels.S_EstimateUnitList on tbOrderFormPact.EstimateUnitID equals tbAgreementType.EstimateUnitID




                                     orderby tbOrderFormPact.GoodsID
                                     select new Goods
                                     {
                                         GoodsID = tbOrderFormPact.GoodsID,

                                         SupplierID = tbOrderFormPact.SupplierID,
                                         GoodsName = tbOrderFormPact.GoodsName,
                                         EstimateUnitName = tbAgreementType.EstimateUnitName,
                                         ArtNo = tbOrderFormPact.ArtNo,

                                         SpecificationsModel = tbOrderFormPact.SpecificationsModel


                                     }
                                    );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<Goods> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Selectaa(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.B_GoodsList
                                     join tbAgreementType in MyModels.B_GoodsChopList on tbOrderFormPact.GoodsChopID equals tbAgreementType.GoodsChopID
                                     join tbGoodsClassify in MyModels.B_GoodsClassifyList on tbOrderFormPact.GoodsClassifyID equals tbGoodsClassify.GoodsClassifyID

                                     orderby tbOrderFormPact.GoodsID
                                     select new Goods
                                     {
                                         GoodsID = tbOrderFormPact.GoodsID,
                                         ReleaseTimeStr = tbOrderFormPact.RegisterTime.ToString(),
                                         GoodsName = tbOrderFormPact.GoodsName,
                                         GoodsChopID = tbOrderFormPact.GoodsChopID,
                                         GoodsChopName = tbAgreementType.GoodsChopName,
                                         ReleaseTimeStrr = tbOrderFormPact.Checktime.ToString(),
                                         GoodsClassifyID = tbOrderFormPact.GoodsClassifyID,
                                         GoodsClassifyName = tbGoodsClassify.GoodsClassifyName

                                     }
                                    );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<Goods> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<Goods> bsgrid = new Bsgrid<Goods>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }
        //生产信息
        public ActionResult Selectzhengshu(BsgridPage bsgridPage)
        {
            var listOrderFormPact = (from tbOrderFormPact in MyModels.S_ContactCertificate

                                     orderby tbOrderFormPact.ContactCertificateID
                                     select new ContactCertificateVo
                                     {
                                         ContactCertificateID = tbOrderFormPact.ContactCertificateID,
                                         ReleaseTimeStr = tbOrderFormPact.ContactCertificateTime.ToString(),
                                         ContactCertificateName = tbOrderFormPact.ContactCertificateName,
                                         ContactCertificateHM = tbOrderFormPact.ContactCertificateHM,
                                         ReleaseTimeStrr = tbOrderFormPact.ContactCertificateTimeless.ToString(),

                                     }
                                    );
            int intTotalRow = listOrderFormPact.Count();//总行数
            List<ContactCertificateVo> listNotices = listOrderFormPact.Skip(bsgridPage.GetStartIndex()).Take(bsgridPage.pageSize).ToList();//分页

            Bsgrid<ContactCertificateVo> bsgrid = new Bsgrid<ContactCertificateVo>();
            bsgrid.success = true;
            bsgrid.totalRows = intTotalRow;
            bsgrid.curPage = bsgridPage.curPage;
            bsgrid.data = listNotices;
            return Json(bsgrid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除供货商
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        public ActionResult DeleteHeTong(int goodsId)
        {
            //定义返回
            string strMsg = "fail";
            try
            {
                B_SupplierList dbGoodsDetail = (from tbGoodsDetail in MyModels.B_SupplierList
                                                where tbGoodsDetail.SupplierID == goodsId
                                                select tbGoodsDetail).Single();

                MyModels.B_SupplierList.Remove(dbGoodsDetail);

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
    }
}

using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class AgreementVo : B_AgreementList
    {
        public int SupplierID { get; set; }

        public string AdjustAccountsFashion { get; set; }

        public string AgreementState { get; set; }


        private string releaseTimeStr;
        public string ReleaseTimeStr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStr = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStr = value;
                }
            }
            get
            {
                return releaseTimeStr;
            }
        }

        //开始作答时间
        private string startTime;
        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                try
                {
                    startTime = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    startTime = value;
                }
            }
        }
        /// <summary>
        /// 提交作答时间
        /// </summary>
        private string endTiem;
        public string EndTiem
        {
            get
            {
                return endTiem;
            }
            set
            {
                try
                {
                    endTiem = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    endTiem = value;
                }
            }
        }
    }
}
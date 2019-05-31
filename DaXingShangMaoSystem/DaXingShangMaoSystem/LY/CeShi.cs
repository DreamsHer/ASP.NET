using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.LY
{
    public class CeShi : B_ConverDeTailList
    {
        /// <summary>
        /// 审核否
        /// </summary>
        public string ExamineNot { get; set; }
        /// <summary>
        /// 配货部门
        /// </summary>
        public string SpouseBRanchName { get; set; }

        /// <summary>
        /// 收货部门
        /// </summary>
        public string StockPlaceName { get; set; }


        /// <summary>
        /// 登记时间
        /// </summary>
        private string egisterTime;
        public string registerTime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    egisterTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    egisterTime = value;
                }
            }
            get
            {
                return egisterTime;
            }
        }

        /// <summary>
        /// 审核时间
        /// </summary>
        private string amineTime;
        public string examineTime
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    amineTime = dt.ToString("yyy-MM-dd");
                }
                catch (Exception)
                {
                    amineTime = value;
                }
            }
            get
            {
                return amineTime;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        public string P_Remember { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string payName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RegisterName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExamineName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string furlName { get; set; }

    }
}
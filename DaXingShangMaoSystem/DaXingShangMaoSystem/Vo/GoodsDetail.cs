using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class GoodsDetail: B_GoodsDetailList
    {
        //打包商品使用
        public string GoodsCode { get; set; }
        public string GoodsTiaoMa { get; set; }
        public string GoodsName { get; set; }
        public string SpecificationsModel { get; set; }
        public int EstimateUnitID { get; set; }
        public string EstimateUnitName { get; set; }

        //打包商品管理
        //public int GoodsDetailIDs { get; set; }

        public string PackCode { get; set; }
        public string PackTiaoMa { get; set; }
        public string PackName { get; set; }
        public string PackSpellCod { get; set; }
        public string PackDengJiRen { get; set; }
        public string PackGengXinRen { get; set; }

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

        private string releaseTimeStrr;
        public string ReleaseTimeStrr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrr = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrr = value;
                }
            }
            get
            {
                return releaseTimeStrr;
            }
        }

      

    }
}
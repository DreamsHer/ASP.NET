using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class OrderFormDetail  : B_OrderFormDetailList
    {
        //订单处理使用
        public string GoodsCode { get; set; }
        public string GoodsTiaoMa { get; set; }
        public string GoodsName { get; set; }
        public Nullable<decimal> AdvanceCess { get; set; }        
        public Nullable<decimal> RetailMonovalent { get; set; }
        public string PackContent { get; set; }
        public Nullable<decimal> NoAdvanceBid { get; set; }
        public Nullable<decimal> AdvanceBid { get; set; }
       
        public string StockNumber { get; set; }
    }
}
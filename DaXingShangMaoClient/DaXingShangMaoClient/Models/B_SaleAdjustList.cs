//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DaXingShangMaoClient.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class B_SaleAdjustList
    {
        public int SaleAdjustID { get; set; }
        public string AdjustCode { get; set; }
        public Nullable<System.DateTime> BeginTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<int> GoodsDetailID { get; set; }
        public Nullable<int> AdjustTypeID { get; set; }
        public Nullable<int> AgioTimeIntervalID { get; set; }
        public Nullable<decimal> Sale { get; set; }
        public Nullable<decimal> NewRateSale { get; set; }
        public string Registrant { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string Auditor { get; set; }
        public Nullable<System.DateTime> Checktime { get; set; }
        public string Executor { get; set; }
        public Nullable<System.DateTime> ExecuteTime { get; set; }
        public string StopRen { get; set; }
        public Nullable<System.DateTime> StopShiJian { get; set; }
        public bool DiaoZhengDanShenHeFou { get; set; }
    }
}

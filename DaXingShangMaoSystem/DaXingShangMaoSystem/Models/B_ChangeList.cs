//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DaXingShangMaoSystem.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class B_ChangeList
    {
        public int ChangeID { get; set; }
        public Nullable<int> WareHouseID { get; set; }
        public string Remember { get; set; }
        public Nullable<int> SpouseBRanchID { get; set; }
        public Nullable<int> StockPlaceID { get; set; }
        public Nullable<int> StockPlaceIDtwo { get; set; }
        public string payName { get; set; }
        public string furlName { get; set; }
        public string RegisterName { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string ExamineName { get; set; }
        public Nullable<System.DateTime> ExamineTime { get; set; }
        public bool ExamineNot { get; set; }
        public Nullable<bool> ShiFouGou { get; set; }
    }
}

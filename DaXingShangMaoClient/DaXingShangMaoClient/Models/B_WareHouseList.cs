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
    
    public partial class B_WareHouseList
    {
        public int WareHouseID { get; set; }
        public string Remember { get; set; }
        public Nullable<int> SpouseBRanchID { get; set; }
        public Nullable<int> StockPlaceID { get; set; }
        public Nullable<int> OrderFormID { get; set; }
        public Nullable<int> OrderFormPactID { get; set; }
        public Nullable<int> StaffID { get; set; }
        public string RegisterName { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string ExamineName { get; set; }
        public Nullable<System.DateTime> ExamineTime { get; set; }
        public bool ExamineNot { get; set; }
        public bool Status { get; set; }
        public bool HoveNot { get; set; }
        public string Appendix { get; set; }
        public string Evidences { get; set; }
        public bool CrushRedNot { get; set; }
        public Nullable<int> QuFenLeiXingID { get; set; }
        public bool PeiHuoNot { get; set; }
        public bool ShiFouKongNot { get; set; }
    }
}

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
    
    public partial class B_OrderFormList
    {
        public int OrderFormID { get; set; }
        public string OrderNumber { get; set; }
        public Nullable<int> OrderFormTypeID { get; set; }
        public Nullable<int> OrderFormPactID { get; set; }
        public Nullable<int> SpouseBRanchID { get; set; }
        public string Place { get; set; }
        public string DeliveryFashion { get; set; }
        public Nullable<System.DateTime> OrderGoodsData { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<decimal> ExpensesOTtaxation { get; set; }
        public Nullable<decimal> ValueTotal { get; set; }
        public Nullable<int> StaffID { get; set; }
        public string Register { get; set; }
        public Nullable<System.DateTime> RegisterTime { get; set; }
        public string CheckRen { get; set; }
        public Nullable<System.DateTime> Checktime { get; set; }
        public bool ConsigneeNot { get; set; }
        public bool ShiFouShouHuo { get; set; }
    }
}

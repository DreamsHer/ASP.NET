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
    
    public partial class B_PackGoodsList
    {
        public int PackGoodsID { get; set; }
        public string PackName { get; set; }
        public string PackCode { get; set; }
        public string PackTiaoMa { get; set; }
        public Nullable<int> EstimateUnitID { get; set; }
        public string PackSpellCod { get; set; }
        public string PackDengJiRen { get; set; }
        public Nullable<System.DateTime> PackDengJiShi { get; set; }
        public string PackGengXinRen { get; set; }
        public Nullable<System.DateTime> PackGengXinShi { get; set; }
        public string PackShuLaing { get; set; }
        public Nullable<int> GoodsDetailID { get; set; }
        public Nullable<int> GoodsPurchasemoneyStatusID { get; set; }
    }
}
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
    
    public partial class B_StockList
    {
        public int StockID { get; set; }
        public Nullable<int> GoodsID { get; set; }
        public Nullable<int> StockPlaceID { get; set; }
        public string TurnoverData { get; set; }
        public string StockNumber { get; set; }
        public string RuKuContent { get; set; }
        public Nullable<decimal> Number { get; set; }
        public Nullable<decimal> ThinCount { get; set; }
        public Nullable<int> PurchaseID { get; set; }
    }
}

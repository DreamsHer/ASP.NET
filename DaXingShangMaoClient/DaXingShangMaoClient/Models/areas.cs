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
    
    public partial class areas
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentId { get; set; }
        public string ShortName { get; set; }
        public string LevelType { get; set; }
        public string CityCode { get; set; }
        public string ZipCode { get; set; }
        public string MergerName { get; set; }
        public string Lng { get; set; }
        public string Lat { get; set; }
        public string Pinyin { get; set; }
        public string FirstChar { get; set; }
        public string shorthand { get; set; }
    }
}

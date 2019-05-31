using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.LY
{
    public class DefaulGoods : B_DefaulGoodsList
    {
        /// <summary>
        /// 缺省明细商品ID
        /// </summary>
        public Nullable<int> DefaulGoodsDetaiLLID { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        public Nullable<int> GoodsIDs { get; set; }

        /// <summary>
        /// 商品代码
        /// </summary>
        public string GoodsCodes { get; set; }

        /// <summary>
        /// 商品条码
        /// </summary>
        public string GoodsTiaoMas { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsNames { get; set; }

        /// <summary>
        /// 商品货号
        /// </summary>
        public string ArtNos { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string SpecificationsModels { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string EstimateUnitNames { get; set; }
    }
}
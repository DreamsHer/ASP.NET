using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class ReturnJsonVo
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool State { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public String Text { get; set; }
        /// <summary>
        /// 附加数据
        /// </summary>
        public object Object { get; set; }
    }
}
﻿using DaXingShangMaoSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DaXingShangMaoSystem.Vo
{
    public class MembershipCardTypeVo : S_MembershipCardTypeList
    {
        //转换时间格式
        private string releaseTimeStr;
        public string ReleaseTimeStr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStr = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStr = value;
                }
            }
            get
            {
                return releaseTimeStr;
            }
        }

        private string releaseTimeStrt;
        public string ReleaseTimeStrr
        {
            set
            {
                try
                {
                    DateTime dt = Convert.ToDateTime(value);
                    releaseTimeStrt = dt.ToString("yyyy-MM-dd");
                }
                catch (Exception)
                {
                    releaseTimeStrt = value;
                }
            }
            get
            {
                return releaseTimeStrt;
            }
        }
    }
}
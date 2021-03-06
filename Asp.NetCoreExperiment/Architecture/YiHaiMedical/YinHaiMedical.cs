﻿using HisMedical;
using System;

namespace YiHaiMedical
{
    /// <summary>
    /// 银海接口
    /// </summary>
    public class YinHaiMedical : IHis
    {
        /// <summary>
        /// his登记号
        /// </summary>
        public string RegisterID { set; private get; }
        /// <summary>
        /// 缴费
        /// </summary>
        /// <returns></returns>
        public dynamic Fee()
        {
            Console.WriteLine("=====完成对银海医保的住院缴费");
            return true;
        }
        /// <summary>
        /// 住院登记
        /// </summary>
        /// <returns></returns>
        public dynamic Register()
        {
            Console.WriteLine("=====完成对银海医保的住院登记");
            return true;
        }
    }
}

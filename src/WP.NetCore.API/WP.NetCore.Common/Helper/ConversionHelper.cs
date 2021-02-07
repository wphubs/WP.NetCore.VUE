using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WP.NetCore.Common
{
    /// <summary>
    ///  转换
    /// </summary>
    public static class ConversionHelper
    {
        #region 数据格式转换
        /// <summary>
        ///  转换成Int
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static int ToInt(this object inputValue)
        {
            return inputValue.IsInt() ? int.Parse(inputValue.ToStringValue()) : 0;
        }

        public static bool ToState(this int inputValue)
        {
            return inputValue == 1;
        }

        /// <summary>
        ///  转换成Int32
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static int ToInt32(this object inputValue)
        {
            return inputValue.IsInt() ? Convert.ToInt32(inputValue) : 0;
        }

        /// <summary>
        ///  转换成Int64
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static long ToInt64(this object inputValue)
        {
            return inputValue.IsInt() ? Convert.ToInt64(inputValue) : 0;
        }

        /// <summary>
        ///  转换成Double
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static Double ToDouble(this object inputValue)
        {
            return !inputValue.IsNullOrEmpty() ? Convert.ToDouble(inputValue) : 0;
        }


        public static Decimal ToDecimal(this object inputValue)
        {
            return !inputValue.IsNullOrEmpty() ? Convert.ToDecimal(inputValue) : 0;
        }


        /// <summary>
        ///  转换成Int64
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool ToBool(this object inputValue)
        {
            return bool.Parse(inputValue.ToStringValue());
        }

        /// <summary>
        ///  转换obj 成string
        /// </summary>
        /// <param name="inputValue">object</param>
        /// <returns></returns>
        public static string ToStringValue(this object inputValue)
        {
            return inputValue == null ? "" : inputValue.ToString();
        }

        /// <summary>
        ///  转换成数据库字符串
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static string ToDbString(this object inputValue)
        {
            return !inputValue.IsNullOrEmpty() ? "'" + inputValue.ToStringValue().Trim().Replace("'", "''") + "'" : "''";
        }

        /// <summary>
        ///  转换成时间类型yyyy-MM-dd
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object inputValue)
        {
            return inputValue.IsNullOrEmpty() ? DateTime.MinValue : Convert.ToDateTime(inputValue);
        }

        public static List<int> GetUserLevelArray(this string inputValue)
        {
            return inputValue == null ? new List<int>() : Array.ConvertAll(inputValue.Split(','), int.Parse).ToList();
        }

        /// <summary>
        ///  转换成时间类型
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <param name="format">时间格式</param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object inputValue, string format)
        {
            return DateTime.ParseExact(inputValue.ToStringValue(), format, null);
        }
        #endregion
    }
    /// <summary>
    ///  验证
    /// </summary>
    public static class VerificationHelper
    {


        #region 正则表达式
        //邮政编码
        private static readonly Regex RegPostCode = new Regex("^\\d{6}$");
        //中国身份证验证
        private static readonly Regex RegCardId = new Regex("^\\d{17}[\\d|X]|\\d{15}|\\d{18}$");
        //数字
        private static readonly Regex RegNumber = new Regex("^\\d+$");
        //固定电话
        private static readonly Regex RegTel = new Regex("^\\d{3,4}-\\d{7,8}|\\d{7,8}$");
        //手机号
        private static readonly Regex RegPhone = new Regex("^[1][3-8]\\d{9}$");
        //电话号码（包括固定电话和手机号）
        private static readonly Regex RegTelePhone = new Regex("^(\\d{3,4}-\\d{7,8}|\\d{7,8})|([1][3-8]\\d{9})$");
        //邮箱
        private static readonly Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");
        //中文
        private static readonly Regex RegChzn = new Regex("[\u4e00-\u9fa5]");
        //IP地址
        private static readonly Regex RegIp = new Regex("((25[0-5]|2[0-4]\\d|1?\\d?\\d)\\.){3}(25[0-5]|2[0-4]\\d|1?\\d?\\d)");
        #endregion

        #region 验证方法
        /// <summary>
        ///  判断是否是数字
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsInt(this object inputValue)
        {
            int num;
            return int.TryParse(inputValue.ToStringValue(), out num);
        }
        /// <summary>
        ///  判断是否是小数
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsDouble(this object inputValue)
        {
            Double dValue;
            return Double.TryParse(inputValue.ToStringValue(), out dValue);
        }
        /// <summary>
        ///  判断是否是小数
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsFloat(this object inputValue)
        {
            float fValue;
            return float.TryParse(inputValue.ToStringValue(), out fValue);
        }
        /// <summary>
        ///  判断字符串是否为空
        ///  空：true，不为空：false
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object inputValue)
        {
            return string.IsNullOrEmpty(inputValue.ToStringValue());
        }

        /// <summary>
        ///  判断字符串是否为Email
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsEmail(this object inputValue)
        {
            var match = RegEmail.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为固话
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsTel(this object inputValue)
        {
            var match = RegTel.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为手机号
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsPhone(this object inputValue)
        {
            var match = RegPhone.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为电话号码
        ///  （包含固定电话和手机号）
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsTelePhone(this object inputValue)
        {
            var match = RegTelePhone.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为IP地址
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsIp(this object inputValue)
        {
            var match = RegIp.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为邮编
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsPostCode(this object inputValue)
        {
            var match = RegPostCode.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为身份证
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsCardId(this object inputValue)
        {
            var match = RegCardId.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为中文
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsChzn(this object inputValue)
        {
            var match = RegChzn.Match(inputValue.ToStringValue());
            return match.Success;
        }
        /// <summary>
        ///  判断字符串是否为数字
        /// </summary>
        /// <param name="inputValue">输入值</param>
        /// <returns></returns>
        public static bool IsNumber(this object inputValue)
        {
            var match = RegNumber.Match(inputValue.ToStringValue());
            return match.Success;
        }
        #endregion
    }
}

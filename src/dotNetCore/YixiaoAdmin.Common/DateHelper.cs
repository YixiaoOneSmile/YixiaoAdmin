using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace YixiaoAdmin.Common
{
    /// <summary>
    /// 与时间处理相关的帮助类
    /// </summary>
    public class DateHelper
    {
        /// <summary>
        /// 取出指定年的指定周的周一
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        public static DateTime GetMondayByWeek(int year,int week)
        {
            //取出当年的第一个周一的日期
            DateTime dateTime = GetFirstMondayByYear(year);
            dateTime = dateTime.AddDays(7 * (week-1));
            return dateTime;

        }

        /// <summary>
        /// 取出指定年的指定周的周日
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        public static DateTime GetSundayByWeek(int year, int week)
        {
            //取出当年的第一个周一的日期
            DateTime dateTime = GetFirstMondayByYear(year);
            dateTime = dateTime.AddDays(7 * week-1);
            return dateTime;
        }

        /// <summary>
        /// 取出传入日期所在的周
        /// </summary>
        /// <param name="year"></param>
        /// <param name="week"></param>
        public static int GetWeekByDate(DateTime dateTime)
        {
            int dayOfWeek = Convert.ToInt32(dateTime.DayOfWeek);//今天星期几
            int fromLastSunday = (-1) * (dayOfWeek + 1);//今日与上周末的天数差
            int days = dateTime.AddDays(fromLastSunday).DayOfYear;//上周末是本年第几天
            int weeks = days / 7;
            if (days % 7 != 0)
            {
                weeks++;
            }
            //此时，weeks为上周是本年的第几周
            return weeks;
        }

        /// <summary>
        /// 获取每一年的第一个工作周的周一
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static DateTime GetFirstMondayByYear(int year)
        {
            //取出当年的第一天
            DateTime dateTime = Convert.ToDateTime($"{year}-01-01");
            while (dateTime.DayOfWeek != DayOfWeek.Monday)
            {
                dateTime = dateTime.AddDays(1);
            }
            return dateTime;
        }
    }

    /// <summary>
    /// 工作周模型
    /// </summary>
    public class WorkWeek
    {
        /// <summary>
        /// 工作周所属年
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 工作周是第几周
        /// </summary>
        public int Week { get; set; }

        /// <summary>
        /// 工作周的周一日期
        /// </summary>
        public DateTime Monday { get; set; }

        /// <summary>
        /// 工作周的周日日期
        /// </summary>
        public DateTime Sunday { get; set; }

        /// <summary>
        /// 根据调用函数当天的日期初始化工作周
        /// </summary>
        public void Init()
        {
            //取今天的日期
            DateTime date = DateTime.Now;

            this.Year = date.Year;
            this.Week = DateHelper.GetWeekByDate(date);
            this.Monday = DateHelper.GetMondayByWeek(this.Year, this.Week);
            this.Sunday = this.Monday.AddDays(7);
        }

        /// <summary>
        /// 根据传入日期初始化工作周
        /// </summary>
        /// <param name="date"></param>
        public void Init(DateTime date)
        {
            this.Year = date.Year;
            this.Week = DateHelper.GetWeekByDate(date);
            //获取传入年与周的周一
            this.Monday = DateHelper.GetMondayByWeek(this.Year, this.Week);
            //将周一的时间加7天
            this.Sunday = this.Monday.AddDays(7);
        }

        /// <summary>
        /// 根据传入年份与工作周初始化工作周
        /// </summary>
        /// <param name="date"></param>
        public void Init(int year, int week)
        {
            this.Year = year;
            this.Week = week;
            //获取传入年与周的周一
            this.Monday = DateHelper.GetMondayByWeek(year, week);
            //将周一的时间加7天
            this.Sunday = this.Monday.AddDays(7);
        }
    }
}

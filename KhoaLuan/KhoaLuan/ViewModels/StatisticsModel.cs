using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KhoaLuan.ViewModels
{
    public class StatisticsModel
    {
        public long TotalPost { get; set; }
        public long AmountPostByMonth { get; set; }
        public long AmountPostByYear { get; set; }
        public long RevenueByDay { get; set; }
        public long RevenueByMonth { get; set; }
        public long RevenueByYear { get; set; }
        public long TotalFreeRoom { get; set; }
        public List<Revenue> RevenueStatisic { get; set; }
    }

    public class Revenue
    {
        public string Day { get; set; }
        public long Value { get; set; }
    }
}
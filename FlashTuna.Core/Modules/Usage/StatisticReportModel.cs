using FlashTuna.Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Modules.Usage
{
    public class StatisticReportModel
    {
        public int DiscoveredMethods { get; set; }
        public int ProblemsCount { get; set; }
        public int ErrorsCount { get; set; }
        public List<TrackableMethodViewModel> Methods { get; set; }
        public string UpdateInterval { get; set; }
        public string TrackingPeriod { get; set; }
    }
}

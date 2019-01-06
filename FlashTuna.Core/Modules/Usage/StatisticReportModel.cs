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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlashTunaPerfomanceAnalyzer.Classes.Models
{
    public class MetricsResultsViewModel
    {
        [Required]
        public DateTime PeriodFrom { get; set; }
        public DateTime? PeriodTo { get; set; }
    }
}

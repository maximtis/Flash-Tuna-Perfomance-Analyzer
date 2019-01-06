using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlashTuna.Core.Modules.Usage
{
    public class Setting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        //1-Days,2-minutes,3-seconds
        public long PeriodType;
        public long Period;
        public string ConnectionString { get; set; }
        public virtual ICollection<TrackedMethod> TrackedMethods { get; set; }
    }
}

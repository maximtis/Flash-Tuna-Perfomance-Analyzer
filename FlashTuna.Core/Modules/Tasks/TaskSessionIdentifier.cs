using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Modules.Tasks
{
    public class TaskSessionIdentifier
    {
        public long Id;
        public Guid Guid { get; set; }
        public DateTime SessionStart { get; set; }
        public DateTime SessionFinish { get; set; }
        public string Tag { get; set; }
        public string ToSessionString()
        {
            return $"{Guid}_{SessionStart.ToString()}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashTuna.Core.Modules.Usage
{
    public class TrackableMethodViewModel
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public bool Selected { get; set; }
    }
}

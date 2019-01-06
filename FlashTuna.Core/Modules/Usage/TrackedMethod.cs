using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlashTuna.Core.Modules.Usage
{
    public class TrackedMethod
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Name { get; set; }

        public long ConfigurationId { get; set; }
        public virtual Setting Configuration { get; set; }
    }
}

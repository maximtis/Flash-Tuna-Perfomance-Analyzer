using System;
using System.Collections.Generic;
using System.Text;

namespace FlashTuna.Core.Modules.Tasks
{
    public class TaskSessionMetadata
    {
        public int ParallelTaskCount { get; set; }
        public TaskSessionIdentifier SessionIdentifier { get; set; }

        public void Finish()
        {
            if (SessionIdentifier == null)
                SessionIdentifier = new TaskSessionIdentifier();
            SessionIdentifier.SessionFinish = DateTime.Now;
        }
        private TaskSessionMetadata()
        {
            ParallelTaskCount = 0;
            SessionIdentifier = new TaskSessionIdentifier()
            {
                Id = (long)new Random().Next(1000000, 9999999),
                Guid = Guid.NewGuid(),
                SessionStart = DateTime.Now,
                Tag = "Demo Session Identifier"
            };
        }

        private static TaskSessionMetadata _instatnce;
        public static TaskSessionMetadata CurrentSession { get
            {
                return _instatnce ?? new TaskSessionMetadata();
            }
        }
    }
}

using System;

namespace CoMute.Core
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.Now;
    }
}

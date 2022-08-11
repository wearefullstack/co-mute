using System;

namespace CoMute.Core
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; } 
    }
}
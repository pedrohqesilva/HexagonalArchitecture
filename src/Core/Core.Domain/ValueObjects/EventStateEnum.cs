using System;

namespace Core.Domain.ValueObjects
{
    [Flags]
    public enum EventStateEnum
    {
        NotPublished = 1,
        Published = 2,
        PublishedFailed = 4,
        Processed = 8
    }
}
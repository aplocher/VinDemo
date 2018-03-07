using System;

namespace VinDemo.Domain.Interfaces
{
    public interface ITrackModifiedDate
    {
        DateTime? ModifiedDate { get; set; }
    }
}
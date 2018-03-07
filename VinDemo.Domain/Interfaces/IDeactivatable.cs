using System;

namespace VinDemo.Domain.Interfaces
{
    public interface IDeactivatable
    {
        DateTime? DeactivatedDate { get; set; }
    }
}
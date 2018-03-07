using System;

namespace VinDemo.Domain.Interfaces
{
    public interface IMember: IDeactivatable, ITrackCreatedDate, ITrackModifiedDate
    {
        DateTime? DateOfBirth { get; set; }
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int? MemberId { get; set; }
        string PhoneNumber { get; set; }
        string Username { get; set; }
    }
}
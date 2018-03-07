using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VinDemo.Domain.Interfaces;
using VinDemo.Domain.ValidationAttributes;

namespace VinDemo.Domain.Entities
{
    public class Member : ITrackCreatedDate, ITrackModifiedDate, IDeactivatable, IMember
    {
        [Column(@"MemberId", Order = 1, TypeName = "int")]
        [Key]
        [Display(Name = "Member ID")]
        public int? MemberId { get; set; }

        [Required]
        [MaxLength(12)]
        [MinLength(4, ErrorMessage = "{0} must include at least {1} characters")]
        [StringLength(12)]
        [Display(Name = "Username")]
        [UsernameMustBeUnique("MemberId")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2, ErrorMessage = "{0} must include at least {1} characters")]
        [StringLength(100)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2, ErrorMessage = "{0} must include at least {1} characters")]
        [StringLength(100)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(75)]
        [StringLength(75)]
        [EmailAddress]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [MaxLength(10)]
        [StringLength(10)]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Date of birth")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Deactivated date")] public DateTime? DeactivatedDate { get; set; }

        // TODO: Consider a different approach?
        // it would be nice to keep this required so it was reflected in the DB schema when generated, but our DAL will force this 
        // value to be set, so from an app-perspective, it's not really necessary.  When checking if valid, it would always return false
        // because the DAL doesn't inject this value until after validation.
        //[Required]
        [Display(Name = "Created date")] public DateTime? CreatedDate { get; set; }

        [Display(Name = "Modified date")] public DateTime? ModifiedDate { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.ViewModels
{
    public class UserEditProfile
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "enter a string between 3 and 50")]
        public string F_name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "invalid email message")]
        [DataType(DataType.EmailAddress)]
        [Remote("uniqueEmail", "Validation", AdditionalFields = "Id", ErrorMessage = "This Email Already exist")]
        public string Email { get; set; }

        public int? Phone { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "enter a string between 3 and 50")]
        public string L_name { get; set; }
        public string? Image {  get; set; }
    }
}

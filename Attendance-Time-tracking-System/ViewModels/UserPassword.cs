using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.ViewModels
{
    public class UserPassword
    {
        public int Id { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Enter a Valid password")]
        [DataType(DataType.Password)]
        [Remote("IsPassword", "Validation", AdditionalFields = "Id", ErrorMessage = "This is Not correct Password")]
        public string oldPassword { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Enter a Valid password")]
        [DataType(DataType.Password)]
        [Remote("IsEqualToPassword", "Validation", AdditionalFields = "oldPassword", ErrorMessage = "This is password is not valid")]
        public string  NewPassword { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Enter a Valid password")]
        [DataType(DataType.Password)]
        [Remote("ConfirmPasswords", "Validation", AdditionalFields = "NewPassword", ErrorMessage = "This is Not matching the new password")]
        public string ConfirmPassword { get; set;}
    }
}

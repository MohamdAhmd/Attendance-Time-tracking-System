using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attendance_Time_tracking_System.Models
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email Format")]
        [MaxLength(250)]
        [EmailAddress(ErrorMessage ="invalid email message")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //at least  8 char upper/lower/numbers/special characers
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$" , ErrorMessage ="Enter a Valid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(50,MinimumLength =3,ErrorMessage ="enter a string between 3 and 50")]
        [Required]
        public string F_name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "enter a string between 3 and 50")]
        public string L_name {  get; set; }
        [Range(00000000000,99999999999)]
        public int? phone { get; set; }


        //this is for validation

        [NotMapped]
        [DataType(DataType.Password)]
        [Remote("confirmpassword", "Validation", AdditionalFields = "Password", ErrorMessage = "the confirm password doesn't match")]
        public string? ConfirmPassword { get; set; }

        public List<Roles> roles { get; set; } = new List<Roles>();
        public List<Attend> attends { get; set; } = new List<Attend>();
    }

    
}

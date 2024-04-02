using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email Format")]
        [MaxLength(250)]
        [EmailAddress(ErrorMessage ="invalid email message")]

        public string Email { get; set; }

        //at least  8 char upper/lower/numbers/special characers
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d@$!%*?&]{8,}$" , ErrorMessage ="Enter a Valid password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [Length(maximumLength: 50, minimumLength: 3, ErrorMessage = "Enter A name between 3 and 50 letter")]
        public string F_name { get; set; }

        [Required]
        [Length(maximumLength: 50, minimumLength: 3, ErrorMessage = "Enter A name between 3 and 50 letter")]
        public string L_name {  get; set; }

        public int? phone { get; set; }

        public bool Status { get; set; }

        public virtual List<Roles> roles { get; set; } = new List<Roles>();
        public virtual List<Attend> attends { get; set; } = new List<Attend>();
    }
}

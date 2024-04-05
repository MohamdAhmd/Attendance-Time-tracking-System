using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.ViewModels
{
    public class LoginVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

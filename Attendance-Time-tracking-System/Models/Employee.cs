using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.Models
{
    public class Employee : User
    {
        [Range(5000, 50000,ErrorMessage ="Salary between 5000 and 50000")]
        
        public int Salary { get; set; }
    }
}

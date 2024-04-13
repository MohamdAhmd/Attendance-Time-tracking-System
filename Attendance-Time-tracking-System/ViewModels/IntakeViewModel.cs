using Attendance_Time_tracking_System.Models; // Add this using directive if Program class is defined in this namespace
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Attendance_Time_tracking_System.ViewModels
{
    public class IntakeViewModel
    {
        public int IntakeId { get; set; }
        public int ProgramId { get; set; }
        public int stdNo { get; set; }
        [Required(ErrorMessage = "Intake name is required.")]
        [Length(maximumLength: 50, minimumLength: 5, ErrorMessage = "Enter a name between 5 and 50 letters.")]       
        public string IntakeName { get; set; }
        public string ProgramName { get; set; }

    }
}

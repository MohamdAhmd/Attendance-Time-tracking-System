namespace Attendance_Time_tracking_System.ViewModels
{
    public class AttendanceList
    {
        public string f_name { get; set; }
        public string l_name { get; set; }
        public int id { get; set; }
        public bool? attendpresent { get; set; } = false;
        public bool? attendleave { get; set; } = false;
    }
}

namespace Attendance_Time_tracking_System.IRepos
{
    public interface IStudentRepo
    {
        public Task<int> AddStudent(Student student, IFormFile personalimage);
        public Student GetStudentById(int id);
        public bool changeattendance(int userId, bool value);
        public List<AttendanceList> GetAllUsersWithRole(int? value, string daystatus);
        public bool PutAllStudentsInAttendanceTable(string daystatus);
        public bool ChangeAllStudentToLate(int[] ids);


    }
}

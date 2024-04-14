namespace Attendance_Time_tracking_System.IRepos
{
    public interface IUserRepo
    {
        public bool isunique(string Email , int Id);

        public User GetUser(string Email , string Password);

        public List<User> GetAllUsers();
        public List<AttendanceList> GetAllUsersWithRole(int value);
        public bool changeattendance(int userId, bool value ,int usertype);
        public bool ChangeLeaveAttendance(int userId, bool value);
        public bool PutAllUsersInAttendanceTable();
        public bool ChangeAllToLate(int[] ids);

    }
}

using Microsoft.AspNetCore.Mvc;

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
        public UserEditProfile GetUserById(int id);
        public  Task<bool> EditUserInfo(UserEditProfile model, IFormFile file);
        public UserPassword UserPassword(int id);
        public bool currentpass(string password, int id);
        public bool updatePass(UserPassword user);

        public User GetUserById(int id);


    }
}

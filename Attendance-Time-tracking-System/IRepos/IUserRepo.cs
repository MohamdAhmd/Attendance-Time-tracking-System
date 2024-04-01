namespace Attendance_Time_tracking_System.IRepos
{
    public interface IUserRepo
    {
        public bool isunique(string Email , int Id);

        public User GetUser(string Email , string Password);

        public List<User> GetAllUsers();
    }
}

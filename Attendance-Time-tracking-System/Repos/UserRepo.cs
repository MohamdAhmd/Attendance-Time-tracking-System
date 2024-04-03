using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Attendance_Time_tracking_System.Repos
{
    public class UserRepo  : IUserRepo
    {
        readonly dbContext db;
        public UserRepo(dbContext db)
        {
            this.db = db;
        }

        public List<User> GetAllUsers()
        {
            var model = db.Users.ToList();
            return model;
        }

        public User GetUser(string Email, string Password)
        {
            var user = db.Users.Include(x=>x.roles).FirstOrDefault(x=>x.Email == Email && x.Password == Password);
            return user;
        }

        public bool isunique(string Email , int Id)
        {
            if (Id == 0)
            {
                return !db.Users.Any(x => x.Email == Email);
            }
            else
            {
                return !db.Users.Any(x => x.Email == Email && x.Id != Id);
            }
        }
    }
}

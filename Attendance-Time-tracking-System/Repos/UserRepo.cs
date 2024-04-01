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

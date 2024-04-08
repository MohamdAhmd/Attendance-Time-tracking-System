using Microsoft.AspNetCore.Http;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Attendance_Time_tracking_System.Repos
{
    public class PermissionRepo : IPermissionRepo
    {
        readonly dbContext db;
        public PermissionRepo(dbContext _db)
        {
            db = _db;
        }


        public List<Permission> GetPermissions()
        {
            int stdId = 34;
            return db.Permissions.Where(a => a.StudentId == stdId).ToList();
        }

        public void create(Permission _P1)
        {
            var newPermission = new Permission {
                day = DateTime.Now,
                StudentId = 34,
                PermissionType = _P1.PermissionType,
                PermissionBody = _P1.PermissionBody,
                PermissionStatus = _P1.PermissionStatus,
            };
            db.Permissions.Add(newPermission);
            db.SaveChanges();
        }

        public Permission GetPermissionByDate(string _date)
        {
            DateTime date = DateTime.Parse(_date); // Get only the date part

            DateTime startDateTime = date;
            DateTime endDateTime = startDateTime.AddMinutes(1); // Adding 100 milliseconds to get the end datetime

            var permissions = db.Permissions
                .FirstOrDefault(p => p.day >= startDateTime && p.day < endDateTime);

            return permissions;
        }

        public void edit(Permission _P1) { }
        public void delete(string _date) {
            DateTime date = DateTime.Parse(_date); // Get only the date part

            DateTime startDateTime = date;
            DateTime endDateTime = startDateTime.AddMinutes(1); // Adding 100 milliseconds to get the end datetime

            var permissions = db.Permissions
                .Where(p => p.day >= startDateTime && p.day < endDateTime)
                .ToList();

            db.RemoveRange(permissions);
            db.SaveChanges();
        }

    }
}

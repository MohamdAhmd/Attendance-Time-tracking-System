using Microsoft.EntityFrameworkCore;

namespace Attendance_Time_tracking_System.Repos
{
    public class PermissionRepo : IPermissionRepo
    {
        readonly dbContext db;
        public PermissionRepo(dbContext _db)
        {
            db = _db;
        }


        public List<Permission> GetPermissions(int id)
        {
            //int id = 34;
            return db.Permissions.Where(a => a.StudentId == id).ToList();
        }

        public void create(Permission _P1,int id)
        {
            var newPermission = new Permission {
                day = DateTime.Now,
                StudentId = id,
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


        public void edit(Permission _P1) {
            var permissionData = GetPermissionByDate(_P1.day.ToString());
            permissionData.PermissionBody = _P1.PermissionBody;
            permissionData.PermissionType = _P1.PermissionType;
            db.SaveChanges();
        }

        public List<StdPermissionVM> StdPermissions(int superId)
        {
            var supervisorId = superId;
            var permissions = db.Permissions
                        .Include(p => p.StudentNavigation)
                        .ThenInclude(s => s.TrackNavigation)
                        .ThenInclude(t => t.InstructorNavigation)
                        .Where(p => p.StudentNavigation.TrackNavigation.InstructorNavigation.Id == supervisorId &&
                                    p.PermissionStatus == "Pending") // Add condition for PermissionStatus
                        .Select(p => new StdPermissionVM
                        {
                            PermissionInfo = p,
                            Fname = p.StudentNavigation.F_name,
                            Lname = p.StudentNavigation.L_name
                        })
                        .ToList();

            return permissions;
        }



        public void ChangeStatus(string date,string status)
        {
            var permission = GetPermissionByDate(date);
            permission.PermissionStatus = status;
            if(status == "Rejected")
            {
                var student = db.Students.FirstOrDefault(a => a.Id == permission.StudentId);
                student.Grade -= 15;
                db.SaveChanges();
            }
                db.SaveChanges();
            
        }
    }
}

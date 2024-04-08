namespace Attendance_Time_tracking_System.Repos
{
    public class PermissionRepo : IPermissionRepo
    {
        readonly dbContext db;
        public PermissionRepo(dbContext _db)
        {
            db = _db;
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
        public void edit(Permission _P1) { }
        public void delete(Permission _P1) { }

    }
}

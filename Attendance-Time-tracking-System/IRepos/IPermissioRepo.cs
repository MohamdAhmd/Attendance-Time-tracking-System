namespace Attendance_Time_tracking_System.IRepos
{
    public interface IPermissionRepo
    {
        public List<Permission> GetPermissions();
        public void create(Permission _P1);
        public void edit(Permission _P1);
        public void delete(string date);

    }
}

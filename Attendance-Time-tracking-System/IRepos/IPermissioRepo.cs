namespace Attendance_Time_tracking_System.IRepos
{
    public interface IPermissionRepo
    {
        public List<Permission> GetPermissions(int id);
        public Permission GetPermissionByDate(string _date);
        public void create(Permission _P1,int id);
        public void edit(Permission _P1);
        public void delete(string date);
        public void ChangeStatus(string date, string status);

        public List<StdPermissionVM> StdPermissions(int superId);
    }
}

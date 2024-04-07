namespace Attendance_Time_tracking_System.IRepos
{
    public interface IDaysRepo
    {
        public List<Days> GetAllDays();
        public Days GetDayById(int id);
        public void AddDay(Days day);
        public void UpdateDay(Days day);
        public void DeleteDay(int id);
    }
}

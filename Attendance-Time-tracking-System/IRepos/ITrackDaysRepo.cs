namespace Attendance_Time_tracking_System.IRepos
{
    public interface ITrackDaysRepo
    {
        public IEnumerable<TrackDays> GetAllDays();
        public TrackDays GetDayById(int id);
        public void AddDay(TrackDays trackDays);
        public void UpdateDay(TrackDays trackDays);
        public void DeleteDay(int id);
    }
}

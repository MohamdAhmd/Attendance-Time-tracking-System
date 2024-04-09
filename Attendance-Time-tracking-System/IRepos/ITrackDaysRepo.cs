namespace Attendance_Time_tracking_System.IRepos
{
    public interface ITrackDaysRepo
    {
        public IEnumerable<TrackDays> GetAllTrackDays();
        public TrackDays GetTrackDayById(int dayId, int trackId);
        public void AddTrackDay(TrackDays trackDays);
        public void UpdateTrackDay(TrackDays trackDays);
        public void DeleteTrackDay(int id);
    }
}

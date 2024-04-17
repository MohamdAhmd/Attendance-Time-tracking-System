namespace Attendance_Time_tracking_System.IRepos
{
    public interface ITrackDaysRepo
    {
        public IEnumerable<TrackDays> GetAllTrackDays();
        //get supervisor track days
        public IEnumerable<TrackDays> GetTrackDaysBySupervisorId(int id);

        public TrackDays GetTrackDayById(int dayId, int trackId);
        public void AddTrackDay(TrackDays trackDays);
        public void UpdateTrackDay(TrackDays trackDays,int? dayId);
        public void DeleteTrackDay(int id);
        
    }
}

﻿namespace Attendance_Time_tracking_System.IRepos
{
    public interface ITrackRepo
    {
        public List<Track> GetAllTracks();
        public List<Track> GetTrackBySupervisorId(int id);

        public List<Instructor> GetAllInstructors();
        public Track GetTrackById(int id);
        public void AddTrack(Track track);
        public void UpdateTrack(Track track);
        public void DeleteTrack(int id);
        public List<Track> GetAllTracksForSuperVisor(int Superid);


    }
}

namespace Attendance_Time_tracking_System.IRepos
{
    public interface IBlobRepo
    {
        public Task<string> AddingImage(IFormFile image);
        public Task<bool> RemoveImage(string imagename);
        public Task<string> UpdateImage(string oldimage, IFormFile image);
    }
}

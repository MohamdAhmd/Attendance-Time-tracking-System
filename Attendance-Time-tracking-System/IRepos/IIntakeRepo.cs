namespace Attendance_Time_tracking_System.IRepos
{
    public interface IIntakeRepo
    {
        public List<Intake> GetAll();
        public void add(Intake _intake);
        public void UpdateIntake(Intake Data);
        public void DeleteIntake(int id);

    }
}

using Attendance_Time_tracking_System.ViewModels;

namespace Attendance_Time_tracking_System.IRepos
{
    public interface IIntakeRepo
    {
        public List<IntakeViewModel> GetAll();

        public Intake GetById(int id);
        public void add(IntakeViewModel _intake);
        public void UpdateIntake(Intake Data);
        public void DeleteIntake(int id);
        public void addToIntakeProgram(IntakeViewModel data);

    }
}

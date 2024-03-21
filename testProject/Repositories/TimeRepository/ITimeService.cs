using testProject.Models.HomeModels;

namespace testProject.Repositories.TimeRepository
{
    public interface ITimeService
    {
        Task<TimeData> GetTime(string city);
    }
}

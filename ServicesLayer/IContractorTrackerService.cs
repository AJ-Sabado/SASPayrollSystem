using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;

namespace ServicesLayer
{
    public interface IContractorTrackerService
    {
        IList<ContractorAttendanceLogModel> CurrentWeekAttendanceLogs { get; }
        ContractorModel? CurrentContractor { get; }
        ContractorAttendanceLogModel? CurrentAttendanceLog { get; }
        decimal TotalWeeklyHoursRendered { get; }

        Task<ContractorAttendanceLogModel?> EndSession(DateTime? timeStamp = null);
        Task<ContractorModel?> InitializeService(Guid UserId);
        Task<ContractorAttendanceLogModel?> StartSession(DateTime? timeStamp = null);
    }
}
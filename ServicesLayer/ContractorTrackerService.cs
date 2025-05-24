using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;

namespace ServicesLayer
{
    public class ContractorTrackerService : IContractorTrackerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContractorModel? CurrentContractor { get; private set; }
        public ContractorAttendanceLogModel? CurrentAttendanceLog { get; private set; }
        public IList<ContractorAttendanceLogModel> CurrentWeekAttendanceLogs { get; private set; } = [];

        public ContractorTrackerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Methods
        public async Task<ContractorModel?> InitializeService(Guid UserId)
        {
            if (UserId == Guid.Empty)
                return null;
            
            //Null checks
            if (CurrentContractor != null)
            {
                if (CurrentContractor.UserId == UserId)
                    return CurrentContractor;
                if (CurrentAttendanceLog != null)
                {
                    CurrentAttendanceLog.TimeOut = DateTime.Now;
                    await _unitOfWork.Save();
                    CurrentAttendanceLog = null;
                }
                CurrentWeekAttendanceLogs.Clear();
            }

            CurrentContractor = await _unitOfWork.ContractorRepository.GetAsync(c => c.UserId == UserId, includeProperties: "ContractorAttendanceLogs,ContractorPayslips");

            UpdateCurrentWeekAttendanceLog();
            return CurrentContractor;
        }

        public async Task<ContractorAttendanceLogModel?> StartSession(DateTime? timeStamp = null)
        {
            if (CurrentContractor == null)
                return null;
            if (CurrentAttendanceLog != null)
                return CurrentAttendanceLog;
            CurrentAttendanceLog = new ContractorAttendanceLogModel()
            {
                ContractorId = CurrentContractor.ContractorId,
                Contractor = CurrentContractor,
                Date = DateOnly.FromDateTime(timeStamp ?? DateTime.Now),
                TimeIn = timeStamp ?? DateTime.Now,
            };
            CurrentContractor.ContractorAttendanceLogs.Add(CurrentAttendanceLog);
            await _unitOfWork.Save();
            return CurrentAttendanceLog;
        }

        public async Task<ContractorAttendanceLogModel?> EndSession(DateTime? timeStamp = null)
        {
            //Checks if the current contractor is null, if the current attendance log is null, and if the time out is already set.
            if (CurrentContractor == null)
                return null;
            if (CurrentAttendanceLog == null)
                return null;
            if (CurrentAttendanceLog.TimeOut != null)
                return CurrentAttendanceLog;
            if (CurrentAttendanceLog.TimeIn == null)
                return CurrentAttendanceLog;
            CurrentAttendanceLog.TimeOut = timeStamp ?? DateTime.Now;
            await _unitOfWork.Save();

            //Set CurrentAttendanceLog to null
            var attendanceLog = CurrentAttendanceLog;
            CurrentAttendanceLog = null;

            UpdateCurrentWeekAttendanceLog();
            return attendanceLog;
        }

        public decimal TotalWeeklyHoursRendered
        {
            get
            {
                if (CurrentContractor == null)
                    return 0;
                var totalHours = CurrentWeekAttendanceLogs.Sum(log => log.Duration);
                if (CurrentAttendanceLog != null && CurrentAttendanceLog.TimeIn.HasValue)
                {
                    var currentDuration = CurrentAttendanceLog.TimeOut != null ? CurrentAttendanceLog.Duration : (decimal)(DateTime.Now - CurrentAttendanceLog.TimeIn.Value).TotalHours;
                    totalHours += currentDuration;
                }
                return totalHours > CurrentContractor.MaximumWeeklyHours ? CurrentContractor.MaximumWeeklyHours : totalHours;
            }
        }

        private void UpdateCurrentWeekAttendanceLog()
        {
            if (CurrentContractor == null)
                return;
            var today = DateTime.Today;
            var sunday = today.AddDays(-(int)today.DayOfWeek);
            CurrentWeekAttendanceLogs = CurrentContractor.ContractorAttendanceLogs
                .Where(log => log.Date >= DateOnly.FromDateTime(sunday))
                .ToList();
        }
    }
}
using DomainLayer.Models.Contractor;
using DomainLayer.Models.ContractorAttendanceLog;

namespace ServicesLayer
{
    public class ContractorTrackerService : IContractorTrackerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ContractorAttendanceLogModel? _currentAttendanceLog;

        public ContractorModel? CurrentContractor { get; private set; }
        public IList<ContractorAttendanceLogModel> _currentWeekAttendanceLogs { get; private set; } = [];

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
                if (_currentAttendanceLog != null)
                {
                    _currentAttendanceLog.TimeOut = DateTime.Now;
                    await _unitOfWork.Save();
                    _currentAttendanceLog = null;
                }
                _currentWeekAttendanceLogs.Clear();
            }

            CurrentContractor = await _unitOfWork.ContractorRepository.GetAsync(c => c.UserId == UserId, includeProperties: "ContractorAttendanceLogs");

            UpdateCurrentWeekAttendanceLog();
            return CurrentContractor;
        }

        public async Task<ContractorAttendanceLogModel?> StartSession()
        {
            if (CurrentContractor == null)
                return null;
            if (_currentAttendanceLog != null)
                return _currentAttendanceLog;
            _currentAttendanceLog = new ContractorAttendanceLogModel()
            {
                ContractorId = CurrentContractor.ContractorId,
                Contractor = CurrentContractor,
                Date = DateOnly.FromDateTime(DateTime.Now),
                TimeIn = DateTime.Now
            };
            CurrentContractor.ContractorAttendanceLogs.Add(_currentAttendanceLog);
            await _unitOfWork.Save();

            UpdateCurrentWeekAttendanceLog();
            return _currentAttendanceLog;
        }

        public async Task<ContractorAttendanceLogModel?> EndSession()
        {
            //Checks if the current contractor is null, if the current attendance log is null, and if the time out is already set.
            if (CurrentContractor == null)
                return null;
            if (_currentAttendanceLog == null)
                return null;
            if (_currentAttendanceLog.TimeOut != null)
                return _currentAttendanceLog;
            if (_currentAttendanceLog.TimeIn == null)
                return _currentAttendanceLog;

            _currentAttendanceLog.TimeOut = DateTime.Now;
            await _unitOfWork.Save();

            //Sets the current attendance log to null and returns the current attendance log.
            var attendanceLog = _currentAttendanceLog;
            _currentAttendanceLog = null;

            UpdateCurrentWeekAttendanceLog();
            return attendanceLog;
        }

        public decimal TotalWeeklyHoursRendered
        {
            get
            {
                if (CurrentContractor == null)
                    return 0;
                var totalHours = _currentWeekAttendanceLogs.Sum(log => log.Duration);
                if (_currentAttendanceLog != null && _currentAttendanceLog.TimeIn.HasValue)
                {
                    var currentDuration = _currentAttendanceLog.TimeOut != null ? _currentAttendanceLog.Duration : (decimal)(DateTime.Now - _currentAttendanceLog.TimeIn.Value).TotalHours;
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
            _currentWeekAttendanceLogs = CurrentContractor.ContractorAttendanceLogs
                .Where(log => log.Date >= DateOnly.FromDateTime(sunday))
                .ToList();
        }
    }
}
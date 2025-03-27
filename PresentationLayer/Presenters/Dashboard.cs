using DomainLayer.Models.User;
using DomainLayer.ViewModels.AttendanceLog;
using DomainLayer.ViewModels.DashboardDetails;
using DomainLayer.ViewModels.JobDeskDetails;
using ServicesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Presenters
{
    public class Dashboard : IDashboardPresenter
    {
        private readonly IUnitOfWork _unitOfWork;

        public Dashboard(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public DashboardDetailsViewModel GetDashboardDetails(IUserModel user)
        {
            return _unitOfWork.GetDashboardDetails(user);
        }
        public IEnumerable<AttendanceLogViewModel> GetAttendanceLogs(IUserModel user)
        {
            return _unitOfWork.GetAttendanceLog(user);
        }
        public JobDeskDetailsViewModel GetJobDeskDetails(IUserModel user)
        {
            return _unitOfWork.GetJobDeskDetails(user);
        }
    }
}

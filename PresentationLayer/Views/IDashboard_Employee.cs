using ServicesLayer;

namespace PresentationLayer
{
    public interface IDashboard_Employee
    {
        void Dashboard_Employee_Load(object sender, EventArgs e);
        void InitAttendanceButtonProperties();
        void InitAttendanceLogProperties();
        void InitDashboardCardProperties();
        void InitDataGrid();
        void InitJobDeskButtons();
        void InitJobDeskPanelProperties();
        void InitLeaveCardProperties();
        void InitMenuButtonProperties();
        void InitSummaryPanelProperties();
        void InitViewProfileButtonProperties();
    }
}
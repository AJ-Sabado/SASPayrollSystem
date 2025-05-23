namespace DomainLayer.Services
{
    public class BusinessIdGenerator
    {
        public static string GenerateContractorAttendanceLogId()
        {
            string dateTime = DateTime.Now.ToString("yyMMddHH");
            string guid = Guid.NewGuid().ToString("N").Substring(0,8);

            return $"CAL-{dateTime}-{guid}";
        }

        public static string GenerateEmployeeAttendanceLogId()
        {
            string dateTime = DateTime.Now.ToString("yyMMddHH");
            string guid = Guid.NewGuid().ToString("N").Substring(0, 8);

            return $"EAL-{dateTime}-{guid}";
        }

        public static string GenerateEmployeeAttendanceRequestId()
        {
            string dateTime = DateTime.Now.ToString("yyMMddHH");
            string guid = Guid.NewGuid().ToString("N").Substring(0, 8);

            return $"EAR-{dateTime}-{guid}";
        }

        public static string GenerateEmployeeLeaveId()
        {
            string dateTime = DateTime.Now.ToString("yyMMddHH");
            string guid = Guid.NewGuid().ToString("N").Substring(0, 8);

            return $"EL-{dateTime}-{guid}";
        }
    }
}

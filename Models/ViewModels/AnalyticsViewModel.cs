namespace IRIS_Web_Rec25_241EC152.Models.ViewModels
{
    public class AnalyticsViewModel
    {
        // Equipment Analytics
        public List<EquipmentBookingStats> MostBookedEquipment { get; set; }
        public List<PeakHourStats> EquipmentPeakHours { get; set; }

        // Court Analytics
        public List<CourtBookingStats> MostBookedCourts { get; set; }
        public List<PeakHourStats> CourtPeakHours { get; set; }

        // Combined Trends
        public Dictionary<string, int> DailyTrends { get; set; }
        public Dictionary<string, int> WeeklyTrends { get; set; }
        public Dictionary<string, int> MonthlyTrends { get; set; }
    }

    public class EquipmentBookingStats
    {
        public string EquipmentName { get; set; }
        public int TotalBookings { get; set; }
        public int TotalHoursBooked { get; set; }
    }

    public class CourtBookingStats
    {
        public string CourtName { get; set; }
        public int TotalBookings { get; set; }
        public int TotalHoursBooked { get; set; }
    }

    public class PeakHourStats
    {
        public int Hour { get; set; }
        public int BookingCount { get; set; }
        public int TotalHours { get; set; }
    }
}

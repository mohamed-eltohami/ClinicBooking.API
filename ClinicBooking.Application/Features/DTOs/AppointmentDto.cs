namespace ClinicBooking.Application.Features.DTOs
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public string Status { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}
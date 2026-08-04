namespace ClinicBooking.Application.Features.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int NoShowCount { get; set; }
    }
}
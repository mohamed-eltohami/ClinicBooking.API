using MediatR;

namespace ClinicBooking.Application.Features.Commands.Doctors.CreateDoctor
{
    public class CreateDoctorCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public TimeSpan WorkStartTime { get; set; }
        public TimeSpan WorkEndTime { get; set; }
    }
}
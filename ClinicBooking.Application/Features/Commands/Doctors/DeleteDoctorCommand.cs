using MediatR;

namespace ClinicBooking.Application.Features.Commands.Doctors.DeleteDoctor
{
    public class DeleteDoctorCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
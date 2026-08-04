using MediatR;

namespace ClinicBooking.Application.Features.Commands.Appointments.CheckInAppointment
{
    public class CheckInAppointmentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
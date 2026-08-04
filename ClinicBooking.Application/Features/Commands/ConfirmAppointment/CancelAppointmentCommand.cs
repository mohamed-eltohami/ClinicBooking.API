using MediatR;

namespace ClinicBooking.Application.Features.Commands.Appointments.CancelAppointment
{
    public class CancelAppointmentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
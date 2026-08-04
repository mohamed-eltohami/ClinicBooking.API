using MediatR;

namespace ClinicBooking.Application.Features.Commands.Appointments.ConfirmAppointment
{
    public class ConfirmAppointmentCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
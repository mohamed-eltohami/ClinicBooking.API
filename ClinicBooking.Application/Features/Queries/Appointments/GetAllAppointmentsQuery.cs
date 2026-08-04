using ClinicBooking.Application.Features.DTOs;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Appointments.GetAllAppointments
{
    public class GetAllAppointmentsQuery : IRequest<List<AppointmentDto>>
    {
    }
}
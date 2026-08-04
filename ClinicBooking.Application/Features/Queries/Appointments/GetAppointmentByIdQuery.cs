using ClinicBooking.Application.Features.DTOs;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Appointments.GetAppointmentById
{
    public class GetAppointmentByIdQuery : IRequest<AppointmentDto>
    {
        public int Id { get; set; }
    }
}
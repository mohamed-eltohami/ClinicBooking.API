using ClinicBooking.Application.Features.DTOs;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Doctors.GetDoctorById
{
    public class GetDoctorByIdQuery : IRequest<DoctorDto>
    {
        public int Id { get; set; }
    }
}
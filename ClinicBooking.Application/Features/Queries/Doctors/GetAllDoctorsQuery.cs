using ClinicBooking.Application.Features.DTOs;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Doctors.GetAllDoctors
{
    public class GetAllDoctorsQuery : IRequest<List<DoctorDto>>
    {
    }
}
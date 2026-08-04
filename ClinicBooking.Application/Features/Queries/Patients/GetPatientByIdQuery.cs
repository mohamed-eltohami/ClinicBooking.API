using ClinicBooking.Application.Features.DTOs;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Patients.GetPatientById
{
    public class GetPatientByIdQuery : IRequest<PatientDto>
    {
        public int Id { get; set; }
    }
}
using ClinicBooking.Application.Features.DTOs;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Patients.GetAllPatients
{
    public class GetAllPatientsQuery : IRequest<List<PatientDto>>
    {
    }
}
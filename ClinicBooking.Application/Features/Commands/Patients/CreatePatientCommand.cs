using MediatR;

namespace ClinicBooking.Application.Features.Commands.Patients.CreatePatient
{
    public class CreatePatientCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
using MediatR;

namespace ClinicBooking.Application.Features.Commands.Patients.DeletePatient
{
    public class DeletePatientCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
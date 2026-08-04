using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using ClinicBooking.Domain.Repository;
using MediatR;

namespace ClinicBooking.Application.Features.Commands.Patients.DeletePatient
{
    public class DeletePatientCommandHandler : IRequestHandler<DeletePatientCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeletePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(request.Id, cancellationToken);

            if (patient is null)
                throw new DomainException("Patient not found.");

            _unitOfWork.Patients.Delete(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
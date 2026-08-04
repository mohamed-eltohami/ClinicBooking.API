using ClinicBooking.Domain.Interfaces;
using MediatR;

namespace ClinicBooking.Application.Features.Commands.Doctors.DeleteDoctor
{
    public class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDoctorCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id, cancellationToken);

            if (doctor is null)
                return false;

            _unitOfWork.Doctors.Delete(doctor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }

    }
}
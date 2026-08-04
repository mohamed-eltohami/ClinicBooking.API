using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using ClinicBooking.Domain.Repository;
using MediatR;

namespace ClinicBooking.Application.Features.Commands.Appointments.CancelAppointment
{
    public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id, cancellationToken);

            if (appointment is null)
                throw new DomainException("Appointment not found.");

            appointment.Cancel();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
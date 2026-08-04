using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using MediatR;

namespace ClinicBooking.Application.Features.Commands.Appointments.ConfirmAppointment
{
    public class ConfirmAppointmentCommandHandler : IRequestHandler<ConfirmAppointmentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ConfirmAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id, cancellationToken);

            if (appointment is null)
                throw new DomainException("Appointment not found.");

            appointment.Confirm();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
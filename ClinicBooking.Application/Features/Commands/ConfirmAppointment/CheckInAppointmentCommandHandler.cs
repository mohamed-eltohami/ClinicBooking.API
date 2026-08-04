using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using MediatR;

namespace ClinicBooking.Application.Features.Commands.Appointments.CheckInAppointment
{
    public class CheckInAppointmentCommandHandler : IRequestHandler<CheckInAppointmentCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckInAppointmentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CheckInAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id, cancellationToken);

            if (appointment is null)
                throw new DomainException("Appointment not found.");

            appointment.CheckIn();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
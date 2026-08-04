using ClinicBooking.Application.Features.Commands.Appointments.Commands.CreateAppointment;
using ClinicBooking.Application.Services;
using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using Hangfire;
using MediatR;

namespace ClinicBooking.Application.Features.Commands.Appointments.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public CreateAppointmentCommandHandler(
            IUnitOfWork unitOfWork,
            IBackgroundJobClient backgroundJobClient)
        {
            _unitOfWork = unitOfWork;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<int> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var conflict = await _unitOfWork.Appointments.HasConflictAsync(
                request.DoctorId, request.ScheduledAt, cancellationToken);

            if (conflict)
                throw new DomainException("This time slot is already booked.");

            var appointment = new Appointment(request.DoctorId, request.PatientId, request.ScheduledAt);

            await _unitOfWork.Appointments.AddAsync(appointment, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var reminderTime = request.ScheduledAt.AddHours(-1);

            if (reminderTime > DateTime.UtcNow)
            {
                _backgroundJobClient.Schedule<IAppointmentJobService>(
                    service => service.SendAppointmentReminderAsync(appointment.Id),
                    reminderTime - DateTime.UtcNow);
            }
            var noShowTime = request.ScheduledAt.AddMinutes(15);
            _backgroundJobClient.Schedule<IAppointmentJobService>(
                service => service.MarkAsNoShowAsync(appointment.Id),
                noShowTime - DateTime.UtcNow);

            return appointment.Id;
        }
    }
}
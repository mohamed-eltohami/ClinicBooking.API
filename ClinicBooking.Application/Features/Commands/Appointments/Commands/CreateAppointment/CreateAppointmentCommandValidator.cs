using ClinicBooking.Application.Features.Commands.Appointments.Commands.CreateAppointment;
using FluentValidation;

namespace ClinicBooking.Application.Features.Commands.Appointments.CreateAppointment
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .GreaterThan(0)
                .WithMessage("DoctorId must be a valid doctor.");

            RuleFor(x => x.PatientId)
                .GreaterThan(0)
                .WithMessage("PatientId must be a valid patient.");

            RuleFor(x => x.ScheduledAt)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Appointment time must be in the future.");
        }
    }
}
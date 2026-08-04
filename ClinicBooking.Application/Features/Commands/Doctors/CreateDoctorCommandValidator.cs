using FluentValidation;

namespace ClinicBooking.Application.Features.Commands.Doctors.CreateDoctor
{
    public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
    {
        public CreateDoctorCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Doctor name is required.")
                .MaximumLength(150);

            RuleFor(x => x.Specialty)
                .NotEmpty().WithMessage("Specialty is required.")
                .MaximumLength(100);

            RuleFor(x => x.WorkStartTime)
                .LessThan(x => x.WorkEndTime)
                .WithMessage("Work start time must be before work end time.");
        }
    }
}
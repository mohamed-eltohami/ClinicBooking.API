using ClinicBooking.Application.Features.Commands.Patients.CreatePatient;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Application.Features.Commands.Patients
{
    public class CreatePatientCommandValidator : AbstractValidator<CreatePatientCommand>
    {
        public CreatePatientCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("patient name is required please")
                .MaximumLength(150);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Nmuber Is Required Please")
                .MaximumLength(16);

        }
    }
}

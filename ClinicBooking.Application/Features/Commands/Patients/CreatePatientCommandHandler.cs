using ClinicBooking.Application.Features.Commands.Patients.CreatePatient;
using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Application.Features.Commands.Patients
{
    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, int>
    {
        public readonly IUnitOfWork _unitOfWork;
        public CreatePatientCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken )
        {
            var patient = new Patient(request.FullName, request.PhoneNumber);

            await _unitOfWork.Patients.AddAsync(patient, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return patient.Id;
        }
    }
}

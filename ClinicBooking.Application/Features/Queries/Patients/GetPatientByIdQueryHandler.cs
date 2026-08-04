using AutoMapper;
using ClinicBooking.Application.Features.DTOs;
using ClinicBooking.Application.Features.Queries.Patients.GetPatientById;
using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Application.Features.Queries.Patients
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, PatientDto> 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPatientByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<PatientDto> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.Patients.GetByIdAsync(request.Id, cancellationToken);
            if (patient is null)
                throw new DomainException("Patient not found.");
            return _mapper.Map<PatientDto>(patient);
        }
    }
}

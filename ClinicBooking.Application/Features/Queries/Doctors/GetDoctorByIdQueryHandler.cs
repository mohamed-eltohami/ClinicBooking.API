using ClinicBooking.Application.Features.DTOs;
using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Doctors.GetDoctorById
{
    public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery, DoctorDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDoctorByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DoctorDto> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.Id, cancellationToken);

            if (doctor is null)
                throw new DomainException("Doctor not found.");

            return new DoctorDto
            {
                Id = doctor.Id,
                FullName = doctor.FullName,
                Specialty = doctor.Specialty,
                WorkStartTime = doctor.WorkStartTime,
                WorkEndTime = doctor.WorkEndTime
            };
        }
    }
}
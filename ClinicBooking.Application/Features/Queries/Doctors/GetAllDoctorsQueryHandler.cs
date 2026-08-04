using AutoMapper;
using ClinicBooking.Application.Features.DTOs;
using ClinicBooking.Domain.Repository;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Doctors.GetAllDoctors
{
    public class GetAllDoctorsQueryHandler
        : IRequestHandler<GetAllDoctorsQuery, List<DoctorDto>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IMapper _mapper;

        public GetAllDoctorsQueryHandler(
            IDoctorRepository doctorRepository,
            IMapper mapper)
        {
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }

        public async Task<List<DoctorDto>> Handle(
            GetAllDoctorsQuery request,
            CancellationToken cancellationToken)
        {
            var doctors = await _doctorRepository.GetAllAsync(cancellationToken);

            return _mapper.Map<List<DoctorDto>>(doctors);
        }
    }
}
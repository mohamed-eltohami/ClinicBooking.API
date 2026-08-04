using AutoMapper;
using ClinicBooking.Application.Features.DTOs;
using ClinicBooking.Domain.Interfaces;
using ClinicBooking.Domain.Repository;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Appointments.GetAllAppointments
{
    public class GetAllAppointmentsQueryHandler : IRequestHandler<GetAllAppointmentsQuery, List<AppointmentDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllAppointmentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AppointmentDto>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _unitOfWork.Appointments.GetAllAsync(cancellationToken);

            return _mapper.Map<List<AppointmentDto>>(appointments);
        }
    }
}
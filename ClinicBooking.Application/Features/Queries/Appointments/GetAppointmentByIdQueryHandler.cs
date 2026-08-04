using AutoMapper;
using ClinicBooking.Application.Features.DTOs;
using ClinicBooking.Domain.Exceptions;
using ClinicBooking.Domain.Interfaces;
using ClinicBooking.Domain.Repository;
using MediatR;

namespace ClinicBooking.Application.Features.Queries.Appointments.GetAppointmentById
{
    public class GetAppointmentByIdQueryHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAppointmentByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppointmentDto> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.Appointments.GetByIdAsync(request.Id, cancellationToken);

            if (appointment is null)
                throw new DomainException("Appointment not found.");

            return _mapper.Map<AppointmentDto>(appointment);
        }
    }
}
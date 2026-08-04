using AutoMapper;
using ClinicBooking.Application.Features.DTOs;
using ClinicBooking.Domain.Entities;

namespace ClinicBooking.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorDto>();
            CreateMap<Patient, PatientDto>();
            CreateMap<Appointment, AppointmentDto>()
               .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }
    }
}
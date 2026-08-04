using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Application.Features.Commands.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<int>
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime ScheduledAt { get; set; }
    }
}

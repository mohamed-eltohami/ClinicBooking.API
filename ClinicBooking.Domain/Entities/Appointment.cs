using ClinicBooking.Domain.Enums;
using ClinicBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public int DoctorId { get; private set; }
        public Doctor Doctor { get; private set; }


        public int PatientId { get; private set; }
        public Patient Patient { get; private set; }


        public DateTime ScheduledAt { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public bool IsCheckedIn { get; private set; }

        private Appointment() { }

        public Appointment(int doctorId, int patientId, DateTime scheduledAt)
        {
            if (scheduledAt <= DateTime.UtcNow)
                throw new DomainException("Appointment time must be in the future.");
            DoctorId = doctorId;
            PatientId = patientId;
            ScheduledAt = scheduledAt;
            Status = AppointmentStatus.Pending;
            IsCheckedIn = false;
        }
        public void Confirm ()
        {
            if(Status != AppointmentStatus.Pending)
                throw new DomainException("Only pending appointments can be confirmed.");
            Status = AppointmentStatus.Confirmed;
        }
        public void Cancel()
        {
            if (Status == AppointmentStatus.Completed)
                throw new DomainException("Completed appointments cannot be cancelled.");

            Status = AppointmentStatus.Cancelled;
        }
        public void CheckIn()
        {
            if (Status != AppointmentStatus.Confirmed)
                throw new DomainException("Only confirmed appointments can be checked in.");

            IsCheckedIn = true;
        }
        public void Complete()
        {
            if (!IsCheckedIn)
                throw new DomainException("Cannot complete an appointment without check-in.");

            Status = AppointmentStatus.Completed;
        }
        public void MarkAsNoShow()
        {
            if (Status != AppointmentStatus.Confirmed || IsCheckedIn)
                throw new DomainException("Only confirmed, non-checked-in appointments can be marked as no-show.");
            Status = AppointmentStatus.NoShow;
        }

    }
}
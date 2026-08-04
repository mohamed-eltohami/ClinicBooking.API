using ClinicBooking.Domain.Enums;
using ClinicBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicBooking.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string FullName { get; private set; }
        public string Specialty { get; private set; }
        public TimeSpan WorkStartTime { get; private set; }
        public TimeSpan WorkEndTime { get; private set; }

        private readonly List<Appointment> _appointment = new();
        public IReadOnlyCollection<Appointment> Appointments => _appointment.AsReadOnly();

        // EF Core
        private Doctor() { }

        public Doctor(string fullName, string specialty, TimeSpan workStart, TimeSpan workEnd)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Doctor name is required.");

            if (workStart >= workEnd)
                throw new DomainException("Work start time must be before work end time.");

            FullName = fullName;
            Specialty = specialty;
            WorkStartTime = workStart;
            WorkEndTime = workEnd;
        }

        public bool IsWithinWorkingHours(TimeSpan time)
            => time >= WorkStartTime && time <= WorkEndTime;

        public bool HasConflict(DateTime scheduledAt, IEnumerable<Appointment> existingAppointments)
        {
            return existingAppointments.Any(a =>
                a.Status != AppointmentStatus.Cancelled &&
                a.Status != AppointmentStatus.NoShow &&
                a.ScheduledAt == scheduledAt);
        }
    }
}
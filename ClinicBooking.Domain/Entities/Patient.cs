using ClinicBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace ClinicBooking.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FullName { get; private set; }
        public string PhoneNumber { get; private set; }
        public int NoShowCount { get; private set; }

        private readonly List<Appointment> _appointment = new();
        public IReadOnlyCollection<Appointment> Appointments => _appointment.AsReadOnly();
        private Patient() { }

        public Patient(string fullName, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("Patient name is required.");
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new DomainException("Phone number is required.");
            FullName = fullName;
            PhoneNumber = phoneNumber;
            NoShowCount = 0;
        }
        public void RecordNoShow()
        {
            NoShowCount++;
        }

    }
}

using ClinicBooking.Domain.Interfaces;
using ClinicBooking.Domain.Repository;
using ClinicBooking.Infrastructure.Persistence;

namespace ClinicBooking.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IDoctorRepository Doctors { get; }
        public IPatientRepository Patients { get; }
        public IAppointmentRepository Appointments { get; }

        public UnitOfWork(
            AppDbContext context,
            IDoctorRepository doctors,
            IPatientRepository patients,
            IAppointmentRepository appointments)
        {
            _context = context;
            Doctors = doctors;
            Patients = patients;
            Appointments = appointments;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}
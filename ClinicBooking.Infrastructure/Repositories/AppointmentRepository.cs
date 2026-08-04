using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Enums;
using ClinicBooking.Domain.Repository;
using ClinicBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Infrastructure.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> HasConflictAsync(int doctorId, DateTime scheduledAt, CancellationToken cancellationToken)
            => await _context.Appointments
                .AnyAsync(a =>
                    a.DoctorId == doctorId &&
                    a.ScheduledAt == scheduledAt &&
                    a.Status != AppointmentStatus.Cancelled &&
                    a.Status != AppointmentStatus.NoShow,
                    cancellationToken);

        public async Task<List<Appointment>> GetUpcomingByDoctorAsync(int doctorId, CancellationToken cancellationToken)
            => await _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.ScheduledAt > DateTime.UtcNow)
                .OrderBy(a => a.ScheduledAt)
                .ToListAsync(cancellationToken);

        public async Task<List<Appointment>> GetByPatientAsync(int patientId, CancellationToken cancellationToken)
            => await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .OrderByDescending(a => a.ScheduledAt)
                .ToListAsync(cancellationToken);
    }
}
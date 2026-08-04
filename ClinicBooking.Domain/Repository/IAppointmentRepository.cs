using ClinicBooking.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicBooking.Domain.Repository
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>  
    {
        Task<bool> HasConflictAsync(int doctorId, DateTime scheduledAt, CancellationToken cancellationToken);
        Task<List<Appointment>> GetUpcomingByDoctorAsync(int doctorId, CancellationToken cancellationToken);
        Task<List<Appointment>> GetByPatientAsync(int patientId, CancellationToken cancellationToken);
    }
}
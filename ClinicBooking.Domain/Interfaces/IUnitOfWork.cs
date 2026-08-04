using ClinicBooking.Domain.Repository;
using System.Threading;
using System.Threading.Tasks;
namespace ClinicBooking.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IDoctorRepository Doctors { get; }
        IPatientRepository Patients { get; }
        IAppointmentRepository Appointments { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Repository;
using ClinicBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Infrastructure.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Patient?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
            => await _context.Patients
                .FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber, cancellationToken);
    }
}
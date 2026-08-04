using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Repository;
using ClinicBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Infrastructure.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Doctor>> GetBySpecialtyAsync(string specialty, CancellationToken cancellationToken)
            => await _context.Doctors
                .Where(d => d.Specialty == specialty)
                .ToListAsync(cancellationToken);
    }
}
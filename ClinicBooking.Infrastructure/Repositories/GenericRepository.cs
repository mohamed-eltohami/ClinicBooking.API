using ClinicBooking.Domain.Entities;
using ClinicBooking.Domain.Repository;
using ClinicBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClinicBooking.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
            => await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
            => await _context.Set<T>().ToListAsync(cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
            => await _context.Set<T>().AddAsync(entity, cancellationToken);

        public void Update(T entity)
            => _context.Set<T>().Update(entity);

        public void Delete(T entity)
            => entity.IsDeleted = true; 
    }
}
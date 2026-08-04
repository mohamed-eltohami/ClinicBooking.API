using ClinicBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.Domain.Repository
{
    
        public interface IGenericRepository<T> where T : BaseEntity
        {
            Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
            Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
            Task AddAsync(T entity, CancellationToken cancellationToken);
            void Update(T entity);
            void Delete(T entity);
        }
    }

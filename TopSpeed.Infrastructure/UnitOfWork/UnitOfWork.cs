using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Infrastructure.Common;
using TopSpeed.Infrastructure.Repositories;

namespace TopSpeed.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Brand = new BrandRepository(_dbContext);
            VehicleType = new VehicleTypeRepository(_dbContext);
            Post = new PostRepository(_dbContext);  
        }


        public IBrandRepository Brand { get; private set; }

        public IVehicleTypeRepository VehicleType { get; private set; }

        public IPostRepository Post { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();   
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}

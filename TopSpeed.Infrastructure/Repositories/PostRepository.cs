using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopSpeed.Application.Contracts.Presistence;
using TopSpeed.Domain.Models;
using TopSpeed.Infrastructure.Common;

namespace TopSpeed.Infrastructure.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task Update(Post post)
        {

            var objFromDb = await _dbContext.Post.FirstOrDefaultAsync(x => x.Id == post.Id);

            if (objFromDb != null)
            {
                objFromDb.BrandId = post.BrandId;
                objFromDb.VehicleTypeId = post.VehicleTypeId;
                objFromDb.Name = post.Name;
                objFromDb.EngineAndFuelType = post.EngineAndFuelType;
                objFromDb.Transmission = post.Transmission;
                objFromDb.Engine = post.Engine;
                objFromDb.Range = post.Range;
                objFromDb.Ratings = post.Ratings;
                objFromDb.SeatingCapacity = post.SeatingCapacity;
                objFromDb.Mileage = post.Mileage;
                objFromDb.PriceFrom = post.PriceFrom;
                objFromDb.PriceTo = post.PriceTo;
                objFromDb.TopSpeed = post.TopSpeed;

                if (post.VehicleImage != null)
                {
                    objFromDb.VehicleImage = post.VehicleImage;
                }

                _dbContext.Update(objFromDb);
            }
        }


        public async Task<Post> GetPostById(Guid id)
        {
            return await _dbContext.Post.Include(x => x.Brand).Include(x => x.VehicleType).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Post>> GetAllPost()
        {
            return await _dbContext.Post.Include(x => x.Brand).Include(x => x.VehicleType).ToListAsync();
        }
    }
}

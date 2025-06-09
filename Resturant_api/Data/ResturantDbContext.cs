using Microsoft.EntityFrameworkCore;
using Resturant_api.Model.Domain;

namespace Resturant_api.Data
{
    public class ResturantDbContext: DbContext
    {
        public ResturantDbContext(DbContextOptions<ResturantDbContext> dbContextOptions): base(dbContextOptions)
        {
                
        }
        public DbSet<Menu> Menus { get; set; }
    }
}

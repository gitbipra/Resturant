using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Resturant_api.Data
{
    public class RestAuthDbContext : IdentityDbContext
    {
        public RestAuthDbContext(DbContextOptions<RestAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "2622aabf-1c94-4298-9648-cce147981df8";
            var WriterRoleId = "4194bab5-32e0-45b9-b6a0-f0fe3265dc2b";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp = WriterRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

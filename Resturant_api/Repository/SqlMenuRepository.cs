using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Resturant_api.Data;
using Resturant_api.Model.Domain;

namespace Resturant_api.Repository
{
    public class SqlMenuRepository : IMenuRepository
    {
        private readonly ResturantDbContext _dbContext;

        public SqlMenuRepository(ResturantDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Menu>> GetAllAsync()
        {
            return await _dbContext.Menus.ToListAsync();
        }

        public async Task<Menu?> GetByMenuIdAsnyc(Guid MenuId)
        {
            return await _dbContext.Menus.FirstOrDefaultAsync(x => x.MenuId == MenuId);
        }

        public async Task<Menu> CreateAsnyc(Menu menu)
        {
            await _dbContext.Menus.AddAsync(menu);
            await _dbContext.SaveChangesAsync();

            return menu;
        }

        public async Task<Menu> UpdateAsnyc(Guid MenuId, Menu menu)
        {
            var existingItem = await _dbContext.Menus.FirstOrDefaultAsync(x => x.MenuId == MenuId);
            if (existingItem == null)
            {
                return null;
            }
            existingItem.FoodName = menu.FoodName;
            existingItem.Category = menu.Category;
            existingItem.FoodType = menu.FoodType;
            existingItem.Price = menu.Price;
            existingItem.Description = menu.Description;
            existingItem.ImageUrl = menu.ImageUrl;
            existingItem.CreateDate = menu.CreateDate;

            await _dbContext.SaveChangesAsync();
            return existingItem;

        }

        public async Task<Menu> DeleteByMenuIdAsync(Guid MenuId)
        {
           var existingItem = await _dbContext.Menus.FirstOrDefaultAsync(x => x.MenuId == MenuId);

            if (existingItem == null)
            {
                return null;
            }
            _dbContext.Menus.Remove(existingItem);
            await _dbContext.SaveChangesAsync();

            return existingItem;
        }

    }
}

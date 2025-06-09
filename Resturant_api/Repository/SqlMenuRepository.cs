using System.Reflection.Metadata.Ecma335;
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

        public async Task<List<Menu>> GetAllAsync(string? filterQuery = null)
        {
            var ItemList = _dbContext.Menus.AsQueryable();
            List<Menu> result = new List<Menu>();

            if (!string.IsNullOrWhiteSpace(filterQuery))
            {
                filterQuery = filterQuery.Trim();

                if (_dbContext.Menus.Any(x => x.FoodName.Contains(filterQuery) && x.FoodType.Contains(filterQuery)))
                {
                    result.AddRange(await _dbContext.Menus.Where(x => x.FoodName.Contains(filterQuery) && x.FoodType.Contains(filterQuery)).ToListAsync());
                }
                else if (_dbContext.Menus.Any(x => x.Category.Contains(filterQuery)))
                {
                    result.AddRange(await _dbContext.Menus.Where(x => x.Category.Contains(filterQuery)).ToListAsync());
                }
                else
                {
                    result.AddRange(await _dbContext.Menus.Where(x => x.FoodType.Contains(filterQuery)).ToListAsync());
                }
            }
            else
            {
                return await ItemList.ToListAsync();
            }
            return result;
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

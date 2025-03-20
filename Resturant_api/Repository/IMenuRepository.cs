using Resturant_api.Model.Domain;

namespace Resturant_api.Repository
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllAsync();
        Task<Menu?> GetByMenuIdAsnyc(Guid MenuId);
        Task<Menu> CreateAsnyc(Menu menu);
        Task<Menu> UpdateAsnyc(Guid MenuId, Menu menu);
        Task<Menu> DeleteByMenuIdAsync(Guid MenuId);
    }
}

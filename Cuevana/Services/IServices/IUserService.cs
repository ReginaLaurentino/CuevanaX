using Cuevana.Models;

namespace Cuevana.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UserNameExistsAsync(string userName);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task<bool> UserExistsAsync(int id);
        Task DeleteUserAsync(int id);
        User ValidateLogin(User usuario);
    }
}

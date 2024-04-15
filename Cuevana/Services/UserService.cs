using Cuevana.Models;
using Cuevana.Repositories.IRepositories;
using Cuevana.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Cuevana.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _userRepository.UserNameExistsAsync(userName);
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return await _userRepository.UserExistsAsync(id);
        }
        public async Task DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);
            }
            catch (DatabaseOperationException ex)
            {
                throw new ServiceException("Error al eliminar el Usuario.", ex);
            }
           
        }
        public User ValidateLogin(User usuario)
        {
            var user = _userRepository.ValidateLogin(usuario);
            return user;
        }
    }
}

using MealMate.Models;

namespace MealMate.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(string id);
    Task<User> UpdateUserAsync(User user, string id);
    Task DeleteUserAsync(string id);
}
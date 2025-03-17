using MealMate.Models;

namespace MealMate.Services.Interfaces
{
    interface IUserService
    {
        Task<User> UpdateUser(User user);

public interface IUserService
{
    Task<User> GetUserByIdAsync(string id);
    Task<User> UpdateUserAsync(User user, string id);
    Task DeleteUserAsync(string id);
}
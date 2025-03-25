namespace MealMate.Services.Interfaces;

public interface IUserService
{
    Task<User> GetUserById(string id);
    Task<User> UpdateUser(User user, string id);

}

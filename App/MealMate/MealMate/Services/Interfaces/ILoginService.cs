namespace MealMate.Services.Interfaces;

public interface ILoginService
{
    Task<User> Login(string email, string password);
    Task<User> CreateUser(User user);
}

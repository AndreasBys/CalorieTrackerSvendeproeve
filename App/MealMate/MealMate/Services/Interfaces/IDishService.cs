
namespace MealMate.Services.Interfaces
{
    interface IDishService
    {

        Task<List<Dish>> GetAllRetter();
        Task<List<Dish>> SearchRetter(string searchTerm);
        Task<DishResponse> CreateDish(DishRequest newFood);
    }
}

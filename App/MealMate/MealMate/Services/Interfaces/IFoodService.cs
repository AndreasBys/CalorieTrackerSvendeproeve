using MealMate.Models;

namespace MealMate.Services.Interfaces;

public interface IFoodService
{
    Task<List<Food>> GetAllFoods();
    Task<Food> GetFoodByBarcode(string id);
}

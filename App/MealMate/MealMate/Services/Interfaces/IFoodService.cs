using MealMate.Models;

namespace MealMate.Services.Interfaces;

public interface IFoodService
{
    Task<List<Food>> GetAllFoods();
    Task<List<Food>> SearchFoods(string searchTerm);
    Task<Food> GetFoodByBarcode(string barcode);
    Task<Food> CreateFood(Food newFood);
}

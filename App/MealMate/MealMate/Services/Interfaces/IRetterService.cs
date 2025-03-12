
namespace MealMate.Services.Interfaces
{
    interface IRetterService
    {

        Task<List<Retter>> GetAllRetter();
        Task<List<Retter>> SearchRetter(string searchTerm);
        Task<Retter> CreateRet(Retter newFood);
    }
}

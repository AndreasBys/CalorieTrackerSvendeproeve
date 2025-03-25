namespace MealMate.Services.Interfaces;

public interface IMacroLogService
{
    Task<MacroLog> CreateMacroLog(dynamic newMacroLog);
    Task<List<MacroLog>> GetTodaysMacroLogs();
}

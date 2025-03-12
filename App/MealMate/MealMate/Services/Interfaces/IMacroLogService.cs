namespace MealMate.Services.Interfaces;

public interface IMacroLogService
{
    Task<MacroLog> CreateMacroLog(NewMacroLog newMacroLog);
    Task<List<MacroLog>> GetTodaysMacroLogs();
}

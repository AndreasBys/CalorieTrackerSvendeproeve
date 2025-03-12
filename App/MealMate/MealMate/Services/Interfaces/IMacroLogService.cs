namespace MealMate.Services.Interfaces;

public interface IMacroLogService
{
    Task<MacroLog> CreateMacroLog(MacroLogRequest newMacroLog);
    Task<List<MacroLog>> GetTodaysMacroLogs();
}

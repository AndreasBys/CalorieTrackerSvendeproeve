namespace MealMate.Services.Interfaces;

public interface IMacroLogService
{
    Task<MacroLog> CreateMacroLog(MacroLog newMacroLog);
}

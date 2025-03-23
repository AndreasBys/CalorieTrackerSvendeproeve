using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services.Interfaces
{
    interface IMacroGoal
    {

        Task CreateMacroGoal(MacroGoal newMacroGoal);
        Task<MacroGoal> GetCurrentMacroGoal();
    }
}

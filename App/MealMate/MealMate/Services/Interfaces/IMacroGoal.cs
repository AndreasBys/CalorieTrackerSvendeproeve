using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services.Interfaces
{
    interface IMacroGoal
    {

        public Task<MacroGoal> CreateMacroGoal(MacroGoal newMacroGoal);

    }
}

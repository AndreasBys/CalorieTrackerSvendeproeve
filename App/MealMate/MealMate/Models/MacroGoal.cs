using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Models
{
    public class MacroGoal
    {
        [Key]
        public string id { get; set; }

        [Required]
        public DateTime startDate { get; set; }

        public DateTime? endDate { get; set; }

        public double? calories { get; set; }

        public double? carbohydrates { get; set; }

        public double? proteins { get; set; }

        public double? fats { get; set; }

        [Required]
        [DefaultValue(5)]
        public int Margin { get; set; } = 5;


        public User user { get; set; } 

    }

    public class MacroGoalResponse()
    {
        public MacroGoal macroGoal { get; set; }
    }
}

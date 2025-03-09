using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Model
{
    class Food
    {
        [Key]
        public int Id { get; set; } // Primary Key

        public string? Barcode { get; set; } // Nullable, not required

        [Required]
        public string Name { get; set; } = string.Empty; // Required field

        public bool Godkendt { get; set; } = false; // Defaults to false

        public double? Calories { get; set; } // Nullable (not required)
        public double? Carbonhydrates { get; set; }
        public double? Protein { get; set; }
        public double? Fat { get; set; }

    }
}

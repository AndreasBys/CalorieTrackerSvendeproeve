using System.ComponentModel.DataAnnotations;

namespace MealMate.Models;

public class MacroLog
{
    [Key]
    public string _id { get; set; }
    public Dish dish { get; set; }
    public Food food { get; set; }
    [Required]
    public int weight { get; set; }
    public DateTime date { get; set; }
    public int calories {get; set;}
    public double carbonhydrates {get; set;}
    public double protein {get; set;}
    public double fat { get; set;}
}

public class MacroLogRequest
{
    [Required]
    public string food { get; set; }
    [Required]
    public int weight { get; set; }
    public MacroLogRequest(string food, int weight)
    {
        this.food = food;
        this.weight = weight;
    }
}

public class MacroLogDishRequest
{
    [Required]
    public string dish { get; set; }
    [Required]
    public int weight { get; set; }
    public MacroLogDishRequest(string dish, int weight)
    {
        this.dish = dish;
        this.weight = weight;
    }
}


public class MacroLogResponse
{
    public MacroLog macroLog { get; set; }
}
public class MacroLogListResponse
{
    public List<MacroLog> macroLogs { get; set; }
}

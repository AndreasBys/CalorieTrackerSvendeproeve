using System.ComponentModel.DataAnnotations;

namespace MealMate.Models;

public class MacroLog
{
    [Key]
    public string _id { get; set; }

    [Required]
    public Food food { get; set; }
    [Required]
    public int weight { get; set; }
    public DateTime date { get; set; }
}

public class NewMacroLog
{
    [Required]
    public string food { get; set; }
    [Required]
    public int weight { get; set; }
    public NewMacroLog(string food, int weight)
    {
        this.food = food;
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

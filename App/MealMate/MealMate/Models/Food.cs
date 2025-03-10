using System.ComponentModel.DataAnnotations;

namespace MealMate.Models;

public class Food
{
    [Key]
    public string _id { get; set; }
    public bool godkendt { get; set; }
    [Required]
    public string name { get; set; }
    public int calories { get; set; }
    public int carbonhydrates { get; set; }
    public int protein { get; set; }
    public int fat { get; set; }
    public int __v { get; set; }
    public string user { get; set; }
}
public class FoodListResponse
{
    public List<Food> Foods { get; set; }
}
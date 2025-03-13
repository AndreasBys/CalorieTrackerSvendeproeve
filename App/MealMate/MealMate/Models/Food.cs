using System.ComponentModel.DataAnnotations;

namespace MealMate.Models;

public class Food
{
    [Key]
    public string? _id { get; set; }
    public string? barcode { get; set; }
    [Required]
    public string name { get; set; }
    public bool approved { get; set; }
    public int calories { get; set; }
    public double carbonhydrates { get; set; }
    public double protein { get; set; }
    public double fat { get; set; }
    public string user { get; set; }
}
public class FoodRequest
{
    public bool approved { get; set; }
    [Required]
    public string name { get; set; }
    public string? barcode { get; set; }
    public int calories { get; set; }
    public double carbonhydrates { get; set; }
    public double protein { get; set; }
    public double fat { get; set; }
    public string user { get; set; }
    public FoodRequest(string name,string barcode, int calories, double carbonhydrates, double protein, double fat, string user) 
    {
        this.name = name;
        this.barcode = barcode;
        this.calories = calories;
        this.carbonhydrates = carbonhydrates;
        this.protein = protein;
        this.fat = fat;
        this.user = user;
    }
}
public class FoodResponse
{
    public Food food { get; set; }
}
public class FoodListResponse
{
    public List<Food> foods { get; set; }
}
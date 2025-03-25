using System.ComponentModel.DataAnnotations;

namespace MealMate.Models;

public class Dish
{

    [Key]
    public string? _id { get; set; }  

    public string name { get; set; }

    public string user { get; set; }

    public List<FoodInDish> foods { get; set; }

    public int calories
    {
        get
        {
            return foods?.Sum(f => f.food.calories) ?? 0;
        }
    }

}

public class FoodInDish
{
    public string name { get; set; } // Bruges kun til at vise nave i listen af dishes
    public Food food { get; set; }
    public string _id { get; set; }

    public int weight { get; set; }

}

public class DishResponse

{
    public Dish dish { get; set; }
    public List<FoodInDish> foods { get; set; }
}

public class DishListResponse
{
    public List<Dish> dishes { get; set; }
}

public class DishRequest
{
    public string name { get; set; }

    public List<FoodInDishRequest> foods { get; set; }

}

public class FoodInDishRequest
{

    public string id { get; set; }

    public int weight { get; set; }
}



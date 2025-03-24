using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Models;

public class Dish
{

    [Key]
    public string? _id { get; set; }  

    public string name { get; set; }

    public string user { get; set; }

    public List<Food> foods { get; set; }

}


public class DishResponse
{
    public Dish dishes { get; set; }
}
public class DishListResponse

{
    public List<Dish> dishes { get; set; }
}

public class DishRequest
{
    public string name { get; set; }

    public List<FoodForDish> foods { get; set; }

}

public class FoodForDish
{
    public string name { get; set; }
    public string _id { get; set; }

    public int weight { get; set; }
}

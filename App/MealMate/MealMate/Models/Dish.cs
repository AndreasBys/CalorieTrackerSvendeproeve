using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Models
{
    public class Dish
    {

        [Key]
        public string? _id { get; set; }  

        public string name { get; set; }

        public string user { get; set; }

        public List<FoodResponse> foods { get; set; }
    }


    public class DishResponse
    {
        public Food retter { get; set; }
    }
    public class DishListResponse
    {
        public List<Dish> dishes { get; set; }
    }

}

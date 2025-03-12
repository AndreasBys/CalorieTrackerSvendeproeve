using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Models
{
    public class Retter
    {

        [Key]
        public string? _id { get; set; }  

        public string name { get; set; }

        public string user { get; set; }
    }


    public class RetterResponse
    {
        public Food retter { get; set; }
    }
    public class RetterListResponse
    {
        public List<Retter> dishes { get; set; }
    }

}

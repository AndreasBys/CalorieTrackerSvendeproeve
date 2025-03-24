using System.ComponentModel.DataAnnotations;

namespace MealMate.Models;

public class User
{
    [Key]
    public string? _id { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public DateTime birthdate { get; set; }
    public int weight{ get; set; }
    public int height { get; set; }
    public string gender { get; set; }
    public bool admin { get; set; }


}
public class UserResponse
{
    public User user { get; set; }
}
//ViewModels: good practice
//defines the contract between Views And Controllers
using System.ComponentModel.DataAnnotations;
namespace SimpleBlog.ViewModels
{
    public class AuthLogin
    {
        //send from Views to Controllers
        [Required]
        public string Username { get; set; }

        [DataType(DataType.Password), Required]
        public string Password { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApplication13.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Заполните Login")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Заполните Password")]
        [JsonIgnore]
        public string Password { get; set; }

        [DataType(DataType.DateTime)]
        public System.DateTime DateRegister { get; set; }
        
    }
}

using System.ComponentModel.DataAnnotations;

namespace Filmix.Models.AccountModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите логин или Email")]
        [Display(Name = "Логин или Email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [StringLength(maximumLength: 30, MinimumLength = 8, ErrorMessage = "Длина пароля должна быть от 8 до 30 символов")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

    }
}

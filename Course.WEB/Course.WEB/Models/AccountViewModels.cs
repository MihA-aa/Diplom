using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Course.WEB.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Емэйл")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Логин должен быть больше 3 и меньше 50 символов", MinimumLength = 3)]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Фамилия должна быть больше 3 и меньше 50 символов", MinimumLength = 3)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Имя должно быть больше 3 и меньше 50 символов", MinimumLength = 3)]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        
        [StringLength(50, ErrorMessage = "Отчество должно быть больше 3 и меньше 50 символов", MinimumLength = 3)]
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Код подтвержающий ваши права")]
        public string RoleCode { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

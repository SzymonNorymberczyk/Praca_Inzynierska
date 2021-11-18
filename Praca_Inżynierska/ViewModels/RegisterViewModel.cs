using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [DataType(DataType.Text)]
        [Remote("IsLoginInUse", "Account", ErrorMessage = "Taki login już istnieje")]
        public string Login { get; set; }

        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [DataType(DataType.Text)]
        [Remote("IsEmailInUse", "Account", ErrorMessage = "Taki email już istnieje")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail nie poprawny")]
        public string Email { get; set; }

        [Display(Name = "Hasło")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są takie same")]
        public string ConfrirmPassword { get; set; }
    }
}

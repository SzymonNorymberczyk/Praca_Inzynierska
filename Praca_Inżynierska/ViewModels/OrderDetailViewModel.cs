using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.ViewModels
{
    public class OrderDetailViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Imie")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string Name { get; set; }

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string Surname { get; set; }

        [Display(Name = "Firma")]
        public string Company { get; set; }

        [Display(Name = "Ulica")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string Street { get; set; }

        [Display(Name = "Numer Ulicy")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string Number { get; set; }

        [Display(Name = "Numer Domu")]
        public string HouseNumber { get; set; }

        [Display(Name = "Kod pocztowy")]
        [RegularExpression("[0-9]{2}-[0-9]{3}", ErrorMessage = "Błedny Kod Pocztowy Wzór ##-###")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string ZIPcode { get; set; }

        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string City { get; set; }

        [Display(Name = "Numer Telefonu")]
        [Phone]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string PhoneNumber { get; set; }

        [Display(Name = "E-mail")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail nie poprawny")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string Email { get; set; }
        public string Nip { get; set; }
    }
}

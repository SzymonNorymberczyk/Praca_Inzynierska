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
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string ZIPcode { get; set; }
        [Display(Name = "Miasto")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string City { get; set; }
        [Display(Name = "Numer Telefonu")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public string PhoneNumber { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Nip { get; set; }
    }
}

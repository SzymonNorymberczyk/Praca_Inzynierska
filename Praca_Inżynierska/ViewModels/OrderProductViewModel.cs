using Praca_Inżynierska.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.ViewModels
{
    public class OrderProductViewModel
    {
        [Key]
        public int Id { get; set; }
        
        [Display(Name = "Waga")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }
        
        [Display(Name = "Długość")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Length { get; set; }
        [Display(Name = "Wysokość")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Height { get; set; }
        [Display(Name = "Szerokość")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Width { get; set; }
        [Display(Name = "Ilość")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        public int Count { get; set; }
        [Display(Name = "Dodatkowe szczegóły")]
        public string Details { get; set; }

        
    }
}

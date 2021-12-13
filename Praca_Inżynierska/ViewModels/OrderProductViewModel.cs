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
        [Range(0, 999.99,ErrorMessage = "Wartość wynosi od 1 do 999")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }
        
        [Display(Name = "Długość")]
        [Range(0, 999.99, ErrorMessage = "Wartość wynosi od 1 do 999")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Length { get; set; }
        [Display(Name = "Wysokość")]
        [Range(0, 999.99, ErrorMessage = "Wartość wynosi od 1 do 999")]
        [Required(ErrorMessage = "To pole musi być wypełnione")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Height { get; set; }
        [Display(Name = "Szerokość")]
        [Range(0, 999.99, ErrorMessage = "Wartość wynosi od 1 do 999")]
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.ViewModels
{
    public class OrderStatusViewModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Status Zamówienia")]
        public string Name { get; set; }
    }
}

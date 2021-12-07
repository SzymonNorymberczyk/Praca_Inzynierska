using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Praca_Inżynierska.ViewModels
{
    public class OrderViewModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
       
        public virtual OrderStatusViewModel OrderStatus { get; set; }
        public virtual OrderDetailViewModel Sender { get; set; }
        public virtual OrderDetailViewModel Receiver { get; set; }
        public virtual OrderProductViewModel Product { get; set; }

        public int OrderStatusId { get; set; }

        public IEnumerable<SelectListItem> StatusTypesSelectListItem { get; set; }
    }
}

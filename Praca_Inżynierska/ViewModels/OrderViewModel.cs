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

        public virtual OrderDetailViewModel Sender { get; set; }
        public virtual OrderDetailViewModel Receiver { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        [Required]
        public string UserId { get; set; }
        
        public virtual ApplicationUser ApplicationUser { get; set; }

        public DateTime DateCreate { get; set; }
        
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.Models
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Weight { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Length { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Height { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Width { get; set; }
        public int Count { get; set; }
        public string Details { get; set; }

        public virtual Order Order { get; set; }

        public int OrderId { get; set; }

    }
}

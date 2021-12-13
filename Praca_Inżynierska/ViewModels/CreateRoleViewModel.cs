using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Nazwa Roli")]
        public string RoleName { get; set; }
    }
}

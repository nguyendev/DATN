using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Models.AccountViewModels
{
    public class RegisterSimpleViewModel
    {
        [Required]
        [Display(Name = "Số chứng minh nhân dân")]
        [DataType(DataType.PhoneNumber)]
        public string UserName { get; set; }

        
    }
}

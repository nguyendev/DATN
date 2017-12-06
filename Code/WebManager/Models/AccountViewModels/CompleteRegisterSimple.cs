using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Models.AccountViewModels
{
    public class CompleteRegisterSimple
    {
        [Display(Name = "Số CMND")]
        public string ID { get; set; }
        [Display(Name = "Mã")]
        public string Code { get; set; }
    }
}

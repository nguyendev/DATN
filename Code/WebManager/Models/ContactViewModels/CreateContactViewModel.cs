using DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Models.ContactViewModels
{
    public class CreateContactViewModel
    {
        [Required]
        public string Code { get; set; }
        [Display(Name ="Tên")]
        [Required]
        public string FirstMidName { get; set; }
        [Required]
        [Display(Name = "Họ")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Giới tính")]
        public Sex Sex { get; set; }
        [Display(Name = "Điện thoại bàn")]
        [DataType(DataType.PhoneNumber)]
        public string Landline { get; set; }
        [Display(Name = "Điện thoại di động")]
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }
        [Required]
        [Display(Name = "Ngày cấp CMND")]
        public DateTime? DateIdentityCard { get; set; }
        [Display(Name = "Nơi cấp CMND")]
        [Required]
        public string WhereIdentityCard { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        public string Zip { get; set; }
        [Display(Name = "Ngày sinh")]
        [Required]
        public DateTime? DateofBirth { get; set; }
    }
}

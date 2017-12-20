using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ImageID { get; set; }
        public string Pic1 { get; set; }
        public string Pic2 { get; set; }
        public string Pic3 { get; set; }
        public string Pic4 { get; set; }
        public string Pic5 { get; set; }
        public string Pic6 { get; set; }
        public string Pic7 { get; set; }
        public string Pic8 { get; set; }
        public string Pic9 { get; set; }
        public string Pic10 { get; set; }
        public string OwnerID { get; set; }
        public Member Owner { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebManager.Models
{
    public class HistoryLogin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long HistoryLoginID { get; set; }
       
        public DateTime DateTime { get; set; }
        public string StudentID { get; set; }
        public Member Student { get; set; }
        public MethodLogin MethodLogin { get; set; }
    }
    public enum MethodLogin
    {
        Password,
        FaceRecognized,
    }
}

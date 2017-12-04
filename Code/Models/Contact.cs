using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long ContactId { get; set; }

        // user ID from AspNetUser table
        public string OwnerID { get; set; }
        public Member Owner { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public long? ImageID { get; set; }
        public Image Image { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string State { get; set; }
        public string Zip { get; set; }
        public DateTime? DateofBirth { get; set; }
        public ContactStatus Status { get; set; }
    }

    public enum ContactStatus
    {
        Submitted,
        Approved,
        Rejected
    }

}

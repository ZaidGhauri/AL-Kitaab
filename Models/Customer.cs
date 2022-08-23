using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alkitaab.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string FirstName { get; set; }
        [StringLength(250)]
        public string LastName { get; set; }
        [StringLength(50)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [StringLength(50)]
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [StringLength(50)]
        public string PostalCode { get; set; }
        public bool IsSubscribe { get; set; }
        [StringLength(500)]
        public string ReferedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace alkitaab.Models
{
    public class PaymentTransaction
    {
        [Key]
        public int ID { get; set; }

        public int OrderID { get; set; }

        public string ReferenceNummber { get; set; }

        public string PaymentDate { get; set; }

        public string Status { get; set; }

        public float Amount { get; set; }
    }
}
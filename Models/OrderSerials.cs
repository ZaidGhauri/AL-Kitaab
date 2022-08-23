using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace alkitaab.Models
{
    public class OrderSerial
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SerialNumber { get; set; }
        public string ChildType { get; set; }
    }
}
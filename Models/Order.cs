using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alkitaab.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int CustomerId { get; set; }
        public float SubTotal { get; set; }
        public float Tax { get; set; }
        public float NetTotal { get; set; }
        public int SixteenOverQty { get; set; }
        public float SixteenOverFee { get; set; }
        public float SixteenOverTotal { get; set; }
        public int BetweenTexAndSixteenQty { get; set; }
        public float BetweenTexAndSixteenFee { get; set; }
        public float BetweenTexAndSixteenTotal { get; set; }
        public int UnderTenQty { get; set; }
        public float UnderTenFee { get; set; }
        public float UnderTenTotal { get; set; }
    }
}

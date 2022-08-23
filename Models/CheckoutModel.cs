using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace alkitaab.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            //Details = new List<OrderDetailModel>();
            Customer = new CustomerModel();
        }
        public int Id { get; set; }
        public float SubTotal
        {
            get { return float.Parse(strSubTotal.Replace('$', ' ')); }
            set { strSubTotal = string.Format("{0:c}", SubTotal); }
        }
        public string strSubTotal { get; set; }
        public float Tax { get; set; }
        public float NetTotal
        {
            get { return float.Parse(strNetTotal.Replace('$', ' ')); }
            set { strNetTotal = string.Format("{0:c}", NetTotal); }
        }
        public string strNetTotal { get; set; }
        public int AdultQty { get; set; }
        public string strAdultFee { get; set; }
        public float AdultFee
        {
            get { return float.Parse(strAdultFee.Replace('$', ' ')); }
            set { strAdultFee = string.Format("{0:c}", AdultFee); }
        }
        public float AdultTotal
        {
            get { return float.Parse(strAdultTotal.Replace('$', ' ')); }
            set { strAdultTotal = string.Format("{0:c}", AdultTotal); }
        }
        public string strAdultTotal { get; set; }
        public int SixteenOverQty { get; set; }
        public string strSixteenOverFee { get; set; }
        public float SixteenOverFee
        {
            get { return float.Parse(strSixteenOverFee.Replace('$', ' ')); }
            set { strSixteenOverFee = string.Format("{0:c}", SixteenOverFee); }
        }
        public float SixteenOverTotal
        {
            get { return float.Parse(strSixteenOverTotal.Replace('$', ' ')); }
            set { strSixteenOverTotal = string.Format("{0:c}", SixteenOverTotal); }
        }
        public string strBetweenTexAndSixteenFee { get; set; }
        public string strSixteenOverTotal { get; set; }
        public int BetweenTexAndSixteenQty { get; set; }
        public float BetweenTexAndSixteenFee
        {
            get { return float.Parse(strBetweenTexAndSixteenFee.Replace('$', ' ')); }
            set { strBetweenTexAndSixteenFee = string.Format("{0:c}", BetweenTexAndSixteenFee); }
        }
        public float BetweenTexAndSixteenTotal
        {
            get { return float.Parse(strBetweenTexAndSixteenTotal.Replace('$', ' ')); }
            set { strBetweenTexAndSixteenTotal = string.Format("{0:c}", BetweenTexAndSixteenTotal); }
        }
        public string strBetweenTexAndSixteenTotal { get; set; }
        public int UnderTenQty { get; set; }
        public string strUnderTenFee { get; set; }
        public float UnderTenFee
        {
            get { return float.Parse(strUnderTenFee.Replace('$', ' ')); }
            set { strUnderTenFee = string.Format("{0:c}", UnderTenFee); }
        }
        public float UnderTenTotal
        {
            get { return float.Parse(strUnderTenTotal.Replace('$', ' ')); }
            set { strUnderTenTotal = string.Format("{0:c}", UnderTenTotal); }
        }
        public string strUnderTenTotal { get; set; }
        public CustomerModel Customer { get; set; }
        //public List<OrderDetailModel> Details { get; set; }
    }
    public class OrderDetailModel
    {
        public int Id { get; set; }
        [StringLength(800)]
        public string Description { get; set; }
        public int Qty { get; set; }
        public float Fee { get; set; }
        public float Total { get; set; }
    }
    public class CustomerModel
    {
        public int Id { get; set; }
        [StringLength(250)]
        [Required]
        public string FirstName { get; set; }
        [StringLength(250)]
        [Required]
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
        [StringLength(100)]
        public string ReferedBy { get; set; }
    }
}

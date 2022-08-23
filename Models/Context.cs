using System.Data.Entity;

namespace alkitaab.Models
{
    public class Context : DbContext
    {
        public Context()
            : base("Name=DBCon")
        {
            
        }
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<OrderSerial> OrderSerial { get; set; }
        public DbSet<PaymentTransaction> PaymentTransaction { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}

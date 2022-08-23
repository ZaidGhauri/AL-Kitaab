namespace alkitaab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentTransactions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ReferenceNummber = c.String(),
                        PaymentDate = c.String(),
                        Status = c.String(),
                        Amount = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Orders", "OrderCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderCode");
            DropTable("dbo.PaymentTransactions");
        }
    }
}

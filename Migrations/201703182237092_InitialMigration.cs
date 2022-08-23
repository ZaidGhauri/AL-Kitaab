namespace alkitaab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 250),
                        LastName = c.String(maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 50),
                        Address = c.String(),
                        City = c.String(),
                        PostalCode = c.String(maxLength: 50),
                        IsSubscribe = c.Boolean(nullable: false),
                        ReferedBy = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        SubTotal = c.Single(nullable: false),
                        Tax = c.Single(nullable: false),
                        NetTotal = c.Single(nullable: false),
                        SixteenOverQty = c.Int(nullable: false),
                        SixteenOverFee = c.Single(nullable: false),
                        SixteenOverTotal = c.Single(nullable: false),
                        BetweenTexAndSixteenQty = c.Int(nullable: false),
                        BetweenTexAndSixteenFee = c.Single(nullable: false),
                        BetweenTexAndSixteenTotal = c.Single(nullable: false),
                        UnderTenQty = c.Int(nullable: false),
                        UnderTenFee = c.Single(nullable: false),
                        UnderTenTotal = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
        }
    }
}

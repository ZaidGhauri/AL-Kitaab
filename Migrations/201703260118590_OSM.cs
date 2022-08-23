namespace alkitaab.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OSM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderSerials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        SerialNumber = c.Int(nullable: false),
                        ChildType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OrderSerials");
        }
    }
}

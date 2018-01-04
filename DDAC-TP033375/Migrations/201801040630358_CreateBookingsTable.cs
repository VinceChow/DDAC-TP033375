namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBookingsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookedAt = c.DateTime(nullable: false),
                        BookedBy_Id = c.String(maxLength: 128),
                        Container_Id = c.Int(nullable: false),
                        Customer_Id = c.Int(nullable: false),
                        Ship_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.BookedBy_Id)
                .ForeignKey("dbo.Containers", t => t.Container_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Ships", t => t.Ship_Id, cascadeDelete: true)
                .Index(t => t.BookedBy_Id)
                .Index(t => t.Container_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Ship_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Ship_Id", "dbo.Ships");
            DropForeignKey("dbo.Bookings", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "Container_Id", "dbo.Containers");
            DropForeignKey("dbo.Bookings", "BookedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "Ship_Id" });
            DropIndex("dbo.Bookings", new[] { "Customer_Id" });
            DropIndex("dbo.Bookings", new[] { "Container_Id" });
            DropIndex("dbo.Bookings", new[] { "BookedBy_Id" });
            DropTable("dbo.Bookings");
        }
    }
}

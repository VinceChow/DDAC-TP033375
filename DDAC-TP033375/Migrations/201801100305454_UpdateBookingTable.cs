namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookingTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "Ship_Id", "dbo.Ships");
            DropIndex("dbo.Bookings", new[] { "Customer_Id" });
            DropIndex("dbo.Bookings", new[] { "Ship_Id" });
            AddColumn("dbo.Bookings", "Schedule_Id", c => c.Int());
            AlterColumn("dbo.Bookings", "Customer_Id", c => c.Int());
            AlterColumn("dbo.Bookings", "Ship_Id", c => c.Int());
            CreateIndex("dbo.Bookings", "Customer_Id");
            CreateIndex("dbo.Bookings", "Schedule_Id");
            CreateIndex("dbo.Bookings", "Ship_Id");
            AddForeignKey("dbo.Bookings", "Schedule_Id", "dbo.Schedules", "Id");
            AddForeignKey("dbo.Bookings", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Bookings", "Ship_Id", "dbo.Ships", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "Ship_Id", "dbo.Ships");
            DropForeignKey("dbo.Bookings", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Bookings", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Bookings", new[] { "Ship_Id" });
            DropIndex("dbo.Bookings", new[] { "Schedule_Id" });
            DropIndex("dbo.Bookings", new[] { "Customer_Id" });
            AlterColumn("dbo.Bookings", "Ship_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "Customer_Id", c => c.Int(nullable: false));
            DropColumn("dbo.Bookings", "Schedule_Id");
            CreateIndex("dbo.Bookings", "Ship_Id");
            CreateIndex("dbo.Bookings", "Customer_Id");
            AddForeignKey("dbo.Bookings", "Ship_Id", "dbo.Ships", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}

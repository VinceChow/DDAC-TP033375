namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeContainerToICollectionInBooking : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "Container_Id", "dbo.Containers");
            DropIndex("dbo.Bookings", new[] { "Container_Id" });
            AddColumn("dbo.Containers", "Booking_Id", c => c.Int());
            CreateIndex("dbo.Containers", "Booking_Id");
            AddForeignKey("dbo.Containers", "Booking_Id", "dbo.Bookings", "Id");
            DropColumn("dbo.Bookings", "Container_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "Container_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Containers", "Booking_Id", "dbo.Bookings");
            DropIndex("dbo.Containers", new[] { "Booking_Id" });
            DropColumn("dbo.Containers", "Booking_Id");
            CreateIndex("dbo.Bookings", "Container_Id");
            AddForeignKey("dbo.Bookings", "Container_Id", "dbo.Containers", "Id", cascadeDelete: true);
        }
    }
}

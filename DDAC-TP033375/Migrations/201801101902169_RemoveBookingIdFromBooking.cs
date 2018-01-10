namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveBookingIdFromBooking : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "BookingId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "BookingId", c => c.Int(nullable: false));
        }
    }
}

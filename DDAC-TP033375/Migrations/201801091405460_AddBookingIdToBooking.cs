namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingIdToBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "BookingId");
        }
    }
}

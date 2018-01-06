namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScheduleAvailabilityToShip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "ScheduleAvailability", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ships", "ScheduleAvailability");
        }
    }
}

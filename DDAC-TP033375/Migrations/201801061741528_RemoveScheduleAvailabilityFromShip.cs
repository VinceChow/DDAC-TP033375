namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveScheduleAvailabilityFromShip : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ships", "ScheduleAvailability");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ships", "ScheduleAvailability", c => c.Boolean(nullable: false));
        }
    }
}

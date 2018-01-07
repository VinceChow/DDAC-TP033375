namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsScheduledToShip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ships", "IsScheduled", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ships", "IsScheduled");
        }
    }
}

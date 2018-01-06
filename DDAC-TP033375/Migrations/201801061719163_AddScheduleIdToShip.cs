namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddScheduleIdToShip : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ships", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Ships", new[] { "Schedule_Id" });
            RenameColumn(table: "dbo.Ships", name: "Schedule_Id", newName: "ScheduleId");
            AlterColumn("dbo.Ships", "ScheduleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ships", "ScheduleId");
            AddForeignKey("dbo.Ships", "ScheduleId", "dbo.Schedules", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ships", "ScheduleId", "dbo.Schedules");
            DropIndex("dbo.Ships", new[] { "ScheduleId" });
            AlterColumn("dbo.Ships", "ScheduleId", c => c.Int());
            RenameColumn(table: "dbo.Ships", name: "ScheduleId", newName: "Schedule_Id");
            CreateIndex("dbo.Ships", "Schedule_Id");
            AddForeignKey("dbo.Ships", "Schedule_Id", "dbo.Schedules", "Id");
        }
    }
}

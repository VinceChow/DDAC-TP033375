namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateShipsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        NumberOfContainerBay = c.Int(nullable: false),
                        NumberOfAvailableContainerBay = c.Int(nullable: false),
                        Schedule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id)
                .Index(t => t.Schedule_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ships", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Ships", new[] { "Schedule_Id" });
            DropTable("dbo.Ships");
        }
    }
}

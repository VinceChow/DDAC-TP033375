namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContainersToShip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Containers", "Ship_Id", c => c.Int());
            CreateIndex("dbo.Containers", "Ship_Id");
            AddForeignKey("dbo.Containers", "Ship_Id", "dbo.Ships", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Containers", "Ship_Id", "dbo.Ships");
            DropIndex("dbo.Containers", new[] { "Ship_Id" });
            DropColumn("dbo.Containers", "Ship_Id");
        }
    }
}

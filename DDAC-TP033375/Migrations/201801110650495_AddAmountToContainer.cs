namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAmountToContainer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Containers", "Amount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Containers", "Amount");
        }
    }
}

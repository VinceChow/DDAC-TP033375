namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNameAndCompanyNameToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CompanyName");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}

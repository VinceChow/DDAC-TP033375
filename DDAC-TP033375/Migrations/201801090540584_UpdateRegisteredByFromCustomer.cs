namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRegisteredByFromCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "RegisteredBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "RegisteredBy_Id" });
            AlterColumn("dbo.Customers", "RegisteredBy_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "RegisteredBy_Id");
            AddForeignKey("dbo.Customers", "RegisteredBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "RegisteredBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "RegisteredBy_Id" });
            AlterColumn("dbo.Customers", "RegisteredBy_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Customers", "RegisteredBy_Id");
            AddForeignKey("dbo.Customers", "RegisteredBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}

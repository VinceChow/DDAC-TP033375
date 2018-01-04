namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCustomersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        IdentificationNumber = c.String(nullable: false, maxLength: 20),
                        Email = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false),
                        RegisteredBy_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RegisteredBy_Id, cascadeDelete: true)
                .Index(t => t.RegisteredBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "RegisteredBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "RegisteredBy_Id" });
            DropTable("dbo.Customers");
        }
    }
}

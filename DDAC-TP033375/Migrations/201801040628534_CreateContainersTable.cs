namespace DDAC_TP033375.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateContainersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Containers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemType = c.String(nullable: false),
                        WeightInTonne = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Containers");
        }
    }
}

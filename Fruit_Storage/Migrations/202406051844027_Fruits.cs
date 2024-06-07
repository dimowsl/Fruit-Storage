namespace Fruit_Storage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fruits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fruit",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Description = c.String(),
                    Price = c.Double(nullable: false),
                    FruitTypeId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FruitType", t => t.FruitTypeId, cascadeDelete: true)
                .Index(t => t.FruitTypeId);

            CreateTable(
                "dbo.FruitType",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fruit", "FruitTypeId", "dbo.Fruit");
            DropIndex("dbo.Fruit", new[] { "FruitTypeId" });
            DropTable("dbo.FruitType");
            DropTable("dbo.Fruit");
        }
    }
}

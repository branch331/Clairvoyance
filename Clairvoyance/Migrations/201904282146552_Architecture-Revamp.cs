namespace Clairvoyance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArchitectureRevamp : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.WeekModels", newName: "Weeks");
            DropForeignKey("dbo.TaskItemModels", "CategoryId", "dbo.CategoryModels");
            DropForeignKey("dbo.TaskItemModels", "DayId", "dbo.DayModels");
            DropForeignKey("dbo.TaskItemModels", "WeekId", "dbo.WeekModels");
            DropIndex("dbo.TaskItemModels", new[] { "CategoryId" });
            DropIndex("dbo.TaskItemModels", new[] { "DayId" });
            DropIndex("dbo.TaskItemModels", new[] { "WeekId" });
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        TaskCategory = c.String(),
                        CategoryId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        WeekId = c.Int(nullable: false),
                        TaskStartDateTime = c.DateTime(nullable: false),
                        TaskEndDateTime = c.DateTime(nullable: false),
                        TaskTimeInterval = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.Weeks", t => t.WeekId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.DayId)
                .Index(t => t.WeekId);
            
            DropTable("dbo.TaskItemModels");
            DropTable("dbo.CategoryModels");
            DropTable("dbo.DayModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DayModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskItemModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TaskName = c.String(),
                        TaskCategory = c.String(),
                        CategoryId = c.Int(nullable: false),
                        DayId = c.Int(nullable: false),
                        WeekId = c.Int(nullable: false),
                        TaskStartDateTime = c.DateTime(nullable: false),
                        TaskEndDateTime = c.DateTime(nullable: false),
                        TaskTimeInterval = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TaskItems", "WeekId", "dbo.Weeks");
            DropForeignKey("dbo.TaskItems", "DayId", "dbo.Days");
            DropForeignKey("dbo.TaskItems", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TaskItems", new[] { "WeekId" });
            DropIndex("dbo.TaskItems", new[] { "DayId" });
            DropIndex("dbo.TaskItems", new[] { "CategoryId" });
            DropTable("dbo.TaskItems");
            DropTable("dbo.Days");
            DropTable("dbo.Categories");
            CreateIndex("dbo.TaskItemModels", "WeekId");
            CreateIndex("dbo.TaskItemModels", "DayId");
            CreateIndex("dbo.TaskItemModels", "CategoryId");
            AddForeignKey("dbo.TaskItemModels", "WeekId", "dbo.WeekModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TaskItemModels", "DayId", "dbo.DayModels", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TaskItemModels", "CategoryId", "dbo.CategoryModels", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.Weeks", newName: "WeekModels");
        }
    }
}

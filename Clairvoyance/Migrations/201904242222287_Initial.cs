namespace Clairvoyance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryModels", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.DayModels", t => t.DayId, cascadeDelete: true)
                .ForeignKey("dbo.WeekModels", t => t.WeekId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.DayId)
                .Index(t => t.WeekId);
            
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DayModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WeekModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MondayDate = c.DateTime(nullable: false),
                        SundayDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItemModels", "WeekId", "dbo.WeekModels");
            DropForeignKey("dbo.TaskItemModels", "DayId", "dbo.DayModels");
            DropForeignKey("dbo.TaskItemModels", "CategoryId", "dbo.CategoryModels");
            DropIndex("dbo.TaskItemModels", new[] { "WeekId" });
            DropIndex("dbo.TaskItemModels", new[] { "DayId" });
            DropIndex("dbo.TaskItemModels", new[] { "CategoryId" });
            DropTable("dbo.WeekModels");
            DropTable("dbo.DayModels");
            DropTable("dbo.CategoryModels");
            DropTable("dbo.TaskItemModels");
        }
    }
}

namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOutlinesToDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Outlines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Courses", "Outline_Id", c => c.Int());
            CreateIndex("dbo.Courses", "Outline_Id");
            AddForeignKey("dbo.Courses", "Outline_Id", "dbo.Outlines", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Outline_Id", "dbo.Outlines");
            DropIndex("dbo.Courses", new[] { "Outline_Id" });
            DropColumn("dbo.Courses", "Outline_Id");
            DropTable("dbo.Outlines");
        }
    }
}

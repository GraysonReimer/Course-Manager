namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGradeBackToOutline : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Outlines", "Grade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Outlines", "Grade");
        }
    }
}

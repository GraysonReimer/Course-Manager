namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCanBeAnElectiveToCourseModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CanBeAnElective", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CanBeAnElective");
        }
    }
}

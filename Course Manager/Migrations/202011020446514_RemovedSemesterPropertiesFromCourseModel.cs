namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSemesterPropertiesFromCourseModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Courses", "FirstSemester");
            DropColumn("dbo.Courses", "SecondSemester");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "SecondSemester", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "FirstSemester", c => c.Boolean(nullable: false));
        }
    }
}

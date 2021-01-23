namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedOccupancyInClassToStudentsEnrolled : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Courses", "Student_Id1", "dbo.Students");
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            DropIndex("dbo.Courses", new[] { "Student_Id1" });
            AddColumn("dbo.Classes", "StudentsEnrolled", c => c.Int(nullable: false));
            DropColumn("dbo.Classes", "Occupancy");
            DropColumn("dbo.Courses", "Student_Id");
            DropColumn("dbo.Courses", "Student_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Student_Id1", c => c.Int());
            AddColumn("dbo.Courses", "Student_Id", c => c.Int());
            AddColumn("dbo.Classes", "Occupancy", c => c.Int(nullable: false));
            DropColumn("dbo.Classes", "StudentsEnrolled");
            CreateIndex("dbo.Courses", "Student_Id1");
            CreateIndex("dbo.Courses", "Student_Id");
            AddForeignKey("dbo.Courses", "Student_Id1", "dbo.Students", "Id");
            AddForeignKey("dbo.Courses", "Student_Id", "dbo.Students", "Id");
        }
    }
}

namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCoursesToStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Student_Id", c => c.Int());
            AddColumn("dbo.Courses", "Student_Id1", c => c.Int());
            CreateIndex("dbo.Courses", "Student_Id");
            CreateIndex("dbo.Courses", "Student_Id1");
            AddForeignKey("dbo.Courses", "Student_Id", "dbo.Students", "Id");
            AddForeignKey("dbo.Courses", "Student_Id1", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Student_Id1", "dbo.Students");
            DropForeignKey("dbo.Courses", "Student_Id", "dbo.Students");
            DropIndex("dbo.Courses", new[] { "Student_Id1" });
            DropIndex("dbo.Courses", new[] { "Student_Id" });
            DropColumn("dbo.Courses", "Student_Id1");
            DropColumn("dbo.Courses", "Student_Id");
        }
    }
}

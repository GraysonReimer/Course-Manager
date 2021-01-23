namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCourseRequestsToStudentModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstChoice_Id = c.Int(),
                        SecondChoice_Id = c.Int(),
                        Student_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.FirstChoice_Id)
                .ForeignKey("dbo.Courses", t => t.SecondChoice_Id)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.FirstChoice_Id)
                .Index(t => t.SecondChoice_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseRequests", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.CourseRequests", "SecondChoice_Id", "dbo.Courses");
            DropForeignKey("dbo.CourseRequests", "FirstChoice_Id", "dbo.Courses");
            DropIndex("dbo.CourseRequests", new[] { "Student_Id" });
            DropIndex("dbo.CourseRequests", new[] { "SecondChoice_Id" });
            DropIndex("dbo.CourseRequests", new[] { "FirstChoice_Id" });
            DropTable("dbo.CourseRequests");
        }
    }
}

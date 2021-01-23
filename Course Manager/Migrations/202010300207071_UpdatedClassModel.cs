namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedClassModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Classes", "course_Id", "dbo.Courses");
            DropForeignKey("dbo.Classes", "Instructor_Id", "dbo.StaffMembers");
            DropIndex("dbo.Classes", new[] { "course_Id" });
            DropIndex("dbo.Classes", new[] { "Instructor_Id" });
            RenameColumn(table: "dbo.Classes", name: "course_Id", newName: "CourseId");
            RenameColumn(table: "dbo.Classes", name: "Instructor_Id", newName: "InstructorId");
            AlterColumn("dbo.Classes", "CourseId", c => c.Int(nullable: false));
            AlterColumn("dbo.Classes", "InstructorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Classes", "CourseId");
            CreateIndex("dbo.Classes", "InstructorId");
            AddForeignKey("dbo.Classes", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Classes", "InstructorId", "dbo.StaffMembers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "InstructorId", "dbo.StaffMembers");
            DropForeignKey("dbo.Classes", "CourseId", "dbo.Courses");
            DropIndex("dbo.Classes", new[] { "InstructorId" });
            DropIndex("dbo.Classes", new[] { "CourseId" });
            AlterColumn("dbo.Classes", "InstructorId", c => c.Int());
            AlterColumn("dbo.Classes", "CourseId", c => c.Int());
            RenameColumn(table: "dbo.Classes", name: "InstructorId", newName: "Instructor_Id");
            RenameColumn(table: "dbo.Classes", name: "CourseId", newName: "course_Id");
            CreateIndex("dbo.Classes", "Instructor_Id");
            CreateIndex("dbo.Classes", "course_Id");
            AddForeignKey("dbo.Classes", "Instructor_Id", "dbo.StaffMembers", "Id");
            AddForeignKey("dbo.Classes", "course_Id", "dbo.Courses", "Id");
        }
    }
}

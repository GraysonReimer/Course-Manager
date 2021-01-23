namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedStudentCoursesToList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Student_Id", c => c.Int());
            CreateIndex("dbo.Classes", "Student_Id");
            AddForeignKey("dbo.Classes", "Student_Id", "dbo.Students", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "Student_Id", "dbo.Students");
            DropIndex("dbo.Classes", new[] { "Student_Id" });
            DropColumn("dbo.Classes", "Student_Id");
        }
    }
}

namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedClassesToDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomNumber = c.Int(nullable: false),
                        course_Id = c.Int(),
                        Instructor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.course_Id)
                .ForeignKey("dbo.StaffMembers", t => t.Instructor_Id)
                .Index(t => t.course_Id)
                .Index(t => t.Instructor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Classes", "Instructor_Id", "dbo.StaffMembers");
            DropForeignKey("dbo.Classes", "course_Id", "dbo.Courses");
            DropIndex("dbo.Classes", new[] { "Instructor_Id" });
            DropIndex("dbo.Classes", new[] { "course_Id" });
            DropTable("dbo.Classes");
        }
    }
}

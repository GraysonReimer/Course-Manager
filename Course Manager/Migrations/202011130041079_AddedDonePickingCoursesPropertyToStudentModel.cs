namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDonePickingCoursesPropertyToStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "DonePickingClasses", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "DonePickingClasses");
        }
    }
}

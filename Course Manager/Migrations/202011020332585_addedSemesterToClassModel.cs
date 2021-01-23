namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedSemesterToClassModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Semester", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "Semester");
        }
    }
}

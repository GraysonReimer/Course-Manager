namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedUserIdInStudentModelToTypeString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "UserId", c => c.Int(nullable: false));
        }
    }
}

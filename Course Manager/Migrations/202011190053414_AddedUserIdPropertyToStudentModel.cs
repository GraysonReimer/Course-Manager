namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserIdPropertyToStudentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "UserId");
        }
    }
}

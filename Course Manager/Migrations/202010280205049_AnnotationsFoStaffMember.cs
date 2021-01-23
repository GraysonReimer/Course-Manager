namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnotationsFoStaffMember : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StaffMembers", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.StaffMembers", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StaffMembers", "LastName", c => c.String());
            AlterColumn("dbo.StaffMembers", "FirstName", c => c.String());
        }
    }
}

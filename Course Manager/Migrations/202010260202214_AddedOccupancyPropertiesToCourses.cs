namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOccupancyPropertiesToCourses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "OccupancyLimit", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "Occupancy", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Occupancy");
            DropColumn("dbo.Courses", "OccupancyLimit");
        }
    }
}

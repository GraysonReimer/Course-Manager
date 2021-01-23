namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedOccupancyFromCourseToClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "OccupancyLimit", c => c.Int(nullable: false));
            AddColumn("dbo.Classes", "Occupancy", c => c.Int(nullable: false));
            DropColumn("dbo.Courses", "OccupancyLimit");
            DropColumn("dbo.Courses", "Occupancy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Occupancy", c => c.Int(nullable: false));
            AddColumn("dbo.Courses", "OccupancyLimit", c => c.Int(nullable: false));
            DropColumn("dbo.Classes", "Occupancy");
            DropColumn("dbo.Classes", "OccupancyLimit");
        }
    }
}

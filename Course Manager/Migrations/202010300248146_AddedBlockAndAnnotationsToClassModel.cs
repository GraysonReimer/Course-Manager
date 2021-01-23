namespace Course_Manager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBlockAndAnnotationsToClassModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Block", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "Block");
        }
    }
}

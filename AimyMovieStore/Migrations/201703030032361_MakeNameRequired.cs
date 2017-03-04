namespace AimyMovieStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeNameRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movie", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movie", "Name", c => c.String());
        }
    }
}

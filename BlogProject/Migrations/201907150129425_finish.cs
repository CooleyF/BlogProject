namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finish : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "BlogBody", c => c.String());
            DropColumn("dbo.BlogPosts", "Body");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BlogPosts", "Body", c => c.String());
            DropColumn("dbo.BlogPosts", "BlogBody");
        }
    }
}

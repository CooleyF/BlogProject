namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new231 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "Body", c => c.String());
            AddColumn("dbo.Comments", "Body", c => c.String());
            DropColumn("dbo.BlogPosts", "Content");
            DropColumn("dbo.Comments", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Content", c => c.String());
            AddColumn("dbo.BlogPosts", "Content", c => c.String());
            DropColumn("dbo.Comments", "Body");
            DropColumn("dbo.BlogPosts", "Body");
        }
    }
}

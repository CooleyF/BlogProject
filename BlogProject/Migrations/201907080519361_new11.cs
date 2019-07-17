namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "Content", c => c.String());
            AddColumn("dbo.Comments", "Content", c => c.String());
            DropColumn("dbo.BlogPosts", "Body");
            DropColumn("dbo.Comments", "Body");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Body", c => c.String());
            AddColumn("dbo.BlogPosts", "Body", c => c.String());
            DropColumn("dbo.Comments", "Content");
            DropColumn("dbo.BlogPosts", "Content");
        }
    }
}

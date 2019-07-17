namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class form : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "AuthorID", c => c.String(maxLength: 128));
            CreateIndex("dbo.BlogPosts", "AuthorID");
            AddForeignKey("dbo.BlogPosts", "AuthorID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPosts", "AuthorID", "dbo.AspNetUsers");
            DropIndex("dbo.BlogPosts", new[] { "AuthorID" });
            DropColumn("dbo.BlogPosts", "AuthorID");
        }
    }
}

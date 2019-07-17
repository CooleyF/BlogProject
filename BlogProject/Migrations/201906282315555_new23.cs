namespace BlogProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "BlogPost_ID", "dbo.BlogPosts");
            DropIndex("dbo.Comments", new[] { "BlogPost_ID" });
            RenameColumn(table: "dbo.Comments", name: "BlogPost_ID", newName: "BlogPostID");
            AlterColumn("dbo.Comments", "BlogPostID", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "BlogPostID");
            AddForeignKey("dbo.Comments", "BlogPostID", "dbo.BlogPosts", "ID", cascadeDelete: true);
            DropColumn("dbo.Comments", "PostID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "PostID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "BlogPostID", "dbo.BlogPosts");
            DropIndex("dbo.Comments", new[] { "BlogPostID" });
            AlterColumn("dbo.Comments", "BlogPostID", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "BlogPostID", newName: "BlogPost_ID");
            CreateIndex("dbo.Comments", "BlogPost_ID");
            AddForeignKey("dbo.Comments", "BlogPost_ID", "dbo.BlogPosts", "ID");
        }
    }
}

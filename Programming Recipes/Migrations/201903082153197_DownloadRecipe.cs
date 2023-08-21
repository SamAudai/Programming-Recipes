namespace Programming_Recipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DownloadRecipe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DownloadRecipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        DownloadDate = c.DateTime(nullable: false),
                        RecipeId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.RecipeId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DownloadRecipes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DownloadRecipes", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.DownloadRecipes", new[] { "UserId" });
            DropIndex("dbo.DownloadRecipes", new[] { "RecipeId" });
            DropTable("dbo.DownloadRecipes");
        }
    }
}

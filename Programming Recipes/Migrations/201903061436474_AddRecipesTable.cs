namespace Programming_Recipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecipesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeTitle = c.String(),
                        RecipeContent = c.String(),
                        RecipeFile = c.String(),
                        LanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "LanguageId", "dbo.Languages");
            DropIndex("dbo.Recipes", new[] { "LanguageId" });
            DropTable("dbo.Recipes");
        }
    }
}

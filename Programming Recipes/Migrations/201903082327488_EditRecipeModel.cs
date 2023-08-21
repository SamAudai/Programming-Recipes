namespace Programming_Recipes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditRecipeModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Recipes", "UserId");
            AddForeignKey("dbo.Recipes", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Recipes", new[] { "UserId" });
            DropColumn("dbo.Recipes", "UserId");
        }
    }
}

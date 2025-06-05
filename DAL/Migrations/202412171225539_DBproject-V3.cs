namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBprojectV3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.invoices", "Users_id", c => c.Int());
            CreateIndex("dbo.invoices", "User_id");
            CreateIndex("dbo.invoices", "Users_id");
            AddForeignKey("dbo.invoices", "Users_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.invoices", "Users_id", "dbo.Users");
            DropIndex("dbo.invoices", new[] { "Users_id" });
            DropIndex("dbo.invoices", new[] { "User_id" });
            DropColumn("dbo.invoices", "Users_id");
        }
    }
}

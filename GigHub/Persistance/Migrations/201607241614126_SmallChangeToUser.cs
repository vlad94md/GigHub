namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallChangeToUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserNotifications", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.UserNotifications", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserNotifications", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserNotifications", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.UserNotifications", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserNotifications", "ApplicationUser_Id");
        }
    }
}

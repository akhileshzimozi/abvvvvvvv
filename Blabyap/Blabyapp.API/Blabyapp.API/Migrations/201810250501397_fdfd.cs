namespace Blabyapp.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdfd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "DisplayName", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime());
            AlterColumn("dbo.AspNetUsers", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "ImageUrl", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
            AlterColumn("dbo.AspNetUsers", "DisplayName", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "FullName", c => c.String(nullable: false));
        }
    }
}

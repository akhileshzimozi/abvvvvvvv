namespace LocationAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201812131 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guides", "GuideAddress", c => c.String());
        }
        


        public override void Down()
        {
            AlterColumn("dbo.Guides", "GuideAddress", c => c.Int(nullable: false));
        }
    }
}

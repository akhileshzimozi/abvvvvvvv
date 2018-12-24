namespace LocationAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20181213 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guides",
                c => new
                    {
                        GuideID = c.Int(nullable: false, identity: true),
                        GuideName = c.String(),
                        GuideAddress = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GuideID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        LocationName = c.String(),
                        LocationCode = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LocationID);
            
            DropTable("dbo.VmTasks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VmTasks",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        BuyerName = c.String(),
                        Achivement = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        Address = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
            DropTable("dbo.Locations");
            DropTable("dbo.Guides");
        }
    }
}

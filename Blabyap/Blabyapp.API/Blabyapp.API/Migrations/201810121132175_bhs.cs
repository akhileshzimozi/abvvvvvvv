namespace Blabyapp.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bhs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.chats",
                c => new
                    {
                        chatID = c.Int(nullable: false, identity: true),
                        RecruiterEmail = c.String(),
                        CandidateEmail = c.String(),
                        ChatMsg = c.String(),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.chatID);
        }
        
        public override void Down()
        {
            DropTable("dbo.chats");
        }
    }
}

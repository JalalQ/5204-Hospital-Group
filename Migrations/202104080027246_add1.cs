namespace News_Events_Payments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.News_events",
                c => new
                    {
                        News_events_id = c.Int(nullable: false, identity: true),
                        News_events_title = c.String(),
                        News_events_content = c.String(),
                        Date_published = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.News_events_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.News_events");
        }
    }
}

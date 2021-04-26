namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userappointment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.appointments", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.appointments", "Id");
            AddForeignKey("dbo.appointments", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.appointments", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.appointments", new[] { "Id" });
            DropColumn("dbo.appointments", "Id");
        }
    }
}

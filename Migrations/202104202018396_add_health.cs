namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_health : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Healthnumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Healthnumber");
        }
    }
}

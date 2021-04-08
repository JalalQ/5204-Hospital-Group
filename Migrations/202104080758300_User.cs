namespace hospitalPrj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "lName", c => c.String());
            AddColumn("dbo.AspNetUsers", "healthNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "healthNumber");
            DropColumn("dbo.AspNetUsers", "lName");
            DropColumn("dbo.AspNetUsers", "Name");
        }
    }
}

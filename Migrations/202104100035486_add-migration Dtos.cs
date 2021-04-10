namespace GeraldtonHospital_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmigrationDtos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Donors", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donors", "Country");
        }
    }
}

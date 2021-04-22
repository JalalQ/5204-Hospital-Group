namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeappoint_fields : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.appointments", "drName");
            DropColumn("dbo.appointments", "department");
        }
        
        public override void Down()
        {
            AddColumn("dbo.appointments", "department", c => c.String());
            AddColumn("dbo.appointments", "drName", c => c.String());
        }
    }
}

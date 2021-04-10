namespace hospitalPrj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAp_col : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.appointments", "drName", c => c.String());
            AddColumn("dbo.appointments", "department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.appointments", "department");
            DropColumn("dbo.appointments", "drName");
        }
    }
}

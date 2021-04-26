namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class departadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.appointments", "departName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.appointments", "departName");
        }
    }
}

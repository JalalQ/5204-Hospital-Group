namespace Simran_hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class time_snap : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vistings", "StartTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Vistings", "EndTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vistings", "EndTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Vistings", "StartTime", c => c.DateTime(nullable: false));
        }
    }
}

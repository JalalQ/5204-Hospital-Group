namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datatypechanged : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobApplications", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobApplications", "Email", c => c.Int(nullable: false));
        }
    }
}

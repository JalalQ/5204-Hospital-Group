namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailvalidates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.JobApplications", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.JobApplications", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.JobApplications", "Dob", c => c.String(nullable: false));
            AlterColumn("dbo.JobApplications", "Contact", c => c.String(nullable: false));
            AlterColumn("dbo.JobApplications", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.JobApplications", "Qualification", c => c.String(nullable: false));
            AlterColumn("dbo.JobApplications", "Experience", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.JobApplications", "Experience", c => c.String());
            AlterColumn("dbo.JobApplications", "Qualification", c => c.String());
            AlterColumn("dbo.JobApplications", "Address", c => c.String());
            AlterColumn("dbo.JobApplications", "Contact", c => c.String());
            AlterColumn("dbo.JobApplications", "Dob", c => c.String());
            AlterColumn("dbo.JobApplications", "LastName", c => c.String());
            AlterColumn("dbo.JobApplications", "FirstName", c => c.String());
        }
    }
}

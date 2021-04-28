namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobapplication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.JobApplications",
                c => new
                    {
                        JobApplicationId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Dob = c.String(),
                        Email = c.Int(nullable: false),
                        Contact = c.String(),
                        Address = c.String(),
                        Qualification = c.String(),
                        Experience = c.String(),
                    })
                .PrimaryKey(t => t.JobApplicationId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.JobApplications");
        }
    }
}

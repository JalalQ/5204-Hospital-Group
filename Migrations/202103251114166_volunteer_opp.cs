namespace Simran_hospital.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class volunteer_opp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Opportunities",
                c => new
                    {
                        OpportunityID = c.Int(nullable: false, identity: true),
                        OpportunityName = c.String(),
                    })
                .PrimaryKey(t => t.OpportunityID);
            
            CreateTable(
                "dbo.Volunteers",
                c => new
                    {
                        VolunteerID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Email = c.String(),
                        DOB = c.DateTime(nullable: false),
                        ContactNumber = c.Int(nullable: false),
                        Language = c.String(),
                        CurrentEmployer = c.String(),
                        Position = c.String(),
                        UniversitySchool = c.String(),
                        Grades = c.Double(nullable: false),
                        WorkExperience = c.String(),
                        WhyInterested = c.String(),
                        PastVolunteer = c.String(),
                        OpportunityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VolunteerID)
                .ForeignKey("dbo.Opportunities", t => t.OpportunityID, cascadeDelete: true)
                .Index(t => t.OpportunityID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Volunteers", "OpportunityID", "dbo.Opportunities");
            DropIndex("dbo.Volunteers", new[] { "OpportunityID" });
            DropTable("dbo.Volunteers");
            DropTable("dbo.Opportunities");
        }
    }
}

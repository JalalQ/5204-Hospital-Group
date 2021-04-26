namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forienkey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.appointments", "doctorID", c => c.Int(nullable: false));
            AddColumn("dbo.appointments", "patientId", c => c.Int(nullable: false));
            CreateIndex("dbo.appointments", "doctorID");
            CreateIndex("dbo.appointments", "patientId");
            AddForeignKey("dbo.appointments", "doctorID", "dbo.doctors", "doctorID", cascadeDelete: true);
            AddForeignKey("dbo.appointments", "patientId", "dbo.patients", "patientId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.appointments", "patientId", "dbo.patients");
            DropForeignKey("dbo.appointments", "doctorID", "dbo.doctors");
            DropIndex("dbo.appointments", new[] { "patientId" });
            DropIndex("dbo.appointments", new[] { "doctorID" });
            DropColumn("dbo.appointments", "patientId");
            DropColumn("dbo.appointments", "doctorID");
        }
    }
}

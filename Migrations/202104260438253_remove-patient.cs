namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removepatient : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.appointments", "patientId", "dbo.patients");
            DropIndex("dbo.appointments", new[] { "patientId" });
            DropColumn("dbo.appointments", "patientId");
            DropTable("dbo.patients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.patients",
                c => new
                    {
                        patientId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        lastName = c.String(),
                        healthNumber = c.String(),
                    })
                .PrimaryKey(t => t.patientId);
            
            AddColumn("dbo.appointments", "patientId", c => c.Int(nullable: false));
            CreateIndex("dbo.appointments", "patientId");
            AddForeignKey("dbo.appointments", "patientId", "dbo.patients", "patientId", cascadeDelete: true);
        }
    }
}

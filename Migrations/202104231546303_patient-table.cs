namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class patienttable : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.patients");
        }
    }
}

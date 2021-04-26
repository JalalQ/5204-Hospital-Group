namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.doctors",
                c => new
                    {
                        doctorID = c.Int(nullable: false, identity: true),
                        fullName = c.String(),
                        cpsoReg = c.Int(nullable: false),
                        email = c.String(),
                        education = c.String(),
                        expertise = c.String(),
                        biography = c.String(),
                    })
                .PrimaryKey(t => t.doctorID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.doctors");
        }
    }
}

namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvisiting : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.visitings",
                c => new
                {
                    DepartmentID = c.Int(nullable: false, identity: true),
                    DepartmentName = c.String(),
                    StartTime = c.Time(nullable: false, precision: 7),
                    EndTime = c.Time(nullable: false, precision: 7),
                    Description = c.String()
                });
               
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.appointments", "DepartmentID", "dbo.visitings");
            DropIndex("dbo.appointments", new[] { "DepartmentID" });
            DropColumn("dbo.appointments", "DepartmentID");
            DropTable("dbo.visitings");
        }
    }
}

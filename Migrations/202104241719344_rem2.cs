namespace team2Geraldton.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rem2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.visitings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.visitings",
                c => new
                    {
                        DepartmentID = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentID);
            
        }
    }
}

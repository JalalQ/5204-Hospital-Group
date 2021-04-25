namespace GeraldtonHospV7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changestomodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Donations", "Donor_DonorID", "dbo.Donors");
            DropIndex("dbo.Donations", new[] { "Donor_DonorID" });
            RenameColumn(table: "dbo.Donations", name: "Donor_DonorID", newName: "DonorID");
            AlterColumn("dbo.Donations", "DonorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Donations", "DonorID");
            AddForeignKey("dbo.Donations", "DonorID", "dbo.Donors", "DonorID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donations", "DonorID", "dbo.Donors");
            DropIndex("dbo.Donations", new[] { "DonorID" });
            AlterColumn("dbo.Donations", "DonorID", c => c.Int());
            RenameColumn(table: "dbo.Donations", name: "DonorID", newName: "Donor_DonorID");
            CreateIndex("dbo.Donations", "Donor_DonorID");
            AddForeignKey("dbo.Donations", "Donor_DonorID", "dbo.Donors", "DonorID");
        }
    }
}

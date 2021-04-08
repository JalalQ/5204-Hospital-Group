namespace News_Events_Payments.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Payment_id = c.Int(nullable: false, identity: true),
                        Patient_firstname = c.String(),
                        Patient_lastname = c.String(),
                        Patient_email = c.String(),
                        Payment_date = c.DateTime(nullable: false),
                        Payment_amount = c.Int(nullable: false),
                        Bill_number = c.Int(nullable: false),
                        Card_number = c.Int(nullable: false),
                        Card_csv = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Payment_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payments");
        }
    }
}

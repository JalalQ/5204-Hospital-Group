namespace hospitalPrj.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_user : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.appointments", "userId", "dbo.users");
            DropIndex("dbo.appointments", new[] { "userId" });
            CreateTable(
                "dbo.clients",
                c => new
                    {
                        clientId = c.Int(nullable: false, identity: true),
                        clientName = c.String(),
                        clientLname = c.String(),
                        healthCN = c.String(),
                    })
                .PrimaryKey(t => t.clientId);
            
            AddColumn("dbo.appointments", "clientId", c => c.Int(nullable: false));
            CreateIndex("dbo.appointments", "clientId");
            AddForeignKey("dbo.appointments", "clientId", "dbo.clients", "clientId", cascadeDelete: true);
            DropColumn("dbo.appointments", "userId");
            DropTable("dbo.users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.users",
                c => new
                    {
                        userId = c.Int(nullable: false, identity: true),
                        userName = c.String(),
                        userLname = c.String(),
                        healthCN = c.String(),
                    })
                .PrimaryKey(t => t.userId);
            
            AddColumn("dbo.appointments", "userId", c => c.Int(nullable: false));
            DropForeignKey("dbo.appointments", "clientId", "dbo.clients");
            DropIndex("dbo.appointments", new[] { "clientId" });
            DropColumn("dbo.appointments", "clientId");
            DropTable("dbo.clients");
            CreateIndex("dbo.appointments", "userId");
            AddForeignKey("dbo.appointments", "userId", "dbo.users", "userId", cascadeDelete: true);
        }
    }
}

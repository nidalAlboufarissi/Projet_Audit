namespace Projet_Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserRisques", "NombreOui", c => c.Int(nullable: false));
            AddColumn("dbo.UserRisques", "NombreNon", c => c.Int(nullable: false));
            AddColumn("dbo.UserRisques", "NombreNull", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserRisques", "NombreNull");
            DropColumn("dbo.UserRisques", "NombreNon");
            DropColumn("dbo.UserRisques", "NombreOui");
        }
    }
}

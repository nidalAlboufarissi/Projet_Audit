namespace Projet_Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserRisques", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserRisques", new[] { "ApplicationUser_Id" });
            AddColumn("dbo.UserRisques", "NombreQst", c => c.Int(nullable: false));
            AddColumn("dbo.UserRisques", "QstTraite", c => c.Int(nullable: false));
            AddColumn("dbo.UserRisques", "Enregistrement_id_Record", c => c.Int());
            CreateIndex("dbo.UserRisques", "Enregistrement_id_Record");
            AddForeignKey("dbo.UserRisques", "Enregistrement_id_Record", "dbo.UserEnregistrements", "id_Record");
            DropColumn("dbo.UserRisques", "UserName");
            DropColumn("dbo.UserRisques", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRisques", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserRisques", "UserName", c => c.String(nullable: false));
            DropForeignKey("dbo.UserRisques", "Enregistrement_id_Record", "dbo.UserEnregistrements");
            DropIndex("dbo.UserRisques", new[] { "Enregistrement_id_Record" });
            DropColumn("dbo.UserRisques", "Enregistrement_id_Record");
            DropColumn("dbo.UserRisques", "QstTraite");
            DropColumn("dbo.UserRisques", "NombreQst");
            CreateIndex("dbo.UserRisques", "ApplicationUser_Id");
            AddForeignKey("dbo.UserRisques", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}

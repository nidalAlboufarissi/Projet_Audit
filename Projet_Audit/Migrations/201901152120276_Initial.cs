namespace Projet_Audit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id_question = c.Int(nullable: false, identity: true),
                        NumQuestion = c.String(),
                        MainQuestion = c.String(nullable: false),
                        Reponse = c.Boolean(nullable: false),
                        Commentaire = c.String(nullable: false),
                        Coefficient = c.Int(nullable: false),
                        Recommandation = c.String(nullable: false),
                        MesurePropose = c.String(nullable: false),
                        Id_risque = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_question)
                .ForeignKey("dbo.Risques", t => t.Id_risque, cascadeDelete: true)
                .Index(t => t.Id_risque);
            
            CreateTable(
                "dbo.Risques",
                c => new
                    {
                        Id_risque = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        score = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id_risque);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserEnregistrements",
                c => new
                    {
                        id_Record = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        UserName = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id_Record)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Nom = c.String(),
                        Prenom = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserResponses",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Reponse = c.Boolean(),
                        score = c.Int(nullable: false),
                        id_Record = c.Int(nullable: false),
                        Question_Id_question = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Questions", t => t.Question_Id_question, cascadeDelete: true)
                .ForeignKey("dbo.UserEnregistrements", t => t.id_Record, cascadeDelete: true)
                .Index(t => t.id_Record)
                .Index(t => t.Question_Id_question);
            
            CreateTable(
                "dbo.UserRisques",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        score = c.Double(nullable: false),
                        Status = c.String(),
                        Id_risque = c.Int(nullable: false),
                        UserName = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Risques", t => t.Id_risque, cascadeDelete: true)
                .Index(t => t.Id_risque)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRisques", "Id_risque", "dbo.Risques");
            DropForeignKey("dbo.UserRisques", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserResponses", "id_Record", "dbo.UserEnregistrements");
            DropForeignKey("dbo.UserResponses", "Question_Id_question", "dbo.Questions");
            DropForeignKey("dbo.UserEnregistrements", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Questions", "Id_risque", "dbo.Risques");
            DropIndex("dbo.UserRisques", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserRisques", new[] { "Id_risque" });
            DropIndex("dbo.UserResponses", new[] { "Question_Id_question" });
            DropIndex("dbo.UserResponses", new[] { "id_Record" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserEnregistrements", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Questions", new[] { "Id_risque" });
            DropTable("dbo.UserRisques");
            DropTable("dbo.UserResponses");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserEnregistrements");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Risques");
            DropTable("dbo.Questions");
        }
    }
}

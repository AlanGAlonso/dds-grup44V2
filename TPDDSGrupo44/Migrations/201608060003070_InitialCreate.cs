namespace TPDDSGrupo44.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bancoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombreDelPOI = c.String(),
                        coordenada = c.Geography(),
                        palabraClave = c.String(),
                        entreCalles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServicioBancoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        Banco_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bancoes", t => t.Banco_Id)
                .Index(t => t.Banco_Id);
            
            CreateTable(
                "dbo.HorarioAbiertoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        dia = c.Int(nullable: false),
                        numeroDeDia = c.Int(nullable: false),
                        numeroDeMes = c.Int(nullable: false),
                        horarioInicio = c.Int(nullable: false),
                        horarioFin = c.Int(nullable: false),
                        ServicioBanco_Id = c.Int(),
                        ServicioBanco_Id1 = c.Int(),
                        ServicioCGP_Id = c.Int(),
                        ServicioCGP_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServicioBancoes", t => t.ServicioBanco_Id)
                .ForeignKey("dbo.ServicioBancoes", t => t.ServicioBanco_Id1)
                .ForeignKey("dbo.ServicioCGPs", t => t.ServicioCGP_Id)
                .ForeignKey("dbo.ServicioCGPs", t => t.ServicioCGP_Id1)
                .Index(t => t.ServicioBanco_Id)
                .Index(t => t.ServicioBanco_Id1)
                .Index(t => t.ServicioCGP_Id)
                .Index(t => t.ServicioCGP_Id1);
            
            CreateTable(
                "dbo.Busquedas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        textoBuscado = c.String(),
                        cantidadDeResultados = c.Int(nullable: false),
                        fecha = c.DateTime(nullable: false),
                        terminal_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DispositivoTactils", t => t.terminal_Id)
                .Index(t => t.terminal_Id);
            
            CreateTable(
                "dbo.DispositivoTactils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        coordenada = c.Geography(),
                        nombre = c.String(),
                        comuna = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CGPs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombreDelPOI = c.String(),
                        coordenada = c.Geography(),
                        direccion = c.String(),
                        numero = c.Int(nullable: false),
                        zonaDelimitadaPorLaComuna = c.Int(nullable: false),
                        palabraClave = c.String(),
                        entreCalles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServicioCGPs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        CGP_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CGPs", t => t.CGP_Id)
                .Index(t => t.CGP_Id);
            
            CreateTable(
                "dbo.LocalComercials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombreDelPOI = c.String(),
                        coordenada = c.Geography(),
                        palabraClave = c.String(),
                        entreCalles = c.String(),
                        rubro_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rubroes", t => t.rubro_Id)
                .Index(t => t.rubro_Id);
            
            CreateTable(
                "dbo.Rubroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        radioDeCercania = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParadaDeColectivoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombreDelPOI = c.String(),
                        coordenada = c.Geography(),
                        palabraClave = c.String(),
                        entreCalles = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalComercials", "rubro_Id", "dbo.Rubroes");
            DropForeignKey("dbo.ServicioCGPs", "CGP_Id", "dbo.CGPs");
            DropForeignKey("dbo.HorarioAbiertoes", "ServicioCGP_Id1", "dbo.ServicioCGPs");
            DropForeignKey("dbo.HorarioAbiertoes", "ServicioCGP_Id", "dbo.ServicioCGPs");
            DropForeignKey("dbo.Busquedas", "terminal_Id", "dbo.DispositivoTactils");
            DropForeignKey("dbo.ServicioBancoes", "Banco_Id", "dbo.Bancoes");
            DropForeignKey("dbo.HorarioAbiertoes", "ServicioBanco_Id1", "dbo.ServicioBancoes");
            DropForeignKey("dbo.HorarioAbiertoes", "ServicioBanco_Id", "dbo.ServicioBancoes");
            DropIndex("dbo.LocalComercials", new[] { "rubro_Id" });
            DropIndex("dbo.ServicioCGPs", new[] { "CGP_Id" });
            DropIndex("dbo.Busquedas", new[] { "terminal_Id" });
            DropIndex("dbo.HorarioAbiertoes", new[] { "ServicioCGP_Id1" });
            DropIndex("dbo.HorarioAbiertoes", new[] { "ServicioCGP_Id" });
            DropIndex("dbo.HorarioAbiertoes", new[] { "ServicioBanco_Id1" });
            DropIndex("dbo.HorarioAbiertoes", new[] { "ServicioBanco_Id" });
            DropIndex("dbo.ServicioBancoes", new[] { "Banco_Id" });
            DropTable("dbo.ParadaDeColectivoes");
            DropTable("dbo.Rubroes");
            DropTable("dbo.LocalComercials");
            DropTable("dbo.ServicioCGPs");
            DropTable("dbo.CGPs");
            DropTable("dbo.DispositivoTactils");
            DropTable("dbo.Busquedas");
            DropTable("dbo.HorarioAbiertoes");
            DropTable("dbo.ServicioBancoes");
            DropTable("dbo.Bancoes");
        }
    }
}

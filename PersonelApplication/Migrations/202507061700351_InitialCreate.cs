namespace PersonelApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberContract = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(),
                        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comments = c.String(),
                        TypeContractId = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Persons", t => t.PersonId, cascadeDelete: true)
                .ForeignKey("dbo.TypesContract", t => t.TypeContractId, cascadeDelete: true)
                .Index(t => t.TypeContractId)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Persons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Age = c.Int(nullable: false),
                        Student = c.Boolean(nullable: false),
                        Contract = c.Boolean(nullable: false),
                        Comments = c.String(),
                        GroupId = c.Int(nullable: false),
                        ContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypesContract",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "TypeContractId", "dbo.TypesContract");
            DropForeignKey("dbo.Contracts", "PersonId", "dbo.Persons");
            DropForeignKey("dbo.Persons", "GroupId", "dbo.Groups");
            DropIndex("dbo.Persons", new[] { "GroupId" });
            DropIndex("dbo.Contracts", new[] { "PersonId" });
            DropIndex("dbo.Contracts", new[] { "TypeContractId" });
            DropTable("dbo.TypesContract");
            DropTable("dbo.Groups");
            DropTable("dbo.Persons");
            DropTable("dbo.Contracts");
        }
    }
}

namespace ARMeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changebooktablemigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblBooks",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Bookname = c.String(),
                        BookAuthor = c.String(),
                        NumOfBooks = c.Int(nullable: false),
                        IsAny = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.Operations",
                c => new
                    {
                        OperationId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OperationId);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        ReaderId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        HaveBooks = c.Boolean(nullable: false),
                        BookId = c.Int(nullable: false),
                        TblBooks_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ReaderId)
                .ForeignKey("dbo.tblBooks", t => t.TblBooks_Id)
                .Index(t => t.TblBooks_Id);
            
            CreateTable(
                "dbo.tblUsers",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Readers", "TblBooks_Id", "dbo.tblBooks");
            DropIndex("dbo.Readers", new[] { "TblBooks_Id" });
            DropTable("dbo.tblUsers");
            DropTable("dbo.Readers");
            DropTable("dbo.Operations");
            DropTable("dbo.tblBooks");
        }
    }
}

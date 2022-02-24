namespace ARMeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
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
                "dbo.Readers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        HaveBooks = c.Boolean(nullable: false),
                        TblBooks_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tblBooks", t => t.TblBooks_BookId)
                .Index(t => t.TblBooks_BookId);
            
            DropTable("dbo.tblBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.tblBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bookname = c.String(),
                        BookAuthor = c.String(),
                        NumOfBooks = c.Int(nullable: false),
                        IsAny = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Readers", "TblBooks_BookId", "dbo.tblBooks");
            DropIndex("dbo.Readers", new[] { "TblBooks_BookId" });
            DropTable("dbo.Readers");
            DropTable("dbo.tblBooks");
        }
    }
}

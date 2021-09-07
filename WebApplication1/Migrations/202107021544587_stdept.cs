namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stdept : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        dept_id = c.Int(nullable: false),
                        dept_name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.dept_id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 20),
                        age = c.Int(nullable: false),
                        email = c.String(),
                        password = c.String(nullable: false),
                        image = c.String(),
                        dept_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Departments", t => t.dept_id, cascadeDelete: true)
                .Index(t => t.dept_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "dept_id", "dbo.Departments");
            DropIndex("dbo.Students", new[] { "dept_id" });
            DropTable("dbo.Students");
            DropTable("dbo.Departments");
        }
    }
}

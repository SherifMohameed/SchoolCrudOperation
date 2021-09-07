namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deptcrs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeptCrs",
                c => new
                    {
                        dept_id = c.Int(nullable: false),
                        crs_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.dept_id, t.crs_id })
                .ForeignKey("dbo.Courses", t => t.crs_id, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.dept_id, cascadeDelete: true)
                .Index(t => t.dept_id)
                .Index(t => t.crs_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeptCrs", "dept_id", "dbo.Departments");
            DropForeignKey("dbo.DeptCrs", "crs_id", "dbo.Courses");
            DropIndex("dbo.DeptCrs", new[] { "crs_id" });
            DropIndex("dbo.DeptCrs", new[] { "dept_id" });
            DropTable("dbo.DeptCrs");
        }
    }
}

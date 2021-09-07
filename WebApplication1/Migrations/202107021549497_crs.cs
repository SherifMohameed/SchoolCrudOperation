namespace WebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class crs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        crs_id = c.Int(nullable: false, identity: true),
                        crs_name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.crs_id);
            
            CreateTable(
                "dbo.StuCrs",
                c => new
                    {
                        st_id = c.Int(nullable: false),
                        crs_id = c.Int(nullable: false),
                        garde = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.st_id, t.crs_id })
                .ForeignKey("dbo.Courses", t => t.crs_id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.st_id, cascadeDelete: true)
                .Index(t => t.st_id)
                .Index(t => t.crs_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StuCrs", "st_id", "dbo.Students");
            DropForeignKey("dbo.StuCrs", "crs_id", "dbo.Courses");
            DropIndex("dbo.StuCrs", new[] { "crs_id" });
            DropIndex("dbo.StuCrs", new[] { "st_id" });
            DropTable("dbo.StuCrs");
            DropTable("dbo.Courses");
        }
    }
}

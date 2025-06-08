namespace AIUBStudentPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DropApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DropApplications", "StudentId", "dbo.Students");
            DropForeignKey("dbo.DropApplications", "CourseId", "dbo.Courses");
            DropIndex("dbo.DropApplications", new[] { "CourseId" });
            DropIndex("dbo.DropApplications", new[] { "StudentId" });
            DropTable("dbo.DropApplications");
        }
    }
}

namespace AIUBStudentPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Section = c.String(),
                        Schedule = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StudentCourseRegistrations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourseRegistrations", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "CourseId" });
            DropTable("dbo.StudentCourseRegistrations");
            DropTable("dbo.Courses");
        }
    }
}

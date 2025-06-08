namespace AIUBStudentPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb3 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.StudentCourseRegistrations", "StudentId");
            AddForeignKey("dbo.StudentCourseRegistrations", "StudentId", "dbo.Students", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourseRegistrations", "StudentId", "dbo.Students");
            DropIndex("dbo.StudentCourseRegistrations", new[] { "StudentId" });
        }
    }
}

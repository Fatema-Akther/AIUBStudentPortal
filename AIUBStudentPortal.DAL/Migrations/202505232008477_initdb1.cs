namespace AIUBStudentPortal.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initdb1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "PasswordHash", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "PasswordHash");
        }
    }
}

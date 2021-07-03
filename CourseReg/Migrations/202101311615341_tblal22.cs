namespace CourseReg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblal22 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CourseRegs", newName: "CourseRegistartions");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CourseRegistartions", newName: "CourseRegs");
        }
    }
}

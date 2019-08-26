namespace NotOtomasyonu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Rol");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Rol", c => c.String());
        }
    }
}

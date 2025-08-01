﻿namespace HC_Pharma.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleToAdminClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Admins", "Role", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "Role");
        }
    }
}

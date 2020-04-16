namespace BeautyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblPromotionAttendee01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Attendees", "PhoneNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Attendees", "Address", c => c.String(nullable: false));
            AlterColumn("dbo.Attendees", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Attendees", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Attendees", "LastName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Attendees", "LastName", c => c.String());
            AlterColumn("dbo.Attendees", "FirstName", c => c.String());
            AlterColumn("dbo.Attendees", "Email", c => c.String());
            AlterColumn("dbo.Attendees", "Address", c => c.String());
            AlterColumn("dbo.Attendees", "PhoneNumber", c => c.String());
        }
    }
}

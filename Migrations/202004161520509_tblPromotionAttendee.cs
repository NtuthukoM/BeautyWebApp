namespace BeautyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblPromotionAttendee : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PromotionAttendees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PromotionId = c.Int(nullable: false),
                        AttendeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PromotionAttendees");
        }
    }
}

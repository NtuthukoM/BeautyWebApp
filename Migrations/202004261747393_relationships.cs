namespace BeautyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PromotionCreators", "Promotion_Id", "dbo.Promotions");
            DropIndex("dbo.PromotionCreators", new[] { "Promotion_Id" });
            CreateIndex("dbo.PromotionAttendees", "PromotionId");
            CreateIndex("dbo.PromotionAttendees", "AttendeeId");
            CreateIndex("dbo.Promotions", "PromotionCreatorId");
            AddForeignKey("dbo.PromotionAttendees", "AttendeeId", "dbo.Attendees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Promotions", "PromotionCreatorId", "dbo.PromotionCreators", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PromotionAttendees", "PromotionId", "dbo.Promotions", "Id", cascadeDelete: true);
            DropColumn("dbo.PromotionCreators", "Promotion_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PromotionCreators", "Promotion_Id", c => c.Int());
            DropForeignKey("dbo.PromotionAttendees", "PromotionId", "dbo.Promotions");
            DropForeignKey("dbo.Promotions", "PromotionCreatorId", "dbo.PromotionCreators");
            DropForeignKey("dbo.PromotionAttendees", "AttendeeId", "dbo.Attendees");
            DropIndex("dbo.Promotions", new[] { "PromotionCreatorId" });
            DropIndex("dbo.PromotionAttendees", new[] { "AttendeeId" });
            DropIndex("dbo.PromotionAttendees", new[] { "PromotionId" });
            CreateIndex("dbo.PromotionCreators", "Promotion_Id");
            AddForeignKey("dbo.PromotionCreators", "Promotion_Id", "dbo.Promotions", "Id");
        }
    }
}

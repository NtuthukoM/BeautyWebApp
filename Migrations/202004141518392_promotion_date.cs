namespace BeautyWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class promotion_date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Promotions", "PromotionDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Promotions", "PromotionDate");
        }
    }
}

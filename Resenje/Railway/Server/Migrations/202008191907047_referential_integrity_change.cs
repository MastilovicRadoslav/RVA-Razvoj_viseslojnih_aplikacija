namespace Server.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class referential_integrity_change : DbMigration
	{
		public override void Up()
		{
			DropForeignKey("dbo.Places", "Country_Id", "dbo.Countries");
			DropForeignKey("dbo.Stations", "Place_Id", "dbo.Places");
			DropIndex("dbo.Places", new[] { "Country_Id" });
			DropIndex("dbo.Roads", new[] { "Label" });
			DropIndex("dbo.Stations", new[] { "Place_Id" });
			AlterColumn("dbo.Places", "Country_Id", c => c.Int(nullable: false));
			AlterColumn("dbo.Stations", "Place_Id", c => c.Int(nullable: false));
			CreateIndex("dbo.Places", "Country_Id");
			CreateIndex("dbo.Stations", "Place_Id");
			AddForeignKey("dbo.Places", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
			AddForeignKey("dbo.Stations", "Place_Id", "dbo.Places", "Id", cascadeDelete: true);
		}

		public override void Down()
		{
			DropForeignKey("dbo.Stations", "Place_Id", "dbo.Places");
			DropForeignKey("dbo.Places", "Country_Id", "dbo.Countries");
			DropIndex("dbo.Stations", new[] { "Place_Id" });
			DropIndex("dbo.Places", new[] { "Country_Id" });
			AlterColumn("dbo.Stations", "Place_Id", c => c.Int());
			AlterColumn("dbo.Places", "Country_Id", c => c.Int());
			CreateIndex("dbo.Stations", "Place_Id");
			CreateIndex("dbo.Roads", "Label", unique: true);
			CreateIndex("dbo.Places", "Country_Id");
			AddForeignKey("dbo.Stations", "Place_Id", "dbo.Places", "Id");
			AddForeignKey("dbo.Places", "Country_Id", "dbo.Countries", "Id");
		}
	}
}

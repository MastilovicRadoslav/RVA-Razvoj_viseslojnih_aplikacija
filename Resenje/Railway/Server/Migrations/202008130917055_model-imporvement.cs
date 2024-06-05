namespace Server.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class modelimporvement : DbMigration
	{
		public override void Up()
		{
			DropForeignKey("dbo.Places", "Country_Id", "dbo.Countries");
			DropForeignKey("dbo.Stations", "Place_Id", "dbo.Places");
			DropForeignKey("dbo.Tracks", "Station_Id", "dbo.Stations");
			AddForeignKey("dbo.Places", "Country_Id", "dbo.Countries", "Id");
			AddForeignKey("dbo.Stations", "Place_Id", "dbo.Places", "Id");
			AddForeignKey("dbo.Tracks", "Station_Id", "dbo.Stations", "Id");
		}

		public override void Down()
		{
			DropForeignKey("dbo.Tracks", "Station_Id", "dbo.Stations");
			DropForeignKey("dbo.Stations", "Place_Id", "dbo.Places");
			DropForeignKey("dbo.Places", "Country_Id", "dbo.Countries");
			AddForeignKey("dbo.Tracks", "Station_Id", "dbo.Stations", "Id", cascadeDelete: true);
			AddForeignKey("dbo.Stations", "Place_Id", "dbo.Places", "Id", cascadeDelete: true);
			AddForeignKey("dbo.Places", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
		}
	}
}

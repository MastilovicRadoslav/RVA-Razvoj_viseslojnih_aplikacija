namespace Server.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.Countries",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Name = c.String(maxLength: 8000, unicode: false),
				})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Name, unique: true);

			CreateTable(
				"dbo.Places",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Name = c.String(maxLength: 8000, unicode: false),
					Country_Id = c.Int(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Countries", t => t.Country_Id)
				.Index(t => t.Country_Id);

			CreateTable(
				"dbo.Roads",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Label = c.String(maxLength: 8000, unicode: false),
					Name = c.String(maxLength: 8000, unicode: false),
				})
				.PrimaryKey(t => t.Id)
				.Index(t => t.Label, unique: true);

			CreateTable(
				"dbo.Stations",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Name = c.String(maxLength: 8000, unicode: false),
					TrackNumber = c.Int(nullable: false),
					Place_Id = c.Int(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Places", t => t.Place_Id)
				.Index(t => t.Name, unique: true)
				.Index(t => t.Place_Id);

			CreateTable(
				"dbo.Tracks",
				c => new
				{
					Id = c.Int(nullable: false, identity: true),
					Entrance = c.Int(nullable: false),
					Label = c.String(maxLength: 3, unicode: false),
					Name = c.String(maxLength: 8000, unicode: false),
					Station_Id = c.Int(),
				})
				.PrimaryKey(t => t.Id)
				.ForeignKey("dbo.Stations", t => t.Station_Id)
				.Index(t => t.Label, unique: true)
				.Index(t => t.Station_Id);

			CreateTable(
				"dbo.Users",
				c => new
				{
					ID = c.Int(nullable: false, identity: true),
					IsAdmin = c.Boolean(nullable: false),
					LastName = c.String(maxLength: 8000, unicode: false),
					Name = c.String(maxLength: 8000, unicode: false),
					Password = c.String(maxLength: 8000, unicode: false),
					UserName = c.String(maxLength: 8000, unicode: false),
				})
				.PrimaryKey(t => t.ID)
				.Index(t => t.UserName, unique: true);

			CreateTable(
				"dbo.RoadStations",
				c => new
				{
					StationID = c.Int(nullable: false),
					RoadID = c.Int(nullable: false),
				})
				.PrimaryKey(t => new { t.StationID, t.RoadID })
				.ForeignKey("dbo.Stations", t => t.StationID, cascadeDelete: true)
				.ForeignKey("dbo.Roads", t => t.RoadID, cascadeDelete: true)
				.Index(t => t.StationID)
				.Index(t => t.RoadID);

		}

		public override void Down()
		{
			DropForeignKey("dbo.Tracks", "Station_Id", "dbo.Stations");
			DropForeignKey("dbo.RoadStations", "RoadID", "dbo.Roads");
			DropForeignKey("dbo.RoadStations", "StationID", "dbo.Stations");
			DropForeignKey("dbo.Stations", "Place_Id", "dbo.Places");
			DropForeignKey("dbo.Places", "Country_Id", "dbo.Countries");
			DropIndex("dbo.RoadStations", new[] { "RoadID" });
			DropIndex("dbo.RoadStations", new[] { "StationID" });
			DropIndex("dbo.Users", new[] { "UserName" });
			DropIndex("dbo.Tracks", new[] { "Station_Id" });
			DropIndex("dbo.Tracks", new[] { "Label" });
			DropIndex("dbo.Stations", new[] { "Place_Id" });
			DropIndex("dbo.Stations", new[] { "Name" });
			DropIndex("dbo.Roads", new[] { "Label" });
			DropIndex("dbo.Places", new[] { "Country_Id" });
			DropIndex("dbo.Countries", new[] { "Name" });
			DropTable("dbo.RoadStations");
			DropTable("dbo.Users");
			DropTable("dbo.Tracks");
			DropTable("dbo.Stations");
			DropTable("dbo.Roads");
			DropTable("dbo.Places");
			DropTable("dbo.Countries");
		}
	}
}

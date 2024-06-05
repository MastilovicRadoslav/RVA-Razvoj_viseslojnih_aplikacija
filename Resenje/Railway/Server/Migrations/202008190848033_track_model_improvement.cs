namespace Server.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class track_model_improvement : DbMigration
	{
		public override void Up()
		{
			RenameColumn(table: "dbo.Tracks", name: "Station_Id", newName: "StationId");
			RenameIndex(table: "dbo.Tracks", name: "IX_Station_Id", newName: "IX_StationId");
		}

		public override void Down()
		{
			RenameIndex(table: "dbo.Tracks", name: "IX_StationId", newName: "IX_Station_Id");
			RenameColumn(table: "dbo.Tracks", name: "StationId", newName: "Station_Id");
		}
	}
}

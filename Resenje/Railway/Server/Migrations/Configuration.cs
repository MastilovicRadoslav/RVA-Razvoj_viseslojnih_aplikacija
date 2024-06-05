namespace Server.Migrations
{
	using Server.Database;
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<RailwayContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			AutomaticMigrationDataLossAllowed = false;
			ContextKey = "RailwayContext";

		}

		protected override void Seed(RailwayContext context)
		{
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method
			//  to avoid creating duplicate seed data.
		}
	}
}

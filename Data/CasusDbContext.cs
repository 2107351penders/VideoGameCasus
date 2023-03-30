using Microsoft.EntityFrameworkCore;
using VideoGameCasus.Models;

namespace VideoGameCasus.Data
{
	public class CasusDbContext : DbContext
	{
        public CasusDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default;
		public DbSet<Game> Games { get; set; } = default;
		public DbSet<GameList> gameLists { get; set; } = default;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Seed database met standaard data gedefinieerd in SeedVideos() en SeedOnderwerpen()
			modelBuilder.Entity<User>().HasData(SeedUsers());
			modelBuilder.Entity<Game>().HasData(SeedGames());
			modelBuilder.Entity<GameList>().HasData(SeedGameLists());
		}

		private List<User> SeedUsers()
		{
			List<User> users = new List<User>();
			return users;
		}

		private List<Game> SeedGames()
		{
			List<Game> games = new List<Game>();
			return games;
		}

		private List<GameList> SeedGameLists()
		{
			List<GameList> gameLists = new List<GameList>();
			return gameLists;
		}
	}
}

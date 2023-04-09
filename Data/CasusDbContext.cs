using Microsoft.EntityFrameworkCore;
using VideoGameCasus.Models;
using static System.Net.WebRequestMethods;

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
			modelBuilder.Entity<User>().HasData(SeedUsers());
			modelBuilder.Entity<Game>().HasData(SeedGames());
			modelBuilder.Entity<GameList>().HasData(SeedGameLists());
		}

		private List<User> SeedUsers()
		{
			List<User> users = new List<User>();
			users.Add(new User { Id = -1, Name = "Tim", Password = "password" });
			return users;
		}

		private List<Game> SeedGames()
		{
			List<Game> games = new List<Game>();
			games.Add(new Game
			{
				Id = -1,
				Name = "Seed Game",
				Summary = "Example game from the database seed",
				Publisher = "Nobody",
				ReleaseDate = "Never",
				Genre = "Fake",
				Platform = "None",
				Cover = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1txv.png",
				Finished = false,
				GameListId = -1
			});
			return games;
		}

		private List<GameList> SeedGameLists()
		{
			List<GameList> gameLists = new List<GameList>();
			gameLists.Add(new GameList
			{
				Id = -1,
				UserId = -1
			});
			return gameLists;
		}
	}
}

namespace VideoGameCasus.Models
{
	public class Game
	{
		public int Id { get; set; }
        public string Name { get; set; }
		public string Summary { get; set; }
		public string Publisher { get; set; }
        public string ReleaseDate { get; set; }
        public string Genre { get; set; }
		public string Platform { get; set; }
		public string Cover { get; set; }
		public bool Finished { get; set; }
        public int GameListId { get; set; }
		public GameList GameList { get; set; }
    }
}

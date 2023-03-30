namespace VideoGameCasus.Models
{
	public class GameList
	{
        public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
    }
}

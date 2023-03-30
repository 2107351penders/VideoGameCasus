namespace VideoGameCasus.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; } // TODO: dit moet geen plaintext zijn
	}
}

using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideoGameCasus.Data;
using VideoGameCasus.Models;

namespace VideoGameCasus.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        private CasusDbContext _context;
        public string userName { get; set; }
		public User User { get; set; }
		public GameList gameList { get; set; }
		public List<Game> games = new List<Game>();

        public IndexModel(ILogger<IndexModel> logger, CasusDbContext dbContext)
		{
			_logger = logger;
			_context = dbContext;
		}

		public IActionResult OnGet()
		{
            if (Request.Cookies["CasusAuth"] != null)
            {
				userName = Request.Cookies["CasusAuth"];

				User = (from User in _context.Users
					   where User.Name.Equals(userName)
					   select User).First();
				
				gameList = (from gameList in _context.gameLists
						   where gameList.User == User
						   select gameList).First();

				games = (from Game in _context.Games
						where Game.GameList == gameList
						select Game).ToList();

                return Page();
            }
            else
            {
				return RedirectToPage("/Login", new { newUser = false });
            }
        }

		public IActionResult OnGetLogOut()
		{
            if (Request.Cookies["CasusAuth"] != null)
			{
				Response.Cookies.Append("CasusAuth", Request.Cookies["CasusAuth"], new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
            }
			return RedirectToPage("/Index");
        }

		public IActionResult OnGetAddGame()
		{
			userName = Request.Cookies["CasusAuth"];

			User = (from User in _context.Users
                    where User.Name.Equals(userName)
                    select User).First();

            gameList = (from gameList in _context.gameLists
                        where gameList.User == User
                        select gameList).First();
            
			return RedirectToPage("/AddGame", new { gameListId = gameList.Id });
		}
	}
}
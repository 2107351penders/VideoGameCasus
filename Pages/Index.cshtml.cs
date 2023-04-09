using System;
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
        public string userName { get; set; }
		private CasusDbContext _context;
		private List<Game> games = new List<Game>();

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

                games = _context.Games.ToList(); // Nog filteren op user

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
	}
}
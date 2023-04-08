using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VideoGameCasus.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger<IndexModel> _logger;
        public string userName { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}

		public IActionResult OnGet()
		{
            if (Request.Cookies["CasusAuth"] != null)
            {
				userName = Request.Cookies["CasusAuth"];
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
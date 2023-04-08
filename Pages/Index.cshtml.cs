using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Web;

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
				return RedirectToPage("/Login");
            }
        }
	}
}
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameCasus.Data;
using VideoGameCasus.Models;

namespace VideoGameCasus.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public String userName { get; set; }

        [BindProperty]
        public String password { get; set; }

        public bool unknownLogin { get; set; } = false;
        private CasusDbContext _context;
        public bool newUser { get; set; } = false;

        public LoginModel(CasusDbContext injectedContext)
        {
            _context = injectedContext;
        }

        public IActionResult OnGet(bool newUser)
        {
            this.newUser = newUser;

            if (Request.Cookies["CasusAuth"] != null)
            {
                // User is al ingelogd!
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public ActionResult OnPost()
        {
            List<User> users = _context.Users.ToList();
            foreach (User user in users)
            {
                if (user.Name.Equals(userName))
                {
                    if (user.Password.Equals(password))
                    {
                        unknownLogin = false;
                        Response.Cookies.Append("CasusAuth", userName);
                        return RedirectToPage("/Index");
                    }
                }
            }
            unknownLogin = true;
            return Page();
        }
    }
}

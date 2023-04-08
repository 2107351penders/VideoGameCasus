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
        
        public LoginModel(CasusDbContext injectedContext)
        {
            _context = injectedContext;
        }
        
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
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
                        // login
                    }
                }
            }
            unknownLogin = true;
            return Page();
        }
    }
}

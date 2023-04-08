using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideoGameCasus.Data;

namespace VideoGameCasus.Pages
{
    public class SignupModel : PageModel
    {
        [BindProperty]
        public string userName { get; set; }
        [BindProperty]
        public string password { get; set; }
        public bool passwordComplexityError { get; set; }
        private CasusDbContext dbContext { get; set; }

        public SignupModel(CasusDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void OnGet()
        {
        }

        // Return true if password meets requirements, false otherwise
        private bool passwordComplexity(String password)
        {
            if (password.Length < 12)
            {
                return false;
            }
            return true;
        }

        public ActionResult OnPost()
        {
            if (passwordComplexity(password))
            {
                dbContext.Users.Add(new Models.User { Name = userName, Password = password });
                dbContext.SaveChanges();
                return RedirectToPage("/Login", new { newUser = true });  ; ;
            }
            else
            {
                passwordComplexityError = true;
                return Page();
            }
        }
    }
}

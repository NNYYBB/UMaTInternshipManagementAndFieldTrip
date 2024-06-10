using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace TrainingUnitManagementSystem.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public RegisterModel(SignInManager<IdentityUser> signInManager ,UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
            public InputModel Input {get; set;}
            public string ReturnUrl {get; set;}
            public void OnGet()
        { 
             ReturnUrl = Url.Content("~/");
        }

        public async Task<ActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");
            if (ModelState.IsValid)
            { 
                var identity = new IdentityUser (userName = Input.Email, Email = Input.Email);
                var result = await _userManager.CreateAsync(Identity, Input.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect(ReturnUrl);
                }
                return Page();



            }
            return Page();
        }


        public class InputModel
        { 
            [Required]
            [EmailAddress]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        
        }
    }
}

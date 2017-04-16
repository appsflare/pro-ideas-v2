using System.ComponentModel.DataAnnotations;

namespace ProIdeas.UI.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

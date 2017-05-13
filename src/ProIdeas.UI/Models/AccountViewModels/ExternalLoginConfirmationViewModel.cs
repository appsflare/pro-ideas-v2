using System.ComponentModel.DataAnnotations;

namespace ProIdeas.UI.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
    }
}

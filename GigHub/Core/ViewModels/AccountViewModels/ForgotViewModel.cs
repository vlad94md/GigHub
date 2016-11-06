using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.ViewModels.AccountViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
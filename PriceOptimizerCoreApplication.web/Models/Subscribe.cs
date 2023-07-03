using System.ComponentModel.DataAnnotations;

namespace PriceOptimizerCoreApplication.web.Models
{
    public class Subscribe
    {
        [Required]
        [Display(Name = "Your Email Address")]
        [EmailAddress]
        public string  EmailAddress { get; set; }
        public string RequestVerificationToken { get; set; }
    }
}

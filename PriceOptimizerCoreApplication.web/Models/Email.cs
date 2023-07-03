using System.ComponentModel.DataAnnotations;

namespace PriceOptimizerCoreApplication.web.Models
{
    public class Email
    {
        [Required]
        [Display(Name = "Your Email Address")]
        [EmailAddress]
        public string  To { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Your Message")]
        public string Body { get; set; }
        [Required]
        [Display(Name = "Your Name")]
        public string  Name { get; set; }
        public string  FromEmail { get; set; }
        public string FromPassword { get; set; }
        public bool IsBodyHtml
        {
            get;
            set;
        }
        public string RequestVerificationToken { get; set; }
    }
}

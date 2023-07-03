using System.ComponentModel.DataAnnotations;

namespace PriceOptimizerCoreApplication.web.Models
{
    public class ServiceRequest
    {
        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Your Email Address")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Your Moblile")]
        public string Moblile { get; set; }
        [Required]
        [Display(Name = "Requested Service")]
        public string ServiceName { get; set; }
        [Required]
        [Display(Name = "Your Message")]
        public string Description { get; set; }
    }
}

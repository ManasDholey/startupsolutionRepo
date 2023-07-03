using System.ComponentModel.DataAnnotations;

namespace PriceOptimizerCoreApplication.web.Domain
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}


namespace PriceOptimizerCoreApplication.web.Util
{
    public class Credential
    {
        private string Email;
        private string Password;
        private string ToEmail;
        public Credential()
        {
            Email = "**********************";
            Password = "*********************";
            ToEmail = "*****************************";
        }
        public string  GetEmail { 
            get { return this.Email.ToString(); } 
        }
        public string GetPassword { 
            get { return this.Password.ToString(); }
        }

        public string GetToEmail
        {
            get { return ToEmail.ToString(); }
        }

    }
}

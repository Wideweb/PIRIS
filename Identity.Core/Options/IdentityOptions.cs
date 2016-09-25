namespace Identity.Core.Options
{
    public class IdentityOptions
    {
        public int PasswordSaltKeySize { get; set; }
        public int LoginAttempts { get; set; }
    }
}

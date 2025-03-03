namespace TheUsers.Domain.Models.Exceptions
{
    public class UnauthorizedException : Exception
    {
        private const string ErrorMessage = "Unauthorized.";
        public UnauthorizedException() : base(ErrorMessage)
        {
        }
    }
}

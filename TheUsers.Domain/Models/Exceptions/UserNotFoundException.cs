namespace TheUsers.Domain.Models.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private const string ErrorMessage = "User not found.";
        public UserNotFoundException() : base(ErrorMessage)
        {
        }
    }
}

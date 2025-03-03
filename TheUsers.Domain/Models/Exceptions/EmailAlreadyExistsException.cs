namespace TheUsers.Domain.Models.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        private const string ErrorMessage = "Email already exists.";
        public EmailAlreadyExistsException() : base(ErrorMessage)
        {
        }
    }
}
